using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.RateMaintenance
{
    public class EnterRate : ITask
    {
        public string Rate  { get; }

        private EnterRate(string rate) =>
            Rate = rate;

        public static EnterRate Value(string rate) => new(rate);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var amount = driver.FindElement(By.XPath(
                "//span[contains(text(),'Rate Details')]/ancestor::e3e-form-anchor-view//span[text()='1']/ancestor::div[@ref='eBodyViewport']//div[@col-id='Amount']"));
            amount.Click();

            amount.FindElement(By.XPath("//input[contains(@name,'Amount')]")).SendKeys(Rate);

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
