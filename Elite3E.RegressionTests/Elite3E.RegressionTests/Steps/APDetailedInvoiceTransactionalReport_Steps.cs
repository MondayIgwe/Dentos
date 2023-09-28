using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.APDetailedInvoiceTransactionalReport;
using Elite3E.RegressionTests.StepHelpers;
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
    public class APDetailedInvoiceTransactionalReport_Steps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public APDetailedInvoiceTransactionalReport_Steps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I run report by searching with invoice number")]
        public void RunReportBySearchingWithInvoiceNumber()
        {
          
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            var invoiceDateFrom = _featureContext[StepConstants.InvoiceDate].ToString();
  
            _actor.AttemptsTo(Click.On(APDetailedInvoiceTransactionalReportLocator.RequestedVoucherSearch));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(APDetailedInvoiceTransactionalReportLocator.InvoiceNumberInputBox, invoiceNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.Submitdialog));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.InvoiceDateFrom, invoiceDateFrom));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(DateControl.SelectDate("Invoice Date To", invoiceDateFrom));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(APDetailedInvoiceTransactionalReportLocator.RunMetricDropDown));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(APDetailedInvoiceTransactionalReportLocator.RunReportButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

           
        }

        [StepDefinition(@"I verify the voucher details in AP Detailed Invoice Transactional Report")]
        public void VerifyTheVoucherDetailsInAPDetailedInvoiceTransactionalReport()
        {
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            var voucherNumber = _featureContext[StepConstants.VoucherAccountNumber].ToString();
            var voucherAmount = _featureContext[StepConstants.VoucherAmount].ToString();
            var taxInputAmount = _featureContext[StepConstants.TaxInputAmount].ToString();

            //Validate amount
            _actor.WaitsUntil(Appearance.Of(APDetailedInvoiceTransactionalReportLocator.VoucherAmountWithoutTax(voucherAmount)), IsEqualTo.True());

            //validate tax
            _actor.WaitsUntil(Appearance.Of(APDetailedInvoiceTransactionalReportLocator.TaxAmountFromTaxRow(taxInputAmount)), IsEqualTo.True());

            //validate voucher id + invoice number
            _actor.WaitsUntil(Appearance.Of(APDetailedInvoiceTransactionalReportLocator.VoucherAndInvoiceLabel(voucherNumber, invoiceNumber)), IsEqualTo.True());

        }


    }
}
