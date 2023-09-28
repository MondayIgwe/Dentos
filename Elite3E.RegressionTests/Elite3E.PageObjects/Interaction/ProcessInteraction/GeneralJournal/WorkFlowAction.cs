using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WorkFlowDashbord;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.WorkFlowDashBoard;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.GeneralJournal
{
    public class WorkFlowAction : ITask
    {
        public string JournalName { get; set; }
        public RibbonAction Action { get; set; }

        WorkFlowAction(string journalName, RibbonAction action)
        {
            JournalName = journalName;
            Action = action;
        }

        public static WorkFlowAction GeneralJournalAction(string journalName, RibbonAction action) =>
            new(journalName, action);


        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.GeneralJournalApproval));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(WorkFlowDashBoardFilter.Search(JournalName, GlobalConstants.ApprovalRequired));
          
            actor.WaitsUntil(Appearance.Of(WorkFlowDashBoardLocators.JournalContainerRow(JournalName)), IsEqualTo.True());

            var rowIndex = actor.GetElementAttribute(WorkFlowDashBoardLocators.JournalContainerRow(JournalName),"row-index");

            actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.OpenButtonByRow(rowIndex)));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            switch (Action)
            {
                case RibbonAction.Approve:
                    actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Approve));
                    break;
                case RibbonAction.Reject:
                    actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Reject));
                    break;
            }
            
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }
    }
}
