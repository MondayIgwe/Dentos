using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Services.PropagationProcess;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiCreatePropagationProcessSteps
    {
        private IProcessService _process = new ProcessService();
        private readonly IPropagationProcessService propagationProcess = new PropagationProcessService();
        private ISessionService _session = new SessionService();
        private IRestResponse _response;
        private readonly FeatureContext _featureContext;
        IPropagationProcessService propagationProcessService = new PropagationProcessService();
        public ApiCreatePropagationProcessSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I have a added '([^']*)' process")]
        public async Task GivenIHaveAAddedProcess(string processName)
        {
            
            var setupProcessEntity = new SetupProcessEntity()
            {
                 ProcessAlias = processName
            };

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.SetupPropagation);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.SetupPropagation);
            _response.IsSuccessful.Should().BeTrue();

            var SetupProcessId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            SetupProcessId.Should().NotBeNull();
            Console.WriteLine("Setup Process Id: " + SetupProcessId);
            setupProcessEntity.SetupProcessId = SetupProcessId;

            _response = await propagationProcessService.GetPropagationProcess(sessionId, processItemId, setupProcessEntity);
            _response.IsSuccessful.Should().BeTrue();
            var processquickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            setupProcessEntity.ProcessValue = processquickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(setupProcessEntity.ProcessAlias)).RowKey;
            setupProcessEntity.ProcessValue.String.Should().NotBeNullOrEmpty();


            await propagationProcessService.AddPropagationProcessAsync(sessionId, processItemId, setupProcessEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostSubmitProcessAsync(sessionId, processItemId, ApiConstants.SetupPropagation);
            _response.IsSuccessful.Should().BeTrue();

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Setup Process: " + setupProcessEntity.ProcessAlias);
            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);
        }
    }
}

