using static Boa.Constrictor.WebDriver.WebLocator;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma
{
    public class PurgeLocator
    {
        public static IWebLocator PurgeButton => L(
            "CloneButton",
            By.XPath("//button/span[contains(text(),'Purge')]"));

        public static IWebLocator OkButton => L(
           "CloneButton",
           By.XPath("//button/span[contains(text(),'OK')]"));

    }
}
