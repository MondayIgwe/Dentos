using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Timekeeper
{
    public class TimeKeeperLeaverChecklistLocators
    {
        public static IWebLocator MultiAttachmentsButtons(string button) => L(
            "AttachmentsButton",//options: Attachments, OK
            //span[contains(text(),'Attachments')]//parent::button
            By.XPath("//span[contains(text(),'"+button+"')]//parent::button"));
        public static IWebLocator CloseAttachmentsButton => L(
            "CloseAttachmentsButton",
            By.XPath("//div[contains(text(),'Attachments')]//ancestor::mat-dialog-container//button[contains(@data-automation-id,'close')]"));
        public static IWebLocator AddFile => L(
            "AddFile",
            By.XPath("//button[@data-automation-id = 'addFile']"));
        public static IWebLocator UploadButton => L(
            "UploadButton",
            By.XPath("//mat-icon[text()='file_upload']//ancestor::button"));
        public static IWebLocator InputFile => L(
            "InputFile",
            By.XPath("//input[@data-automation-id = 'inputFile']//following-sibling::input"));
        public static IWebLocator UploadedFileValidation(string filename) => L(
            "UploadedFileValidation",//div[text()=' Attachments ']//ancestor::e3e-dialog-content//span[contains(text(),'TextToUpload.txt')]
            By.XPath("//div[text()=' Attachments ']//ancestor::e3e-dialog-content//span[contains(text(),'"+ filename + "')]"));
        public static IWebLocator AttachmentsNumber => L(
            "AttachmentsNumber",
            By.XPath("//span[contains(text(),'Attachments')]//following-sibling::span"));
        public static IWebLocator CancelDialogYesButton=> L(
            "CancelDialogYesButton",
            By.XPath("//button[@data-automation-id='cancel-dialog-yes-button']"));
        public static IWebLocator ChildVerticalMenuButton => L(
            "ChildVerticalMenuButton",
            By.XPath("//e3e-form-tabbed-view-menu//mat-icon[contains(text(),'more_vert')]//parent::button"));
        public static IWebLocator ChildVerticalMenuOptionLabels => L(
            "ChildVerticalMenuOptionLabels",
            By.XPath("//div[@class='mat-menu-content']//span[text()][@class='mat-checkbox-label']"));
        public static IWebLocator OfficeConfigurationRequiredFields => L(
            "OfficeConfigurationRequiredFields",
            By.XPath("//span[contains(@class,'required-indicator')]//following-sibling::span"));
        public static IWebLocator LeaverClerkInput => L(
            "LeaverClerkInput",
            By.XPath("//input[contains(@name,'/FinanceClerk')]"));
        public static IWebLocator ReadOnlyFeeEarnerName => L(
            "ReadOnlyFeeEarnerName",
            By.XPath("//div[contains(@name,'/LeaverRel.DisplayName')]"));
        public static IWebLocator ReadOnlyOffice => L(
            "ReadOnlyOffice",
            By.XPath("//div[contains(@name,'/LeaverRel.TkprDate.Office1.Description')]"));
        public static IWebLocator ReadOnlyDesc => L(
            "ReadOnlyDesc",
            By.XPath("//div[contains(@name,'/LeaverRel.TkprDate.Department1.Description')]"));
        public static IWebLocator ReadOnlySection => L(
            "ReadOnlySection",
            By.XPath("//div[contains(@name,'/LeaverRel.TkprDate.Section1.Description')]"));
        public static IWebLocator ReadOnlyTitle => L(
            "ReadOnlyTitle",
            By.XPath("//div[contains(@name,'/LeaverRel.TkprDate.Title1.Description')]"));

        public static IWebLocator NextActionUserInput => L(
            "NextActionUserInput",
            By.XPath("//*[contains(@name,'/NextActionUser')]"));

        public static IWebLocator ReassignToInput => L(
            "ReassignToInput",
            By.XPath("//input[contains(@name,'/ReassignTo')]"));

        public static IWebLocator NoFurtherActionUserCheckbox(string input) => L(
            "NoFurtherActionUserCheckbox",//mat-checkbox[contains(@data-automation-id,'/IsNoFurtherActionReq')]//input[@type='checkbox']
            By.XPath("//mat-checkbox[contains(@data-automation-id,'/IsNoFurtherActionReq')]"+input));

        public static IWebLocator LeaverReadyToDepartCheckbox(string input) => L(
            "LeaverReadyToDepartCheckbox",//mat-checkbox[contains(@data-automation-id,'/IsReadyToDepart')]//input[@type='checkbox']
            By.XPath("//mat-checkbox[contains(@data-automation-id,'/IsReadyToDepart')]"+input));

        //Checks Locators Checkboxes
        public static IWebLocator TimeChecksCheckBox(string number, string input) => L(
            "TimeChecksCheckBox",//mat-checkbox[contains(@data-automation-id,'/IsTimeCheck2')]//input[@type='checkbox']
            By.XPath("//mat-checkbox[contains(@data-automation-id,'/IsTimeCheck"+number+"')]" + input));
        
        public static IWebLocator MatterChecksCheckBox(string number, string input) => L(
            "MatterChecksCheckBox",
            //Matter checkbox has a different ID format for the first box compared to the second and third
            //mat-checkbox[contains(@data-automation-id,'/IsMatter1Check')]//input[@type='checkbox']
            //mat-checkbox[contains(@data-automation-id,'/IsMatterCheck2')]//input[@type='checkbox']
            By.XPath("//mat-checkbox[contains(@data-automation-id,'/" + (number.Equals("1") ? "IsMatter1Check" : "IsMatterCheck"+number) + "')]" + input));

        public static IWebLocator ExpenseChecksCheckBox(string number, string input) => L(
            "ExpenseChecksCheckBox",//mat-checkbox[contains(@data-automation-id,'/IsExpCheck1')]//input[@type='checkbox']
            By.XPath("//mat-checkbox[contains(@data-automation-id,'/IsExpCheck" + number + "')]" + input));

        //mat-checkbox[contains(@data-automation-id,'/IsWFCheck3')]//input[@type='checkbox']IsWfCheck2
        public static IWebLocator WorkFlowChecksCheckBox(string number, string input) => L(
            "WorkFlowChecksCheckBox",
            //Workflow checkbox has a different case format for the second box compared to the first and third
            //mat-checkbox[contains(@data-automation-id,'/IsWfCheck2')]//input[@type='checkbox']
            //mat-checkbox[contains(@data-automation-id,'/IsWFCheck3')]//input[@type='checkbox']
            By.XPath("//mat-checkbox[contains(@data-automation-id,'/" + (number.Equals("2") ? "IsWfCheck2" : "IsWFCheck" + number) + "')]" + input));

        //Checks Locators Comments
        //Div changes to an INPUT after it's enabled
        public static IWebLocator TimeChecksComment(string number) => L(
            "TimeChecksComment",//*[contains(@name,'/TimeCheck1Comment')]
            By.XPath("//*[contains(@name,'/TimeCheck" + number + "Comment')]"));        
        public static IWebLocator MatterChecksComment(string number) => L(
            "MatterChecksComment",//div[contains(@name,'/MatterCheck2Comment')]
            By.XPath("//*[contains(@name,'/MatterCheck" + number + "Comment')]"));
        public static IWebLocator ExpenseChecksComment(string number) => L(
            "ExpenseChecksComment",//div[contains(@name,'/ExpCheck1Comment')]
            By.XPath("//*[contains(@name,'/ExpCheck" + number + "Comment')]"));
        public static IWebLocator WorkFlowChecksComment(string number) => L(
            "WorkFlowChecksComment",
            //Workflow Comment has a different case for second field
            //div[contains(@name,'/WFCheck1Comment')]
            //*[contains(@name,'/WfCheck2Comment')]
            By.XPath("//*[contains(@name,'/" + (number.Equals("2") ? "WfCheck2" : "WFCheck" + number) + "Comment')]"));

        //Checks Locators Users
        public static IWebLocator TimeChecksUser(string number) => L(
            "TimeChecksUser",//div[contains(@name,'/TimeCheck2User')]
            By.XPath("//div[contains(@name,'/TimeCheck" + number + "User')]"));        
        public static IWebLocator MatterChecksUser(string number) => L(
            "MatterChecksUser",//div[contains(@name,'/MatterCheck2User')]
            By.XPath("//div[contains(@name,'/MatterCheck" + number + "User')]"));
        public static IWebLocator ExpenseChecksUser(string number) => L(
            "ExpenseChecksUser",//div[contains(@name,'/ExpCheck1User')]
            By.XPath("//div[contains(@name,'/ExpCheck" + number + "User')]"));
        public static IWebLocator WorkFlowChecksUser(string number) => L(
            "WorkFlowChecksUser",//div[contains(@name,'/WFCheck1User')]
            By.XPath("//div[contains(@name,'/" + (number.Equals("2") ? "WfCheck2" : "WFCheck" + number) + "User')]"));

        //Checks Locators Dates
        public static IWebLocator TimeChecksDate(string number) => L(
            "TimeChecksDate",//div[contains(@name,'/TimeCheck2Date')]
            By.XPath("//div[contains(@name,'/TimeCheck" + number + "Date')]"));        
        public static IWebLocator MatterChecksDate(string number) => L(
            "MatterChecksDate",//div[contains(@name,'/MatterCheck2Date')]
            By.XPath("//div[contains(@name,'/MatterCheck" + number + "Date')]"));
        public static IWebLocator ExpenseChecksDate(string number) => L(
            "ExpenseChecksDate",//div[contains(@name,'/ExpCheck1Date')]
            By.XPath("//div[contains(@name,'/ExpCheck" + number + "Date')]"));
        public static IWebLocator WorkFlowChecksDate(string number) => L(
            "WorkFlowChecksDate",//div[contains(@name,'/WFCheck1Date')]
            By.XPath("//div[contains(@name,'/" + (number.Equals("2") ? "WfCheck2" : "WFCheck" + number) + "Date')]"));

        public static IWebLocator WorkflowHistoryChildIndexes => L(
            "WorkflowHistoryChildIndexes",
            By.XPath("//h5[text()='Workflow History']//ancestor::mat-tab-group//div[@col-id='index']//span[not(@style)][text()]"));

    }
}
