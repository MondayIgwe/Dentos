using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF
{
    public class UDFLocators
    {
        public static IWebLocator code => L(
              "code", By.XPath("//input[contains(@data-automation-id,'Code')]"));
        public static IWebLocator description => L(
            "description", By.XPath("//input[contains(@data-automation-id,'Description')]"));
        public static IWebLocator udfInput => L(
            "udf", By.XPath("//input[contains(@data-automation-id,'UDF_ccc')]"));
        public static IWebLocator isPrintTemplateCol => L(
            "isPrintColumn", By.XPath("(//div[@col-id='IsPrintTemplate'])[1]"));
        public static IWebLocator isPrintTemplateCheckBox => L(
           "isPrintCheckbox", By.XPath("//div[@col-id='IsPrintTemplate']//span//i"));
        public static IWebLocator isActive => L(
        "isActive", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsActive')]"));
        public static IWebLocator validationList => L(
           "validationList", By.XPath("//div[contains(@data-automation-id,'UDFValidationList_ccc')]//parent::e3e-readonly-input"));
        public static IWebLocator archetype => L(
          "archetype", By.XPath("//div[contains(@data-automation-id,'UDFArchetype')]//parent::e3e-readonly-input"));
        public static IWebLocator type => L(
         "type", By.XPath("//div[contains(@data-automation-id,'UDFType')]//mat-icon[text()='arrow_drop_down']"));
        public static IWebLocator typeInput => L(
         "type", By.XPath("//input[contains(@data-automation-id,'UDFType')]"));
        public static IWebLocator label => L(
        "label", By.XPath("//input[contains(@data-automation-id,'UDFLabel')]"));
        public static IWebLocator validationDropdown => L(
        "valdropdown", By.XPath("//e3e-bound-input//div[contains(@data-automation-id,'UDFValidationList_ccc_BOUND')]//button"));
        public static IWebLocator validationListInput => L(
        "validationList", By.XPath("//input[contains(@data-automation-id,'UDFValidationList_ccc')]"));
        public static IWebLocator activity(string text) => L(
        "activity", By.XPath("//span[text()='"+text+"']"));
        public static IWebLocator archetypeInput => L(
       "validationList", By.XPath("//input[contains(@data-automation-id,'UDFArchetype')]"));
        public static IWebLocator listHeader => L(
       "listHeader", By.XPath("//h3[text()='UDF List']"));
        public static IWebLocator UDFList => L(
      "code", By.XPath("//input[contains(@data-automation-id,'UDFList_ccc')]"));
        public static IWebLocator PrintTemplateLabel => L(
          "isPrintTemplateLabel", By.XPath("//div[@col-id='IsPrintTemplate']//span[@ref='eText']"));
    }
}
