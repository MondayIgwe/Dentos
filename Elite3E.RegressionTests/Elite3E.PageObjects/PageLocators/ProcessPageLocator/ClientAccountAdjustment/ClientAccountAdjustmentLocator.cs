using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountAdjustment
{
    public class ClientAccountAdjustmentLocator
    {
        public static IWebLocator EFTCheckbox => L(
            "EFT checkbox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'/TrustAdjType1.IsEFT')]//input[@type='checkbox']"));

        public static IWebLocator DepositCheckbox => L(
            "Deposit checkbox",
            By.XPath("//mat-checkbox[contains(@data-automation-id,'/IsDeposit')]//input[@type='checkbox']"));

    }
}
