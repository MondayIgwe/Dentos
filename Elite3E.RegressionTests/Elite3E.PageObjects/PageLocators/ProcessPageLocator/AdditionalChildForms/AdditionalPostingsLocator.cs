using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.AdditionalChildForms
{
    public class AdditionalPostingsLocator
    {
        public const string  AdditionalChildFormAnchorView = "//span[contains(@class,'form-title')][contains(text(),'Additional Postings')]/ancestor::e3e-form-anchor-view";

        public static IWebLocator GlType => L(
            "Additional Form Posting GL Type",
            By.XPath(AdditionalChildFormAnchorView + "//input[contains(@data-automation-id,'GLType')]"));

        public static IWebLocator PostingStageList => L(
            "Additional Form Posting Stage List",
            By.XPath(AdditionalChildFormAnchorView + "//input[contains(@data-automation-id,'PostingStageList')]"));

        public static IWebLocator Value => L(
            "Additional Form Value",
            By.XPath(AdditionalChildFormAnchorView + "//input[contains(@data-automation-id,'Value')]"));

        public static IWebLocator DebitGlUnit => L(
            "Additional Form Debit GL Unit",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='DebitMaskRel.GLUnit']"));

        public static IWebLocator DebitGlNatural => L(
            "Additional Form Debit GL Natural",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='DebitMaskRel.GLNatural']"));
        public static IWebLocator DebitGlUnitLocal => L(
            "Additional Form Debit GLUnitLocal",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='DebitMaskRel.GLUnitLocal']"));

        public static IWebLocator DebitGlDepartment => L(
            "Additional Form Debit GL Department",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='DebitMaskRel.GLDepartment']"));

        public static IWebLocator DebitGlSection => L(
            "Additional Form Debit GL Section",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='DebitMaskRel.GLSection']"));

        public static IWebLocator DebitGlOffice => L(
            "Additional Form Debit GL Office",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='DebitMaskRel.GLOffice']"));

        public static IWebLocator DebitGlTimekeeper => L(
            "Additional Form Debit GL Timekeeper",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='DebitMaskRel.GLTimekeeper']"));

        public static IWebLocator DebitMaskSearch => L(
            "Additional Form Debit Mask Search",
            By.XPath(AdditionalChildFormAnchorView + "//div[contains(@data-automation-id,'DebitMask')]//mat-icon"));

        public static IWebLocator CreditGlUnit => L(
            "Additional Form Credit GL Unit",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='CreditMaskRel.GLUnit']"));

        public static IWebLocator CreditGlNatural => L(
            "Additional Form Credit GL Natural",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='CreditMaskRel.GLNatural']"));

        public static IWebLocator CreditGlUnitLocal => L(
            "Additional Form Credit GLUnitLocal",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='CreditMaskRel.GLUnitLocal']"));

        public static IWebLocator CreditGlDepartment => L(
            "Additional Form Credit GL Department",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='CreditMaskRel.GLDepartment']"));

        public static IWebLocator CreditGlSection => L(
            "Additional Form Credit GL Section",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='CreditMaskRel.GLSection']"));

        public static IWebLocator CreditGlOffice => L(
            "Additional Form Credit GL Office",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='CreditMaskRel.GLOffice']"));

        public static IWebLocator CreditGlTimekeeper => L(
            "Additional Form Credit GL Timekeeper",
            By.XPath(AdditionalChildFormAnchorView + "//input[@name='CreditMaskRel.GLTimekeeper']"));

        public static IWebLocator CreditMaskSearch => L(
            "Additional Form Credit Mask Search",
            By.XPath(AdditionalChildFormAnchorView + "//div[contains(@data-automation-id,'CreditMask')]//mat-icon"));

        public static IWebLocator AnchorMask => L(
            "Additional Form Anchor Mask",
            By.XPath(AdditionalChildFormAnchorView + "//input[contains(@name,'AnchorMask')]"));

        public static IWebLocator NarrativeTextBox => L(
            "Additional Form Narrative Text Box",
            By.XPath(AdditionalChildFormAnchorView + "//textarea[contains(@data-automation-id,'AdditionalPostNarrative_ccc')]"));

    }
}
