using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class FlyOutButtonMenu : ITask
    {
        public string FlyOutButtonText { get; }
        public string TopLevelButtonText { get; }

        private FlyOutButtonMenu(string topLevelButtonText, string flyOutButtonText)
        {
            TopLevelButtonText = topLevelButtonText;
            FlyOutButtonText = flyOutButtonText;
        }

        public static FlyOutButtonMenu Click(string topLevelButtonText, string flyOutButtonText) =>
            new(topLevelButtonText, flyOutButtonText);

        public void PerformAs(IActor actor)
        {

            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Clicks a process within a drop down.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            driver.FindElement(CommonLocator.DownArrowButtonAfterButton(TopLevelButtonText).Query).Click();

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Hover.Over(CommonLocator.ButtonElementContainsText(TopLevelButtonText)));

            driver.SwitchTo().ActiveElement().Click();

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}

