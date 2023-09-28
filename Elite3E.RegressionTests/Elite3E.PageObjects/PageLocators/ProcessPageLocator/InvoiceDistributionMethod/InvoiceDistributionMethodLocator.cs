using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceDistributionMethod
{
    public class InvoiceDistributionMethodLocator
    {
        public static IWebLocator Code => L(
         "Code",
          By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator Description => L(
 "Description",
  By.XPath("//input[contains(@name,'Description')]"));

        public static IWebLocator DefaultCheckbox => L(
 "DefaultCheckbox",
  By.XPath("//mat-checkbox[contains(@data-automation-id,'Is_Default')]"));

        public static IWebLocator AutoDispatch => L(
 "AutoDispatchCheckbox",
  By.XPath("//mat-checkbox[contains(@data-automation-id,'IsAutoDispatch')]"));

        public static IWebLocator RTKDispatch => L(
     "RTKDispatch",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsRTKDispatch')]"));

        public static IWebLocator FinanceDispatch => L(
     "FinanceDispatch",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsFinanceDispatch')]"));
        public static IWebLocator NextInvoiceNumber => L(
        "NextInvoiceNumber",
        By.XPath("//input[contains(@name,'NextInvNumber')]"));

        public static IWebLocator NextCreditNoteNumber => L(
        "NextCreditNoteNumber",
        By.XPath("//input[contains(@name,'NextCRNoteNumber')]"));

        public static IWebLocator NextTaxInvoiceNumber => L(
       "NextTaxInvoiceNumber",
       By.XPath("//input[contains(@name,'NextTaxInvNumber')]"));

        public static IWebLocator NextCreditNoteTaxNumber => L(
        "NextCreditNoteTaxNumber",
        By.XPath("//input[contains(@name,'NextCRTaxInvNumber')]"));

        public static IWebLocator InvoiceType => L(
        "InvoiceType",
        By.XPath("//input[contains(@name,'InvoiceType_ccc')]"));
    }
}
