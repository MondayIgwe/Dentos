using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.RateType;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public  class RateTypeMaintenanceData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public RateTypeService _rateTypeService = new();
        private ILookUpService _lookUpService = new LookUpService();
        public IRestResponse _response;        

        public async Task<string> CreateRateTypeAsync(ApiRateTypeEntity rateType)
        {
            rateType = DefaultRegionalValues.GetRateTypeDefaultValues(rateType);
            
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.RateTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Does Rate Type Exist? 
            //Check Description

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, rateType.RateTypeDescription);
            var existingRateType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingRateType.RowCount > 0 && existingRateType.Rows.Any(x => x.Attributes.Description.Equals(rateType.RateTypeDescription)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given RateType Description Exists : " + rateType.RateTypeDescription);
                return rateType.RateTypeDescription;
            }

            //Check Code
            existingRateType = null;
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, rateType.RateTypeCode);
            existingRateType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingRateType.RowCount > 0 && existingRateType.Rows.Any(x => x.Attributes.Code.Equals(rateType.RateTypeCode)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given RateType Code Exists : " + rateType.RateTypeCode);
                return rateType.RateTypeDescription;
            }

            //Create New Rate Type. Click ADD:

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.RateTypeProcess, ApiConstants.RateTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            var rateTypeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            AssertionExtensions.Should((string)rateTypeId).NotBeNull();
            Console.WriteLine("RateTypeId: " + rateTypeId);
            rateType.RateTypeId = rateTypeId;

            return await AddDataAndSubmitAsync(sessionId, processItemId, rateType);
        }


        public async Task<string> UpdateRateTypeAsync(ApiRateTypeEntity rateType)
        {
            rateType = DefaultRegionalValues.GetRateTypeDefaultValues(rateType);

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.RateTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Does Rate Type Exist? 
            //Check Code

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, rateType.RateTypeCode);
            var existingRateType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingRateType.RowCount == 0 || !existingRateType.Rows.Any(x => x.Attributes.Code.Equals(rateType.RateTypeCode)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("Unable to update RateType. Creating new Type: " + rateType.RateTypeCode);
                return await CreateRateTypeAsync(rateType);
            }

            //Add Existing Rate Type to Worklist
            var row = existingRateType.Rows.Where(x => x.Attributes.Code.Equals(rateType.RateTypeCode)).FirstOrDefault();
            row.Should().NotBeNull();
            var batchAddRowKey = row.RowKey;
            AssertionExtensions.Should((string)batchAddRowKey).NotBeNull();
            Console.WriteLine("Batch Add RowKey: " + batchAddRowKey);

            //Validate Data
            IList<bool> validationList = new List<bool>();
            validationList.Add(row.Attributes.Code.Equals(rateType.RateTypeCode));
            validationList.Add(row.Attributes.Description.Equals(rateType.RateTypeDescription));
            validationList.Add(row.Attributes.IsCostType.ToString().Equals(ResolveCheckBox(rateType.IsDisbursementCheckbox)));
            validationList.Add(row.Attributes.IsStandard.ToString().Equals(ResolveCheckBox(rateType.IsStandardRateCheckbox)));
            validationList.Add(row.Attributes.IsFirmDefault.ToString().Equals(ResolveCheckBox(rateType.IsFirmDefaultCheckbox)));

            //if all required checkboxes are correct, cancel the update
            if (validationList.All(x => x))
            {
                Console.WriteLine("Update Unnecessary for RateType: " + rateType.RateTypeCode);
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();
                return rateType.RateTypeDescription;
            }

            //Add Rate Type to Worklist with BatchAdd
            _response = await _rateTypeService.AddSearchResultToWorklistAsync(sessionId, processItemId, batchAddRowKey);
            rateType.RateTypeId = batchAddRowKey.Replace("-", "");

            return await UpdateRateDataAndSubmitAsync(sessionId, processItemId, rateType);
        }

        private async Task<string> AddDataAndSubmitAsync(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        { 
            //Add Rate Type Data
            rateType.RateTypeCurrency = await LookUp.GetCurrencyLookUpKeyValue(sessionId, rateType.RateTypeCurrencyDisplayName);
            
            _response = await _rateTypeService.AddRateTypeDataCode(sessionId, processItemId, rateType);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _rateTypeService.AddRateTypeDataDescription(sessionId, processItemId, rateType);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _rateTypeService.AddRateTypeDataCurrency(sessionId, processItemId, rateType);
            _response.IsSuccessful.Should().BeTrue();

            //Checking High Level TimeKeeper Checkbox
            if(!string.IsNullOrEmpty(rateType.IsTimeKeeperCheckbox))
            {
                rateType.IsTimeKeeperCheckboxValue = ResolveCheckBox(rateType.IsTimeKeeperCheckbox);
                _response = await _rateTypeService.AddRateTypeDataTimekeeperCheckbox(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Checking High Level Disbursement Checkbox
            if(!string.IsNullOrEmpty(rateType.IsDisbursementCheckbox))
            {
                rateType.IsDisbursementCheckboxValue = ResolveCheckBox(rateType.IsDisbursementCheckbox);
                _response = await _rateTypeService.AddRateTypeDataDisbursementCheckbox(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Checking Whether Rate should be Standard Or Not
            if (!string.IsNullOrEmpty(rateType.IsStandardRateCheckbox))
            {
                rateType.IsStandardRateCheckboxValue = ResolveCheckBox(rateType.IsStandardRateCheckbox);
                _response = await _rateTypeService.AddRateTypeDataStandardCheckbox(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Checking Whether Rate should be a Firm Default Or Not
            if (!string.IsNullOrEmpty(rateType.IsFirmDefaultCheckbox))
            {
                rateType.IsFirmDefaultCheckboxValue = ResolveCheckBox(rateType.IsFirmDefaultCheckbox);
                _response = await _rateTypeService.AddRateTypeDataFirmDefaultCheckbox(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //If the valid for timekeeper checkboxes need to be ticked
            if (!string.IsNullOrEmpty(rateType.IsValidForTimekeeperCheckboxes))
            {
                rateType.IsValidForTimekeeperCheckboxesValues = ResolveCheckBox(rateType.IsValidForTimekeeperCheckboxes);
                _response = await _rateTypeService.AddRateTypeDataValidForTimekeeperCheckboxes(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //If the valid for matter checkboxes need to be ticked
            if (!string.IsNullOrEmpty(rateType.IsValidForMatterCheckboxes))
            {
                rateType.IsValidMatterCheckboxesValues = ResolveCheckBox(rateType.IsValidForMatterCheckboxes);
                _response = await _rateTypeService.AddRateTypeDataValidForMatterCheckboxes(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            if(!string.IsNullOrEmpty(rateType.EffectiveDate.String))
            {
                //2021-11-01
                rateType.EffectiveDate = DateTime.Parse(rateType.EffectiveDate.String, new CultureInfo("en-US", true)).ToString("yyyy-MM-dd");
                //Effective date
                _response = await _rateTypeService.GetEffectiveInformationAsync(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
                var effectiveDateRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
                effectiveDateRowId.Should().NotBeNull();
                Console.WriteLine("Effective date Row Id: " + effectiveDateRowId);
                _response = await _rateTypeService.AddEffectiveInformationAsync(sessionId, processItemId, effectiveDateRowId, rateType);
                _response.IsSuccessful.Should().BeTrue();

                //Rate type
                _response = await _rateTypeService.GetRateDetailsAsync(sessionId, processItemId, effectiveDateRowId, rateType);
                _response.IsSuccessful.Should().BeTrue();

                var ratedetailsRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
                ratedetailsRowId.Should().NotBeNull();
                Console.WriteLine("Rated Details Row Id: " + ratedetailsRowId);
                _response = await _rateTypeService.AddRateDetailsAsync(sessionId, processItemId, effectiveDateRowId, ratedetailsRowId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.RateTypeProcess);
            _response.Content.Should().Contain("responseType\":1");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("The Given RateType Description " + rateType.RateTypeDescription);
                return rateType.RateTypeDescription;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }

        private async Task<string> UpdateRateDataAndSubmitAsync(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            _response = await _rateTypeService.AddRateTypeDataDescription(sessionId, processItemId, rateType);
            _response.IsSuccessful.Should().BeTrue();

            //Checking High Level TimeKeeper Checkbox
            if (!string.IsNullOrEmpty(rateType.IsTimeKeeperCheckbox))
            {
                rateType.IsTimeKeeperCheckboxValue = ResolveCheckBox(rateType.IsTimeKeeperCheckbox);
                _response = await _rateTypeService.AddRateTypeDataTimekeeperCheckbox(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Checking High Level Disbursement Checkbox
            if (!string.IsNullOrEmpty(rateType.IsDisbursementCheckbox))
            {
                rateType.IsDisbursementCheckboxValue = ResolveCheckBox(rateType.IsDisbursementCheckbox);
                _response = await _rateTypeService.AddRateTypeDataDisbursementCheckbox(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Checking Whether Rate should be Standard Or Not
            if (!string.IsNullOrEmpty(rateType.IsStandardRateCheckbox))
            {
                rateType.IsStandardRateCheckboxValue = ResolveCheckBox(rateType.IsStandardRateCheckbox);
                _response = await _rateTypeService.AddRateTypeDataStandardCheckbox(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Checking Whether Rate should be a Firm Default Or Not
            if (!string.IsNullOrEmpty(rateType.IsFirmDefaultCheckbox))
            {
                rateType.IsFirmDefaultCheckboxValue = ResolveCheckBox(rateType.IsFirmDefaultCheckbox);
                _response = await _rateTypeService.AddRateTypeDataFirmDefaultCheckbox(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //If the valid for timekeeper checkboxes need to be ticked
            if (!string.IsNullOrEmpty(rateType.IsValidForTimekeeperCheckboxes))
            {
                rateType.IsValidForTimekeeperCheckboxesValues = ResolveCheckBox(rateType.IsValidForTimekeeperCheckboxes);
                _response = await _rateTypeService.AddRateTypeDataValidForTimekeeperCheckboxes(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            //If the valid for matter checkboxes need to be ticked
            if (!string.IsNullOrEmpty(rateType.IsValidForMatterCheckboxes))
            {
                rateType.IsValidMatterCheckboxesValues = ResolveCheckBox(rateType.IsValidForMatterCheckboxes);
                _response = await _rateTypeService.AddRateTypeDataValidForMatterCheckboxes(sessionId, processItemId, rateType);
                _response.IsSuccessful.Should().BeTrue();
            }

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.RateTypeProcess);
            _response.Content.Should().Contain("responseType\":1");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("The Given RateType Description " + rateType.RateTypeDescription);
                return rateType.RateTypeDescription;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }



        private static string ResolveCheckBox(string input)
        {
            string result = null;

            if (string.IsNullOrEmpty(input))
                return "0";

            input = input.ToLower();

            if (input.Equals("0") || input.Equals("1"))
            {
                result = input;
            }
            else
            {
                result = (input.Equals("yes")) ? "1" : "0";
            }
            return result;
        }
    }
}
