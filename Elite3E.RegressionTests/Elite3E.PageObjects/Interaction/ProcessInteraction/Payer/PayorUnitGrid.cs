using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payer
{
    public class PayorUnitGrid : IQuestion<string>
    {
        private PayorUnitGrid()
        {
        }

        public static PayorUnitGrid Title() => new();

        public string RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            return driver.FindElement(PayorUnitSubProcessMenuLocator.Title.Query).Text;

        }
    }
    
}
