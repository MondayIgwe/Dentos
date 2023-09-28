using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;


namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF
{
    public class UDFValidationListLocators
    {
        public static IWebLocator codeValueElement => L(
           "codeValue", By.XPath("//div[contains(@data-automation-id,'Code_BOUND')]//div[contains(@data-automation-id,'Code')]"));
        public static IWebLocator udfValueElement => L(
             "udfValue", By.XPath("//div[@col-id='UDF_ccc' and @role='gridcell']"));
        public static IWebLocator udfValidationItemDescription => L(
            "udfvalidationdescription", By.XPath("//input[contains(@name,'UDFValidationListItem_ccc') and contains(@data-automation-id,'Description')]"));
        public static IWebLocator udfValidationItemCode => L(
            "udfvalidationcode", By.XPath("//input[contains(@name,'UDFValidationListItem_ccc') and contains(@data-automation-id,'Code')]"));
    }
}
