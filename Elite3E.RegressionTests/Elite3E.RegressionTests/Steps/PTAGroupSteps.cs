using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.PTAGroup;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class PTAGroupSteps
    {
        private readonly FeatureContext _featureContext;
        private readonly Actor _actor;

        public PTAGroupSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the pta group process")]
        public void GivenINavigateToThePtaGroupProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PTAGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I fill out the pta group fields")]
        public void GivenIFillOutThePtaGroupFields(Table table)
        {
            var phaseListCode = _featureContext[StepConstants.PhaseListCode].ToString();
            var taskListCode = _featureContext[StepConstants.TaskListCode].ToString();
            var activityListCode = _featureContext[StepConstants.ActivityListCode].ToString();

            _actor.AttemptsTo(SendKeys.To(PTAGroupLocators.PTAGroupCodeInput, table.Rows[0][ColumnNames.PtaGroupCode])) ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
           
            _actor.AttemptsTo(SendKeys.To(PTAGroupLocators.PTAGroupDescriptionInput, table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(PTAGroupLocators.PhaseListDropdown, phaseListCode + Keys.Enter));
            _actor.AttemptsTo(SendKeys.To(PTAGroupLocators.TaskListDropdown, taskListCode + Keys.Enter));
            _actor.AttemptsTo(SendKeys.To(PTAGroupLocators.ActivityListDropdown, activityListCode + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[ColumnNames.PtaGroupCode] = table.Rows[0][ColumnNames.Description];
        }

        [When(@"I add the pta group to the matter")]
        public void WhenIAddThePtaGroupToTheMatter()
        {
            var ptaGroupDescription = _featureContext[ColumnNames.PtaGroupCode].ToString();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Matter));
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.EffectiveDatedInformation, GlobalConstants.Form));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("PTA Group Fees 1", ptaGroupDescription));
        }

        [When(@"I verify the pta group is added to the matter")]
        public void WhenIVerifyThePtaGroupIsAddedToTheMatter()
        {
            var ptaGroupDescription = _featureContext[ColumnNames.PtaGroupCode].ToString();
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.EffectiveDatedInformation, GlobalConstants.Form));
            _actor.GetElementText(PTAGroupLocators.PtaGroupFeesInput).Should().BeEquivalentTo(ptaGroupDescription) ;
        }

    }
}
