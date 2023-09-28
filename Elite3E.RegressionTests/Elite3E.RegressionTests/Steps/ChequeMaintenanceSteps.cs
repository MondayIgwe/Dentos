using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChequeMaintenance;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountAdjustment;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ChequeMaintenanceSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ChequeMaintenanceSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I select an existing cheque")]
        public void ThenISelectAnExistingCheque()
        {
            var chequeNumber = _featureContext[StepConstants.ChequeNumber].ToString();
            _actor.AttemptsTo(QuickFind.Search(chequeNumber));
        }

        [StepDefinition(@"I void the cheque")]
        public void ThenIVoidTheCheque(Table table)
        {
            _actor.AttemptsTo(Click.On(ChequeMaintenanceLocators.VoidChequeBox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate("Void Date", table.Rows[0][ColumnNames.VoidDate]));
            _actor.AttemptsTo(SendKeys.To(ChequeMaintenanceLocators.VoidReason, table.Rows[0][ColumnNames.VoidReason]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }
        [Then(@"I verify the audit button opens the auditing window")]
        public void ThenIVerifyTheAuditButtonOpensTheAuditingWindow()
        {
            _actor.AttemptsTo(Click.On(ChequeMaintenanceLocators.AuditButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
