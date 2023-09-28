using System;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess
{
    public class EntryAndModifyProcessLocators
    {
        public static IWebLocator ProcessAddDropdown => L(
            "Process Add Dropdown",
            By.XPath("//span[text()=' Add ']/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator ProcessCloneButton => L(
            "Process Clone Button",
            By.XPath("//span[contains(text(),'Clone')]"));

        public static IWebLocator SearchTextBox => L(
            "Search TextBox",
            By.XPath("//div[@id='pregrid-content']//input[contains(@class, 'mat-input-element')]"));

        public static IWebLocator Matter => L(
           "Matter",
           By.XPath("//input[contains(@name,'Matter')] |"+getDivElementText("/Matter")));

        public static IWebLocator ChargeSearchButton => L(
            "Search Button",
            By.XPath("//span[contains(text(),'SEARCH')]"));

        public static IWebLocator ChargeRadioButton => L(
            "Charge Radio Button",
            By.XPath("//mat-icon[text()='radio_button_unchecked']"));

        public static IWebLocator ChargeSelectButton => L(
            "Select Button",
            By.XPath("//span[contains(text(),'SELECT')]"));

        public static IWebLocator CostMatterField => L(
            "CostMatterField",
            By.XPath("//input[contains(@name, 'Matter')]"));

        public static IWebLocator NextDisbursementEntry => L(
            "NextDisbursementEntry",
            By.XPath("//mat-icon[text()='skip_next']"));

        public static IWebLocator InvoiceNumberField => L(
            "Invoice Number Field",
            By.XPath("//input[contains(@name, 'InvNum')][not(contains(@name, 'FiscalInvNum'))]"));

        public static IWebLocator PostAllButton => L(
            "Post All Button",
            By.XPath("//span[contains(text(),'Post All')]"));

        public static IWebLocator ProcessUpdateButton => L(
            "Process Update Button",
            By.XPath("//span[contains(text(),'Update')]"));

        public static IWebLocator BillablePurgeField => L(
            "Billable Purge Field",
            By.XPath("//input[contains(@name, 'PurgeType')]"));

        public static IWebLocator AddNewButton(string childProcess) => L(
            "Add New Button",
            By.XPath("//span[contains(text(), '" + childProcess + "')]/..//span[contains(text(),'Add New')]"));

        public static IWebLocator ExpandChildProcess(string childProcess) => L(
            "ExpandChildProcess",
            By.XPath("//span[contains(text(), '" + childProcess + "')]/..//mat-icon[text()='expand_more']"));

        public static IWebLocator DisbursementType => L(
            "Disbursement Type",
            By.XPath("//input[contains(@name,'CostType')] | "+ getDivElementText("CostType")));

        public static IWebLocator DisbursementTypeSearchIcon => L(
            "Disbursement Type Search Icon",
            By.XPath("//input[contains(@name,'CostType')]/../..//mat-icon[text()='search']"));

        public static IWebLocator EmptyDisbursementTypeSearch => L(
            "EmptyDisbursementTypeSearch",
            By.XPath("//input[contains(@name, 'WorkCostType')]/../..//mat-icon[text()='search']"));

        public static IWebLocator EmptyDisbursementType => L(
            "EmptyDisbursementType",
            By.XPath("//div[@class='bound-column-container warn']//span"));

        public static IWebLocator DisbursementTaxCode => L(
           "DisbursementTaxCode",
           By.XPath("//input[contains(@name, 'TaxCode')]"));

        public static IWebLocator BillablePurgeDisbursementType => L(
            "BillablePurgeDisbursementType",
            By.XPath("//input[contains(@name, 'WorkCostType')]"));

        public static IWebLocator GridDropdown(string childProcess) => L(
            "ChildProcessGridDropdown",
            By.XPath("//span[contains(text(), '" + childProcess + "')]/..//span[text()=' Grid ']/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator ChildFormGridDropDown => L(
            "ChildFormGridDropDown",
            By.XPath("//span[text()='Apply Client Account ']//following::mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator FormDropdown(string childProcess) => L(
            "ChildProcessFormDropdown",
            By.XPath("//span[contains(text(), '" + childProcess + "')]/..//span[contains(text(), 'Form')]/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator FullFormButton => L(
            "FullFormButton",
            By.XPath("//span[text()=' Form - Full']"));

        public static IWebLocator ProformaBillAmount => L(
            "Proforma Bill Amount",
            By.XPath("//input[contains(@name, 'EditAmt')]"));

        public static IWebLocator Narrative => L(
            "Narrative",
            By.CssSelector("div.ql-editor.ql-blank p"));

        public static IWebLocator DisbursementDetailsNarrative => L(
            "DisbursementDetailsNarrative",
             By.XPath("//span[text()='Narrative']/../..//div[@class='ql-editor']"));

        public static IWebLocator NarrativeOnVoucher => L(
            "Narrative On Voucher",
             By.XPath("//span[text()='Narrative']/../..//div[contains(@class,'ql-container') or contains(@class,'ql-editor ql-blank')]"));
        
        public static IWebLocator NarrativeOnVoucherEditable => L(
            "Narrative On Voucher",
             By.XPath("//span[text()='Narrative']/../..//div[contains(@class,'ql-container') or contains(@class,'ql-editor ql-blank')][@contenteditable]"));

        public static IWebLocator AnticipatedCheckBox => L(
            "AnticipatedCheckBox",
             //By.XPath("//span[text()='Anticipated']/../..//input[@type='checkbox']/.."));
             By.XPath("//mat-checkbox[contains(@data-automation-id, 'Anticipated')]"));

        public static IWebLocator DisbursementTypeSearchInput => L(
            "Disbursement Type Search Input",
            By.XPath("//div[@id='pregrid-content']//input[contains(@class, 'mat-input')]"));

        public static IWebLocator WorkCurrency => L(
            "Work Currency",
            By.XPath("//input[contains(@name,'Currency')] | " + getDivElementText("/Currency")));

        public static IWebLocator WorkAmount => L(
            "WorkAmount",
            By.XPath("//input[contains(@name,'WorkAmt')] | " + getDivElementText("/WorkAmt")));

        public static IWebLocator Payee => L(
           "Payee",
           By.XPath("//input[contains(@name, 'Payee')][not(contains(@name, 'PayeeSite'))]"));

        public static IWebLocator TransactionTypeInput => L(
           "TransactionTypeInput",
           By.XPath("//input[contains(@name,'TranType')]"));

        public static IWebLocator VoucherStatus => L(
           "VoucherStatus",
           By.XPath("//input[contains(@name,'VchrStatus')]"));

        public static IWebLocator InvoiceAmount => L(
           "InvoiceAmount",
           By.XPath("//input[contains(@name, 'Amount')]"));

        public static IWebLocator VoucherDirectGLAmount => L(
          "VoucherDirectGLAmount",
          By.XPath("//input[contains(@data-automation-id,'VchrDirectGL') and contains(@name,'Amount')]"));

        public static IWebLocator SearchExpenseGLAccount => L(
          "SearchExpenseGLAccount",
          By.XPath("//span[contains(text(),'Expense GL Account')]/parent::*/following-sibling::*//button"));

        public static IWebLocator InputTaxCode => L(
            "InputTaxCode",
            By.XPath("//input[contains(@name, 'InputTaxCode')]"));

        public static IWebLocator DeTaxButton => L(
           "DeTaxButton",
           By.XPath("//span[text()=' De-Tax ']"));

        public static IWebLocator TaxCode => L(
           "TaxCode",
           By.XPath("//input[contains(@name, 'TaxCode')][not(contains(@name, 'InputTaxCode'))][not(contains(@name, 'WHTaxCode'))]"));

        public static IWebLocator PurgeTypeLabel => L(
           "PurgeTypeLabel",
           By.XPath("//span[contains(text(),'Purge Type')]"));

        public static IWebLocator ProformaTaxCode => L(
           "ProformaTaxCode",
           By.XPath("//input[contains(@name, 'WorkTaxCode')]"));

        public static IWebLocator VoucherFormButton => L(
            "VoucherFormButton",
            By.XPath("//span[text()=' Form']"));

        public static IWebLocator GridButton => L(
            "GridButton",
            By.XPath("//span[text()=' Grid']"));

        public static IWebLocator VoucherAddButton => L(
            "VoucherAddButton",
            By.XPath("//span[contains(text(), 'Disbursement Card')]/..//span[text()=' Add ']"));

        public static IWebLocator VoucherDisbursementType => L(
          "VoucherDisbursementType",
          By.XPath("//input[contains(@name, 'CostType')]"));

        public static IWebLocator VoucherMatter => L(
          "VoucherMatter",
          By.XPath("//input[contains(@name, 'Matter')]"));

        public static IWebLocator VoucherFeeEarner => L(
          "VoucherFeeEarner",
          By.XPath("//input[contains(@name, 'Timekeeper')][not(contains(@name, 'SpvTimekeeper'))][not(contains(@name, 'GLAcct.GLTimekeeper'))]"));

        public static IWebLocator VoucherAmount => L(
          "VoucherAmount",
          By.XPath("//input[contains(@name, 'OrigAmt')]"));

        public static IWebLocator VoucherNumber => L(
            "Voucher Number",
            By.XPath("//div[contains(@name,'VchrAutoNumber')]"));

        public static IWebLocator VoucherOffice => L(
          "VoucherOffice",
          By.XPath("//span[contains(text(), 'Disbursement Card')]/../../..//input[contains(@name, 'Office')]"));

        public static IWebLocator ValidateDisbursementEntry(string entry) => L(
           "ValidateEntry",
           //div[text()='1653']
           By.XPath("//div[@col-id='Description' and contains(text(),'" + entry + "')]"));

        public static IWebLocator ValidateEntry(string entry) => L(
            "ValidateEntry",
            //div[text()='1653']
            By.XPath("//div[text()='" + entry + "']"));

        public static IWebLocator ValidateDropDownSelection(string option) => L(
           "ValidateDropDownSelection",
           By.XPath("//span[text()='" + option + "']"));

        public static IWebLocator GetChargeTypeValue =>
            L("Charge Type Locator", By.XPath("//input[contains(@name,'ChrgType')]"));

        public static IWebLocator CarryOverCheckBox => L(
            "CarryOverCheckBox",
            By.XPath("//span[text()='Carry Over']/../..//input[@type='checkbox']/.."));

        public static IWebLocator ChargeTypeToDelete => L(
            "ChargeTypeToDelete",
            By.XPath("//span[contains(text(), 'Auto')]"));

        public static IWebLocator ChargeTypeDropDown => L(
            "ChargeTypeDropDown",
            By.XPath("//input[contains(@name,'ChrgType')]"));

        public static IWebLocator ChargeTypeDropDownArrow => L("Charge Type arrow Button ",
            By.XPath("//input[contains(@name,'ChrgType')]/parent::div/following-sibling::div//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator AddEntry => L(
            "AddEntry",
            By.XPath("//button[span[contains(text(),'Add')]]"));

        public static IWebLocator DeleteEntry => L(
            "DeleteEntry",
            By.XPath("//button/span[contains(text(),'Delete')]"));


        public static IWebLocator ExtraEntry(string entryType, string matterNumber) => L(
            "ExtraEntry",
            By.XPath("//div[text()='" + entryType + "']/parent::div//div[@col-id='Matter'][not(text()='" + matterNumber + "')]"));

        public static IWebLocator FirstEntry(string text) => L(
            "FirstEntry",
            By.XPath("//div[text()='1']/..//div[text()='" + text + "']"));

        // Code added by Sandeep
        public static IWebLocator ChargeModifyUpdateButton => L(
            "ChargeModifyUpdateButton",
            By.XPath("(//button[span[contains(text(),'Update')]])[last()]"));

        public static IWebLocator ChargeTypeInput => L(
            "ChargeTypeInput",
            By.XPath("//input[contains(@name,'ChrgType')] | " + getDivElementText("/ChrgType")));

        public static IWebLocator ChargeEntryWorkAmountInput => L(
            "DeTaxButton",
            By.XPath("//input[contains(@name,'Amount')]"));

        public static IWebLocator ChargeModifyWorkAmountInput => L(
            "DeTaxButton",
            By.XPath("//input[contains(@name,'WorkAmt')]"));

        public static IWebLocator WorkDate => L(
            "WorkDate",
            By.XPath("//input[contains(@name,'WorkDate')] | //div[contains(@name,'WorkDate')]"));

        public static IWebLocator FeeEarnerName => L(
            "FeeEarnerName",
            By.XPath("//div[contains(@name,'Timekeeper1.DisplayName')]"));

        public static IWebLocator TransactionType => L(
            "TransactionType",
            By.XPath("//div[contains(@name,'TransactionType')]"));

        public static IWebLocator MatterNumber => L(
            "MatterNumber",
            By.XPath("//div[contains(@name,'/BillMatter')]"));

        public static IWebLocator FeeEarner => L(
           "FeeEarner",
           By.XPath("//div[contains(@name,'//input[contains(@data-automation-id,'/Timekeeper')]')]"));

        public static IWebLocator TimeType => L(
       "TimeType",
       By.XPath("//input[contains(@name,'TimeType')]"));

        public static IWebLocator WorkHours => L(
       "WorkHours",
       By.XPath("//input[contains(@name,'WorkHrs')]"));

        public static IWebLocator WorkRate => L(
     "WorkRate",
     By.XPath("//input[contains(@name,'/WorkRate')] | " + getDivElementText("/WorkRate")));

        public static IWebLocator WIPHours => L(
     "WIPHours",
     By.XPath("//input[contains(@name,'WIPHrs')]"));

        public static IWebLocator WIPRate => L(
     "WIPRate",
     By.XPath("//input[contains(@name,'WIPRate')] | " + getDivElementText("WIPRate")));

        public static IWebLocator WIPQty => L(
    "WIPQty",
    By.XPath("//input[contains(@name,'WIPQty')]| " + getDivElementText("WIPQty")));

        public static IWebLocator WIPAmt => L(
     "WIPAmt",
     By.XPath("//input[contains(@name,'WIPAmt')] | " + getDivElementText("/WIPAmt")));

        public static IWebLocator Language => L(
     "Language",
     By.XPath("//input[contains(@name,'Language')] | " + getDivElementText("/Language")));

        public static IWebLocator Office => L(

      "Office",
      By.XPath("//input[contains(@name,'Office')] | " + getDivElementText("/Office")));

        public static IWebLocator WorkType => L(
      "WorkType",
      By.XPath("//input[contains(@name,'WorkType')]"));

        public static IWebLocator TimeKeeper => L(
    "TimeKeeper",
    By.XPath("//input[contains(@name,'/Timekeeper')]|" + getDivElementText("/Timekeeper")));

        public static IWebLocator WorkQuantity => L(
        "WorkQuantity",
        By.XPath("//input[contains(@name,'/WorkQty')]|" + getDivElementText("/WorkQty")));

        public static IWebLocator DisbursementCurrency => L(
        "DisbursementCurrency",
        By.XPath("//div[contains(@data-automation-id,'/Currency_BOUND')]//input[contains(@name,'Currency')] | //div[contains(@data-automation-id,'/Currency_BOUND')]//div[contains(@name,'Currency')]"));

        public static IWebLocator RefCurrency => L(
        "RefCurrency",
        By.XPath("//input[contains(@name,'RefCurrency')]|" + getDivElementText("RefCurrency")));

        public static IWebLocator RefRate => L(
       "RefRate",
       By.XPath("//input[contains(@name,'/RefRate')] |" + getDivElementText("/RefRate")));

        public static IWebLocator GlPostingsButton => L(
            "GlPostingsButton",
            By.XPath("//button[span[contains(text(),'GL Postings')]]"));

        public static IWebLocator CloseGlPostingsButton => L(
            "GlPostingsButton",
            By.XPath("//button[span[contains(text(),'Close')]]"));

        public static IWebLocator PostingsInformationHeader => L(
     "PostingsInformationHeader",
     By.XPath("//div[contains(text(),'Posting Information')][contains(@class,'label')]"));

        public static IWebLocator GlPostingsStatus => L(
      "GlPostingsStatus",
      By.XPath("//span[text()='Status']//ancestor::e3e-bound-input//div[contains(@name,'JMStatusList')]"));

        public static IWebLocator JournalManager => L(
      "JournalManager",
      By.XPath("//div[contains(@data-automation-id,'BasePostHeaderRel.JM_BOUND')]//div[contains(@data-automation-id,'BasePostHeaderRel.JM')]"));

        public static IWebLocator GLMaskedValues => L(
    "GLMaskedValues",
    By.XPath("//div[@col-id='BasePostRel.GLAcct1.MaskedAlias' and @role='gridcell']"));

        public static IWebLocator CostIndex => L(
        "CostIndex",
        By.XPath("//div[contains(@name,'/CostIndex')]"));

        public static IWebLocator TimeIndex => L(
        "TimeIndex",
        By.XPath("//div[contains(@name,'/TimeIndex')]"));

        public static IWebLocator ChargeIndex => L(
        "ChargeIndex",
        By.XPath("//div[contains(@name,'/ChrgCardIndex')]"));

        public static IWebLocator ReceiptIndex => L(
        "ReceiptIndex",
        By.XPath("//div[contains(@name,'/RcptIndex')]"));

        public static IWebLocator DisbursementChangeReason => L(
        "DisbursementChangeReason",
        By.XPath("//input[contains(@name,'CostReasonType')]"));

        public static IWebLocator NewDisbursementTypeFilter => L(
        "NewDisbursementTypeFilter",
        By.XPath("//div[@col-id='WorkCostType' and @title='Required']"));

        private static String getDivElementText(String elementName) {
            return "//div[@name[substring(.,string-length(.) - string-length('"+ elementName+ "') + 1) = '" + elementName + "']]";


        }
    }
}