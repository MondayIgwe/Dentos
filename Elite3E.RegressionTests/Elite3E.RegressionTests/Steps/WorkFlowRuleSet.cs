using TechTalk.SpecFlow;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using FluentAssertions;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.WorkFlowRuleRouting;
using OpenQA.Selenium;
using Elite3E.Infrastructure.Selenium;
using System;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WorkflowRules;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class WorkFlowRuleSet
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public WorkFlowRuleSet(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the Worflow Rule Set Routing process")]
        public void GivenINavigateToTheWorflowRuleSetRoutingProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.WorkflowRuleSet, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"all the correct fields should exist")]
        public void ThenAllTheCorrectFieldsShouldExist()
        {
            
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowRule));
            if (_actor.DoesElementExist(CommonLocator.ExpandButton))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ExpandButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            _actor.AsksFor(Field.IsAvailable(WorkflowRuleRoutingLocators.WorkFlowRuleCard)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(WorkflowRuleRoutingLocators.WorkflowRuleSetHeader)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(WorkflowRuleRoutingLocators.DoARoleTypeHeader)).Should().Be(true);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I provide all rule set  data")]
        public void WhenIProvideAllRuleSetData(Table table)
        {
             string doaRole = _featureContext[StepConstants.DoARoleContext].ToString();
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.WorkflowRule, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(WorkflowRuleRoutingLocators.RuleSetDropdown, table.Rows[0]["RuleSet"]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(WorkflowRuleRoutingLocators.DoaRoleType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(WorkflowRuleRoutingLocators.RoleSetInput, doaRole + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"the rules must be saved correctly")]
        public void WhenTheRulesMustBeSavedCorrectly(Table table)
        {
            string doaRole = _featureContext[StepConstants.DoARoleContext].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.WorkflowRuleSet, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(WorkflowRuleRoutingLocators.Role(doaRole))).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(WorkflowRuleRoutingLocators.Role(table.Rows[0]["RuleSet"]))).Should().Be(true);
            _actor.AttemptsTo(DeleteWorkflowRoutingRule.With(doaRole, table.Rows[0]["RuleSet"]));
            _actor.AttemptsTo(SearchProcess.ByName(Process.WorkflowRuleSet, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(WorkflowRuleRoutingLocators.Role(doaRole)), IsEqualTo.False(), 1);
            _actor.WaitsUntil(Appearance.Of(WorkflowRuleRoutingLocators.Role(table.Rows[0]["RuleSet"])), IsEqualTo.False(), 1);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }







    }
}
