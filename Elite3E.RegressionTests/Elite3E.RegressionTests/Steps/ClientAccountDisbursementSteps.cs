using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ProformaTrust;
using FluentAssertions;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.Infrastructure.Selenium;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.Interaction.ProcessInteraction.ClientAccountDisbursement;
using System;
using Elite3E.PageObjects.PageLocators.ClientAccountDisbursement;
using OpenQA.Selenium;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ClientAccountDisbursementSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ClientAccountDisbursementSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the client account disbursement process")]
        public void GivenINavigateToTheClientAccountDisbursementProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountDisbursement));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I add a new client account disbursement record")]
        public void GivenIAddANewClientAccountDisbursementRecord(Table table)
        {
            var clientAccountDisbursementEntity = table.CreateInstance<ClientAccountDisbursementEntity>();
            clientAccountDisbursementEntity.MatterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            if (String.IsNullOrEmpty(clientAccountDisbursementEntity.IntendedUse))
            {
                clientAccountDisbursementEntity.IntendedUse = _featureContext[StepConstants.ClientAccountCodeContext].ToString();
            }
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAccountDisbursement));
            _actor.AttemptsTo(EnterClientAccountDisbursmentInfo.With(clientAccountDisbursementEntity));

        }

        [Given(@"I add new disbursement")]
        public void GivenIAddNewDisbursement()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAccountDisbursement));
        }

        [Given(@"I load balance for the client account")]
        public void GivenILoadBalanceForTheClientAccount(Table table)
        {
            var clientAccountDisbursementEntity = table.CreateInstance<ClientAccountDisbursementEntity>();

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientAccountDisbursementLocators.DisbursementTypeInput, clientAccountDisbursementEntity.DisbursementType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(clientAccountDisbursementEntity.ClientAccountAcct))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Client Account Acct", clientAccountDisbursementEntity.ClientAccountAcct));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("Balance")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.CheckSelectbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Hover.Over(CommonLocator.SelectButtonDialog));
            _actor.AttemptsTo(DoubleClick.On(CommonLocator.SelectButtonDialog));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(clientAccountDisbursementEntity.Amount))
            {
                if (_actor.DoesElementExist(ClientAccountDisbursementLocators.AmountInput))
                {
                    _actor.AttemptsTo(SendKeys.To(ClientAccountDisbursementLocators.AmountInput, clientAccountDisbursementEntity.Amount));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                }
                if (!string.IsNullOrEmpty(clientAccountDisbursementEntity.PaymentName))
                {
                    _actor.AttemptsTo(SendKeys.To(ClientAccountDisbursementLocators.PaymentNameInput, clientAccountDisbursementEntity.PaymentName));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
            }
        }

        [When(@"I get the disbursement index of the client account disbursement")]
        public void WhenIGetTheDisbursementIndexOfTheClientAccountDisbursement()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.CardIndex] = _actor.AsksFor(Text.Of(ClientAccountDisbursementLocators.TrustDisbursementIndex));
        }
        [When(@"I navigate to the client account disbursement section")]
        public void WhenINavigateToTheClientAccountDisbursementSection()
        {
            _actor.WaitsUntil(Appearance.Of(ClientAccountDisbursementLocators.ClientAccountDisbursementSection), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.ClientAccountDisbursementSection));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I navigate to the client disbursement section and search by '([^']*)'")]
        public void WhenINavigateToTheClientDisbursementSectionAndSearchBy(string feeEarnerName)
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            var amount = Convert.ToDecimal(_featureContext[StepConstants.AmountNumberContext].ToString()).ToString("#.00");
            var todayDate = DateTime.Now.ToShortDateString();
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.TrustDisbursementRequestFinanceApprovalFilterIcon));
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.CompletedDateSortDisbursementRequestEntry));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.CompletedDateSortDisbursementRequestEntry));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAccountDisbursementLocators.TrustDisbursementRequestFinanceInput, feeEarnerName + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ClientAccountDisbursementLocators.TrustDisbursementGenericOpenButton("Trust Disbursement Request Entry - Finance Approval", amount, todayDate)), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.TrustDisbursementGenericOpenButton("Trust Disbursement Request Entry - Finance Approval", amount, todayDate)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowHistory));
            _actor.AsksFor(Text.Of(ClientAccountDisbursementLocators.MatterInput)).Should().Contain(matter);
        }

        [When(@"I navigate to trust disbursement finance approval section and search by '([^']*)'")]
        public void WhenINavigateToTrustDisbursementFinanceApprovalSectionAndSearchBy(string feeEarnerName)
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            var amount = Convert.ToDecimal(_featureContext[StepConstants.AmountNumberContext].ToString()).ToString("#.00");
            var todayDate = DateTime.Now.ToShortDateString();
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.TrustDisbursementFinanceApprovalFilterIcon));
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.CompletedDateSortDisbursementFinanceApproval));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.CompletedDateSortDisbursementFinanceApproval));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAccountDisbursementLocators.TrustDisbursementRequestFinanceApprovalInput, feeEarnerName + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ClientAccountDisbursementLocators.TrustDisbursementGenericOpenButton("Trust Disbursement Finance Approval - Approval ", amount, todayDate)), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountDisbursementLocators.TrustDisbursementGenericOpenButton("Trust Disbursement Finance Approval - Approval ", amount, todayDate)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowHistory));
            _actor.AsksFor(Text.Of(ClientAccountDisbursementLocators.MatterInput)).Should().Contain(matter);
        }
    }

}

