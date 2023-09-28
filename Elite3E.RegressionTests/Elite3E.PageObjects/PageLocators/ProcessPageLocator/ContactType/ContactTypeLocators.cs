using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ContactType
{
    public class ContactTypeLocators
    {
        public static IWebLocator code => L(
              "code", By.XPath("//input[contains(@data-automation-id,'Code')]"));
        public static IWebLocator codeValue => L(
              "codeValue", By.XPath("//e3e-readonly-input//div[contains(@data-automation-id,'Code')]"));
        public static IWebLocator description => L(
            "description", By.XPath("//input[contains(@data-automation-id,'Description')]"));
        public static IWebLocator contactTypeCheckBox(string id) => L(
             "checkbox", By.XPath("//mat-checkbox[contains(@data-automation-id,'"+id+"')]"));
        public static IWebLocator contactTypeHeader => L(
           "header", By.XPath("//h3[text()='Contact Type']"));
      
    }
}
