using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Customer
{
    public class FeeEarnerMaintenanceLocator
    {

        public static IWebLocator Entity => L(
            "Entity",
            By.XPath("//input[contains(@name, 'Entity')]"));

        public static IWebLocator DisplayName => L(
            "Name",
            By.XPath("//input[contains(@name,'DisplayName')]"));

        public static IWebLocator Status => L(
            "Status",
            By.XPath("//input[contains(@name,'TkprStatus')]')]"));

        public static IWebLocator SignatureInput => L(
            "Text Box",
            By.XPath("//Input[contains(@data-automation-id,'SelectSignature_ccc')]"));

        public static IWebLocator SignatureLabel => L(
            "Text Box",
            By.XPath("//div[contains(@name,'Signature_ccc')]/ancestor::div[contains(@data-automation-id,'Signature_ccc_BOUND')]//span"));

        public static IWebLocator GetSignatureText => L(
            "Get Signature Text",
            By.XPath("//div[contains(@name,'Signature_ccc')]"));
       

        public static IWebLocator SelectLanguage => L(
            "Language",
            By.XPath("//input[contains(@name,'Language')] [not(contains(@name, 'LocalLanguageName'))]"));
        public static IWebLocator LocalLanguageName => L(
            "LocalLanguage",
            By.XPath("//input[contains(@name,'LocalLanguageName_ccc')]"));

        public static IWebLocator DefaultCurrency => L(
            "CurrencyDefault",
            By.XPath("//div[contains(@name,'RateTypeRel.CurrencyDefault')]"));

        public static IWebLocator DefaultRate => L(
            "Rate",
            By.XPath("//input[contains(@name,'DefaultRate')]"));

        public static IWebLocator FENumberInput => L(
           "FENumberInput",
           By.XPath("//input[contains(@name, 'advancedParameters[TimekeeperDrillDown].where.predicates.2.value')]"));

        public static IWebLocator FeeReport => L(
         "FeeReport",
         By.XPath("//span[.='Fee Earner Detail']"));

        public static IWebLocator MatterDRillDown => L(
      "MatterDRillDown",
      By.XPath("//span[.='MatterDrillDown']"));

        public static IWebLocator MessgaeLogText => L(
      "MessgaeLogText",
      By.XPath("//e3e-report-data-container//div//div//span"));

        public static IWebLocator StartDateLabel => L(
            "StartDateLabel",
            By.XPath("//span[text()='Start Date']"));
        public static IWebLocator NewOfficeName => L(
            "NewOfficeName", 
            By.XPath("//*[@title='Office']/following-sibling::*//*[text()]"));
        public static IWebLocator OldOfficeName => L(
            "OldOfficeName",
            By.XPath("(//*[@title='Office']/following-sibling::*//*[text()])[last()]"));

        public static IWebLocator WithholdingTaxAttribute => L(
       "WithholdingTaxAttribute",
       By.XPath("//e3e-report-bound-control//div//span[text()='Withholding Tax']"));

        public static IWebLocator attributeDiv => L(
   "attributeDiv",
   By.XPath("//span[.='Attribute']"));
        public static IWebLocator Rate => L(
           "Rate input",
           By.XPath("//e3e-money-input//input[contains(@name,'TkprRateDateDet_1')]"));

        public static IWebLocator CurrencyDropdown => L(
    "CurrencyDropdown",
    By.XPath("//input[contains(@data-automation-id,'Currency')]"));

        public static IWebLocator RateDiv => L(
   "RateDiv",
   By.XPath("//div[@aria-rowindex='2']//div[@col-id='Rate']"));

        public static IWebLocator FENumber => L(
           "FENumber",
           By.XPath("//e3e-readonly-input//div[contains(@data-automation-id,'Number')]"));

        public static IWebLocator ReportList => L(
       "ReportList",
       By.XPath("//e3e-report-data-container//div[@class='e3e-report-data-container_control ng-star-inserted']//e3e-report-bound-control//div//span"));

        public const string FeeEarnerRates = "Fee Earner Rates";

        public const string EffectiveDatedRates = "Effective Dated Rates";
        public const string RateDetail = "Rate Details";

        public const string PartnerPoints = "Partner Points";

        public const string BudgetPartnerPoints = "Budget Partner Points";

        public const string PartnerPointsValue = "Partner Points Value";

        public const string BudgetPartnerPointsValue = "Budget Partner Points Value";

        public static IWebLocator PartnerPointsLabel(string text) => L(
            "Rate",
            By.XPath(
                "//span[text()='" + text + "']/ancestor::div[contains(@data-automation-id,'PartnerPoints')]//input"));
    }
}
