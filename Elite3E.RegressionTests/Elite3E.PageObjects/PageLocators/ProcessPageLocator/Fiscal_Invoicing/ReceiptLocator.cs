using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing
{
    using static Boa.Constrictor.WebDriver.WebLocator;

    public class ReceiptLocator
    {
        public static IWebLocator ReceiptType => L(
            "ReceiptType",
            By.XPath("//input[contains(@name,'ReceiptType')]"));

        public static IWebLocator DocumentNumber => L(
            "CheckBox",
            By.XPath("//input[contains(@name,'DocNumber')]"));

        public static IWebLocator Payer => L(
            "Payor",
            By.XPath("//span[text()='Payer']//ancestor::e3e-bound-input//input"));


        public static IWebLocator ButtonContainer => L(
            "ButtonContainer",
            By.XPath("//e3e-form-anchor-view-header[div/span[contains(text(),'Invoices')]]"));        

        public static IWebLocator SelectFirstRow => L(
            "SelectFirstRow",
            By.CssSelector("e3e-query-grid div[ref='eCenterContainer'] div[row-id='0']"));

        public static IWebLocator ReceiptAmount => L(
            "ReceiptAmount",
            By.XPath("//input[contains(@name,'RcptAmt')]"));

        public static IWebLocator InvoiceReceiptAmount => L(
            "InvoiceReceiptAmount",
            By.XPath("//input[contains(@name,'InvCurrRcptAmt')]"));

        public static IWebLocator TotalAppliedDiv => L(
            "TotalAppliedDiv",
            By.XPath("//div[contains(@name,'totPmtApplied')]"));

        public static IWebLocator TotalAppliedButton => L(
            "TotalAppliedButton",
            By.XPath("//button[span[contains(text(),'Total Applied')]]"));

        public static IWebLocator OperatingUnit => L(
            "OperatingUnit",
            By.XPath("//input[contains(@name,'process-folder-unit')]"));

        public static IWebLocator Narrative => L(
            "OperatingUnit",
            By.CssSelector("div.ql-editor.ql-blank p"));

        public static IWebLocator UpdateButton => L(
            "TotalAppliedButton",
            By.XPath("//button[span[contains(text(),'Update')]]"));

        public static IWebLocator ReceiptDate => L(
            "TotalAppliedButton",
            By.XPath("//input[contains(@name,'RcptDate')]"));

        public static IWebLocator SearchInput => L("Search Textbox", By.CssSelector("input.mat-input-element.mat-form-field-autofill-control.cdk-text-field-autofill-monitored.ng-pristine.ng-valid.ng-touched"));

        public static IWebLocator UnallocatedButton => L(
                "UnallocatedButton",
        By.XPath("//span[text()=' Unallocated ']"));

        public static IWebLocator AddChildFormButton => L(
                "AddChildFormButton",
        By.XPath("//button[text()=' Add ']"));

        public static IWebLocator UnallocatedType => L(
                "UnallocatedType",
        By.XPath("//input[contains(@name, 'UnalType')]"));

        public static IWebLocator Matter => L(
                "Matter",
        By.XPath("//input[contains(@name, 'Matter')]"));

        public static IWebLocator UnallocatedReceiptAmount => L(
            "UnallocatedReceiptAmount",
            By.XPath("//span[text()='Unallocated ']/../../..//input[contains(@name, 'RcptAmt')]"));

        public static IWebLocator DropDownSelection(string item) => L(
            "DropDownSelection",
            By.XPath("//span[text()='" + item + "']"));


        public static IWebLocator DepositNumber => L(
           "DepositNumber",
           By.XPath("//input[contains(@name,'/DepositNumber')]"));

        public static IWebLocator AlowShortPayCheckbox => L(
        "AlowShortPayCheckbox",
        By.XPath("//div[contains(@data-automation-id,'IsAllowShortPay_BOUND')]//mat-checkbox"));

        public static IWebLocator PresCurrency => L(
        "PresCurrency",
        By.XPath("//div[contains(@name,'ProfMasterRel_ccc.PresCurrency_ccc')]"));

        public static IWebLocator PresExchangeRate=> L(
        "PresExchangeRate",
        By.XPath("//div[contains(@name,'ProfMasterRel_ccc.PresExchangeRate_ccc')]"));

        public static IWebLocator PresInvoiceAmount => L(
        "PresInvoiceAmount",
        By.XPath("//div[contains(@name,'ProfMasterRel_ccc.PresInvoiceAmount_ccc')]"));
    }
}
