using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.GenearlJournal
{
    public class GeneralJournalLocators
    {
        public static IWebLocator Category => L
             ("General Category", By.XPath("//input[contains(@name,'Category')]"));
        public static IWebLocator Currency => L
            ("General Journal Currency drop down", By.XPath("//input[contains(@name,'Currency')]"));

        public static IWebLocator Journal => L
            ("General Journal Journal Input box", By.XPath("//input[contains(@data-automation-id,'GJTranNum')]"));

        public static IWebLocator Status => L
            (" General Journal Status Read only Input box", By.XPath("//div[contains(@name,'JMRel.JMStatusList')]"));

        public static IWebLocator GlUnit => L(
            "Journal Detail GL Unit", By.XPath("//input[contains(@name,'GLUnit')]"));

        public static IWebLocator GlNatural => L(
            "Journal Detail GL Natural", By.XPath("//input[contains(@name,'GLNatural')]"));
        public static IWebLocator GlUnitLocal => L(
            "Journal Detail GLUnitLocal", By.XPath("//input[contains(@name,'GLUnitLocal')]"));

        public static IWebLocator GlDepartment => L(
            "Journal Detail GL Department", By.XPath("//input[contains(@name,'GLDepartment')]"));

        public static IWebLocator GlSection => L(
            "Journal Detail GL Section", By.XPath("//input[contains(@name,'GLSection')]"));

        public static IWebLocator GlOffice => L(
            "Journal Detail GL Office", By.XPath("//input[contains(@name,'GLOffice')]"));
        public static IWebLocator GlTimekeeper => L(
            "Journal DetailGL Timekeeper", By.XPath("//input[contains(@name,'GLTimekeeper')]"));

        public static IWebLocator OriginalDR => L(
            "Journal Detail Orginal DR", By.XPath("//input[contains(@name,'OrigDR')]"));

        public static IWebLocator OriginalCR => L(
            "Journal Detail Orginal DR", By.XPath("//input[contains(@name,'OrigCR')]"));

        public static IWebLocator GlAccountSearch => L(
             "Journal Details  GlAccount Search", By.XPath("//div[contains(@data-automation-id,'GLAcct')]/following-sibling::div//span/mat-icon"));

        public static IWebLocator GLType => L(
             "GLType", By.XPath("//input[contains(@name,'/GLType')]"));

        public static IWebLocator GLBook => L(
             "GLBook", By.XPath("//input[contains(@name,'/GLBook')]"));
        public static IWebLocator GLBookValue => L(
            "GLBook", By.XPath("//div[contains(@name,'/GLBook')]"));
        public static IWebLocator DescriptionInput => L(
         "DescriptionInput", By.XPath("//input[contains(@data-automation-id,'Description')]"));

    }
}
