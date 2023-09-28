using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;


namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.OverrideSetSystemOptions
{
    public class OverrideSetSystemOptionsLocators
    {
        public static IWebLocator GroupType(string text) => L(
                "GroupType",
         By.XPath("//div[text()=' " + text + " ']"));

        public static IWebLocator ProfAttachmentOption(string text) => L(
              "ProfAttachmentOption",
        By.XPath("//e3e-server-page-label-control[@title='" + text + "']//div//span"));

        public static IWebLocator MyMatterIsgeneratedOption(string text) => L(
             "ProfAttachmentOption",
       By.XPath("//e3e-server-page-label-control[@title='" + text + "']"));

        public static IWebLocator ProfAttachmentTemplateOption(string text) => L(
             "ProfAttachmentOption",
       By.XPath("//e3e-server-page-label-control[@title='"+text+"']"));

        public static IWebLocator ProfAttachmentTemplateOptionFirmOverride(string text) => L(
           "ProfAttachmentOption",
       By.XPath("//e3e-server-page-label-control[@title='"+text+"']//following::e3e-server-page-bound-control-renderer"));

        public static IWebLocator ProfAttachmentTemplateOptionFirmOverrideInput => L(
           "ProfAttachmentOption",
       By.XPath("//e3e-bound-input//div[@data-automation-id='grdOptions_part_bdy_r103_c2_ctl0_BOUND']//input"));

        public static IWebLocator OptionName(string text) => L(
               "OptionName",
         By.XPath("//e3e-server-page-label-control[@title='" + text + "']//div//span"));

        public static IWebLocator OptionValue(string text) => L(
            "OptionValue",
      By.XPath("//e3e-server-page-label-control[@title='Default workflow proforma generation status']//following::e3e-server-page-label-control//span"));
        

        public static IWebLocator OptionNameOther(string text) => L(
               "OptionNameOther",
         By.XPath("//e3e-server-page-label-control[@title='" + text + "']//div//span"));

        public static IWebLocator WorkflowOption(string text) => L(
             "OptionName",
       By.XPath("//e3e-server-page-label-control[@title='" + text + "']//div//span"));

        public static IWebLocator HiddenCards(string text) => L(
              "HiddenCard",
         By.XPath("//span[text()=' " + text + " ']"));

        public static IWebLocator StartWorkFlowCheckbox => L(
             "HiddenCard",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsStartWF_ccc')]//input"));

        public static IWebLocator OptionDescriptionColumn => L(
           "OptionDescriptionColumn",
      By.XPath("//div[@col-id='OptionDescription']"));

        

    }
}
