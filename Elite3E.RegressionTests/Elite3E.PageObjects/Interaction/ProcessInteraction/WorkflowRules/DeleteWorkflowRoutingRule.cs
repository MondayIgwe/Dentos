using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.WorkFlowRuleRouting;
using System;
namespace Elite3E.PageObjects.Interaction.ProcessInteraction.WorkflowRules
{
    public class DeleteWorkflowRoutingRule :ITask
    {

        private string DOARole { get; }
        private string RuleSet { get; }

        public DeleteWorkflowRoutingRule(string doaRole, string ruleSet)
        {
            DOARole = doaRole;
            RuleSet = ruleSet;
        }
        public static DeleteWorkflowRoutingRule With(string doaRole, string ruleSet) =>
            new(doaRole, ruleSet);


        public void PerformAs(IActor _actor)
        {
            int rowIndex = Convert.ToInt32(_actor.GetElementAttribute(WorkflowRuleRoutingLocators.RowIndex(DOARole),"aria-rowindex"));
            //rowIndex-1 is used to get to the right record as the first row in the table starts with aria-rowindex=2
            _actor.AttemptsTo(Click.On(WorkflowRuleRoutingLocators.RowRecord((rowIndex - 1).ToString())));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.WorkflowRule, ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
