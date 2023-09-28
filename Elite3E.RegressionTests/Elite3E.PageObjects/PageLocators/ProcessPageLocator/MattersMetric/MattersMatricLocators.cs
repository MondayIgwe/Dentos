using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.MattersMetric
{
    public class MattersMatricLocators
    {
        public static IWebLocator RequestedMattersIcon => L(
       "RequestedMattersIcon",
       By.XPath("//e3e-bound-input//div[contains(@data-automation-id,'ReqMatters')]//button"));

    }
}
