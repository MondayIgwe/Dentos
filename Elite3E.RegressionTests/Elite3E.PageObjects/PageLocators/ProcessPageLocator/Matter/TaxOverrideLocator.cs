using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter
{
   public  class TaxOverrideLocator
    {
        public static IWebLocator CountryInput => L(
            "CountryInput",
            By.XPath("//input[contains(@name,'/Country')]"));
        public static IWebLocator TaxAreaInput => L(
            "TaxAreaInput",
            By.XPath("//input[contains(@name,'/TaxArea')]"));

        public static IWebLocator InputLocator => L(
            "Description",
            By.CssSelector("e3e-form-anchor-view input.mat-input-element.mat-form-field-autofill-control.cdk-text-field-autofill-monitored.ng-pristine.ng-valid"));

        public static IWebLocator GridLocator => L(
            "Code Disabled",
            By.CssSelector("div.ag-center-cols-container"));

        public static IWebLocator GridRows => L(
            "Grid Row",
            By.CssSelector("div[role='row']"));

        public static IWebLocator GridColumns => L(
            "Grid Columns",
            By.CssSelector("div[role='gridcell']"));

        public static IWebLocator GridRoot => L(
            "Grid Root",
            By.CssSelector("e3e-form-anchor-view"));

    }
}
