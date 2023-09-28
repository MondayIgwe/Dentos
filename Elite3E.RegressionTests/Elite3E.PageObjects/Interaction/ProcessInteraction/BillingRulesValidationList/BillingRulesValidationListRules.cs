using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Entity.BillingRules;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.BillingRulesValidationList;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.BillingRulesValidationList
{
    public class BillingRulesValidationListRules : ITask
    {
        public BillingRulesValidationListRulesEntity BillingRulesValidationListRulesEntity { get; }

        private BillingRulesValidationListRules(BillingRulesValidationListRulesEntity billingRulesValidationListRulesEntity) =>
            BillingRulesValidationListRulesEntity = billingRulesValidationListRulesEntity;

        public static BillingRulesValidationListRules AddDataWith(
            BillingRulesValidationListRulesEntity billingRulesValidationListRulesEntity) =>
            new(billingRulesValidationListRulesEntity);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            string section = "Billing Rules Validation List - Rules";

            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, section));
            actor.AttemptsTo(ChildProcessMenu.ClickOn(section, ChildProcessMenuAction.Add));

            actor.AttemptsTo(SendKeys.To(BillingRulesValidationListLocators.BillingRule, BillingRulesValidationListRulesEntity.BillingRule));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Checkbox.SetStatus(BillingRulesValidationListLocators.BillingRuleCheckBox(BillingRulesValidationListRulesEntity.WarningCheckBox), true));

            actor.AttemptsTo(Checkbox.SetStatus(BillingRulesValidationListLocators.BillingRuleCheckBox(BillingRulesValidationListRulesEntity.ErrorCheckBox), true));
        }
    }
}
