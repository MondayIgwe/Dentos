using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Entity
{
   public static class EntityMaintenancePopupLocator
    {
        public static IWebLocator EntityPersonRadioButton => L(
            "Entity With",
            By.XPath("//*[@id='mat-radio-3']/label/div[2]"));
        
    }
}
