using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountCheque;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ClientAccountChequeSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ClientAccountChequeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the client account cheque process")]
        public void GivenINavigateToTheClientAccountChequeProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountCheque));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add new disbursement for the client account cheque")]
        public void GivenIAddNewDisbursementForTheClientAccountCheque()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAccountCheques));
            _actor.AttemptsTo(ChildProcessView.SwitchToView("Disbursements", "Form"));
            _actor.AttemptsTo(Click.On(ClientAccountChequeLocator.SelectNewDisbursementDropDown));
            _actor.AttemptsTo(Click.On(ClientAccountChequeLocator.SelectNewDisbursementDropDownOption));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }


        [Given(@"I add a new client account cheque")]
        public void GivenIAddANewClientAccountCheque(Table table)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var clientAccountChequeEntity = table.CreateInstance<ClientAccountChequeEntity>();
            clientAccountChequeEntity.ChequeNumber = new Random().Next(100, 12000).ToString();
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Client Account Acct", clientAccountChequeEntity.ClientAccountAcc));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientAccountChequeLocator.ChequeNumber, clientAccountChequeEntity.ChequeNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.ChequeNumber] = clientAccountChequeEntity.ChequeNumber;

            _actor.AttemptsTo(SendKeys.To(ClientAccountChequeLocator.NameCheque, clientAccountChequeEntity.NameOnCheque));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Printer", clientAccountChequeEntity.Printer));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Print Template", clientAccountChequeEntity.Template));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Given(@"I void a cheque")]
        public void GivenIVoidACheque(Table table)
        {
            _actor.AttemptsTo(Checkbox.SetStatus(ClientAccountChequeLocator.IsVoidedChequebox,true));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Void Reason", table.Rows[Index.Start][ColumnNames.VoidReason]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.DoesElementExist(ClientAccountChequeLocator.ReverseDisbursementCheckbox).Should().BeTrue();
            _featureContext[StepConstants.DisbursementCard] = _actor.GetElementText(ClientAccountChequeLocator.DisbursementNumberDiv);

        }

        [Then(@"I verify that the cheque disbursement does not exist")]
        public void ThenIVerifyThatTheChequeDisbursementDoesNotExist()
        {
            var disbursementNumber = _featureContext[StepConstants.DisbursementCard].ToString();
            _actor.AttemptsTo(QuickFind.Search(disbursementNumber));
            _actor.DoesElementExist(CommonLocator.NoSearchRecords).Should().BeTrue();
        }






    }
}
