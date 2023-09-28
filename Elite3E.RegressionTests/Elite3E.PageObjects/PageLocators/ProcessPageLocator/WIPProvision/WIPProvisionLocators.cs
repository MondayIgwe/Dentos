using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.WIPProvision
{
    public class WIPProvisionLocators
    {
        public static IWebLocator glDate => L(
              "GlDate",By.XPath("//input[contains(@data-automation-id,'GLDate')]"));
        public static IWebLocator throughDate => L(
            "ThroughDate",By.XPath("//input[contains(@data-automation-id,'ThruDate')]"));
        public static IWebLocator editType => L(
            "EditType",By.XPath("//input[contains(@data-automation-id,'WPEditOptionList')]"));
        public static IWebLocator matter => L(
            "Matter",By.XPath("//input[contains(@data-automation-id,'Matter')]"));

        public static IWebLocator gridRowValue(string text) => L(
           "MatterNumber",By.XPath("//div[@col-id='"+text+"']//parent::div[@role='gridcell']"));



    }
}
