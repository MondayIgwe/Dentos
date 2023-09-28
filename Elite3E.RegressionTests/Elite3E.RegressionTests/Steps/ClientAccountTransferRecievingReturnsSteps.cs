using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WorkFlowDashbord;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountDisbursement;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountTransfer;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.WorkFlowDashBoard;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class ClientAccountTransferRecievingReturnsSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        public ClientAccountTransferRecievingReturnsSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];

        }

        [Then(@"Then i open the client account transfer")] 
        public void ThenThenIOpenTheClientAccountTransfer()
        {
            var PaymentAmount =  _featureContext[StepConstants.AmountNumberContext].ToString(); 

            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.ClientAccountTransfer));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(PaymentAmount, GlobalConstants.TrustTransferRequestEntryFinanceApproval));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.CompletedDateSortTransferRequest2));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.OpenButton));

        }

        [StepDefinition(@"I proxy as user transferring matter's billing fee earner")]
        public void IProxyAsTransferingUser()
        {
            var user = _featureContext[StepConstants.TransferingBillingTimekeeper].ToString();
            _featureContext[StepConstants.User] = user;
            _actor.AttemptsTo(ProxyAs.User(user));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I request approval from the '([^']*)' account fee earner")]
        public void ThenIRequestApprovalFromTheAccountFeeEarner(string to)
        {
            var FeeEaner = _featureContext[StepConstants.FeeEarnerName].ToString();
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.ApprovedBy, FeeEaner + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.ApprovalRequired));
        }
        [Given(@"I proxy as receiving matter's billing fee earner")]
        public void GivenIProxyAsReceivingMattersBillingFeeEarner()
        {
            var FeeEaner = _featureContext[StepConstants.RecievingBillingTimekeeper].ToString();
            _actor.AttemptsTo(ProxyAs.User(FeeEaner));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [Then(@"I reopen the client account transfer")]
        public void ThenIReopenTheClientAccountTransfer()
        {
            var PaymentAmount = _featureContext[StepConstants.AmountNumberContext].ToString();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(PaymentAmount, GlobalConstants.TrustTransferApproval));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.CompletedDateSortTransferRequest2));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.OpenButton));

        }
        [Then(@"I return it")]
        public void ThenIRejectIt()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Return));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Ok));
        }

        [Then(@"Then i open the client account transfer link for trust transfer finance approval")]
        public void ThenThenIOpenTheClientAccountTransferLinkForTrustTransferFinanceApproval()
        {
            var PaymentAmount = _featureContext[StepConstants.AmountNumberContext].ToString(); 

            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.ClientAccountTransfer));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(PaymentAmount, GlobalConstants.TrustTransferFinanceApproval));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.CompletedDateSortTransferRequest2));
             _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());    
             _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.OpenButton));
        }

    }
}
