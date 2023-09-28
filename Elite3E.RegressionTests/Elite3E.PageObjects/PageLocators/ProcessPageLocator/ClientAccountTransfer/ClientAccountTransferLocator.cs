using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountTransfer
{
    public class ClientAccountTransferLocator
    {
        public static IWebLocator TransferType => L(
        "Transfer Type",
        By.XPath("//input[contains(@name, '/TrustTransferType')]"));

        public static IWebLocator ReverseCheckbox => L(
     "ReverseCheckbox",
     By.XPath("//mat-checkbox[contains(@data-automation-id,'IsReversed')]"));

        public static IWebLocator TransferDocumentNumber => L(
        "Transfer Number",
        By.XPath("//input[contains(@name, '/DocumentNumber')]"));

        public static IWebLocator FromAccount => L(
        "From Account",
        By.XPath("//input[contains(@name, '/TrustTransferDetFrom') and contains(@name, '/BankAcctTrust')]"));

        public static IWebLocator FromMatter => L(
        "From Matter",
        By.XPath("//input[contains(@name, '/TrustTransferDetFrom') and contains(@name,'/attributes/Matter')]"));

        public static IWebLocator FromMatterName => L(
    "From Matter Name",
    By.XPath("//div[contains(@data-automation-id,'TrustTransferDetFrom')]//div[contains(@name, 'WF_TrustTransfer_ccc') and contains(@name,'Matter1.DisplayName')]"));

        public static IWebLocator FromIntendedUse => L(
        "From IntendedUse",
        By.XPath("//input[contains(@name, '/TrustTransferDetFrom') and contains(@name,'/TrustIntendedUse')]"));

        public static IWebLocator FromAmount => L(
        "From Amount",
        By.XPath("//input[contains(@name, '/TrustTransferDetFrom') and contains(@name,'/Amount')]"));

        public static IWebLocator ToAccount => L(
            "To Account",
            By.XPath("//input[contains(@name, '/TrustTransferDetTo') and contains(@name, '/BankAcctTrust')]"));

        public static IWebLocator ToMatter => L(
        "To Matter",
        By.XPath("//input[contains(@name, '/TrustTransferDetTo') and contains(@name,'/attributes/Matter')]"));

        public static IWebLocator ToMatterName => L(
    "To Matter name",
    By.XPath("//div[contains(@data-automation-id,'TrustTransferDetTo')]//div[contains(@name, 'WF_TrustTransfer_ccc') and contains(@name,'Matter1.DisplayName')]"));

        public static IWebLocator ToIntendedUse => L(
       "To IntendedUse",
       By.XPath("//input[contains(@name, '/TrustTransferDetTo') and contains(@name,'/TrustIntendedUse')]"));

        public static IWebLocator ToAmount => L(
        "To Amount",
        By.XPath("//input[contains(@name, '/TrustTransferDetFrom') and contains(@name,'/Amount')]"));

        public static IWebLocator ClientAccountTransferFilterIcon => L(
        "ClientAccountTransferFilterIcon",
        By.XPath("//mat-panel-title//div[text()='Trust Transfer Request Entry - Finance Approval ']//following::mat-icon[text()='filter_list']"));

        public static IWebLocator TrustTransferFilterIcon => L(
        "TrustTransferFilterIcon",
        By.XPath("//div[text()='Trust Transfer Approval - Finance Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//mat-icon[text()='filter_list']"));

        public static IWebLocator TrustTransferFinanceApprovalFilterIcon => L(
"TrustTransferFilterIcon",
By.XPath("//div[text()='Trust Transfer Finance Approval - Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//mat-icon[text()='filter_list']"));


        public static IWebLocator ClientAccountTransferInput => L(
       "ClientAccountTransferInput",
        By.XPath("//div[text()='Trust Transfer Request Entry - Finance Approval ']//following::input"));

        public static IWebLocator TrustTransferInput => L(
"TrustTransferInput",
By.XPath("//div[text()='Trust Transfer Approval - Finance Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//input"));

        public static IWebLocator TrustTransferFinanceApprovalInput => L(
"TrustTransferFinanceApprovalInput",
By.XPath("//div[text()='Trust Transfer Finance Approval - Approval ']/parent::mat-panel-title/following-sibling::mat-panel-description//input"));


        public static IWebLocator ClientAccountTransferOpenButton => L(
  "ClientAccountTransferOpenButton",
  By.XPath("//div[text()='Trust Transfer Request Entry - Finance Approval ']/parent::mat-panel-title//following::e3e-workflow-inbox-grid-actions//button//span[text()=' Open ']"));

        public static IWebLocator TrustTransferOpenButton => L(
  "TrustTransferOpenButton",
  By.XPath("//div[text()='Trust Transfer Approval - Finance Approval ']/parent::mat-panel-title//following::e3e-workflow-inbox-grid-actions//button//span[text()=' Open ']"));

        public static IWebLocator TrustTransferFinanceApprovalOpenButton => L(
"TrustTransferFinanceApprovalOpenButton",
By.XPath("//div[text()='Trust Transfer Finance Approval - Approval ']//ancestor::mat-expansion-panel-header/following-sibling::div//button//span[text()=' Open ']"));

        public static IWebLocator TrustTransferApprovalFinanceApprovalOpenButton => L(
"TrustTransferApprovalFinanceApprovalOpenButton",
By.XPath("//div[text()='Trust Transfer Approval - Finance Approval ']//ancestor::mat-expansion-panel-header/following-sibling::div//button//span[text()=' Open ']"));


        public static IWebLocator ApprovalFromMatterNumber => L(
  "ApprovalFromMatterNumber",
  By.XPath("//div[contains(@data-automation-id,'TrustTransferDetFrom') and contains(@data-automation-id,'/Matter_BOUND')]//div[contains(@name,'/attributes/Matter')]"));

        public static IWebLocator ApprovalToMatterNumber => L(
  "ApprovalToMatterNumber",
  By.XPath("//div[contains(@data-automation-id,'TrustTransferDetTo') and contains(@data-automation-id,'/Matter_BOUND')]//div[contains(@name,'/attributes/Matter')]"));

        public static IWebLocator WorkflowChildForm => L(
      "WorkflowChildForm",
      By.XPath("//mat-card[contains(text(),'Workflow History')]"));
        public static IWebLocator TransferFromApprovedCheckbox => L(
      "TransferFromApprovedCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsApproved_ccc')]//input[@aria-checked='true']"));
        public static IWebLocator ApprovalRequiredInput => L(
       "ApprovalRequiredInput",
       By.XPath("//input[contains(@data-automation-id,'ApprovalBy_ccc')]"));
        public static IWebLocator CompletedDateSortTransferRequest => L(
      "CompletedDateSortTransferRequest",
      By.XPath("//div[text()='Trust Transfer Request Entry - Finance Approval ']//ancestor::mat-expansion-panel-header/following-sibling::div//span[text()='Completed Date']"));

        public static IWebLocator CompletedDateSortFinanceTransferRequest => L(
       "CompletedDateSortFinanceTransferRequest",
        By.XPath("//div[text()='Trust Transfer Approval - Finance Approval ']//ancestor::mat-expansion-panel-header/following-sibling::div//span[text()='Completed Date']"));

        public static IWebLocator ApprovedBy => L(
       "Approved By",
       By.XPath("//input[contains(@name,'/ApprovalBy_ccc')]"));

        public static IWebLocator OpenButton => L(
        "OpenButton",
        By.XPath("//div[@class='filter-container action-active']//following::e3e-workflow-inbox-grid-actions//button//span[text()=' Open ']"));

        public static IWebLocator CompletedDateSortTransferRequest2 => L(
        "CompletedDateSortTransferRequest",
        By.XPath("//div[@class='filter-container action-active']//ancestor::mat-expansion-panel-header/following-sibling::div//span[text()='Completed Date']"));

        public static IWebLocator CompletedDateSortTrustTransferFinanceApprovalRequest => L(
"CompletedDateSortTrustTransferFinanceApprovalRequest",
By.XPath("//div[text()='Trust Transfer Finance Approval - Approval ']//ancestor::mat-expansion-panel-header/following-sibling::div//span[text()='Completed Date']"));


        public static IWebLocator ClientAccountTransferGenericOpenButton(string sectionName, string amount, string transDate) => L(
"ClientAccountTransferFilterIcon",
By.XPath("//div[contains(text(),'" + sectionName + "')]/ancestor::mat-expansion-panel//span[contains(text(),'Open') and ancestor::div[@role='row' and @row-index=//div[text()='" + amount + "' and (following-sibling::div[text()='" + transDate + "'] or preceding-sibling::div[text()='" + transDate + "'])]/ancestor::div/@row-index]]"));



    }
}
