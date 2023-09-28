using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank
{
    public class BankAccountClientAccountLocators
    {
        public static IWebLocator Bank => L(
            "Bank",
            By.XPath("//div[contains(@data-automation-id,'/Bank_BOUND')]//input[contains(@name,'Bank')]"));

        public static IWebLocator AccountName => L(
           "AccountName",
           By.XPath("//div[contains(@data-automation-id,'/Name_BOUND')]//input[contains(@name,'/BankAcctTrust')]"));

        public static IWebLocator Description => L(
           "Description",
           By.XPath("//input[contains(@name,'/Description')]"));

        public static IWebLocator MoneyType => L(
           "MoneyType",
           By.XPath("//input[contains(@name,'/TrustAcctMoneyList')]"));

        public static IWebLocator BankAccountType => L(
           "BankAccountType",
           By.XPath("//input[contains(@name,'/BankAcctType')]"));
        public static IWebLocator BankGroup => L(
           "BankAccountType",
           By.XPath("//input[contains(@name,'/BankGroup')]"));

        public static IWebLocator Status => L(
           "Status",
           By.XPath("//input[contains(@name,'/BankAcctStatusList')]"));

        public static IWebLocator CurrencyInputAttribute(string text) => L(
   "CurrencyInputAttribute",
   By.XPath("//input//following::span//label//span[text()='"+text+"']"));

        public static IWebLocator AccountNumber => L(
           "AccountNumber",
           By.XPath("//input[contains(@name,'/AcctNum')]"));

        public static IWebLocator Office => L(
           "Office",
           By.XPath("//input[contains(@name,'/Office')]"));

        public static IWebLocator Language => L(
           "Language",
           By.XPath("//input[contains(@name,'/Language')]"));

        public static IWebLocator Currency => L(
           "Currency",
           By.XPath("//input[contains(@name,'/Currency')]"));

        public static IWebLocator AnchorMask => L(
           "AnchorMask",
           By.XPath("//input[contains(@name,'/AnchorMask')]"));

        public static IWebLocator GLType => L(
           "GLType",
           By.XPath("//input[contains(@name,'/GLType')]"));

        public static IWebLocator CashGLAccount => L(
         "CashGLAccount",
         By.XPath("//div[contains(@data-automation-id,'/CashGLAcct_BOUND')]"));

        public static IWebLocator ContraGLAccount => L(
         "ContraGLAccount",
         By.XPath("//div[contains(@data-automation-id,'/ContraGLAcct_BOUND')]"));

        public static IWebLocator PowerAndEstatesInformation => L(
         "PowerAndEstatesInformation",
         By.XPath("//mat-card[contains(text(),'Power And Estates Information')]"));

        public static IWebLocator SearchIconBasedOnLabel(string label) => L(
        "SearchIconBasedOnLabel",
        By.XPath("//span[contains(text(),'" + label + "')]/parent::*/following-sibling::*//button"));


        public static IWebLocator MatterClientAccount => L(
    "MatterClientAccount",
    By.XPath("//mat-card[contains(text(),'Matter Client Account')]"));

        public static IWebLocator RestrictAccountUsersAndRoles => L(
    "RestrictAccountUsersAndRoles",
    By.XPath("//mat-card[contains(text(),'Restrict Account to Users/Roles')]"));

        public static IWebLocator EditDropDown => L(
            "EntityEditDropDown",
            By.XPath("//span[text()=' Edit ']/ancestor::button/following-sibling::button//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator NewButton => L(
      "NewButton",
      By.XPath("//span[text()=' New']"));

        public static IWebLocator EntityName => L(
        "EntityName",
        By.XPath("//input[contains(@name, 'Entity')]"));

        public static IWebLocator BankSite => L(
        "BankSite",
        By.XPath("//input[contains(@name, '/Site')]"));

        public static IWebLocator PositivePayTemplate => L(
       "PositivePayTemplate",
       By.XPath("//input[contains(@data-automation-id,'PositivePayTemplate')]"));

        public static IWebLocator PositivePayClientTemplate => L(
       "PositivePayClientTemplate",
       By.XPath("//input[contains(@data-automation-id,'/PositivePayTrustTemplate')]"));


        public static IWebLocator ABARoutingNumber => L(
        "ABARoutingNumber",
        By.XPath("//input[contains(@name, '/BranchNum')]"));

        public static IWebLocator Name => L(
       "Name",
       By.XPath("//input[contains(@name,'/Name')]"));

        public static IWebLocator PositivePayAPCheckbox => L(
       "PositivePayAPCheckbox",
       By.XPath("//span[text()='Positive Pay (AP)']/parent::label/following-sibling::div//mat-checkbox[contains(@data-automation-id,'IsPositivePay')]"));

        public static IWebLocator PositivePayClientAccountCheckbox => L(
    "PositivePayClientAccountCheckbox",
    By.XPath("//span[text()='Positive Pay (Client Account)']/parent::label/following-sibling::div//mat-checkbox[contains(@data-automation-id,'IsPositivePayTrust')]"));

        public static IWebLocator ContraGlAccountSearch => L(
       "ContraGlAccountSearch", By.XPath("//div[contains(@data-automation-id,'ContraGLAcct')]/following-sibling::div//span/mat-icon"));

        public static IWebLocator CashGlAccountSearch => L(
        "CashGlAccountSearch", By.XPath("//div[contains(@data-automation-id,'CashGLAcct')]/following-sibling::div//span/mat-icon"));
    }
}
