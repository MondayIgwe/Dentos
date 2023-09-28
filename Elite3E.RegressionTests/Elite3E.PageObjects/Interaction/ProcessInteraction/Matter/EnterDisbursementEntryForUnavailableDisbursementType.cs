using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class EnterDisbursementEntryForUnavailableDisbursementType : ITask
    {
        public List<DisbursementEntryEntity> DisbursementEntriesEntity { get; }
        public string MatterNumber { get; }

        private EnterDisbursementEntryForUnavailableDisbursementType(List<DisbursementEntryEntity> disbursementEntryEntity, string matterNumber)
        {
            DisbursementEntriesEntity = disbursementEntryEntity;
            MatterNumber = matterNumber;
        }

        public static EnterDisbursementEntryForUnavailableDisbursementType With(List<DisbursementEntryEntity> disbursementEntryEntity,
            string matterNumber) => new(disbursementEntryEntity, matterNumber);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            //  var clickAdd = false;

            foreach (var disbursementEntry in DisbursementEntriesEntity)
            {
                actor.AttemptsTo(SearchProcess.ByName(GlobalConstants.DisbursementEntry));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.WorkDate, disbursementEntry.WorkDate));
                actor.AttemptsTo(SendKeys.To(DisbursementModifyLocator.Matter, MatterNumber));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(SendKeys.To(DisbursementModifyLocator.DisbursementType, disbursementEntry.DisbursementType));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Dropdown.SelectOptionByName(DisbursementModifyLocator.WorkCurrency, disbursementEntry.WorkCurrency));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(SendKeys.To(DisbursementModifyLocator.WorkAmount, disbursementEntry.WorkAmount));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(Click.On(DisbursementModifyLocator.InternalComments));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                driver.FindElement(DisbursementModifyLocator.Narrative.Query).SendKeys(disbursementEntry.Narrative);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                if (!string.IsNullOrEmpty(disbursementEntry.TaxCode))
                {
                    actor.AttemptsTo(Lookup.SearchAndSelectSingle("Tax Code", disbursementEntry.TaxCode));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }

                actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.PostAll));
            }
        }
    }
}
