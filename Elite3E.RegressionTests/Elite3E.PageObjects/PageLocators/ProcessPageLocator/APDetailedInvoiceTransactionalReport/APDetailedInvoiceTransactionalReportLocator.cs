using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.APDetailedInvoiceTransactionalReport
{
    public class APDetailedInvoiceTransactionalReportLocator
    {
        public static IWebLocator RequestedVoucherSearch => L(
              "RequestedVoucherSearch", By.XPath("//span[text()='Requested Voucher']//ancestor::e3e-bound-input//mat-icon[text()='search']"));

        public static IWebLocator InvoiceNumberInputBox => L(
              "InvoiceNumberInputBox", By.XPath("(//input/following::span[text()='Invoice Number']/ancestor::div[contains(@class,'form-container')][1]//input)[last()]"));

        public static IWebLocator RunMetricDropDown => L(
             "RunMetrciDropDown", By.XPath("//button[contains(@data-automation-id,'RunMetric_Button-dropdown')]"));

        public static IWebLocator RunReportButton => L(
             "RunReportButton", By.XPath("//button[contains(@data-automation-id,'RunReport_Button')]"));

        public static IWebLocator VoucherAmountWithoutTax(String amount) => L(
             "VoucherAmountWithoutTax", By.XPath("//div[text()='"+amount+"' and ancestor::td[@col-index='0'] and ancestor::td/following-sibling::td//div[not(contains(@text,'Tax'))]]"));

        public static IWebLocator TaxAmountFromTaxRow(String taxAmount) => L(
             "TaxAmountFromTaxRow", By.XPath("//div[text()='"+taxAmount+"' and ancestor::td[@col-index='0'] and ancestor::td/following-sibling::td//div[text()='Tax']]"));

        public static IWebLocator VoucherAndInvoiceLabel(String voucherNumber, string invoiceNumber) => L(
             "VoucherNumberAndInvoiceNumber", By.XPath("//*[contains(text(),'"+voucherNumber+ " " +invoiceNumber+"')]"));

    }
}
