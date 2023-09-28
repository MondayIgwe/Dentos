using System;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.VendorPayeeMerge
{
    public class VendorPayeeMergeLocators
    {

        public static IWebLocator Comments => L(
            "Comments",
            By.XPath("//textarea[contains(@data-automation-id,'Comments')]"));

        
    }
}
