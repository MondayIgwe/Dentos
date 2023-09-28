using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceType
{
    public class InvoiceTypeLocator
    {
        public static IWebLocator InvCode => L(
        "InvCode",
         By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator InvDescription => L(
         "InvDescription",
          By.XPath("//input[contains(@name,'Description')]"));

        public static IWebLocator Unit => L(
       "Unit",
        By.XPath("//input[contains(@name,'/Unit')]"));

        public static IWebLocator Office => L(
         "Office",
          By.XPath("//input[contains(@name,'/Office')]"));
        public static IWebLocator OfficeCell => L(
        "OfficeCell",
         By.XPath("//div[@col-id='Office']/span/div"));

        public static IWebLocator InvoiceOverrideValue => L(
       "InvoiceOverrideValue",
        By.XPath("//input[contains(@name,'/InvoiceOverride')]"));
        public static IWebLocator InvoiceOverrideCell => L(
        "InvoiceOverrideCell",
         By.XPath("//div[@col-id='InvoiceOverride']/span/div"));

            public static IWebLocator InvoiceOverrideRows => L(
        "InvoiceOverrideRows",
         By.XPath("//span[contains(text(),'Invoice Override - Unit/Office')]/ancestor::mat-card//div[@role='rowgroup' and @ref='eContainer']/div[@role='row']"));
        
    }
}
