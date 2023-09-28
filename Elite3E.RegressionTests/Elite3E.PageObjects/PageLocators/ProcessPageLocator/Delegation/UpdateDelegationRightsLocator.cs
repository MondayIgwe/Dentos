using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Delegation
{
   public class UpdateDelegationRightsLocator
    {
        public static IWebLocator DelegateUserToGetRightsInput => L(
            "DelegateUserToGetRightsInput",
            By.XPath("//input[contains(@name,'NxUser')]"));

        public static IWebLocator EffectiveDate => L(
            "Effective Date",
            By.XPath("//input[contains(@name,'EffDate')]"));

        public static IWebLocator AddDelegator => L(
            "AddDelegator",
            By.XPath("//span[contains(text(),'Delegator')]//parent::div//span[contains(text(),'Add')]//parent::button"));

        public static IWebLocator DelegateUserToGiveRightsInput => L(
            "DelegateUserToGiveRightsInput",
            By.XPath("//input[contains(@name,'DelegateUser')]"));

        public static IWebLocator DelegatorAllWorkflowsCheckbox => L(
            "DelegatorAllWorkflowsCheckbox",
            By.XPath("//i[contains(@class,'material-icons')][not(text()='search')]"));

        public static IWebLocator DelegatorWorkflowsRowExpander => L(
            "DelegatorWorkflowsRowExpander",
            By.XPath("//e3e-row-expander//span[contains(@class,'ag-icon')]"));

        public static IWebLocator AddWorkflowRow => L(
            "AddWorkflowRow",
            By.XPath("//span[contains(text(),'Workflow')][not(contains(text(),'Delegation'))]//parent::div//span[contains(text(),'Add')]//parent::button"));

        public static IWebLocator WorkflowRowDropdownInput => L(
            "WorkflowRowDropdownInput",
            By.XPath("//input[contains(@name,'/Workflow')]"));

        public static IWebLocator GrantWorkflowAccessCheckbox(string workflow) => L(
            "WorkflowRowDropdownInput",
            By.XPath("//span[text()='" + workflow + "']//ancestor::div[@role='gridcell']//parent::div[@role='row']//div[@col-id='IsGrant']//i"));

        public static IWebLocator NoRecordsSearchResult => L(
            "NoRecordsSearchResult",
            By.XPath("//div[text()='No Records Matching the Search Criteria']"));

        public static IWebLocator CloseSearchResults => L(
            "CloseSearchResults",
            By.XPath("//mat-icon[text()='close']//ancestor::button"));
    }
}
