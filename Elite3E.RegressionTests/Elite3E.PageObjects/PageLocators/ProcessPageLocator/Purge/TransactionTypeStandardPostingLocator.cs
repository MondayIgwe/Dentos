using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge
{
    public class TransactionTypeStandardPostingLocator
    {

        public static IWebLocator StandardPostingSection => L(
          "Standard Posting Section",
          By.XPath("//h5[contains(text(),'Standard Postings')]"));

        public static IWebLocator RevenueRecognition => L(
          "Revenue Recognition",
          By.XPath("//input[contains(@name,'PostingStageList')]"));
        public static IWebLocator ArReturnResult(string text) => L(
         "ArReturnResult",
         By.XPath("//div[text()='" + text + "  ']"));

        public static IWebLocator GlType => L(
          "GL Natural",
          By.XPath("//input[contains(@name,'GLType')]"));

        public static IWebLocator GlUnit => L(
            "GL Unit",
            By.Name("ARMaskRel.GLUnit"));
        public static IWebLocator ARSearchIcon => L(
            "AR Search Icon",
            By.XPath("//div[contains(@data-automation-id,'ARMask_BOUND')]//mat-icon"));
        public static IWebLocator GlNatural => L(
           "GL Natural",
           By.Name("ARMaskRel.GLNatural"));

        public static IWebLocator GlUnitLocal => L(
          "GL Unit Local",
          By.Name("ARMaskRel.GLUnitLocal"));

        public static IWebLocator GlDepartment => L(
          "GL Department",
          By.Name("ARMaskRel.GLDepartment"));

        public static IWebLocator GlSection => L(
          "GL Section",
          By.Name("ARMaskRel.GLSection"));
        public static IWebLocator GlOffice => L(
          "GL Office",
          By.Name("ARMaskRel.GLOffice"));
        public static IWebLocator GlTimekeeper => L(
          "GL Timekeeper",
          By.Name("ARMaskRel.GLTimekeeper"));
    }
}
