using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA;
using OpenQA.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.CoA
{
    public class EnterCoALocalAccount : ITask
    {
        public CoALocalAccountEntity CoALocalAccountEntity { get; }

        private EnterCoALocalAccount(CoALocalAccountEntity coALocalAccountEntity) =>
            CoALocalAccountEntity = coALocalAccountEntity;

        public static EnterCoALocalAccount With(CoALocalAccountEntity coALocalAccountEntity) =>
            new(coALocalAccountEntity);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(CoALocalLocators.NaturalInput, CoALocalAccountEntity.Natural + Keys.Tab));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(CoALocalLocators.UnitInput, CoALocalAccountEntity.Unit));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(CoALocalAccountEntity.LocalAccount))
            {
                actor.AttemptsTo(SendKeys.To(CoALocalLocators.LocalAccount, CoALocalAccountEntity.LocalAccount));

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(CoALocalAccountEntity.FirmDescription))
            {
                actor.AttemptsTo(SendKeys.To(CoALocalLocators.FirmDescription, CoALocalAccountEntity.FirmDescription + Keys.Tab));
            }

            if (!string.IsNullOrEmpty(CoALocalAccountEntity.LocalDescription))
            {
                actor.AttemptsTo(SendKeys.To(CoALocalLocators.LocalDescription, CoALocalAccountEntity.LocalDescription + Keys.Tab));
            }

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.WaitsUntil(Existence.Of(CommonLocator.Submit), IsEqualTo.False());

        }
    }
}
