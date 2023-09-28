using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payee
{
    public class VendorPayeeMaintenanceLocator
    {
        public static IWebLocator Vendor => L(
            "Code",
            By.XPath("//input[contains(@name,'Vendor')]"));
        public static IWebLocator EntityInput => L(
            "Code",
            By.XPath("//input[contains(@name,'Entity')]"));

        public static IWebLocator GlobalVendor => L(
            "GlobalVendor",
            By.XPath("//input[contains(@name,'GlobalVendor_ccc')]"));

        public static IWebLocator PaymentTerms => L(
            "Description",
            By.XPath("//input[contains(@name,'Terms')]"));

        public static IWebLocator Office => L(
            "Description",
            By.XPath("//input[contains(@name,'attributes/Office')]"));

        public static IWebLocator SetDefaultBank => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsDefault')]/label/div"));

        public static IWebLocator GettDefaultBank => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsDefault')]/label/div/input"));

        public static IWebLocator Description => L(
            "Description",
            By.XPath("//input[contains(@name,'Description')]"));
        public static IWebLocator AccountNumber => L(
            "AcctNum",
            By.XPath("//input[contains(@name,'AcctNum')]"));
        public static IWebLocator BeneficiaryName => L(
            "Description",
            By.XPath("//input[contains(@name,'BeneficiaryName_ccc')]"));
        public static IWebLocator BankCode => L(
           "BankCode",
           By.XPath("//input[contains(@name,'BankCode_ccc')]"));
        public static IWebLocator BankAddress1 => L(
            "BankAddress1",
            By.XPath("//input[contains(@name,'BankAddress1_ccc')]"));
        public static IWebLocator BankAddress2 => L(
            "BankAddress2",
            By.XPath("//input[contains(@name,'BankAddress2_ccc')]"));
        public static IWebLocator BankAddress3 => L(
            "BankAddress3",
            By.XPath("//input[contains(@name,'BankAddress3_ccc')]"));
        public static IWebLocator ClearingCodeType => L(
            "ClearingCodeType",
            By.XPath("//input[contains(@name,'ClearingCodeType_ccc') and contains(@data-automation-id,'IB')=false]"));

        public static IWebLocator ClearingCode => L(
            "ClearingCode",
            By.XPath("//input[contains(@name,'ClearingCode_ccc') and contains(@data-automation-id,'IB')=false]"));
        public static IWebLocator PaymentReference => L(
            "PaymentReference",
            By.XPath("//input[contains(@name,'PaymentReference_ccc')]"));
        public static IWebLocator IbAddress1 => L(
            "IBAddress1",
            By.XPath("//input[contains(@name,'IBAddress1_ccc')]"));
        public static IWebLocator IbAddress2 => L(
            "IBAddress2",
            By.XPath("//input[contains(@name,'IBAddress2_ccc')]"));
        public static IWebLocator IbAddress3 => L(
            "IBAddress3",
            By.XPath("//input[contains(@name,'IBAddress3_ccc')]"));
        public static IWebLocator IbClearingCodeType => L(
            "IBClearingCodeType",
            By.XPath("//input[contains(@name,'IBClearingCodeType_ccc')]"));
        public static IWebLocator IbClearingCode => L(
            "IBClearingCode",
            By.XPath("//input[contains(@name,'IBClearingCode_ccc')]"));
        public static IWebLocator SwiftCode => L(
            "SwiftCode",
            By.XPath("//input[contains(@name,'SwiftCode_ccc')]"));
        public static IWebLocator PayNow => L(
            "PayNow",
            By.XPath("//input[contains(@name,'PayNow_ccc')]"));
        public static IWebLocator Site => L(
            "Site1099",
            By.XPath("//input[contains(@name,'Site1099')]"));

        public static IWebLocator PayeeBank => L(
        "PayeeBank",
        By.XPath("//mat-card[contains(text(),'Payee Bank')]"));

        public static IWebLocator PayeeAccount => L(
        "PayeeAccount",
        By.XPath("//mat-card[contains(text(),'Payee Account')]"));

        public static IWebLocator PayeeEEOC => L(
        "PayeeEEOC",
        By.XPath("//mat-card[contains(text(),'Payee EEOC')]"));

        public static IWebLocator APContact => L(
        "APContact",
        By.XPath("//mat-card[contains(text(),'AP Contact')]"));

        public static IWebLocator VendorName => L(
        "APContact",
        By.XPath("//div[contains(@data-automation-id,'/Vendor_BOUND')]/div"));

        public static IWebLocator PayeeNum => L(
        "PayeeNum",
        By.XPath("//div[contains(@data-automation-id,'PayeeNum')]"));

        public static IWebLocator PayeeName => L(
        "PayeeName",
        By.XPath("//div[contains(@data-automation-id,'Name_BOUND')]"));

        public static IWebLocator PayeeTypeInput => L(
        "PayeeType Input",
        By.XPath("//input[contains(@data-automation-id,'PayeeType')]"));

        public static IWebLocator PayeeType => L(
      "PayeeType",
      By.XPath("//div[contains(@data-automation-id,'PayeeType')]"));

        public static IWebLocator PayeeStatus => L(
       "PayeeStatus",
       By.XPath("//div[contains(@data-automation-id,'PayeeStatus')]"));

        public static IWebLocator VoucherStatus => L(
        "PayeeName",
        By.XPath("//div[contains(@data-automation-id,'VchrStatus')]"));


        public static IWebLocator Unit => L(
       "Unit",
       By.XPath("//input[contains(@data-automation-id,'/NxUnit')]"));

        public static IWebLocator Currency => L(
      "Currency",
      By.XPath("//input[contains(@name,'attributes/Currency')]"));

        public static IWebLocator IsDefaultBankCheckbox => L(
     "IsDefaultBankCheckbox",
     By.XPath("//mat-checkbox[contains(@data-automation-id,'IsDefault')]"));

        public static IWebLocator PayeePredicate => L(
      "PayeePredicate", By.XPath("//span[text()='Payee Predicate']//ancestor::e3e-bound-input//mat-icon[text()='search']"));

        public static IWebLocator AUSupplierDataRecord(string columnName,string header, string value) => L(
    "AUSupplierDataRecord", By.XPath("//div[text()='"+ columnName + "']/ancestor::td//following-sibling::td[@col-index=//div[contains(text(),'"+ header + "')]/ancestor::th/@col-index]//div[text()='"+value+"']"));

        public static IWebLocator Is1099Checkbox => L(
     "Is1099Checkbox",
     By.XPath("//mat-checkbox[contains(@data-automation-id,'Is1099')]"));

        public static IWebLocator Is1099DropDown => L(
       "Is1099DropDown",
       By.XPath("//input[contains(@data-automation-id,'Flag1099')]"));

        public static IWebLocator IRSNameControl => L(
           "IRS Name Control",
           By.XPath("//input[contains(@name,'NameCtrl')]"));

            public static IWebLocator Status => L(
            "Status",
            By.XPath("//input[contains(@data-automation-id,'PayeeStatus')]"));

        public static IWebLocator VendorNameText => L(
    "VendorNameText",
    By.XPath("//div[contains(@data-automation-id,'/Vendor_BOUND')]/div//div[contains(@data-automation-id,'Vendor')]"));

        public static IWebLocator TaxCertificateDate => L(
       "TaxCertificateDate",
       By.XPath("//input[contains(@data-automation-id,'TaxCertificateDate_ccc')]"));
    }
}
