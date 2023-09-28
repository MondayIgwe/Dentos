using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Client
{
    public class ClientLocators

    {
        public static IWebLocator EntityType => L(
                   "Entity Type",
                   By.XPath("//input[contains(@data-automation-id,'EntityType')]"));

        public static IWebLocator InvoiceSite => L(
                   "InvoiceSite",
                   By.XPath("//input[contains(@data-automation-id,'InvoiceSite')]"));

        public static IWebLocator Name => L(
                   "Name",
                   By.XPath("//input[contains(@data-automation-id,'Name')]"));


        public static IWebLocator SortName => L(
                    "SortName",
                    By.XPath("//input[contains(@data-automation-id,'SortName')]"));



        public static IWebLocator Street => L(
                  "Street",
                  By.XPath("//input[contains(@data-automation-id,'Street')]"));


        public static IWebLocator FirstName => L(
            "First Name",
            By.XPath("//input[contains(@data-automation-id,'FirstName')]"));

        public static IWebLocator LastName => L(
            "Last Name",
            By.XPath("//input[contains(@data-automation-id,'LastName')]"));

        public static IWebLocator FlatFeeTimeTypeInput => L(
           "FlatFeeTimeTypeInput",
           By.XPath("//input[contains(@name,'TimeType')]"));

        public static IWebLocator FlatFeeTimeTypeDropdownIcon => L(
           "FlatFeeTimeTypeDropdownIcon",
           By.XPath("//span[contains(text(),'Flat Fees')]//following::e3e-small-list-input//mat-icon"));

        public static IWebLocator FlatFeeTimeTypeDropdownOption(string text) => L(
          "FlatFeeTimeTypeDropdownOption",
          By.XPath("//mat-option//span[text()='"+text+"']"));

        public static IWebLocator FlatFeeCurrencyInput => L(
        "FlatFeeCurrencyInput",
        By.XPath("//input[contains(@name,'Currency') and contains(@name,'MattFlatFee')]"));

        public static IWebLocator FormatCode => L(
            "Format Code",
            By.XPath("//input[contains(@data-automation-id,'NameFormat')]"));

        public static IWebLocator Relationship => L(
            "Relationship",
            By.XPath("//input[contains(@data-automation-id,'RelType')]"));

        public static IWebLocator Description => L(
            "Description",
            By.XPath("//input[contains(@data-automation-id,'Description')]"));

        public static IWebLocator SiteType => L(
            "SiteType",
            By.XPath("//input[contains(@data-automation-id,'SiteType')]"));
        public static IWebLocator Country => L(
            "Country",
            By.XPath("//input[contains(@data-automation-id,'Country')]"));

        public static IWebLocator Language => L(
           "Language",
           By.XPath("//input[contains(@data-automation-id,'Language')]"));

        public static IWebLocator SiteDescription => L(
            "SiteDescription",
            By.Id("//input[@id='mat-input-332']"));
        public static IWebLocator Entity => L(
           "Entity",
           By.XPath("//input[contains(@data-automation-id,'Entity')]"));

        public static IWebLocator ClientName => L(
           "ClientName",
           By.XPath("//input[contains(@data-automation-id,'DisplayName')]"));

        public static IWebLocator DateOpened => L(
          "DateOpened",
          By.XPath("//input[contains(@data-automation-id,'OpenDate')]"));

        public static IWebLocator DateClosed => L(
         "DateClosed div",
         By.XPath("//div[contains(@name,'CloseDate')]"));

        public static IWebLocator Status => L(
        "Status",
        By.XPath("//input[contains(@data-automation-id,'CliStatusType')]"));


        public static IWebLocator StatusDate => L(
      "StatusDate",
      By.XPath("//input[contains(@data-automation-id,'CliStatusDate')]"));

        public static IWebLocator OpeningFeeEarner => L(
      "OpeningFeeEarner",
      By.XPath("//input[contains(@data-automation-id,'OpenTkpr')]"));



        public static IWebLocator BillingFeeEarner => L(
            "BillingFeeEarner",
            By.XPath("//input[contains(@data-automation-id,'BillTkpr')]"));

        public static IWebLocator EffectiveStDt => L(
            "EffectiveStDt",
            By.XPath("//input[contains(@data-automation-id,'EffStart')]"));

        public static IWebLocator ResponsibleFeeEarner => L(
            "ResponsibleFeeEarner",
            By.XPath("//input[contains(@data-automation-id,'RspTkpr')]"));


        public static IWebLocator SupervisorTimeKeeper => L(
           "SupervisorTimeKeeper",
           By.XPath("//input[contains(@data-automation-id,'SpvTkpr')]"));

        public static IWebLocator Office => L(
          "Office",
          By.XPath("//input[contains(@data-automation-id,'Office')]"));

        public static IWebLocator EffectiveDatedChldForm => L(
          "EffectiveDatedChldForm",
          By.XPath("//h5[contains(text(),'Effective Dated Information')]"));

        public static IWebLocator RelationshipChildForm => L(
          "RelationshipChildForm",
          By.XPath("//h5[contains(text(),'Relationships')]"));

        public static IWebLocator SiteDesc => L(
          "SiteDesc",
          By.XPath("//e3e-bound-input[div/label/span[(text()='Description')]]//e3e-string-input/div/mat-form-field/div/div/div/input"));

        public static IWebLocator CloseChildFormButton => L( 
           "CloseChildFormButton",
           By.CssSelector("button.child-form-tabs-btn.child-form-close-btn.ng-star-inserted.active"));

        public static IWebLocator GlobalVendor => L(
            "GlobalVendorDropDown",
            By.XPath("//input[contains(@data-automation-id,'GlobalVendor_ccc')]"));

        public static IWebLocator GlobalClientNumber => L(
        "Global Client Number",
        By.XPath("//input[contains(@data-automation-id,'GlobalClientReference_ccc')]"));

        public static IWebLocator RiskScore => L(
        "Risk Score",
        By.XPath("(//input[contains(@data-automation-id,'RiskScore')])[2]"));

        public static IWebLocator CreditScoreRating => L(
        "Credit Score Rating",
        By.XPath("//input[contains(@data-automation-id,'CreditScoreRating')]"));

        public static IWebLocator CreditLimit => L(
        "Credit Limit",
        By.XPath("(//input[contains(@data-automation-id,'CreditLimit')])[2]"));

        public static IWebLocator AMLRisk => L(
        "AML Risk",
        By.XPath("//input[contains(@data-automation-id,'AMLRisk')]"));

        public static IWebLocator FinanceRiskScore => L(
        "Finance Risk Score",
        By.XPath("//input[contains(@data-automation-id,'FinanceRiskScore')]"));

        public static IWebLocator Currency => L(
          "Instance_ccc",
        By.XPath("(//input[contains(@data-automation-id, 'Currency')])[2]")); 
        
        public static IWebLocator ClientCurrency => L(
          "Currency",
        By.XPath("//input[contains(@data-automation-id, 'Currency')]"));

        public static IWebLocator ClientForm(string text) => L(
        "ClientEbillingAdditionalInformationForm",
        By.XPath("//mat-card[contains(text(),'" + text + "')]"));

        public static IWebLocator IsEBillingUpdateRequiredCheckbox => L(
             "SetIsEBillingUpdateRequiredCheckbox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsEBillingUpdateRequired_ccc')]/label/div/input"));
        public static IWebLocator ClientDefaultsEntityOffice => L(
         "ClientDefaultsEntityOffice",
         By.XPath("(//input[contains(@data-automation-id,'Office')])[2]"));

        public static IWebLocator Department => L(
         "Department",
         By.XPath("//input[contains(@data-automation-id,'Department')]"));

        public static IWebLocator PTAFees1 => L(
        "PTAFees1",
        By.XPath("//input[contains(@data-automation-id,'PTAFees1')]"));

        public static IWebLocator PTACost1 => L(
        "PTACost1",
        By.XPath("//input[contains(@data-automation-id,'PTACost1')]"));

        public static IWebLocator PTACharge1 => L(
        "PTACharge1",
        By.XPath("//input[contains(@data-automation-id,'PTACharge1')]"));

        public static IWebLocator PTAFees2 => L(
        "PTAFees2",
        By.XPath("//input[contains(@data-automation-id,'PTAFees2')]"));

        public static IWebLocator PTACost2 => L(
        "PTACost2",
        By.XPath("//input[contains(@data-automation-id,'PTACost2')]"));

        public static IWebLocator PTACharge2 => L(
        "PTACharge2",
        By.XPath("//input[contains(@data-automation-id,'PTACharge2')]"));


        public static IWebLocator FormNavigationCards => L(
            "FormNavigationCards",
            By.XPath("//span[text()='Form Navigation']//ancestor::mat-sidenav//mat-card"));

        public static IWebLocator ChildFormAddButton(string childForm, string button) => L(
            "ChildFormAddButton",//span[contains(text(),'Credit Details')]//parent::div//span[contains(text(),'Add')]//parent::button
            By.XPath("//span[contains(text(),'"+ childForm + "')]//parent::div//span[contains(text(),'"+ button + "')]//parent::button"));

        public static IWebLocator CreditDetails_RiskScore => L(
            "CreditDetails_RiskScore",
            By.XPath("//span[contains(text(),'Credit Details')]//ancestor::e3e-form-anchor-view//input[contains(@name,'/RiskScore')]"));

        public static IWebLocator CreditDetails_CreditScoreRating => L(
            "CreditDetails_CreditScoreRating",
            By.XPath("//span[contains(text(),'Credit Details')]//ancestor::e3e-form-anchor-view//input[contains(@name,'/CreditScoreRating')]"));

        public static IWebLocator CreditDetails_CreditLimit => L(
            "CreditDetails_CreditLimit",
            By.XPath("//span[contains(text(),'Credit Details')]//ancestor::e3e-form-anchor-view//input[contains(@name,'/CreditLimit')]"));

        public static IWebLocator CreditDetails_Currency => L(
            "CreditDetails_Currency",
            By.XPath("//span[contains(text(),'Credit Details')]//ancestor::e3e-form-anchor-view//input[contains(@name,'/Currency')]"));

        public static IWebLocator CreditDetails_AMLRisk => L(
            "CreditDetails_AMLRisk",
            By.XPath("//span[contains(text(),'Credit Details')]//ancestor::e3e-form-anchor-view//input[contains(@name,'/AMLRisk')]"));

        public static IWebLocator CreditDetails_FinanceRiskScore => L(
            "CreditDetails_FinanceRiskScore",
            By.XPath("//span[contains(text(),'Credit Details')]//ancestor::e3e-form-anchor-view//input[contains(@name,'/FinanceRiskScore')]"));

        public static IWebLocator AuditCreditDetailsHeader => L(
            "AuditCreditDetailsHeader",
            By.XPath("//div[contains(text(),'Audit Credit Details')][contains(@class,'label')]"));

        public static IWebLocator AuditCreditDetailsExpand => L(
            "AuditCreditDetailsExpand",
            By.XPath("//div[contains(text(),'Audit Credit Details')]//ancestor::e3e-dialog-content//mat-icon[contains(text(),'expand_')]"));

        public static IWebLocator AuditCreditDetailsRows => L(
            "AuditCreditDetailsRows",
            By.XPath("//div[contains(text(),'Audit Credit Details')]//ancestor::e3e-dialog-content//div[@ref='rootWrapperBody']//div[@role='gridcell']//parent::div[@role='row']"));

        public static IWebLocator CreditDetails => L(
            "CreditDetails",
            By.XPath("//mat-card[contains(text(),'Credit Details')]"));

        public static IWebLocator EffectiveDatedInformation => L(
            "EffectiveDatedInformation",
            By.XPath("//mat-card[contains(text(),'Effective Dated Information')]"));
        public static IWebLocator ClientGroup => L(
            "ClientGroup",
            By.XPath("//mat-card[contains(text(),'Client Group')]"));
        public static IWebLocator TimeTypeGroup => L(
             "TimeTypeGroup",
             By.XPath("//mat-card[contains(text(),'Time Type Group')]"));
        public static IWebLocator BillingRulesValidationList => L(
            "BillingRulesValidationList",
            By.XPath("//mat-card[contains(text(),'Billing Rules Validation List')]"));
        public static IWebLocator ClientDefaults => L(
            "CreditDetails",
            By.XPath("//mat-card[contains(text(),'Client Defaults')]"));
        public static IWebLocator BillingContacts => L(
            "BillingContacts",
            By.XPath("//mat-card[contains(text(),'Billing Contacts')]"));
        public static IWebLocator RateExceptionGroup => L(
            "RateExceptionGroup",
            By.XPath("//mat-card[contains(text(),'Rate Exception Group')]"));
        public static IWebLocator RateException => L(
            "RateException",
            By.XPath("//mat-card[contains(text(),'Rate Exception')]"));
        public static IWebLocator TemplateOption  => L(
            "TemplateOption",
            By.XPath("//mat-card[contains(text(),'Template Option ')]"));
        public static IWebLocator MaskOverrideValues => L(
            "MaskOverrideValues",
            By.XPath("//mat-card[contains(text(),'Mask Override Values')]"));
        public static IWebLocator FlatFees => L(
            "FlatFees",
            By.XPath("//mat-card[contains(text(),'Flat Fees')]"));
        public static IWebLocator ProformaAdjustments => L(
            "ProformaAdjustments",
            By.XPath("//mat-card[contains(text(),'Proforma Adjustments')]"));
        public static IWebLocator AlternativeBillingArrangements => L(
            "AlternativeBillingArrangements",
            By.XPath("//mat-card[contains(text(),'Alternative Billing Arrangements')]"));
        public static IWebLocator UDF => L(
            "UDF",
            By.XPath("//mat-card[contains(text(),'UDF')]"));

        public static IWebLocator MatterCardTitle(string cardTitle) => L(
      "ClientCardTitle",  
      By.XPath("//mat-card[contains(text(),'" + cardTitle + "')]"));

        public static IWebLocator ClientNoteType => L(
"ClientNoteType",
   By.XPath("//span[contains(text(),'Client Notes')]//ancestor::e3e-form-anchor-view//span[text()='Note Type']//ancestor::e3e-bound-input//input"));
        public static IWebLocator NextActionOwner => L(
"NextActionOwner",
   By.XPath("//input[contains(@name,'NextActionOwner')]"));

        public static IWebLocator ClientNote => L(
        "ClientNote",
    By.XPath("//span[contains(text(),'Client Notes')]//ancestor::e3e-form-anchor-view//span[text()='Note']//ancestor::e3e-bound-input//textarea"));

    }
}
