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
    public class CollectionWorkflowLocators
    {
        public static IWebLocator IsInvoiceAttachCheckBox => L(
        "IsInvAttachCheckBox",
         By.XPath("//mat-checkbox[contains(@data-automation-id,'IsInvAttach_ccc')]/label/div/input"));
        public static IWebLocator ebhInvoicesStatusDateDiv => L(
       "ebhInvoicesStatusDateDiv",
        By.XPath("//div[contains(@name,'eBHInvStatusDateUnbound')]"));
        public static IWebLocator ebhInvoiceStatusDiv => L(
       "ebhInvoiceStatusDiv",
        By.XPath("//div[contains(@name,'eBHInvStatusUnbound')]"));
        public static IWebLocator ebhInvoiceCommentDiv => L(
       "ebhInvoiceCommentDiv",
        By.XPath("//div[contains(@name,'eBHInvCommentUnbound')]"));
        public static IWebLocator CustomLanguageFieldInput => L(
         "CustomLanguageFieldInput",
         By.XPath("//input[contains(@name,'Language_ccc')]"));
        public static IWebLocator CustomLanguageFieldCollectionStepInput => L(
       "CustomLanguageFieldCollectionStepInput",
       By.XPath("//input[contains(@name,'Language_ccc') and contains(@name,'CollectionStep')]"));
        public static IWebLocator CollectorInput => L(
       "CollectorInput",
       By.XPath("//input[contains(@name,'Collector')]"));
        public static IWebLocator CollectionOfficeInput => L(
       "CollectionOfficeInput",
       By.XPath("//input[contains(@name,'CollectionOffice')]"));
        public static IWebLocator BillingOfficeDiv => L(
       "BillingOfficeDiv",
       By.XPath("//e3e-readonly-input//div[contains(@name,'BillingOffice')]"));
        public static IWebLocator EmailBodyTextarea => L(
    "EmailBodyTextarea",
    By.XPath("//textarea[contains(@data-automation-id,'EmailBody_ccc')]"));


    }
}
