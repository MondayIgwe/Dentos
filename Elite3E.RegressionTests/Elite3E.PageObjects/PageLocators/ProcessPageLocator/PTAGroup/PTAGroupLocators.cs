using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.PTAGroup
{
    public class PTAGroupLocators
    {
        public static IWebLocator PTAGroupCodeInput => L(
           "PTAGroupCodeInput",
           By.XPath("//input[contains(@data-automation-id,'Code')]"));

        public static IWebLocator PTAGroupDescriptionInput => L(
           "PTAGroupDescriptionInput",
           By.XPath("//input[contains(@data-automation-id,'Description')]"));

        public static IWebLocator PhaseListDropdown => L(
           "PhaseListDropdown",
           By.XPath("//input[contains(@data-automation-id,'PhaseList')]"));

        public static IWebLocator TaskListDropdown => L(
           "TaskListDropdown",
           By.XPath("//input[contains(@data-automation-id,'TaskList')]"));

        public static IWebLocator ActivityListDropdown => L(
           "ActivityListDropdown",
           By.XPath("//input[contains(@data-automation-id,'ActivityList')]"));

        public static IWebLocator PtaGroupFeesInput => L(
          "PtaGroupFeesInput",
          By.XPath("//span[text()='PTA Group Fees 1']//following::input[contains(@data-automation-id,'PTAGroup')]"));


    }
}
