using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Notification
{
    public class NotificationTaskAndLogLocators
    {
        public static IWebLocator RefreshButton => L(
            "RefreshButton",
            By.XPath("//span[contains(text(),' Refresh ')]//parent::button"));

        public static IWebLocator ViewPreviouslyRunTasksButton => L(
            "ViewPreviouslyRunTasksButton",
            By.XPath("//span[contains(text(),'View Previously Run Tasks')]//parent::button"));

        public static IWebLocator PreviouslyRunTasksHeader => L(
            "PreviouslyRunTasksHeader",
            By.XPath("//div[@class='e3e-dialog__title-bar-label'][contains(text(),'Previously Run Tasks')]"));

        public static IWebLocator QueuedTasksHeader => L(
            "QueuedTasksHeader",
            By.XPath("//div[contains(text(),'Queued And Running Tasks')]"));

        public static IWebLocator TaskInQueue(string task) => L(
            "TaskInQueue",//div[contains(text(),'Queued And Running Tasks')]//following::span[contains(text(),'ML Delegation Task')]
            By.XPath("//div[contains(text(),'Queued And Running Tasks')]//following::span[contains(text(),'"+ task + "')]"));
    
        public static IWebLocator PreviouslyRunTaskRow(string task) => L(
            "PreviouslyRunTaskRow",//span[contains(text(),'ML Delegation Task')]//ancestor::div[@role='row']//span[text()]
            By.XPath("//span[contains(text(),'"+ task +"')]//ancestor::div[@role='row']//span[text()]"));
    }
}
