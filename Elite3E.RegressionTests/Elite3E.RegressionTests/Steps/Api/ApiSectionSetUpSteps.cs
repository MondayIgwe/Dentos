using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.Section;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiSectionSetUpSteps
    {
        
        private readonly ILookUpService _lookUpService = new LookUpService();

        private IProcessService _process = new ProcessService();
        private readonly ISectionSetUpService _sectionSetUpService = new SectionSetUpService();
        private ISessionService _session = new SessionService();
        private IRestResponse _response;
        private readonly FeatureContext _featureContext;
        public ApiSectionSetUpSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }
        [Given(@"I search or create a section")]
        public async void GivenISearchOrCreateASection(Table table)
        {
            var sectionEntity = table.CreateInstance<ApiSectionEntity>();
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.Section);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.Section);
            _response.IsSuccessful.Should().BeTrue();

            var sectionSetupId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            sectionSetupId.Should().NotBeNull();
            Console.WriteLine("Section Id: " + sectionSetupId);
            sectionEntity.SectionId = sectionSetupId;

            // Search for feeEarner 
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, sectionEntity.Code);

            if (_response.Content.Length > 2)

            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();
            }
            else
            {
                //Add GL Section
                _response = await _sectionSetUpService.GetGLSectionSearchGLTypeValueAsync(sessionId, processItemId, sectionEntity);
                _response.IsSuccessful.Should().BeTrue();
                var glTypequickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                sectionEntity.GlSectionValue = glTypequickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(sectionEntity.GLSection)).RowKey;
                sectionEntity.GlSectionValue.String.Should().NotBeNullOrEmpty();


                _response = await _sectionSetUpService.AddSectionAsync(sessionId, processItemId, sectionEntity);
                _response.IsSuccessful.Should().BeTrue();

                _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.Section);
                _response.IsSuccessful.Should().BeTrue();

                if (_response.IsSuccessful)
                {
                    Console.WriteLine("Section added : " + sectionEntity.Code);

                }
                else
                    Console.WriteLine("test failed: " + _response.StatusCode);
            }         
        }
    }
}
