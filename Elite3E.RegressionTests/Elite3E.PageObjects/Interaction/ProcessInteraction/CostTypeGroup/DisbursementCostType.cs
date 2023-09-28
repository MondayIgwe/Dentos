using System;
using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.CostTypeGroup
{
    public class EnterDisbursementCostType : ITask
    {
        public List<string> CostType { get; }

        private EnterDisbursementCostType(List<String> costType) =>
            CostType = costType;

        public static EnterDisbursementCostType With(List<String> costType) => new(costType);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            foreach (var costType in CostType)
            {
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                var input = driver.FindElement(AnticipatedTypeLocators.SearchTextBox.Query);

                input.SendKeys(costType);

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(Click.On(AnticipatedTypeLocators.SearchButton));
                break;

            }
        }
    }
}