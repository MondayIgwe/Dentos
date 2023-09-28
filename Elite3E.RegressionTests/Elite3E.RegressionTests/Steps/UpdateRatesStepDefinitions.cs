using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Helper;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.RateMaintenance;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Timekeeper;
using Elite3E.RegressionTests.StepHelpers;
using OpenQA.Selenium;
using System.IO;
using TechTalk.SpecFlow;
using UploadFile = Elite3E.PageObjects.Interaction.CommonInteraction.UploadFile;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class UpdateRatesStepDefinitions
    {
        private readonly FeatureContext _featureContext;
        private readonly Actor _actor;
        public UpdateRatesStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }
        [StepDefinition(@"I upload '([^']*)'")]
        public void GivenIUpload(string upload)
        {
            string fileNameWithPath = Path.Combine(SystemIOHelper.DIR_RESOURCES, upload);
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.UploadButton), IsEqualTo.True());
            _actor.AttemptsTo(Click.On(TimeKeeperLeaverChecklistLocators.UploadButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(UploadFile.Upload(upload));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }
        [Given(@"I navigate fee earner rate load and profitability cost rate load")]
        public void GivenINavigateFeeEarnerRateLoadAndProfitabilityCostRateLoad()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerRateLoadProfitabilityCostRateLoad,false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I click the '([^']*)' button\.")]
        public void ThenIClickTheButton_(string validate)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Button(validate)));
        }
        [Given(@"I search for the rate")]
        public void GivenISearchForTheRate()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var rateCode = _featureContext[StepConstants.RateCode].ToString();

            _actor.AttemptsTo(QuickFind.Search(rateCode));
        }
        [Then(@"I update rate type value '([^']*)'")]
        public void ThenIUpdateRateTypeValue(string amount)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(RateMaintenanceLocators.RateType1Value, amount + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}


