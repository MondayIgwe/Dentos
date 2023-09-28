using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ProfileRoleService;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elite3E.RestAPI.Models.Response;
using Elite3E.RestServices.Models.ResponseModels.Profile;
using System.Linq;
using System;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ProfileData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private ILookUpService _lookUpService = new LookUpService();
        public IProfileService _profileService = new ProfileService();

        /// <summary>
        /// Searches the Profile/User then returns it's assigned roles
        /// </summary>
        /// <param name="profileEntity"></param>
        /// <returns></returns>
        public async Task<List<string>> SearchAndVerifyProfileRoles(ApiProfileEntity profileEntity)
        {
            //Get Session ID
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.NxBaseUser);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Quick Search the User/Profile
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, profileEntity.Profile);
            var existingProfile = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            if(existingProfile.RowCount >= 1)
            {
                profileEntity.ProfileRowKey = existingProfile.Rows
                                        .FirstOrDefault(x => x.Attributes.BaseUserName == profileEntity.Profile).RowKey;
            }             
            else         
                Assert.Fail("No such User/Profile: " + profileEntity.Profile + " Exists");
            

            //Select the Profile
            _response = await _profileService.SelectTheProfileAsync(sessionId, processItemId, profileEntity);
            _response.IsSuccessful.Should().BeTrue();

            //Get the Roles Assigned to the Profile/User
            _response = await _profileService.GetBaseUserRoles(sessionId, processItemId, profileEntity.ProfileRowKey);
            _response.IsSuccessful.Should().BeTrue();

            //Save the list
            BaseUserRoot result = JsonConvert.DeserializeObject<BaseUserRoot>(_response.Content);
            List<string> roles = new List<string>();

            foreach (var item in result.data.objects.NxUser_RoleChild.Rows)
            {
                roles.Add(item.Value.Attributes.RoleID.AliasValue);
            }
            _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();

            return roles;
        }
    }
}
