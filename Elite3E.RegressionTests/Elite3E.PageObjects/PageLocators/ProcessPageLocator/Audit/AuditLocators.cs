using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Audit
{
    public class AuditLocators
    {
        public static IWebLocator SearchResultRowCountInput => L(
            "SearchResultRowCountInput",
            By.XPath("//mat-form-field[contains(@class,'shown-results-field')]//input"));

        public static IWebLocator FormTitles => L(
            "FormTitles",
            By.XPath("//*[contains(@class,'form-title')]"));

        public static IWebLocator FindResultRows => L(
            "QuickFindResultRows",
            By.XPath("//div[contains(@id,'FindContainer')]//e3e-locked-column//ancestor::div[@role='row']"));

        public static IWebLocator ArchitypeAddButton(string architype, string button) => L(
            "ArchitypeAddButton",//*[contains(text(),'Currency Exchange Rates')]//parent::div//span[contains(text(),'Add')]//parent::button
            By.XPath("//*[contains(text(),'" + architype + "')]//parent::div//span[contains(text(),'" + button + "')]//parent::button"));

        public static IWebLocator ArchitypeExpandMoreButton(string architype) => L(
            "ArchitypeExpandMoreButton",//*[contains(text(),'Currency Exchange Rates')]//parent::div//mat-icon[text()='expand_more']
            By.XPath("//*[contains(text(),'" + architype + "')]//parent::div//mat-icon[text()='expand_more']"));

        public static IWebLocator AuditHeader => L(
            "AuditHeader",//div[contains(text(),'Audit')][contains(text(),'Payee Bank')][contains(@class,'label')]
            By.XPath("//div[contains(text(),'Audit')][contains(@class,'label')]"));

        public static IWebLocator AuditCloseButton => L(
            "AuditCreditDetailsCloseButton",
            By.XPath("//ancestor::e3e-dialog-content//span[contains(text(),'Close')]//parent::button"));

        public static IWebLocator AuditChildformButton => L(
            "AuditChildformButton",
            By.XPath("//button[contains(@data-automation-id,'childObjects/Task/actions/Audit')]//span[text()=' Audit ']"));
    }
}
