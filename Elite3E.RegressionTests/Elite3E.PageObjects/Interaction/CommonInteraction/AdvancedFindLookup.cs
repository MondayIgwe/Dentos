using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class AdvancedFindLookup : ITask
    {
        public List<AdvancedFindSearchEntity> SearchCriteriaList { get; }

        private AdvancedFindLookup(List<AdvancedFindSearchEntity> searchCriteriaList) => SearchCriteriaList = searchCriteriaList;

        public static AdvancedFindLookup GetSearchResults(List<AdvancedFindSearchEntity> searchCriteriaList) =>
          new(searchCriteriaList);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Searches for a record(s) using a search criteria that includes the search column, search operator and search value.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            for (int count = 0; count < SearchCriteriaList.Count; count++)
            {
                var columnSearch =
                    driver.FindElement(CommonLocator.AdvanceFindLookupAttribute(count).Query);
                columnSearch.SendKeys(Keys.Control + "a");
                columnSearch.SendKeys(SearchCriteriaList[count].SearchColumn);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                columnSearch.SendKeys(Keys.Tab);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                driver.FindElement(CommonLocator.AdvanceFindLookupOperator(count).Query).SendKeys(SearchCriteriaList[count].SearchOperator + Keys.Tab);


                driver.FindElement(CommonLocator.AdvanceFindLookupValue(count).Query).SendKeys(SearchCriteriaList[count].SearchValue);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            actor.AttemptsTo(Click.On(CommonLocator.Submitdialog));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
