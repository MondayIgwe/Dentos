using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank
{
    public class BankGroupMaintenanceLocator
    {
        public static IWebLocator BankReconciliationFrequency => L(
            "BankReconciliationFrequency",
            By.XPath("//input[contains(@name,'BankRecFrequency_ccc')]"));

        public static IWebLocator BankReconciliationFrequencyDropDown => L("Bank ReconciliationFrequency DrowDoen Button", 
            By.XPath("//input[contains(@name,'BankRecFrequency_ccc')]/parent::div/following-sibling::div//mat-icon"));

        public static IWebLocator ModuleType => L("ModuleType",
         By.XPath("//input[contains(@name,'/BankModuleList')]"));

        public static IWebLocator ReconciliationRuleSet => L("ReconciliationRuleSet",
        By.XPath("//input[contains(@name,'/BankRecRuleSet')]"));

        public static IWebLocator BankToBank => L("BankToBankCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'/IsOffsettingBankTranReconAllowed')]"));

        public static IWebLocator CashToCash => L("CashToCashCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'/IsOffsettingCashTranReconAllowed')]"));


        public static IWebLocator ReconcileAcrossStatements => L("ReconcileAcrossStatementsCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'/IsReconcileByGroup')]"));
    }
}
