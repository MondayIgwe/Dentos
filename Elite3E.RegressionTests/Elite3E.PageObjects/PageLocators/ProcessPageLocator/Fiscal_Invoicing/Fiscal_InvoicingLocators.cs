using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing
{
    using static Boa.Constrictor.WebDriver.WebLocator;

    public class Fiscal_InvoicingLocators
    {
        public static IWebLocator NextFiscalInvoiceNumber => L(
            "NextFiscalInvoiceNumber",
            By.XPath("//input[contains(@data-automation-id,'NextFiscalInvoiceNumber')]"));

        public static IWebLocator FiscalInvoiceSuffix => L(
            "FiscalInvoiceSuffix",
            By.XPath("//input[contains(@data-automation-id,'FiscalInvoiceSuffix')]"));

        public static IWebLocator BillGLType => L(
           "BillGLType",
           By.XPath("//input[contains(@data-automation-id,'BillGLType')]"));
        public static IWebLocator SuspenseGLType => L(
           "SuspenseGLType",
           By.XPath("//input[contains(@data-automation-id,'SuspenseGLType')]"));

        public static IWebLocator FiscalInvoicePrefix => L(
           "FiscalInvoicePrefix",
           By.XPath("//input[contains(@data-automation-id,'FiscalInvoicePrefix')]"));


        public static IWebLocator ActiveCheckbox => L(
          "ActiveCheckbox",
          By.XPath("//input[@id='mat-checkbox-4338-input']"));
        


        public static IWebLocator SearchContainer => L(
            "SearchContainer",
             By.XPath("//input[contains(@ref,'eCenterViewport')]"));

        public static IWebLocator Activerecord => L(
           "Activerecord",
            By.XPath("//input[@id='ag-input-id-118656']"));

        public static IWebLocator GetActive => L(
        "GetActive",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsActive')]/label/div/input"));

        public static IWebLocator Update => L(
       "Update",
     By.XPath("//span[contains(text(),'Update')]"));


        public static IWebLocator TexTValue => L(
     "TexTValue",
    By.XPath( "//span[contains(text(),'Fiscal Invoice Suffix')]"));
        public static IWebLocator Applyfilter => L(
     "Applyfilter",
       By.XPath("//mat-checkbox[contains(@data-automation-id,'apply-filter')]/label/div/input"));

        public static IWebLocator ShowButton => L(
       "ShowButton",
      By.XPath ("//span[contains(text(),'Show')]"));

        public static IWebLocator Activerecordtext => L(
    "Activerecordtext",
        By.XPath("//div[contains(text(),'Dentons Canada GP Incorporated')]"));

        public static IWebLocator Fiscalsetupheader => L(
    "Fiscalsetupheader",
      By.XPath("//span[contains(text(),'Fiscal Invoice Setup')]"));

        public static IWebLocator GetActivebox => L(
        "GetActivebox",
         By.XPath("//mat-checkbox[@id='mat-checkbox-4742']"));
        public static IWebLocator Searchres => L(
               "Searchres",
              By.ClassName ("ag-cell ag-cell-not-inline-editing ag-cell-auto-height grid-cell ag-cell-value ag-cell-focus"));

        public static IWebLocator FiscalInvoiceCheckBox => L(
                 "FiscalInvoiceCheckBox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsFiscalInvoice_ccc')]/label/div/input"));

        public static IWebLocator FiscalInvoicetext => L(
                 "FiscalInvoicetext",
        By.XPath("//span[contains(text(),'Create Fiscal Invoice')]"));

        public static IWebLocator BillNoPrint => L(
                 "BillNoPrint",
        By.XPath("//span[contains(text(),'BillNoPrint')]"));

        public static IWebLocator UnitCode => L(
                 "UnitCode",
        By.XPath("//input[contains(@data-automation-id,'Code')]"));
           

        public static IWebLocator UnitDescription => L(
                 "UnitDescription",
        By.XPath("//input[contains(@data-automation-id,'Description')]"));


        public static IWebLocator NarrativeText => L(
        "NarrativeText",
        By.XPath("//div[contains(@data-automation-id,'Narrative')]//div[@class='ql-editor']/p"));

        public static IWebLocator ExchangeRate => L(
        "ExchangeRate",
        By.XPath("//div[contains(@name,'PresExchangeRate_ccc')]"));

        public static IWebLocator PresInvoiceAmount => L(
        "PresInvoiceAmount",
        By.XPath("//div[contains(@name,'PresInvoiceAmount_ccc')]"));

        public static IWebLocator GovtUploadRunDate => L(
     "GovtUploadRunDate",
     By.XPath("//div[contains(@name,'DateTimeRun')]"));

        public static IWebLocator GovtUploadTemplate => L(
     "GovtUploadTemplate",
     By.XPath("//div[contains(@name,'DCSTemplate') and contains(@name,'InvElecHistory_ccc')]"));

            public static IWebLocator PresDate => L(
    "PresDate",
    By.XPath("//div[contains(@name,'PresCurrencyDate_ccc')]"));

        public static IWebLocator PresCurrency => L(
        "PresCurrency",
        By.XPath("//input[contains(@name,'PresCurrency_ccc')]"));

        public static IWebLocator Currency => L(
        "Currency",
        By.XPath("//div[contains(@data-automation-id,'/Currency_BOUND')]//div[contains(@name,'/Currency')]"));

    }
}
