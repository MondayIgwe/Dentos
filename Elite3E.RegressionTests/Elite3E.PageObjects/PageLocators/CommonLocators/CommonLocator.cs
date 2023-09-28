using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.CommonLocators
{
    public static class CommonLocator
    {
        public static IWebLocator AdvanceFindSearchAttribute(string attributeName) => L(
            "AdvanceFindSearchAttribute",//*[contains(@class,'mat-input-element') and @placeholder='Confidential Vendor']
            By.XPath("//*[contains(@class,'mat-input-element') and @placeholder='" + attributeName + "']"));

        public static IWebLocator AdvanceFindSearchAttribute(int rowIndex) => L(
            "AdvanceFindSearchAttribute",
            By.Name("advancedFindWorklist.where.predicates." + rowIndex + ".attribute"));

        public static IWebLocator AdvanceFindSearchOperator(int rowIndex) => L(
            "AdvanceFindSearchAttribute", //mat-select[data-automation-id='advancedFindWorklist.where.predicates.0.operator']
            By.CssSelector("mat-select[data-automation-id='advancedFindWorklist.where.predicates." + rowIndex + ".operator']"));

        public static IWebLocator AdvanceFindSearchOperatorArrow(int rowIndex) => L(
            "AdvanceFindSearchOperatorArrow", //mat-select[@data-automation-id='advancedFindWorklist.where.predicates.0.operator']//div[@class='mat-select-arrow']
            By.XPath("//mat-select[@data-automation-id='advancedFindWorklist.where.predicates." + rowIndex + ".operator']//div[@class='mat-select-arrow']"));

        public static IWebLocator AdvanceFindSearchOperatorOptions(string option) => L(
            "AdvanceFindSearchOperatorOptions", //div[contains(@class,'mat-select-panel')]//span[@class='mat-option-text'][contains(text(),'Not Equal To')]
            By.XPath("//div[contains(@class,'mat-select-panel')]//span[@class='mat-option-text'][contains(text(),'" + option + "')]"));

        public static IWebLocator AdvanceFindSearchValue(int rowIndex) => L(
            "AdvanceFindSearchAttribute",
            By.Name("advancedFindWorklist.where.predicates." + rowIndex + ".value"));

        public static IWebLocator AdvanceFindSearchReslColumn(string columnName) => L(
            "AdvanceFindSearchReslColumn",
            By.XPath("//div[@role='columnheader']//span[text()='" + columnName + "']"));

        public static IWebLocator ViewChildProcessMenuButton => L(
          "ChildProcessMenuButton",
          By.XPath("//button[contains(@class,'child-form-tabs-btn options-menu')]"));

        public static IWebLocator ParentProcessAddButton => L(
            "ParentProcessButton",
            By.XPath("//e3e-process-item/child::div[@class='header-container']//span[contains(text(),'Add')]"));

        public static IWebLocator ParentProcessDeleteButton => L(
            "ParentProcessButton",
            By.XPath("//e3e-process-item/child::div[@class='header-container']//span[contains(text(),'Delete')]"));

        public static IWebLocator ParentProcessUpdateButton => L(
            "ParentProcessButton",
            By.XPath("//e3e-process-item/child::div[@class='header-container']//span[contains(text(),'Update')]"));


        public static IWebLocator AddChildProcessButton => L(
            "AddChildProcessButton",
          //By.XPath("//button[@class='mat-menu-item ng-star-inserted']"));
          By.XPath("//div[@class='cdk-overlay-connected-position-bounding-box']/following-sibling::div//button"));

        public static IWebLocator ActiveProcess => L(
            "Button",
            By.XPath("//e3e-progress[contains(@class,'e3e-progress_page-loader')]"));

        public static IWebLocator Avatar => L("Avatar", By.CssSelector("particle-avatar#theFirstAvatar"));

        public static IWebLocator DisabledSelect => L(
          "DisabledSelect",
          By.XPath("//button[@disabled='true']//span[text()=' SELECT ']"));

        public static IWebLocator SearchFirstRow => L(
          "SearchFirstRow",
          By.XPath("//div[@ref='centerContainer']//div[@role='row' and @row-index='0']"));

        public static IWebLocator MinimizeChildForm => L(
         "MinimizeChildForm",
         By.XPath("//mat-icon[text()='fullscreen_exit']"));

        public static IWebLocator NoSearchRecords => L(
          "NoSearchRecords",
          By.XPath("//div[text()='No Records Matching the Search Criteria']"));

        public static IWebLocator Button(string buttonText) => L(
           "Button",
           By.XPath("//button/span[contains(text(),'" + buttonText + "')]")); //ToDo: Replace with ButtonElementContainsText

        public static IWebLocator ButtonElement => L(
            "Find Button",
            By.CssSelector("button"));

        public static IWebLocator ExpandButton => L(
            "ExpandButton",
            By.XPath("//span//button//mat-icon[text()='expand_more']"));

        public static IWebLocator ButtonElementById(string buttonIdValue) =>
            L(buttonIdValue, //search-title-button
                By.Id(buttonIdValue));

        public static IWebLocator ButtonElementContainsText(string buttonText) => L(
             buttonText,//span[contains(text(),'Enquiry')]
             By.XPath("//span[contains(text(),'" + buttonText + "')]"));
        public static IWebLocator ButtonContainsText(string buttonText) => L(
            buttonText,//span[contains(text(),'Enquiry')]
            By.XPath("//button//span[contains(text(),'" + buttonText + "')]"));
        public static IWebLocator FindButtonTagElementContainsText(string text) => L(
            "VerticalMenu",
            By.XPath("//button[contains(text(),' " + text + "')]"));
        public static IWebLocator ButtonWithIndex(string text, int index) => L(
          "ButtonWithIndex",
          By.XPath("(//span[normalize-space()='" + text + "'])[" + index + "]"));
        public static IWebLocator CancelProxyUserIcon => L("Avatar", By.CssSelector("div.offset-avatar.ng-star-inserted"));

        public static IWebLocator CancelProxyUserCloseButton => L("Cancel Proxy Button", By.CssSelector("button.close.mat-icon-button.ng-star-inserted"));

        public static IWebLocator ChildProcessMenuButton => L(
            "ChildProcessMenuButton",
            By.XPath("//button[contains(@class,'child-form-tabs-btn options-menu')]"));
        public static IWebLocator CloseChildForm => L(
           "CloseChildForm",
      By.XPath(" //button[contains(@class,'child-form-tabs-btn child-form-close-btn ng-star-inserted active')]"));

        public static IWebLocator ChildProcessName(string text) => L(
            "GLForm",
            By.XPath("//button[mat-checkbox/label/span[contains(text(),'" + text + "')]]"));

        public static IWebLocator ChildFormLabels => L(
            "ChildFormLabels",
            By.XPath("//mat-sidenav[contains(@class,'child-nav-menu')]//mat-card"));

        public static IWebLocator ChildFormAction(string formText, string actionButton) => L(
            "Form",
            By.XPath("//span[contains(text(),'" + formText + "')]/following-sibling::e3e-form-collection-actions//span[contains(text(),'" + actionButton + "')]"));

        public static IWebLocator CloseButton => L("Advanced Find Close", By.CssSelector("particle-button[data-automation-id='e3e-dialog-close-button']")); // Todo : Do we need this ?

        public static IWebLocator CloseFlyOutIcon => L(
            "Close Error Icon",
            By.CssSelector("mat-icon[aria-describedby='cdk-describedby-message-6']"));

        public static IWebLocator CloseDialogue => L(
           "CloseDialogue",
           By.XPath("//button//span//mat-icon[text()='close']"));

        public static IWebLocator ParticleCLose => L(
          "ParticleCLose",
          By.XPath("//particle-button[@data-automation-id='e3e-dialog-close-button']"));

        public static IWebLocator DownArrowButtonAfterButton(string buttonText) => L(
            "Entity Link",
            By.XPath("//particle-button-dropdown/div/button[span[contains(text(),'" + buttonText + "')]]//following-sibling::button"));

        public static By ErrorMessages => By.CssSelector("div.message");

        public static IWebLocator ErrorIcon => L("Error Icon", By.CssSelector("div.error-thumbnail-background.ng-star-inserted"));

        public static IWebLocator FindButton => L("FindButton", By.CssSelector("e3e-process-item button"));

        public static IWebLocator FindInputElementUsingText(string text) => L("Input tag", By.XPath("//span[text()='" + text + "']/parent::label/following-sibling::*//input"));
        public static IWebLocator FindInputElementUsingContainsText(string text) => L("Input tag", By.XPath("//input[contains(@name,'" + text + "')] | //span[text()='" + text + "']/parent::label/following-sibling::*//input"));
        public static IWebLocator FindInputElementUsingTextAndIndex(string text, int index) => L("Input tag", By.XPath("(//input[contains(@name,'" + text + "')])[" + index + "]"));

        public static IWebLocator FindDivElementContainsText(string text) => L("Div Tag", By.XPath("//div[contains(text(),'" + text + "')]"));
        public static IWebLocator FindDivElementContainsExactText(string text) => L("Div Tag", By.XPath("//div[contains(text(),'" + text + "')]"));
        public static IWebLocator FindDivElementContainsName(string text) => L("Div Tag", By.XPath("//div[contains(@name,'" + text + "')]"));

        public static IWebLocator SelectDropDown(string text) => L(
            "Office",
            By.XPath(" //input[contains(@name,'" + text + "')]/parent::div/following-sibling::div//mat-icon"));

        public static IWebLocator ProxyAs => L("Proxy As Button", By.XPath("//button[@role='menuitem']//span[.='Proxy As']")); // ToDo:

        public static IWebLocator ProxyUser => L("Proxy Search input", By.XPath("//particle-overflow-menu[@class='show']//mat-form-field//input[@type='search']"));
        public static IWebLocator ProxyUserOption(string user) => L(
            "ProxyUserOption", //div[contains(@id,'proxyUserId_')]//span[text()='FRD003_NoRoles']//parent::button
            By.XPath("//div[contains(@id,'proxyUserId_')]//span[text()='" + user + "']//parent::button"));

        public static IWebLocator YouAreProxyingAs(string username) => L("YouAreProxyingAs", By.XPath("//div[text()='You are proxying as']//parent::div//div[contains(text(),'" + username + "')]"));

        public static IWebLocator SwitchToView(string view) => L("SwitchToView", By.XPath("//mat-icon[contains(text(),'" + view + "')]"));

        public static IWebLocator ThreeEIcon => L("3E Icon", By.CssSelector("particle-toolbar"));

        public static IWebLocator UserSettings => L("3E Icon", By.CssSelector("mat-icon[aria-describedby='User Settings']"));

        public static IWebLocator CloseSideMenu => L("CloseSideMenu", By.XPath("//mat-toolbar//div[@class='logo logo-lg active']"));

        //Side Navigation Menu Buttons
        public static IWebLocator SideNavMenuButtons(string menuItem) => L(
            "navMenuButtonsCollapsed",
            //mat-sidenav[contains(@style,'visible')]//mat-icon[text()='dashboard']//parent::button
            By.XPath("//mat-sidenav[contains(@style,'visible')]//mat-icon[text()='" + menuItem + "']//parent::button"));

        // Dashboard Locators
        public static IWebLocator Entity => L(
            "Entity Link",
            By.CssSelector("a[title='Entity Maintenance']"));

        public static IWebLocator Loading => L(
            "Loading",
            By.CssSelector("div.loading-complete"));

        public static IWebLocator SearchIcon => L("Search Icon", By.CssSelector("div.search-button"));

        public static IWebLocator SearchInput => L("Search Input", By.Id("mat-input-1"));

        public static IWebLocator SearchResults => L("Search Results", By.CssSelector("div.result-item.insert_drive_file span, div.result-item.insert_chart span, div.result-item.dashboard span"));

        public static IWebLocator QuickFindSearchResults(string searchText) => L("Search Results Text",
            By.XPath("//div[contains(text(),'" + searchText + "')]/parent::div"));

        public static IWebLocator SelectResultsByRowId(string searchText, string rowId) => L(
            "Select Search results By Id",
            By.XPath("//div[contains(text(),'" + searchText + "')]/parent::div[@row-id=" + rowId + "]"));

        public static IWebLocator DropDownValueToSelect(string optionToSelect) => L(
            "DropDownValueToSelect",//mat-option[span/span[contains(text(),'FEES')]]
            By.XPath("//mat-option[span/span[contains(text(),'" + optionToSelect + "')]]"));

        public static IWebLocator ExpandDropDownOptionButton(string dataId) => L(
            "DropDownValueToSelect",
            By.XPath("//div[contains(@data-automation-id,'" + dataId + "')]//child::mat-icon"));

        public static IWebLocator DropDownOptions => L(
            "DropDownValueToSelect",
            By.XPath("//mat-option/span/span"));

        public static IWebLocator Homepage => L(
            "Homepage",
            By.XPath("//span[contains(text(),'Home Page')]"));

        //public static IWebLocator SearchTextBox => L("Search TextBox", By.CssSelector("input.mat-input-element.mat-form-field-autofill-control.cdk-text-field-autofill-monitored.ng-pristine.ng-valid.ng-touched"));
        public static IWebLocator SearchTextBox => L("Search TextBox", By.XPath("//div[contains(@class,'dialog')]//div[@class='search-container']//input"));



        //e3e-quickfind-content//input)[1]
        public static IWebLocator DialogueCancel => L(
            "DialogueCancel",
            By.XPath("//button[@data-automation-id='Title-Cancel-Button']"));

        public static IWebLocator Close => L(
            "Select Button",
            By.CssSelector("particle-button[data-automation-id='e3e-dialog-close-button']"));

        public static IWebLocator CloseProforma => L(
           "CloseProforma",
           By.XPath("//button[contains(@data-automation-id,'Close')]"));

        public static IWebLocator CLOSE => L(
            "Close Button",
            By.XPath("//button[@data-automation-id='CLOSE']"));

        public static IWebLocator CloseForm => L(
          "Select Button",
          By.CssSelector("//span[@class='mat-button-wrapper' and text()=' Close ']"));

        public static IWebLocator SearchResultsCheckBox => L(
            "Search Results check box",
            By.XPath("//div[@class ='ag-center-cols-container']//input[@ref='eInput']"));

        public static IWebLocator SearchByInput => L(
            "Search Input",
            By.XPath("//div[@id='pregrid-content']//input[contains(@class, 'mat-input')]"));
        //Ribbon menu locators

        public static IWebLocator RibbonButtonElements => L(
            "RibbonButtonTextList",
            By.XPath("//div[@class='toolbar-buttons']//span[text()]"));

        public static IWebLocator Save => L(
            "Save button",
            By.XPath("//button[@data-automation-id='Save']"));
        public static IWebLocator SaveNote => L(
           "SaveNote",
           By.XPath("(//span[contains(text(),' Save ')])[2]"));

        public static IWebLocator CancelNote => L(
          "CancelNote",
          By.XPath("(//span[contains(text(),' Cancel ')])[2]"));

        public static IWebLocator Terminate => L(
            "Terminate button",
            By.XPath("//button[@data-automation-id='TERMINATE']"));

        public static IWebLocator Approve => L(
            "Submit button",
            By.XPath("//button[@data-automation-id='APPROVE']"));

        public static IWebLocator Reject => L(
            "Submit button",
            By.XPath("//button[@data-automation-id='REJECT']"));

        public static IWebLocator RibbonActionClose => L(
            "RibbonActionClose",
            By.XPath("//button[@data-automation-id='Close']"));
        public static IWebLocator ActionClose => L(
            "RibbonActionClose",
            By.XPath("//button[@data-automation-id='CLOSE']"));

        public static IWebLocator Submit => L(
            "Submit button",
            By.XPath("//button[@data-automation-id='SUBMIT' or @data-automation-id='RELEASE' or @data-automation-id='Release' or @data-automation-id='Submit' or @data-automation-id='Dashboard_Submit_Button']"));

        public static IWebLocator Cancel => L(
            "Cancel Button",
            By.XPath("//button[@data-automation-id='CANCEL' or @data-automation-id='Cancel']"));

        public static IWebLocator SelectButton => L(
            "SelectButton",
            By.XPath("//button//span[text()=' SELECT ']"));

        public static IWebLocator Submitdialog => L(
           "Submitdialog",
           By.XPath("//div[@class='e3e-dialog__content']//button[@id='select-title-button']"));

        public static IWebLocator PostAll => L(
            "PostAll Button",
            By.XPath("//button[@data-automation-id='Post']"));

        public static IWebLocator Generate => L(
            "Generate Button",
            By.XPath("//button[@data-automation-id='Generate']"));

        public static IWebLocator GeneratePreview => L(
            "Generate Button",
            By.XPath("//button[@data-automation-id='GenPreviewCks']"));

        public static IWebLocator SubmitStay => L(
            "SubmitStayButton",
            By.XPath("//button[@data-automation-id='SUBMITSTAY']"));

        public static IWebLocator BillNoPrint => L(
            "Generate Button",
            By.XPath("//button[@data-automation-id='BillNP']"));
        public static IWebLocator XProfomaEditBillNoPrint => L(
       "Generate Button",
       By.XPath("//button[@data-automation-id='Bill_NP']"));

        public static IWebLocator BillGroup => L(
         "BillGroup Button",
         By.XPath("//button[@data-automation-id='BillGroup']"));
        public static IWebLocator ProcExclude => L(
            "Generate Button",
            By.XPath("//button[@data-automation-id='Proc_Excl']"));

        public static IWebLocator Print => L(
            "Generate Button",
            By.XPath("//button[@data-automation-id='Title-Print-Button']"));

        public static IWebLocator PrintReceipt => L(
            "PrintReceipt Button",
            By.XPath("//button[@data-automation-id='Print']"));

        public static IWebLocator PrintJobNameInput => L(
           "PrintJobNameInput",
           By.XPath("//input[@data-automation-id='Option-PrintJobName']"));

        public static IWebLocator PrintToScreenCheckbox => L(
          "PrintToScreenCheckbox",
          By.XPath("//mat-checkbox[contains(@data-automation-id,'Option-PrintToScreen')]"));

        public static IWebLocator PrintTemplateDropdown => L(
          "PrintTemplateDropdown",
          By.XPath("//mat-select[contains(@data-automation-id,'Option-Template')]"));

        public static IWebLocator CancelDialog => L(
            "Cancel Dialog",
            By.CssSelector("e3e-cancel-dialog"));

        public static IWebLocator Yes => L(
            "Yes on Cancel Dialog",
            By.XPath("//button[@data-automation-id='cancel-dialog-yes-button']"));

        public static IWebLocator No => L(
            "No on Cancel Dialog",
            By.XPath("//button[@data-automation-id='cancel-dialog-no-button']"));

        public static IWebLocator RunReport => L(
            "RunReport",
            By.XPath("//button[@data-automation-id='Presentation_RunReport_Button']"));

        public static IWebLocator SelectChildSection = L(
            "Form Navigation",
            By.CssSelector("div.child-nav-card-container div"));

        public static IWebLocator InformationMessage = L(
            "Form Navigation",
            By.CssSelector("span.message"));

        public static IWebLocator VerticalMenu => L(
            "VerticalMenu",
            By.XPath("(//button[span/mat-icon[contains(text(),'more_vert')]])[2]"));

        public static IWebLocator ButtonContainer(string sectionName) => L(
            "ButtonContainer",
            By.XPath("//e3e-form-anchor-view-header[div/span[contains(text(),'" + sectionName + "')]]"));

        public static IWebLocator SelectButtonDialog => L(
           "SelectButtonDialog",
           By.XPath("//button[contains(@class,'e3e-dialog-button')]//span[text()=' Select ']"));

        public static IWebLocator FileInput => L(
                "FileInput",
        By.XPath("//input[@type='file']"));

        public static IWebLocator CalendarIcon(string text) => L(
            "CalendarIcon",//span[text()='End Date']/parent::label/following-sibling::div//button
            By.XPath("//span[text()='" + text + "']/parent::label/following-sibling::div//button"));

        public static IWebLocator CalendarIconWithIndex(string text, int index) => L(
            "CalendarIcon",
            By.XPath("(//span[text()='" + text + "']/parent::label/following-sibling::div//button)[" + index + "]"));

        public static IWebLocator Calendar => L(
            "Calendar",
            By.XPath("//mat-calendar[contains(@class, 'mat-calendar')]"));

        public static IWebLocator CalendarNextMonth => L(
            "CalendarNextMonth",
            By.XPath("//button[@aria-label='Next month']"));

        public static IWebLocator CalendarPreviousMonth => L(
            "CalendarPreviousMonth",
            By.XPath("//button[@aria-label='Previous month']"));

        public static IWebLocator CalendarMonthYear => L(
            "CalendarMonthYear",
            By.XPath("//button[@aria-label='Choose month and year']//span[@class='mat-button-wrapper']"));

        public static IWebLocator CalendarSelectCell(string text) => L(
            "CalendarSelectCell",
            By.XPath("//div[contains(@class, 'mat-calendar-body-cell-content')][text()='" + text + "']"));

        public static IWebLocator TabbedViewChildForm(string text) => L(
            "TabbedViewChildForm",
            By.XPath("//h5[text()='" + text + "']"));

        public static IWebLocator OpenGridFlyOutMenu(string gridTitle, string mainButton) => L(
            "OpenGridFlyOutMenu",
            By.XPath(
                "//span[contains(text(),'" + gridTitle + "')]/ancestor::e3e-form-anchor-view//button[span[contains(text(),'" + mainButton + "')]]/following-sibling::button"));

        public static IWebLocator GridFlyOutButtonClick(string buttonText) => L(
            "GridFlyOutButtonClick",
            By.XPath("//button[span[contains(text(),'" + buttonText + "')]]"));

        public static IWebLocator ViewLastRecordOnGrid(string gridTitle) => L(
            "ViewLastRecordOnGrid",
            By.XPath("//span[contains(text(),'" + gridTitle + "')]/ancestor::e3e-form-anchor-view//button[span/mat-icon[contains(text(),'skip_next')]]"));

        public static IWebLocator Record(string number) => L(
                    "Record",//div[text()='select-title-button']
            By.XPath("//div[text()='" + number + "']"));

        public static IWebLocator GenerateEdit => L(
            "GenerateEdit",
            By.XPath("//button[@data-automation-id='GenerateEdit']"));

        public static IWebLocator Split => L(
            "Split",
            By.XPath("//button[@data-automation-id='Split']"));

        public static IWebLocator Bill => L(
            "Bill",
            By.XPath("//button[@data-automation-id='Bill']"));

        public static IWebLocator Export => L(
           "Export",
           By.XPath("//button[contains(@data-automation-id,'SystemActions_Export_Action')]"));

        public static IWebLocator Okay => L(
          "OK",
          By.XPath("//span[text()= ' Ok ']"));
        public static IWebLocator Update => L(
         "OK",
         By.XPath("//span[text()= ' Update ']"));
        // Code added for Lookup control single select
        public static IWebLocator LookupInput(string fieldLabel) => L(
            "LookupInput",//span[text()='Contact Type']/parent::label/parent::div//e3e-big-list-input//input
            By.XPath("//span[text()='" + fieldLabel + "']/parent::label/parent::div//e3e-big-list-input//input"));

        public static IWebLocator getCheckBox(string fieldLabel) => L(
            "LookupInput",//span[text()='Vendor Merge']/parent::label//following-sibling::*//mat-checkbox//input
            By.XPath("//span[text()='" + fieldLabel + "']/parent::label//following-sibling::*//mat-checkbox//input"));



        public static IWebLocator ColumnLabel(string columnLabel) => L(
            "FieldLabel",//span[contains(text(),'Unit ICB')]/parent::*/parent::*/following-sibling::*//div[contains(@class,'header')]//span[text()='AP Matter']
            By.XPath("//div[contains(@class,'header')]//span[text()='" + columnLabel + "']"));

        public static IWebLocator FieldLabel(string fieldLabel) => L(
            "ColumnLabel",
            By.XPath("//span[text()='" + fieldLabel + "']/parent::label"));


        public static IWebLocator LookupSearchButton(string fieldLabel) => L(
            "LookupSearchButton",
            By.XPath("//span[text()='" + fieldLabel + "']/parent::label/parent::div//e3e-big-list-input//button"));
        public static IWebLocator SelectLookupSearchFirstRow(string value) => L(
            "SelectLookupSearchFirstRow",
            By.XPath("//e3e-dialog-content//div[@role='gridcell'][text()='" + value + "']"));
        public static IWebLocator SelectRow(string rowid) => L(
          "SelectRow",
          By.XPath("//e3e-dialog-content//div[@role='gridcell'][@col-id='Description']/ancestor::div[@row-id='" + rowid + "']"));
        public static IWebLocator LookupSelectButton => L(
            "SelectLookupSearchFirstRow",
            By.XPath("//button[span[contains(text(),'SELECT')]]"));
        public static IWebLocator GetAllTheRowsFromSearchResults => L(
           "GetAllTheRowsFromSearchResults",
           By.XPath("//e3e-dialog-content//div[@role='gridcell'][@col-id][text()]"));
        public static IWebLocator WIPLabel(string fieldLabel) => L(
            "WIPLabel",
            By.XPath("//e3e-form-anchor-view-header//span[contains(text(),'" + fieldLabel + "')]"));

        public static IWebLocator Description => L(
             "Description",
             By.XPath("//input[contains(@name,'Description')]"));
        public static IWebLocator Code => L(
             "Code",
             By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator GetActiveCheckbox => L(
             "SetActiveCheckbox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsActive')]/label/div/input"));

        public static IWebLocator GetDisableIntegrationDiv => L(
            "SetDisableIntegrationdiv", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsDisableIntegration')]/label/div"));

        public static IWebLocator GetIsDisableIntegrationCheckbox => L(
            "SetActiveCheckbox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsDisableIntegration')]/label/div/input"));

        public static IWebLocator DescriptionColumn => L(
             "DescriptionColumn", By.XPath("//div[@col-id='Description' and @role='gridcell']"));

        public static IWebLocator SelectItemSearch => L(
         "SearchCriteria", By.XPath("//textarea[contains(@data-automation-id,'SearchCriteria')]"));

        public static IWebLocator AddButton => L("AddButton", By.CssSelector("button"));

        public static IWebLocator ChildFormBillingRulesRedecription => L("Billing Rules Validation List Descprtion Locator",
                By.XPath("//div[@col-id='EBillValListRel.Description'][@role='gridcell']/descendant::span[@title]"));

        public static IWebLocator HomeDashboard => L("HomeDashboard",
            By.XPath("//particle-menu[@data-automation-id='Nav_BuiltIn_Dashboard']"));

        public static IWebLocator ChildSearchIcon => L(
           "ChildSearchIcon",
           By.XPath("//button[@class='mat-icon-button ng-star-inserted']//span[@class='mat-button-wrapper']//mat-icon[text()='search']"));

        public static IWebLocator ChildFilterIcon(string formName) => L(
       "ChildFilterIcon",
       By.XPath("//span[contains(text(),'" + formName + "')]//following::mat-icon[text()='filter_list']"));

        public static IWebLocator ChildSearchInput => L(
         "ChildSearchInput",
         By.XPath("//div[contains(@class,'ng-trigger-slideInOut')]//input"));

        public static IWebLocator SearchResult(string text) => L(
                    "SearchResult",
            By.XPath("//span[text()='" + text + "']"));

        public static IWebLocator ToastMessage => L("ToastMessage", By.XPath("//span[@class='message']"));

        public static IWebLocator SearchButton => L(
                "SearchButton",
        By.XPath("//button[@id='search-title-button']"));

        public static IWebLocator ProcessToolBarTitle => L("Process Tool Bar Title", By.XPath("//div [@class= 'primary-toolbar']//h3"));
        public static IWebLocator ProcessDialogTitle => L("Process Dialog Title", By.XPath("//div [contains(@class, 'title-bar-label')]"));
        public static IWebLocator FilterButton(string header) => L(
            "FilterButton",
            //div[contains(text(),'GJ Request')]//ancestor::mat-expansion-panel-header//button[@title='Filter']
            By.XPath("//div[contains(text(),'" + header + "')]//ancestor::mat-expansion-panel-header//button[@title='Filter']"));
        public static IWebLocator FilterInput(string header) => L(
            "FilterInput",
            By.XPath("//div[contains(text(),'" + header + "')]//ancestor::mat-expansion-panel-header//input"));

        public static IWebLocator QuickFindExactSearchResults(string searchText) => L("Search Results Text",
         By.XPath("//div[text()='" + searchText + "']/parent::div"));

        public static IWebLocator MetricTableNameInput => L(
           "MetricTableNameInput",
           By.XPath("//input[contains(@name,'/MetricTableName')]"));

        public static IWebLocator FirstRow => L(
           "FirstRow",
           By.XPath("//input[@ref='eInput']//ancestor::div[@role='row' and @row-index='0']"));

        public static IWebLocator ValidateText(string fileName) => L(
        "ValidateFile",
         By.XPath("//span[text()=' " + fileName + " ']"));

        public static IWebLocator ApprovalRequired => L(
            "ApprovalRequiredButton",
            By.XPath("//button[@data-automation-id='APPROVAL_REQUIRED']"));

        public static IWebLocator Return => L(
         "ReturnButton",
         By.XPath("//button[@data-automation-id='RETURN']"));

        public static IWebLocator OkButton => L(
         "OkButton",
         By.XPath("//button[@data-automation-id='OK']"));

        public static IWebLocator Ok => L(
        "OK",
        By.XPath("//button/span[contains(text(),'OK')]"));

        public static IWebLocator ProcessPayments => L(
       "ProcessPayments Button",
       By.XPath("//button[contains(@data-automation-id,'PrintCksWL')]"));

        public static IWebLocator Reconcile => L(
         "Reconcile Button",
         By.XPath("//button[@data-automation-id='Reconcile']"));

        public static IWebLocator ReturnReject => L(
        "ReturnReject Button",
        By.XPath("//button[contains(@data-automation-id,'RETURN_REJECT')]"));

        public static IWebLocator DefaultRate => L(
         "DefaultRate",
         By.XPath("//input[contains(@name,'DefaultRate')]"));

        public static IWebLocator StartDate => L(
        "StartDate",
        By.XPath("//input[contains(@name,'StartDate')]"));

        public static IWebLocator Office => L(
       "Office",
       By.XPath("//input[contains(@name,'Office')]"));

        public static IWebLocator AdvanceFindLookupAttribute(int rowIndex) => L(
         "AdvanceFindLookupAttribute",
         By.Name("advancedFindLookup.where.predicates." + rowIndex + ".attribute"));

        public static IWebLocator AdvanceFindLookupOperator(int rowIndex) => L(
            "AdvanceFindLookupOperator", //mat-select[data-automation-id='advancedFindWorklist.where.predicates.0.operator']
            By.CssSelector("mat-select[data-automation-id='advancedFindLookup.where.predicates." + rowIndex + ".operator']"));

        public static IWebLocator AdvanceFindLookupValue(int rowIndex) => L(
           "AdvanceFindLookupValue",
           By.Name("advancedFindLookup.where.predicates." + rowIndex + ".value"));

        public static IWebLocator AdvanceFindBaseSearchAttribute => L(
       "AdvanceFindBaseSearchAttribute",
       By.XPath("//input[contains(@name,'advancedParameters[BasePostHeader]') and contains(@name,'attribute')]"));

        public static IWebLocator AdvanceFindBaseSearchOperator => L(
        "AdvanceFindBaseSearchOperator",
        By.XPath("//mat-select[contains(@data-automation-id,'advancedParameters[BasePostHeader]') and contains(@aria-label,'Operator')]"));

        public static IWebLocator AdvanceFindBaseSearchValue => L(
        "AdvanceFindBaseSearchValue",
        By.XPath("//input[contains(@name,'advancedParameters[BasePostHeader]') and contains(@name,'value')]"));

        public static IWebLocator SelectByText(string text) => L(
        "AdvanceFindBaseSearchValue",
        By.XPath("//span[text()='" + text + "']"));

        public static IWebLocator Valuetype => L(
       "ValuetypeInput",
       By.XPath("//input[contains(@name,'SegValTypeList')]"));

        public static IWebLocator GridLoc(string text) => L(
               "GridValue",
       By.XPath("//span[contains(text(),'" + text + "')]/following::e3e-report-data-grid"));

        public static IWebLocator FileUploadButton => L(
       "Button",
       By.XPath("//span/mat-icon[text()='file_upload']"));

        public static IWebLocator GridFilterButton => L(
   "Filter Button",
   By.XPath("//mat-icon[contains(text(),'filter_list')]"));

        public static IWebLocator GridFilterInput => L(
            "Filter Input",
            By.XPath("//div[@ref='eHeaderViewport']/ancestor::e3e-form-anchor-view/mat-card/e3e-form-anchor-view-header/div/div/input"));


        public static IWebLocator ProcessPieChart(string chartName) => L(
            "ProcessPieChart",
            By.XPath("//mat-icon[contains(text(),'pie_chart')]/following-sibling::span[contains(text(),'" + chartName + "')]"));

        public static IWebLocator CloseFilter => L(
           "CloseFilter",
           By.XPath("//button//span//mat-icon[text()='close']"));

        public static IWebLocator Unit => L(
        "Unit",
        By.XPath("//input[contains(@data-automation-id,'NxUnit')]"));

        public static IWebLocator Currency => L(
        "Currency",
        By.XPath("//input[contains(@data-automation-id,'Currency')]"));

        public static IWebLocator Sent => L(
        "Sent Button",
        By.XPath("//span[text()= ' Sent ']"));
        public static IWebLocator processSectionName => L(
            "processSectionName",
            By.XPath("//e3e-process-item//span[@class='page-title']"));

        public static IWebLocator FindChildElementUsingText(string text) => L(
            "Input tag",
            By.XPath("//mat-card[contains(text(),'" + text + "')]"));

        public static IWebLocator AdvancedFindTab => L(
        "AdvancedFindTab",
        By.XPath("//div[text()='Advanced Find']"));

        public static IWebLocator AdvancedFindInputAttribute(string text) => L(
        "AdvancedFindInputAttribute",
        By.XPath("//input//following::span[text()='" + text + "']"));
        public static IWebLocator StatusDropDown => L(
        "StatusDropDown",
        By.XPath("//input[contains(@data-automation-id, 'Status')]"));

        public static IWebLocator SearchResultSelect => L(
        "SearchResultSelect",
        By.XPath("//i[contains(text(),'check_box_outline_blank')]"));
        public static IWebLocator SearchResultCount => L(
        "SearchResultCount",
        By.XPath("//h5[text()='Invoice Manager']/parent::div/following-sibling::div"));

        public static IWebLocator ExactSpanText(string text) => L(
        "ExactSpanText", By.XPath("//span[text()='" + text + "']"));

        public static IWebLocator SearchResultTotal => L(
        "SearchResultTotal",
        By.XPath("//span[@class='total-results-caption']"));

        public static IWebLocator InvoiceManagerSearch => L(
         "CloseProforma",
         By.XPath("//button[contains(@data-automation-id,'/LoadInvoiceManagerData')]"));
        public static IWebLocator AdvancedFindResultAttributeHeader(string text) => L(
        "AdvancedFindResultAttributeHeader",
        By.XPath("//div[@ref='eLabel']/span[@ref='eText' and text()='" + text + "']"));

        public static IWebLocator Warning => L(
        "Warning", By.XPath("//i[text()='warning']"));

        public static IWebLocator EmailAddrDiv => L(
         "EmailAddrDiv",
         By.XPath("//input[contains(@name,'EmailAddr')]"));

        public static IWebLocator Searchbtn => L(
         "EmailAddrDiv",
         By.XPath("//button[contains(@data-automation-id,'Search')]"));

    }
}
