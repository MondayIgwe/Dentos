using static Boa.Constrictor.WebDriver.WebLocator;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.WorkFlowRuleRouting
{
    public class WorkflowRuleRoutingLocators
    {

        public static IWebLocator WorkFlowRuleCard => L(
            "WorkFlowRuleCard",
            By.XPath("//mat-card[contains(text(),'Workflow Rule Set Routing Details')]"));

        public static IWebLocator RuleSetDropdown => L(
            "RuleSetDropdown",
            By.XPath("//div[@col-id='NxWFRuleSet']//e3e-small-list-cell-editor//input"));

        public static IWebLocator RoleSetInput => L(
           "RoleSetDropdown",
           By.XPath("//input[contains(@data-automation-id,'DOARoleType')]"));

        public static IWebLocator RoleDiv => L(
           "RoleDiv",
           By.XPath("//div[@row-index='0']//div[@col-id='DOARoleType']"));

        public static IWebLocator WorkflowRuleSetHeader => L(
           "RoleSetDropdown",
           By.XPath("//span[text()='Workflow Rule Set']"));

        public static IWebLocator DoARoleTypeHeader => L(
           "RoleSetDropdown",
           By.XPath("//span[text()='DoA Role Type']"));

        public static IWebLocator Role(string text) => L(
           "Role",
           By.XPath("//span[text()='"+text+"']"));

        public static IWebLocator WorkflowRecord(string text) => L(
         "Role",
         By.XPath("//span[@title='" + text + "']"));
        public static IWebLocator RowIndex(string doaRole) => L(
         "RowIndex",
         By.XPath("//span[text()='" + doaRole + "']/ancestor::div[@col-id='DOARoleType'][@role='gridcell']/parent::div"));

        public static IWebLocator RowRecord(string index) => L(
         "RowRecord",
         By.XPath("//div[@role='gridcell']//span[text()='" + index + "']"));

        public static IWebLocator DoaRoleType => L(
       "DoaRoleType",
       By.XPath("//input[contains(@name,'NxWFRuleSet')]/ancestor::div[@col-id]/following-sibling::div[@col-id='DOARoleType']"));

    }
}
