using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter
{
    public class MatterLocator
    {
        public static IWebLocator MatterTaxOverride => L(
                "MatterTaxOverride",
        By.XPath("//input[contains(@name,'MatterTaxOverride')]"));
        public static IWebLocator BillionInformationHeader => L(
                "BillionInformationHeader",
        By.XPath("//span[contains(text(),'Billing Information')][not(contains(text(),'Electronic'))]"));

        public static IWebLocator MatterName => L(
                "MatterName",
        By.XPath("//input[contains(@name,'DisplayName')]"));

        public static IWebLocator MatterWIP => L(
                "MatterWIP",
        By.XPath("//input[contains(@data-automation-id,'WPType')]"));

        public static IWebLocator MatterBillEmailTo =>
            L("Bill Email to text box", By.CssSelector("input[name*='BillEmail']"));

        public static IWebLocator MatterTitle => L(
               "MatterTitle",
       By.XPath("//span[contains(@class,'page-title')]"));

        public static IWebLocator ToTaxArea => L(
       "ToTaxArea",
       By.XPath("//input[contains(@data-automation-id,'ToTaxArea')]"));

        public static IWebLocator StatusChangeDate => L(
                "StatusChangeDate",
        By.XPath("//input[contains(@name,'MattStatusDate')]"));

        public static IWebLocator OpenDate => L(
                "OpenDate",
        By.XPath("//input[contains(@name,'OpenDate')]"));

        public static IWebLocator FeeEarnerReadOnlyTextBox => L(
            "Fee Earner read only box",
            By.XPath("//div[contains(@data-automation-id,'OpenTkpr')][@class='input default']"));

        public static IWebLocator RemittanceAccount => L(
                "RemittanceAccount",
        By.XPath("//input[contains(@name,'BankAcct_ccc')]"));

        public static IWebLocator Status => L(
                "Status",
        By.XPath("//input[contains(@name, 'MattStatus')][not(contains(@name, 'MattStatusDate'))]/../..//mat-icon[text()='arrow_drop_down']"));


        public static IWebLocator MatStatus => L(
                "MatStatus",
        By.XPath("//span[text()='Status']//ancestor::e3e-bound-input//*[contains(@name,'/MattStatus')]"));

        public static IWebLocator TimeKeeperIndexDiv => L(
               "TimeKeeperIndexDiv",
       By.XPath("//span[text()='Fee Earner Index']//following::div[contains(@data-automation-id,'TkprNumber') and contains(@data-automation-id,'TkprNumber_BOUND')]"));

        public static IWebLocator CloseDate => L(
                "CloseDate",
        By.XPath("//div[contains(@name,'/CloseDate')]"));

        public static IWebLocator CloseType => L(
                "CloseType",
        By.XPath("//span[text()='Close Type']//ancestor::e3e-bound-input//*[contains(@name,'/MattCloseType')]"));

        public static IWebLocator MatterType => L(
                "MatterType",
        By.XPath("//input[contains(@data-automation-id,'MattType')]"));

        public static IWebLocator MatterAttribute => L(
               "MatterType",
       By.XPath("//input[contains(@data-automation-id,'MattAttribute')]"));

        public static IWebLocator Language => L(
              "Language",
      By.XPath("//input[contains(@data-automation-id,'Language')]"));

        public static IWebLocator MatterCategory => L(
               "MatterType",
       By.XPath("//input[contains(@data-automation-id,'MattCategory')]"));

        public static IWebLocator Office => L(
                "Office",
        By.XPath("//input[contains(@name, 'Office')][contains(@name, 'childObjects')]/../..//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator Rate => L(
                "Rates",
        By.XPath("//span[text()='Rate']/../..//input"));

        public static IWebLocator MatterCurrencyMethod => L(
                "MatterCurrencyMethod",
        By.XPath("//input[contains(@name, 'CurrencyDateList')]"));

        public static IWebLocator MasterMatterCheckbox => L(
                "MasterMatterCheckbox",
        By.XPath("//span[text()='Master Matter']/../..//div[contains(@class, 'checkbox-inner')]"));

        public static IWebLocator Delete => L(
                "Delete",
        By.XPath("//span[text()=' Delete ']"));

        public static IWebLocator AdvancedFindTab => L(
                "AdvancedFindTab",
        By.XPath("//div[text()='Advanced Find']"));

        public static IWebLocator AdvancedSearchCriteria(string column) => L(
                "AdvancedSearchCriteria",
        By.XPath("//mat-select[@data-automation-id='advancedFindWorklist.where.predicates." + column + ".operator']//div[@class='mat-select-arrow']"));


        public static IWebLocator AdvancedLookupSearchCriteria(string column) => L(
                "AdvancedSearchCriteria",
        By.XPath("//mat-select[@data-automation-id='advancedFindLookup.where.predicates." + column + ".operator']//div[@class='mat-select-arrow']"));


        public static IWebLocator AdvancedSearchText(string column) => L(
                "AdvancedSearchText",
        By.XPath("//input[@name='advancedFindWorklist.where.predicates." + column + ".value']"));

        public static IWebLocator AdvancedLookupSearchText(string column) => L(
               "AdvancedSearchText",
       By.XPath("//input[@name='advancedFindLookup.where.predicates." + column + ".value']"));

        public static IWebLocator SearchResults => L(
                "SearchResults",
        By.XPath("//span[@class='total-results-caption']"));


        public static IWebLocator DropDownSelection(string item) => L(
            "DropDownSelection",
            By.XPath("//span[text()=' " + item + " ']"));

        public static IWebLocator AddSubmatters => L(
            "AddSubmatters",
            By.XPath("//span[text()=' Add Submatters ']"));

        public static IWebLocator SplitDescription => L(
            "SplitDescription",
            By.XPath("//input[contains(@name, 'Description')]"));

        public static IWebLocator SplitType => L(
            "SplitType",
            By.XPath("//input[contains(@name, 'SplitType')]"));

        public static IWebLocator Submatter => L(
            "Submatter",
            By.XPath("//input[contains(@name, 'SubMatter')]"));

        public static IWebLocator WarningField => L(
            "WarningField",
            By.XPath("//div[@class='bound-column-container warn']"));

        public static IWebLocator SplitPercentage => L(
            "SplitPercentage",
            By.XPath("//input[contains(@name, 'Percentage')]"));

        public static IWebLocator MatterMaintenanceHeading => L(
            "MatterMaintenanceHeading",
            By.XPath("//h3[text()='Matter Maintenance']"));

        public static IWebLocator FeeEarner => L(
            "FeeEarner",
            By.XPath("//input[contains(@name, 'OpenTkpr')]"));

        public static IWebLocator AdditionalMatterNumberField => L(
           "AdditionalMatterNumber",
           By.XPath("//input[contains(@name, 'AddNumber_ccc')]"));
        public static IWebLocator ClientRelationshipCreditForm => L(
          "ClientRelationshipCreditForm",
          By.XPath("//mat-card[contains(text(),'Client Relationship Credit')]"));
        public static IWebLocator ProjectManagementCreditForm => L(
          "ProjectManagementCreditForm",
          By.XPath("//mat-card[contains(text(),'Project Management Credit')]"));
        public static IWebLocator RelationshipEnhancementCredit => L(
          "RelationshipEnhancementCredit",
          By.XPath("//mat-card[contains(text(),'Relationship Enhancement Credit')]"));

        public static IWebLocator MatterAdditionalInformation => L(
          "MatterEBillingAdditionalInformation",
          By.XPath("//mat-card[contains(text(),'Matter Additional Information')]"));

        public static IWebLocator BillingGroup => L(
          "BillingGroup",
          By.XPath("//mat-card[contains(text(),'Billing Group')]"));
        public static IWebLocator TimeTypeGroup => L(
          "TimeTypeGroup",
          By.XPath("//mat-card[contains(text(),'Time Type Group')]"));
        public static IWebLocator CostTypeGroup => L(
          "CostTypeGroup",
          By.XPath("//mat-card[contains(text(),'Cost Type Group')]"));
        public static IWebLocator ChargeTypeGroup => L(
          "ChargeTypeGroup",
          By.XPath("//mat-card[contains(text(),'Charge Type Group')]"));
        public static IWebLocator EffectiveDatedInformation => L(
          "EffectiveDatedInformation",
          By.XPath("//mat-card[contains(text(),'Effective Dated Information')]"));
        public static IWebLocator ClientRelationshipCredit => L(
          "ClientRelationshipCredit",
          By.XPath("//mat-card[contains(text(),'Client Relationship Credit')]"));

        public static IWebLocator ProjectManagementCredit => L(
         "ProjectManagementCredit",
         By.XPath("//mat-card[contains(text(),'Project Management Credit')]"));
        public static IWebLocator MatterRatesChildForm => L(
         "MatterRatesChildForm",
         By.XPath("//mat-card[contains(text(),'Matter Rates')]"));
        public static IWebLocator RateExceptionGroup => L(
         "RateExceptionGroup",
         By.XPath("//mat-card[contains(text(),'Rate Exception Group')]"));

        public static IWebLocator RateException => L(
        "RateException",
        By.XPath("//mat-card[contains(text(),'Rate Exception')]"));

        public static IWebLocator MatterDisbursementTypeSummarisationOverrides => L(
        "MatterDisbursementTypeSummarisationOverrides",
        By.XPath("//mat-card[contains(text(),'Matter Disbursement Type Summarisation Overrides')]"));

        public static IWebLocator ProformaAdjustments => L(
        "ProformaAdjustments",
        By.XPath("//mat-card[contains(text(),'Proforma Adjustments')]"));

        public static IWebLocator Sites => L(
        "Sites",
        By.XPath("//mat-card[contains(text(),'Sites')]"));
        public static IWebLocator WestlawKeyNumbers => L(
        "WestlawKeyNumbers",
        By.XPath("//mat-card[contains(text(),'Westlaw Key Numbers')]"));
        public static IWebLocator MatterTemplateOption => L(
        "MatterTemplateOption",
        By.XPath("//mat-card[contains(text(),'Matter Template Option')]"));
        public static IWebLocator MaskOverrideValues => L(
        "MaskOverrideValues",
        By.XPath("//mat-card[contains(text(),'Mask Override Values')]"));
        public static IWebLocator MatterBudget => L(
        "MatterBudget",
        By.XPath("//mat-card[contains(text(),'Matter Budget')]"));
        public static IWebLocator MatterPhaseExceptions => L(
        "RateExceptionGroup",
        By.XPath("//mat-card[contains(text(),'Matter Phase Exceptions')]"));
        public static IWebLocator FlatFees => L(
        "FlatFees",
        By.XPath("//mat-card[contains(text(),'Flat Fees')]"));
        public static IWebLocator IndustryGroupsMatter => L(
        "IndustryGroupsMatter",
        By.XPath("//mat-card[contains(text(),'Industry Groups for a Matter')]"));
        public static IWebLocator PracticeTeamsMatter => L(
        "PracticeTeamsMatter",
        By.XPath("//mat-card[contains(text(),'Practice Teams for a Matter')]"));
        public static IWebLocator Case => L(
        "Case",
        By.XPath("//mat-card[contains(text(),'Case')]"));
        public static IWebLocator MatterPayer => L(
        "MatterPayer",
        By.XPath("//mat-card[contains(text(),'Matter Payer')]"));
        public static IWebLocator MatterTaxArticle => L(
        "MatterTaxArticle",
        By.XPath("//mat-card[contains(text(),'Matter Tax Article')]"));
        public static IWebLocator MatterNotes => L(
        "MatterNotes",
        By.XPath("//mat-card[contains(text(),'Matter Notes')]"));
        public static IWebLocator AlternativeBillingArrangements => L(
        "AlternativeBillingArrangements",
        By.XPath("//mat-card[contains(text(),'Alternative Billing Arrangements')]"));
        public static IWebLocator UDF => L(
        "UDF",
        By.XPath("//mat-card[contains(text(),'UDF')]"));
        public static IWebLocator BillingContacts => L(
        "BillingContacts",
        By.XPath("//mat-card[contains(text(),'Billing Contacts')]"));

        public static IWebLocator StartDate => L(
         "AddButton",
         By.XPath(" //input[contains(@data-automation-id,'StartDate')]"));

        public static IWebLocator CostMarkUpField => L(
           "cost",
           By.XPath("//input[contains(@data-automation-id,'CostMarkUpPct_ccc')]"));
        public static IWebLocator GrossMarkUpField => L(
           "gross",
           By.XPath(" //input[contains(@data-automation-id,'GrossWithholdingMarkUpPct_ccc')]"));

        public static IWebLocator TimekeeperName => L(
       "TimekeeperName",
         By.XPath("//input[contains(@data-automation-id,'TkprName')]"));
        public static IWebLocator TimekeeperOffice => L(
         "TimekeeperName",
       By.XPath(" //input[contains(@data-automation-id,'Office')]"));
        public static IWebLocator TimekeeperTitle => L(
        "TimekeeperTitle",
         By.XPath("//input[contains(@data-automation-id,'Title')]"));
        public static IWebLocator GlobalTimekkeper => L(
          "Global Timekeeper",
          By.XPath("//h3[text()='Global Timekeeper']"));
        public static IWebLocator TimekeeperNumber => L(
        "TimekeeperNumber",
        By.XPath("//input[contains(@data-automation-id,'Number')]"));

        public static IWebLocator timeKeeperValue => L(
         "tkvalue", By.XPath("//input[contains(@data-automation-id,'Number')]"));
        public static IWebLocator BillingContactCard => L(
            "CentralBillingCOntact", By.XPath("//mat-card[contains(text(),'Billing Contacts')]"));

        public static IWebLocator MatterPayor => L(
          "matterpayer",
              By.XPath("//mat-card[contains(text(),'Matter Payor') or contains(text(),'Matter Payer') ]"));

        public static IWebLocator MatterCardTitle(string cardTitle) => L(
          "MatterCardTitle", //mat-card[contains(text(),'Matter Notes')]
              By.XPath("//mat-card[contains(text(),'" + cardTitle + "')]"));
        public static IWebLocator CreateContact => L(
          "create",
              By.XPath("//span[contains(text(),'Billing Contacts')]//following::span[text()=' Create Contact ']"));
        public static IWebLocator PayorField => L(
         "payor",
             By.XPath("//input[contains(@data-automation-id,'Payor') and contains(@name,'NewContactPopup_Matter')]"));
        public static IWebLocator MatterPayerField => L(
         "matterpayor",
             By.XPath("//span[contains(text(),'Payor Detail')]//following::input[contains(@data-automation-id,'Payor')]")); 
        
        public static IWebLocator MatterPayorInput => L(
         "Matter Payor Input",
             By.XPath("//input[contains(@data-automation-id,'MattPayorDetail') and contains(@data-automation-id,'attributes/Payor')]"));

        public static IWebLocator MatterPayerDetail => L(
            "MatterPayerDetail",
            By.XPath("//input[contains(@data-automation-id,'MattPayorDetail') and contains(@data-automation-id,'attributes/Payor')]"));
        public static IWebLocator GetInput(string text) => L(
         "payor",
             By.XPath("//input[contains(@data-automation-id,'" + text + "') and  contains(@name,'MatterContacts_ccc')]"));
        public static IWebLocator MatterPayerDateInput => L(
           "MatterPayerDateInput",
          By.XPath("//input[contains(@data-automation-id,'EffDate')]"));
        public static IWebLocator ClickDiv(string text) => L(
      "emailvalue",
          By.XPath(" //div[contains(@col-id,'" + text + "')]//span//div"));
        public static IWebLocator BillingContactSearchIcon(string text) => L(
        "BillingContactSearchIcon",
            By.XPath("//div[@col-id='" + text + "']//span//i"));

        public static IWebLocator ContactPayerInput => L(
      "ContactPayerInput",
          By.XPath("//input[contains(@name,'MatterContacts_ccc')]"));

        public static IWebLocator ClientBillingRulesOptOutCheckBox => L(
        "Client Billing Rules Opt Out Check Box", By.XPath("//mat-checkbox[contains(@data-automation-id,'CliBillingRulesOptOut_ccc')]//input"));

        public static IWebLocator BillSubjectToClientApprovalCheckbox => L(
        "BillSubjectToClientApprovalCheckbox",
         By.XPath("//mat-checkbox[contains(@data-automation-id,'IsSubjectToClientApproval_ccc')]"));

        public static IWebLocator EbillingCheckbox => L(
       "EbillingCheckbox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsEBill')]"));
        public static IWebLocator EbillingCheckedCheckbox => L(
      "EbillingCheckedCheckbox",
       By.XPath("//mat-checkbox[contains(@data-automation-id,'IsEBill_cc')]//input[@aria-checked='true']"));

        public static IWebLocator MatterNoteType => L(
       "MatterNoteType",
           By.XPath("//span[contains(text(),'Matter Notes')]//ancestor::e3e-form-anchor-view//span[text()='Note Type']//ancestor::e3e-bound-input//input"));

        public static IWebLocator NextActionOwner => L(
        "NextActionOwner",
           By.XPath("//input[contains(@name,'NextActionOwner_ccc')]"));

        public static IWebLocator NextActionDate => L(
        "NextActionDate",
           By.XPath("//input[contains(@name,'NextActionDate_ccc')]"));

        public static IWebLocator MatterNote => L(
        "MatterNote",
            By.XPath("//span[contains(text(),'Matter Notes')]//ancestor::e3e-form-anchor-view//span[text()='Note']//ancestor::e3e-bound-input//textarea"));

        public static IWebLocator MatterClientName => L(
        "MatterClientName",
            By.XPath("//div[contains(@name,'Client1.ClientDispName')]"));

        public static IWebLocator Client => L(
        "Client",
            By.XPath("//input[contains(@data-automation-id,'/attributes/Client')]"));

        public static IWebLocator OpeningFE => L(
        "OpeningFE",
            By.XPath("//input[contains(@data-automation-id,'/attributes/OpenTkpr')]"));

        public static IWebLocator MatterStatus => L(
       "MatterStatus",
           By.XPath("//div[contains(@data-automation-id,'MattStatus_BOUND')]/div//input[contains(@name,'MattStatus')]"));

        public static IWebLocator CurrencyDateList => L(
       "CurrencyDateList",
           By.XPath("//input[contains(@name,'/CurrencyDateList')]"));
        public static IWebLocator QuickBillCheckBox => L(
       "Client Billing Rules Opt Out Check Box", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsQuickBill_ccc')]//input"));

        public static IWebLocator InvoiceDistributionMethod => L(
      "InvoiceDistributionMethod",
          By.XPath("//input[contains(@name,'InvDistMethod_ccc')]"));

        public static IWebLocator InvoiceOverride => L(
        "InvoiceOverride",
          By.XPath("//input[contains(@name,'InvoiceOverride')]"));

        public static IWebLocator MatterAttributeCodeInput => L(
       "MatterAttributeCodeInput",
         By.XPath("//input[contains(@data-automation-id,'Code')]"));

        public static IWebLocator MatterAttributeDescriptionInput => L(
       "MatterAttributeDescriptionInput",
         By.XPath("//input[contains(@data-automation-id,'Description')]"));

        public static IWebLocator MatterAttributeFinanceFirstStepCheckbox => L(
      "MatterAttributeFinanceFirstStepCheckbox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsFinanceFirstStep_ccc')]"));

        public static IWebLocator PresentationCurrency => L(
       "PresentationCurrency",
        By.XPath("//input[contains(@name,'PresCurrency_ccc')]"));
        public static IWebLocator ClientReferenceNumber => L(
        "ClientReferenceNumber",
        By.XPath("//div[contains(@data-automation-id,'/ClientReference_ccc')]//input[contains(@data-automation-id,'/attributes/ClientRef')]"));

        public static IWebLocator ClientReferenceStartDate => L(
        "ClientReferenceStartDate",
        By.XPath("//div[contains(@data-automation-id,'/ClientReference_ccc')]//input[contains(@data-automation-id,'/attributes/StartDate')]"));

        public static IWebLocator UDFDecimalValue => L(
       "UDFDecimalValue",
       By.XPath("//input[contains(@data-automation-id,'/UDFDecimal')]"));
        public static IWebLocator UDFLabel => L(
       "UDFLabel",
       By.XPath("//div[contains(@data-automation-id,'/UDFLabel_BOUND')]//div[contains(@data-automation-id,'/UDFLabel')]"));

        public static IWebLocator UDFList => L(
      "UDFList",
      By.XPath("//div[contains(@data-automation-id,'/UDFList_ccc')]"));

        public static IWebLocator BillingContactEntityPerson(string name) => L(
         "BillingContactEntityPerson",
         By.XPath("//div[@col-id='EntityPerson']//span[text()='" + name + "']"));

        public static IWebLocator TargetMatter => L(
        "TargetMatter",
        By.XPath("//input[contains(@data-automation-id,'TargetMatter')]"));

        public static IWebLocator EmailToBill => L(
        "EmailToBill",
        By.XPath("//input[contains(@data-automation-id,'BillEmail')]"));

        public static IWebLocator VolumeDiscountGroup => L(
        "VolumeDiscountGroup",
        By.XPath("//input[contains(@data-automation-id,'VolumeDiscountGroup')]"));

         public static IWebLocator IsLeadVolumeDiscountMatter => L(
        "IsLeadVolumeDiscountMatter",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsLeadVolumeDiscountMatter')]"));

        public static IWebLocator UDFStringInput => L(
       "UDFStringInput",
       By.XPath("//input[contains(@name,'UDFString')]"));

        public static IWebLocator UDFDateInput => L(
       "UDFDateInput",
       By.XPath("//input[contains(@name,'UDFDate')]"));
    }
}
