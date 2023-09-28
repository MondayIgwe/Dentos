using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Entity
{
    using static Boa.Constrictor.WebDriver.WebLocator;

    public class PersonEntityLocator
    {
        public static IWebLocator EntityType => L(
            "Entity Type",
            By.XPath("//input[contains(@name, 'EntityType')]"));

        public static IWebLocator FirstName => L(
            "First Name",
            By.XPath("//input[contains(@name, 'FirstName')]"));

        public static IWebLocator LastName => L(
            "Last Name",
            By.XPath("//input[contains(@name, 'LastName')]"));

        public static IWebLocator FormatCode => L(
            "Format Code",
            By.XPath("//input[contains(@name, 'NameFormat')]"));

        public static IWebLocator DoB => L(
            "DoB",
            By.XPath("//input[contains(@name, 'BirthDate')]"));
        
        public static IWebLocator CheckName => L(
            "CheckName",
            By.XPath("//span[text()=' Check Name ']"));

        public static IWebLocator SiteType => L(
            "SiteType",
            By.XPath("//input[contains(@name, 'SiteType')]"));

        public static IWebLocator DefaultSite => L(
            "DefaultSite",
            By.XPath("//span[text()='Default Site']/../..//div[contains(@class, 'checkbox-inner')]"));

        public static IWebLocator Country => L(
            "Country",
            By.XPath("//input[contains(@name, 'Country')]"));

        public static IWebLocator Street => L(
            "Street",
            By.XPath("//input[contains(@name, 'Street')]"));

        public static IWebLocator Description => L(
            "Description",
            By.XPath("//input[contains(@name, 'Description')][(contains(@name, 'Site'))][not(contains(@name, 'AddrDescription'))]"));
        public static IWebLocator EntityFirstNameValue => L("To get the first name ", By.XPath("//div[@col-id='FirstName'][@role='gridcell']"));

        public static IWebLocator Language => L("Language dropDown", By.XPath("//input[contains(@name,'Language')]"));
        public static IWebLocator ContactEmail => L("ContactEmail", By.XPath("//mat-card[contains(text(),'Contact Emails')]"));
        public static IWebLocator EmailAddress => L("EmailAdress", By.XPath("//input[contains(@data-automation-id,'EmailAddr')]"));
        public static IWebLocator EmailType => L("EmailType", By.XPath("//input[contains(@data-automation-id,'EmailType')]"));
        public static IWebLocator EmailDescription => L("EmailDescr", By.XPath("//input[contains(@name,'EntityPersonEmail_ccc') and contains(@data-automation-id,'Description')]"));
        public static IWebLocator EmailIsDefaultCheckbox => L("EmailIsDefaultCheckbox", By.XPath("//mat-checkbox[contains(@data-automation-id,'/Is_Default')]//input"));
        public static IWebLocator StartDate => L("StartDate", By.XPath("//input[contains(@name,'EntityPersonEmail_ccc') and contains(@data-automation-id,'StartDate')]"));
        public static IWebLocator EndDate => L("EndDate", By.XPath("//input[contains(@name,'EntityPersonEmail_ccc') and contains(@data-automation-id,'EndDate')]"));

    }
}
