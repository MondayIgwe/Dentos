using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.RateMaintenance
{
    public class FutureDate : ITask
    {


        private FutureDate() {}
            

        public static FutureDate Select() => new();

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            driver.FindElement(By.XPath(
                "//span[contains(text(),'Rate Details')]/ancestor::e3e-form-anchor-view//span[text()='1']/ancestor::div[@ref='eBodyViewport']//div[@col-id='IsFuture']//i")).Click();
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
