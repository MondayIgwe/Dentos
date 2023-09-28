using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Services;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services.GlobalVendor;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class GlobalVendorTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IGlobalVendorService _globlaVendorService = new GlobalVendorService();

        [Test]
        public async Task CreateGlobalVendorTask()
        {
            var globalVendorEntity = new ApiGlobalVendorEntity()
            {
                Code = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                Description = "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10")
            };

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.GlobalVendorProcessName);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.GlobalVendorProcessName);
            _response.IsSuccessful.Should().BeTrue();

            var globalVendorId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            globalVendorId.Should().NotBeNull();
            Console.WriteLine("globalVendorId: " + globalVendorId);
            globalVendorEntity.GlobalVendorId = globalVendorId;

            _response = await _globlaVendorService.AddGlobalVendorAsync(sessionId, processItemId, globalVendorEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostSubmitProcessAsync(sessionId, processItemId, ApiConstants.GlobalVendorProcessName);
            if (_response.IsSuccessful)
            {
                Console.WriteLine("Submitted ChargeTypeGroupCode : " + globalVendorEntity.Description.String);

            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);
        }
    }
}
