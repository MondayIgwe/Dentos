using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA;
using Elite3E.PageObjects.Interaction.CommonInteraction;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.CoA
{
    public class EnterCoALegacy : ITask
    {
        public CoALegacyEntity CoALegacyEntity { get; }

        private EnterCoALegacy(CoALegacyEntity coALegacyEntity) =>
            CoALegacyEntity = coALegacyEntity;

        public static EnterCoALegacy With(CoALegacyEntity coALegacyEntity) => new(coALegacyEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(CoALegacyLocator.CodeInput, CoALegacyEntity.Code));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(CoALegacyEntity.FirmDescription))
            {
                actor.AttemptsTo(SendKeys.To(CoALocalLocators.FirmDescription, CoALegacyEntity.FirmDescription));
            }

            if (!string.IsNullOrEmpty(CoALegacyEntity.LocalDescription))
            {
                actor.AttemptsTo(SendKeys.To(CoALocalLocators.LocalDescription, CoALegacyEntity.LocalDescription));
            }

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
