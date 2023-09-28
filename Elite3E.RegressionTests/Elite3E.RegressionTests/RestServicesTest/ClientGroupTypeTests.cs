using Elite3E.Infrastructure.Extensions;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Services.ClientGroupType;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public  class ClientGroupTypeTests
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IClientGroupService _clientGroupService = new ClientGroupService();

        [Test]
        public async Task CreateClientGroupTypeTask()
        {
            var clientGroupTypeEntity = new ApiClientGroupTypeEntity()
            {
                GroupCode = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                GroupDescription = "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
            };

            //Start Session
            _response = await _session.GetSessionResponseAsync();

            //Get Session ID
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ClientGroupTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Add New
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ClientGroupTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            //Get client group ID for Data Input
            clientGroupTypeEntity.Id = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            clientGroupTypeEntity.Id.Should().NotBeNull();
            Console.WriteLine("chargeTypeId: " + clientGroupTypeEntity.Id);


            //Fill in client group type Data
            _response = await _clientGroupService.AddClientTypeGroupDataAsync(sessionId, processItemId, clientGroupTypeEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostSubmitProcessAsync(sessionId, processItemId, ApiConstants.ClientGroupTypeProcess);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted ChargeTypeCode : " + clientGroupTypeEntity.GroupDescription.String);
            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);
        }
    }
}
