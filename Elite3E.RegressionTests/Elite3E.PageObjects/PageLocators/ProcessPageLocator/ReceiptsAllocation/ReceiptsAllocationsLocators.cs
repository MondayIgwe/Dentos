using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ReceiptsAllocation
{
    public class ReceiptsAllocationsLocators
    {
        public static IWebLocator Unallocated => L(
         "Unallocated",
        By.XPath("//input[contains(@name, 'UnalType')]"));

        public static IWebLocator GlTypeInput => L(
        "GLTypeInput",
       By.XPath("//input[contains(@data-automation-id,'GLType')]"));

        public static IWebLocator Matter => L(
         "Matter",
        By.XPath("//input[contains(@name, 'Matter')]"));

        public static IWebLocator Amount => L(
         "Amount",
        By.XPath("//input[contains(@name, 'RcptAmt')][not(contains(@name, 'RcptUnallocated'))]"));

        public static IWebLocator GLAmountInput => L(
        "GLAmount Input",
       By.XPath("//input[contains(@name, 'RcptGL') and contains(@data-automation-id,'RcptAmt')]"));

        public static IWebLocator GLAccountInput => L(
       "GLAccount Input",
        By.XPath("//e3e-segments-input//div[contains(@data-automation-id,'GLAcct')]"));

        public static IWebLocator AmountUnallocated => L(
        "AmountUnallocated",
         By.XPath("//input[contains(@name, 'RcptAmt')][contains(@name, 'RcptUnallocated')]"));

        public static IWebLocator RcptDate => L(
        "RcptDate",
        By.XPath("//input[contains(@name, 'RcptDate')]"));

        public static IWebLocator ReceiptType => L(
         "ReceiptType",
         By.XPath("//input[contains(@name, 'ReceiptType')]"));

        public static IWebLocator Currency => L(
         "Currency",
         By.XPath("//input[contains(@name, 'Currency')]"));

        public static IWebLocator RcptAmt => L(
         "RcptAmt",
         By.XPath("//input[contains(@name, 'RcptAmt')]"));

        public static IWebLocator RcptADocNumber => L(
        "DocNumber",
        By.XPath("//input[contains(@name, 'DocNumber')]"));

        public static IWebLocator ReversalCheckbox => L(
      "ReversalCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsReversed')]"));

        public static IWebLocator ReversalAndReallocateCheckbox => L(
        "ReversalAndReallocateCheckbox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsReverseAndReallocate')]"));

        public static IWebLocator ReverseDate => L(
        "ReverseDate",
         By.XPath("//input[contains(@data-automation-id,'ReverseDate')]"));

        public static IWebLocator ReversalReason => L(
       "ReversalReason",
        By.XPath("//input[contains(@data-automation-id,'ReverseReason')]"));

        public static IWebLocator ReversedReceiptDiv(string text) => L(
       "ReversedReceiptDiv",
       By.XPath("//div[@row-id='0']//div[text()='"+text+ "']//following::e3e-boolean-column//mat-icon[text()='done']"));

        public static IWebLocator ReversalComment => L(
       "ReversalComment",
       By.XPath("//e3e-text-input//textarea[contains(@data-automation-id,'ReverseComment')]"));

        public static IWebLocator Payer => L(
         "Payer",
        By.XPath("//input[contains(@name, 'Payor') and not (contains(@name, 'DrawnByPayor')) and not  (contains(@name, 'PayorName'))]"));

        public static IWebLocator Narrative => L(
        "Narrative",
        By.XPath("//span[text()='Narrative']/../..//div[@class='ql-editor ql-blank']"));

        public static IWebLocator OperatingUnit => L(
         "OperatingUnit",
         By.XPath("//input[contains(@name,'process-folder-unit')]"));

        public static IWebLocator MatterNumber => L(
         "MatterNumber",
         By.XPath("//div[contains(@data-automation-id,'/Matter_BOUND')]/div//div[contains(@name,'Matter')]"));

            public static IWebLocator Invoices => L(
          "Invoices",
         By.XPath("//mat-card[contains(text(),'Invoices')]"));
        public static IWebLocator UnallocatedChildForm => L(
         "UnallocatedChildForm",
        By.XPath("//mat-card[contains(text(),'Unallocated')]"));

        public static IWebLocator GeneralLedger => L(
         "GeneralLedger",
        By.XPath("//mat-card[contains(text(),'General Ledger')]"));

        public static IWebLocator ApplyClientAccount => L(
         "ApplyClientAccount",
        By.XPath("//mat-card[contains(text(),'Apply Client Account')]"));

        public static IWebLocator BilledOnAccount => L(
         "BilledOnAccount",
        By.XPath("//mat-card[contains(text(),'Billed on Account')]"));

        public static IWebLocator NarrativeEditor => L(
        "NarrativeEditor",
       By.XPath("//div[contains(@data-automation-id,'/Narrative_BOUND')]"));

        public static IWebLocator DoubtfulARGLNatural => L(
            "DoubtfulARGLNatural",
            By.XPath("//input[contains(@name,'/DoubtfulARGLNatural')]"));

        public static IWebLocator WriteOffCheckbox => L(
            "WriteOffCheckbox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'WriteOff')]"));

        public static IWebLocator DoubtfulWriteOffCheckbox => L(
            "DoubtfulWriteOffCheckbox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsDoubtful')]"));
        
    }
}
