using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using System.Linq;
using Elite3E.PageObjects.PageLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class SearchProcess : ITask
    {
        public string ProcessName { get; }
        public bool WaitForAddButton { get; }
        private SearchProcess(string processName, bool waitForAddButton)
        {
            ProcessName = processName;
            WaitForAddButton = waitForAddButton;
        }

        public static SearchProcess ByName(string processName, bool waitForAddButton = true) =>
            new(processName, waitForAddButton);

        public void PerformAs(IActor actor)
        {
            var browser = actor.Using<BrowseTheWeb>().WebDriver;
            //Searches for a process.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.WaitsUntil(Existence.Of(CommonLocator.SearchIcon), IsEqualTo.True(), 60);

            actor.AttemptsTo(Click.On(CommonLocator.SearchIcon));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.WaitsUntil(Existence.Of(CommonLocator.SearchInput), IsEqualTo.True(), 60);

            actor.AttemptsTo(SendKeys.To(CommonLocator.SearchInput, ProcessName));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var resultProcesses = browser.FindElements(CommonLocator.SearchResults.Query);

            resultProcesses.FirstOrDefault(ele => ele.Text.Trim() == ProcessName)?.Click();

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (WaitForAddButton)
                actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)),
                    IsEqualTo.True());

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }
}
