using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.InvoiceDistributionMethod
{
    public class SearchOrCreate : IQuestion<int>
    {
        public List<AdvancedFindSearchEntity> AdvancedFindSearch { get; }

        private SearchOrCreate(List<AdvancedFindSearchEntity> advancedFindSearch) =>
            AdvancedFindSearch = advancedFindSearch;

        public static SearchOrCreate AdvancedSearch(
            List<AdvancedFindSearchEntity> advancedFindSearch) => new(advancedFindSearch);

        public int RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            var recordCount = actor.AsksFor(AdvancedFind.GetSearchResults(AdvancedFindSearch));

            // check invoice distribution exists

            switch (recordCount.Count)
            {
                case > 0:
                    driver.FindElement(CommonLocator.SearchResultsCheckBox.Query).Click();
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    break;
                default:
                    actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    break;
            }

            return recordCount.Count;
        }
    }
}