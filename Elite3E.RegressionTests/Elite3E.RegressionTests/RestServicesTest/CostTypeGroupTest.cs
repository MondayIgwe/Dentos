using Elite3E.Infrastructure.Extensions;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Services.CostTypeGroup;
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
    public class CostTypeGroupTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public ICostTypeGroupService _costTypeGroupService = new CostTypeGroupService();


        [Test]
        public async Task CreateCostTypeGroupIncludeListTask()
        {
            var costTypeEntity = new ApiCostTypeGroupEntity()
            {
                Code = "CTCode_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+7"),
                Description = "CTDesc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+7"),
                CostTypeGroupIsExcludeOrIncludeListValue = "1",
                CostTypeGroupExcludeOrIncludeListOption = ApiConstants.IsIncludeList
            };

            await CreateCostTypeGroup(costTypeEntity);
        }

        [Test]
        public async Task CreateCostTypeGroupExcludeListTask()
        {
            var costTypeEntity = new ApiCostTypeGroupEntity()
            {
                Code = "CTCode_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+7"),
                Description = "CTDesc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+7"),
                CostTypeGroupIsExcludeOrIncludeListValue ="1",
                CostTypeGroupExcludeOrIncludeListOption = ApiConstants.IsExcludeList,
            };

            await CreateCostTypeGroup(costTypeEntity);
        }

        private async Task CreateCostTypeGroup(ApiCostTypeGroupEntity costTypeEntity)
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.CostTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.CostTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();

            var costTypeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            costTypeId.Should().NotBeNull();
            Console.WriteLine("Cost Type Id: " + costTypeId);
            costTypeEntity.CostTypeId = costTypeId;
         
            _response = await _costTypeGroupService.AddCostTypeGroupDataAsync(sessionId, processItemId, costTypeEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.CostTypeGroupProcess);
            if (_response.IsSuccessful)
            {
                Console.WriteLine("Submitted CostTypeGroupCode : " + costTypeEntity.Code.String);
            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);
        }
    }
}
