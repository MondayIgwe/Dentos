using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess
{
    public class CloneMatter : ITask
    {
        public EntryAndModifyProcessEntity cloneMatterNumber { get; }

        private CloneMatter(EntryAndModifyProcessEntity cloneMatter) =>
            cloneMatter = cloneMatterNumber;

        public static CloneMatter With(EntryAndModifyProcessEntity cloneMatter) => new(cloneMatter);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ProcessAddDropdown));

            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ProcessCloneButton));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }

}