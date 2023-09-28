using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess
{
    public class AddCostTypeGroupsOnMatter : ITask
    {
        public string CostTypeGroup { get; }
        private const string Section = "Cost Type Group";

        private AddCostTypeGroupsOnMatter(string costTypeGroup) =>
            CostTypeGroup = costTypeGroup;

        public static AddCostTypeGroupsOnMatter With(string costTypeGroup) => new(costTypeGroup);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ChildProcessMenu.ClickOn(Section, ChildProcessMenuAction.Add));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.SearchTextBox, CostTypeGroup));

            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSearchButton));
            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeRadioButton));
            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSelectButton));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
    }
}