using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceManager
{
    public class InvoiceManagerLocators
    {
        public static IWebLocator MatterGroupInquiryDropdown => L(
        "MatterGroupInquiry-dropdown",
         By.XPath("//button[contains(@data-automation-id,'MatterGroupInquiry')]//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator FoundSearchCheckBox => L(
       "FoundSearchCheckBox",
        By.XPath("//span/i[contains(text(),'check_box')]"));

        public static IWebLocator Comment => L(
        "Comment",
        By.XPath("//span[text()='Comment']//ancestor::e3e-bound-input//textarea"));

        public static IWebLocator IsShowDoubtfulCheckbox => L(
          "SetDoubtfulCheckbox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsShowDoubtful')]/label/div/input"));

        public static IWebLocator IsShowDisputedCheckbox => L(
        "SetDoubtfulCheckbox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsShowDisputed')]/label/div/input"));

        public static IWebLocator AdvancedLookupSearchCriteria(string column) => L(
        "AdvancedSearchCriteria",
        By.XPath("//mat-select[@data-automation-id='advancedFindWorklist.where.predicates." + column + ".operator']//div[@class='mat-select-arrow']"));

        public static IWebLocator AdvancedSearchCriteria(string column) => L(
        "AdvancedSearchCriteria",
        By.XPath("//mat-select[@data-automation-id='advancedFindWorklist.where.predicates." + column + ".operator']//div[@class='mat-select-arrow']"));

    }
}
