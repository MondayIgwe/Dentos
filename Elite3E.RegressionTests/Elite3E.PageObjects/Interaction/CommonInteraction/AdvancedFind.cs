using System;
using System.Collections.Generic;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class AdvancedFind : IQuestion<List<Object>>
    {
        public List<AdvancedFindSearchEntity> SearchCriteria { get; }

        private AdvancedFind(List<AdvancedFindSearchEntity> searchCriteria) => SearchCriteria = searchCriteria;

        public static AdvancedFind GetSearchResults(List<AdvancedFindSearchEntity> searchCriteria) =>
            new(searchCriteria);

        public List<Object> RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Searches for a record(s) using a search criteria that includes the search column, search operator and search value.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(CommonLocator.FindDivElementContainsText("Advanced Find")));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            for (int count = 0; count < SearchCriteria.Count; count++)
            {
                var columnSearch =
                    driver.FindElement(CommonLocator.AdvanceFindSearchAttribute(count).Query);
                columnSearch.SendKeys(Keys.Control + "a");
                columnSearch.SendKeys(SearchCriteria[count].SearchColumn);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                columnSearch.SendKeys(Keys.Tab);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                if(actor.DoesElementExist(CommonLocator.AdvanceFindSearchOperatorArrow(count)))
                {
                    actor.AttemptsTo(Click.On(CommonLocator.AdvanceFindSearchOperatorArrow(count)));
                    actor.WaitsUntil(Appearance.Of(CommonLocator.AdvanceFindSearchOperatorOptions(SearchCriteria[count].SearchOperator)), IsEqualTo.True());
                    actor.AttemptsTo(ScrollToElement.At(CommonLocator.AdvanceFindSearchOperatorOptions(SearchCriteria[count].SearchOperator)));
                    actor.AttemptsTo(Click.On(CommonLocator.AdvanceFindSearchOperatorOptions(SearchCriteria[count].SearchOperator)));
                }
                else
                {
                    driver.FindElement(CommonLocator.AdvanceFindSearchOperator(count).Query).SendKeys(SearchCriteria[count].SearchOperator);
                }
                
                driver.FindElement(CommonLocator.AdvanceFindSearchValue(count).Query).SendKeys(SearchCriteria[count].SearchValue);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }


            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var rows = driver.FindElements(By.CssSelector("div[ref='centerContainer'] div[role='row']"));


            var gridList = new List<Object>();

            foreach (var row in rows)
            {
                var columns = row.FindElements(By.CssSelector("div[role='gridcell']"));
                var columnValues = from element in columns select element.Text;

                object obj = columnValues.ToArray();

                gridList.Add(obj);
            }

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            return gridList;
        }
    }
    
}
