using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class DeleteProcess : ITask
    {
        public DeleteProcess()
        {

        }
        public static DeleteProcess ClickDelete() => new();

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(JScript.ClickOn(CommonLocator.ParentProcessDeleteButton));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.WaitsUntil(Appearance.Of(CommonLocator.ParentProcessAddButton), IsEqualTo.False());

        }
    }
}
