using System;
using System.Linq;
using System.Threading;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class SearchAndAddToChildProcess : ITask
    {
        public string SearchText { get; }
        public string SectionName { get; }

        private SearchAndAddToChildProcess(string sectionName, string searchText) {
            SearchText = searchText;
            SectionName = sectionName;
        }

        public static SearchAndAddToChildProcess With(string sectionName, string searchText) =>
            new(sectionName, searchText);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView,SectionName));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var rootElement = driver.FindElement(CommonLocator.ButtonContainer(SectionName).Query);

            var button = rootElement.FindElements(CommonLocator.AddButton.Query).FirstOrDefault(ele => ele.Text.Trim().Equals("Add"));
            
            actor.AttemptsTo(JScript.ClickOn(button));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            Thread.Sleep(TimeSpan.FromSeconds(2)); 

            driver.SwitchTo().ActiveElement().SendKeys(SearchText);

            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            //Get the row id from the results displayed
            var rowIndex = driver.FindElement(CommonLocator.QuickFindSearchResults(SearchText).Query).GetAttribute("row-index");

            driver.FindElement(CommonLocator.SelectResultsByRowId(SearchText, rowIndex).Query).Click();

            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
