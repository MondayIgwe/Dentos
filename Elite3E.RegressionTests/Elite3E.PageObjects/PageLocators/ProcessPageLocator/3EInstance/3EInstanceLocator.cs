using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator._3EInstance
{
    public class _3EInstanceLocator
    {

       
        public static IWebLocator RegionDropDownIcon => L(
       "Regiondropdown",
         By.XPath("//e3e-bound-input//div[contains(@data-automation-id,'SetupRegion_ccc')]//button"));

        public static IWebLocator RegionDropDown => L(
          "RegionDropDown",
        By.XPath("//input[contains(@data-automation-id,'SetupRegion_ccc')]"));
        public static IWebLocator InstanceTypeDropDownIcon => L(
        "InstanceTypeDropDown",
        By.XPath("//e3e-bound-input//div[contains(@data-automation-id,'InstanceType_ccc')]//button"));

        public static IWebLocator InstanceTypeDropDown => L(
       "InstanceTypeDropDown",
       By.XPath("//input[contains(@data-automation-id,'InstanceType_ccc')]"));


    }
}
