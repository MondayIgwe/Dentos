using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ClientAccountIntendedUse;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ClientAccountIntendedUseData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private ILookUpService _lookUpService = new LookUpService();
        public IClientAccountIntendedUseService _clientAccountIntendedUseService = new ClientAccountIntendedUseService();

        public async Task<string> SearchAndCreateAClientAccountIntendedUseDataAsync(ApiClientIntendedUseEntity clientIntendedUseEntity)
        {

            clientIntendedUseEntity = new ApiClientIntendedUseEntity()
            {
                Code = !string.IsNullOrEmpty(clientIntendedUseEntity.Code.String) ? clientIntendedUseEntity.Code.String : "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                Description = !string.IsNullOrEmpty(clientIntendedUseEntity.Description.String) ? clientIntendedUseEntity.Description.String : "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
            };

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ClientAccountIntendedUse);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, clientIntendedUseEntity.Code.String);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Client Account Intended Use Code Exists : " + clientIntendedUseEntity.Code.String);
                return clientIntendedUseEntity.Code.String;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ClientAccountIntendedUse);
            _response.IsSuccessful.Should().BeTrue();

            var clientAccountId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            clientAccountId.Should().NotBeNull();
            Console.WriteLine("clientAccountId: " + clientAccountId);
            clientIntendedUseEntity.ClientAccountId = clientAccountId;

            _response = await _clientAccountIntendedUseService.AddCLientAccountDataAsync(sessionId, processItemId, clientIntendedUseEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ClientAccountIntendedUse);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted Client Account Des : " + clientIntendedUseEntity.Description.String);
                return clientIntendedUseEntity.Description.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }
    }
}
