using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.CoA
{
    public class GetCoALegacy : IQuestion<CoALegacyEntity>
    {

        private GetCoALegacy()
        {
        }

        public static GetCoALegacy Data() => new();

        public CoALegacyEntity RequestAs(IActor actor)
        {

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var coALegacyEntity = new CoALegacyEntity
            {
                Code = actor.AsksFor(Text.Of(CoALegacyLocator.GetCode)).Trim(),
                FirmDescription = actor.AsksFor(ValueAttribute.Of(CoALegacyLocator.FirmDescription)).Trim(),
                LocalDescription = actor.AsksFor(ValueAttribute.Of(CoALegacyLocator.LocalDescription)).Trim()
            };

            return coALegacyEntity;
        }
    }

}