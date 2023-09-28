using System;
using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.ChargeTypeGroup
{
    public class EnterAnticipatedChargeType : ITask
    {
        public List<string> ChargeType { get; }

        private EnterAnticipatedChargeType(List<String> chargeType) =>
            ChargeType = chargeType;

        public static EnterAnticipatedChargeType With(List<String> chargeType) => new(chargeType);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;


            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var chargeType in ChargeType)
            {

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                var input = driver.FindElement(AnticipatedTypeLocators.SearchTextBox.Query);

                input.SendKeys(chargeType);

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(Click.On(AnticipatedTypeLocators.SearchButton));
                break;

            }


        }
    }
}