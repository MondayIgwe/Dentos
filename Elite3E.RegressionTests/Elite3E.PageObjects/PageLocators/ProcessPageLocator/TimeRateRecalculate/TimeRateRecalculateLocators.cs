using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.TimeRateRecalculate
{
    public class TimeRateRecalculateLocators
    {
      public static IWebLocator RequestedTimecardsIcon => L(
     "RequestedtimecardsIcon",
      By.XPath("//e3e-bound-input//div[contains(@data-automation-id,'ReqTimeCards')]//button"));
      
        public static IWebLocator WorkRateCheckbox => L(
             "WorkRateCheckbox", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsRecalcWork')]/label/div/input"));

        public static IWebLocator PreviewDropDown => L(
            "PreviewDropDown", By.XPath("//mat-icon[contains(text(),'arrow_drop_down')]"));
    }
}
