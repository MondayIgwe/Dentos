using OpenQA.Selenium;
using Boa.Constrictor.WebDriver;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.CostTypeGroup
{
    public static class CostTypeGroupLocators
    {
        public static IWebLocator InputLocator => L(
            "Description",
            By.CssSelector(
                "e3e-form-anchor-view input.mat-input-element.mat-form-field-autofill-control.cdk-text-field-autofill-monitored.ng-pristine.ng-valid"));

        public static IWebLocator ValidateCostTypeGroup(string costTypeGroup) => L(
           "ValidateCostTypeGroup",
           By.XPath("//span[text()='" + costTypeGroup + "']"));

        public static IWebLocator NewCostTypeGroup => L(
           "NewCostTypeGroup",
           By.XPath("//span[text()=' New Cost Type Group ']"));
                
        public static IWebLocator ExistingCostTypeGroups => L(
           "ExistingCostTypeGroups",
           By.XPath("//div[@col-id='CostTypeGroup']//mat-form-field[not(contains(@class, 'disabled'))]"));

        public static IWebLocator DeleteCostTypeGroup => L(
           "DeleteCostTypeGroup",
           By.XPath("//span[contains(text(), 'Cost Type Group')]/..//span[text()=' Delete ']"));
        
        public static IWebLocator OkButton => L(
           "OkButton",
           By.XPath("//button[@data-automation-id='ok@0']"));
                
        public static IWebLocator SearchIcon => L(
           "SearchIcon",
           By.XPath("//div[@class='mat-form-field-flex']//mat-icon[text()='search']"));               

        public static IWebLocator DisbursementOption(string option) => L(
           "DisbursementOption",
           By.XPath("//div[text()='" + option + "']/..//div[contains(text(),'H')]"));
                
        public static IWebLocator DisbursementTypeSearchIcon => L(
           "DisbursementTypeSearchIcon",
           By.XPath("//input[contains(@name, 'WorkCostType')]/../..//mat-icon[text()='search']"));

        public static IWebLocator ClickFirstRowCodeCostTypeGrid => L(
            "ClickFirstRowCodeChargeTypeGrid",
            By.XPath("(//div[@role='gridcell'][@col-id='CostTypeGroupRel.Code'])[1]"));

        public static IWebLocator Description => L(
            "Description",
            By.XPath("//input[contains(@data-automation-id,'/Description')]"));

        public static IWebLocator CostTypeDetail => L(
        "CostTypeDetail",
        By.XPath("//mat-card[contains(text(),'Cost Type Detail')]"));
    }
}