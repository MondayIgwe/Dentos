using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Collection
{
    public class CollectionDetailsLocator
    {
        public static IWebLocator InvoiceCollection => L(
         "InvoiceCollection",
         By.XPath("//div[contains(text(),'Invoice in Collection')]"));

        public static IWebLocator CollectionDetails => L(
            "CollectionDetails",
            By.XPath("//span[contains(text(),'Collection Details')]"));

        public static IWebLocator AdvanceFindLookupAttribute(int rowIndex) => L(
       "AdvanceFindLookupAttribute",
       By.Name("advancedParameters[CollInvoiceComments_ccc].where.predicates." + rowIndex + ".attribute"));

        public static IWebLocator AdvanceFindLookupOperator(int rowIndex) => L(
            "AdvanceFindLookupOperator",
            By.XPath("//mat-select[@data-automation-id='advancedParameters[CollInvoiceComments_ccc].where.predicates." + rowIndex + ".operator']"));

        public static IWebLocator AdvanceFindLookupValue(int rowIndex) => L(
           "AdvanceFindLookupValue",
           By.Name("advancedParameters[CollInvoiceComments_ccc].where.predicates." + rowIndex + ".value"));

        public static IWebLocator Description => L(
      "Description",
      By.XPath("//div[@title='Description']/following-sibling::div//span"));

        public static IWebLocator ParameterHeader => L(
       "ParameterHeader",
       By.XPath("//span[text()='Parameters']"));

        public static IWebLocator CollectionItemLookup => L(
       "CollectionItemLookup",
       By.XPath("//input[contains(@name,'/CollectionItem')]"));

        public static IWebLocator CollectionStatus => L(
       "CollectionStatus",
       By.XPath("//input[contains(@name,'/CollectionStatus')]"));

        public static IWebLocator Payer => L(
        "Payer",
        By.XPath("//input[contains(@name,'/Payors')]"));

        public static IWebLocator Clients => L(
        "Clients",
        By.XPath("//input[contains(@name,'/Clients')]"));

        public static IWebLocator Matters => L(
        "Matters",
        By.XPath("//input[contains(@name,'/Matters')]"));

        public static IWebLocator Invoices => L(
        "Invoices",
        By.XPath("//input[contains(@name,'/Invoices')]"));

        public static IWebLocator BillTimekeeper => L(
         "BillTimekeeper",
         By.XPath("//input[contains(@name,'/BillTimekeeper')]"));

        public static IWebLocator CollectionTotalHeader => L(
       "CollectionTotalHeader",
       By.XPath("//div[@title='Collection Total']//span[text()='Collection Total']"));

        public static IWebLocator RespTimekeeper => L(
 "RespTimekeeper",
 By.XPath("//input[contains(@name,'/RespTimekeeper')]"));

        public static IWebLocator SupvTimekeeper => L(
 "SupvTimekeeper",
 By.XPath("//input[contains(@name,'/SupvTimekeeper')]"));

        public static IWebLocator IncludeInactiveCollectionItemsCheckbox => L(
     "IncludeInactiveCollectionItemsCheckbox",
     By.XPath("//mat-checkbox[contains(@data-automation-id,'IncludeInactiveCollectionItems')]"));

        public static IWebLocator IncludePaidCollectionInvoicesCheckbox => L(
         "IncludePaidCollectionInvoicesCheckbox",
         By.XPath("//mat-checkbox[contains(@data-automation-id,'IncludePaidCollectionInvoices')]"));
        public static IWebLocator IncludeReversedCollectionInvoicesCheckbox => L(
 "IncludeReversedCollectionInvoicesCheckbox",
 By.XPath("//mat-checkbox[contains(@data-automation-id,'IncludeReversedCollectionInvoices')]"));
    }
}
