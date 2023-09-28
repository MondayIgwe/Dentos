using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class GetMatterTaxOverrideForMatter : IQuestion<string>
    {
        private GetMatterTaxOverrideForMatter()
        {
        }

        public static GetMatterTaxOverrideForMatter Value() => new();

        public string RequestAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.WaitsUntil(Appearance.Of(MatterLocator.MatterTaxOverride), IsEqualTo.True());

            return actor.AsksFor(ValueAttribute.Of(MatterLocator.MatterTaxOverride)); 
        }
    }
}
