using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using  System.Linq;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using FluentAssertions;
using System.Threading;
using System;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class InvoiceFeatureSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public InvoiceFeatureSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I quick search by invoice number")]
        public void WhenIQuickSearchByMatterNumber()
        {
            _actor.AttemptsTo(QuickFind.Search(_featureContext[StepConstants.InvoiceNumberContext].ToString()));
        }

        [Given(@"I navigate to the invoices process")]
        public void GivenINavigateToBankAccountClientAccount()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.Invoices, false));
            if (_actor.DoesElementExist(InvoicesLocator.InvoiceMasterProcess))
            {
                _actor.AttemptsTo(Click.On(InvoicesLocator.InvoiceMasterProcess));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"I navigate to the Invoice process")]
        public void WhenINavigateToTheInvoiceProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SearchIcon));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchInput, "Invoices"));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var browser = _actor.Using<BrowseTheWeb>().WebDriver;
            var resultProcesses = browser.FindElements(CommonLocator.SearchResults.Query);

            resultProcesses.FirstOrDefault(ele => ele.Text.Trim() == "Invoices")?.Click();
        }

        [Then(@"I verify the '([^']*)' field does not exists on the advanced find")]
        public void ThenIVerifyTheFieldDoesNotExistsOnTheAdvancedFind(string parameter)
        {
            _actor.AttemptsTo(Click.On(MatterLocator.AdvancedFindTab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(BankAccountClientAccountLocators.CurrencyInputAttribute(parameter)).Should().BeFalse();
        }


        [StepDefinition(@"add select an invoice")]
        public void WhenAddSelectAnInvoice()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
           
            _actor.AttemptsTo(Click.On(InvoicesLocator.SearchIcon));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            if (driver.FindElements(InvoicesLocator.InvNum.Query).Count > 0) return;

            _actor.AttemptsTo(Click.On(InvoicesLocator.FirstRowInGrid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(InvoicesLocator.SelectBtn));


        }

        [Then(@"confirm invoice field exist")]
        public void ThenConfirmInvoiceFieldExist()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(InvoicesLocator.InvNum), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I validate gl postings status")]
        public void ThenIValidateGlPostingsStatus()
        {
            int timeout = 24;
            int counter = 0;
            string invoiceStatus = String.Empty;

            while (counter < timeout)
            {
                _actor.WaitsUntil(Appearance.Of(InvoicesLocator.PostingsInformationHeader), IsEqualTo.True());
                if (_actor.DoesElementExist(InvoicesLocator.GlPostingsStatus))
                {
                    invoiceStatus = _actor.GetElementText(InvoicesLocator.GlPostingsStatus);
                    if (!string.IsNullOrEmpty(invoiceStatus))
                    {
                        invoiceStatus.Should().NotBeEquivalentTo("Waiting for Period to open");
                        invoiceStatus.Should().NotBeEquivalentTo("Error");
                        if (invoiceStatus.Equals("Posted", StringComparison.CurrentCultureIgnoreCase))
                            break;
                    }
                }
                _actor.AttemptsTo(Click.On(InvoicesLocator.CloseGlPostingsButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                Thread.Sleep(TimeSpan.FromSeconds(10));

                _actor.AttemptsTo(Click.On(InvoicesLocator.GlPostingsButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                counter++;
            }

            _actor.DoesElementExist(InvoicesLocator.GlPostingsStatus).Should().BeTrue();
            invoiceStatus = _actor.GetElementText(InvoicesLocator.GlPostingsStatus);
            invoiceStatus.Should().NotBeNullOrEmpty();
            invoiceStatus.Should().BeEquivalentTo("Posted");

             var journalManager = _actor.AsksFor(Text.Of(InvoicesLocator.JournalManager));
            _featureContext[StepConstants.JournalManager] = journalManager;
           
        }

        [Then(@"I validate tax amount in gl postings")]
        public void ThenIValidateTaxAmountInGlPostings()
        {
            var taxInputAmount = _featureContext[StepConstants.TaxInputAmount].ToString();
            var voucherAmount = _featureContext[StepConstants.VoucherAmount].ToString();
            _actor.WaitsUntil(Appearance.Of(InvoicesLocator.GLPostingsGridAmount(taxInputAmount)), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(InvoicesLocator.GLPostingsGridAmount(voucherAmount)), IsEqualTo.True());
        }

        [Then(@"I validate the gl timekeeper is '([^']*)'")]
        public void ThenIValidateTheGlTimekeeperIs(string timeKeeperValue)
        {
            var glMaskedValues = _actor.AsksFor(TextList.For(EntryAndModifyProcessLocators.GLMaskedValues));
            glMaskedValues.Any(item => item.EndsWith(timeKeeperValue));
            _featureContext[StepConstants.GLMaskedValues] = glMaskedValues;
        }

        [Then(@"I verify the invoice type")]
        public void ThenIVerifyTheInvoiceType()
        {
             var invoiceType = _featureContext[StepConstants.InvoiceType].ToString();
            _actor.AsksFor(Text.Of(InvoicesLocator.InvoiceType)).Should().Contain(invoiceType);
        }

    }
}
