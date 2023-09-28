using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;


namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payee
{
    public class PayeeLocator
    {
        public static IWebLocator APContactCard => L(
          "Code",
          By.XPath("//mat-card[contains(text(),'AP Contact')]"));
        public static IWebLocator CreateAPContact => L(
          "create",
              By.XPath("//span[contains(text(),'AP Contact')]//following::span[text()=' Create Contact ']"));
        public static IWebLocator GetPayeeInput(string text) => L(
      "payor",
          By.XPath("//input[contains(@data-automation-id,'"+text+"') and  contains(@name,'PayeeContacts_ccc')]"));

        public static IWebLocator ClickPayeeDiv(string text) => L(
      "emailvalue",
          By.XPath(" //div[contains(@col-id,'" + text + "')]//span//div"));

    }
}
