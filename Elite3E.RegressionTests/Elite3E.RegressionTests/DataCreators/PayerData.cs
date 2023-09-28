using Elite3E.RegressionTests.DataCreators;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.Payer;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.Data
{
    public class PayerData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IPayerService _payerMaintenanceService = new PayerService();
        private ILookUpService _lookUpService = new LookUpService();
        private readonly EntityData _entityData = new EntityData();

        public async Task<string> CreateAPayer(ApiPayerEntity payer)
        {

            payer = DefaultRegionalValues.GetPayerEntityDefaultValues(payer.PayerName);

            payer.EntityName = await _entityData.SearchOrCreateAnEntityPerson(payer.Entity);

            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.Payer);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, payer.PayerName);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Payer Exists : " + payer.PayerName);
                return payer.PayerName;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.Payer);
            _response.IsSuccessful.Should().BeTrue();

            var payerId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            payerId.Should().NotBeNull();
            Console.WriteLine("PayerId: " + payerId);
            payer.PayerId = payerId;

            _response = await _payerMaintenanceService.GetEntitySearchList(sessionId, processItemId, payer);
            _response.IsSuccessful.Should().BeTrue();
            var quickSerach = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            payer.EntityCode = quickSerach.Rows.FirstOrDefault(value => value.Alias.Equals(payer.EntityName)).RowKey;

            // get look up key values
            payer.TaxAreaCode = await LookUp.GetLookUpKeyValue(sessionId, "TaxArea", payer.TaxArea);

            _response = await _payerMaintenanceService.AddPayerDataAsync(sessionId, processItemId, payer);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _payerMaintenanceService.GetSiteSearchList(sessionId, processItemId, payer);
            _response.IsSuccessful.Should().BeTrue();
            var quick = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            payer.SiteCode = quick.Rows.FirstOrDefault(value => value.Alias.Equals(payer.Site, StringComparison.CurrentCultureIgnoreCase)).RowKey;

            _response = await _payerMaintenanceService.AddSiteData(sessionId, processItemId, payer);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.Payee);

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Payer : " + payer.PayerName);
                return payer.PayerName;
            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode.ToString());
            return null;

        }
    }
}

