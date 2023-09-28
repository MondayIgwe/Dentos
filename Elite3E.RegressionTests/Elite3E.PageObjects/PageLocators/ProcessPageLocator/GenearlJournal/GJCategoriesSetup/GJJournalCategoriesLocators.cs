using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.GenearlJournal.GJCategoriesSetup
{
    public class GJJournalCategoriesLocators
    {

        public static IWebLocator CheckBox(string checkBoxLabel) => L("Check Box" + checkBoxLabel, By.XPath("//mat-checkbox[contains(@data-automation-id,'" + checkBoxLabel + "')]"));

    }
}
