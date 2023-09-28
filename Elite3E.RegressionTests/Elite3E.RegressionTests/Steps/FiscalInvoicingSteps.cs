using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using OpenQA.Selenium;
using System.Linq;
using Elite3E.Infrastructure.Configuration;
using Elite3E.PageObjects.Interaction.ProcessInteraction.FiscalInvoice;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using System.Collections.Generic;
using System.Threading;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.ProcessInteraction.OperatingUnit;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class FiscalInvoicingSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public FiscalInvoicingSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I open the Fiscal Invoice Setup process")]
        public void WhenIOpenTheFiscalInvoiceSetupProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FiscalInvoiceSetup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I enter a negative value for next fiscal invoice number")]
        public void WhenIEnterANegativeValueForNextFiscalInvoiceNumber()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.NextFiscalInvoiceNumber, "-5"));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.FiscalInvoiceSuffix));
        }

        [When(@"I click update")]
        public void WhenIClickUpdate()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(Fiscal_InvoicingLocators.Update), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"a error message ""(.*)"" is displayed")]
        public void ThenAErrorMessageIsDisplayed(string message)
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var messages = _actor.AsksFor(ProcessError.Messages());
            messages.Count.Should().Be(1);
            messages[0].Should().BeEquivalentTo(message);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"click on submit")]
        public void ThenClickOnSubmit()
        {
            _actor.WaitsUntil(Existence.Of(CommonLocator.Submit), IsEqualTo.True());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"Submit the form")]
        public void ThenSubmitTheForm()
        {
            _actor.WaitsUntil(Existence.Of(CommonLocator.Submit), IsEqualTo.True());
            // _actor.WaitsUntil(TaxRatesLocator.Submit is true)
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            //_actor.WaitsUntil(Existence.Of(GLRecieptsLocators.Verifyratesaved), IsEqualTo.True());
            //_actor.WaitsUntil(Existence.Of(GLRecieptsLocators.Verifyratesaved), IsEqualTo.True());
        }

        [Then(@"Cancel the process")]
        public void ThenCancelTheProcss()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I open the  Unit Setup process and search for ""(.*)""")]
        public void WhenIOpenTheUnitSetupProcessAndSearchFor(string searchText)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.UnitSetup));
            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"clone the unit")]
        public void WhenCloneTheUnit(Table table)
        {

            var unitEntity = table.CreateInstance<UnitEntity>();
            _actor.AttemptsTo(FlyOutButtonMenu.Click(StepConstants.Add, StepConstants.Clone));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.UnitCode, table.Rows[0][ColumnNames.Code]));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.UnitDescription, table.Rows[0][ColumnNames.Description]));

            _featureContext[StepConstants.UnitNumberContext] = unitEntity;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I fill the relevant fields - active fiscal invoicing setup record already exists")]
        public void WhenIFillTheRelevantFields_ActiveFiscalInvoicingSetupRecordAlreadyExists(Table table)
        {
            var fiscalEntity = table.CreateInstance<FiscalInvoiceCreateEntity>();
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.Unit, fiscalEntity.Unit));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.BillGLType, fiscalEntity.BillGlType));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.SuspenseGLType, fiscalEntity.SuspenseGlType));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.FiscalInvoicePrefix, "121"));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.NextFiscalInvoiceNumber, "510"));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.FiscalInvoiceSuffix, "131"));
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.TexTValue));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I fill the relevant fields - an inactive fiscal invoicing setup record already exists")]
        public void WhenIFillTheRelevantFields_AnInactiveFiscalInvoicingSetupRecordAlreadyExists(Table table)
        {
            var fiscalEntity = table.CreateInstance<FiscalInvoiceCreateEntity>();
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.Unit, fiscalEntity.Unit));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.BillGLType, fiscalEntity.BillGlType));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.SuspenseGLType, fiscalEntity.SuspenseGlType));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.FiscalInvoicePrefix, "auto_5"));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.NextFiscalInvoiceNumber, "25"));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.FiscalInvoiceSuffix, "555"));
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.TexTValue));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I fill the relevant fields -  a fiscal invoicing setup record does not already exist")]
        public void WhenIFillTheRelevantFields_AFiscalInvoicingSetupRecordDoesNotAlreadyExist()
        {
            _actor.AttemptsTo(SendKeys.To(CommonLocator.Unit, "Dentons Egypt LLC "));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.BillGLType, "Local Adj - Dentons Australia Limited"));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.SuspenseGLType, "Local Adj - Dentons Australia Limited"));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.FiscalInvoicePrefix, "auto_"));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.NextFiscalInvoiceNumber, "610"));
            _actor.AttemptsTo(SendKeys.To(Fiscal_InvoicingLocators.FiscalInvoiceSuffix, "1001"));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search using advanced find in Fiscal Invoice setup process")]
        public void WhenISearchUsingAdvancedFindInFiscalInvoiceSetupProcess(Table table)
        {

            _actor.AttemptsTo(SearchProcess.ByName(Process.FiscalInvoiceSetup, false));
            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            var FiscalActive = _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteriaCol));
            _featureContext[StepConstants.Active] = FiscalActive;
        }

        [When(@"I disable the active checkbox")]
        public void WhenIDisableTheActiveCheckbox()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(Fiscal_InvoicingLocators.Applyfilter), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.Applyfilter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.ShowButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(Fiscal_InvoicingLocators.ShowButton), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(Fiscal_InvoicingLocators.Activerecordtext), IsEqualTo.True());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.Activerecordtext));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            _actor.WaitsUntil(Appearance.Of(Fiscal_InvoicingLocators.Fiscalsetupheader), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.GetActive));
        }

        [When(@"I select the active checkbox")]
        public void WhenISelectTheActiveCheckbox()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(Fiscal_InvoicingLocators.Fiscalsetupheader), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(FiscalInvoiceEntity.Select());
        }

        [When(@"I select the inactive record")]
        public void WhenISelectTheInactiveRecord()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.Applyfilter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.ShowButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.Activerecordtext));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
        }

        [When(@"I search for the active unit ""(.*)""")]
        public void WhenISearchForTheActiveUnit(string searchText)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FiscalInvoiceSetup));

            _actor.AttemptsTo(QuickFind.Search(searchText));
        }

        [StepDefinition(@"I set create fiscal invoice")]
        public void WhenISetCreateFiscalInvoice()
        {
            if (!_actor.AsksFor(SelectedState.Of(ProformaEditLocator.GetCreateFiscalInvoice)))
            {
                _actor.AttemptsTo(JScript.ClickOn(ProformaEditLocator.SetCreateFiscalInvoice));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"the tax invoice number is generated")]
        public void ThenTheTaxInvoiceNumberIsGenerated()
        {
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var invoiceNumber = message.Split(":")[1].Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0];
            _featureContext[StepConstants.InvoiceNumberContext] = invoiceNumber;
            message.Should().Contain("Auto-generated Tax Invoice Number:" + ApplicationConfigurationBuilder.Instance.FiscalInvoicePrefix);
        }

        [StepDefinition(@"the invoice number is generated")]
        public void ThenTheInvoiceNumberIsGenerated()
        {
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            message.Should().NotContainEquivalentOf("error");
            var invoiceNumber = message.Split(":")[1].Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0];

            _featureContext[StepConstants.InvoiceNumberContext] = invoiceNumber;

            Console.WriteLine("Invoice Number : " + invoiceNumber);
        }

        [StepDefinition(@"I view the invoices")]
        public void WhenIViewTheInvoices()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.Invoices, false));

            if (_actor.DoesElementExist(InvoicesLocator.InvoiceMasterProcess))
            {
                _actor.AttemptsTo(Click.On(InvoicesLocator.InvoiceMasterProcess));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            _actor.AttemptsTo(QuickFind.Search(invoiceNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Invoices));
        }

        [Then(@"I verify the invoice type is correct")]
        public void ThenIVerifyTheInvoiceTypeIsCorrect()
        {
            var actualInvoiceType = _actor.GetElementText(InvoicesLocator.InvoiceType);
            var expectedInvoiceType = _featureContext[StepConstants.InvoiceType].ToString();
            actualInvoiceType.Should().BeEquivalentTo(expectedInvoiceType);
        }

        [Then(@"I verify the sections in the invoices")]
        public void ThenIVerifyTheSectionsInTheInvoices()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Invoices));
            _actor.AsksFor(Field.IsAvailable(InvoicesLocator.InvoiceDetail)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(InvoicesLocator.PayerDetail)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(InvoicesLocator.EBillingHistory)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(InvoicesLocator.ProformaList)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(InvoicesLocator.Time)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(InvoicesLocator.Disbursement)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(InvoicesLocator.Charge)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(InvoicesLocator.CreditNoteTaxArticle)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"submit the invoice with the full credit note set")]
        public void WhenSubmitTheInvoiceWithTheFullCreditNoteSet()
        {
            if (!_actor.AsksFor(SelectedState.Of(InvoicesLocator.GetFullCreditNote)))
            {
                _actor.AttemptsTo(Click.On(InvoicesLocator.SetFullCreditNote));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"submit the invoice with the full credit note set with '(.*)'")]
        public void WhenSubmitTheInvoiceWithTheFullCreditNote(string reasonType)
        {
            if (!_actor.AsksFor(SelectedState.Of(InvoicesLocator.GetFullCreditNote)))
            {
                _actor.AttemptsTo(Click.On(InvoicesLocator.SetFullCreditNote));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(reasonType))
            {
                _actor.AttemptsTo(SendKeys.To(InvoicesLocator.InputReasonType, reasonType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Then(@"the credit note is generated")]
        public void ThenTheCreditNoteIsGenerated()
        {
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var creditNoteNumber = message.Split(" ").Last();

            _featureContext[StepConstants.CreditNoteNumberContext] = creditNoteNumber;

            creditNoteNumber.Should().NotBeNullOrEmpty();
        }

        [Then(@"the full credit note is disabled")]
        public void ThenTheFullCreditNoteIsDisabled()
        {
            _actor.AsksFor(Field.IsAvailable((ProformaEditLocator.GetFullCreditNote))).Should().BeFalse();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"print the invoice")]
        [Given(@"I print the invoice")]
        public void GivenIPrintTheInvoice()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!_actor.AsksFor(SelectedState.Of(InvoicesLocator.GetPrintToScreen)))
            {
                _actor.AttemptsTo(Click.On(InvoicesLocator.SetPrintToScreen));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!_actor.AsksFor(SelectedState.Of(InvoicesLocator.GetSaveToLocal)))
            {
                _actor.AttemptsTo(Click.On(InvoicesLocator.SetSaveToLocal));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Print));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Homepage), IsEqualTo.True(), 60);

        }

        [When(@"I search and select all the invoices")]
        public void WhenISearchAndSelectAllTheInvoices()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.Invoices, false));
            if (_actor.DoesElementExist(InvoicesLocator.InvoiceMasterProcess))
            {
                _actor.AttemptsTo(Click.On(InvoicesLocator.InvoiceMasterProcess));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();

            _actor.AttemptsTo(QuickFind.Search(invoiceNumber));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectAllTitleButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the credit note is available")]
        public void ThenTheCreditNoteIsAvailable()
        {
            decimal.Parse(_actor.AsksFor(Text.Of(InvoicesLocator.GridTotalAmount))).Should().Be(0);
        }

        [StepDefinition(@"I add a new receipt")]
        public void WhenIAddANewReceipt(Table table)
        {
            var receiptEntity = table.CreateInstance<ReceiptEntity>();

            _actor.AttemptsTo(SearchProcess.ByName(Process.ReceiptsApplyReversePayments));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(receiptEntity.OperatingUnit))
            {
                _actor.AttemptsTo(WorkList.View(WorkListViewEntity.Folder));
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ReceiptLocator.OperatingUnit, receiptEntity.OperatingUnit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(WorkList.View(WorkListViewEntity.Worklist));
            }

            EnterReceiptDetails(receiptEntity);

            _featureContext[StepConstants.ReceiptTypeContext] = (string.IsNullOrEmpty(receiptEntity.ReceiptType)) ? _featureContext[StepConstants.ReceiptTypeContext] : receiptEntity.ReceiptType;
            _featureContext[StepConstants.ReceiptDateContext] = receiptEntity.ReceiptDate;
            _featureContext[StepConstants.ReceiptDocumentContext] = receiptEntity.DocumentNumber;
            _featureContext[StepConstants.DocumentNumber] = receiptEntity.DocumentNumber;
            _featureContext[StepConstants.PayerContext] = receiptEntity.Payer;

            Console.WriteLine("Receipt Number: " + receiptEntity.DocumentNumber);
        }

        [StepDefinition(@"I update the receipt")]
        public void GivenIUpdateTheReceipt(Table table)
        {
            var receiptEntity = table.CreateInstance<ReceiptEntity>();
            receiptEntity.ReceiptType = (string.IsNullOrEmpty(receiptEntity.ReceiptType)) ? _featureContext[StepConstants.ReceiptTypeContext].ToString() : receiptEntity.ReceiptType;

            EnterReceiptDetails(receiptEntity);
            _actor.AttemptsTo(Click.On(ReceiptLocator.ReceiptDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        private void EnterReceiptDetails(ReceiptEntity receiptEntity)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(receiptEntity.ReceiptDate))
            {
                _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.ReceiptDate, receiptEntity.ReceiptDate));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(receiptEntity.ChequeDate))
            {
                _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.ChequeDate, receiptEntity.ChequeDate));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            receiptEntity.ReceiptType = (string.IsNullOrEmpty(receiptEntity.ReceiptType)) ? _featureContext[StepConstants.ReceiptTypeContext].ToString() : receiptEntity.ReceiptType;
            if (!string.IsNullOrEmpty(receiptEntity.ReceiptType))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Receipt Type", receiptEntity.ReceiptType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(receiptEntity.DepositNumber))
            {
                _actor.AttemptsTo(SendKeys.To(ReceiptLocator.DepositNumber, receiptEntity.DepositNumber + Keys.Tab));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(receiptEntity.DocumentNumber))
            {
                _actor.AttemptsTo(SendKeys.To(ReceiptLocator.DocumentNumber, receiptEntity.DocumentNumber + Keys.Tab));
                // _actor.AttemptsTo(SendKeys.To(ReceiptLocator.Payer, receiptEntity.Payer + Keys.Tab));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _featureContext[StepConstants.ReceiptDocumentContext] = receiptEntity.DocumentNumber;
            }

            if (!string.IsNullOrEmpty(receiptEntity.Narrative))
            {
                var driver = _actor.Using<BrowseTheWeb>().WebDriver;
                driver.FindElement(ReceiptLocator.Narrative.Query).SendKeys(receiptEntity.Narrative);
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(receiptEntity.ReceiptAmount))
            {
                var driver = _actor.Using<BrowseTheWeb>().WebDriver;
                driver.FindElement(ReceiptLocator.ReceiptAmount.Query).SendKeys(receiptEntity.ReceiptAmount);
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }

        [When(@"add the invoice on the receipt")]
        public void WhenAddTheInvoiceOnTheReceipt()
        {
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            _actor.AttemptsTo(SearchAndAddToChildProcess.With(StepConstants.Invoices, invoiceNumber));
        }

        [StepDefinition(@"the payer is auto populated")]
        public void ThenThePayerIsAutoPopulated()
        {
            _actor.AsksFor(ValueAttribute.Of(ReceiptLocator.Payer)).Trim().Should().NotBeNullOrEmpty();
        }

        [StepDefinition(@"I receipt the total amount")]
        public void WhenIReceiptTheTotalAmount()
        {
            _actor.AttemptsTo(Click.On(ReceiptLocator.ReceiptAmount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var amount = _actor.AsksFor(Text.Of(ReceiptLocator.TotalAppliedDiv)).Trim();

            _actor.AttemptsTo(SendKeys.To(ReceiptLocator.ReceiptAmount, amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ReceiptLocator.TotalAppliedButton));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I receipt an amount less than invoice amount")]
        public void GivenIReceiptAnAmountLessThanInvoiceAmount()
        {
            var amount = decimal.Parse(_actor.AsksFor(Text.Of(ReceiptLocator.TotalAppliedDiv)).Trim());
            amount = amount - 200;
            _actor.AttemptsTo(SendKeys.To(ReceiptLocator.InvoiceReceiptAmount, amount.ToString()));
            _actor.AttemptsTo(SendKeys.To(ReceiptLocator.ReceiptAmount, amount.ToString()));
        }


        [When(@"I verify the write off amount in the receipt '([^']*)'")]
        public void WhenIVerifyTheWriteOffAmountInTheReceipt(string writeOffAmount)
        {
            _actor.AttemptsTo(Click.On(ReceiptLocator.TotalAppliedButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var amount = _actor.AsksFor(Text.Of(ReceiptLocator.TotalAppliedDiv)).Replace(",", "").Trim();
            amount.Should().Contain(writeOffAmount);
            _actor.AttemptsTo(Click.On(ReceiptLocator.AlowShortPayCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ReceiptLocator.ReceiptAmount, amount));
        }

        [When(@"I add the receipt amount '([^']*)' and total it")]
        public void WhenIAddTheReceiptAmountAndTotalIt(string amount)
        {
            _actor.AttemptsTo(SendKeys.To(ReceiptLocator.InvoiceReceiptAmount, amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ReceiptLocator.TotalAppliedButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ReceiptLocator.ReceiptAmount, amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search by a billing office")]
        public void WhenISearchByABillingOffice(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.InvoiceManager));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(InvoicesLocator.BillingOfficeInput, table.Rows[Index.Start][ColumnNames.Office]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.Button("Search")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify that the results appear")]
        public void ThenIVerifyThatTheResultsAppear()
        {
            _actor.DoesElementExist(InvoicesLocator.InvoiceManagerResultsCheckbox).Should().BeTrue();
        }



        [Then(@"I can submit the receipt")]
        [When(@"submit the receipt")]
        [Then(@"I can submit the direct cheque")]
        [Then(@"I can submit the vendor")]
        [Then(@"I can submit the matter")]
        [StepDefinition(@"I submit the form")]
        [StepDefinition(@"I submit the workflow")]
        public void WhenSubmitTheReceipt()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify information message does not contain error")]
        public void ThenIVerifyInformationMessageDoesNotContainError()
        {
            if (_actor.DoesElementExist(CommonLocator.InformationMessage, 30))
            {
                var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
                message.Should().NotContainEquivalentOf("error");
            }
        }


        [When(@"I submit the voucher")]
        public void WhenISubmitTheVoucher()
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            driver.FindElement(VendorLocators.VoucherOrigAmount.Query).SendKeys(Keys.Control + "a");
            driver.FindElement(VendorLocators.VoucherOrigAmount.Query).SendKeys(Keys.Delete);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }

        [StepDefinition(@"update the receipt")]
        [Then(@"update the direct cheque")]
        public void GivenUpdateTheReceipt()
        {
            _actor.AttemptsTo(JScript.ClickOn(ReceiptLocator.UpdateButton));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"change the operating unit ""(.*)""")]
        public void GivenChangeTheOperatingUnit(string operatingUnit)
        {
            _actor.AttemptsTo(UpdateOperatingUnit.ChangeTheUserUnit(operatingUnit));
        }

        [Then(@"the invoice is set as paid")]
        public void ThenTheInvoiceIsSetAsPaid()
        {
            _actor.AsksFor(SelectedState.Of(InvoicesLocator.GetPaid)).Should().BeTrue();
        }

        [Then(@"the tax invoice number displayed on invoices")]
        public void ThenTheTaxInvoiceNumberDisplayedOnInvoices()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Text.Of(InvoicesLocator.TaxInvoiceNumber)).Should().NotBeNullOrEmpty();
        }

        [When(@"I add an unallocated child form")]
        public void WhenIAddAnUnallocatedChildForm(Table table)
        {
            var unallocatedEntity = table.CreateInstance<UnallocatedEntity>();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var menuItems = new List<string>() { unallocatedEntity.ChildForm };
            _actor.AttemptsTo(AddChildProcess.ByName(menuItems));

            unallocatedEntity.Matter = (string.IsNullOrEmpty(unallocatedEntity.Matter)) ? _featureContext[StepConstants.MatterNumberContext].ToString() : unallocatedEntity.Matter;
            EnterUnallocatedDetails(unallocatedEntity);
        }

        [When(@"I add an unallocated child form for submatter 1")]
        public void WhenIAddAnUnallocatedChildFormForSubmatter1(Table table)
        {
            var unallocatedEntity = table.CreateInstance<UnallocatedEntity>();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var menuItems = new List<string>() { unallocatedEntity.ChildForm };
            _actor.AttemptsTo(AddChildProcess.ByName(menuItems));

            unallocatedEntity.Matter = (string.IsNullOrEmpty(unallocatedEntity.Matter)) ? _featureContext[StepConstants.SubMatterNumberContextOne].ToString() : unallocatedEntity.Matter;
            EnterUnallocatedDetails(unallocatedEntity);
        }
        [When(@"I add an unallocated child form for submatter 2")]
        public void WhenIAddAnUnallocatedChildFormForSubmatter(Table table)
        {
            var unallocatedEntity = table.CreateInstance<UnallocatedEntity>();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var menuItems = new List<string>() { unallocatedEntity.ChildForm };
            _actor.AttemptsTo(AddChildProcess.ByName(menuItems));

            unallocatedEntity.Matter = (string.IsNullOrEmpty(unallocatedEntity.Matter)) ? _featureContext[StepConstants.SubMatterNumberContextTwo].ToString() : unallocatedEntity.Matter;
            EnterUnallocatedDetails(unallocatedEntity);
        }

        private void EnterUnallocatedDetails(UnallocatedEntity unallocatedEntity)
        {
            var matterNumber = unallocatedEntity.Matter;

            if (!string.IsNullOrEmpty(unallocatedEntity.UnallocatedType))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ReceiptLocator.UnallocatedType, unallocatedEntity.UnallocatedType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(unallocatedEntity.Matter))
            {
                _actor.AttemptsTo(SendKeys.To(ReceiptLocator.Matter, matterNumber));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(unallocatedEntity.ReceiptAmount))
            {
                _actor.AttemptsTo(SendKeys.To(ReceiptLocator.UnallocatedReceiptAmount, unallocatedEntity.ReceiptAmount));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(Click.On(ProformaEditLocator.CloseChildFormButton));
        }

        [When(@"I try to create a fiscal invoice from fiscal invoice create")]
        public void WhenITryToCreateAFiscalInvoiceFromFiscalInvoiceCreate(Table table)
        {
            var fiscalInvoiceCreateEntity = table.CreateInstance<FiscalInvoiceCreateEntity>();

            _actor.AttemptsTo(SearchProcess.ByName(Process.FiscalInvoiceCreate, false));

            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();

            _actor.AttemptsTo(SendKeys.To(FiscalInvoiceCreateLocator.ProformaInvoice, invoiceNumber));

            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.TaxDate, fiscalInvoiceCreateEntity.TaxDate));
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.GLDate, fiscalInvoiceCreateEntity.GlDate));
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.CurrencyDate, fiscalInvoiceCreateEntity.CurrencyDate));
        }

        [Then(@"a doubtfull write off message is displayed")]
        public void ThenADoubtfullWriteOffMessageIsDisplayed()
        {
            var messages = _actor.AsksFor(ProcessError.Messages());
            messages.Count.Should().Be(1);
            messages[0].Should().StartWith(StepConstants.DoubtfulErrorMessageStart);
            messages[0].Should().EndWith(StepConstants.DoubtfulErrorMessageEnd);

        }

        [When(@"select the invoice details")]
        public void WhenSelectTheInvoiceDetails()
        {
            _actor.AttemptsTo(Click.On(InvoicesLocator.InvoiceDetailsTab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ExpandButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"soft disbursement and taxes are part of the Invoice")]
        public void ThenSoftDisbursementAndTaxesArePartOfTheInvoice()
        {
            decimal.Parse(_actor.AsksFor(Text.Of(InvoicesLocator.InvoiceDetailsSoftDisbursement)).Trim()).Should().BeGreaterThan(1);
            decimal.Parse(_actor.AsksFor(Text.Of(InvoicesLocator.InvoiceDetailsTaxes)).Trim()).Should().BeGreaterThan(1);

            //  _actor.AttemptsTo(Click.On(InvoicesLocator.CloseChildFormButton));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I view the gl postings")]
        [StepDefinition(@"view the gl postings")]
        public void WhenIViewTheGlPostings()
        {
            _actor.AttemptsTo(ScrollToElement.At(InvoicesLocator.GlPostingsButton));
            _actor.AttemptsTo(Click.On(InvoicesLocator.GlPostingsButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"gl type is suspense gl type set on the fiscal invoice setup")]
        [Then(@"I close the gl postings")]
        public void ThenGlTypeIsSuspenseGlTypeSetOnTheFiscalInvoiceSetup()
        {
            // Check where to find the Suspense GL Account?
            _actor.AttemptsTo(Click.On(InvoicesLocator.CloseGlPostingsButton));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I create a fiscal invoice from fiscal invoice create")]
        public void GivenICreateAFiscalInvoiceFromFiscalInvoiceCreate(Table table)
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            var fiscalInvoiceCreateEntity = table.CreateInstance<FiscalInvoiceCreateEntity>();

            _actor.AttemptsTo(SearchProcess.ByName(Process.FiscalInvoiceCreate, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(FiscalInvoiceCreateLocator.TaxDate, fiscalInvoiceCreateEntity.TaxDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(FiscalInvoiceCreateLocator.GlDate, fiscalInvoiceCreateEntity.GlDate));
            _actor.AttemptsTo(SendKeys.To(FiscalInvoiceCreateLocator.CurrencyDate, fiscalInvoiceCreateEntity.CurrencyDate));

            //Searching for invoice three times due to timing issues
            for (int count = 1; count < 4; count++)
            {
                Console.WriteLine("Attempt: " + count);

                _actor.AttemptsTo(Click.On(FiscalInvoiceCreateLocator.ProformaInvoiceSearch));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                driver.FindElement(FiscalInvoiceCreateLocator.ProformaInvoiceSearchInput.Query).Clear();
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(FiscalInvoiceCreateLocator.ProformaInvoiceSearchInput, invoiceNumber));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(FiscalInvoiceCreateLocator.Search));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                try
                {
                    _actor.AttemptsTo(Click.On(FiscalInvoiceCreateLocator.SelectSearchResult));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.AttemptsTo(Click.On(FiscalInvoiceCreateLocator.Select));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    break;
                }
                catch (Exception ex)
                {
                    Console.Write("Error: " + ex.Message);

                    _actor.AttemptsTo(Click.On(CommonLocator.CloseButton));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    Thread.Sleep(20000);
                }
            }
        }


        [Then(@"gl type is bill gl type set on the fiscal invoice setup")]
        public void ThenGlTypeIsBillGlTypeSetOnTheFiscalInvoiceSetup()
        {
            // Check with Farhad on where to find the Suspense GL Account?
            _actor.AttemptsTo(Click.On(InvoicesLocator.CloseGlPostingsButton));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the tax invoice number starts with the prefix set on the fiscal invoice setup")]
        public void ThenTheTaxInvoiceNumberStartsWithThePrefixSetOnTheFiscalInvoiceSetup()
        {
            var taxInvoiceNumber = _actor.AsksFor(Text.Of(InvoicesLocator.TaxInvoiceNumber)).Trim();
            taxInvoiceNumber.Should().StartWith(ApplicationConfigurationBuilder.Instance.FiscalInvoicePrefix);
        }


        [When(@"I search for the saved unallocated type")]
        public void WhenISearchForTheSavedUnallocatedType()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.UnallocatedType));

            var unallocated = (UnallocatedTypeEntity)_featureContext[StepConstants.UnallocatedTypeContext];

            var searchText = unallocated.Description;

            _actor.AttemptsTo(QuickFind.Search(searchText));
        }


        [When(@"I search for the saved receipt type")]
        public void WhenISearchForTheSavedReceiptType()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ReceiptType));

            var receiptType = (ReceiptTypeEntity)_featureContext[StepConstants.ReceiptTypeContext];

            var searchText = receiptType.Description;

            _actor.AttemptsTo(QuickFind.Search(searchText));
        }

        [Then(@"I verify the sections in fiscal invoice setup")]
        public void ThenIVerifyTheSectionsInFiscalInvoiceSetup()
        {
            _actor.DoesElementExist(CommonLocator.Unit).Should().Be(true);
            _actor.DoesElementExist(Fiscal_InvoicingLocators.BillGLType).Should().Be(true);
            _actor.DoesElementExist(Fiscal_InvoicingLocators.SuspenseGLType).Should().Be(true);
            _actor.DoesElementExist(Fiscal_InvoicingLocators.NextFiscalInvoiceNumber).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the prefix of the invoice matches the invoice override")]
        public void WhenIVerifyThePrefixOfTheInvoiceMatchesTheInvoiceOverride()
        {
            _featureContext[StepConstants.InvoiceNumberContext].ToString().Should().Contain(_featureContext[StepConstants.InvoicePrefix].ToString());
        }
        [Then(@"I verify the narratives populated in Invoices")]
        public void WhenIVerifyTheNarrativesPopulatedInInvoices()
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            var invoiceNarrative = _featureContext[StepConstants.InvoiceNarrative].ToString().Replace("@MatterNumber@", matter);
            _actor.AttemptsTo(ScrollToElement.At(Fiscal_InvoicingLocators.NarrativeText));
            _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.NarrativeText)).Should().Contain(invoiceNarrative);
        }

        [Then(@"I verify the government system uplaod date child form details")]
        public void WhenIVerifyTheGovernmentSystemUplaodDateChildFormDetails()
        {
            var date = DateTime.Now.ToShortDateString();
            //  var governmentSystemTemplate = _featureContext[StepConstants.GovernmentSystemTemplate].ToString();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.GovernmentSystemUploadDate));
            // _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.GovtUploadTemplate)).Should().Contain(governmentSystemTemplate);
            _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.GovtUploadRunDate)).Should().Contain(date);
        }

        [StepDefinition(@"I generate the government system upload for the invoice")]
        public void WhenIGenerateTheGovernmentSystemUploadForTheInvoice()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.GovernmentSystemUpload, false));
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            _actor.AttemptsTo(QuickFind.Search(invoiceNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Generate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"I verify the presentation currency and invoice amount")]
        public void WhenIVerifyThePresentationCurrencyAndInvoiceAmount()
        {
            var invoiceAmount = _featureContext[StepConstants.AmountNumberContext].ToString();
            var presCurrency = _featureContext[StepConstants.PresCurrency].ToString();

            var date = DateTime.Now.ToShortDateString();

            _actor.AttemptsTo(ScrollToElement.At(Fiscal_InvoicingLocators.ExchangeRate));
            if (_actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.Currency)).Contains(presCurrency))
            {
                var presExchangeRate = _featureContext[StepConstants.PresExchangeRate].ToString();
                _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.ExchangeRate)).Should().Contain(presExchangeRate);
                _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.PresInvoiceAmount)).Replace(",", "").Should().Contain(invoiceAmount);
                _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.PresDate)).Should().Contain(date);
                _actor.AsksFor(ValueAttribute.Of(Fiscal_InvoicingLocators.PresCurrency)).Should().Contain(presCurrency);
            }
            else
            {
                _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.PresInvoiceAmount)).Replace(",", "").Should().NotContain(invoiceAmount);
                _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.PresDate)).Should().Contain(date);
                _actor.AsksFor(ValueAttribute.Of(Fiscal_InvoicingLocators.PresCurrency)).Should().Contain(presCurrency);
                _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.ExchangeRate)).Should().NotBeNullOrEmpty();
            }
        }



        [When(@"I verify the presentation currency and invoice amount in receipts")]
        public void WhenIVerifyThePresentationCurrencyAndInvoiceAmountInReceipts()
        {
            var invoiceAmount = _featureContext[StepConstants.AmountNumberContext].ToString();
            var presCurrency = _featureContext[StepConstants.PresCurrency].ToString();
            var presExchangeRate = _featureContext[StepConstants.PresExchangeRate].ToString();

            if (!(string.IsNullOrEmpty(presExchangeRate)))
            {
                _actor.AsksFor(Text.Of(Fiscal_InvoicingLocators.ExchangeRate)).Should().Contain(presExchangeRate);
                _actor.AsksFor(Text.Of(ReceiptLocator.InvoiceReceiptAmount)).Replace(",", "").Should().Contain(invoiceAmount);

            }
            else
            {
                _actor.AsksFor(Text.Of(ReceiptLocator.InvoiceReceiptAmount)).Replace(",", "").Should().NotContain(invoiceAmount);
            }
            _actor.AsksFor(Text.Of(ReceiptLocator.PresCurrency)).Should().Contain(presCurrency);
        }

        
    }
}
