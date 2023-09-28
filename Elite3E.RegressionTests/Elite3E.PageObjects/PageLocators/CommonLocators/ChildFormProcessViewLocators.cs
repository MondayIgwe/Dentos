using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.CommonLocators
{
    public class ChildFormProcessViewLocators
    {
        public static IWebLocator CurrentView(string childProcessSection) => L(
            "Current View Selected",//span[contains(@class,'form-title')][contains(text(),'WP Amounts (1)')]/following-sibling::e3e-form-view-selector//button[@class='mat-button primary-btn ng-star-inserted']/span
            By.XPath("//span[contains(@class,'form-title')][contains(text(),'" + childProcessSection + "')]/following-sibling::e3e-form-view-selector//button[@class='mat-button primary-btn ng-star-inserted']/span"));

        public static IWebLocator SelectActionDropDown(string childProcessSection) => L(
            "Drop down for action",
            By.XPath("//span[contains(@class,'form-title')][contains(text(),'" + childProcessSection + "')]/following-sibling::e3e-form-view-selector//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator SelectOverlyButton(string button) => L(
           "Overly Button: " + button,
           By.XPath("//div[@class='cdk-overlay-pane']//button[@role='menuitem']/span[contains(text(),'" + button + "')]"));

        public static IWebLocator SelectFlyOutMenu(string childProcessSection, string parentDropDownButtonName) => L(
            "Child process Section parent dropdown",
            By.XPath(
                " //span[contains(@class,'form-title')][contains(text(),'"+ childProcessSection +"')]//ancestor::e3e-form-anchor-view//button[span[contains(text(),'"+ parentDropDownButtonName +"')]]/following-sibling::button"));


    }
}
