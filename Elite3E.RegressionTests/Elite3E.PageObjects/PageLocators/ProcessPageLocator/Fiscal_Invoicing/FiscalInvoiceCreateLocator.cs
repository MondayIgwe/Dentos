using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing
{
   public class FiscalInvoiceCreateLocator
    {
        public static IWebLocator ProformaInvoice => L(
            "Payor",
            By.XPath("//input[contains(@name,'InvMaster')]"));
        public static IWebLocator TaxDate => L(
            "TaxDate",
            By.XPath("//input[contains(@name,'FiscalTaxDate')]"));
        public static IWebLocator GlDate => L(
            "GlDate",
            By.XPath("//input[contains(@name,'FiscalGLDate')]"));
        public static IWebLocator CurrencyDate => L(
            "CurrencyDate",
            By.XPath("//input[contains(@name,'FiscalCurrencyDate')]"));
        
        public static IWebLocator ProformaInvoiceSearch => L(
            "ProformaInvoiceSearch",
            By.XPath("//span[text()='Proforma Invoice']/../..//mat-icon[text()='search']"));
        
        public static IWebLocator ProformaInvoiceSearchInput => L(
            "ProformaInvoiceSearchInput",
            By.XPath("//mat-form-field[@floatplaceholder='never']//input"));

        public static IWebLocator Search => L(
            "Search",
            By.XPath("//span[text()=' SEARCH ']"));
        
        public static IWebLocator SelectSearchResult => L(
            "SelectSearchResult",
            By.XPath("//mat-icon[text()='radio_button_unchecked']"));
        
        public static IWebLocator Select => L(
            "Select",
            By.XPath("//span[text()=' SELECT ']"));
    }
}
