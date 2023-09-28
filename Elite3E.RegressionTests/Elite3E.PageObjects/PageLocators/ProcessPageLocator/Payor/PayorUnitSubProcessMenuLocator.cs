using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor
{
    public static class PayorUnitSubProcessMenuLocator
    {
        public static IWebLocator FindButton => L(
            "Find Button",
            By.CssSelector("e3e-form-anchor-view button"));
        public static IWebLocator Title => L(
            "Title",
            By.XPath("//e3e-form-anchor-view-header//div/span[contains(text(),'Payor Unit')]"));
    }
}
