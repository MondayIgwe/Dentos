using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxRate;
using System.Linq;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.RateMaintenance
{
    public class FutureDateDetail : ITask
    {


        private FutureDateDetail() {}
            

        public static FutureDateDetail Select() => new();

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver; 
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var gridRows = driver.FindElements(TaxRatesLocator.RateDetailsGridAmount.Query);

            var futureDateDetails = gridRows.FirstOrDefault(ele => ele.GetAttribute("col-id").Equals("IsSelected"));
            futureDateDetails?.Click();
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
