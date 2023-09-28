using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.SetUpPropagation
{
    public class AddOrSelectAnSetUpPropagationProcess : IQuestion<bool>
    {
        public List<AdvancedFindSearchEntity> AdvancedFindSearch { get; }

        private AddOrSelectAnSetUpPropagationProcess(List<AdvancedFindSearchEntity> advancedFindSearch) =>
            AdvancedFindSearch = advancedFindSearch;

        public static AddOrSelectAnSetUpPropagationProcess GetProcessResult(
            List<AdvancedFindSearchEntity> advancedFindSearch) => new(advancedFindSearch);

        public bool RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            var recordCount = actor.AsksFor(AdvancedFind.GetSearchResults(AdvancedFindSearch));

            // check propagation exists 

            switch (recordCount.Count)
            {
                case > 0:
                    driver.FindElement(CommonLocator.SearchResultsCheckBox.Query).Click();
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    return true;
                default:
                    actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    return false;
            }

        }
    }
}
