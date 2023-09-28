using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class ChildProcessMenu : ITask
    {
        public ChildProcessMenuAction MenuAction { get;}
        public string SectionToFindButton { get; }

        private ChildProcessMenu(string sectionToFindButton, ChildProcessMenuAction menuAction)
        {
            SectionToFindButton = sectionToFindButton;
            MenuAction = menuAction;
        }

        public static ChildProcessMenu ClickOn(string sectionToFindButton, ChildProcessMenuAction menuAction) =>
            new(sectionToFindButton, menuAction);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Performs an action in the child process such as add or delete.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var xPath = $"//e3e-form-anchor-view-header[div/span[contains(text(),'{SectionToFindButton}')] or div/div/span[contains(text(),'{SectionToFindButton}')]]";

            var root = driver.FindElement(By.XPath(xPath));

            // Needed for Add new Button
            string action = MenuAction.ToString() == "AddNew" ? "Add New" : MenuAction.ToString();

            var button = root.FindElements(CommonLocator.ButtonElement.Query)
                .FirstOrDefault(ele => ele.Text.Equals(action));

            actor.AttemptsTo(JScript.ClickOn(button));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
    
}
