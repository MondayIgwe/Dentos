using System;
using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CostTypeGroup;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.CostTypeGroup
{
    public class EnterCostTypeGroup : ITask
    {
        public List<string> CostTypeGroups { get; }
        const string Section = "Cost Type Detail";

        private EnterCostTypeGroup(List<String> costTypeGroups) =>
            CostTypeGroups = costTypeGroups;

        public static EnterCostTypeGroup With(List<String> costTypeGroups) => new(costTypeGroups);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            foreach (var costTypeGroup in CostTypeGroups)
            {
                actor.AttemptsTo(ChildProcessMenu.ClickOn(Section,ChildProcessMenuAction.Add));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                if (costTypeGroup.Equals("Catering") || costTypeGroup.Equals("Airfare") || costTypeGroup.Equals("Courier & Delivery"))
                {
                    actor.AttemptsTo(Click.On(CostTypeGroupLocators.SearchIcon));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, costTypeGroup));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSearchButton));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(Click.On(CostTypeGroupLocators.DisbursementOption(costTypeGroup)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSelectButton));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
                else
                {
                    var input = driver.FindElement(CostTypeGroupLocators.InputLocator.Query);
                    input.SendKeys(costTypeGroup);
                }
            }

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}