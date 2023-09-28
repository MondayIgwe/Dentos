using System;
using System.Linq;
using System.Text.RegularExpressions;
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
using Elite3E.RestServices.Services.PayeeMaintenance;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class PayeeMaintenanceData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public CreateVendorMaintenance _vendorService = new();
        private ILookUpService _lookUpService = new LookUpService();
        public IPayeeMaintenanceService _payeeMaintenanceService = new PayeeMaintenanceService();
        private readonly EntityData _entityData = new();

        public async Task<string> SearchAndCreatePayeeMaintenanceDataAsync(ApiPayeeEntity payeeDetails)
        {
            var payee = DefaultRegionalValues.GetPayeeEntityDefaultValues(payeeDetails.Name.String);

            //var payee = new ApiPayeeEntity
            //{
            //    Name = payeeDetails.Name,
            //    PaymentTerm = "Immediate",
            //    Office = "Aberdeen",
            //    Unit = "Dentons UK and Middle East LLP",
            //    Currency = "EUR",
            //    Status = "Approved",
            //    VendorName = await _vendorService.CreateAVendor(),
            //    // payee.VendorName = await vendorData.CreateVendoAsync(vendorEntity.Vendor);
            //    EntityName = await _entityData.SearchOrCreateAnEntityPerson()
            //};

            payee.VendorName = await _vendorService.CreateAVendor();
            payee.EntityName = await _entityData.SearchOrCreateAnEntityPerson();
            payeeDetails.EntityName = payee.EntityName;
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.PayeeMaintenance);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();


            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, payee.EntityName);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Payee Exists : " + payee.Name.String);
                return payee.Name.String;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.Payee);
            _response.IsSuccessful.Should().BeTrue();

            var payeeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges
                .FirstOrDefault().Value.String;
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
                string message = _response.Headers.Where(x => x.Name.Equals("X-3E-Message"))
                    .Select(s => s.Value.ToString()).ToList()[0];
                string formatPayee = message.Replace("%20", "");
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
