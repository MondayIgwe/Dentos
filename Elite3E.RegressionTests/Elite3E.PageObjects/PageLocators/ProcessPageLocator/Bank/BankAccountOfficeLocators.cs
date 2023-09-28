using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank
{
   public  class BankAccountOfficeLocators
    {
        
        public static IWebLocator GetRemittanceAccount => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsRemittance')]/label/div/input")); 

        public static IWebLocator SetRemittanceAccount => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsRemittance')]/label/div"));
        public static IWebLocator GetUltimateRemittanceAccount => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsUltimateRemittance_ccc')]/label/div/input")); 

        public static IWebLocator SetUltimateRemittanceAccount => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsUltimateRemittance_ccc')]/label/div")); 

        public static IWebLocator AccountName => L(
            "Description",
            By.XPath("//input[contains(@name,'Name')]"));
        public static IWebLocator Description => L(
           "Description",
           By.XPath("//input[contains(@name,'Description')]"));
    }
}
