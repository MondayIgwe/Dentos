using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess
{
    public class PostDisbursement : ITask
    {
        public List<DisbursementEntryEntity> DisbursementEntries { get; }

        private PostDisbursement(List<DisbursementEntryEntity> disbursementEntry)
        {
            DisbursementEntries = disbursementEntry;
        }

        public static PostDisbursement EntryWith(List<DisbursementEntryEntity> disbursementEntry) =>
            new(disbursementEntry);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            var clickAdd = false;

            foreach (var entry in DisbursementEntries)
            {
                if (clickAdd)
                {
                    actor.AttemptsTo(JScript.ClickOn(EntryAndModifyProcessLocators.AddEntry));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
                if (!string.IsNullOrEmpty(entry.WorkDate))
                {
                    actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.WorkDate, entry.WorkDate));
                }
                actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.WorkCurrency, entry.WorkCurrency));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherMatter, entry.MatterNumber + Keys.Tab));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Lookup.SearchAndSelectSingle("Disbursement Type", entry.DisbursementType));

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.WorkAmount, entry.WorkAmount +Keys.Tab));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(Click.On(DisbursementModifyLocator.InternalComments));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                if (actor.DoesElementExist(EntryAndModifyProcessLocators.Narrative))
                {
                    if (!string.IsNullOrEmpty(entry.Narrative))
                    {
                        driver.FindElement(EntryAndModifyProcessLocators.Narrative.Query).SendKeys(entry.Narrative);
                    }
                }

                if (!string.IsNullOrEmpty(entry.TaxCode))
                {
                    actor.AttemptsTo(Lookup.SearchAndSelectSingle(GlobalConstants.TaxCode, entry.TaxCode));
                }

                clickAdd = true;
            }

        }
    }
}
