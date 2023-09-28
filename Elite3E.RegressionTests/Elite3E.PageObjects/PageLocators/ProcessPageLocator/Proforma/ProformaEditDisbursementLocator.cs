using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;


namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma
{
    public class ProformaEditChargeLocator
    {
        public static IWebLocator ContainerRows => L(
            "Entity Link",
            By.CssSelector("e3e-grid-view div[ref='centerContainer'] div[role='row']"));

        public static IWebLocator AddNewButton => L(
           "AddNewButton",
           By.XPath("//button[span[contains(text(),'Add New')]]"));

        public static IWebLocator ChargeTypeGridCloumn => L(
            "ChargeTypeGridCloumn",
            By.CssSelector("div[role='gridcell'][col-id='WorkChrgType']"));

        public static IWebLocator ChargeTypeInput => L(
            "Charge Type Input",
            By.XPath("//input[contains(@name,'WorkChrgType')]"));

        public static IWebLocator WorkAmountGridCloumn => L(
          "Entity Link",
          By.CssSelector("div[role='gridcell'][col-id='WorkAmt']"));

        public static IWebLocator WorkAmountInput => L(
            "WorkDate",
            By.XPath("//input[contains(@name,'WorkAmt')]"));

        public static IWebLocator ViewChargeDetailsGrid => L(
       "Entity Link",
      By.XPath("//h5[contains(text(),'Charge Details')]"));
        public static IWebLocator BillingContact => L(
           "CentralBillingCOntact",
               By.XPath("//mat-card[contains(text(),'Billing Contact')]"));
        public static IWebLocator CreateContact => L(
        "createcontact",
            By.XPath(" //span[contains(text(),'Billing Contact')]//following::span[text()=' Create Contact ']"));

        public static IWebLocator Payer => L(
        "createcontact",
            By.XPath("//input[contains(@data-automation-id,'Payor') and contains(@name,'NewContactPopup_Proforma')]"));

        public static IWebLocator GetProformaInput(string text) => L(
       "payor",
      By.XPath("//input[contains(@data-automation-id,'" + text + "') and  contains(@name,'ProformaContacts_ccc')]"));

        public static IWebLocator ClickProformaDiv(string text) => L(
         "emailvalue",
          By.XPath("//div[contains(@col-id,'" + text + "')]//span//div"));

        public static IWebLocator NoChargeCheckbox => L(
         "NoChargeCheckbox",
          By.XPath("//div[contains(@data-automation-id,'ProfDetailCost')]//mat-checkbox[contains(@data-automation-id,'IsNoCharge')]"));

        public static IWebLocator DisplayCheckbox => L(
         "DisplayCheckbox",
          By.XPath("//div[contains(@data-automation-id,'ProfDetailCost')]//mat-checkbox[contains(@data-automation-id,'IsDisplay')]"));

        public static IWebLocator DisbursementBillAmount => L(
         "DisbursementBillAmount",
          By.XPath("//div[contains(@name,'ProfDetailCost') and contains(@name,'EditAmt')]"));

        public static IWebLocator ChargeBillAmount => L(
         "ChargeBillAmount",
          By.XPath("//div[contains(@name,'ProfDetailChrg') and contains(@name,'EditAmt')]"));

        public static IWebLocator FeeDetailsBillAmount => L(
         "FeeDetailsBillAmount",
          By.XPath("//div[contains(@name,'ProfDetailTime') and contains(@name,'EditAmt')]"));

    }
}
