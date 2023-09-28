using System;
using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CostTypeGroup;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess
{
    public class ValidateCostTypeGroupsOnMatter : ITask
    {
        public List<string> AddGroups { get; }

        private ValidateCostTypeGroupsOnMatter(List<String> chargeTypeGroup) =>
            AddGroups = chargeTypeGroup;

        public static ValidateCostTypeGroupsOnMatter With(List<String> GroupsOnMatter) => new(GroupsOnMatter);

        public void PerformAs(IActor actor)
        {
            foreach (var costTypeGroup in AddGroups)
            {
                actor.WaitsUntil(Existence.Of(CostTypeGroupLocators.ValidateCostTypeGroup(costTypeGroup)), IsEqualTo.True());
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }
    }
}
