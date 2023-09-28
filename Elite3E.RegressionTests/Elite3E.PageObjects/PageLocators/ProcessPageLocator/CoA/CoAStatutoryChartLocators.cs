using System;
using OpenQA.Selenium;
using Boa.Constrictor.WebDriver;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA
{
   
    public static class CoAStatutoryChartLocators
    {
        public static IWebLocator AccountNumber => L(
            "AccountNumber",
            By.XPath("//input[contains(@data-automation-id,'StatAcctNum')]"));

        public static IWebLocator FirmDescription => L(
            "FirmDescription",
            By.XPath("//input[contains(@data-automation-id,'StatAcctDescFirm')]"));
        public static IWebLocator LocalDescription => L(
            "LocalDescription",
            By.XPath("//input[contains(@data-automation-id,'StatAcctDescLocal')]"));
    }
}
