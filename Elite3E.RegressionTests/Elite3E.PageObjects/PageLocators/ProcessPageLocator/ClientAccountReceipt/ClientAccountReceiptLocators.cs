using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountReceipt
{
    public class ClientAccountReceiptLocators
    {
        public static IWebLocator ClientAccountReceiptType => L(
            "ClientAccountReceiptType",
            By.XPath("//input[contains(@data-automation-id,'TrustReceiptType')]"));

        public static IWebLocator GlPostingsButton => L(
            "GlPostingsButton",
            By.XPath("//button//span[text()=' GL Postings ']"));

        public static IWebLocator JournalManagerDiv => L(
          "JournalManagerDiv",
          By.XPath("//div[contains(@data-automation-id,'BasePostHeaderRel.JM_BOUND')]//e3e-readonly-input"));

        public static IWebLocator PostStatusDiv => L(
        "PostStatusDiv",
        By.XPath("//div[contains(@data-automation-id,'BasePostHeaderRel.JMRel.JMStatusList_BOUND')]//e3e-readonly-input"));

        public static IWebLocator MatterNo => L(
            "MatterNo",
            By.XPath("//input[contains(@data-automation-id,'Matter')]"));

        public static IWebLocator DocumentNumber => L(
          "DocumentNumber",
          By.XPath("//input[contains(@data-automation-id,'DocumentNumber')]"));

        public static IWebLocator Amount => L(
          "Amount",
          By.XPath("//input[contains(@data-automation-id,'Amount')]"));

        public static IWebLocator ClientAccountReceiptDetailCard => L(
        "ClientAccountReceiptDetail",
        By.XPath("//mat-card[contains(text(),'Client Account Receipt Detail')]"));

        public static IWebLocator TrustIntendedUse => L(
         "TrustIntendedUse",
         By.XPath("//input[contains(@data-automation-id,'TrustIntendedUse')]"));

        public static IWebLocator ReasonComment => L(
       "ReasonComment",
       By.XPath("//textarea[contains(@data-automation-id,'Comment')]"));

        public static IWebLocator ReversedCheckbox => L(
       "ReversedCheckbox",
       By.XPath("//div//mat-checkbox[contains(@data-automation-id,'IsReversed')]"));


    }
}
