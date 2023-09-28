using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class QuickFind : ITask
    {
        public string SearchText { get; }

        private QuickFind(string searchText) => SearchText = searchText;

        public static QuickFind Search(string searchText) => new(searchText);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Searches for a record(s) using the quick find method.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

           actor.AttemptsTo(Click.On(CommonLocator.FindDivElementContainsText("Quick Find")));
            
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, SearchText));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)), IsEqualTo.True());
            
            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
    
}
