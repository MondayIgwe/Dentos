using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ClientGroupType;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ClientGroupData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        private ILookUpService _lookUpService = new LookUpService();
        public IClientGroupService _clientGroupService = new ClientGroupService();
        public IRestResponse _response;

        public async Task<string> SearchAndCreateClientGroupType(string clientGroupTypeDescription)
        {
            var clientgroup = new ApiClientGroupTypeEntity()
            {
                GroupCode = clientGroupTypeDescription,
                GroupDescription = clientGroupTypeDescription
            };

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ClientGroupTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Serach for the Client 
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, clientGroupTypeDescription);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Client exists : " + clientGroupTypeDescription);
                return clientGroupTypeDescription;
            }

            //Add New
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ClientGroupTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            //Get client group ID for Data Input
            clientgroup.Id = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            clientgroup.Id.Should().NotBeNull();
            Console.WriteLine("chargeTypeId: " + clientgroup.Id);


            //Fill in client group type Data
            _response = await _clientGroupService.AddClientTypeGroupDataAsync(sessionId, processItemId, clientgroup);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostSubmitProcessAsync(sessionId, processItemId, ApiConstants.ClientGroupTypeProcess);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted ChargeTypeCode : " + clientgroup.GroupDescription.String);
                return clientgroup.GroupDescription.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }
    }
}
