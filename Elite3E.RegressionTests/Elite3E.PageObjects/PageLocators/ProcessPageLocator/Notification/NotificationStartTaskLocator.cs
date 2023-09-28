using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Notification
{
   public class NotificationStartTaskLocator
    {
        public static IWebLocator TaskInput => L(
            "Input Field for Task",
            By.XPath("//input[contains(@data-automation-id,'/NxNtfTask')]"));

        public static IWebLocator TaskDropdownOptions(string task) => L(
            "Dropdown option specific to Task",
            By.XPath("//div[@class='cdk-overlay-pane']//span[text()='"+ task + "']//ancestor::mat-option"));
        
        public static IWebLocator GoButton => L(
            "Go button",
            By.XPath("//span[contains(text(),'GO ')]//parent::button"));

        public static IWebLocator ConfirmationToast => L(
            "Toast Message Confirming Task Added",
            By.XPath("//span[text()='Task has been added to the queue']"));

    }
}
