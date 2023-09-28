using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.MatterGroupEnquiry
{
    public class MatterGroupLocator
    {
        public static IWebLocator ClientInformationDropDown => L(
                "ClientInformationDropDown",
        By.XPath("//span[contains(text(),'Client ‒ ')]/parent::div//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator ClientInformationFullScreen => L(
                "ClientInformationFullScreen",
        By.XPath("//span[contains(text(),'Client ‒ ')]/parent::div//mat-icon[contains(text(),'fullscreen')]"));

        public static IWebLocator ClientInformationDropDownOptions(string option) => L(
                "ClientInformationDropDownOptions",//div[@role='menu']//button[contains(text(),'Client/Matter Notes')]
        By.XPath("//div[@role='menu']//button[contains(text(),'" + option + "')]"));

        public static IWebLocator GridLoc => L(
                "GridValue",
        By.XPath("//e3e-report-data-grid"));

        public static IWebLocator ARHistoryDate => L(
               "ARHistoryDate",
       By.XPath("//span[contains(text(),'A/R History - Date')]"));

        public static IWebLocator TransactionEntry(string transactionType,string invoiceNumber) => L(
              "TransactionEntry",
      By.XPath("//div[text()='"+transactionType+"']//ancestor::tr//a[text()='"+invoiceNumber+"']"));


    }
}
