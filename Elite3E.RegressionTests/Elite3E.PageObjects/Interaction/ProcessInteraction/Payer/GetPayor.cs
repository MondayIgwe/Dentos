using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payer
{
    public class GetPayor : IQuestion<PayerEntity>
    {

        private GetPayor()
        {
        }

        public static GetPayor Data() => new();

        public PayerEntity RequestAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var payor = new PayerEntity()
            {
                Entity = actor.AsksFor(ValueAttribute.Of(PayerLocator.Entity)).Trim(),
                PayerName = actor.AsksFor(ValueAttribute.Of(PayerLocator.PayerName)).Trim(),
                TaxArea = actor.AsksFor(ValueAttribute.Of(PayerLocator.TaxArea)).Trim(),
                Site = actor.AsksFor(ValueAttribute.Of(PayerLocator.Site)).Trim()
            };       

            return payor;
        }
    }
    
}
