using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaskList
{
    public class TaskListLocators
    {

        public static IWebLocator TaskListCodeDiv => L(
        "TaskListCodeDiv",
        By.XPath("//div[contains(@data-automation-id,'TaskList') and contains(@name,'Code')]"));

        public static IWebLocator FirmTaskListCode => L(
        "FirmTaskListCode",
        By.XPath("//div[@ref='eBodyViewport']//input[contains(@data-automation-id,'TaskList') and contains(@name,'Code')]"));

        public static IWebLocator FirmTaskListDescription => L(
        "FirmTaskListDescription",
        By.XPath("//div[@ref='eBodyViewport']//input[contains(@name,'Description')]"));

        public static IWebLocator FirmTask => L(
       "FirmTask",
       By.XPath("//div[@ref='eBodyViewport']//input[contains(@name,'FirmTask')]"));

    }
}
