using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class PurgeDetailAnticipated : ITask
    {
        public bool Excluded;
        private PurgeDetailAnticipated(bool exclude)
        {
            Excluded = exclude;
        }

        public static PurgeDetailAnticipated Exclude(bool exclude) => new(exclude);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var defaultCheckboxGet = driver.FindElement(PurgeDetailLocator.GetExcludeAnticipated.Query);
            var defaultCheckboxSet = driver.FindElement(PurgeDetailLocator.SetExcludeAnticipated.Query);

            var selected = defaultCheckboxGet.Selected;

            switch (selected)
            {
                case false when Excluded:
                case true when !Excluded:
                    defaultCheckboxSet.Click();
                    break;
            }

            actor.WaitsUntil(Appearance.Of(PurgeDetailLocator.CalculateButton), IsEqualTo.True(), 8);

        }
    }

}
