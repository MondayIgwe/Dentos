using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.CommonLocators
{
    public class ChildFormNavigationLocators
    {
        public static IWebLocator NavigateToEffectiveDatedInformation => L(
            "Effective Dated Information",
            By.XPath("//mat-card[contains(text(),'Effective Dated Information')]"));

        public static IWebLocator NavigateToMatterRates => L(
            "Matter Rates",
            By.XPath("//mat-card[contains(text(),'Matter Rates')]"));
        

    }
}
