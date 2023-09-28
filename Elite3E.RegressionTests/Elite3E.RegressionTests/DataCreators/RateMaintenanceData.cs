using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.RateMaintenance;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class RateMaintenanceData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IRateMaintenance _rateMaintenance = new RateMaintenanceService();
        private ILookUpService _lookUpService = new LookUpService();

        public async Task<string> SearchAndCreateRateAsync(ApiRateMaintenanceEntity apiRateMaintenanceEntity)
        {
            var rateMaintenance = DefaultRegionalValues.GetRateDefaultValues(apiRateMaintenanceEntity);
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.RateMaintenanceProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, rateMaintenance.Code.String);
            var quickResponse = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (quickResponse.Rows != null 
                && quickResponse.Rows.Any(s => s.Attributes.Description.Equals(rateMaintenance.Description.String, StringComparison.CurrentCultureIgnoreCase) 
                || s.Attributes.Code.Equals(rateMaintenance.Code.String, StringComparison.CurrentCultureIgnoreCase)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Rate Exists : " + rateMaintenance.Code.String);
                return rateMaintenance.Code.String;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.RateMaintenanceProcess);
            _response.IsSuccessful.Should().BeTrue();

            var rateId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            rateId.Should().NotBeNull();
            Console.WriteLine("RateId: " + rateId);
            rateMaintenance.RateId = rateId;

            _response = await _rateMaintenance.GetRateTypeOneSearchList(sessionId, processItemId, rateMaintenance);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            rateMaintenance.RateTypeValue = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(rateMaintenance.RateTypeDescription)).Attributes.Code;

            _response = await _rateMaintenance.AddRateAsync(sessionId, processItemId, rateMaintenance);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.RateMaintenanceProcess);
            if (_response.IsSuccessful)
            {
                Console.WriteLine("Submitted RateMaintenance : " + rateMaintenance.Code.String);
                return rateMaintenance.Code.String;
            }
            
            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }
    }
}
