using OpenQA.Selenium;
using Boa.Constrictor.WebDriver;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA
{
    public static class CoALegacyLocator
    {
        public static IWebLocator CodeInput => L(
      "Unit",
      By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator GetCode => L(
           "Code",
           By.XPath("//div[contains(@name,'Code')]"));

        public static IWebLocator FirmDescription => L(
       "Firm Description",
       By.XPath("//input[contains(@name,'Description')]"));

        public static IWebLocator LocalDescription => L(
        "Local Description",
        By.XPath("//input[contains(@name,'LocalDescription')]"));

        public static IWebLocator LocalDescriptionLabel => L(
        "Local Description Label",
        By.XPath("//div[contains(@data-automation-id,'LocalDescription_BOUND')]/label"));

        public static IWebLocator LegacyAccount => L(
       "LegacyAccount",
       By.XPath("//div[@title='Legacy Account']"));

    }
}