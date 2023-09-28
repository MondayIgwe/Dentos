using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.GLNatural
{
    public class GLNaturalLocators
    {
        public static IWebLocator GLNatural => L(
           "GLNaturalInput",
           By.XPath("//input[contains(@name,'GLNat')]"));

        public static IWebLocator AccountCategory => L(
          "AccountCategory",
          By.XPath("//input[contains(@name,'GLAcctCategoryList')]"));

        public static IWebLocator IsControlCheckbox => L(
            "ControlAccountCheckBox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsControl')]/label/div/input"));

        public static IWebLocator IsAggregateCheckbox => L(
            "IsAggregateCheckBox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsAggregate')]/label/div/input"));

        public static IWebLocator IsAutoAddCheckbox => L(
         "IsAggregateCheckBox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsAutoAdd')]/label/div/input"));

        public static IWebLocator GLNaturalChild => L(
         "GLNaturalChildInput",
         By.XPath("//input[contains(@name,'GLNatural')]"));

        public static IWebLocator COAStatChartInput => L(
        "StatChartInput",
        By.XPath("//input[contains(@data-automation-id,'StatChartHdr_ccc')]"));

        public static IWebLocator LocalAccountFieldDiv => L(
         "LocalAccountFieldDiv",
         By.XPath("//div[@row-index='0']//div[@col-id='GLLocal']"));

        public static IWebLocator GLNaturalFieldDiv => L(
        "GLNaturalFieldDiv",
        By.XPath("//div[@row-index='0']//div[@col-id='GLNatural']"));

        public static IWebLocator LocalAccountFieldInput => L(
         "LocalAccountFieldInput",
         By.XPath("//input[contains(@data-automation-id,'GLLocal')]"));

        public static IWebLocator IsGLSecurityCheckBox => L(
         "IsGLSecurityCheckBox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsSubledger')]/label/div/input"));
 
        public static IWebLocator RoleDropdown => L(
       "RoleDropdown",
       By.XPath("//input[contains(@name,'Role')]"));

        public static IWebLocator ItemUnitInput => L(
        "ItemUnitInput",
        By.XPath("//input[contains(@data-automation-id,'GLUnitGroupDet_ccc')]"));

        public static IWebLocator IsLeadCheckbox(string row) => L(
        "IsLeadCheckbox",
        By.XPath("//div[@row-index='"+row+"']//div[@col-id='IsLead']//span//i"));

        public static IWebLocator GLNaturalRowDiv(string glnatural) => L(
       "GLNaturalRowDiv",
       By.XPath("//div[@col-id='GLNatural']//span[@title='"+glnatural+"']"));

        public static IWebLocator IsLeadCheckboxDisabledTicked => L(
      "IsLeadCheckboxDisabledTicked",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsLeadInGLUnitGroup_ccc')]//input[@disabled]"));

        public static IWebLocator CodeValue => L(
            "CodeValue or Approver name",
            By.XPath("//div[contains(@class,'input') and contains(@data-automation-id,'Code')]"));

        public static IWebLocator Approver1 => L(
            "Approver1",
            By.XPath("//input[contains(@name,'Approver1')]"));
        public static IWebLocator Approver2 => L(
            "Approver2",
            By.XPath("//input[contains(@name,'Approver2')]"));
        public static IWebLocator Approver3 => L(
            "Approver3",
            By.XPath("//input[contains(@name,'Approver3')]"));

    }
}
