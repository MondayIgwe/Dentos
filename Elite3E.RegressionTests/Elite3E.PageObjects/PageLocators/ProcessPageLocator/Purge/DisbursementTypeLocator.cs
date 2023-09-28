using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge
{
   public  class DisbursementTypeLocator
    {
        public static IWebLocator CodeInput => L(
           "Code",
           By.XPath("//input[contains(@name,'Code')]"));
        public static IWebLocator GetCode => L(
           "Code",
           By.XPath("//div[contains(@name,'Code')]"));

        public static IWebLocator Description => L(
            "Description",
            By.XPath("//input[contains(@name,'Description')]"));

        public static IWebLocator TransactionType => L(
            "Currency Type",
            By.XPath("//input[contains(@name,'TransactionType')]"));

        public static IWebLocator GetCheckBox => L(
             "CheckBox",
             By.CssSelector("input.mat-checkbox-input.cdk-visually-hidden"));

        public static IWebLocator SetCheckBox => L(
            "CheckBox",
            By.CssSelector("div.mat-checkbox-inner-container.mat-checkbox-inner-container-no-side-margin"));
    }
}
