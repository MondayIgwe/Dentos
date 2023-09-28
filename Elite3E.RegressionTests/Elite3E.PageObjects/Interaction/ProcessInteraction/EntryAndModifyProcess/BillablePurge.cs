using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess
{
    public class BillablePurge : ITask
    {
        public EntryAndModifyProcessEntity Purge { get; }

        private BillablePurge(EntryAndModifyProcessEntity purge)
        {
            Purge = purge;
        }
        public static BillablePurge With(EntryAndModifyProcessEntity purge) => new(purge);


        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ExpandChildProcess(GlobalConstants.DisbursementDetails)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            try
            {
                actor.WaitsUntil(Appearance.Of(CommonLocator.OpenGridFlyOutMenu(GlobalConstants.DisbursementDetails,
                    GlobalConstants.FormFull)), IsEqualTo.True(), 1);
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);

                actor.AttemptsTo(Click.On(CommonLocator.OpenGridFlyOutMenu(GlobalConstants.DisbursementDetails,
                        GlobalConstants.Grid)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Click.On(CommonLocator.GridFlyOutButtonClick(GlobalConstants.FormFull)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.AddNewButton(GlobalConstants.DisbursementDetails)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            // View the newly added record
            actor.AttemptsTo(Click.On(CommonLocator.ViewLastRecordOnGrid(GlobalConstants.DisbursementDetails)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.BillablePurgeDisbursementType, Purge.DisbursementType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.ProformaBillAmount, Purge.BillAmount));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.ProformaTaxCode, Purge.TaxCode));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //driver.FindElement(EntryAndModifyProcessLocators.DisbursementDetailsNarrative.Query).Clear();
            if(string.IsNullOrEmpty(actor.GetElementText(EntryAndModifyProcessLocators.NarrativeOnVoucher))) 
                actor.FindOne(EntryAndModifyProcessLocators.NarrativeOnVoucherEditable).SendKeys(Purge.Narrative);
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.AnticipatedCheckBox));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.SkipDisbursementEntry));
            //actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.AnticipatedCheckBox));
            //actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.BillablePurgeDisbursementType, Purge.DisbursementType));
            //actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.ProformaTaxCode, Purge.TaxCode));
            //actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //driver.FindElement(EntryAndModifyProcessLocators.NarrativeOnVoucher.Query).SendKeys(Purge.Narrative);
            //actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            // Change the form view back to grid view
        }
    }
}
