using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Enums;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.UserRoleManagement;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class UserRoleData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private readonly ILookUpService _lookUpService = new LookUpService();
        private readonly IUserRoleManagementService userRoleManagementService = new UserRoleManagementService();
        private string sessionId;

        public async Task<string> GetLoggedInUserName()
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionResponse = JsonConvert.DeserializeObject<Session>(_response.Content);
            var sessionId = sessionResponse.Id.ToString();
            sessionId.Should().NotBeNull();

            string username = sessionResponse?.User.Name;
            username.Should().NotBeNullOrEmpty();
            return username;
        }

            public async Task<bool> SearchAndCreateUser(UserRoleManagementEntity userEntity)
        {
            //Setting Regional Specific Default Values. e.g. DefaultOperatingAlias
            userEntity = DefaultRegionalValues.GetUserRoleDefaultValues(userEntity);

            userEntity.NetworkAlias = @"dentons\" + userEntity.UserName.Replace(" ","");
            userEntity.EmailAddress = userEntity.UserName.Replace(" ","").Replace("-","") + "@dentons.global";
            userEntity.CanProxy = "1";
            userEntity.CanEditDashboard = "1";
            userEntity.IsAllowTimeEntry = "1";
            userEntity.CanEditGlobalModel = "1";
            userEntity.DashboardAlias = "Welcome";

            _response = await _session.GetSessionResponseAsync();

            sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.UserRoleManagment);
            _response.IsSuccessful.Should().BeTrue();
            userEntity.ProcessItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            userEntity.ProcessItemId.Should().NotBeNull();

            // Search for User 
            _response = await _lookUpService.GetWorkListAsync(sessionId, userEntity.ProcessItemId, userEntity.UserName);
            var quickSearchResult = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (_response.Content.Length > 2 && quickSearchResult.Rows.Any(x => x.Attributes.BaseUserName.Equals(userEntity.UserName)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, userEntity.ProcessItemId);
                _response.IsSuccessful.Should().BeTrue();
                Console.WriteLine("The Given User exits:" + userEntity.UserName);
                return false;
            }

            //Add New
            _response = await _process.AddNewProcessAsync(sessionId, userEntity.ProcessItemId, ApiConstants.UserRoleManagment, "NxUser");
            _response.IsSuccessful.Should().BeTrue();

            userEntity.UserRoleManagementId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            userEntity.UserRoleManagementId.Should().NotBeNull();
            Console.WriteLine("UserRoleManagementId: " + userEntity.UserRoleManagementId);

            _response = await userRoleManagementService.GetDataRoleAsync(sessionId, userEntity.ProcessItemId, userEntity);
            _response.IsSuccessful.Should().BeTrue();
            var dataRoleResponse = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            userEntity.DataRoleValue = dataRoleResponse.Rows.FirstOrDefault(value => value.Alias.Equals(userEntity.DataRoleAlias)).RowKey;
            userEntity.DataRoleValue.String.Should().NotBeNullOrEmpty();

            _response = await userRoleManagementService.GetDashboardAsync(sessionId, userEntity.ProcessItemId, userEntity);
            _response.IsSuccessful.Should().BeTrue();
            var dashboardResponse = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            userEntity.DashboardValue = dashboardResponse.Rows.FirstOrDefault(value => value.Alias.Equals(userEntity.DashboardAlias)).RowKey;
            userEntity.DashboardValue.String.Should().NotBeNullOrEmpty();

            userEntity.DefaultOperatingUnitValue = await LookUp.GetLookUpKeyValueByAliasAsync(sessionId, "NxUnit", userEntity.DefaultOperatingAlias);
            
            userEntity.LanguageValue = await LookUp.GetLookUpKeyValueByAliasAsync(sessionId, "NxFWKLanguage", userEntity.LanguageAlias);

            _response = await userRoleManagementService.AddUserAsync(sessionId, userEntity.ProcessItemId, userEntity);
            _response.IsSuccessful.Should().BeTrue();
            return true;
        }

        public async Task AddRoleToUser(UserRoleManagementEntity userEntity)
        {

            foreach(var userRole in userEntity.UserRole)
            {
                //adding child role
                _response = await _process.AddNewSubProcessAsync(sessionId, userEntity.ProcessItemId, userEntity.UserRoleManagementId, ApiConstants.UserRoleManagment, ApiConstants.UserRoleChild);
                _response.IsSuccessful.Should().BeTrue();

                //Getting Child ID
                userRole.UserRoleManagementChildId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
                userRole.UserRoleManagementChildId.Should().NotBeNull();
                Console.WriteLine("UserRoleManagementChildId: " + userRole.UserRoleManagementChildId);

                //Getting User Role Value from Alias

                _response = await userRoleManagementService.GetUserRoleAdvancedSearchList(sessionId, userEntity.ProcessItemId, userEntity, userRole.UserRoleManagementChildId, userRole.UserRoleAlias);
                _response.IsSuccessful.Should().BeTrue();
                var roleResponse = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                userRole.UserRoleValue = roleResponse.Rows.FirstOrDefault(value => value.Alias.Equals(userRole.UserRoleAlias)).RowKey;
                userRole.UserRoleValue.String.Should().NotBeNullOrEmpty();
            }

            userEntity.UserRole.Count().Should().BeGreaterThan(0);

            _response = await userRoleManagementService.AddUserRolesAsync(sessionId, userEntity.ProcessItemId, userEntity);
            _response.IsSuccessful.Should().BeTrue();

        }

        public async Task SubmitUserCreation(UserRoleManagementEntity userRoleManagementEntity)
        {
            //submit
            _response = await _process.PostReleaseProcessAsync(sessionId, userRoleManagementEntity.ProcessItemId, ApiConstants.UserRoleManagment);
            _response.Content.Should().Contain("responseType\":1");

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted User Name: " + userRoleManagementEntity.UserName);
            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);
        }

    }
}
