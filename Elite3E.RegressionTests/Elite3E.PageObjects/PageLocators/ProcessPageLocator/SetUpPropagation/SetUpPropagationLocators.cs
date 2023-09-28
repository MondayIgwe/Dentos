using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.SetUpPropagation
{
    public class SetUpPropagationLocators
    {
        public static IWebLocator Instance => L(
           "Instance_ccc",
     By.XPath("//input[contains(@data-automation-id, 'Instance_ccc')]"));

        public static IWebLocator ProcessInput => L(
             "ProcessInput",
       By.XPath("//input[contains(@data-automation-id, 'Process')]"));

        public static IWebLocator ProcessName => L(
            "ProcessNameInput",
            By.XPath("//div[contains(@name, 'ArchetypeName')]"));

        public static IWebLocator AddedItems(string text) => L(
            "AddedItems",
      By.XPath("//span[contains(text(),'Items to Include / Exclude ')]//following::span[contains(text(),'"+text+"')]"));

        public static IWebLocator ControlSource => L(
            "ControlSource",
      By.XPath("//input[contains(@data-automation-id, 'ControlSource')]"));

        public static IWebLocator IncludeList => L(
          "IncludeList",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsInclude')]/label/div/input"));

        public static IWebLocator SelectOption => L(
         "SelectOption",
      By.XPath("//i[contains(@class,'material-icons')]"));

        public static IWebLocator ExcludeList => L(
        "IsExclude",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsExclude')]/label/div/input"));

        public static IWebLocator Role => L("NX Role drop down",
            By.XPath("//span[text()='Role']/parent::label/following-sibling::div//input[contains(@name,'NxRole')]"));

        public static IWebLocator InstancesRowCount => L(" 3E Instance Row Count ",
            By.XPath(
                "//span[contains(text(),'3E Instances')]/ancestor::div[contains(@class,'form-header')]//span[@class='row-count']"));

        public static IWebLocator InstanceNextRowButton => L("3E Instances Next Row Button ",
            By.XPath(
                "//span[contains(text(),'3E Instances')]/ancestor::div[contains(@class,'form-header')]//mat-icon[contains(@class,'next-button')][text()='play_arrow']"));

        public static IWebLocator InstanceSkipToFirstButton => L("3E instance move to first row button",
            By.XPath(
                "//span[contains(text(),'3E Instances')]/ancestor::div[contains(@class,'form-header')]//mat-icon[text()='skip_previous']"));

    }
}
