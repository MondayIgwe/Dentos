using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Time
{
    public class TimeModifyLocators
    {
        public static IWebLocator FeeEarner => L(
            "FeeEarner",
            By.XPath("//input[contains(@name, 'Timekeeper')]"));

        public static IWebLocator InternalComments => L(
            "InternalComments",
            By.XPath("//textarea[contains(@data-automation-id, 'InternalComments')]"));
        
        public static IWebLocator User => L(
            "User",
            By.XPath("//div[@class='contentname' or @class='altcontentname']"));
        
        public static IWebLocator OpenWorkflow => L(
            "OpenWorkflow",
            By.XPath("//div[@class='toolbar-buttons']//button[not(@disabled='true')]//span[text()=' Open ']"));

        public static IWebLocator UserFeeEarnerMapTimekeeperNumbers => L(
            "UserFeeEarnerMapTimekeeperNumbers",
            By.XPath("//div[@ref='centerContainer']//div[@col-id='Timekeeper']//span[text()]"));


        public static IWebLocator TimeCard => L(
            "TimeCard",
            By.XPath("//div[@col-id='TimeIndex'][@role='gridcell']"));

        public static IWebLocator PurgeType => L(
            "PurgeType",
            By.XPath("//input[contains(@name, 'PurgeType')]"));

        public static IWebLocator PurgeTypeReason => L(
            "PurgeTypeReason",
            By.XPath("//input[contains(@name, 'PurgeReasonType_ccc')]"));
        
        public static IWebLocator GetNarrative => L(
            "GetNarrative",
            By.XPath("//div[@class='ql-editor']//p"));

        public static IWebLocator TimeCardSearchResultCardNumbers => L(
            "TimeCardSearchResultCardNumbers",
            By.XPath("//div[@ref='eContainer']//div[@col-id='OrigTimeIndex']"));

        public static IWebLocator TimeCardSearchResultCheckBox (string timeCardNumber) => L(
            "TimeCardSearchResultCheckBox",
            By.XPath("//div[@col-id='OrigTimeIndex'][text()='" + timeCardNumber + "']//parent::div//div[@ref='eCheckbox']"));

        public static IWebLocator TimeCardSearchResultSelectButton => L(
            "TimeCardSearchResultSelectButton",
            By.XPath("//button[@id='select-title-button']"));

        public static IWebLocator PostedBy => L(
            "PostedBy",
            By.XPath("//div[contains(@name,'PostedUser_ccc')]"));
    }
}
