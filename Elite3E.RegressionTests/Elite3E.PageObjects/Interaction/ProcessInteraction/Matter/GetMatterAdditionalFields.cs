using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class GetMatterAdditionalFields : IQuestion<MatterEntity>
    {

        private GetMatterAdditionalFields()
        {
        }

        public static GetMatterAdditionalFields Data() => new();

        public MatterEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var matter = new MatterEntity()
            {
                AdditionalMatterNumber = driver.FindElement(MatterLocator.AdditionalMatterNumberField.Query).GetAttribute("value"),
                CostMarkUp = driver.FindElement(MatterLocator.CostMarkUpField.Query).GetAttribute("value").Replace("0",""),
                GrossMarkUp = driver.FindElement(MatterLocator.GrossMarkUpField.Query).GetAttribute("value").Remove(1),
            };

            return matter;
        }
    }
}



