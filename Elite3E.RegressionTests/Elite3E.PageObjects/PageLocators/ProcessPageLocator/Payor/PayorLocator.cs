using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor
{
    public class PayerLocator
    {
        public static IWebLocator Entity => L(
            "Description",
            By.XPath("//input[contains(@name,'Entity')]"));
        public static IWebLocator PayerName => L(
            "PayerName",
            By.XPath("//input[contains(@name,'DisplayName')]"));

        public static IWebLocator Site => L(
            "Site",
            By.XPath("//input[contains(@name,'Site') and contains(@name,'StmtSite')=false]"));
        public static IWebLocator TaxArea => L(
            "TaxArea",
                By.XPath("//input[contains(@name,'TaxArea')]"));
        public static IWebLocator CentralBillingCOntact => L(
           "CentralBillingCOntact",
               By.XPath("//mat-card[contains(text(),'Central Billing Contacts')]"));
        public static IWebLocator ContactType => L(
           "ContactType",
               By.XPath("//input[contains(@data-automation-id,'ContactType')]"));
        public static IWebLocator CreateContact => L(
          "createcontact",
              By.XPath(" //span[contains(text(),'Central Billing Contacts')]//following::span[text()=' Create Contact ']"));
        public static IWebLocator FirstName => L(
           "FirstName",
               By.XPath("//input[contains(@data-automation-id,'FirstName')]"));
        public static IWebLocator ContactName => L(
          "ContactName",
              By.XPath("//input[contains(@data-automation-id,'ContactName')]"));

        public static IWebLocator LastName => L(
        "LastName",
      By.XPath("//input[contains(@data-automation-id,'LastName')]"));
        public static IWebLocator EmailSalutation => L(
           "EmailSalutation",
               By.XPath("//input[contains(@data-automation-id,'EmailSalutation')]"));
        public static IWebLocator Email => L(
           "Email",
               By.XPath("//input[contains(@name,'EmailAddr')]"));

        public static IWebLocator GetPayerInput(string text) => L(
         "payor",
        By.XPath("//input[contains(@data-automation-id,'" + text + "') and  contains(@name,'PayorContacts_ccc')]"));

        public static IWebLocator ClickPayerDiv(string text) => L(
         "emailvalue",
          By.XPath("//div[contains(@col-id,'" + text + "')]//span//div"));

        public static IWebLocator PayorUnit => L(
      "PayorUnit",
     By.XPath("//mat-card[contains(text(),'Payor Unit')]"));

        public static IWebLocator BillingContacts => L(
      "BillingContacts",
     By.XPath("//mat-card[starts-with(text(),'Billing Contacts')]"));

        public static IWebLocator TaxIDOneInput => L(
      "TaxID1Input",
     By.XPath("//input[contains(@data-automation-id,'TaxNum')]"));
        public static IWebLocator TaxIDOneDiv => L(
    "TaxID1Div",
   By.XPath("//div[contains(@name,'Payor1.TaxNum')]"));
        public static IWebLocator TaxIDTwoInput => L(
      "TaxID2Input",
     By.XPath("//input[contains(@data-automation-id,'AltNum')]"));
        public static IWebLocator TaxIDTwoDiv => L(
     "TaxID2Div",
    By.XPath("//div[contains(@name,'Payor1.AltNum')]"));
        public static IWebLocator ProfPayerSite => L(
           "ProfPayerSite",
           By.XPath("//input[contains(@name,'Site') and contains(@name,'ProfPayor')]"));
        public static IWebLocator BillingContactPayor => L(
             "Payor",
          By.XPath("//div[contains(text(),' New Contact ')]/following::input[contains(@data-automation-id,'/attributes/Payor')]"));
    }
}
