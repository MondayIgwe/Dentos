using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.BillingRulesValidationList;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace Elite3E.RegressionTests.Data
{
    public class BillingRulesValidationListData
    {
        private readonly IProcessService _process = new ProcessService();
        private readonly ISessionService _session = new SessionService();
        private readonly ILookUpService _lookUpService = new LookUpService();
        private readonly IBillingRulesValidationListService _billingRulesValidationListService = new BillingRulesValidationListService();
        private IRestResponse _response;

        public async Task<string> SearchOrCreateBillingRulesValidationListForProformaTimeCheckAsync(string code, string description)
        {
            var billingRules = new ApiBillingRulesValidationListEntity
            {
                Code = (string.IsNullOrEmpty(code)) ? "Proforma Time_Check the negative and positive amounts" : code,
                Description = (string.IsNullOrEmpty(description)) ? "Proforma Time_Check the negative and positive amounts" : description,
                BillingRulesValidationListRules = new List<RulesList>
                {
                    new RulesList()
                    {
                        BillRuleDescrption = "Check the corresponding negative and positive amounts of GWTMU",
                        IsProformaEdit = "1",
                        IsBillError = "1"
                    }
                }
            };

            return await SearchOrCreateBillingRulesValidationListAsync(billingRules);
        }

        public async Task<string> SearchOrCreateBillingRulesValidationListForProformaPeriodCheckAsync(string code, string description)
        {
            var billingRules = new ApiBillingRulesValidationListEntity
            {
                Code = (string.IsNullOrEmpty(code)) ? "Proforma Date period" : code,
                Description = (string.IsNullOrEmpty(description)) ? "Proforma Date period" : description,
                BillingRulesValidationListRules = new List<RulesList>
                {
                    new RulesList()
                    {
                        BillRuleDescrption = "Checks the existence of Costcards outside the Invoice Period",
                        IsProformaEdit = "1",
                        IsBillError = "1"
                    },
                    new RulesList()
                    {
                        BillRuleDescrption = "Checks the existence of Timecards outside the Invoice Period",
                        IsProformaEdit = "1",
                        IsBillError = "1"
                    }
                }

            };

            return await SearchOrCreateBillingRulesValidationListAsync(billingRules);
        }



        public async Task<string> SearchOrCreateBillingRulesValidationListAsync(ApiBillingRulesValidationListEntity billingRules)
        {
             //Get Session Id

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.BillRulesValdationList);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, billingRules.Code.String);
            var existingBillingRules = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingBillingRules.Rows != null)
            {
                if (existingBillingRules.Rows.FirstOrDefault().Attributes.Code.Equals(billingRules.Code.String))
                {
                    _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                    _response.IsSuccessful.Should().BeTrue();

                    Console.WriteLine("The Given Billing Rule Validation List Code Exists : " + billingRules.Code.String);
                    return billingRules.Code.String;
                }
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.BillRulesValdationList);
            _response.IsSuccessful.Should().BeTrue();

            var billingRuleId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            AssertionExtensions.Should((string)billingRuleId).NotBeNull();
            Console.WriteLine("Entity Id: " + billingRuleId);
            billingRules.Id = billingRuleId;

            _response = await _billingRulesValidationListService.AddBillingRulesDataAsync(processItemId, sessionId, billingRules);
            _response.IsSuccessful.Should().BeTrue();

            foreach (var rule in billingRules.BillingRulesValidationListRules)
            {

                //Get  Billing Rules Validation list rules child objects row Id
                _response = await _billingRulesValidationListService.GetBillingRulesValidationListRulesAsync(sessionId, processItemId, billingRules);
                _response.IsSuccessful.Should().BeTrue();
                rule.RowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
                rule.RowId.Should().NotBeNull();
                Console.WriteLine("Billing Rules Validation List Rules Id: " + rule.RowId);

                _response = await _billingRulesValidationListService.GetBillingRulesForBillingListAsync(sessionId, processItemId, billingRules, rule);

                _response.IsSuccessful.Should().BeTrue();

                var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

                rule.BillRuleCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(rule.BillRuleDescrption)).Attributes.Code;

                _response = await _billingRulesValidationListService.AddBillingRulesValidationListRulesAsync(sessionId, processItemId, billingRules, rule);

                _response.IsSuccessful.Should().BeTrue();

                if (!string.IsNullOrEmpty(rule.IsPendingWarning.String))
                    await AddPendingWarning(billingRules, sessionId, processItemId, rule);

                if (!string.IsNullOrEmpty(rule.IsPostWarning.String))
                    await AddPostWarning(billingRules, sessionId, processItemId, rule);

                if (!string.IsNullOrEmpty(rule.IsProformaGen.String))
                    await AddProformaGenrationWarning(billingRules, sessionId, processItemId, rule);

                if (!string.IsNullOrEmpty(rule.IsProformaEdit.String))
                    await AddProformaEditWarning(billingRules, sessionId, processItemId, rule);

                if (!string.IsNullOrEmpty(rule.IsBillWarning.String))
                    await AddBillWarning(billingRules, sessionId, processItemId, rule);

                if (!string.IsNullOrEmpty(rule.IsPendingError.String))
                    await AddPendingError(billingRules, sessionId, processItemId, rule);

                if (!string.IsNullOrEmpty(rule.IsPostError.String))
                    await AddPostError(billingRules, sessionId, processItemId, rule);

                if (!string.IsNullOrEmpty(rule.IsBillError.String))
                    await AddBillError(billingRules, sessionId, processItemId, rule);
            }

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.BillRulesValdationList);
            _response.Content.Should().Contain("responseType\":1");
            if (_response.IsSuccessful)
            {
                Console.WriteLine("Billing rules validation list  Created : " + billingRules.Code.String);
                return billingRules.Code.String;
            }

            Console.WriteLine("Creation of Billing Rules validation list failed: " + _response.StatusCode);
            return null;

        }

        private async Task AddPendingWarning(ApiBillingRulesValidationListEntity billingRules, string sessionId, string processItemId, RulesList rule)
        {
            _response = await _billingRulesValidationListService.AddBillingRulesPendingWarningAsync(sessionId, processItemId, billingRules, rule);

            _response.IsSuccessful.Should().BeTrue();
        }
        private async Task AddPostWarning(ApiBillingRulesValidationListEntity billingRules, string sessionId, string processItemId, RulesList rule)
        {
            _response = await _billingRulesValidationListService.AddBillingRulesPostWarningAsync(sessionId, processItemId, billingRules, rule);

            _response.IsSuccessful.Should().BeTrue();
        }
        private async Task AddProformaGenrationWarning(ApiBillingRulesValidationListEntity billingRules, string sessionId, string processItemId, RulesList rule)
        {
            _response = await _billingRulesValidationListService.AddBillingRulesProformaGenrationWarningAsync(sessionId, processItemId, billingRules, rule);

            _response.IsSuccessful.Should().BeTrue();
        }
        private async Task AddProformaEditWarning(ApiBillingRulesValidationListEntity billingRules, string sessionId, string processItemId, RulesList rule)
        {
            _response = await _billingRulesValidationListService.AddBillingRulesProformaEditWarningAsync(sessionId, processItemId, billingRules, rule);

            _response.IsSuccessful.Should().BeTrue();
        }
        private async Task AddBillWarning(ApiBillingRulesValidationListEntity billingRules, string sessionId, string processItemId, RulesList rule)
        {
            _response = await _billingRulesValidationListService.AddBillingRulesBillWarningAsync(sessionId, processItemId, billingRules, rule);

            _response.IsSuccessful.Should().BeTrue();
        }
        private async Task AddPendingError(ApiBillingRulesValidationListEntity billingRules, string sessionId, string processItemId, RulesList rule)
        {
            _response = await _billingRulesValidationListService.AddBillingRulesPendingErrorAsync(sessionId, processItemId, billingRules, rule);

            _response.IsSuccessful.Should().BeTrue();
        }
        private async Task AddPostError(ApiBillingRulesValidationListEntity billingRules, string sessionId, string processItemId, RulesList rule)
        {
            _response = await _billingRulesValidationListService.AddBillingRulesPostErrorAsync(sessionId, processItemId, billingRules, rule);

            _response.IsSuccessful.Should().BeTrue();
        }
        private async Task AddBillError(ApiBillingRulesValidationListEntity billingRules, string sessionId, string processItemId, RulesList rule)
        {
            _response = await _billingRulesValidationListService.AddBillingRulesBillErrorAsync(sessionId, processItemId, billingRules, rule);

            _response.IsSuccessful.Should().BeTrue();
        }
    }
}
