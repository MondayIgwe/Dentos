using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.RateMaintenance
{
    public class RateMaintenanceLocators
    {
        public static IWebLocator Code => L(
             "Code",
        By.XPath("//input[contains(@data-automation-id,'Code')]"));

        public static IWebLocator Description => L(
             "Description",
        By.XPath("//input[contains(@data-automation-id,'Description')]"));

        public static IWebLocator Formula => L(
             "Formula",
        By.XPath("//e3e-text-input//textarea[contains(@data-automation-id,'Formula')]"));

        public static IWebLocator RateType1Value => L(
             "RateType1Value",
        By.XPath("//input[contains(@data-automation-id,'Rate1')]"));

        public static IWebLocator TestFormulaButton => L(
            "TestFormulaButton",
        By.XPath("//button//span[text()=' Test Formula ']"));

        public static IWebLocator TestResultDiv => L(
            "TestResultDiv",
        By.XPath("//div[contains(@data-automation-id,'Result') and contains(@name,'Rate')]"));

        public static IWebLocator RateExceptionList => L(
            "RateExceptionList",
        By.XPath("//input[contains(@data-automation-id,'RateExcList')]"));

        public static IWebLocator AddNewRateGroup => L(
           "AddNewRateGroup",
       By.XPath("//button//span[text()=' Add new Rate Exception Group ']"));

        public static IWebLocator Rate => L(
           "Rate Input",
       By.XPath("//e3e-big-list-input//input[contains(@data-automation-id,'RateExcDet') and contains(@name,'Rate')]"));

        public static IWebLocator ClientMaintenanceHeader => L(
          "ClientMaintenanceHeader",
      By.XPath("//h3[text()='Client Maintenance']"));

        public static IWebLocator RateExceptionGroupInput => L(
         "RateExceptionGroupInput",
     By.XPath("//e3e-big-list-input//input[contains(@name,'RateExc')]"));

        public static IWebLocator RateDiv => L(
        "RateDiv",
     By.XPath("//div[contains(@name,'MatterRate_Test')]"));

        public static IWebLocator StandardRateCheckbox => L(
       "StandardRateCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsStandard')]"));

        public static IWebLocator DefaultCurrency => L(
     "DefaultCurrency dropdown",
     By.XPath("//input[contains(@data-automation-id,'CurrencyDefault')]"));

        public static IWebLocator DefaultRate => L(
     "DefaultRate",
     By.XPath("//input[contains(@data-automation-id,'DefaultRate')]"));

        public static IWebLocator RateTypeAmount => L(
     "RateTypeAmount",
      By.XPath("//e3e-money-input//input[contains(@name,'RateDateDet')]"));

        public static IWebLocator RateTypeCurrency => L(
     "RateTypeCurrency",
     By.XPath("//input[contains(@name,'Currency') and contains(@data-automation-id,'RateDateDet')]"));

        public static IWebLocator EffectiveDatedRatesStartDate => L(
     "EffectiveDatedRatesStartDate",
     By.XPath("//input[contains(@name,'RateType') and contains(@data-automation-id,'EffStart')]"));

        public static IWebLocator EffectiveDatedRatesDescription => L(
      "EffectiveDatedRatesDescription",
      By.XPath("//input[contains(@name,'RateTypeDate') and contains(@data-automation-id,'Description')]"));

        public static IWebLocator EffectiveDatedRatesDefaultRate=> L(
     "EffectiveDatedRatesDefaultRate",
     By.XPath("//input[contains(@name,'RateTypeDate') and contains(@data-automation-id,'DefaultRate')]"));


    }
}
