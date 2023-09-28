using System;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ReceiptsAllocation;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Selenium;
using FluentAssertions;
using OpenQA.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GenearlJournal;
using FluentAssertions;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class RecieptAllocationSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public RecieptAllocationSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"add a new reciept")]
        public void WhenAddANewReciept(Table table)
        {
            var receiptEntity = table.CreateInstance<ReceiptEntity>();
            string receiptType = !string.IsNullOrEmpty(receiptEntity.ReceiptType) ? receiptEntity.ReceiptType
                : _featureContext[StepConstants.ReceiptTypeContext].ToString();

            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.ReceiptType, receiptType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.RcptADocNumber, receiptEntity.DocumentNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.Amount, receiptEntity.ReceiptAmount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var docNum = receiptEntity.DocumentNumber;
            _featureContext[StepConstants.DocumentNumber] = docNum;
        }

        [StepDefinition(@"I add a new general ledger row")]
        public void WhenIAddANewGeneralLedgerRow(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.GeneralLedger));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.GeneralLedger, ChildProcessMenuAction.Add));

            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.GlTypeInput, table.Rows[0][ColumnNames.GLType] + Keys.Enter));
            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.GLAmountInput, table.Rows[0][ColumnNames.ReceiptAmount]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add the gl account")]
        public void WhenIAddTheGlAccount(Table table)
        {
            _actor.AttemptsTo(Click.On(GeneralJournalLocators.GlAccountSearch));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, table.Rows[0][ColumnNames.GLAccount]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Record(table.Rows[0][ColumnNames.GLAccount])));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I add the payer")]
        public void WhenIAddThePayer()
        {
            var payer = _featureContext[StepConstants.ClientNumber].ToString();
            if (!string.IsNullOrEmpty(payer))
            {
                _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.Payer, payer));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"allocate the new reciept")]
        public void WhenAllocateTheNewReciept(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Unallocated));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Unallocated", ChildProcessMenuAction.Add));

            var matterNumber = _featureContext[StepConstants.MatterNumberContext];

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ReceiptsAllocationsLocators.Unallocated, table.Rows[0][ColumnNames.Unnallocated]));
            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.Matter, (string)matterNumber));
            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.AmountUnallocated, table.Rows[0][ColumnNames.ReceiptAmount]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(table.Rows[0][ColumnNames.Narrative].ToString()))
            {
                _actor.GetDriver().FindElement(ReceiptsAllocationsLocators.Narrative.Query).SendKeys(table.Rows[0][ColumnNames.Narrative]);
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(WorkList.View(WorkListViewEntity.Folder));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ReceiptsAllocationsLocators.OperatingUnit, table.Rows[0][ColumnNames.OperatingUnit]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WorkList.View(WorkListViewEntity.Worklist));
        }

        [Then(@"I can submit the reciept allocation")]
        public void ThenICanSubmitTheRecieptAllocation()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(LocatorConstants.UpdateButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I remove the invoice for the receipt")]
        public void ThenIRemoveTheInvoiceForTheReceipt()
        {
            _actor.DoesElementExist(ReceiptsAllocationsLocators.ReversalCheckbox).Should().BeFalse();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Invoices));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.Invoices, ChildProcessMenuAction.Remove));
        }

        [When(@"I search for the document number")]
        public void WhenISearchForTheDocumentNumber()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ReceiptsApplyReversePayments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var docNum = _featureContext[StepConstants.DocumentNumber].ToString();
            _actor.AttemptsTo(QuickFind.Search(docNum));

        }

        [Then(@"I verify the sections in the receipts")]
        public void ThenIVerifyTheSectionsInTheReceipts()
        {
            _actor.DoesElementExist(ReceiptsAllocationsLocators.NarrativeEditor).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Invoices));
            _actor.AsksFor(Field.IsAvailable(ReceiptsAllocationsLocators.Invoices)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ReceiptsAllocationsLocators.UnallocatedChildForm)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ReceiptsAllocationsLocators.GeneralLedger)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ReceiptsAllocationsLocators.ApplyClientAccount)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ReceiptsAllocationsLocators.BilledOnAccount)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"delete the receipt")]
        public void ThenDeleteTheReceipt()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.Remove)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I locate and search for a receipt to reverse")]
        [StepDefinition(@"I locate a submitted receipt")]
        public void GivenILocateAndSearchForAReceiptToReverse()
        {
            var documentNumber = _featureContext[StepConstants.ReceiptDocumentContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ReceiptsApplyReversePayments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(documentNumber));
        }

        [StepDefinition(@"I locate the reversed receipt")]
        public void GivenILocateTheReversedReceipt()
        {
            var documentNumber = _featureContext[StepConstants.ReceiptDocumentContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ReceiptsApplyReversePayments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(documentNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ReceiptsAllocationsLocators.ReversedReceiptDiv(documentNumber)));
            _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I can verify that the receipt is reversed")]
        public void GivenICanVerifyThatTheReceiptIsReversed()
        {
            var docNumber = _featureContext[StepConstants.DocumentNumber].ToString();
            _actor.DoesElementExist(ReceiptsAllocationsLocators.ReversedReceiptDiv(docNumber)).Should().BeTrue();
        }

        [StepDefinition(@"I perform the reversal")]
        public void GivenIPerformTheReversal(Table table)
        {
            _actor.AttemptsTo(Click.On(ReceiptsAllocationsLocators.ReversalCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(DateControl.SelectDate(StepConstants.ReversalDate, table.Rows[0][ColumnNames.ReversalDate]));
            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.ReversalReason, table.Rows[0][ColumnNames.Reason] + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.ReversalComment, table.Rows[0][ColumnNames.Comment]));

            if (!string.IsNullOrEmpty(table.Rows[Index.Start][ColumnNames.ReAllocate]) &&
                table.Rows[Index.Start][ColumnNames.ReAllocate].ToLower().Contains("true"))
            {
                _actor.AttemptsTo(Click.On(ReceiptsAllocationsLocators.ReversalAndReallocateCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
           
        }

        [StepDefinition(@"I locate the submitted receipt")]
        public void ThenILocateTheSubmittedReceipt()
        {
           string documentNumber= _featureContext[StepConstants.ReceiptDocumentContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ReceiptsApplyReversePayments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(documentNumber));
        }

        [Then(@"I verify the doubtful A/R natural field is readonly and editable")]
        public void ThenIVerifyTheDoubtfulARNaturalFieldIsReadonlyAndEditable()
        {
            //verify the field is read only
            _actor.DoesElementExist(ReceiptsAllocationsLocators.DoubtfulARGLNatural).Should().Be(false);

            _actor.AttemptsTo(Click.On(ReceiptsAllocationsLocators.WriteOffCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //enable the doubtful checkbox
            _actor.AttemptsTo(Click.On(ReceiptsAllocationsLocators.DoubtfulWriteOffCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
           
            //verify the field is editable
            _actor.DoesElementExist(ReceiptsAllocationsLocators.DoubtfulARGLNatural).Should().Be(true);

        }

        [Given(@"I update the doubtful A/R GL natural value to '([^']*)'")]
        public void GivenIUpdateTheDoubtfulARGLNaturalValueTo(string doubtfulARNatural)
        {
            _actor.AttemptsTo(SendKeys.To(ReceiptsAllocationsLocators.DoubtfulARGLNatural, doubtfulARNatural+Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I validate doubtful AR GL natural '([^']*)' is shown")]
        public void ThenIValidateDoubtfulARGLNaturalIsShown(string doubtfulARNatural)
        {
            var glMaskedValues = _actor.AsksFor(TextList.For(EntryAndModifyProcessLocators.GLMaskedValues));
            glMaskedValues.Any(item => item.Contains(doubtfulARNatural));
            _featureContext[StepConstants.GLMaskedValues] = glMaskedValues;
        }


    }


}
