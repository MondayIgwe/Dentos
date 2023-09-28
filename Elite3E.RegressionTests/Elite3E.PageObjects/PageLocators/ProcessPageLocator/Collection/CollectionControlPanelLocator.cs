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
    public class CollectionControlPanelLocator
    {
        public static IWebLocator ManageCollectionsBy => L(
         "ManageCollectionsBy",
         By.XPath("//input[contains(@name,'CollectionItemLevelType')]"));

        public static IWebLocator CurrencyType => L(
        "CurrencyType",
        By.XPath("//input[contains(@name,'CurrencyType')]"));

        public static IWebLocator CurrencyConversionDateType => L(
       "CurrencyConversionDateType",
       By.XPath("//input[contains(@name,'CurrencyConversionDateType')]"));

        public static IWebLocator DefaultEmailAddr => L(
          "DefaultEmailAddr",
          By.XPath("//input[contains(@name,'DefaultEmailAddr')]"));

        public static IWebLocator PayorAndBillingOffice => L(
         "PayorAndBillingOffice",
         By.XPath("//input[contains(@name,'SearchByPayorBillingOffice_ccc')]"));

        public static IWebLocator OfficeCollectionGroupLink => L(
        "OfficeCollectionGroupLink",
        By.XPath("//mat-card[starts-with(text(),'Office-Collection Group Link')]"));

        public static IWebLocator PracticeGroupCollectionGroupLink => L(
          "PracticeGroupCollectionGroupLink",
          By.XPath("//mat-card[starts-with(text(),'Practice Group-Collection Group Link')]"));

        public static IWebLocator DepartmentCollectionGroupLink => L(
        "DepartmentCollectionGroupLink",
        By.XPath("//mat-card[starts-with(text(),'Department-Collection Group Link')]"));

        public static IWebLocator SectionCollectionGroupLink => L(
        "SectionCollectionGroupLink",
        By.XPath("//mat-card[starts-with(text(),'Section-Collection Group Link')]"));

        public static IWebLocator FeeEarnerCollectionGroupLink => L(
       "FeeEarnerCollectionGroupLink",
       By.XPath("//mat-card[starts-with(text(),'Fee Earner-Collection Group Link')]"));

        public static IWebLocator PayerCollectionGroupLink => L(
       "PayerCollectionGroupLink",
       By.XPath("//mat-card[starts-with(text(),'Payer-Collection Group Link')]"));

        public static IWebLocator ClientCollectionGroupLink => L(
      "ClientCollectionGroupLink",
      By.XPath("//mat-card[starts-with(text(),'Client-Collection Group Link')]"));

        public static IWebLocator MatterCollectionGroupLink => L(
         "MatterCollectionGroupLink",
         By.XPath("//mat-card[starts-with(text(),'Matter-Collection Group Link')]"));

        public static IWebLocator DepartmentOfficeCollectionGroupLink => L(
         "DepartmentOfficeCollectionGroupLink",
         By.XPath("//mat-card[starts-with(text(),'Department/Office-Collection Group Link')]"));

        public static IWebLocator FeeEarnerOfficeCollectionGroupLink => L(
         "FeeEarnerOfficeCollectionGroupLink",
         By.XPath("//mat-card[starts-with(text(),'Fee-earner/Office-Collection Group Link')]"));

        public static IWebLocator ClientOfficeCollectionGroupLink => L(
        "ClientOfficeCollectionGroupLink",
        By.XPath("//mat-card[starts-with(text(),'Client/Office-Collection Group Link')]"));

        public static IWebLocator ClientTypeCollectionGroupLink => L(
       "ClientTypeCollectionGroupLink",
       By.XPath("//mat-card[starts-with(text(),'Client Type-Collection Group Link')]"));

        public static IWebLocator MatterTypeCollectionGroupLink => L(
       "MatterTypeCollectionGroupLink",
       By.XPath("//mat-card[starts-with(text(),'Matter Type-Collection Group Link')]"));

        public static IWebLocator PayorOfficeCollectionGroupLink => L(
       "PayorOfficeCollectionGroupLink",
       By.XPath("//mat-card[starts-with(text(),'Payor/Office-Collection Group Link')]"));
        public static IWebLocator ManageCollectionsByDiv => L(
         "ManageCollectionsByDiv",
         By.XPath("//div[contains(@name,'CollectionItemLevelType')]"));

        public static IWebLocator CurrencyTypeDiv => L(
        "CurrencyTypeDiv",
        By.XPath("//div[contains(@name,'CurrencyType')]"));

        public static IWebLocator CurrencyConversionDateTypeDiv => L(
       "CurrencyConversionDateTypeDiv",
       By.XPath("//div[contains(@name,'CurrencyConversionDateType')]"));

        public static IWebLocator DefaultEmailAddrDiv => L(
          "DefaultEmailAddrDiv",
          By.XPath("//div[contains(@name,'DefaultEmailAddr')]"));

        public static IWebLocator PayorAndBillingOfficeDiv => L(
         "PayorAndBillingOfficeDiv",
         By.XPath("//div[contains(@name,'SearchByPayorBillingOffice_ccc')]"));

        public static IWebLocator Payer => L(
         "Payer",
         By.XPath("//input[contains(@name,'/Payor') and contains (@name,'PayorBillingOffCollGroupLink_ccc')]"));

             public static IWebLocator Office => L(
          "Office",
          By.XPath("//div[@col-id='Office'and @role='gridcell']"));

        public static IWebLocator CollectionGroup => L(
         "CollectionGroup",
         By.XPath("//div[@col-id='CollectionGroup'and @role='gridcell']"));

        public static IWebLocator ManageCollectionsByLevelTye => L(
         "ManageCollectionsByLevelTye",
         By.XPath("//div[@col-id='CollectionItemLevelType'and @role='gridcell']"));

        public static IWebLocator InvoiceAccumulated => L(
         "InvoiceAccumulated",
         By.XPath("//div[@col-id='IsInvoiceAccumulated'and @role='gridcell']"));

        public static IWebLocator SortString => L(
         "SortString",
         By.XPath("//div[@col-id='SortString'and @role='gridcell']"));

        public static IWebLocator Active => L(
         "Active",
         By.XPath("//div[@col-id='IsActive'and @role='gridcell']"));

          public static IWebLocator StartDate => L(
         "StartDate",
         By.XPath("//div[@col-id='StartDate'and @role='gridcell']"));

        public static IWebLocator EndDate => L(
       "EndDate",
       By.XPath("//div[@col-id='EndDate'and @role='gridcell']"));



    }
}
