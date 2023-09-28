using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxCode;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.TaxCode
{
    public class EnterTaxCode : ITask
    {
        public TaxCodeEntity TaxCodeEntity { get; }

        private EnterTaxCode(TaxCodeEntity taxCodeEntity) =>
            TaxCodeEntity = taxCodeEntity;

        public static EnterTaxCode With(TaxCodeEntity taxCodeEntity) => new(taxCodeEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(TaxCodeEntity.Code))
                actor.AttemptsTo(SendKeys.To(TaxCodeLocator.SetCode, TaxCodeEntity.Code));

            if (!string.IsNullOrEmpty(TaxCodeEntity.Description))
                actor.AttemptsTo(SendKeys.To(TaxCodeLocator.Description, TaxCodeEntity.Description));

            if(!string.IsNullOrEmpty(TaxCodeEntity.TaxToolRef))
                actor.AttemptsTo(SendKeys.To(TaxCodeLocator.TaxToolRef, TaxCodeEntity.TaxToolRef));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
