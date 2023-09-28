using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Instance
{
    public class InstanceTypeLocators
    {

       
        public static IWebLocator GetIsProductionCheckbox => L(
          "SetIsProductionCheckbox",
         By.XPath("//mat-checkbox[contains(@data-automation-id,'IsProduction')]/label/div/input"));
              
       
    }
}
