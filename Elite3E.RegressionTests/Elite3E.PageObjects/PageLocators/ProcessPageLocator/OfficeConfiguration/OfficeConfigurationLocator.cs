using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.OfficeConfiguration
{
    public class OfficeConfigurationLocator
    {
        public static IWebLocator MatterAttribute=> L(
                "MatterAttribute",
         By.XPath("//input[contains(@name,'/MattAttribute')]"));
        public static IWebLocator InvoiceTextLanguage => L(
        "InvoiceTextLanguage",
        By.XPath("//input[contains(@pendo-id,'/InvoiceText_ccc/rows/attributes/Language')]"));

        public static IWebLocator EnteredCoverLetterNarrative => L(
        "EnteredCoverLetterNarrative",
        By.XPath("//div[contains(@data-automation-id,'/CoverLetterNarrative')]//div[@class='ql-editor']"));

        public static IWebLocator CoverLetterNarrative => L(
        "CoverLetterNarrative",
        By.XPath("//div[contains(@data-automation-id,'/CoverLetterNarrative')]//div[@class='ql-editor ql-blank']"));

        public static IWebLocator EnteredInvoiceNarrative => L(
        "EnteredInvoiceNarrative",
        By.XPath("//div[contains(@data-automation-id,'/InvoiceNarrative')]//div[@class='ql-editor']"));

        public static IWebLocator InvoiceNarrative => L(
        "InvoiceNarrative",
        By.XPath("//div[contains(@data-automation-id,'/InvoiceNarrative')]//div[@class='ql-editor ql-blank']"));

        public static IWebLocator LegalName => L(
        "LegalName",
        By.XPath("//textarea[contains(@data-automation-id,'LegalName') and contains(@data-automation-id,'LegalName_ccc')]"));

        public static IWebLocator LegalNameLanguage => L(
        "LegalNameLanguage",
        By.XPath("//input[contains(@pendo-id,'/LegalName_ccc/rows/attributes/Language')]"));

        public static IWebLocator GovtSysTemplate => L(
        "GovtSysTemplate",
        By.XPath("//input[contains(@name,'GovernmentSystemUploadTemplate')]"));
    }
}
