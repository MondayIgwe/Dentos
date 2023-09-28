using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ClientAccountDisbursement
{
    public class ClientAccountDisbursementLocators
    {
        public static IWebLocator TransactionDateInput => L(
            "TransactionDateInput",
            By.XPath("//input[contains(@data-automation-id,'TranDate')]"));

        public static IWebLocator DisbursementTypeInput => L(
          "DisbursementTypeInput",
          By.XPath("//input[contains(@data-automation-id,'TrustDisbursementType')]"));

        public static IWebLocator BankAccountAcctInput => L(
         "BankAccountAcctInput",
         By.XPath("//input[contains(@data-automation-id,'BankAcctTrust')]"));

        public static IWebLocator MatterInput => L(
         "MatterInput",
         By.XPath("//input[contains(@data-automation-id,'Matter')]"));

        public static IWebLocator IntendedUseDropdown => L(
         "IntendedUseDropdown",
         By.XPath("//input[contains(@data-automation-id,'TrustIntendedUse')]"));

        public static IWebLocator AmountInput => L(
        "AmountInput",
        By.XPath("//input[contains(@data-automation-id,'Amount')]"));

        public static IWebLocator DocumentNumberInput => L(
        "DocumentNumberInput",
        By.XPath("//input[contains(@data-automation-id,'DocumentNumber')]"));

        public static IWebLocator PaymentNameInput => L(
       "PaymentNameInput",
       By.XPath("//input[contains(@data-automation-id,'PayeeName')]"));

        public static IWebLocator UseDetailsInput => L(
      "UseDetailsInput",
      By.XPath("//input[contains(@data-automation-id,'TrustIntendedUseDet')]"));

        public static IWebLocator TrustDisbursementIndex => L(
        "TrustDisbursementIndex",
        By.XPath("//div[contains(@name,'/TrustDsbmtIndex')]"));


        public static IWebLocator ClientAccountDisbursementSection => L(
            "ClientAccountDisbursementSection",
            By.XPath("//div[@title='Client Account Disbursement' and text()='Client Account Disbursement']"));

        public static IWebLocator TrustDisbursementRequestFinanceApprovalFilterIcon => L(
     "TrustDisbursementRequestFinanceApprovalFilterIcon",
     By.XPath("//div[text()='Trust Disbursement Request Entry - Finance Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//mat-icon[text()='filter_list']"));

        public static IWebLocator CompletedDateSortDisbursementRequestEntry => L(
        "CompletedDateSortDisbursementRequestEntry",
        By.XPath("//div[text()='Trust Disbursement Request Entry - Finance Approval ']//ancestor::mat-expansion-panel-header/following-sibling::div//span[text()='Completed Date']"));

        public static IWebLocator TrustDisbursementRequestFinanceInput => L(
        "TrustDisbursementRequestFinanceInput",
        By.XPath("//div[text()='Trust Disbursement Request Entry - Finance Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//input"));


        public static IWebLocator TrustDisbursementFinanceApprovalFilterIcon => L(
     "TrustDisbursementFinanceApprovalFilterIcon",
     By.XPath("//div[text()='Trust Disbursement Finance Approval - Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//mat-icon[text()='filter_list']"));

        public static IWebLocator CompletedDateSortDisbursementFinanceApproval => L(
        "CompletedDateSortDisbursementFinanceApproval",
        By.XPath("//div[text()='Trust Disbursement Finance Approval - Approval ']//ancestor::mat-expansion-panel-header/following-sibling::div//span[text()='Completed Date']"));

        public static IWebLocator TrustDisbursementRequestFinanceApprovalInput => L(
        "TrustDisbursementRequestFinanceApprovalInput",
        By.XPath("//div[text()='Trust Disbursement Finance Approval - Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//input"));



        public static IWebLocator TrustDisbursementGenericOpenButton(string sectionName, string amount, string transDate) => L(
        "TrustReceiptGenericOpenButton",
        By.XPath("//div[contains(text(),'" + sectionName + "')]/ancestor::mat-expansion-panel//span[contains(text(),'Open') and ancestor::div[@role='row' and @row-index=//div[text()='" + amount + "' and (following-sibling::div[text()='" + transDate + "'] or preceding-sibling::div[text()='" + transDate + "'])]/ancestor::div/@row-index]]"));

        public static IWebLocator CheckSelectbox => L(
     "CheckSelectbox",
     By.XPath("//span//i[text()='check_box_outline_blank']"));
    }
}
