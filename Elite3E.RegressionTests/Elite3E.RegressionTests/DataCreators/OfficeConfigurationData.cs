using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.Office_Configuration;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public  class OfficeConfigurationData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public CreatePayeeMaintenance _payeeMaintenance = new();
        private ILookUpService _lookUpService = new LookUpService();
        public IOfficeConfigurationService _officeConfigService = new OfficeConfigurationService();        

        public async Task<string> SearchAndCreateOfficeConfigurationDataAsync(ApiOfficeConfiguration officeConfig)
        {
            officeConfig = DefaultRegionalValues.GetClientMaintenanceDefaultValues(officeConfig);

            //officeConfig.Office = (string.IsNullOrEmpty(officeConfig.Office)) ? "Amsterdam" : officeConfig.Office;
            //officeConfig.DisbursementTypeValue = (string.IsNullOrEmpty(officeConfig.DisbursementTypeValue)) ? "Completion BACS" : officeConfig.DisbursementTypeValue;

            officeConfig.PayeeKey = await _payeeMaintenance.CreateAPayee();

            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.OfficeCOnfigurationProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, officeConfig.Office);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Office COnfig Exists for office: " + officeConfig.OfficeKey);
                return officeConfig.Office;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.OfficeCOnfigurationProcess);
            _response.IsSuccessful.Should().BeTrue();

            var OfficeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            AssertionExtensions.Should((string)OfficeId).NotBeNull();
            Console.WriteLine("officeId: " + OfficeId);
            officeConfig.OfficeId = OfficeId;

            // get look up key values
            officeConfig.OfficeKey = await LookUp.GetLookUpKeyValue(sessionId, "Office", officeConfig.Office);
            officeConfig.DisbursementTypeKey = await LookUp.GetLookUpKeyValue(sessionId, "TrustDisbursementType", officeConfig.DisbursementTypeValue);

            _response = await _officeConfigService.GetLookupSearchPayee(sessionId, processItemId, officeConfig);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            officeConfig.PayeeValue = quickSearch.Rows.FirstOrDefault(value => value.Attributes.PayeeNum.Equals(officeConfig.PayeeKey)).Attributes.PayeeIndex;

            _response = await _officeConfigService.GetLookupTimekeeperLeaver(sessionId, processItemId, officeConfig);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearchTimekeeper = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            officeConfig.TimeKeeperValue = quickSearchTimekeeper.Rows[0].RowKey;


            _response = await _officeConfigService.AddOfficeConfigurationDataAsync(sessionId, processItemId, officeConfig);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostSubmitProcessAsync(sessionId, processItemId, ApiConstants.OfficeCOnfigurationProcess);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                Console.WriteLine("Config Submitted for : " + officeConfig.Office);
                return officeConfig.Office;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }
    }
}
