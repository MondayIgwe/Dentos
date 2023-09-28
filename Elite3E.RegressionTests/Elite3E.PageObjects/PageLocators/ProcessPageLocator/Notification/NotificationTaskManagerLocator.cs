using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Notification
{
   public class NotificationTaskManagerLocator
    {
        public static IWebLocator BaseTask_BusinessObjectRadio => L(
            "Select Task Radio Button option - Business Object",
            By.XPath("//div[text()='Business Object Task']//ancestor::mat-radio-button//input"));
        
        public static IWebLocator BaseTask_EventRadio => L(
            "Select Task Radio Button option - Event",
            By.XPath("//div[text()='Event Task']//ancestor::mat-radio-button//input"));

        public static IWebLocator SelectTaskButton => L(
            "Select Button for dialog box",
            By.XPath("//button[@data-automation-id='subclass-dialog-select-button']"));

        public static IWebLocator BusinessObjectInput => L(
            "Input Field for Business Object Task",
            By.XPath("//input[contains(@data-automation-id,'/BusinessObject')]"));

    }
}
