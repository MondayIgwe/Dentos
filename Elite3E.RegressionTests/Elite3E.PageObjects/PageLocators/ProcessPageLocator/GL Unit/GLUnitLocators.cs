using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.GL_Unit
{
    public class GLUnitLocators
    {
        public static IWebLocator GLValue => L(
        "GLValueInput",
        By.XPath("//input[contains(@name,'GLValue')]"));

        public static IWebLocator Unit => L(
       "GLUnit",
       By.XPath("//input[contains(@name,'NxUnit')]"));

       public static IWebLocator IsUseLocalCheckbox => L(
      "IsUseLocalCheckBox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsUseLocal')]/label/div/input"));

        public static IWebLocator GLLocalChart => L(
       "GLLocalChart",
        By.XPath("//input[contains(@name,'GLLocalChart')]"));

        public static IWebLocator LocalAccountColumnValue(string GlNatural) => L(
            "LocalAccountColumnValueBasedOnGLNaturalRowValue",
            By.XPath("//span[text()='"+ GlNatural + "']/ancestor::div[@role='row']//div[@col-id='GLLocal']//span[@title]"));

        public static IWebLocator LocalAccountColumnInput => L(
            "LocalAccountColumnInput",
            By.XPath("//input[contains(@name,'GLLocal')]"));

        public static IWebLocator LocalAccountColumnFilterButton => L(
           "LocalAccountColumnFilterButton",
           By.XPath("//particle-button[@icon='search']//button//mat-icon[text()='search']"));
        public static IWebLocator MaximizeChildGlForm(string text) => L(
          "MaximizeChildGlForm",
          By.XPath("//span[contains(text(),'"+text+"')]//following::mat-icon[text()='fullscreen']"));

        public static IWebLocator LocalAccountColumnFilterInputButton => L(
           "LocalAccountColumnFilterInputButton",
           By.XPath("//particle-button//button//span//mat-icon[text()='search']//following::input"));

        public static IWebLocator LocalAccountColumnText(string account) => L(
       "LocalAccountColumnText",
       By.XPath("//div[@col-id='GLLocal']//span[text()='"+account+"']"));



        public static IWebLocator DeleteLocalAccountButton => L(
          "DeleteLocalAccountButton",
          By.XPath("//button[contains(@data-automation-id,'GLUnitLocal')]//span[text()=' Delete ']"));


        public static IWebLocator IsDefaultColCheckedStatus(string GlNatural) => L(
            "IsDefaultCheckedStatus",
            By.XPath("//span[text()='" + GlNatural + "']/ancestor::div[@role='row']//div[@col-id='IsDefault']//i[contains(@class,'checked')]"));

        public static IWebLocator StaturoyAccountColumnValue(string GlNatural) => L(
            "StaturoyAccountColumnValueBasedOnGLNaturalRowValue",
            By.XPath("(//span[text()='" + GlNatural + "']/ancestor::div[@role='row']//div[@col-id='StatChartDet_ccc']//span[@title])[last()]"));

        public static IWebLocator StaturoyAccountInput => L(
            "StaturoyAccountInput",
            By.XPath("//input[contains(@data-automation-id,'StatChartDet_ccc')]"));

        public static IWebLocator StaturoyAccountLocalDescColValue(string GlNatural) => L(
            "StaturoyAccountLocalDescColValueBasedOnGLNaturalRowValue",
            By.XPath("(//span[text()='" + GlNatural + "']/ancestor::div[@role='row']//div[contains(@col-id,'StatAcctDescLocal')]//span[@title])[last()]"));

        public static IWebLocator StaturoyAccountFirmDescColValue(string GlNatural) => L(
            "StaturoyAccountFirmDescColValueColValueBasedOnGLNaturalRowValue",
            By.XPath("(//span[text()='" + GlNatural + "']/ancestor::div[@role='row']//div[contains(@col-id,'StatAcctDescFirm')]//span[@title])[last()]"));

    }
}
