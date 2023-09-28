using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.GetRateTest
{
    public class GetRateTestLocators
    {
        public static IWebLocator WorkDate => L(
                "WorkDate input",
                By.XPath("//input[contains(@data-automation-id,'WorkDate')]"));
        public static IWebLocator TimeKeeper => L(
               "TimeKeeper input",
               By.XPath("//input[contains(@data-automation-id,'Timekeeper')]"));
        public static IWebLocator Matter => L(
               "Matter input",
               By.XPath("//input[contains(@data-automation-id,'Matter')]"));
        public static IWebLocator TransactionType => L(
               "TransactionType input",
               By.XPath("//input[contains(@data-automation-id,'TransactionType')]"));
        public static IWebLocator GetRateButton => L(
               "GetRateButton",
               By.XPath("//button//span[.=' Get Rate ']"));
        public static IWebLocator MessageLog => L(
               "MessageLog",
               By.XPath("//textarea[contains(@data-automation-id,'MsgLog')]"));
        public static IWebLocator FeeEarnerName => L(
               "FeeEarnerName",
               By.XPath("//div[contains(@data-automation-id,'TkprDispName') and contains(@name,'TkprDispName')]"));

    }
}
