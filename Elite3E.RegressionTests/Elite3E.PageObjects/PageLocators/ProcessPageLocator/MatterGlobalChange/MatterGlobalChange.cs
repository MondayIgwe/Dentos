using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.MatterGlobalChange
{
    public class MatterGlobalChange
    {

        public static IWebLocator RequestedMatterSearch => L(
            "RequestedMatterSearch",
            By.XPath("//input[contains(@data-automation-id,'ReqMatters')]//following::mat-icon"));

        public static IWebLocator MatterNumber => L(
            "MatterNumber",
            By.XPath("//input[@name='advancedFindLookup.where.predicates.0.value']"));

        public static IWebLocator PreviewButton => L(
            "PreviewButton",
            By.XPath("//button//span[.=' Preview ']"));

        public static IWebLocator CalculateButton => L(
         "CalculateButton",
         By.XPath("//button[contains(@data-automation-id,'Calculate')]"));

        public static IWebLocator CalculateDiv => L(
        "CalculateDiv",
        By.XPath("//div[contains(@name,'MatterCount')]"));

        public static IWebLocator MatterListing => L(
       "MatterListing",
       By.XPath("//span[.='Matter Listing']"));

        public static IWebLocator BillingTimeKeeper => L(
       "BillingTimeKeeper",
       By.XPath("//input[contains(@name,'BillTkpr')]"));

        public static IWebLocator MatterDrillNumber => L(
       "MatterDrillNumber",
       By.XPath("//input[@name='advancedParameters[MatterDrillDown].where.predicates.0.value']"));



    }
}