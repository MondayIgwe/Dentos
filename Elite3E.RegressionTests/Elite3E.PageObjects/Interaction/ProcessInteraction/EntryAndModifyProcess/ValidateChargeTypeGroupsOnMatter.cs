using System;
using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess
{
    public class ValidateChargeTypeGroupsOnMatter : ITask
    {
        public List<string> AddGroups { get; }

        private ValidateChargeTypeGroupsOnMatter(List<String> chargeTypeGroup) =>
            AddGroups = chargeTypeGroup;

        public static ValidateChargeTypeGroupsOnMatter With(List<String> GroupsOnMatter) => new(GroupsOnMatter);

        public void PerformAs(IActor actor)
        {
            foreach (var chargeTypeGroup in AddGroups)
            {
                actor.WaitsUntil(Existence.Of(ChargeTypeGroupLocators.ValidateChargeTypeGroup(chargeTypeGroup)), IsEqualTo.True());
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }
    }
}
