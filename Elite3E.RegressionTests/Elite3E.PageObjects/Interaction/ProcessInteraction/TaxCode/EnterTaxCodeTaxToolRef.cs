using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxCode;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.TaxCode
{
    public class EnterTaxCodeTaxToolRef : ITask
    {
        public string TaxCodeToolRef { get; }

        private EnterTaxCodeTaxToolRef(string taxCodeToolRef) =>
            TaxCodeToolRef = taxCodeToolRef;

        public static EnterTaxCodeTaxToolRef From(string taxCodeToolRef) => new(taxCodeToolRef);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(TaxCodeLocator.TaxToolRef, TaxCodeToolRef));

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
    }
}
