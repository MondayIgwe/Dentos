using System;
using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.ChargeTypeGroup
{
    public class EnterChargeTypeGroup : ITask
    {
        public List<string> ChargeTypeGroups { get; }

        private EnterChargeTypeGroup(List<String> chargeTypeGroups) =>
            ChargeTypeGroups = chargeTypeGroups;

        public static EnterChargeTypeGroup With(List<String> chargeTypeGroups) => new(chargeTypeGroups);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            string section = "Charge Type Detail";

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var chargeTypeGroup in ChargeTypeGroups)
            {

                actor.AttemptsTo(ChildProcessMenu.ClickOn(section,ChildProcessMenuAction.Add));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                var input = driver.FindElement(ChargeTypeGroupLocators.InputLocator.Query);
                input.Click();
                input.SendKeys(chargeTypeGroup);

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
    }
}