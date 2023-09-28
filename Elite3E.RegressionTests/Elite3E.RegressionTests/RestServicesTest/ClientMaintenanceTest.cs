using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.Client;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class ClientMaintenanceTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IClientService _clientService = new ClientService();

        [Test]
        public async Task CreateClient()
        {

            var client = new ApiClientMaintenanceEntity()
            {
                Name = "Client_Automation" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                EntityName = "Test_FirstName1027330104 Test_LastName545737507",
                OpeningFeeEarnerName = "Test_FirstName28908459 Test_LastName1238693067",
                BillingFeeEarnerName = "Test_FirstName770779688 Test_LastName304551896",
                SupervisingFeeEarnerName = "Test_FirstName746379478 Test_LastName1013809348",
                ResponsibleFeeEarnerName    = "Test_FirstName738473147 Test_LastName1365521245",
                InvoiceSiteName = "London UK site",
                Office = "London (EU)",
                Country = "UNITED KINGDOM",
                Currency = "GBP - British Pound",
                Status = "Fully Open"
            };
            //Get Session Id

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ClientProcessName);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ClientProcessName, ApiConstants.ClientProcessName);
            _response.IsSuccessful.Should().BeTrue();

            client.Id = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            client.Id.Should().NotBeNull();
            Console.WriteLine("Client Id: " + client.Id);

            //QuickSearch GET entity for client
            _response = await _clientService.GetClientEntitySearchList(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.Entity = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(client.EntityName)).RowKey;
            client.Entity.Should().NotBeNull();

            _response = await _clientService.GetClientOpeningFeeEarnerSearchList(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.OpeningFeeEarnerAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.OpeningFeeEarnerName)).Alias;
            client.OpeningFeeEarnerKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.OpeningFeeEarnerName)).RowKey;
            
            
            // get look up key values
            client.CountryCode = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "Country", client.Country);
            client.CurrencyCode = await LookUp.GetCurrencyLookUpKeyValue(sessionId, client.Currency);
            client.StatusCode = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "CliStatusType", client.Status);

            _response = await _clientService.AddClientDataAsync(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();
            //Add more validation for this response

            _response = await _clientService.GetClientInvoiceSiteSearchList(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.InvoiceSite = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(client.InvoiceSiteName)).RowKey;
            client.Entity.Should().NotBeNull();

            _response = await _clientService.AddClientInvoiceSiteDataAsync(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _clientService.AddEffectiveDatedInformationAsync(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();
            var rowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 2);
            rowId.Should().NotBeNull();
            Console.WriteLine("Effective date info Row Id: " + rowId);

            _response = await _clientService.GetClientBillingFeeEarnerSearchList(sessionId, processItemId, rowId, client);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.BillingFeeEarnerAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.BillingFeeEarnerName)).Alias;
            client.BillingFeeEarnerRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.BillingFeeEarnerName)).RowKey;


            _response = await _clientService.GetClientResponsibleFeeEarnerSearchList(sessionId, processItemId, rowId, client);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.ResponsibleFeeEarnerAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.ResponsibleFeeEarnerName)).Alias;
            client.ResponsibleFeeEarnerRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.ResponsibleFeeEarnerName)).RowKey;

            _response = await _clientService.GetClientSupervisingFeeEarnerSearchList(sessionId, processItemId, rowId, client);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.SupervisingFeeEarnerAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.SupervisingFeeEarnerName)).Alias;
            client.SupervisingFeeEarnerRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.SupervisingFeeEarnerName)).RowKey;

            client.OfficeKey = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "Office", client.Office);

            _response = await _clientService.UpdateEffectiveDateInformationAsync(sessionId, processItemId, rowId, client);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ClientProcessName);
            if (_response.IsSuccessful)
            {
                Console.WriteLine("Client Name : " + client.Name.String);

            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);


        }
    }
}
