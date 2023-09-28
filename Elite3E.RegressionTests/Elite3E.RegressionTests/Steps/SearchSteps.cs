using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class SearchSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public SearchSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"matter maintenance is open")]
        public void GivenMatterMaintenanceIsOpen()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }      

        [Then(@"verify matter is returned")]
        public void ThenVerifyMatterIsReturned()
        {
           _actor.WaitsUntil(Appearance.Of(MatterLocator.MatterTitle), IsEqualTo.True());
        }

    }
}
