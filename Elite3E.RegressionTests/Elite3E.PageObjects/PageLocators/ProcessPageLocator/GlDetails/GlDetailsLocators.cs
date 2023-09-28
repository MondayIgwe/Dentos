using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;


namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.GlDetails
{
    public class GlDetailsLocators
    {
        public static IWebLocator GlDetailSubledgerEnquiryInqChart => L(
            "Gl description UnitOverride description locator",
            By.XPath(" //em[contains(text(),'(GLDetailSubledgerInq)')]/parent::span"));
       
        public static IWebLocator GlDescriptionUnitOverride => L(
            "Gl description UnitOverride description locator",
            By.XPath("//textarea[contains(@data-automation-id,'GLDetailUnitOverride_ccc')]"));

        public static IWebLocator GlDescriptionLanguage => L(
            "Gl description language description locator",
            By.XPath("//textarea[contains(@data-automation-id,'GLDetailDescriptionLanguage_ccc')][not(contains(@data-automation-id,'GLDetailUnitOverride_ccc'))]"));

        public static IWebLocator GlDetailsBillInvoiceSearchButton => L(
            "GlDetails search button",
            By.XPath("//span[contains(text(),'Billing Invoice')]/ancestor::div[contains(@data-automation-id,'/InvMaster_BOUND')]//mat-icon[text()='search']"));


        public static IWebLocator GlDetailsVoucherSearchButton => L(
            "GlDetails Voucher button",
            By.XPath("//span[contains(text(),'Voucher')]/ancestor::div[contains(@data-automation-id,'/Voucher_BOUND')]//mat-icon[text()='search']"));


        public static IWebLocator GlDetailsReceiptSearchButton => L(
            "GlDetails Receipt button",
            By.XPath("//span[contains(text(),'Receipt')]/ancestor::div[contains(@data-automation-id,'/RcptMaster_BOUND')]//mat-icon[text()='search']"));

        public static IWebLocator GlDetailsChequesSearchButton => L(
            "GlDetails Cheques button",
            By.XPath("//span[contains(text(),'Cheques')]/ancestor::div[contains(@data-automation-id,'/CkMaster_BOUND')]//mat-icon[text()='search']"));

        public static IWebLocator Query => L(
            "Query",
            By.XPath("//input[contains(@data-automation-id,'GLDetailDescriptionVariables_ccc')]"));

        public static IWebLocator LanguageDescription => L(
    "LanguageDescription",
    By.XPath("//mat-card[contains(text(),'Language Description')]"));
    }

}
