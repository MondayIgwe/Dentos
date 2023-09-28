using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class MainProcessMenu : ITask
    {
        public MainProcessMenuAction MenuAction { get;}

        private MainProcessMenu(MainProcessMenuAction menuAction) => MenuAction = menuAction;

        public static MainProcessMenu ClickOn(MainProcessMenuAction menuAction) => new(menuAction);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Clicks a process in the main form such as update or add.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var button = driver.FindElements(CommonLocator.FindButton.Query)
                .FirstOrDefault(ele => ele.Text.Equals(MenuAction.ToString()));

            actor.AttemptsTo(JScript.ClickOn(button));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
    
}
