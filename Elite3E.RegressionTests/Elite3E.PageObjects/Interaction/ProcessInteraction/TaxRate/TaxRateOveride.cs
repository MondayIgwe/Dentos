using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxRate;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.TaxRate
{
    public class TaxRateOverride : IQuestion<TaxRateEntity>
    {

        private TaxRateOverride()
        {
        }

        public static TaxRateOverride Data() => new();

        public TaxRateEntity RequestAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var taxRateOverrideEntity = new TaxRateEntity
            {
                StartDate = Convert.ToDateTime(actor.AsksFor(ValueAttribute.Of(TaxRatesLocator.CutOffDate)))
                    .ToShortDateString()
            };
            return taxRateOverrideEntity;
        }
    }

}
