using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.PostQueue
{
    public class PostQueueLocators
    {
        public static IWebLocator ShowButton => L(
          "ShowButton",
          By.XPath("//button//span[text()=' Show ']"));

        public static IWebLocator PostUserDiv => L(
           "PostUserDiv",
           By.XPath("//div[@row-id='0']//div[@col-id='NxUserRel.BaseUserName']"));

        public static IWebLocator PostSourceDiv => L(
           "PostSourceDiv",
           By.XPath("//div[@row-id='0']//div[@col-id='PostSource']"));

        public static IWebLocator PostMgrStatusDiv => L(
           "PostMgrStatusDiv",
           By.XPath("//div[@row-id='0']//div[@col-id='PostMgrStatusList']"));
    }
}
