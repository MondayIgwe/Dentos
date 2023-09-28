using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.BillingRulesValidationList
{
    public class BillingRulesValidationListLocators
    {
        public static IWebLocator InputCode => L(
            "Billing Rules Validation List InputCcode Text Box",
            By.XPath("//input[contains(@name,'EBillValList')][contains(@name,'Code')]"));

        public static IWebLocator BillingRule => L(
           "Billing Rules Validation List InputCcode Text Box",
           By.XPath("//input[contains(@name,'EBillRule')]"));


        public static IWebLocator TextAreaDescription => L(
            " Billing Rules Validation List Text Area Description",
            By.XPath("//textarea[contains(@data-automation-id,'EBillValList')][contains(@data-automation-id,'Description')]"));

        public static IWebLocator  BillingRuleSearchButton => L(
            " Billing Rules Validation List - Rules 'Billing Rule' Search Button",
            By.XPath("//div[contains(@data-automation-id,'EBillRule_BOUND')]//button/span"));

        // checkBoxName examples : IsBillError, IsPendingWarning, IsProformaEdit, IsPendingError ( Get these values from inspecting the elements)
        public static IWebLocator BillingRuleCheckBox(string checkBoxName) => L(
            " Billing Rules Validation List - Rules 'Billing Rule'" + checkBoxName + " Check Box",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'" + checkBoxName + "')]//input"));
            
    }
}
