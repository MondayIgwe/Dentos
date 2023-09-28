using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class TitleData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public ILookUpService _lookUpService = new LookUpService();

        public async Task<string> SearchForTitleValueElseGetATitleDescriptionAsync(string titleDescription = null)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.TitleProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            // search for Title Exists 

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, titleDescription);
            var results = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (results == null)
            {
                _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId);
                results = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            }
            var titleDesccription = results.Rows.FirstOrDefault().Attributes.Description;

            Console.WriteLine("Title Value : " + titleDesccription);
            _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();

            return titleDesccription;
        }
    }
}
