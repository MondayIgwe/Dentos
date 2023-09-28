using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma
{
    public class ProformaEditLocator
    {
        public static IWebLocator SetCreateFiscalInvoice => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsFiscalInvoice_ccc')]//div[@class='mat-checkbox-frame']"));

        public static IWebLocator GetCreateFiscalInvoice => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsFiscalInvoice_ccc')]/label/div/input"));

        public static IWebLocator SetFullCreditNote => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsFullCreditNote')]/label/div"));

        public static IWebLocator GetFullCreditNote => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsFullCreditNote')]/label/div/input"));

        public static IWebLocator CloseChildFormButton => L(
            "CloseChildFormButton",
            By.CssSelector("button.child-form-tabs-btn.child-form-close-btn.ng-star-inserted.active"));

        public static IWebLocator UpdateButton => L(
            "ChargeModifyUpdateButton",
            By.XPath("//button[span[contains(text(),'Update')]]"));

        public static IWebLocator CreateMatterNoteCheckbox => L(
           "CreateMatterNoteCheckbox",
           By.XPath("//mat-checkbox[contains(@data-automation-id,'IsCreateMatterNote_ccc')]//input[@aria-checked='true']"));

        public static IWebLocator LeadMatter => L(
            "LeadMatter",
            By.XPath("//span[text()='LeadMatter']/parent::label/following-sibling::div//div[contains(@data-automation-id,'/LeadMatter')]"));

        public static IWebLocator ProformaTotal => L(
       "ProformaTotal",
       By.XPath("//mat-card[contains(text(),'Proforma Total')]"));

        public static IWebLocator ProformaPayer => L(
       "ProformaPayer",
       By.XPath("//mat-card[contains(text(),'Proforma Payer')]"));
        public static IWebLocator ProformaPayerLayer => L(
       "ProformaPayerLayer",
       By.XPath("//mat-card[contains(text(),'Proforma Payer by Layer')]"));
        public static IWebLocator ApplyAdjustment => L(
       "ApplyAdjustment",
       By.XPath("//mat-card[contains(text(),'Apply Adjustment')]"));
        public static IWebLocator FeeDetails => L(
       "FeeDetails",
       By.XPath("//mat-card[contains(text(),'Fee Details')]"));
        public static IWebLocator PresentationParagraph => L(
       "ProformaTotal",
       By.XPath("//mat-card[contains(text(),'Presentation Paragraph')]"));
        public static IWebLocator DisbursementDetails => L(
       "DisbursementDetails",
       By.XPath("//mat-card[contains(text(),'Disbursement Details')]"));
        public static IWebLocator ChargeDetails => L(
       "ChargeDetails",
       By.XPath("//mat-card[contains(text(),'Charge Details')]"));
        public static IWebLocator ApplyClientAccount => L(
       "ApplyClientAccount",
       By.XPath("//mat-card[contains(text(),'Apply Client Account')]"));
        public static IWebLocator ApplyUnallocated => L(
       "ApplyUnallocated",
       By.XPath("//mat-card[contains(text(),'Apply Unallocated')]"));
        public static IWebLocator ApplyBOA => L(
       "ApplyBOA",
       By.XPath("//mat-card[contains(text(),'Apply BOA')]"));
        public static IWebLocator TemplateOptions => L(
       "TemplateOptions",
       By.XPath("//mat-card[contains(text(),'Template Options')]"));
        public static IWebLocator DateOverrides => L(
       "DateOverrides",
       By.XPath("//mat-card[contains(text(),'Date Overrides')]"));
        public static IWebLocator ProformaTaxArticle => L(
       "ProformaTaxArticle",
       By.XPath("//mat-card[contains(text(),'Proforma Tax Article')]"));
        public static IWebLocator BillingRulesValidationMessages => L(
       "BillingRulesValidationMessages",
       By.XPath("//mat-card[contains(text(),'Billing Rules Validation Messages')]"));
        public static IWebLocator BillingContact => L(
       "BillingContact",
       By.XPath("//mat-card[contains(text(),'Billing Contact')]"));

        public static IWebLocator ProformaDate => L(
     "ProformaDate",
     By.XPath("//input[contains(@name,'ProfDate')]"));

        public static IWebLocator BillFeeEarner => L(
   "BillFeeEarner",
   By.XPath("//input[contains(@name,'BillTkpr')]"));

        public static IWebLocator RespFeeEarner => L(
   "RespFeeEarner",
   By.XPath("//input[contains(@name,'RspTkpr')]"));

        public static IWebLocator SpvFeeEarner => L(
  "SpvFeeEarner",
  By.XPath("//input[contains(@name,'SpvTkpr')]"));

        public static IWebLocator CollFeeEarner => L(
  "CollFeeEarner",
  By.XPath("//input[contains(@name,'CollTkpr')]"));

        public static IWebLocator BillSubjectToClientApprovalDiv => L(
  "BillSubjectToClientApprovalDiv",
  By.XPath("//e3e-readonly-input//div[contains(@data-automation-id,'WarningMsg')]"));

        public static IWebLocator FeeDetailsNoChargeCheckbox => L(
             "FeeDetailsNoChargeCheckbox",
              By.XPath("//div[contains(@data-automation-id,'ProfDetailTime')]//mat-checkbox[contains(@data-automation-id,'IsNoCharge')]"));

        public static IWebLocator FeeDetailsDisplayCheckbox => L(
         "FeeDetailsDisplayCheckbox",
          By.XPath("//div[contains(@data-automation-id,'ProfDetailTime')]//mat-checkbox[contains(@data-automation-id,'IsDisplay')]"));

        public static IWebLocator InvoiceNarrativeText => L(
         "InvoiceNarrativeText",
          By.XPath("//div[contains(@data-automation-id,'InvNarrative')]//div[@class='ql-editor']/p"));

        public static IWebLocator CoverLetterNarrativeText => L(
         "CoverLetterNarrativeText",
          By.XPath("//div[contains(@data-automation-id,'CoverLetterNarrative')]//div[@class='ql-editor']/p"));
        public static IWebLocator InvoiceType => L(
         "InvoiceType",
          By.XPath("//input[contains(@name,'InvoiceType_ccc')]"));

        public static IWebLocator NoteTypeHeader => L(
         "NoteTypeHeader",
          By.XPath("//div[@col-id='NoteType' and @role='columnheader']"));

        public static IWebLocator MatterNotes(string note) => L(
        "MatterNotes",
         By.XPath("//span[@title='" + note + "']/ancestor::div[@role='row']//span[@title='Matter']"));

        public static IWebLocator ClientNotes(string note) => L(
       "ClientNotes",
        By.XPath("//span[@title='" + note + "']/ancestor::div[@role='row']//span[@title='Client']"));

            public static IWebLocator ClientReference => L(
    "ClientReference",
    By.XPath("//input[contains(@data-automation-id,'ClientRef_ccc')]"));

        public static IWebLocator PresCurrency => L(
        "PresCurrency",
        By.XPath("//input[contains(@name,'PresCurrency_ccc')]"));
        public static IWebLocator ProformaLanguage => L(
        "ProformaLanguage",
        By.XPath("//div[contains(@data-automation-id,'WF_ProformaEdit') and contains(@data-automation-id,'/Currency') and (@class='input default')]"));

        public static IWebLocator PresExchangeRate => L(
        "PresExchangeRate",
        By.XPath("//input[contains(@data-automation-id,'PresExchangeRate_ccc')]"));

        public static IWebLocator AlternativeBankDetails => L(
      "AlternativeBankDetails",
      By.XPath("//input[contains(@name,'AlternativeBankDetails_ccc')]"));

        public static IWebLocator InvoiceDistributionMethod => L(
        "InvoiceDistributionMethod",
        By.XPath("//input[contains(@name,'InvDistMethod_ccc')]"));

        public static IWebLocator MatterNextActionOwner(string note) => L(
   "MatterNextActionOwner",
    By.XPath("//span[@title='" + note + "']/ancestor::div[@role='row']//div[@col-id='NextActionOwner']"));

        public static IWebLocator ClientNextActionOwner(string note) => L(
       "ClientNextActionOwner",
        By.XPath("//span[@title='" + note + "']/ancestor::div[@role='row']//div[@col-id='NextActionOwner']"));

        public static IWebLocator MatterNextActionDate(string note) => L(
"MatterNextActionDate",
By.XPath("//span[@title='" + note + "']/ancestor::div[@role='row']//div[@col-id='NextActionDate']"));

        public static IWebLocator ClientNextActionDate(string note) => L(
       "ClientNextActionDate",
        By.XPath("//span[@title='" + note + "']/ancestor::div[@role='row']//div[@col-id='NextActionDate']"));

        public static IWebLocator BillingOffice => L(
         "BillingOffice",
          By.XPath("//input[contains(@name,'BillingOffice')]"));

        public static IWebLocator FromTaxArea => L(
         "FromTaxArea",
          By.XPath("//input[contains(@name,'FromTaxArea')]"));

    }
}
