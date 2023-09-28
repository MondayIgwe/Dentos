using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountReceiptRequest;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountTransfer;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using System.Linq;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ClientAccountReceiptRequestSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ClientAccountReceiptRequestSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I open the client account receipt request process")]
        public void WhenIOpenTheClientAccountReceiptRequestProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountReceiptRequest, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I complete all the client account receipt required fields")]
        public void GivenICompleteAllTheClientAccountReceiptRequiredFields(Table table)
        {
            string matterNo;
            try
            {
                matterNo = _featureContext[StepConstants.MatterNumberContext].ToString();
            }
            catch
            {
                matterNo = _featureContext[StepConstants.SubMatterNumberContextOne].ToString();
            }

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAccountReceiptRequest));
            _actor.AttemptsTo(DateControl.SelectDate("Transaction Date", table.Rows[0][ColumnNames.TransactionDate]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.NarrativeTextArea));
            _actor.GetDriver().FindElement(ClientAccountReceiptRequestLocators.NarrativeTextArea.Query).SendKeys(table.Rows[0][ColumnNames.Narrative]);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptDetailCard));

            //make amount random so we will be able to use it for searching later on: Bug:https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/53143
            table.Rows[0][ColumnNames.Amount] = new Random().Next(50, 999).ToString();
            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptRequestLocators.Amount, table.Rows[0][ColumnNames.Amount]));
            _featureContext[StepConstants.AmountNumberContext] = table.Rows[0][ColumnNames.Amount];
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Matter #", matterNo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            

            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptRequestLocators.ReasonCommentInput, table.Rows[0][ColumnNames.Reason]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I set the aml checks complete checkbox to true")]
        public void WhenISetTheAmlChecksCompleteCheckboxToTrue()
        {
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.AMLChecksCompleteCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"the approval checkbox is checked")]
        public void WhenTheApprovalCheckboxIsChecked()
        {
            _actor.DoesElementExist(ClientAccountReceiptRequestLocators.ApprovedCheckboxChecked).Should().BeTrue();
        }

        [Then(@"I close the client receipt process")]
        public void ThenICloseTheClientReceiptProcess()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.CLOSE));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"the client account receipt record does exist")]
        public void ThenTheClientAccountReceiptRecordDoesExist()
        {
            var amounttoSearch = _featureContext[StepConstants.AmountNumberContext].ToString();
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptFilterIcon));
            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptRequestLocators.ClientAccountReceiptFilterInput, amounttoSearch + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(ClientAccountReceiptRequestLocators.ClientAccountReceiptApprovalRecord).Should().BeTrue();
        }

        [StepDefinition(@"I open the client account receipt task")]
        public void ThenIOpenTheClientAccountReceiptTask()
        {
            var amounttoSearch = _featureContext[StepConstants.AmountNumberContext].ToString();
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptBillingTimeKeeperFilterIcon));
            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptRequestLocators.ClientAccountReceiptBillingTimeKeeperInput, amounttoSearch + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(ClientAccountReceiptRequestLocators.ClientAccountReceiptRecordOpenButton), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptRecordOpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I approve the task by clicking submit")]
        public void ThenIApproveTheTaskByClickingSubmit()
        {
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ApprovedCheckbox));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I reject the receipt task")]
        public void WhenIRejectTheReceiptTask()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.ReturnReject));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ConfirmCheckbox));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Ok));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }



        [Given(@"I close the task item")]
            public void GivenICloseTheTaskItem()
            {
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Close));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }


            [StepDefinition(@"I navigate to the client account receipt section")]
            public void ThenINavigateToTheClientAccountReceiptSection()
            {
                _actor.WaitsUntil(Appearance.Of(ClientAccountReceiptRequestLocators.ClientAccountReceiptSection), IsEqualTo.True(), 1);
                _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptSection));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        [Given(@"I open the trust receipt request entry finance approval task")]
        public void GivenIOpenTheTrustReceiptRequestEntryFinanceApprovalTask()
        {
            var matter = _featureContext[StepConstants.SubMatterNumberContextOne].ToString();
            var amount = Convert.ToDecimal(_featureContext[StepConstants.AmountNumberContext].ToString()).ToString("#.00");
            var todayDate = DateTime.Now.ToShortDateString();
            _actor.AttemptsTo(ScrollToElement.At(ClientAccountReceiptRequestLocators.TrustReceiptTransactionDateSortReceiptRequest));
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.TrustReceiptTransactionDateSortReceiptRequest));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.TrustReceiptTransactionDateSortReceiptRequest));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.TrustReceiptEntryFinanceApprovalFilterIcon));
            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptRequestLocators.TrustReceiptEntryFinanceInput, amount + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ClientAccountReceiptRequestLocators.TrustReceiptGenericOpenButton("Trust Receipt Request Entry - Finance Approval", amount, todayDate)), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.TrustReceiptGenericOpenButton("Trust Receipt Request Entry - Finance Approval ", amount, todayDate)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowHistory));
            _actor.AsksFor(ValueAttribute.Of(ClientAccountReceiptRequestLocators.MatterInput)).Should().Contain(matter);
        }


        [StepDefinition(@"the client account receipt record does not exist")]
            public void ThenTheClientAccountReceiptRecordDoesNotExist()
            {
                var amounttoSearch = _featureContext[StepConstants.AmountNumberContext].ToString();
                _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptFilterIcon));
                _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptRequestLocators.ClientAccountReceiptFilterInput, amounttoSearch + Keys.Enter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.ClientAccountReceiptApprovalRecord).Should().BeFalse();
            }

            [Given(@"I open the client account receipt approval process")]
            public void GivenIOpenTheClientAccountReceiptApprovalProcess()
            {
                _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountReceiptApproval));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            [Given(@"I open the client account receipt finance process")]
            public void GivenIOpenTheClientAccountReceiptFinanceProcess()
            {
                _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountReceiptFinance));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            [Then(@"the client account receipt approval process should exist")]
            public void ThenTheClientAccountReceiptApprovalProcessShouldExist()
            {
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAccountReceiptApproval));
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.ClientAccountReceiptApprovalForm).Should().BeTrue();
            }

            [Then(@"the client account receipt finance process should exist")]
            public void ThenTheClientAccountReceiptFinanceProcessShouldExist()
            {
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAccountReceiptFinance));
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.ClientAccountReceiptFinanceForm).Should().BeTrue();
            }

            [Then(@"the client account receipt request process should exist")]
            public void ThenTheClientAccountReceiptRequestProcessShouldExist()
            {
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAccountReceiptRequest));
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.ClientAccountReceiptDetailForm).Should().BeTrue();
            }

            [Then(@"all the client account receipt request fields should exist")]
            public void ThenAllTheClientAccountReceiptRequestFieldsShouldExist()
            {
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.TransactionDate).Should().BeTrue();
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.Amount).Should().BeTrue();
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.MatterInput).Should().BeTrue();
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.IntendedUse).Should().BeTrue();
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.ApprovedCheckbox).Should().BeTrue();
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.AMLChecksCompleteCheckbox).Should().BeTrue();
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.ClientReceiptRequiredCheckbox).Should().BeTrue();
                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.Narrative).Should().BeTrue();
            }

            [StepDefinition(@"I open the client account receipt record as a finance reviewer")]
            public void WhenIOpenTheClientAccountReceiptRecordAsAFinanceReviewer()
            {
                _actor.AttemptsTo(SearchProcess.ByName("Workflow Dashboard", false));
                _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptSection));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                var amounttoSearch = _featureContext[StepConstants.AmountNumberContext].ToString();
                _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptFinanceApprovalFilterIcon));
                _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptRequestLocators.ClientAccountReceiptFinanceApprovalInput, amounttoSearch + Keys.Enter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.DoesElementExist(ClientAccountReceiptRequestLocators.ClientReceiptFinanceApprovalOpenButton).Should().BeTrue();
                _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientReceiptFinanceApprovalOpenButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        [StepDefinition(@"I navigate to the client receipt section and search by '([^']*)'")]
        public void GivenINavigateToTheClientReceiptSectionAndSearchBy(string feeEarnerUser)
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            var amount = Convert.ToDecimal(_featureContext[StepConstants.AmountNumberContext].ToString()).ToString("#.00");
            var todayDate = DateTime.Now.ToShortDateString();
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.TrustReceiptFinanceApprovalFilterIcon));
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.CompletedDateSortReceiptRequest));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.CompletedDateSortReceiptRequest));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptRequestLocators.TrustReceiptFinanceInput, feeEarnerUser + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ClientAccountReceiptRequestLocators.TrustReceiptGenericOpenButton("Trust Receipt Finance Approval - Request Entry", amount, todayDate)), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.TrustReceiptGenericOpenButton("Trust Receipt Finance Approval - Request Entry", amount, todayDate)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.WorkflowHistory));
            _actor.AsksFor(Text.Of(ClientAccountReceiptRequestLocators.MatterInput)).Should().Contain(matter);
        }

    }
}

