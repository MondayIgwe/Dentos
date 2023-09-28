using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChequeMaintenance;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountAdjustment;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.PaymentSelectionGeneration;
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
    public class PaymentSelectionGenerationSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public PaymentSelectionGenerationSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Then(@"I add a new payment selection generation record")]
        public void ThenIAddANewPaymentSelectionGenerationRecord()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I complete mandatory fields")]
        public void ThenICompleteMandatoryFields(Table table)
        {
            var paymentEntity = table.CreateInstance<PaymentSelectionGenerationEntity>();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PaymentSelectionParameters));
            _actor.AttemptsTo(SendKeys.To(PaymentSelectionGenerationLocators.Description, paymentEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.Description] = paymentEntity.Description;

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Bank Account", paymentEntity.BankAccount));
            _actor.AttemptsTo(DateControl.SelectDate("Payment Date", paymentEntity.PaymentDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add a new selection criteria")]
        public void WhenIAddANewSelectionCriteria(Table table)
        {
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.SelectionCriteria, GlobalConstants.Form));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.SelectionCriteria, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(DateControl.SelectDate("Payment Date", table.Rows[0][ColumnNames.PaymentDate]));
        }

        [When(@"I search for the voucher number")]
        public void WhenISearchForTheVoucherNumber()
        {
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Voucher", invoiceNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search for a payee")]
        public void WhenISearchForAPayee()
        {
            var payee = _featureContext[StepConstants.PayeeNameContext].ToString();
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Payee", payee));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I test the selection")]
        public void WhenITestTheSelection()
        {
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.TestSelectionButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I verify the test result")]
        public void WhenIVerifyTheTestResult()
        {
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.TestSelectionResults));
            var text = _actor.GetDriver().FindElement(PaymentSelectionGenerationLocators.TestSelectionResults.Query).GetAttribute("value");    
            text.Should().Contain("1");
        }

        [StepDefinition(@"I generate it")]
        public void WhenIGenerateIt()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Generate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I set the status to ""([^""]*)""")]
        public void WhenISetTheStatusTo(string approved)
        {
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.ProposedPayments, GlobalConstants.Form));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(PaymentSelectionGenerationLocators.StatusInput,approved));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I navigate to the cheque maintenance process")]
        public void GivenINavigateToTheChequeMaintenanceProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ChequeMaintenance, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I navigate to the payment selection generation process")]
        public void GivenINavigateToThePaymentSelectionGenerationProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PaymentSelectiongeneration));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I verify that I'm on the payment selection edit page")]
        public void WhenIVerifyThatImOnThePaymentSelectionEditPage()
        {
            _actor.DoesElementExist(PaymentSelectionGenerationLocators.PaymentSelectionEditHeader).Should().BeTrue();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I verify that the proposed child form is displayed")]
        public void WhenIVerifyThatTheProposedChildFormIsDisplayed()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EditPaymentSelection));
            _actor.DoesElementExist(PaymentSelectionGenerationLocators.ProposedPaymentChildHeader).Should().BeTrue();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I allocate the payment")]
        public void WhenIAllocateThePayment()
        {
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.AllocateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I process the payment")]
        public void WhenIProcessThePayment()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.ProcessPayments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I tick the pay electronically checkbox")]
        public void ThenITickThePayElectronicallyCheckbox()
        {
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.PayElectronicallyCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I click process payment")]
        public void WhenIClickProcessPayment()
        {
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.ProcessPaymentsButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I process payment")]
        public void WhenIProcessPayment()
        {
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.ProcessPaymentsPrintButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I verify the page is process payments")]
        public void WhenIVerifyThePageIsProcessPayments()
        {
            _actor.DoesElementExist(PaymentSelectionGenerationLocators.ProcesPaymentsHeader).Should().BeTrue();
            _featureContext[StepConstants.PaymentSelectionIndex] = _actor.GetElementText(PaymentSelectionGenerationLocators.PaymentSelectionIndexDiv);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I update the cheque printer and template")]
        public void WhenIUpdateTheChequePrinterAndTemplate(Table table)
        {
            var paymentEntity = table.CreateInstance<PaymentSelectionGenerationEntity>();
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Cheque Template", paymentEntity.ChequeTemplate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (_actor.DoesElementExist(PaymentSelectionGenerationLocators.ChequePrinter))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Cheque Printer", paymentEntity.ChequePrinter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [StepDefinition(@"I navigate to the payment preview with voucher information report")]
        public void GivenINavigateToThePaymentPreviewWithVoucherInformationReport()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PaymentPrevieWithVoucherInformation, false));
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.PaymentPreviewOption));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I search by the payment selection index")]
        public void ThenISearchByThePaymentSelectionIndex(Table table)
        {
            var selectionIndex = _featureContext[StepConstants.PaymentSelectionIndex].ToString();
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.SearchByFieldColumnInput));
            _actor.AttemptsTo(SendKeys.To(PaymentSelectionGenerationLocators.SearchByFieldColumnInput, table.Rows[0][ColumnNames.SearchType] + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.OperatorLocatorDropdown));
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.SearchByEqualOperator));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(PaymentSelectionGenerationLocators.SearchbyValueInput, selectionIndex + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I verify that the report information is correct")]
        public void WhenIVerifyThatTheReportInformationIsCorrect( )
        {
            var payee = _featureContext[StepConstants.PayeeNameContext];
            var selectionIndex = _featureContext[StepConstants.PaymentSelectionIndex];
            var reportData = _actor.GetElementTextList(PaymentSelectionGenerationLocators.ReportDataDiv);

            reportData.Exists(x => x.Contains(payee.ToString().Trim())).Should().BeTrue();
            reportData.Exists(x => x.Contains(selectionIndex.ToString().Trim())).Should().BeTrue();

        }

        [StepDefinition(@"I select an existing payment selection")]
        public void GivenISelectAnExistingPaymentSelection()
        {
            var paymentSelection = _featureContext[StepConstants.Description].ToString();
            _actor.AttemptsTo(QuickFind.Search(paymentSelection));
        }

        [StepDefinition(@"I delete the payment selection")]
        public void ThenIDeleteThePaymentSelection()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ParentProcessDeleteButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I verify the delete was successful")]
        public void WhenIVerifyTheDeleteWasSuccessful()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PaymentSelectiongeneration));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var paymentSelection = _featureContext[StepConstants.Description].ToString();
            _actor.AttemptsTo(QuickFind.Search(paymentSelection));
            _actor.DoesElementExist(CommonLocator.NoSearchRecords).Should().BeTrue();
        }

        [Then(@"I verify that the proposed payment childform is displayed")]
        public void ThenIVerifyThatTheProposedPaymentChildformIsDisplayed()
        {
            _actor.DoesElementExist(PaymentSelectionGenerationLocators.ProposedPaymentChildHeader).Should().BeTrue();
        }

        [When(@"I click detail listing")]
        public void WhenIClickDetailListing()
        {
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.ListingDropdown));
            _actor.AttemptsTo(Click.On(PaymentSelectionGenerationLocators.DetailListingOption));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I verify the payment preview")]
        public void WhenIVerifyThePaymentPreview()
        {
             _actor.DoesElementExist(PaymentSelectionGenerationLocators.ReportDataDiv).Should().BeTrue();
        }

    }
}
