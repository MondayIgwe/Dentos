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
using Elite3E.RestServices.Models.ResponseModels.LookUpResponseModel;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.Client;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ClientMaintenanceData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IClientService _clientService = new ClientService();
        private ILookUpService _lookUpService = new LookUpService();
        private readonly EntityData _entityData = new();

        public async Task<ApiClientMaintenanceEntity> ClientData(ApiClientMaintenanceEntity clientData)
        {
            var firstSpaceIndex = clientData.EntityName.IndexOf(" ", StringComparison.Ordinal);
            var firstName = firstSpaceIndex > 0 ? clientData.EntityName.Substring(0, firstSpaceIndex) : clientData.EntityName;
            var lastName = firstSpaceIndex > 0 ? clientData.EntityName.Substring(firstSpaceIndex + 1) : String.Empty;


            string feeEarnerFirstName = "Auto_David";
            string feeEarnerLastName = "Auto_Affleck";

            if (!string.IsNullOrEmpty(clientData.FeeEarnerFullName))
            {
                firstSpaceIndex = clientData.FeeEarnerFullName.IndexOf(" ", StringComparison.Ordinal);
                feeEarnerFirstName = firstSpaceIndex > 0 ? clientData.FeeEarnerFullName.Substring(0, firstSpaceIndex) : clientData.FeeEarnerFullName;

                feeEarnerLastName = firstSpaceIndex > 0 ? clientData.FeeEarnerFullName.Substring(firstSpaceIndex + 1) : String.Empty;
            }

            var clientTestData = new ClientDetailsEntity()
            {
                ClientEntity = new ApiEntity()
                {
                    FirstName = firstName,
                    LastName = lastName
                },
                FeeEarnerEntity = new ApiEntity()
                {
                    FirstName = feeEarnerFirstName,
                    LastName = feeEarnerLastName,
                },
                Client = clientData
            };

            clientTestData.ClientEntity.FormattedName =
               ($"{clientTestData.ClientEntity.FirstName.String} {clientTestData.ClientEntity.LastName.String}").TrimEnd();
            clientTestData.FeeEarnerEntity.FormattedName =
                ($"{clientTestData.FeeEarnerEntity.FirstName.String} {clientTestData.FeeEarnerEntity.LastName.String}").TrimEnd();

            var client = await CreateClientDataAsync(clientTestData);
            clientData.ClientNumber = client.ClientNumber;
            return clientData;
        }

        public async Task AddBillingRulesValidationListToAnExistingClientAsync(ApiClientMaintenanceEntity client)
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ClientProcessName);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Serach for the Client 
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId,
                client.EntityName);

            var clientResposeModel = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.ClientNumberRowKeyValue = clientResposeModel.Rows.FirstOrDefault().RowKey; ;

            _response = await _clientService.SelectTheClientAsync(sessionId, processItemId, client);
            client.Id = client.ClientNumberRowKeyValue.Replace("-", "");

            // check if the client has already assigned billing rules validation list 

            _response = await _clientService.GetExistingBillingRulesValidationListAsync(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();

            foreach (var billingRule in client.BillingRulesValidationList)
            {
                client.BillingRulesValidationName = billingRule;
                if (!_response.Content.Contains(client.BillingRulesValidationName))
                {
                    _response = await _clientService.GetBillingRulesValidationListAsync(sessionId, processItemId, client);
                    var results = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                    client.BillingRulesValidationKey = new List<Guid>();
                    client.BillingRulesValidationKey.Add(new Guid(results.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(client.BillingRulesValidationName)).RowKey));
                    _response = await _clientService.AddBillingRulesValidationListAsync(sessionId, processItemId, client);
                    _response.IsSuccessful.Should().BeTrue();
                }
            }

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId,
                ApiConstants.ClientProcessName);
            _response.Content.Should().Contain("responseType\":1,");
        }

        public async Task<ApiClientMaintenanceEntity> CheckClientExitsElseCreateClient(ApiClientMaintenanceEntity client)
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ClientProcessName);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            // check client Data
            await ClientData(client);

            //Serach for the Client 
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId,
                client.EntityName);

            var clientResposeModel =
                    JsonConvert
                        .DeserializeObject<
                            LookupClientResponseModel>(_response.Content);
            client.ClientNumber = clientResposeModel.Rows.FirstOrDefault().Attributes.Number.ToString();

            _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();

            return client;
        }

        private async Task<ClientDetailsEntity> CreateClientDataAsync(ClientDetailsEntity clientData)
        {

            var feeEarnerEntity = new ApiFeeEarnerEntity()
            {
                EntityName = clientData.FeeEarnerEntity.FormattedName
            };

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ClientProcessName);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Serach for the Client 
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId,
                clientData.ClientEntity.FormattedName);

            var existingClientRows = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingClientRows.Rows != null)
            {
                foreach (var existingClient in existingClientRows.Rows)
                {
                    if (existingClient.Attributes.DisplayName.Equals(clientData.ClientEntity.FormattedName,
                            StringComparison.CurrentCultureIgnoreCase))
                    {

                        //Storing ClientNumber for UI Test

                        clientData.ClientNumber = existingClient.Attributes.Number.ToString();

                        _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                        _response.IsSuccessful.Should().BeTrue();

                        Console.WriteLine("The Given Client exists : " + clientData.ClientEntity.FormattedName);
                        return clientData;
                    }
                }

            }

            clientData.Client.Name = clientData.ClientEntity.FormattedName;
            var client = DefaultRegionalValues.GetClientMaintenanceDefaultValues(clientData.Client);

            //Entity for the client  --
            client.OpeningFeeEarnerName =
                (await new FeeEarnerData().SearchAndCreateFeeEranerData(feeEarnerEntity, clientData.FeeEarnerEntity))
                .EntityName;
            client.BillingFeeEarnerName = string.IsNullOrEmpty(client.BillingFeeEarnerName) ? client.OpeningFeeEarnerName : client.BillingFeeEarnerName;
            client.SupervisingFeeEarnerName = string.IsNullOrEmpty(client.SupervisingFeeEarnerName) ? client.OpeningFeeEarnerName : client.SupervisingFeeEarnerName;
            client.ResponsibleFeeEarnerName = string.IsNullOrEmpty(client.ResponsibleFeeEarnerName) ? client.OpeningFeeEarnerName : client.ResponsibleFeeEarnerName;

            client.EntityName = await _entityData.SearchOrCreateAnEntityPerson(clientData.ClientEntity.FormattedName);

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ClientProcessName,
                ApiConstants.ClientProcessName);
            _response.IsSuccessful.Should().BeTrue();

            client.Id = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges
                .FirstOrDefault().Value.String;
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
            client.OpeningFeeEarnerAlias = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.OpeningFeeEarnerName)).Alias;
            client.OpeningFeeEarnerKey = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.OpeningFeeEarnerName)).RowKey;


            // get look up key values
            client.CountryCode = await LookUp.GetLookUpKeyValue(sessionId, "Country", client.Country);
            client.CurrencyCode = await LookUp.GetCurrencyLookUpKeyValue(sessionId, client.Currency);
            client.StatusCode = await LookUp.GetLookUpKeyValue(sessionId, "CliStatusType", client.Status);

            _response = await _clientService.AddClientDataAsync(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();

            if (!string.IsNullOrEmpty(client.DateOpened))
            {
                client.DateOpened = DateTime.Parse(client.DateOpened, new CultureInfo("en-US", true)).ToString("d/M/yyyy");
                _response = await _clientService.AddDateOpendAsync(sessionId, processItemId, client);
                _response.IsSuccessful.Should().BeTrue();
            }

            var firstSpaceIndex = client.InvoiceSiteName.IndexOf(" ", StringComparison.Ordinal);
            var firstName = firstSpaceIndex > 0 ? client.InvoiceSiteName.Substring(0, firstSpaceIndex) : client.InvoiceSiteName;
            var lastName = firstSpaceIndex > 0 ? client.InvoiceSiteName.Substring(firstSpaceIndex + 1) : String.Empty;
            client.InvoiceSiteName =
               ($"{firstName} {lastName}").TrimEnd();
            _response = await _clientService.GetClientInvoiceSiteSearchList(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.InvoiceSite = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(client.InvoiceSiteName, StringComparison.CurrentCultureIgnoreCase))
                .RowKey;
            client.Entity.Should().NotBeNull();

            _response = await _clientService.AddClientInvoiceSiteDataAsync(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _clientService.AddEffectiveDatedInformationAsync(sessionId, processItemId, client);
            _response.IsSuccessful.Should().BeTrue();
            var rowId = JsonHelper.JsonReaderChecker(_response.Content, "rows", 1);
            rowId.Should().NotBeNull();
            Console.WriteLine("Effective date info Row Id: " + rowId);

            _response = await _clientService.GetClientBillingFeeEarnerSearchList(sessionId, processItemId, rowId,
                client);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.BillingFeeEarnerAlias = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.BillingFeeEarnerName)).Alias;
            client.BillingFeeEarnerRowKey = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.BillingFeeEarnerName)).RowKey;


            _response = await _clientService.GetClientResponsibleFeeEarnerSearchList(sessionId, processItemId, rowId,
                client);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.ResponsibleFeeEarnerAlias = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.ResponsibleFeeEarnerName)).Alias;
            client.ResponsibleFeeEarnerRowKey = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.ResponsibleFeeEarnerName)).RowKey;

            _response = await _clientService.GetClientSupervisingFeeEarnerSearchList(sessionId, processItemId, rowId,
                client);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            client.SupervisingFeeEarnerAlias = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.SupervisingFeeEarnerName)).Alias;
            client.SupervisingFeeEarnerRowKey = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.DisplayName.Equals(client.SupervisingFeeEarnerName)).RowKey;

            client.OfficeKey = await LookUp.GetLookUpKeyValue(sessionId, "Office", client.Office);

            _response = await _clientService.UpdateEffectiveDateInformationAsync(sessionId, processItemId, rowId,
                client);
            _response.IsSuccessful.Should().BeTrue();

            if (!string.IsNullOrEmpty(client.EDIStartDate))
            {
                client.EDIStartDate = DateTime.Parse(client.EDIStartDate, new CultureInfo("en-US", true)).ToString("d/M/yyyy");
                _response = await _clientService.AddEDIStartDateAsync(sessionId, processItemId, rowId, client);
                _response.IsSuccessful.Should().BeTrue();
            }

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId,
                ApiConstants.ClientProcessName);
            _response.Content.Should().Contain("responseType\":1,");

            if (_response.IsSuccessful)
            {
                _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId,
                    clientData.ClientEntity.FormattedName);

                //Storing ClientNumber for UI Test
                var clientResposeModel =
                    JsonConvert
                        .DeserializeObject<
                            LookupClientResponseModel>(_response.Content);
                clientData.ClientNumber = clientResposeModel.Rows[0].Attributes.Number.ToString();

                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                //_response.IsSuccessful.Should().BeTrue(); //Not required. Causes test to fail.

                Console.WriteLine("Client Name : " + client.Name.String);
                return clientData;

            }

            throw new Exception("error Creating the Client" + _response.ErrorMessage);
        }
    }


    public class ClientDetailsEntity
    {
        public ApiEntity ClientEntity { get; set; }
        public ApiEntity FeeEarnerEntity { get; set; }
        public ApiEntity Entity { get; set; }
        public string ClientNumber { get; set; }
        public ApiClientMaintenanceEntity Client { get; set; }
    }
}
