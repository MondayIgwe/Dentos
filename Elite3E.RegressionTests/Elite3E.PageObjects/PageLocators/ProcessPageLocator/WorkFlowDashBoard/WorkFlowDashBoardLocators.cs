using static Boa.Constrictor.WebDriver.WebLocator;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.WorkFlowDashBoard
{
    public class WorkFlowDashBoardLocators
    {
        public static IWebLocator GeneralJournalApproval => L(
            "WorkflowName : General Journal Approval",
            By.XPath("//div[text()='General Journal Approval']"));

        public static IWebLocator JournalContainerRow(string journal) => L(
            "Locate parent div for Journal ", //div[@title='Test Journal 27/01']/parent::div
            By.XPath("//div[@title='" + journal + "']/parent::div"));

        public static IWebLocator OpenButtonByRow(string rowIndex) => L(
            "Locate open button by row index ",
            By.XPath("//div[@row-index='" + rowIndex + "']/descendant::button"));

        public static IWebLocator RejectGridJournal(string journal) => L(
            "Locate reject journal in the list",
            By.XPath("//div[contains(text(),'Reject')]/ancestor::e3e-workflow-inbox-form//div[@title='" + journal + "']"));

        public static IWebLocator WorkflowInboxHeader() => L(
            "WorkflowInboxHeader",
            By.XPath("//span[text()='Workflow Inbox']"));

        public static IWebLocator WorkflowInboxRights() => L(
            "WorkflowInboxRights",
            By.XPath("//span[text()='Workflow Inbox']//ancestor::particle-panel//div[@ref='eViewport'][contains(@style,'100')]//div[@role='gridcell'][text()][@col-id='workflowName']"));

        public static IWebLocator InboxTimekeeperLeaverCheckedOpenButton => L(
            "InboxTimekeeperLeaverCheckedOpenButton",
            By.XPath("//input[@type='checkbox']//parent::div[contains(@class,'checked')]//ancestor::div[@role='row']//button"));

        public static IWebLocator ApprovalRequiredGridPaginationTotalPages => L(
           "Pagination_TotalPages",
           By.XPath("(//span[@data-automation-id='Pagination_TotalPages'])[1]"));
        public static IWebLocator ApprovalRequiredGridPaginationNextPage => L(
       "PaginationNextPage",
       By.XPath("(//button[@data-automation-id='Pagination_NextPage'])[1]"));

        public static IWebLocator ClientAccountDisbursement => L(
       "WorkflowName : Client Account Disbursement",
       By.XPath("//div[text()='Client Account Disbursement']"));

       public static IWebLocator ClientAccountTransfer => L(
     "WorkflowName : Client Account Transfer",
      By.XPath("//div[text()='Client Account Transfer']"));

        public static IWebLocator SetDispatchOpenButton => L(
        "SetDispatchOpenButton",
        By.XPath("//div[text()='Set Dispatch Date ']/ancestor::div[@class='workflow-inbox-form']//div[@role='gridcell']//button//span[text()=' Open ']"));

        public static IWebLocator BillingWorkflowTask => L(
        "BillingWorkflowTask",
        By.XPath("//div[text()='Billing Workflow']"));

        public static IWebLocator InvoiceDispatchWorkflowTask => L(
"InvoiceDispatchWorkflowTask",
By.XPath("//div[text()='Invoice Dispatch']"));

        public static IWebLocator RouteRTKOpenButton => L(
            "RouteRTKOpenButton",
            By.XPath("//div[text()='Route to Responsible Timekeeper ']/ancestor::div[@class='workflow-inbox-form']//div[@role='gridcell']//button//span[text()=' Open ']"));

        public static IWebLocator RouteFinanceOpenButton => L(
    "RouteFinanceOpenButton",
    By.XPath("//div[text()='Route to Finance Support ']/ancestor::div[@class='workflow-inbox-form']//div[@role='gridcell']//button//span[text()=' Open ']"));

        public static IWebLocator RouteNotSetFinanceOpenButton => L(
        "RouteNotSetFinanceOpenButton",
        By.XPath("//div[text()='Invoice Dispatch Method not set ']/ancestor::e3e-workflow-inbox-form//div[@role='gridcell']//button//span[text()=' Open ']"));
    }
}

