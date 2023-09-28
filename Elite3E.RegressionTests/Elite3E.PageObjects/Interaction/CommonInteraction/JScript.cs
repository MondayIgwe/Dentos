using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class JScript : ITask
    {
        private IWebLocator Locator { get; set; }
        private IWebElement Element { get; set; }
        private JScript(IWebLocator locator)
        {
            Locator = locator;
        }

        private JScript(IWebElement element)
        {
            Element = element;
        }

        public static JScript ClickOn(IWebLocator locator) => new(locator);

        public static JScript ClickOn(IWebElement element) => new(element);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (Locator!= null)
            {
                 Element = driver.FindElement(Locator.Query);
            }
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("arguments[0].click();", Element);

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
