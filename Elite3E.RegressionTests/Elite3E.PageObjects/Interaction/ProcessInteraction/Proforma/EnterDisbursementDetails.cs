using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.Infrastructure.Entity.ProformaEdit;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Proforma
{
    public class EnterDisbursementDetails : ITask
    {
        public ProformaEditDisbursementEntity ProformaEditDisbursementEntity { get; }

        private EnterDisbursementDetails(ProformaEditDisbursementEntity proformaEditDisbursementEntity)
        {
            ProformaEditDisbursementEntity = proformaEditDisbursementEntity;
        }

        public static EnterDisbursementDetails With(ProformaEditDisbursementEntity proformaEditDisbursementEntity) =>
            new(proformaEditDisbursementEntity);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!(string.IsNullOrEmpty(ProformaEditDisbursementEntity.Reason)))
            {
                actor.AttemptsTo(Lookup.SearchAndSelectSingle("Disbursement Reason", ProformaEditDisbursementEntity.Reason));
            }
            
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.DisbursementDetails));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.DisbursementDetails, GlobalConstants.FormFull));
            
            try
            {
                actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ExpandChildProcess(GlobalConstants.DisbursementDetails)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            catch (Exception e)
            {
                Console.Write("Error: " + e.Message);
            }

            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.AddNewButton(GlobalConstants.DisbursementDetails)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.NextDisbursementEntry));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(ProformaEditDisbursementLocator.DisbursementTypeInput, ProformaEditDisbursementEntity.DisbursementType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(ProformaEditDisbursementLocator.IsAnticipatedCheckbox));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.WorkAmount, ProformaEditDisbursementEntity.WorkAmount));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.ProformaTaxCode, ProformaEditDisbursementEntity.TaxCode));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //Check to see if the Narrative text area is populated or not, if not the element will exist

            if (string.IsNullOrEmpty(actor.GetElementText(EntryAndModifyProcessLocators.NarrativeOnVoucher)))
                actor.FindOne(EntryAndModifyProcessLocators.NarrativeOnVoucherEditable).SendKeys(ProformaEditDisbursementEntity.Narrative);

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}