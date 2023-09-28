using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ChargeTypeGroup;
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

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class ChargeTypeGroupTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IChargeTypeGroupService _chargeTypeGroupService = new ChargeTypeGroupService();

        [Test]
        public async Task CreateChargeTypeGroupExcludeListTask()
        {
            var chargeTypeGroupEntity = new ApiChargeTypeGroupEntity()
            {
                ChargeTypeGroupCode = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                ChargeTypeGroupDescription = "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                ChargeTypeGroupExcludeOrIncludeListOption = ApiConstants.IsExcludeList,
                ChargeTypeGroupIsExcludeOrIncludeListValue = "1"
            };
            await CreateChargeTypeGroup(chargeTypeGroupEntity);
        }

        [Test]
        public async Task CreateChargeTypeGroupIncludeListTask()
        {
            var chargeTypeGroupEntity = new ApiChargeTypeGroupEntity()
            {
                ChargeTypeGroupCode = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                ChargeTypeGroupDescription = "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                ChargeTypeGroupExcludeOrIncludeListOption = ApiConstants.IsIncludeList,
                ChargeTypeGroupIsExcludeOrIncludeListValue = "1"
            };
            await CreateChargeTypeGroup(chargeTypeGroupEntity);
        }

        private async Task CreateChargeTypeGroup(ApiChargeTypeGroupEntity chargeTypeGroupEntity)
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ChargeTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();

            var chargeTypeGroupId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            chargeTypeGroupId.Should().NotBeNull();
            Console.WriteLine("chargeTypeGroupId: " + chargeTypeGroupId);
            chargeTypeGroupEntity.ChargeTypeGroupId = chargeTypeGroupId;

            _response = await _chargeTypeGroupService.AddChargeTypeGroupAsync(sessionId, processItemId, chargeTypeGroupEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeGroupProcess);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted ChargeTypeGroupCode : " + chargeTypeGroupEntity.ChargeTypeGroupCode.String);

            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);
        }
    }
}
