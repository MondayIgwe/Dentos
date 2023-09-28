using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;


namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma
{
    public class ProformaGenerationLocator
    {
        public static IWebLocator TransferMatter => L(
           "Transfer Matter",
           By.XPath("//span[text()='Matter']/parent::label/parent::div//e3e-big-list-input//input"));

        public static IWebLocator OpenCloseProformaGenerationGrid => L(
            "Entity Link",
            By.XPath("//span[contains(text(), 'Proforma Generation')]/..//mat-icon[contains(text(), 'expand')]"));

        public static IWebLocator childMenuOptions => L(
            "tab",
            By.XPath("//e3e-form-tabbed-view-menu//mat-icon[contains(text(),'more_vert')]"));

        public static IWebLocator profGeneration => L(
          "tab",
          By.XPath("//span[text()=' Proforma Generation ']"));

        public static IWebLocator profGenerationCard => L(
          "profGenerationCard",
          By.XPath("//mat-card[contains(text(),'Proforma Generation')]"));

        public static IWebLocator profGenerationAdd => L(
       "add",
       By.XPath("//button[text()=' Add ']"));

        public static IWebLocator RootGridContainerProformaGeneration => L(
           "Entity Link",
           By.XPath("//span[contains(text(), 'Proforma Generation')]/../../../..//mat-card[@class='particle-card no-padding mat-elevation-z4 mat-card']"));

        public static IWebLocator Button => L(
            "Button",
            By.CssSelector("button"));

        public static IWebLocator ClearPrintOptionsButton => L(
            "ClearPrintOptionsButton",
            By.XPath("//button//span[text()=' Clear Print Options ']"));

        public static IWebLocator MatterColumn => L(
          "Entity Link",
          By.CssSelector("div[role='gridcell'][col-id='Matter']"));

        public static IWebLocator ProformaGenerationGridDropDown => L(
            "ProformaGenerationGridDropDown",
            By.XPath("//span[contains(text(), 'Proforma Generation')]//following::mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator MatterCell => L(
          "MatterCell",
            By.XPath("//div[@ref='eBodyViewport']//div[@col-id='Matter']"));

        public static IWebLocator Matter => L(
          "Entity Link",
            By.XPath("//input[contains(@name,'Matter')]"));

        public static IWebLocator MatterInput => L("Proforma Generation Matter Input Text ",
            By.XPath("//span[text()='Matter']/parent::label/following-sibling::div//input"));

        public static IWebLocator Description => L(
           "WorkDate",
           By.XPath("//input[contains(@name,'Description')]"));

        public static IWebLocator SetIncludeOtherProformas => L(
        "CheckBox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsIncludeOtherProforma')]/label/div"));

        public static IWebLocator GetIncludeOtherProformas => L(
        "CheckBox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsIncludeOtherProforma')]/label/div/input"));

        public static IWebLocator ProformaStatus => L(
          "WorkDate",
          By.XPath("//input[contains(@name,'ProfStatus')]"));

        public static IWebLocator ChangeStatusTo => L(
          "WorkDate",
          By.XPath("//input[contains(@name,'/ProfStatusOther')]"));

        public static IWebLocator ToTaxArea => L(
            "ToTaxArea",
            By.XPath("//input[contains(@name, 'ToTaxArea')]"));

        public static IWebLocator ProformaListIsSelected => L(
            "ProformaListIsSelected",
            By.XPath("//div[@col-id='IsSelected']//i[contains(text(), 'check_box_outline_blank')]"));

        public static IWebLocator EditProforma => L(
            "EditProforma",
            By.XPath("//span[text()=' Edit Proforma ']"));

        public static IWebLocator ProformaWorkflowTask => L(
           "ProformaWorkflowTask",
           By.XPath("//div[text()='3E Proforma Workflow']"));

        public static IWebLocator BillingDashboard => L(
            "BillingDashboard",
            By.XPath("//a[text()='Billing']"));

        public static IWebLocator XProformaGeneration => L(
            "XProformaGeneration",
            By.XPath("//a[text()='X Proforma Generation']"));

        public static IWebLocator XProformaGenerationCard => L(
          "XProformaGenerationCard",
          By.XPath("//h3[text()='X Proforma Generation']"));

        public static IWebLocator WFProformaGenerationCard => L(
         "WFProformaGenerationCard",
         By.XPath("//h3[text()='WF Proforma Generation']"));

        public static IWebLocator TemplateName => L(
        "TemplateName",
        By.XPath("//e3e-readonly-input//div[contains(@data-automation-id,'TemplateName')]"));

        public static IWebLocator TemplateNameFormat => L(
       "TemplateNameFormat",
       By.XPath("//e3e-readonly-input//div[contains(@data-automation-id,'TemplateFormat')]"));

        public static IWebLocator TemplateOptions => L(
       "TemplateOptions",
       By.XPath("//span[text()=' PrintOptions ']//ancestor::button"));

        public static IWebLocator DropdownOptions => L(
      "DropdownOptions",
      By.XPath("//mat-option//span"));

        public static IWebLocator TemplateDropdown => L(
      "TemplateDropdown",
      By.XPath("//mat-select[@data-automation-id='Option-Template']"));


        public static IWebLocator DataOverride => L(
        "DataOverride",
        By.XPath("//mat-card[contains(text(),'Date Overrides')]"));

        public static IWebLocator ProformaGeneration => L(
        "ProformaGeneration",
        By.XPath("//mat-card[contains(text(),'Proforma Generation')]"));

        public static IWebLocator CardPredicate => L(
        "CardPredicate",
        By.XPath("//mat-card[contains(text(),'Card Predicate')]"));

        public static IWebLocator ProformaSortingOptions => L(
        "ProformaSortingOptions",
        By.XPath("//mat-card[contains(text(),'Proforma Sorting Options')]"));

        public static IWebLocator ProformaTemplateOptions => L(
        "ProformaTemplateOptions",
        By.XPath("//mat-card[contains(text(),'Proforma Template Options')]"));

        public static IWebLocator MatterCellValue => L(
         "MatterCellValue",
           By.XPath("//div[@ref='eBodyViewport']//div[@col-id='Matter']/span/div/span"));
        public static IWebLocator PopulateButton => L(
         "PopulateButtonInMultiProform",
           By.XPath("//span[text()=' Populate ']"));

        public static IWebLocator WorkflowDiv(string text) => L(
         "WorkflowDiv",
           By.XPath("//div[text()='" + text + "']"));

        public static IWebLocator ProformaBillHeader => L(
         "ProformaBillHeader",
           By.XPath("//h3[text()='WF Proforma Bill']"));

        public static IWebLocator ReturnToBTKCheckbox => L(
        "ReturnToBTKCheckbox",
          By.XPath("//mat-checkbox[contains(@data-automation-id,'IsReturnBTK_ccc')]"));

        public static IWebLocator ProformaPayerCard => L(
         "ProformaPayerCard",
           By.XPath("//mat-card[contains(text(),'Proforma Payers')]"));

        public static IWebLocator BillToProformaPayerCard => L(
            "BillToProformaPayerCard",
            By.XPath("//mat-card[contains(text(),'Proforma Payer')]"));

        public static IWebLocator ProformaPayerInput => L(
         "ProformaPayerInput",
           By.XPath("//div[contains(@data-automation-id,'/attributes/Payor1.DisplayName_BOUND')]//div[contains(@data-automation-id,'/attributes/Payor1.DisplayName')]"));


        public static IWebLocator UpdatePayorButton => L(
        "UpdatePayorButton",
          By.XPath("//button[contains(@data-automation-id,'UpdatePayor_ccc')]"));

        public static IWebLocator PercentageInput => L(
      "PercentageInput",
        By.XPath("//input[contains(@data-automation-id,'Percentage')]"));
     
        public static IWebLocator ReturnToBTKText => L(
         "ReturnToBTKText",
           By.XPath("//span[text()='Return to BTK']"));

        public static IWebLocator InvoiceTypeInput => L(
        "InvoiceTypeInput",
          By.XPath("//input[contains(@data-automation-id,'InvoiceType')]"));

        public static IWebLocator ReloadButton(string formHeader) => L(
         "ReloadButton",
           By.XPath("//span[contains(text(), '" + formHeader + "')]/ancestor::div[contains(@class, 'form-header')]//button[contains(@data-automation-id, 'Reload-dropdown')]"));


        public static IWebLocator ReloadActionsButton(string value) => L(
         "ReloadButton",
           By.XPath("//div[contains(@class,'mat-menu-content')]/child::button[contains(@data-automation-id,'" + value + "')]"));

    }
}
