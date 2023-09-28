using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.CoA
{
    public class GetCoALocalAccount : IQuestion<CoALocalAccountEntity>
    {

        private GetCoALocalAccount()
        {
        }

        public static GetCoALocalAccount Data() => new();

        public CoALocalAccountEntity RequestAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var coALocalAccountEntity = new CoALocalAccountEntity
            {
                Natural = actor.AsksFor(Text.Of(CoALocalLocators.GetNatural)).Trim(),
                Unit = actor.AsksFor(Text.Of(CoALocalLocators.GetUnit)).Trim(),
                FirmDescription = actor.AsksFor(ValueAttribute.Of(CoALocalLocators.FirmDescription)).Trim(),
                LocalDescription = actor.AsksFor(ValueAttribute.Of(CoALocalLocators.LocalDescription)).Trim()
            };
            return coALocalAccountEntity;
        }
    }

}