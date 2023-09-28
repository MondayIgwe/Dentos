using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.Infrastructure.Entity.ProformaEdit;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Proforma
{
    public class EnterChargeDetails : ITask
    {
        public ProformaEditChargeEntity ProformaEditChargeEntity { get; }

        private EnterChargeDetails(ProformaEditChargeEntity proformaEditChargeEntity)
        {
            ProformaEditChargeEntity = proformaEditChargeEntity;
        }

        public static EnterChargeDetails With(ProformaEditChargeEntity proformaEditChargeEntity) =>
            new(proformaEditChargeEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView,null));

            actor.AttemptsTo(Click.On(ProformaEditChargeLocator.ViewChargeDetailsGrid));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(ProformaEditChargeLocator.AddNewButton));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ChildProcessView.SwitchToView("Charge Details", GlobalConstants.FormFull));

            actor.AttemptsTo(SendKeys.To(ProformaEditChargeLocator.ChargeTypeInput, ProformaEditChargeEntity.ChargeType));
            
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(ProformaEditChargeLocator.WorkAmountInput, ProformaEditChargeEntity.WorkAmount));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
;