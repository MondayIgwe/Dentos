using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxCode
{
   public  class TaxCodeTaxLocator
    {

        public static IWebLocator Grid => L(
            "Description",
            By.CssSelector("e3e-form-anchor-view"));

        public static IWebLocator Tax => L(
            "Description",
            By.XPath("//input[contains(@name,'TaxCodeTax')]"));
    }
}
