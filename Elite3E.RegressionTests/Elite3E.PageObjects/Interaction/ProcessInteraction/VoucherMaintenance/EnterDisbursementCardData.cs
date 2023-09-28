using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;
using System;
using System.Collections.Generic;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.VoucherMaintenance
{
    public class EnterDisbursementCardData : ITask
    {
        public VendorEntity VendorEntity { get; }
        public string MatterNumber { get; }

        private EnterDisbursementCardData(VendorEntity voucherEntity, string matterNumber)
        {
            VendorEntity = voucherEntity;
            MatterNumber = matterNumber;
        }

        public static EnterDisbursementCardData EnterDisbursementCardDetails(VendorEntity vendorEntity, string matter) => new(vendorEntity, matter);

        public void PerformAs(IActor actor)
        {
            //send keys to Disbursement Card
            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherMatter, MatterNumber));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Lookup.SearchAndSelectSingle("Disbursement Type", VendorEntity.DisbursementType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherAmount, VendorEntity.VoucherAmount));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(DisbursementModifyLocator.InternalComments));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (string.IsNullOrEmpty(actor.GetElementText(EntryAndModifyProcessLocators.NarrativeOnVoucher)))
                actor.FindOne(EntryAndModifyProcessLocators.NarrativeOnVoucherEditable).SendKeys(VendorEntity.Narrative);

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(VendorLocators.InputTaxCode, VendorEntity.InputTaxCode));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!string.IsNullOrEmpty(VendorEntity.TaxCode))
             {
                actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.TaxCode, VendorEntity.TaxCode));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            
            
        }

    }
}
