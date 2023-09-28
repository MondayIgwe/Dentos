using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;
namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.VolumeDiscount
{
    public class VolumeDiscountLocator
    {

        public static IWebLocator Office => L(
            "Office",
            By.XPath("//input[contains(@data-automation-id,'Office')]"));


        public static IWebLocator ChargeType => L(
            "ChargeType",
            By.XPath("//input[contains(@data-automation-id,'ChrgType')]"));


        public static IWebLocator IncreaseChargeType => L(
            "IncreaseChargeType",
            By.XPath("//input[contains(@data-automation-id,'IncreaseChrgType')]"));

        public static IWebLocator CalculationMethod => L(
           "CalculationMethod",
           By.XPath("//input[contains(@data-automation-id,'VolumeDiscountCalcList')]"));

        public static IWebLocator Currency => L(
           "Currency",
           By.XPath("//input[contains(@data-automation-id,'Currency')]"));

        public static IWebLocator TierAmount => L(
       "TierAmount",
       By.XPath("//input[contains(@data-automation-id,'TierAmount')]"));

        public static IWebLocator DiscountPercent => L(
       "TierAmount",
       By.XPath("//input[contains(@data-automation-id,'DiscountPercent')]"));
    }
}
