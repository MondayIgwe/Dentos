using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class EnterMatterTaxOverrideForMatter : ITask
    {
        public string TaxCodeToolRef { get; }

        private EnterMatterTaxOverrideForMatter(string taxCodeToolRef) =>
            TaxCodeToolRef = taxCodeToolRef;

        public static EnterMatterTaxOverrideForMatter From(string taxCodeToolRef) => new(taxCodeToolRef);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.WaitsUntil(Appearance.Of(MatterLocator.MatterTaxOverride), IsEqualTo.True());
            actor.AttemptsTo(Lookup.SearchAndSelectSingle("Matter Tax Override",TaxCodeToolRef));
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
    }
}
