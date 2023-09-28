using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank
{
   public class FbrItemsReportLocator
    {
        public static IWebLocator BankGroup => L(
            "Bank Group",
            By.XPath("//input[contains(@name,'BankGroup')]"));
        public static IWebLocator BankStatement => L(
            "Bank Statement",
            By.XPath("//input[contains(@name,'BankStmt')]"));
        public static IWebLocator GlBook => L(
            "GLBook",
            By.XPath("//input[contains(@name,'GLBook')]"));

        public static IWebLocator WorkSheetId => L(
            "WorkSheetId",
            By.XPath("//input[contains(@name,'BankRecWorkHdr')]"));

        public static IWebLocator ReconciliationComment  => L(
            "GLBook",
            By.XPath("//div[contains(text(),'Reconciliation Comment')]"));

        public static IWebLocator ReconciledAsOfDate => L(
            "ReconciledAsOfDate",
            By.XPath("//input[contains(@name,'AsOfDate')]"));

        public static IWebLocator BankAccount => L(
        "BankAccount",
        By.XPath("//input[contains(@name,'BankAcct')]"));

        //used indexes as no relationship could be found between the elements
        public static IWebLocator UnpostedReceipts => L(
        "UnpostedReceipts",
        By.XPath("//span[text()='Less Unposted Receipts']/ancestor::div[@class='e3e-report-data-container_control ng-star-inserted']/following-sibling::div[1]//span"));

        //used indexes as no relationship could be found between the elements
        public static IWebLocator UnPostedPayments => L(
        "UnPostedPayments",
        By.XPath("//span[text()='Less Unposted Payments']/ancestor::div[@class='e3e-report-data-container_control ng-star-inserted']/following-sibling::div[1]//span"));
    }
}
