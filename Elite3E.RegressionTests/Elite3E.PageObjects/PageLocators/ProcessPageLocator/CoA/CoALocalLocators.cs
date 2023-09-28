using OpenQA.Selenium;
using Boa.Constrictor.WebDriver;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA
{
    public static class CoALocalLocators
    {
        public static IWebLocator NaturalInput => L(
          "Natural",
          By.XPath("//input[contains(@name,'NaturalGCOA_ccc')]"));

        public static IWebLocator NaturalAccountInput => L(
         "NaturalAccountInput",
         By.XPath("//input[contains(@name,'NaturalGCOA_ccc') and contains(@data-automation-id,'Natural')]"));

        public static IWebLocator GetNatural => L(
           "Code",
           By.XPath("//div[contains(@name,'Natural')]"));

        public static IWebLocator UnitInput => L(
          "Unit",
          By.XPath("//input[contains(@name,'UnitChartMap_ccc')]"));

        public static IWebLocator LocalAccount => L(
            "LocalAccount",
            By.XPath("//span[text()='Local Account']/ancestor::div[contains(@data-automation-id,'LocalAccount_BOUND')]//input"));

        public static IWebLocator GetUnit => L(
      "Code",
      By.XPath("//div[contains(@name,'UnitChartMap_ccc')]"));

        public static IWebLocator FirmDescription => L(
           "Firm Description",
           By.XPath("//input[contains(@name,'Description')]"));

        public static IWebLocator LocalDescription => L(
            "Local Description",
            By.XPath("//input[contains(@name,'LocalDescription')]"));

        public static IWebLocator LegacyNatural => L(
           "Local Description",
           By.XPath("//input[contains(@name,'LegacyChartDet_ccc') and contains(@data-automation-id,'Natural')]"));

        public static IWebLocator LegacyFirmDescription => L(
           "Local Description",
           By.XPath("//input[contains(@name,'LegacyChartDet_ccc') and contains(@data-automation-id,'Description')]"));

        public static IWebLocator LocalDescriptionLabel => L(
            "Local Description Label",
            By.XPath("//div[contains(@data-automation-id,'LocalDescription_BOUND')]/label"));

        public static IWebLocator FirstNaturalValue => L(
              "Natural value",
              By.XPath("//e3e-readonly-input//div[contains(@data-automation-id,'NaturalGCOA_ccc') and contains(@data-automation-id,'LocalAccount_ccc')]"));

        public static IWebLocator GlUnit => L(
           "GlUnit",
           By.XPath("//input[contains(@name, 'GLUnitRef')]"));

        public static IWebLocator GlUnitDropDownLocator => L(
            "GlUnitDropDownLocator",
            By.XPath("//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator LegacyChart => L(
           "LegacyChart",
           By.XPath("//input[contains(@name, 'LegacyChartHdr_ccc')]"));

        public static IWebLocator Region => L(
           "Region",
           By.XPath("//input[contains(@name,'Region_cc')]/ancestor:: mat-form-field//mat-icon"));

        public static IWebLocator CoaRegion => L(
           "CoaRegion",
           By.XPath("//input[contains(@name,'Region_cc')]"));

        public static IWebLocator ImportLegacyChart => L(
         "ImportLegacyChart",
         By.XPath("//button//span[text()=' Import Legacy Chart ']"));

        public static IWebLocator CloneChart => L(
           "CloneChart",
           By.XPath("//span[text()=' Clone Chart ']"));

        public static IWebLocator MappingDetailGridHeader => L(
            "Local Description Label",
            By.XPath("//e3e-form-anchor-view/descendant::span[@role='columnheader']"));
    }
}