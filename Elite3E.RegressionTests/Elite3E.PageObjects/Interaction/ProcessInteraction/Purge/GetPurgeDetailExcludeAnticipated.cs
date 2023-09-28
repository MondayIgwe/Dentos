using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class GetPurgeDetailExcludeAnticipated : IQuestion<IWebElement>
    {

        private GetPurgeDetailExcludeAnticipated()
        {
        }

        public static GetPurgeDetailExcludeAnticipated Element() => new();

        public IWebElement RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver; 
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            return driver.FindElement(PurgeDetailLocator.GetExcludeAnticipated.Query);

        }
    }
    
}
