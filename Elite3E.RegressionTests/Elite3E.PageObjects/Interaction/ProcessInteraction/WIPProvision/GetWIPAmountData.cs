using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.WIPProvision;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.WIPProvision
{
    public class GetWIPAmountData : IQuestion<WIPProvisionEntity>
    {

        private GetWIPAmountData()
        {
        }

        public static GetWIPAmountData Data() => new();

        public WIPProvisionEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var wipProvision = new WIPProvisionEntity()
            {
                Matter = driver.FindElement(WIPProvisionLocators.gridRowValue("Matter").Query).Text.Trim(),
                client = driver.FindElement(WIPProvisionLocators.gridRowValue("MatterRel.Client1.DisplayName").Query).Text,
                currency = driver.FindElement(WIPProvisionLocators.gridRowValue("Currency").Query).Text,
                wipfees = driver.FindElement(WIPProvisionLocators.gridRowValue("WIPFees_ccc").Query).Text,
                office = driver.FindElement(WIPProvisionLocators.gridRowValue("MattDateRel.Office").Query).Text,
                wipType = driver.FindElement(WIPProvisionLocators.gridRowValue("MatterRel.WPType").Query).Text
            };

            return wipProvision;
        }
    }
}



