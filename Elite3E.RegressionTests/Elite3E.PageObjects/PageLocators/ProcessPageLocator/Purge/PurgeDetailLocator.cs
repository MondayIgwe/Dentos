using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge
{
   public  class PurgeDetailLocator
    {

        public static IWebLocator SetExcludeAnticipated=> L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsExcludeAnticipated_ccc')]/label/div"));

        public static IWebLocator GetExcludeAnticipated => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsExcludeAnticipated_ccc')]/label/div/input"));

        public static IWebLocator SetPurgeDisbursement => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsPurgeCost')]/label/div"));

        public static IWebLocator GetPurgeDisbursement => L(
            "CheckBox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'IsPurgeCost')]/label/div/input"));

        public static IWebLocator PurgeType => L(
            "Purge Type",
            By.XPath("//input[contains(@name,'PurgeType')]"));
        
        public static IWebLocator EndDate => L(
            "End Date",
            By.XPath("//input[contains(@name,'EndDate')]"));
        public static IWebLocator CalculateButton => L(
            "End Date",
             By.XPath("//button/span[contains(text(),'Calculate')]"));
        public static IWebLocator PreviewButton => L(
            "End Date",
            By.XPath("//button/span[contains(text(),'Preview')]"));

        public static IWebLocator RequestedMattersElement => L(
           "End Date",
           By.XPath("//e3e-bound-input[div/label/span='Requested Matters']"));
        //form-field-button form-field-frying-pan mat-icon-button ng-star-inserted

        public static IWebLocator RequestedMattersLookup => L(
          "End Date",
          By.CssSelector("button.form-field-button.form-field-frying-pan.mat-icon-button.ng-star-inserted"));

        public static IWebLocator RequestedMattersSearchValue => L(
         "RequestedMattersSearchValue",
         By.Name("advancedFindLookup.where.predicates.0.value"));

        public static IWebLocator RequestedMattersSubmitButton => L(
         "RequestedMattersSubmitButton",
         By.Id("select-title-button"));

        public static IWebLocator PurgedMatter => L(
           "Start Date",
           By.XPath("//div[contains(@name,'MattersFound')]"));

        public static IWebLocator PurgedDisbursement => L(
          "Start Date",
          By.XPath("//div[contains(@name,'CostCards')]"));

    }
}
