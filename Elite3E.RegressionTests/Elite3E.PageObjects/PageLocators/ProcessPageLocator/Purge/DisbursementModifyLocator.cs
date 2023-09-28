using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge
{
    public class DisbursementModifyLocator
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

        public static IWebLocator RefCurrency => L(
           "RefCurrency",
           By.XPath("//input[contains(@data-automation-id,'RefCurrency')]"));

        public static IWebLocator WorkAmount => L(
            "WorkAmount",
            By.XPath("//input[contains(@name,'WorkAmt')]"));

        public static IWebLocator ReferenceRate => L(
           "ReferenceRate",
           By.XPath("//input[contains(@name,'RefRate')]"));

        public static IWebLocator Narrative => L(
          "Narrative Text Box",
           By.XPath("//span[text()='Narrative']//ancestor::e3e-bound-input//div[@contenteditable]/p"));

        public static IWebLocator TaxCode => L(
            "TaxCode",
            By.XPath("//input[contains(@name,'TaxCode')]"));

        public static IWebLocator InternalComments => L(
           "TaxCode",
           By.XPath("//textarea[contains(@data-automation-id,'InternalComments')]"));

        public static IWebLocator PurgeTypeDisabled => L(
           "PurgeType",
           By.XPath("//div[contains(@name,'PurgeType')]"));

        public static IWebLocator PurgeType => L(
          "PurgeType",
          By.XPath("//input[contains(@name,'PurgeType')]"));
    }
}
