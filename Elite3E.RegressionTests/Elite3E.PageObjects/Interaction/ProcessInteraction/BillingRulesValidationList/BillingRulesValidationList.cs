using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity.BillingRules;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.BillingRulesValidationList;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.BillingRulesValidationList
{
    public class BillingRulesValidationList : ITask
    {
        public BillingRulesValidationListEntity BillingRulesValidationListEntity { get; }

        private BillingRulesValidationList(BillingRulesValidationListEntity billingRulesValidationListEntity) =>
            BillingRulesValidationListEntity = billingRulesValidationListEntity;

        public static BillingRulesValidationList AddDataWith(
            BillingRulesValidationListEntity billingRulesValidationListEntity) => new(billingRulesValidationListEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(BillingRulesValidationListEntity.Code))
                actor.AttemptsTo(SendKeys.To(BillingRulesValidationListLocators.InputCode, BillingRulesValidationListEntity.Code));

            if (!string.IsNullOrEmpty(BillingRulesValidationListEntity.Description))
                actor.AttemptsTo(SendKeys.To(BillingRulesValidationListLocators.TextAreaDescription, BillingRulesValidationListEntity.Description));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
