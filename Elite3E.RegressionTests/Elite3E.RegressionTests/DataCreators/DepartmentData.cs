using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.Department;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class DepartmentData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public ILookUpService _lookUpService = new LookUpService();
        public IDepartmentService _departmentService = new DepartmentService();


        public async Task<ApiDepartmentEntity> SearchAndCreateDepartmentAsync(ApiDepartmentEntity departmentEntity)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.Department);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Search for Existing Department using Description
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, departmentEntity.Description);
            var quickResponse = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (quickResponse.Rows != null && quickResponse.Rows.Any(s => isEqualsIgnoreCase(s.Attributes.Description,departmentEntity.Description) || isEqualsIgnoreCase(s.Attributes.Code, departmentEntity.DepartmentCode)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Department Description Exists : " + departmentEntity.Description);
                return departmentEntity;
            }

            //Search for Existing Department using Code
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, departmentEntity.DepartmentCode);
            quickResponse = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (quickResponse.Rows != null && quickResponse.Rows.Any(s => isEqualsIgnoreCase(s.Attributes.Description,departmentEntity.Description) || isEqualsIgnoreCase(s.Attributes.Code, departmentEntity.DepartmentCode)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Department Code Exists : " + departmentEntity.DepartmentCode);
                return departmentEntity;
            }

            //Create new
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.Department);
            _response.IsSuccessful.Should().BeTrue();

            var departmentID = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
           
            AssertionExtensions.Should((string)departmentID).NotBeNull();
            Console.WriteLine("Department Id: " + departmentID);
            departmentEntity.Id = departmentID;

            //--Mandatory Data Input--

            //Inputting Code
            await _departmentService.AddDepartmentCodeAsync(sessionId, processItemId, departmentEntity);

            //Inputting Description
            await _departmentService.AddDescriptionAsync(sessionId, processItemId, departmentEntity);

            //Inputting GL Department
            _response = await _departmentService.GetGLDepartmentAsync(sessionId, processItemId, departmentEntity);
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            var row = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(departmentEntity.GLDepartmentAlias));
            departmentEntity.GLDepartmentValue = row.RowKey;
            departmentEntity.GLDepartmentAlias = row.Attributes.GLValue;
            await _departmentService.AddGLDepartmentAsync(sessionId, processItemId, departmentEntity);

            //--Optional Data Input--

            //Inputting Department Group
            if (!string.IsNullOrEmpty(departmentEntity.DepartmentGroupAlias))
            {
                departmentEntity.DepartmentGroupValue = await LookUp.GetLookUpKeyValueByAliasAsync(sessionId, "DepartmentGroup", departmentEntity.DepartmentGroupAlias);
                await _departmentService.AddDepartmentGroupAsync(sessionId, processItemId, departmentEntity);
            }

            //Inputting Default Checkbox
            if (!string.IsNullOrEmpty(departmentEntity.IsDefaultCheckBoxAlias))
            {
                departmentEntity.IsDefaultCheckBoxValue = ResolveCheckbox(departmentEntity.IsDefaultCheckBoxAlias);
                await _departmentService.AddIsDefaultCheckboxAsync(sessionId, processItemId, departmentEntity);
            }

            //Inputting Active Checkbox
            if (!string.IsNullOrEmpty(departmentEntity.IsActiveCheckBoxAlias))
            {
                departmentEntity.IsActiveCheckBoxValue = ResolveCheckbox(departmentEntity.IsActiveCheckBoxAlias);
                await _departmentService.AddIsActiveCheckboxAsync(sessionId, processItemId, departmentEntity);
            }

            //Inputting Start Date
            if (!string.IsNullOrEmpty(departmentEntity.StartDate))
            {
                //Ensure Format is: 2022-02-17 (yyyy-MM-dd)
                await _departmentService.AddStartDateAsync(sessionId, processItemId, departmentEntity);
            }

            //Inputting End Date
            if (!string.IsNullOrEmpty(departmentEntity.EndDate))
            {
                //Ensure Format is: 2022-02-17 (yyyy-MM-dd)
                await _departmentService.AddEndDateAsync(sessionId, processItemId, departmentEntity);
            }

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.Department);
            _response.Content.Should().Contain("responseType\":1");
            
            if (_response.IsSuccessful)
            {
                Console.WriteLine("departmentEntity has been Created");
                return departmentEntity;
            }

            Console.WriteLine("Failed To submit Department Creation");
            return departmentEntity;
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
