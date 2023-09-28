using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ClientAccountReceipt;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ClientAccountReceiptData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private ILookUpService _lookUpService = new LookUpService();
        public IClientAccountReceiptService _clientAccountReceiptService = new ClientAccountReceiptService();

        public async Task<string> SearchAndCreateAClientAccountReceiptDataAsync(ApiClientAccountReceiptEntity clientAccountReceipt)
        {
            /** var clientAccountReceipt = new ApiClientAccountReceiptEntity()
             {
                 ClientAccountReceiptType = "Cash",
                 ClientAccountAcct = "Singapore - SCB Bank Trust Acc - GBP",
                 DocumentNumber = "Receipt_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+8"),
                 Amount = new Random().Next(200, 1900),
                 MatterNumber = "100000006"
             };**/

            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ClientAccountReceipt);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, clientAccountReceipt.DocumentNumber.String);
            var quickSearchResult = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if ((quickSearchResult.Rows != null) && (quickSearchResult.RowCount != 0 ))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Client Account Receipt Exists : " + clientAccountReceipt.DocumentNumber.String);
                return clientAccountReceipt.DocumentNumber.String;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ClientAccountReceipt);
            _response.IsSuccessful.Should().BeTrue();

            var clientAccountReceiptId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            clientAccountReceiptId.Should().NotBeNull();
            Console.WriteLine("clientAccountReceiptId: " + clientAccountReceiptId);
            clientAccountReceipt.ClientAccountReceiptId = clientAccountReceiptId;

            //Lookup the CLient Account Receipt Type
            clientAccountReceipt.ClientAccountReceiptType = await LookUp.GetLookUpKeyValue(sessionId, "TrustReceiptType", clientAccountReceipt.ClientAccountReceiptType.String);

            //Get the Client Account Receipt Detail RowKey
            _response = await _clientAccountReceiptService.GetClientAccountReceiptRowKeyDetail(processItemId, sessionId, clientAccountReceipt);
            var clientReceiptDetailId = JsonHelper.JsonReaderChecker(_response.Content, "id", 2);
            clientReceiptDetailId.Should().NotBeNull();
            Console.WriteLine("Client Receipt detail Row Id: " + clientReceiptDetailId);

            //Get the Matter Row key
            _response = await _clientAccountReceiptService.GetClientAccountMatter(sessionId, processItemId, clientReceiptDetailId, clientAccountReceipt);
            _response.IsSuccessful.Should().BeTrue();
            var clientAccountMatterRow = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            clientAccountReceipt.MatterNumber = clientAccountMatterRow.Rows.FirstOrDefault(value => value.Alias.Equals(clientAccountReceipt.MatterNumber.String)).RowKey;
            clientAccountReceipt.MatterNumber.String.Should().NotBeNullOrEmpty();

            //Add the Client Account Receipt Detail data
            _response = await _clientAccountReceiptService.AddClientAccountReceiptDetailDataAsync(sessionId, processItemId, clientReceiptDetailId, clientAccountReceipt);
            _response.IsSuccessful.Should().BeTrue();

            //Submit
            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ClientAccountReceipt);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted Client Account Receipt for Matter : " + clientAccountReceipt.MatterNumber.String);
                return clientAccountReceipt.DocumentNumber.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode + "for Matter" + clientAccountReceipt.MatterNumber);

            return null;
        }
    }
}