using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ProformaTrust
{
    public static class ProformaTrustLocators
    {
        public static IWebLocator Update => L(
           "Update",
           By.XPath(" //span[contains(text(),'Update')]"));

        public static IWebLocator AddChildFormMenu => L(
            "AddChildFormMenu",
        By.XPath("//button[text()=' Add ']"));

        public static IWebLocator Select => L(
            "Select",
         By.XPath("//e3e-dialog-content/descendant::span[contains(text(),'Select')]"));

        public static IWebLocator SetAllowForBillingCheckbox => L(
            "AllowForBillingCheckbox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsAllowBilling_ccc')]/label/div"));
        public static IWebLocator SelectAutoRecord => L(
            "Select Auto Record",
        By.XPath("//e3e-dialog-content/descendant::i[contains(@class,'material-icons')][contains(text(),'check_box_outline_blank')]"));

        public static IWebLocator GetIsAllowDisbursement => L(
            "AllowDisbursement",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsAllowDisbursement')]/label/div"));
        public static IWebLocator GetAllowForBillingCheckbox => L(
           "AllowForBillingCheckbox",
           By.XPath("//mat-checkbox[contains(@data-automation-id,'IsAllowBilling_ccc')]/label/div/input"));

        public static IWebLocator SetAllowDisbursement => L(
            "AllowDisbursement",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsAllowDisbursement')]/label/div/input"));
        public static IWebLocator Office => L(
            "Office",
          By.XPath("//div[contains(@data-automation-id,'Office_BOUND')]//input[contains(@data-automation-id, 'Office')]"));

        public static IWebLocator ApplyClientAccountMenu => L(
            "Apply Client account menu",
        By.XPath("//span[text()=' Apply Client Account ']"));

        public static IWebLocator AdjustmentType => L(
           "AdjustmentType",
        By.XPath("//input[contains(@data-automation-id,'TrustAdjustmentType')]"));

        public static IWebLocator TrustDisbursementType => L(
            "TrustDisbursementType",
        By.XPath("//input[contains(@name,'TrustDisbursementType')]"));

        public static IWebLocator TimeKeeperLeaver => L(
           "TimeKeeperLeaver",
        By.XPath("//input[contains(@name,'FinanceClerk')]"));

        public static IWebLocator ClientAccountReceiptTypeDefault => L(
          "ClientAccountReceiptTypeDefault",
        By.XPath("//input[contains(@data-automation-id,'TrustReceiptType')]"));

        public static IWebLocator ClientAccountReceiptApprovalRequiredCheckbox => L(
       "ClientAccountReceiptApprovalRequiredCheckbox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsTrustRcptApprovalRequired')]//input"));

        public static IWebLocator ClientAccountReceiptApprovalRequiredCheckboxChecked => L(
       "ClientAccountReceiptApprovalRequiredCheckboxChecked",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsTrustRcptApprovalRequired')]//input[@aria-checked='true']"));

        public static IWebLocator ClientAccountAcctDefault => L(
          "ClientAccountAcctDefault",
       By.XPath("//input[contains(@data-automation-id,'BankAcctTrust')]"));

        public static IWebLocator Payee => L(
            "Payee",
        By.XPath("//input[contains(@name,'Payee')]"));
        public static IWebLocator DaysToDispatch => L(
            "DaysToDispatch",
        By.XPath("//input[contains(@name,'DaysToDispatch')]"));

        public static IWebLocator TrustIntendedUse => L(
            "TrustIntendedUse",
        By.XPath("//input[contains(@name,'TrustIntendedUse')]"));

        public static IWebLocator Reload => L(
            "Reload",
        By.XPath("//span[contains(text(),'Reload')]"));


        public static IWebLocator Code => L(
            "Code",
        By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator Description => L(
          "Description",
      By.XPath("//input[contains(@name,'Description')]"));


        public static IWebLocator TrustReceiptType => L(
          "TrustReceiptType",
      By.XPath("//input[contains(@name,'TrustReceiptType')]"));

        public static IWebLocator ClientAccountReceiptComments => L(
            "TrustReceiptType",
            By.XPath("//textarea[contains(@data-automation-id,'Comment')]"));

        public static IWebLocator BankAcctTrust => L(
          "BankAcctTrust",
      By.XPath("//input[contains(@name,'BankAcctTrust')]"));

        public static IWebLocator ClientAccReceiptDetForm => L(
          "ClientAccReceiptDetForm",
      By.XPath("//h5[contains(text(),'Client Account Receipt Detail')]"));

        public static IWebLocator Matter => L(
          "Matter",
      By.XPath("//input[contains(@name,'Matter')]"));

        public static IWebLocator Amount => L(
          "Amount",
      By.XPath("//input[contains(@name,'Amount')]"));

        public static IWebLocator IntendedUse => L(
          "IntendedUse",
      By.XPath("//input[contains(@name,'TrustIntendedUse')]"));
        public static IWebLocator PayeeInput => L(
         "Payee",
     By.XPath("//input[contains(@data-automation-id,'Payee')]"));

        public static IWebLocator BillingGroup => L(
          "BillingGroup",
      By.XPath("//input[contains(@name,'BillingGroup')]"));

        public static IWebLocator Officegroup => L(
          "Office",
      By.XPath("//input[contains(@name,'Office')]"));

        public static IWebLocator ProfDate => L(
          "ProfDate",
      By.XPath("//input[contains(@name,'ProfDate')]"));

        public static IWebLocator Form => L(
         "Form",
        By.XPath("//button[span[contains(text(),'Form')]]"));

        public static IWebLocator CloseAlert => L(
            "CloseAlert",
            By.XPath("//mat-icon[@class='close-alert mat-icon notranslate material-icons mat-icon-no-color']"));
        public static IWebLocator UseDetails => L(
       "UseDetails",
      By.XPath("//input[contains(@data-automation-id,'TrustIntendedUseDet')]"));

        public static IWebLocator DaysToClear => L(
       "DaysToClear",
      By.XPath("//input[contains(@name,'/ClearDays')]"));

        public static IWebLocator ClientAccountReceiptDetail => L(
        "ClientAccountReceiptDetail",
            By.XPath("//mat-card[contains(text(),'Client Account Receipt Detail')]"));

        public static IWebLocator DrawnBy => L(
        "Drawn By",
            By.XPath("//input[contains(@name,'/attributes/DrawnBy')]"));

        public static IWebLocator ProformerApproverCheckbox => L(
        "ProformerApproverCheckbox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsApproverCannotBill')]"));

        public static IWebLocator ProformerApproverChecked => L(
        "ProformerApproverChecked",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsApproverCannotBill')]//input[@aria-checked='true']"));
    }
}
