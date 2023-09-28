using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor
{
    public class VendorLocators
    {
        public static IWebLocator Entity => L(
                "Entity",
        By.XPath("//input[contains(@name, 'Entity')]"));

        public static IWebLocator StatusDropDown => L(
                "StatusDropDown",
        By.XPath("//input[contains(@name, 'VendorStatus')]/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator VendorStatus => L(
            "Status",
            By.XPath("//input[contains(@name, 'VendorStatus')]"));

        public static IWebLocator GetActiveCheckbox => L(
                "GetActiveCheckbox",
        By.XPath("//span[text()='Active']/../..//input[@type='checkbox']"));

        public static IWebLocator CheckDuplicates => L(
                "CheckDuplicates Button",
        By.XPath("//button//span[.=' Check Duplicates ']"));

        public static IWebLocator CheckPayeeDuplicates => L(
              "CheckPayeeDuplicates",
      By.XPath("//span[contains(text(),'Payee  ')]//following::button//span[.=' Check Duplicates ']"));

        public static IWebLocator SetActiveCheckbox => L(
                "SetActiveCheckbox",
        By.XPath("//span[text()='Active']/../..//input[@type='checkbox']/.."));

        public static IWebLocator CodeFieldDisabled => L(
                "CodeFieldDisabled",
        By.XPath("//div[contains(@name, 'Code')][@disabled='true']"));

        public static IWebLocator GlobalVendorDropDown => L(
            "GlobalVendorDropDown",
            By.XPath(
                "//input[contains(@data-automation-id, 'GlobalVendor_ccc')]/parent::div/following-sibling::div//mat-icon"));

        public static IWebLocator GlobalVendor => L(
            "GlobalVendorDropDown",
            By.XPath(
                "//input[contains(@data-automation-id, 'GlobalVendor_ccc')]"));

        public static IWebLocator GlobalVendorField => L(
                "GlobalVendorField",
        By.XPath("//input[contains(@name, 'GlobalVendor_ccc')]"));

        public static IWebLocator TransactionType => L(
                "TransactionType",
        By.XPath("//input[contains(@name, 'TransactionType')]"));

        public static IWebLocator GetHardDisbursementCheckbox => L(
                "GetHardDisbursementCheckbox",
        By.XPath("//span[text()='Hard Disbursement']/../..//input[@type='checkbox']"));

        public static IWebLocator SetHardDisbursementCheckbox => L(
                "SetHardDisbursementCheckbox",
        By.XPath("//span[text()='Hard Disbursement']/../..//input[@type='checkbox']/.."));

        public static IWebLocator GetBarristerCheckbox => L(
                "GetBarristerCheckbox",
        By.XPath("//span[text()='Barrister']/../..//input[@type='checkbox']"));

        public static IWebLocator SetBarristerCheckbox => L(
                "SetBarristerCheckbox",
        By.XPath("//span[text()='Barrister']/../..//input[@type='checkbox']/.."));

        public static IWebLocator Attachments => L(
                "Attachments",
        By.XPath("//span[text()=' Attachments ']"));

        public static IWebLocator AddFile => L(
                "AddFile",
        By.XPath("//span[text()=' Add File ']"));

        public static IWebLocator ValidateFile(string fileName) => L(
                "ValidateFile",
        By.XPath("//span[text()=' " + fileName + " ']"));

        public static IWebLocator CloseButton => L(
                "CloseButton",
        By.XPath("//button[@data-automation-id='close@0']"));

        public static IWebLocator ValidateAttachment(string num) => L(
                "ValidateAttachment",
        By.XPath("//span[text()=' Attachments ']/..//span[@id='mat-badge-content-0'][text()='" + num + "']"));

        public static IWebLocator PayDate => L(
                "PayDate",
        By.XPath("//input[contains(@name, 'PayDate')]"));
        public static IWebLocator InputTaxCode => L(
                "InputTaxCode",
        By.XPath("//input[contains(@data-automation-id,'InputTaxCode')]"));
        public static IWebLocator InputAmount => L(
        "InputAmount",
       By.XPath("//input[contains(@data-automation-id,'InputAmt')]"));
        public static IWebLocator VoucherTaxCard => L(
       "InputAmount",
      By.XPath("//div//mat-card[contains(text(),'Voucher Taxes')]"));
        public static IWebLocator DivInput => L(
      "div",
     By.XPath("//div[@row-index='0']//div[@col-id='InputAmt']"));

        public static IWebLocator CurrencyDate => L(
                "CurrencyDate",
        By.XPath("//input[contains(@name, 'CurrDate')]"));

        public static IWebLocator BarristerGender_Field => L(
                "BarristerGender_field",
        By.XPath("//input[contains(@name, 'BarristerGender_ccc')]"));

        public static IWebLocator BarristerGender_Required => L(
                "BarristerGender_required",
        By.XPath("//div[contains(@data-automation-id,'BarristerGender')]//span[contains(@class, 'required-indicator')]"));

        public static IWebLocator BarristerSeniority_Field => L(
                "BarristerSeniority_field",
        By.XPath("//input[contains(@name, 'BarristerSeniority_ccc')]"));

        public static IWebLocator BarristerSeniority_Required => L(
                "BarristerSeniority_required",
        By.XPath("//div[contains(@data-automation-id,'BarristerSeniority')]//span[contains(@class, 'required-indicator')]"));

        public static IWebLocator BarristerName_Field => L(
                "BarristerName_field",
        By.XPath("//input[contains(@name, 'BarristerName_ccc')]"));

        public static IWebLocator BarristerName_Required => L(
                "BarristerName_required",
        By.XPath("//div[contains(@data-automation-id,'BarristerName')]//span[contains(@class, 'required-indicator')]"));

        public static IWebLocator VoucherDirectGLAmount => L(
          "VoucherDirectGLAmount",
          By.XPath("//input[contains(@name, 'Amount')][contains(@name, 'VchrDirectGL')]"));
        public static IWebLocator VoucherOrigAmount => L(
         "VoucherOrigAmount",
         By.XPath("//input[contains(@data-automation-id,'Amount') and contains(@name,'Voucher')]"));

        public static IWebLocator GLUnit => L(
          "GLUnit",
          By.XPath("//input[@name='GLAcct1.GLUnit']"));

        public static IWebLocator Currency => L(
        "Currency",
        By.XPath("//input[contains(@data-automation-id,'Currency')]"));

        public static IWebLocator Unit => L(
        "Unit",
        By.XPath("//input[contains(@data-automation-id,'NxUnit')]"));

        public static IWebLocator GLNatural => L(
          "GLNatural",
          By.XPath("//input[@name='GLAcct1.GLNatural']"));

        public static IWebLocator GLUnitLocal => L(
        "GLUnitLocal",
        By.XPath("//input[contains(@data-automation-id,'GLAcct1.GLUnitLocal')]"));

        public static IWebLocator GLDepartment => L(
          "GLDepartment",
          By.XPath("//input[@name='GLAcct1.GLDepartment']"));

        public static IWebLocator GLSection => L(
          "GLSection",
          By.XPath("//input[@name='GLAcct1.GLSection']"));

        public static IWebLocator GLOffice => L(
          "GLOffice",
          By.XPath("//input[@name='GLAcct1.GLOffice']"));

        public static IWebLocator GLTimekeeper => L(
          "GLTimekeeper",
          By.XPath("//input[@name='GLAcct1.GLTimekeeper']"));

        public static IWebLocator TaxCode => L(
          "TaxCode",
          By.XPath("//input[contains(@data-automation-id,'TaxCode') and contains(@name,'VchrDirectGL')]"));

        public static IWebLocator EntityEditDropDown => L(
          "EntityEditDropDown",
          By.XPath("//span[text()=' Edit ']/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator NewOrganisation => L(
          "NewOrganisation",
          By.XPath("//span[text()=' New Organisation']"));

        public static IWebLocator OrganisationName => L(
          "OrganisationName",
          By.XPath("//input[contains(@name, 'OrgName')]"));

        public static IWebLocator CheckOrganisation => L(
          "CheckOrganisation",
          By.XPath("//button/span[contains(text(),'Check Org')]"));

        public static IWebLocator SiteAdd => L(
          "SiteAdd",
          By.XPath("//span[text()='Sites']/..//span[text()=' Add ']"));

        public static IWebLocator SiteTypeDropDown => L(
          "SiteTypeDropDown",
          By.XPath("//input[contains(@name, 'SiteType')]/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator SiteType => L(
            "SiteType",
            By.XPath("//input[contains(@name, 'SiteType')]"));

        public static IWebLocator AddressStreet => L(
          "AddressStreet",
          By.XPath("//input[contains(@name, 'Street')]"));

        public static IWebLocator AddressCountryDropDown => L(
          "AddressCountryDropDown",
          By.XPath("//input[contains(@name, 'Country')]/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator AddressCountry => L(
            "AddressCountry",
            By.XPath("//input[contains(@name, 'Country')]"));

        public static IWebLocator PaymentTermsDropDown => L(
          "PaymentTermsDropDown",
          By.XPath("//input[contains(@name, 'Terms')]/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator PaymentTerms => L(
            "PaymentTerms",
            By.XPath("//input[contains(@name, 'Terms')]"));

        public static IWebLocator RowVendor(string row) => L(
           "FirstRowVendor",
           By.XPath("//div[@row-index='" + row + "' and @row-id='" + row + "']//div[@ref='eCheckbox']"));

        public static IWebLocator OfficeDropDown => L(
          "OfficeDropDown",
          By.XPath("//input[contains(@name, 'Office')]/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator Office => L(
            "Office",
            By.XPath("(//input[contains(@name, 'Office')])[last()]"));

        public static IWebLocator LanguageDropDown => L(
         "OfficeDropDown",
         By.XPath("//input[contains(@name, 'Language')]/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator Language => L(
            "Office",
            By.XPath("(//input[contains(@name, 'Language')])[last()]"));

        public static IWebLocator UpdateButton => L(
          "UpdateButton",
          By.XPath("//span[text()=' Update ']"));

        public static IWebLocator CommentsLocator => L("Comments Text area",
            By.XPath("//textarea[contains(@data-automation-id,'Comment')]"));

        public static IWebLocator PayNowButton => L(
            "PayNowButton",
            By.XPath("//span[contains(text(),'Pay Now')]//parent::button"));

        public static IWebLocator PayNowTitle => L(
            "PayNowTitle",
            By.XPath("//div[contains(@class,'title-bar')][contains(text(),'Pay Now')]"));

        public static IWebLocator PayNow_ChequeNumber => L(
            "PayNow_ChequeNumber",
            By.XPath("//input[contains(@name,'/CheckNumber')]"));

        public static IWebLocator PayNow_OK => L(
            "PayNow_OK",
            By.XPath("//span[contains(text(),'OK')]//parent::button"));

        public static IWebLocator CreateChequeCheckbox => L(
            "CreateChequeCheckbox",
            By.XPath("//span[contains(text(),'Create Cheque')]//ancestor::e3e-bound-input//input"));

        public static IWebLocator VoucherStatusDropDown => L(
            "VoucherStatusDropDown",
            By.XPath("//input[contains(@name,'/VchrStatus')]"));

        public static IWebLocator VoucherDirectGL => L(
        "VoucherDirectGL",
        By.XPath("//mat-card[contains(text(),'Voucher Direct GL')]"));

        public static IWebLocator DisbursementCard => L(
        "DisbursementCard",
        By.XPath("//mat-card[contains(text(),'Disbursement Card')]"));

        public static IWebLocator VoucherTaxes => L(
        "VoucherTaxes",
        By.XPath("//mat-card[contains(text(),'Voucher Taxes')]"));

        public static IWebLocator VoucherWithholdingTax => L(
        "VoucherWithholdingTax",
        By.XPath("//mat-card[contains(text(),'Voucher Withholding Tax')]"));
        public static IWebLocator Voucher1099 => L(
        "Voucher1099",
        By.XPath("//mat-card[contains(text(),'Voucher 1099')]"));
        public static IWebLocator ChequesAppliedAgainstVoucher => L(
        "ChequesAppliedAgainstVoucher",
        By.XPath("//mat-card[contains(text(),'Cheques Applied Against Voucher')]"));

        public static IWebLocator UDF => L(
       "UDF",
       By.XPath("//mat-card[contains(text(),'UDF')]"));

        public static IWebLocator EntityName => L(
       "EntityName",
       By.XPath("//div[contains(@data-automation-id,'/Entity_BOUND')]//div[contains(@name,'/Entity')]"));

        public static IWebLocator InvoiceNumber => L(
        "InvoiceNumber",
        By.XPath("//input[contains(@name,'/InvNum')]"));

        public static IWebLocator TermsCode => L(
        "TermsCode",
        By.XPath("//input[contains(@name,'Terms')]"));

        public static IWebLocator APGLAccount => L(
        "APGLAccount",
        By.XPath("//div[contains(@data-automation-id,'APGLAcct_BOUND')]"));

        public static IWebLocator VoucherStatus => L(
        "VoucherStatus",
        By.XPath("//div[contains(@data-automation-id,'VchrStatus')]"));

        public static IWebLocator Name => L(
        "Name",
        By.XPath("//input[contains(@name,'/Name')]"));
        public static IWebLocator VendorNumber => L(
        "VendorNumber",
        By.XPath("//input[contains(@name,'/VendorNum')]"));

        public static IWebLocator TaxButton => L(
        "TaxButtonInDisbursementCard",
        By.XPath("//e3e-form-anchor-view-header[descendant::span[contains(text(),'Disbursement Card')]]//following-sibling::e3e-form//button//span[contains(text(),'Tax')]"));

        public static IWebLocator TaxCodeInVoucherTaxes(string TaxCode) => L(
        "TaxCodeValueInvoucherTaxesSection",
         By.XPath("//span[contains(text(),'Voucher Taxes')]/ancestor::mat-card[1]//span[text()='"+ TaxCode+ "']"));

        public static IWebLocator TaxInputAmount => L(
       "TaxInputAmountFromVoucherTaxesSection",
       By.XPath("//div[@col-id='InputAmt' and contains(@class,'editable')]//span[@title]"));

        public static IWebLocator VendorTypeDropDown => L(
        "VendorTypeDropDown",
        By.XPath("//input[contains(@data-automation-id, 'VendorType')]"));

        public static IWebLocator VendorCategoryDropDown => L(
        "VendorCategoryDropDown",
        By.XPath("//input[contains(@data-automation-id, 'VendorCategory')]"));

        public static IWebLocator ButtonWithIndex(string text, int index) => L(
           "ButtonWithIndex",
           By.XPath("(//span[text()='" + text + "'])["+ index +"]"));

        public static IWebLocator PayeeStatusDropDown => L(
       "VendorCategoryDropDown",
       By.XPath("//input[contains(@data-automation-id, 'PayeeStatus')]/parent::div/following-sibling::div//mat-icon"));

        public static IWebLocator AddEditSiteButton(string siteName) => L(
            "AddEditSiteButton",
            By.XPath("//button[@data-automation-id[substring(.,string-length(.) - string-length('"+ siteName+ "') + 1) = '"+ siteName +"']]"));

        public static IWebLocator DisbursementType(string disbursementType) => L(
    "DisbursementType",
    By.XPath("//div[@col-id='CostType']//span[contains(text(),'"+ disbursementType + "')]"));

        public static IWebLocator WorkAmount => L(
        "WorkAmount",
        By.XPath("//input[contains(@name, '/WorkAmt')]"));

        public static IWebLocator TaxCertificateDate => L(
        "TaxCertificateDate",
        By.XPath("//input[contains(@data-automation-id,'TaxCertificateDate_ccc')]"));
    }
}
