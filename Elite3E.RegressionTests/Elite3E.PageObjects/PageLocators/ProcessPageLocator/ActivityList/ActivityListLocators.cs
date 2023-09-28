using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ActivityList
{
    public class ActivityListLocators
    {
        public static IWebLocator ActivityListCodeDiv => L(
       "ActivityListCodeDiv",
       By.XPath("//div[contains(@data-automation-id,'ActivityList') and contains(@name,'Code')]"));

        public static IWebLocator FirmActivityListCode => L(
       "FirmActivityListCode",
       By.XPath("//div[@ref='eBodyViewport']//input[contains(@data-automation-id,'ActivityList') and contains(@name,'Code')]"));

        public static IWebLocator FirmActivityListDescription => L(
        "FirmActivityListDescription",
        By.XPath("//div[@ref='eBodyViewport']//input[contains(@name,'Description')]"));

        public static IWebLocator FirmActivity => L(
      "FirmActivity",
      By.XPath("//div[@ref='eBodyViewport']//input[contains(@name,'FirmActivity')]"));

    }
}
