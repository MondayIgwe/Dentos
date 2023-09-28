using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.GjCategorySetup;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public  class GJCategoryData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public ILookUpService _lookUpService = new LookUpService();
        public IGjCategorySetupService _gjCategoryService = new GjCategorySetupService();

        public async Task<GJCategoryEntity> CreateGJCategoryAsync(GJCategoryEntity gjCategoryEntity)
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.GJCategory);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Search for Existing Category
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, gjCategoryEntity.GJCategoryDescription);
            var quickResponse = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (quickResponse.Rows != null && quickResponse.Rows.Any(s => isEqualsIgnoreCase(s.Attributes.Description, gjCategoryEntity.GJCategoryDescription) || isEqualsIgnoreCase(s.Attributes.CategoryCode, gjCategoryEntity.GJCategoryCode)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Department Description Exists : " + gjCategoryEntity.GJCategoryDescription);
                return gjCategoryEntity;
            }

            //Create new
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.GJCategory);
            _response.IsSuccessful.Should().BeTrue();

            var gJCategorySetupId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            gJCategorySetupId.Should().NotBeNull();
            Console.WriteLine("Entity Id: " + gJCategorySetupId);
            gjCategoryEntity.GJCategorySetupId = gJCategorySetupId;

            gjCategoryEntity.IsRequireApprovalCheckboxValue = ResolveCheckbox(gjCategoryEntity.IsRequireApprovalCheckboxAlias);

            await _gjCategoryService.AddGJCategoryAsync(sessionId, processItemId, gjCategoryEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.GJCategory);
            _response.IsSuccessful.Should().BeTrue();

            Console.WriteLine("GJ Category : " + gjCategoryEntity.GJCategoryCode);
            return gjCategoryEntity;
        }

        private static string ResolveCheckbox(string input)
        {
            string result = null;            

            input = input.ToLower();

            if (input.Equals("0") || input.Equals("1"))
            {
                result = input;
            }
            else
            {
                result = (input.Equals("yes")) ? "1" : "0";
            }
            return result;
        }

        private static bool isEqualsIgnoreCase(string text, string text2)
        {
            return text.ToLower().Equals(text2.ToLower());
        }
    }
}
