using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Time
{
    public class TimeEntryLocator
    {
        public static IWebLocator FeeEarner => L(
            "Fee Earner",
            By.XPath("//input[contains(@name, 'Timekeeper')][not(contains(@name, 'SpvTimekeeper'))]"));

        public static IWebLocator Hours => L(
            "Hours",
            By.XPath("//input[contains(@name,'WorkHrs')]"));

        public static IWebLocator WorkRate => L(
            "Work Rate text field",
            By.XPath("//input[contains(@name,'WorkRate')]"));
        public static IWebLocator WorkAmount => L(
            "Work Amount text field",
            By.XPath("//input[contains(@name,'WorkAmt')]"));

        public static IWebLocator Language => L(
            "Language",
            By.XPath("//input[contains(@name,'Language')]"));

        public static IWebLocator Matter => L(
            "Matter",
            By.XPath("//input[contains(@name,'Matter')]"));

        public static IWebLocator WorkListRowOption(string rowNumber) => L(
            "WorkListRowOption",//div[@ref='eBodyViewport']//div[@col-id='indexCol'][text()='1']//parent::div[@aria-label='Grid.ariaRowSelect']
            By.XPath("//div[@ref='eBodyViewport']//div[@col-id='indexCol'][text()='"+rowNumber+"']//parent::div[@aria-label='Grid.ariaRowSelect']"));

        public static IWebLocator Narrative => L(
            "Narrative",
            By.XPath("//span[text()='Narrative']//ancestor::e3e-bound-input//div[contains(@class,'ql-editor')]"));

        public static IWebLocator TaxCode => L(
            "TaxCodeDropDown",
            By.XPath("//input[contains(@name,'/TaxCode')]"));
        
        public static IWebLocator TimeTypeDropDown => L(
            "TimeTypeDropDown",
            By.XPath("//input[contains(@name, 'TimeType')]"));

        public static IWebLocator WorkCurrencyDropDown => L(
            "WorkCurrencyDropDown",
            By.XPath("//input[contains(@name,'Currency')]"));
        
        public static IWebLocator TotalPendingHours => L(
            "Total Pending Hours",
            By.XPath("//div[contains(@name,'TotalPendHrs')]"));

        public static IWebLocator TotalPostedHours => L(
            "Total Posted Hours",
            By.XPath("//div[contains(@name,'TotalPostHrs')]"));
        
        public static IWebLocator TimeType => L(
            "Time Type",
            By.XPath("//input[contains(@name,'TimeType')]"));

        public static IWebLocator WorkType => L(
            "Work Type",
            By.XPath("//input[contains(@name,'WorkType')]"));

        public static IWebLocator InternalComments => L("InternalComments",
            By.XPath("//textarea[contains(@data-automation-id,'InternalComments')]"));

        public static IWebLocator Office => L("Office",
           By.XPath("//textarea[contains(@data-automation-id,'Office')]"));

    }
}
