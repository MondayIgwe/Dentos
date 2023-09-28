using Boa.Constrictor.Screenplay;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GLNatural;
using Elite3E.RegressionTests.StepHelpers;
using System;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class CreateGLAccountLocalNaturalRequiredStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public CreateGLAccountLocalNaturalRequiredStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Then(@"I want to add secure journal user/role access")]
        public void ThenIWantToAddSecureJournalUserRoleAccess(Table table)
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.SecureJournalUserRoleAccess, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(GLNaturalLocators.RoleDropdown, table.Rows[0]["Role"]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StartDate, table.Rows[0]["StartDate"], 2));
        }
    }
}
