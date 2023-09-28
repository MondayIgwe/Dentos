using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Matter;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ChildForm;
using Elite3E.RestServices.Services.MatterService;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace Elite3E.RegressionTests.RestServicesTest
{

    public class MatterTest
    {
        public IMatterService _matterService = new MatterService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IOpenChildFormService _childFormService = new OpenChildFormService();
        public IProcessDataService _processDataService = new ProcessDataService();
        public IRestResponse _response;

        [Test]
        public async Task CreateMatter()
        {

            ApiMatterEntity matter = new ApiMatterEntity()
            {
                Client = "Client_Automationat_HTOvMOn",
                Status = "Fully Open",
                OpenDate = "11/3/2021",
                MatterName = "TestMatter" + 4,
                Currency = "GBP - British Pound",
                MatterCurrencyMethod = "Bill",
                Office = "London (EU)",
                Department = "UKME_Banking",
                Section = "UKME_Admin",
                Rate = "Standard",
                ChargeTypeGroupList = new List<string> { "Desc_at_3e8glw7" },
                CostTypeGroupList = new List<string> { "Desc_at_VowqCrR" },
                BillingGroupList = new List<string> { "Test Billing Group" }
            };

            //Get Session Id

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.MatterProcessName);
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.MatterProcessName);

            var matterId = JsonConvert.DeserializeObject<AddProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            matterId.Should().NotBeNull();
            matter.MatterId = matterId;
            Console.WriteLine("Matter Id: " + matterId);

            _response = await _matterService.GetMatterClientAsync(sessionId, processItemId, matter);
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            matter.ClientAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(matter.Client)).Alias;
            matter.ClientRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(matter.Client)).RowKey;

            matter.StatusCode = await LookUp.GetLookUpKeyValue(sessionId, "MattStatus", matter.Status);
            matter.CurrencyCode = await LookUp.GetCurrencyLookUpKeyValue(sessionId, matter.Currency);
            matter.CurrencyMethodCode = await LookUp.GetLookUpKeyValue(sessionId, "CurrencyDateList", matter.MatterCurrencyMethod);


            _response = await _matterService.AddMatterAsync(processItemId, sessionId, matter);
            _response.Should().NotBeNull();

            _response = await _matterService.GetEffectiveDatedRowInformationAsync(processItemId, sessionId, matter);

            var effectiveDateRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 2); // ToDo: Write a dynamic reader using key value 
            effectiveDateRowId.Should().NotBeNull();
            Console.WriteLine("Effective date Row Id: " + effectiveDateRowId);

            matter.OfficeKey = await LookUp.GetLookUpKeyValue(sessionId, "Office", matter.Office);
            matter.DepartmentKey = await LookUp.GetLookUpKeyValue(sessionId, "Department", matter.Department);
            matter.SectionKey = await LookUp.GetLookUpKeyValue(sessionId, "Section", matter.Section);

            _response = await _matterService.AddEffectiveDatedInformationAsync(processItemId, sessionId, effectiveDateRowId, matter);

            _response = await _matterService.GetMatterRateAsync(processItemId, sessionId, matter);
            var mattertRateRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 2); // ToDo: Write a dynamic reader using key value 
            mattertRateRowId.Should().NotBeNull();
            Console.WriteLine("Matter Rate  Row Id: " + mattertRateRowId);

            _response = await _matterService.GetMatterRateAsync(sessionId, processItemId, mattertRateRowId, matter);
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            matter.RateAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.Rate)).Alias;
            matter.RateCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.Rate)).Attributes.Code;

            _response = await _matterService.AddMatterRateAsync(processItemId, sessionId, mattertRateRowId, matter);
            _response.Should().NotBeNull();

            //Add new Billing Information then submit
            _response = await _matterService.GetNewBillingSiteAsync(processItemId, sessionId, matter);
            _response?.Should().NotBeNull();

            matter.SiteId = new List<string>();

            matter.SiteId.Add(JsonHelper.GetTagValue2(_response.Content, "currentRowId"));
            matter.SiteId.Add(JsonHelper.GetTagValue2(_response.Content, "popupInstanceId"));


            _response = await _matterService.AddNewBillingSiteAsync(processItemId, sessionId, matter);
            _response.IsSuccessful.Should().BeTrue();
            _response = await _matterService.PostBillingSiteDataAsync(processItemId, sessionId, matter);
            _response.IsSuccessful.Should().BeTrue();

            // Add chargetype group

            foreach (var chargeTypeGroupName in matter.ChargeTypeGroupList)
            {
                matter.ChargeTypeGroupName = chargeTypeGroupName;
                _response = await _matterService.GetMatterChargeTypeGroupAsync(sessionId, processItemId, matter);
                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                matter.ChargeTypeGroup = new List<Guid>();
                matter.ChargeTypeGroup.Add(new Guid(quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.ChargeTypeGroupName)).RowKey));
                _response = await _matterService.SelectChargeTypeGroupAsync(processItemId, sessionId, matter);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Add cost type group
            foreach (var costTypeGroupName in matter.CostTypeGroupList)
            {
                matter.CostTypeGroupName = costTypeGroupName;
                _response = await _matterService.GetMatterCostTypeGroupAsync(sessionId, processItemId, matter);
                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                matter.CostTypeGroup = new List<Guid>();
                matter.CostTypeGroup.Add(new Guid(quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.CostTypeGroupName)).RowKey));
                _response = await _matterService.SelectCostTypeGroupAsync(processItemId, sessionId, matter);
                _response.IsSuccessful.Should().BeTrue();
            }

            // Add billing Group 

           
            _response = await _matterService.GetBillingGroupFormAsync(processItemId, sessionId, matter);
            var billingResponseObject = JsonConvert.DeserializeObject<AddBillingGroupResponseModel>(_response.Content);

            var billingGroupRowId = billingResponseObject.DataStateChanges.FirstOrDefault().Value.String;
            Console.WriteLine("Matter Billing  Row Id: " + billingGroupRowId);

            foreach (var billingGroup in matter.BillingGroupList)
            {
                matter.BillingGroupDescription = billingGroup;
                _response = await _matterService.GetMatterBillingGroupAsync(sessionId, processItemId, billingGroupRowId, matter);
                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                matter.BillingGroupCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.BillingGroupDescription)).RowKey;
                _response = await _matterService.SelectBillingGroupAsync(processItemId, sessionId, billingGroupRowId, matter);
                _response.IsSuccessful.Should().BeTrue();
            }

            // Submit Matter
            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.MatterProcessName);
            _response.IsSuccessful.Should().BeTrue();

            string matterNumber = _response.Headers.Where(x => x.Name.Equals("X-3E-Message")).Select(s => s.Value.ToString()).ToList()[0];
            string formatMatter = matterNumber.Replace("%20", "");
            Regex regex = new Regex(@"(?<=Number+)([0-9]*)");
            string matterNumberGenerated = regex.Match(formatMatter).Value;

            Console.WriteLine("Matter Number Generated " + matterNumberGenerated);

        }
       
    }
}
