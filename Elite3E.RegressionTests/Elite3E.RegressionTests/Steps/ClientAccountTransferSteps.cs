using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.ClientAccountTransfer;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountReceiptRequest;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountTransfer;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
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
    public class ClientAccountTransferSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ClientAccountTransferSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }


        [StepDefinition(@"I navigate to the client account transfer section")]
        public void GivenINavigateToTheClientAccountTransferSection()
        {
            _actor.WaitsUntil(Appearance.Of(ClientAccountReceiptRequestLocators.ClientAccountTransferSection), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountTransferSection));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I navigate to the client account transfer process")]
        public void GivenINavigateToTheClientAccountTransferProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountTransferProcess));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [StepDefinition(@"I make a client account transfer request")]
        public void IMakeAClientAccountTransferRequest(Table table)
        {

            var clientTransfer = table.CreateInstance<ClientAccountTransferEntity>();

            clientTransfer.FromMatter = _featureContext[StepConstants.SubMatterNumberContextOne].ToString();

            clientTransfer.ToMatter = _featureContext[StepConstants.SubMatterNumberContextTwo].ToString();

            if (String.IsNullOrEmpty(clientTransfer.Amount))
            {
                clientTransfer.Amount = _featureContext[StepConstants.AmountNumberContext].ToString();
            }
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountTransfer, false));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowHistory));
            _actor.AsksFor(Field.IsAvailable(ClientAccountTransferLocator.WorkflowChildForm)).Should().Be(true);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ClientAccountTransfer.With(clientTransfer));
            _featureContext[StepConstants.SubMatterNameContextOne] = _actor.AsksFor(Text.Of(ClientAccountTransferLocator.FromMatterName)).Trim();
            _featureContext[StepConstants.SubMatterNameContextTwo] = _actor.AsksFor(Text.Of(ClientAccountTransferLocator.ToMatterName)).Trim();
        }

        [StepDefinition(@"I navigate to the client account transfer section and search by '([^']*)'")]
        public void GivenINavigateToTheClientAccountTransferSectionAndSearchBy(string feeEarnerUser)
        {
            var matterOne = _featureContext[StepConstants.SubMatterNumberContextOne].ToString();
            var matterTwo = _featureContext[StepConstants.SubMatterNumberContextTwo].ToString();
            var amount = Convert.ToDecimal(_featureContext[StepConstants.AmountNumberContext].ToString()).ToString("#.00");
            var todayDate = DateTime.Now.ToShortDateString();
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.ClientAccountTransferFilterIcon));
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.CompletedDateSortTransferRequest));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.CompletedDateSortTransferRequest));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.ClientAccountTransferInput, feeEarnerUser + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ClientAccountTransferLocator.ClientAccountTransferGenericOpenButton("Trust Transfer Request Entry - Finance Approval", amount, todayDate)), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.ClientAccountTransferGenericOpenButton("Trust Transfer Request Entry - Finance Approval", amount, todayDate)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowHistory));
            _actor.AsksFor(Text.Of(ClientAccountTransferLocator.ApprovalFromMatterNumber)).Should().Contain(matterOne);
            _actor.AsksFor(Text.Of(ClientAccountTransferLocator.ApprovalToMatterNumber)).Should().Contain(matterTwo);
        }


        [StepDefinition(@"I open the trust transfer finance approval task and search by '([^']*)'")]
        public void GivenIOpenTheTrustTransferFinanceApprovalTaskAndSearchBy(string feeEarnerUser)
        {
            var matterOne = _featureContext[StepConstants.SubMatterNumberContextOne].ToString();
            var matterTwo = _featureContext[StepConstants.SubMatterNumberContextTwo].ToString();
            var amount = Convert.ToDecimal(_featureContext[StepConstants.AmountNumberContext].ToString()).ToString("#.00");
            var todayDate = DateTime.Now.ToShortDateString();
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.TrustTransferFilterIcon));
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.TrustTransferInput, feeEarnerUser + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.CompletedDateSortFinanceTransferRequest));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.CompletedDateSortFinanceTransferRequest));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ClientAccountTransferLocator.ClientAccountTransferGenericOpenButton("Trust Transfer Approval - Finance Approval", amount, todayDate)), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.ClientAccountTransferGenericOpenButton("Trust Transfer Approval - Finance Approval", amount, todayDate)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowHistory));
            _actor.AsksFor(Text.Of(ClientAccountTransferLocator.ApprovalFromMatterNumber)).Should().Contain(matterOne);
            _actor.AsksFor(Text.Of(ClientAccountTransferLocator.ApprovalToMatterNumber)).Should().Contain(matterTwo);
        }

        [Given(@"I open the trust transfer finance approval task in receiving fee earner inbox and search by '([^']*)'")]
        public void GivenIOpenTheTrustTransferFinanceApprovalTaskInReceivingFeeEarnerInboxAndSearchBy(string feeEarnerUser)
        {

            var amount = Convert.ToDecimal(_featureContext[StepConstants.AmountNumberContext].ToString()).ToString("#.00");
            var todayDate = DateTime.Now.ToShortDateString();
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.TrustTransferFinanceApprovalFilterIcon));
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.TrustTransferFinanceApprovalInput, feeEarnerUser + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.CompletedDateSortTrustTransferFinanceApprovalRequest));
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.CompletedDateSortTrustTransferFinanceApprovalRequest));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ClientAccountTransferLocator.ClientAccountTransferGenericOpenButton("Trust Transfer Finance Approval - Approval", amount, todayDate)), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.ClientAccountTransferGenericOpenButton("Trust Transfer Finance Approval - Approval", amount, todayDate)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var matterNameOne = _featureContext[StepConstants.SubMatterNameContextOne].ToString();
            var matterNameTwo = _featureContext[StepConstants.SubMatterNameContextTwo].ToString();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowHistory));
            _actor.AsksFor(Field.IsAvailable(ClientAccountTransferLocator.WorkflowChildForm)).Should().Be(true);
            _actor.AsksFor(Text.Of(ClientAccountTransferLocator.FromMatterName)).Should().Contain(matterNameOne);
            _actor.AsksFor(Text.Of(ClientAccountTransferLocator.ToMatterName)).Should().Contain(matterNameTwo);
        }


        [When(@"the transfer from approval checkbox is checked")]
        public void WhenTheTransferFromApprovalCheckboxIsChecked()
        {
            _actor.DoesElementExist(ClientAccountTransferLocator.TransferFromApprovedCheckbox).Should().BeTrue();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowHistory));
            _actor.AsksFor(Field.IsAvailable(ClientAccountTransferLocator.WorkflowChildForm)).Should().Be(true);
        }


        [When(@"the approval required field is entered with '([^']*)'")]
        public void WhenTheApprovalRequiredFieldIsEnteredWith(string receivingFeeEarner)
        {
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.ApprovalRequiredInput, receivingFeeEarner + Keys.Enter));
        }


        [When(@"I send the request for ApprovalRequired")]
        public void WhenISendTheRequestForApprovalRequired()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.ApprovalRequired));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add a new client account transfer request")]
        public void GivenIAddANewClientAccountTransferRequest(Table table)
        {
            var clientTransfer = table.CreateInstance<ClientAccountTransferEntity>();
            clientTransfer.FromMatter = _featureContext[StepConstants.SubMatterNumberContextOne].ToString();
            clientTransfer.ToMatter = _featureContext[StepConstants.SubMatterNumberContextTwo].ToString();

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, "Client Account Transfer"));
            var transferNumber = clientTransfer.TransferNumber + "545";
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.TransferDocumentNumber, transferNumber));
            _featureContext[StepConstants.TransactionNumber] = transferNumber;

            _actor.AttemptsTo(ClientAccountTransfer.With(clientTransfer));
        }

        [StepDefinition(@"I quick search by transfer number")]
        public void GivenIQuickSearchByTransferNumber()
        {
            _actor.AttemptsTo(QuickFind.Search(_featureContext[StepConstants.TransactionNumber].ToString()));
        }

        [When(@"I reverse the client account transfer")]
        public void WhenIReverseTheClientAccountTransfer(Table table)
        {
            _actor.AttemptsTo(Click.On(ClientAccountTransferLocator.ReverseCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Reverse Reason", table.Rows[0][ColumnNames.Reason]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Then(@"I verify the transfer does not exist")]
        public void ThenIVerifyTheTransferDoesNotExist()
        {
            _actor.DoesElementExist(CommonLocator.NoSearchRecords).Should().BeTrue();
        }


    }
}
