using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;


namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payee
{
    public class BankAccountTypeLocator
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

        public static IWebLocator SetIntermediaryBank => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsIntermediary_ccc')]/label/div"));

        public static IWebLocator GetIntermediaryBank => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsIntermediary_ccc')]/label/div/input"));

        public static IWebLocator IntermediaryBankLabel => L(
            "Local Description Label",
            By.XPath("//div[contains(@data-automation-id,'IsIntermediary_ccc_BOUND')]/label"));

    }
}
