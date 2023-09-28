using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.BillingGroup;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class BillingGroupTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IBillingGroupService _billingGroupService = new BillingGroupService();

        [Test]
        public async Task CreateBillingGroupTask()
        {
            var billingGroupEntity = new ApiBillingGroupEntity()
            {
              Name = "ICB-Unit 1002" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
              Icb = "1",
              UnitDueFromDescription = "Dentons PNG",
              UnitDueToDescription = "Dentons UK and Middle East LLP",
              Description = "ICB-Unit 1002" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
              MatterNumber = "100000010" // Always need to change the matter number as once used is not available for any other Billing Group
            };
            await CreateBillingGroup(billingGroupEntity);
        }

        private async Task CreateBillingGroup(ApiBillingGroupEntity billingGroupEntity)
        {
            //Get Session Id 
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.BillingGroupName);
            _response.IsSuccessful.Should().BeTrue();

            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            // Adding New Guid Id for the Record (Clicking on Add Button)
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.BillingGroupName);
            _response.IsSuccessful.Should().BeTrue();

            billingGroupEntity.Id = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            billingGroupEntity.Id.Should().NotBeNull();
            Console.WriteLine("billingGroupId: " + billingGroupEntity.Id);

            // Calling Dropdown to get the value from the description provided for Unit From
            billingGroupEntity.UnitDueFromValue = await LookUp.GetLookUpKeyValueByAliasAsync(sessionId, "NxUnit", billingGroupEntity.UnitDueFromDescription);
           
            // Calling Dropdown to get the value from the description provided Unit To
            billingGroupEntity.UnitDueToValue = await LookUp.GetLookUpKeyValueByAliasAsync(sessionId, "NxUnit", billingGroupEntity.UnitDueToDescription);

            // Add the required data
            _response = await _billingGroupService.AddBillingGroupAsync(sessionId, processItemId, billingGroupEntity);
            _response.IsSuccessful.Should().BeTrue();

            // Get the Matter
            _response = await _billingGroupService.GetBillingGroupMatterAsync(sessionId, processItemId, billingGroupEntity);

            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            // Assigning the Matter key Value
            billingGroupEntity.MatterKeyValue = new List<Guid>
            {
                new(quickSearch.Rows
                    .FirstOrDefault(value => value.Attributes.Number.Equals(billingGroupEntity.MatterNumber)).RowKey)
            };

            // Adding the Matters to the Billing Group
            _response = await _billingGroupService.AddBillingGroupMatterAsync(sessionId, processItemId,
                billingGroupEntity);

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.BillingGroupName);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Billing Group : " + billingGroupEntity.Description.String);

            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);
        }
    }
}
