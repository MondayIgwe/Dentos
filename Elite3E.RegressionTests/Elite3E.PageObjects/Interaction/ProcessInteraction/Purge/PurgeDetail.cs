using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class PurgeDetail : ITask
    {
        public string Matter { get; }

        private PurgeDetail(string matter) =>
            Matter = matter;

        public static PurgeDetail AddMatters(string matter) => new(matter);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var root = driver.FindElement(PurgeDetailLocator.RequestedMattersElement.Query);

            var lookupButton = root.FindElement(PurgeDetailLocator.RequestedMattersLookup.Query);

            lookupButton.Click();
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(PurgeDetailLocator.RequestedMattersSearchValue, Matter.Replace("-", "|")));
            actor.AttemptsTo(Click.On(PurgeDetailLocator.RequestedMattersSubmitButton));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
