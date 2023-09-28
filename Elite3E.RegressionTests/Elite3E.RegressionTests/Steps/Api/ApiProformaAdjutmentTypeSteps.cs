using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.PartialCreditNotes;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiProformaAdjutmentTypeSteps
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ApiProformaAdjutmentTypeSteps(FeatureContext featureContext)
        {
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
            _featureContext = featureContext;
        }

        [When(@"I get adjustment code from Api")]
        public async Task WhenIGetAdjustmentCodeFromApi()
        {
            var defaultAdjType = _actor.GetElementText(PartialCreditNotesLocators.CreditNoteAdjustmentTypeInput);
            _featureContext[StepConstants.AdjustmentTypeDescriptionContext] = defaultAdjType;
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.PartialCreditNotes);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.PartialCreditNotes);
            _response.IsSuccessful.Should().BeTrue();

            try
            {
                var code = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "ProfAdjustType", defaultAdjType);
                _featureContext[StepConstants.AdjustmentTypeCodeContext] = code;
            }
            catch (Exception e)
            {
                Console.WriteLine("ProfAdjustType Code doesn't exists. API didn't responded with required value.." + e.ToString());
            }
            _response = await _process.PostCancelProcessAsync(sessionId, processItemId);

        }

    }

}
