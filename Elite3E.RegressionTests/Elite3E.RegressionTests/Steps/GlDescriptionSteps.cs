using System;
using System.Linq;
using System.Threading;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Enums;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GlDetails;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class GlDescriptionSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public GlDescriptionSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I update unit override '(.*)'")]
        public void WhenIUpdateUnitOverride(string description)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(GlDetailsLocators.GlDescriptionUnitOverride, description));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [When(@"I update both language '(.*)' and unit override '(.*)'")]
        public void WhenIUpdateLanguageAndUnitOverride(string languageDescription, string unitDescription)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(GlDetailsLocators.GlDescriptionLanguage, languageDescription));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
          //  _actor.AttemptsTo(SendKeys.To(GlDetailsLocators.GlDescriptionUnitOverride, unitDescription));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [When(@"I update language '(.*)'")]
        public void WhenIUpdateLanguage(string description)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(GlDetailsLocators.GlDescriptionLanguage, description));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [StepDefinition(@"I advanced find and select")]
        public void GivenIAdvancedFindAndSelect(Table table)
        { 
            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteriaCol));
            driver.FindElement(CommonLocator.SearchResultsCheckBox.Query).Click();
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
        }

        [Given(@"I search for a process '(.*)' and select a chart '(.*)'")]
        public void WhenISearchForAProcessAndSelectAChart(string searchText, string chartText)
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SearchIcon));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchInput, searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(GlDetailsLocators.GlDetailSubledgerEnquiryInqChart));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

       [When(@"I create GL Detail Subledger '(.*)' report")]
        public void WhenICreateGLDetailSubledgerReport(GlDetailSubledgerReports reportType)
        {
            var record = string.Empty;
            
            for (var retry = 0; retry < 12; retry++)
            {
                switch (reportType)
                {
                    case GlDetailSubledgerReports.BillingInvoice:
                        record = _featureContext[StepConstants.InvoiceNumberContext].ToString();
                        _actor.AttemptsTo(Click.On(GlDetailsLocators.GlDetailsBillInvoiceSearchButton));
                        break;
                    case GlDetailSubledgerReports.Cheques:
                        record = _featureContext[StepConstants.ChequeNumber].ToString();
                        _actor.AttemptsTo(Click.On(GlDetailsLocators.GlDetailsChequesSearchButton));
                        break;
                    case GlDetailSubledgerReports.Receipts:
                        record = _featureContext[StepConstants.ReceiptDocumentContext].ToString();
                        _actor.AttemptsTo(Click.On(GlDetailsLocators.GlDetailsReceiptSearchButton));
                        break;
                    case GlDetailSubledgerReports.Voucher:
                        record = _featureContext[StepConstants.InvoiceNumberContext].ToString();
                        _actor.AttemptsTo(Click.On(GlDetailsLocators.GlDetailsVoucherSearchButton));
                        break;
                }

                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, record));
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.Record(record)));
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.RunReport));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));

                if (!message.Contains("No records found. Please select different parameters."))
                {
                    break;
                }
                Thread.Sleep(TimeSpan.FromSeconds(15));
            }
            Console.WriteLine("Record Id: " + record);
        }

        [Then(@"I verify the billing invoice report description")]
        public void ThenIVerifyTheReportDescription()
        {
            var invoiceNumber =  _featureContext[StepConstants.InvoiceNumberContext].ToString();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var timeKeeperName =  _featureContext[StepConstants.FeeEarnerName].ToString()?.Trim();

            string desc = $"//e3e-report-data-grid//div[contains(text(),'<Auto Billed Time> Invoice: {invoiceNumber}, Matter: {matterNumber}, TimeKeeperName: {timeKeeperName}')]";
            //e3e-report-data-grid//div[contains(text(),'<Auto Billed Time> Invoice: 3000-200000004, Matter: 100600004, TimeKeeperName: Lena EarnerSurname')]

            By description = By.XPath(desc);            
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            

            if(!_actor.DoesElementExist(description))
            {
                int max = 5;
                int counter = 0;
                while (counter < max)
                {
                    var list = _actor.FindAll(By.XPath("//e3e-report-data-grid"));
                    _actor.scrollToElementInView(list.LastOrDefault());                    
                    //ext.PressKeyWithActions("pagedown");
                    //ext.ScrollByPage();
                    if (_actor.DoesElementExist(description))
                        break;
                    counter++;
                }
            }

            _actor.DoesElementExist(description).Should().BeTrue();
        }


        [Then(@"I verify the voucher report description")]
        public void ThenIVerifyTheVoucherDescription()
        {
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            var payeeName = _featureContext[StepConstants.Payee].ToString();
            var voucherNumber = _featureContext[StepConstants.VoucherAccountNumber].ToString();
            var description =
                $"//e3e-report-data-grid//div[contains(text(),'<Auto language AP Voucher> Name: {payeeName}, Voucher: {voucherNumber}, Invoice: {invoiceNumber}')]";
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var displayed = driver.FindElement(By.XPath(description)).Displayed;
            displayed.Should().BeTrue();
        }

        [Then(@"I verify the receipt report description")]
        public void ThenIVerifyTheReceiptReportDescription()
        {
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            var date = _featureContext[StepConstants.ReceiptDateContext].ToString();
            var receiptType = _featureContext[StepConstants.ReceiptTypeContext].ToString();
            var description =
                $"//e3e-report-data-grid//div[contains(text(),'<Auto Paid Time> Receipt Type: {receiptType}, Receipt Date: {date}, Invoice: {invoiceNumber}')]";
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var displayed = driver.FindElement(By.XPath(description)).Displayed;
            displayed.Should().BeTrue();
        }

        [Then(@"I verify the cheque report description")]
        public void ThenIVerifyTheChequeReportDescription()
        {
            var chequeNumber = _featureContext[StepConstants.ChequeNumber].ToString();
            var payee = _featureContext[StepConstants.PayeeNameContext].ToString();
            var bankName = _featureContext[StepConstants.BankAccountContext].ToString();
            var description =
                $"//e3e-report-data-grid//div[contains(text(),'<Auto unit AP Check> Payee: {payee}, Cheque: {chequeNumber}, Bank: {bankName}')]";
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var displayed = driver.FindElement(By.XPath(description)).Displayed;
            displayed.Should().BeTrue();
        }

        [Then(@"I verify the sections in GL detail description")]
        public void ThenIVerifyTheSectionsInGLDetailDescription()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.GeneralLedger));
            _actor.DoesElementExist(GlDetailsLocators.Query).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(GlDetailsLocators.LanguageDescription)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
