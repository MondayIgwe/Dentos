using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountReceiptRequest
{
    public class ClientAccountReceiptRequestLocators
    {
        public static IWebLocator ClientAccountReceiptDetailForm => L(
            "ClientAccountReceiptDetailForm",
            By.XPath("//span[contains(text(),'Client Account Receipt Detail')]"));

        public static IWebLocator ClientAccountReceiptApprovalForm => L(
           "ClientAccountReceiptRequestForm",
           By.XPath("//span[text()='Client Account Receipt Approval']"));

        public static IWebLocator ClientAccountReceiptFinanceForm => L(
           "ClientAccountReceiptRequestForm",
           By.XPath("//span[text()='Client Account Receipt Finance']"));

        public static IWebLocator ClientAccountReceiptSection => L(
            "ClientAccountReceiptSection",
            By.XPath("//div[@title='Client Account Receipt' and text()='Client Account Receipt']"));

        public static IWebLocator ClientAccountTransferSection => L(
    "ClientAccountTransferSection",
    By.XPath("//div[@title='Client Account Transfer' and text()='Client Account Transfer']"));

        public static IWebLocator ClientAccountReceiptApprovalRecord => L(
          "ClientAccountReceiptApprovalRecord",
          By.XPath("//mat-panel-title//div[text()='Trust Receipt Request Entry ']//following::e3e-workflow-inbox-grid-actions//div//span[text()=' Open ']"));

        public static IWebLocator ClientAccountReceiptFilterIcon => L(
         "ClientAccountReceiptFilterIcon",
         By.XPath("//mat-panel-title//div[text()='Trust Receipt Request Entry ']//following::mat-icon[text()='filter_list']"));

        public static IWebLocator ClientAccountReceiptFilterInput => L(
         "ClientAccountReceiptFilterInput",
         By.XPath("//div[text()='Trust Receipt Request Entry ']//following::input"));

        public static IWebLocator ClientAccountReceiptBillingTimeKeeperFilterIcon => L(
        "ClientAccountReceiptBillingTimeKeeperFilterIcon",
        By.XPath("//mat-panel-title//div[text()='Trust Receipt Request Entry - Bill Tkpr Approval Required ']//following::mat-icon[text()='filter_list']"));
       
        public static IWebLocator ClientAccountReceiptBillingTimeKeeperInput => L(
        "ClientAccountReceiptFilterInput",
        By.XPath("//div[text()='Trust Receipt Request Entry - Bill Tkpr Approval Required ']//following::input"));
       
        public static IWebLocator ClientAccountReceiptFinanceApprovalFilterIcon => L(
       "ClientAccountReceiptFinanceApprovalFilterIcon",
       By.XPath("//mat-panel-title//div[text()='Trust Receipt Approval - Finance Approval ']//following::mat-icon[text()='filter_list']"));

        public static IWebLocator ClientAccountReceiptFinanceApprovalInput => L(
       "ClientAccountReceiptFilterInput",
       By.XPath("//div[text()='Trust Receipt Approval - Finance Approval ']//following::input"));

        public static IWebLocator ClientAccountReceiptRecordOpenButton => L(
          "ClientAccountReceiptRecordOpenButton",
          By.XPath("//e3e-workflow-inbox-grid-actions//button//span[text()=' Open ']"));

        public static IWebLocator ClientReceiptFinanceApprovalOpenButton => L(
          "ClientAccountReceiptRecordOpenButton",
          By.XPath("//mat-panel-title//div[contains(text(),'Finance Approval ')]//following::e3e-workflow-inbox-grid-actions//span[text()=' Open ']"));

        public static IWebLocator TransactionDate => L(
          "TransactionDate",
          By.XPath("//input[contains(@data-automation-id,'TranDate')]"));

        public static IWebLocator Amount => L(
        "Amount",
        By.XPath("//input[contains(@data-automation-id,'Amount')]"));

        public static IWebLocator MatterInput => L(
        "MatterInput",
        By.XPath("//input[contains(@data-automation-id,'Matter')]"));

        public static IWebLocator IntendedUse => L(
        "IntendedUse",
        By.XPath("//input[contains(@data-automation-id,'TrustIntendedUse')]"));

        public static IWebLocator ApprovedCheckbox => L(
      "ApprovedCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsApproved_ccc')]"));

        public static IWebLocator ApprovedCheckboxChecked => L(
     "ApprovedCheckboxChecked",
     By.XPath("//mat-checkbox[contains(@data-automation-id,'IsApproved_ccc')]//input[@aria-checked='true']"));

        public static IWebLocator ClientAccountReceiptDetailCard => L(
      "ClientAccountReceiptDetailCard",
      By.XPath("//mat-card[contains(text(),'Receipt Detail')]"));

        public static IWebLocator AMLChecksCompleteCheckbox => L(
      "AMLChecksCompleteCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsAMLChecksComplete_ccc')]"));

        public static IWebLocator ClientReceiptRequiredCheckbox => L(
      "ClientReceiptRequiredCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsCliRcptRequired_ccc')]//input"));

        public static IWebLocator Narrative => L("Narrative Text Box",
           By.XPath("//e3e-narrative-input//quill-editor//ancestor::e3e-bound-input//div[@contenteditable]/p"));

        public static IWebLocator NarrativeTextArea => L("NarrativeTextArea",
           By.XPath("//e3e-narrative-input//div[@class='ql-editor ql-blank']"));

        public static IWebLocator ReasonCommentInput => L("ReasonCommentInput",
        By.XPath("//textarea[contains(@data-automation-id,'Comment')]"));

        public static IWebLocator ConfirmCheckbox => L(
    "ConfirmCheckbox in Approval",
    By.XPath("//div[@col-id='IsSelected']//i[contains(text(), 'check_box_outline_blank')]"));

        public static IWebLocator TrustReceiptFinanceApprovalFilterIcon => L(
      "TrustReceiptFinanceApprovalFilterIcon",
      By.XPath("//div[text()='Trust Receipt Finance Approval - Request Entry ']/parent::mat-panel-title/following-sibling::mat-panel-description//mat-icon[text()='filter_list']"));

        public static IWebLocator CompletedDateSortReceiptRequest => L(
        "CompletedDateSortReceiptRequest",
        By.XPath("//div[text()='Trust Receipt Finance Approval - Request Entry ']//ancestor::mat-expansion-panel-header/following-sibling::div//span[text()='Completed Date']"));

        public static IWebLocator TrustReceiptFinanceInput => L(
        "TrustReceiptFinanceInput",
        By.XPath("//div[text()='Trust Receipt Finance Approval - Request Entry ']/parent::mat-panel-title/following-sibling::mat-panel-description//input"));


        public static IWebLocator TrustReceiptGenericOpenButton(string sectionName, string amount, string transDate) => L(
        "TrustReceiptGenericOpenButton",
        By.XPath("//div[contains(text(),'" + sectionName + "')]/ancestor::mat-expansion-panel//span[contains(text(),'Open') and ancestor::div[@role='row' and @row-index=//div[text()='" + amount + "' and (following-sibling::div[text()='" + transDate + "'] or preceding-sibling::div[text()='" + transDate + "'])]/ancestor::div/@row-index]]"));

        public static IWebLocator TrustReceiptEntryFinanceApprovalFilterIcon => L(
     "TrustReceiptEntryFinanceApprovalFilterIcon",
     By.XPath("//div[text()='Trust Receipt Request Entry - Finance Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//mat-icon[text()='filter_list']"));
        public static IWebLocator TrustReceiptTransactionDateSortReceiptRequest => L(
        "TrustReceiptCompletedDateSortReceiptRequest",
        By.XPath("//div[text()='Trust Receipt Request Entry - Finance Approval ']//ancestor::mat-expansion-panel-header/following-sibling::div//span[text()='Transaction Date']"));

        public static IWebLocator TrustReceiptEntryFinanceInput => L(
        "TrustReceiptEntryFinanceInput",
        By.XPath("//div[text()='Trust Receipt Request Entry - Finance Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//input"));
    }
}
