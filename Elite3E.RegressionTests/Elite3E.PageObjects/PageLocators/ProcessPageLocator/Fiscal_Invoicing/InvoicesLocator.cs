using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing
{
    using static Boa.Constrictor.WebDriver.WebLocator;

    public class InvoicesLocator
    {
        public static IWebLocator SetFullCreditNote => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsFullCreditNote')]/label/div"));

        public static IWebLocator InvNum => L(
          "InvNum",
      By.XPath("//div[contains(@name,'attributes/InvNumber')]"));

        public static IWebLocator GetFullCreditNote => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsFullCreditNote')]/label/div/input"));

        public static IWebLocator SetPrintToScreen => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'Option-PrintToScreen')]/label/div"));

        public static IWebLocator GetPrintToScreen => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'Option-PrintToScreen')]/label/div/input"));

        public static IWebLocator FirstRowInGrid => L(
           "FirstRowInGrid",
          By.XPath("//div[@row-id='0']//div [@col-id= 'selected']"));

        public static IWebLocator SearchIcon => L(
          "SearchIcon",
     By.XPath("//span[contains(text(),'SEARCH')]"));

        public static IWebLocator SelectBtn => L(
        "SelectBtn",
      By.XPath("//span[contains(text(),' SELECT ') and  not (contains(text(),' SELECT ALL '))]"));

        public static IWebLocator GridTotalAmount => L(
            "GridTotalAmount",
            By.XPath("(//div[@role='gridcell'][@col-id='OrgAmt'])[last()]"));

        public static IWebLocator GetPaid => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsPaid')]/label/div/input"));
        public static IWebLocator BillingOfficeInput => L(
           "BillingOfficeInput",
           By.XPath("//input[contains(@name,'BillOfficeInvMgr')]"));
        public static IWebLocator InvoiceManagerResultsCheckbox => L(
           "InvoiceManagerResultsCheckbox",
           By.XPath("//span//i[contains(text(),'check_box_outline_blank')]"));

        public static IWebLocator TaxInvoiceNumber => L(
            "OperatingUnit",
            By.XPath("//div[contains(@name,'TaxInvNumber')]"));

        public static IWebLocator InvoiceDetailsTab => L(
            "InvoiceDetailsTab",
            By.XPath("//mat-card[contains(text(),'Invoice Detail')]"));

        public static IWebLocator InvoiceDetailsFees => L(
            "InvoiceDetailsFees",
            By.XPath("//div[contains(@name,'ARFee')]"));

        public static IWebLocator InvoiceDetailsSoftDisbursement => L(
            "InvoiceDetailsFees",
            By.XPath("//div[contains(@name,'ARSCo')]"));

        public static IWebLocator InvoiceDetailsTaxes => L(
            "InvoiceDetailsTaxes",
            By.XPath("//div[contains(@name,'ARTax')]"));

        public static IWebLocator CloseChildFormButton => L(
            "CloseChildFormButton",
            By.CssSelector("button.child-form-tabs-btn.child-form-close-btn.ng-star-inserted.active"));

        public static IWebLocator GlPostingsButton => L(
            "GlPostingsButton",
            By.XPath("//button[span[contains(text(),'GL Postings')]]"));

        public static IWebLocator CloseGlPostingsButton => L(
            "GlPostingsButton",
            By.XPath("//button[span[contains(text(),'Close')]]"));

        public static IWebLocator InvoiceMasterProcess => L(
          "InvoiceMasterProcess",
          By.XPath("//span//em[text()=' (InvMaster)']"));

        public static IWebLocator InputReasonType => L(
            "GlPostingsButton",
            By.XPath("//input[contains(@name,'ReasonType')]"));

        public static IWebLocator GLPostingsGridAmount(string amount) => L(
            "GlPostingsGridamount",
            By.XPath("//div[contains(@col-id,'Amount') and contains(@class,'edit')]//span[@title and text()='"+amount+"']"));

        public static IWebLocator GlPostingsStatus => L(
           "GlPostingsStatus",
           By.XPath("//span[text()='Status']//ancestor::e3e-bound-input//div[contains(@name,'JMStatusList')]"));

        public static IWebLocator JournalManager => L(
          "JournalManager",
          By.XPath("//span[text()='Journal Manager']/parent::label/following-sibling::*//div[text()]"));


        public static IWebLocator PostingsInformationHeader => L(
            "PostingsInformationHeader",
            By.XPath("//div[contains(text(),'Posting Information')][contains(@class,'label')]"));

        public static IWebLocator LeadMatter => L(
            "LeadMatter",
            By.XPath("//div[contains(@data-automation-id,'LeadMatter_BOUND')]//div[contains(@name,'LeadMatter')]"));

        public static IWebLocator InvoiceDetail => L(
       "InvoiceDetail",
           By.XPath("//mat-card[contains(text(),'Invoice Detail')]"));

        public static IWebLocator PayerDetail => L(
       "PayerDetail",
           By.XPath("//mat-card[contains(text(),'Payer Detail')]"));

        public static IWebLocator EBillingHistory => L(
       "EBillingHistory",
           By.XPath("//mat-card[contains(text(),'E-Billing History')]"));

        public static IWebLocator ProformaList => L(
       "ProformaList",
           By.XPath("//mat-card[contains(text(),'Proforma List')]"));

        public static IWebLocator Time => L(
       "Time",
           By.XPath("//mat-card[contains(text(),'Time')]"));


        public static IWebLocator Disbursement => L(
       "Disbursement",
           By.XPath("//mat-card[contains(text(),'Disbursement')]"));

        public static IWebLocator Charge => L(
       "Charge",
           By.XPath("//mat-card[contains(text(),'Charge')]"));

        public static IWebLocator CreditNoteTaxArticle => L(
       "CreditNoteTaxArticle",
           By.XPath("//mat-card[contains(text(),'Credit Note Tax Article')]"));

        public static IWebLocator SetSaveToLocal => L(
            "SetSaveToLocal",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'Option-SaveToLocal')]/label/div"));

        public static IWebLocator GetSaveToLocal => L(
            "GetSaveToLocal",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'Option-SaveToLocal')]/label/div/input"));

        public static IWebLocator InvoiceType => L(
            "InvoiceType",
            By.XPath("//div/div[contains(@Data-automation-id,'InvoiceType_ccc')]"));
    }

}
