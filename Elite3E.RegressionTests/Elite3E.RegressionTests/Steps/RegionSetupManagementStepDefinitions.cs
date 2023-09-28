using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;


namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class RegionSetupManagementStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public RegionSetupManagementStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

       
        [Then(@"I want to verify that the region has been added")]
        public void ThenIWantToVerifyThatTheRegionHasBeenAdded()
        {
            var regionCode = ((RegionEntity)_featureContext[StepConstants.RegionEntity]).Code;
            _actor.AttemptsTo(SearchProcess.ByName(Process.Region));
            _actor.AttemptsTo(QuickFind.Search(regionCode));
        }

  

        [Then(@"I want to verify that the region has been updated")]
        public void ThenIWantToVerifyThatTheRegionHasBeenUpdated()
        {
            string text = "updated description";
            var regionCode = ((RegionEntity)_featureContext[StepConstants.RegionEntity]).Code;
            _actor.AttemptsTo(SearchProcess.ByName(Process.Region));
            _actor.AttemptsTo(QuickFind.Search(regionCode));
            bool state = _actor.AsksFor(SelectedState.Of(CommonLocator.GetIsDisableIntegrationCheckbox));
            state.Should().BeFalse();
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var description = driver.FindElement(CommonLocator.Description.Query).GetAttribute("value");
            description.Should().Contain(text);
        }

        [Then(@"I want to update the region")]
        public void ThenIWantToUpdateTheRegion(Table table)
        {
            var regionEntity = table.CreateInstance<RegionEntity>();
            var regionCode = ((RegionEntity)_featureContext[StepConstants.RegionEntity]).Code;
            _actor.AttemptsTo(SearchProcess.ByName(Process.Region));
            _actor.AttemptsTo(QuickFind.Search(regionCode));
            _actor.AttemptsTo(EditRegionProcess.EditRegionDetails(regionEntity));
        }
    }
}