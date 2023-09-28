using System;
using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess
{
    public class AddChargeTypeGroupsOnMatter : ITask
    {
        public List<string> AddGroups { get; }

        private AddChargeTypeGroupsOnMatter(List<String> addMatter) =>
            AddGroups = addMatter;

        public static AddChargeTypeGroupsOnMatter With(List<String> groupsOnMatter) => new(groupsOnMatter);

        public void PerformAs(IActor actor)
        {
            foreach (var newGroup in AddGroups)
            {
                actor.AttemptsTo(ChildProcessMenu.ClickOn("Charge Type Group", ChildProcessMenuAction.Add));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.SearchTextBox, newGroup));

                actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSearchButton));
                actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeRadioButton));
                actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSelectButton));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            actor.AttemptsTo(Click.On(CommonLocator.Update));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }
    }

}