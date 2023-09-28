using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.FeeEarner
{
    public class FeeEarnerLocators
    {
        public static IWebLocator Entity => L(
            "Entity",
            By.XPath("//input[contains(@name,'Entity')]"));

        public static IWebLocator Status => L(
            "Status",
            By.XPath("//input[contains(@name,'Status')]"));

        public static IWebLocator Office => L(
            "Office",
            By.XPath("//input[contains(@name,'Office')]"));

        public static IWebLocator Department => L(
            "Department",
            By.XPath("//input[contains(@name,'Department')]"));

        public static IWebLocator Section => L(
            "Section",
            By.XPath("//input[contains(@name,'Section')]"));

        public static IWebLocator Title => L(
            "Title",
            By.XPath("//input[contains(@name,'Title')][not(contains(@name, 'HR'))]"));

        public static IWebLocator RateClass => L(
            "RateClass",
            By.XPath("//input[contains(@name,'RateClass')]"));

        public static IWebLocator RateType => L(
            "RateType",
            By.XPath("//input[contains(@name,'RateType')]"));

        public static IWebLocator Currency => L(
            "Currency",
            By.XPath("//input[contains(@name,'Currency')][not(contains(@name, 'CurrencyLookBackDate'))]"));

        public static IWebLocator DefaultRate => L(
            "DefaultRate",
            By.XPath("//input[contains(@name,'DefaultRate')]"));

        public static IWebLocator DisplayName => L(
           "DisplayName",
           By.XPath("//input[contains(@name,'/DisplayName')]"));

        public static IWebLocator TimeKeeperStatus => L(
           "TimeKeeperStatus",
           By.XPath("//input[contains(@name,'/TkprStatus')]"));

        public static IWebLocator EffectiveDatedInformation => L(
        "BilledOnAccount",
       By.XPath("//mat-card[contains(text(),'Effective Dated Information')]"));
        public static IWebLocator PartnerPoints => L(
        "PartnerPoints",
       By.XPath("//mat-card[contains(text(),'Partner Points')]"));

        public static IWebLocator FeeEarnerRates => L(
        "FeeEarnerRates",
       By.XPath("//mat-card[contains(text(),'Fee Earner Rates')]"));

        public static IWebLocator FeeEarnerPracticeGroup => L(
        "FeeEarnerPracticeGroup",
       By.XPath("//mat-card[contains(text(),'Fee Earner Practice Group')]"));

        public static IWebLocator Teams => L(
        "Teams",
       By.XPath("//mat-card[contains(text(),'Teams')]"));


        public static IWebLocator FeeEarnerSchool => L(
        "FeeEarnerSchool",
       By.XPath("//mat-card[contains(text(),'Fee Earner School')]"));

        public static IWebLocator FeeEarnerAccreditation => L(
        "FeeEarnerAccreditation",
       By.XPath("//mat-card[contains(text(),'Fee Earner Accreditation')]"));

        public static IWebLocator FeeEarnerGLNaturalAccounts => L(
        "FeeEarnerGLNaturalAccounts",
       By.XPath("//mat-card[contains(text(),'Fee Earner GL Natural Accounts')]"));
        public static IWebLocator HRData => L(
        "HRData",
       By.XPath("//mat-card[contains(text(),'HR Data')]"));

        public static IWebLocator FTEData => L(
        "FTEData",
       By.XPath("//mat-card[contains(text(),'FTE Data')]"));

        public static IWebLocator MaskOverrideValues => L(
        "MaskOverrideValues",
       By.XPath("//mat-card[contains(text(),'Mask Override Values')]"));

        public static IWebLocator FeeEarnerObjective => L(
        "FeeEarnerObjective",
       By.XPath("//mat-card[contains(text(),'Fee Earner Objective')]"));

        public static IWebLocator FeeEarnerNotes => L(
        "FeeEarnerNotes",
       By.XPath("//mat-card[contains(text(),'Fee earner Notes')]"));

        public static IWebLocator UDF => L(
        "UDF",
       By.XPath("//mat-card[contains(text(),'UDF')]"));
        
        public static IWebLocator PageTitle => L(
        "PageTitle",
       By.XPath("//span[@class='page-title'][text()='Fee earner']"));

        public static IWebLocator TimeKeeperPageTitle => L(
       "PageTitle",
      By.XPath("//span[@class='page-title'][text()='Timekeeper']"));

        public static IWebLocator WorkflowUserLookup => L(
        "WorkflowUserLookup",
       By.XPath("//span[text()='Workflow User']//ancestor::e3e-bound-input//input"));

		public static IWebLocator CoverLetterNarrativeLanguage => L(
        "CoverLetterNarrativeLanguage",
        By.XPath("//input[contains(@name,'/Language') and contains(@name,'TkprEmailBody_ccc')]"));
		
        public static IWebLocator WorkflowCollaboratorsInput => L(
        "WorkflowCollaboratorsInput",
       By.XPath("//input[contains(@data-automation-id,'WFCollaborator_ccc')]"));

        public static IWebLocator TimeKeeperCoverLetterNarrative => L(
        "TimeKeeperCoverLetterNarrative",
       By.XPath("//div[contains(@data-automation-id,'/CoverLetterNarrative')]//div[@class='ql-editor ql-blank']"));

        public static IWebLocator EnteredTimeKeeperCoverLetterNarrative => L(
        "EnteredTimeKeeperCoverLetterNarrative",
        By.XPath("//div[contains(@data-automation-id,'/CoverLetterNarrative')]//div[@class='ql-editor']"));
    }
}
