using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.WIPProvision;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.WIPProvision
{
    public class EnterWIPProvisionData : ITask
    {

        public WIPProvisionEntity WipProvisionEntity { get; }

        private EnterWIPProvisionData(WIPProvisionEntity wipProvisionEntity) =>
            WipProvisionEntity = wipProvisionEntity;

        public static EnterWIPProvisionData With(WIPProvisionEntity wipProvisionEntity) => new(wipProvisionEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(WIPProvisionLocators.matter, WipProvisionEntity.Matter));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(WIPProvisionLocators.glDate, WipProvisionEntity.glDate));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(WIPProvisionLocators.throughDate, WipProvisionEntity.throughDate));
            actor.AttemptsTo(Dropdown.SelectOptionByName(WIPProvisionLocators.editType, WipProvisionEntity.editType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
