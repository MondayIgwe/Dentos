using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank
{
   public class FullBankReconciliationLocator
    {
        public static IWebLocator BankCashUnreconciledTransactions => L(
            "Description",
            By.XPath("//h5[contains(text(),'Bank/Cash Unreconciled Transactions')]"));

        public static IWebLocator GridHeaderReconComment => L(
            "GridHeaderReconColumn",
            By.XPath("//div[@title='Recon Comment']"));
        
        public static IWebLocator horizontalScrollLocator => L(
            "horizontalScrollLocator",
            By.XPath("//div[text()='Total']//ancestor::div[@ref='eRootWrapper']//div[contains(@class,'horizontal-scroll-container')]/parent::div[contains(@class,'horizontal-scroll')]"));

        public static IWebLocator BankGroup => L(
           "BankGroup",
           By.XPath("//input[contains(@name,'BankGroup')]"));


        public static IWebLocator LoadTransactions => L(
          "LoadTransactions",
          By.XPath("//span[contains(text(),'Load Transactions')]"));

        public static IWebLocator WorksheetId => L(
           "WorksheetId",
           By.XPath("//input[contains(@name,'/WorksheetID')]"));

        public static IWebLocator CashType(string cashType) => L(
            "Cash Type",
            By.XPath("//div[@col-id='CashTypeList']//span[text()='"+ cashType + "']"));

        public static IWebLocator Amount(string amount) => L(
        "Amount",
        By.XPath("//div[@col-id='Amount']//span[text()='" + amount + "']"));

        public static IWebLocator ClearCheckbox => L(
        "ClearCheckbox",
        By.XPath("//div[@col-id='IsMatched']//span/i[contains(text(),'check_box')]"));

        public static IWebLocator MatchButton => L(
        "MatchButton",
        By.XPath("//button/span[text()=' Match ']"));

        public static IWebLocator ClosedCheckbox => L(
        "ClosedCheckbox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsReconciled')]"));

        public static IWebLocator AdvanceFindSearchAttribute(int rowIndex) => L(
            "AdvanceFindSearchAttribute",
            By.Name("advancedParameters[ReconciledItemsRptObj].where.predicates." + rowIndex + ".attribute"));

        public static IWebLocator AdvanceFindSearchOperator(int rowIndex) => L(
            "AdvanceFindSearchAttribute",
            By.CssSelector("mat-select[data-automation-id='advancedParameters[ReconciledItemsRptObj].where.predicates." + rowIndex + ".operator']"));


        public static IWebLocator AdvanceFindSearchValue(int rowIndex) => L(
            "AdvanceFindSearchAttribute",
            By.Name("advancedParameters[ReconciledItemsRptObj].where.predicates." + rowIndex + ".value"));

          public static IWebLocator RunReport => L(
        "RunReport",
        By.XPath("//button/span[contains(text(),'Run Report')]"));

        public static IWebLocator ReconciledTransaction(string reference,string amount) => L(
            "ReconciledTransaction",
            By.XPath("//div[text()='"+ reference + "']/ancestor::tr//td//div[text()='"+amount+"']"));

        public static IWebLocator GroupsHeader => L(
            "GroupsHeader",
            By.XPath("//span[contains(text(),'Groups:')]"));

        public static IWebLocator BankTransactionsHeader => L(
        "BankTransactionsHeader",
        By.XPath("//span[contains(text(),'Bank/Cash Unreconciled Transactions')]"));

    }
}
