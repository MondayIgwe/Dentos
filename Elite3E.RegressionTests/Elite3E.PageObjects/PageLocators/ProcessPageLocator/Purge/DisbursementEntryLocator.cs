using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge
{
   public  class DisbursementEntryLocator
    {
        public static IWebLocator WorkDate => L(
           "WorkDate",
           By.XPath("//input[contains(@name,'WorkDate')]"));

        public static IWebLocator DisbursementType => L(
            "DisbursementType",
            By.XPath("//input[contains(@name,'CostType')]"));

        public static IWebLocator Matter => L(
           "Matter",
           By.XPath("//input[contains(@name,'Matter')]"));

        public static IWebLocator WorkCurrency => L(
            "WorkCurrency",
            By.XPath("//input[contains(@name,'Currency')]"));

        public static IWebLocator WorkAmount => L(
            "WorkAmount",
            By.XPath("//input[contains(@name,'WorkAmt')]"));

        public static IWebLocator ReferenceRate => L(
           "ReferenceRate",
           By.XPath("//input[contains(@name,'RefRate')]"));

        public static IWebLocator Narrative => L(
             "CheckBox",
             By.CssSelector("div.ql-editor.ql-blank p"));

        public static IWebLocator TaxCode => L(
            "TaxCode",
            By.XPath("//input[contains(@name,'TaxCode')]"));

        public static IWebLocator InternalComments => L(
           "TaxCode",
           By.XPath("//textarea[contains(@data-automation-id,'InternalComments')]"));
       
    }
}
