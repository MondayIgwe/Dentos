using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Selenium;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess
{
    public class AddVoucher : ITask
    {
        public EntryAndModifyProcessEntity VoucherDetails { get; }

        public string MatterNumber { get; }

        private AddVoucher(EntryAndModifyProcessEntity voucherDetails, string matterNumber)
        {
            VoucherDetails = voucherDetails;
            MatterNumber = matterNumber;
        }

        public static AddVoucher With(EntryAndModifyProcessEntity voucherDetails, string matterNumber) =>
            new(voucherDetails, matterNumber);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WorkList.View(WorkListViewEntity.Folder));
            actor.AttemptsTo(Dropdown.SelectOptionByName(ReceiptLocator.OperatingUnit, VoucherDetails.OperationUnit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.InvoiceNumberField, VoucherDetails.InvoiceNumber));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(VoucherDetails.InvoiceDate))
                actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.InvoiceDate, VoucherDetails.InvoiceDate));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.Payee, VoucherDetails.Payee));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.InvoiceAmount, VoucherDetails.InvoiceAmount));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.DisbursementCard,GlobalConstants.Form));

            actor.AttemptsTo(JScript.ClickOn(EntryAndModifyProcessLocators.VoucherAddButton));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherDisbursementType, VoucherDetails.DisbursementType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherMatter, MatterNumber));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherFeeEarner, VoucherDetails.FeeEarner));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherAmount, VoucherDetails.VoucherAmt));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(JScript.ClickOn(DisbursementModifyLocator.InternalComments));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());


            if (string.IsNullOrEmpty(actor.GetElementText(EntryAndModifyProcessLocators.NarrativeOnVoucher)))
                actor.FindOne(EntryAndModifyProcessLocators.NarrativeOnVoucherEditable).SendKeys(VoucherDetails.Narrative); 
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.TaxCode, VoucherDetails.TaxCode));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherOffice, VoucherDetails.Office));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(JScript.ClickOn(EntryAndModifyProcessLocators.DeTaxButton));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ProcessUpdateButton));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }

}