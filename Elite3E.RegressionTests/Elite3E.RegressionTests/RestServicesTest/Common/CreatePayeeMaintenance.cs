using Elite3E.Infrastructure.Extensions;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.PayeeMaintenance;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RegressionTests.DataCreators.DefaultData;

namespace Elite3E.RegressionTests.RestServicesTest.Common
{
    public class CreatePayeeMaintenance
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public CreateVendorMaintenance _vendorService = new();
        public IPayeeMaintenanceService _payeeMaintenanceService = new PayeeMaintenanceService();
        private ILookUpService _lookUpService = new LookUpService();
        private readonly EntityData _entityData = new();

        public async Task<string> SearchOrCreatePayee(string payeeName)
        {
            return await CreateAPayee(payeeName);
        }


        public async Task<string> CreateAPayee(string payeeName = null)
        {

            payeeName = (string.IsNullOrEmpty(payeeName)) ? "Payee" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+7") : payeeName;
            var payee = DefaultRegionalValues.GetPayeeEntityDefaultValues(payeeName);
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.PayeeMaintenance);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, payee.Name.String);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Payee Exists : " + payee.Name.String);
                return payee.Name.String;
            }

            payee.VendorName = await _vendorService.CreateAVendor();
            payee.EntityName = await _entityData.SearchOrCreateAnEntityPerson();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.Payee);
            _response.IsSuccessful.Should().BeTrue();

            var payeeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            payeeId.Should().NotBeNull();
            Console.WriteLine("PayeeId: " + payeeId);
            payee.PayeeId = payeeId;

            _response = await _payeeMaintenanceService.GetEntitySearchList(sessionId, processItemId, payee);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            payee.EntityValue = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(payee.EntityName)).RowKey;

            _response = await _payeeMaintenanceService.GetVendorSearchList(sessionId, processItemId, payee);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            payee.VendorValue = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(payee.VendorName)).RowKey;

            // get look up key values
            payee.PaymentTermvalue = await LookUp.GetLookUpKeyValue(sessionId, "Terms", payee.PaymentTerm);
            payee.OfficeCode = await LookUp.GetLookUpKeyValue(sessionId, "Office", payee.Office);
            payee.StatusCode = await LookUp.GetLookUpKeyValue(sessionId, "PayeeStatus", payee.Status);


            _response = await _payeeMaintenanceService.AddPayeeMaintenanceDataAsync(sessionId, processItemId, payee);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.Payee);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string payeeNumber = _response.Headers.Where(x => x.Name.Equals("X-3E-Message")).Select(s => s.Value.ToString()).ToList()[0];
                string formatPayee = payeeNumber.Replace("%20", "");
                Regex regex = new Regex(@"(?<=Number+)([0-9-]*)");
                string payeeNumberGenerated = regex.Match(formatPayee).Value;

                Console.WriteLine("Submitted Payee : " + payeeNumberGenerated);
                return payeeNumberGenerated;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;


        }

    }
}
