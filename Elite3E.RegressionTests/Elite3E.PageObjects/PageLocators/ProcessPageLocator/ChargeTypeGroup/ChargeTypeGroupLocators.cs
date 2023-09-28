using OpenQA.Selenium;
using Boa.Constrictor.WebDriver;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup
{
    public static class ChargeTypeGroupLocators
    {

        public static IWebLocator Code => L(
            "Code",
            By.XPath("//input[contains(@name, 'Code')]"));

        public static IWebLocator Description => L(
            "Description",
            By.XPath("//input[contains(@name, 'Description')]"));

        public static IWebLocator InputLocator => L(
            "Description",
            By.XPath("//div[@class='input']//input"));

        public static IWebLocator ValidateChargeTypeGroup(string chargeTypeGroup) => L(
           "ValidateChargeTypeGroup",
           By.XPath("//span[text()='" + chargeTypeGroup + "']"));

        public static IWebLocator ValidateChargeTypeGroupByInput => L(
            "ValidateChargeTypeGroup",
            By.XPath("//input[contains(@name,'ChrgTypeGroup')]"));

        public static IWebLocator NewChargeTypeGroup => L(
           "NewChargeTypeGroup",
           By.XPath("//span[text()=' New Charge Type Group ']"));
                
        public static IWebLocator ExistingChargeTypeGroups => L(
           "ExistingChargeTypeGroups",
           By.XPath("//div[@col-id='ChrgTypeGroup']//mat-form-field[not(contains(@class, 'disabled'))]"));
        
        public static IWebLocator DeleteChargeTypeGroup => L(
           "DeleteChargeTypeGroup",
           By.XPath("//span[contains(text(), 'Charge Type Group')]/..//span[text()=' Delete ']"));
                
        public static IWebLocator ExcludeListCheckbox => L(
           "ExcludeListCheckbox",
           By.XPath("//span[text()='Exclude List']/../..//input[@type='checkbox']/.."));               

        public static IWebLocator SelectChargeType(string chargeType) => L(
           "SelectChargeType",
           By.XPath("//div[text()='" + chargeType + "']/..//input[@type='checkbox']/.."));
                
        public static IWebLocator NewChargeTypeInputDropDown => L(
           "NewChargeTypeInputDropDown",
           By.XPath("//input[contains(@name, 'WorkChrgType')]/../..//mat-icon[text()='arrow_drop_down']"));
                
        public static IWebLocator NewChargeTypeInput => L(
           "NewChargeTypeInput",
           By.XPath("//div[@col-id= 'WorkChrgType'][@title='Required']"));

        public static IWebLocator ExistingChargeTypeDropDown => L(
           "ExistingChargeTypeDropDown",
           By.XPath("//input[contains(@name, 'WorkChrgType')]/../..//mat-icon[text()='arrow_drop_down']"));
                
        public static IWebLocator ExistingChargeType => L(
           "ExistingChargeType",
           By.XPath("//div[@col-id='WorkChrgType']//div[@class='bound-column-container']//span[not(@title='Taxes')]"));

        public static IWebLocator ClickFirstRowCodeChargeTypeGrid => L(
            "ClickFirstRowCodeChargeTypeGrid",
            By.XPath("(//div[@role='gridcell'][@col-id='ChrgTypeGroupRel.Code'])[1]"));

        public static IWebLocator ChargeTypeDetail => L(
      "ChargeTypeDetail",
      By.XPath("//mat-card[contains(text(),'Charge Type Detail')]"));
    }
}