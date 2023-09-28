using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup
{
    public static class AnticipatedTypeLocators
    {

        public static IWebLocator SearchTextBox => L(
            "Search TextBox",
            By.XPath("//div[@id='pregrid-content']//input[contains(@class, 'mat-input-element')]"));

        public static IWebLocator SearchButton => L(
            "Search Button",
            By.XPath("//span[text()=' SEARCH ']"));

        public static IWebLocator RadioButton => L(
            "Radio Button",
            By.XPath("//mat-icon[contains(text(),'radio_button_unchecked')]"));
    }
}