using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Collection;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Collection_Details
{
    public class CollectionDetailsAdvancedSearch : ITask
    {
        public List<AdvancedFindSearchEntity> SearchCriteriaList { get; }

        private CollectionDetailsAdvancedSearch(List<AdvancedFindSearchEntity> searchCriteriaList) => SearchCriteriaList = searchCriteriaList;

        public static CollectionDetailsAdvancedSearch GetSearchResults(List<AdvancedFindSearchEntity> searchCriteriaList) =>
          new(searchCriteriaList);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Searches for a record(s) using a search criteria that includes the search column, search operator and search value.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            for (int count = 0; count < SearchCriteriaList.Count; count++)
            {
                var columnSearch =
                    driver.FindElement(CollectionDetailsLocator.AdvanceFindLookupAttribute(count).Query);
                columnSearch.SendKeys(Keys.Control + "a");
                columnSearch.SendKeys(SearchCriteriaList[count].SearchColumn);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                columnSearch.SendKeys(Keys.Tab);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                driver.FindElement(CollectionDetailsLocator.AdvanceFindLookupOperator(count).Query).SendKeys(SearchCriteriaList[count].SearchOperator);


                driver.FindElement(CollectionDetailsLocator.AdvanceFindLookupValue(count).Query).SendKeys(SearchCriteriaList[count].SearchValue);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }
    }
}
