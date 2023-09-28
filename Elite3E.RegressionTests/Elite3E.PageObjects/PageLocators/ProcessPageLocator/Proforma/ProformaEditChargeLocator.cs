using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;


namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma
{
    public class ProformaEditDisbursementLocator
    {
        public static IWebLocator ContainerRows => L(
            "Entity Link",
            By.CssSelector("e3e-grid-view div[ref='eCenterColsClipper'] div[role='row']"));

        public static IWebLocator AddNewButton => L(
           "AddNewButton",
           By.XPath("//button[span[contains(text(),'Add New')]]"));

        public static IWebLocator WorkDateGridCloumn => L(
            "WorkDate",
            By.CssSelector("div[role='gridcell'][col-id='WorkDate']"));

        public static IWebLocator WorkDateInput => L(
            "WorkDate",
            By.XPath("//input[contains(@name,'WorkDate')]"));

        public static IWebLocator DisbursementTypeGridCloumn => L(
          "Entity Link",
          By.CssSelector("div[role='gridcell'][col-id='WorkCostType']"));
        
        public static IWebLocator DisbursementTypeInput => L(
            "WorkDate",
            By.XPath("//input[contains(@name,'WorkCostType')]"));

        public static IWebLocator IsAnticipatedGridColumn => L(
          "Entity Link",
          By.CssSelector("div[role='gridcell'][col-id='IsAnticipated']"));
        
        public static IWebLocator IsAnticipatedCheckbox => L(
            "IsAnticipatedCheckbox",
            By.XPath("//span[text()='Anticipated']/../..//div[contains(@class, 'checkbox-inner')]"));

        public static IWebLocator FullFormDropDown => L(
          "FullFormDropDown",
          By.XPath("//span[text()=' Form - Full ']/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator GridButton => L(
            "GridButton",
            By.XPath("//span[text()=' Grid']"));

        public static IWebLocator BillQuantity => L(
          "Entity Link",
          By.CssSelector("div[role='gridcell'][col-id='EditQnt']"));

        public static IWebLocator ReferenceCurrencyGridColumn => L(
         "Entity Link",
         By.CssSelector("div[role='gridcell'][col-id='RefCurrency']"));

        public static IWebLocator ReferenceCurrencyInput => L(
         "Entity Link",
          By.XPath("//input[contains(@name,'RefCurrency')]"));

        public static IWebLocator WorkAmountGridColumn => L(
        "Entity Link",
        By.CssSelector("div[role='gridcell'][col-id='WorkAmt']"));

        public static IWebLocator WorkAmountInput => L(
        "Entity Link",
        By.XPath("//input[contains(@name,'WorkAmt')]"));

        public static IWebLocator TaxCodeGridColumn => L(
        "Entity Link",
        By.CssSelector("div[role='gridcell'][col-id='WorkTaxCode']"));

        public static IWebLocator TaxCodeInput => L(
        "Entity Link",
       By.XPath("//input[contains(@name,'WorkTaxCode')]"));

        public static IWebLocator Narrative => L(
        "Entity Link",
        By.CssSelector("div.ql-editor.ql-blank p"));

        public static IWebLocator ViewDisbursementGrid => L(
       "ViewDisbursementGrid",
      By.XPath("//h5[contains(text(),'Disbursement Details')]"));

        public static IWebLocator SelectFirstRow => L(
            "SelectFirstRow", By.XPath("//i/ancestor::div[@role='gridcell'][@col-id='IsSelected']"));


        public static IWebLocator NoChargeCheckbox => L(
             "NoChargeCheckbox",
              By.XPath("//div[contains(@data-automation-id,'ProfDetailChrg')]//mat-checkbox[contains(@data-automation-id,'IsNoCharge')]"));

        public static IWebLocator DisplayCheckbox => L(
         "DisplayCheckbox",
          By.XPath("//div[contains(@data-automation-id,'ProfDetailChrg')]//mat-checkbox[contains(@data-automation-id,'IsDisplay')]"));
    }
}
