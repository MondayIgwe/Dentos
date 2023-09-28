using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxCode
{
   public  class TaxCodeLocator
    {
        public static IWebLocator SetCode => L(
            "Description",
            By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator GetCode => L(
            "Description",
            By.XPath("//div[contains(@name,'Code')]"));

        public static IWebLocator Description => L(
            "Code Disabled",
            By.XPath("//input[contains(@name,'Description')]"));

        public static IWebLocator TaxToolRef => L(
            "Code Disabled",
            By.XPath("//input[contains(@name,'TaxToolRef_ccc')]"));
    }
}
