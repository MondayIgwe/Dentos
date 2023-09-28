using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.VoucherMaintenance
{
    public class EnterVoucherMaintenanceData : ITask
    {
        public VendorEntity VendorEntity { get; }
        public string PayeeName { get; }
        private EnterVoucherMaintenanceData(VendorEntity voucherEntity, string payeeName) 
         {   VendorEntity = voucherEntity;
             PayeeName = payeeName;
         }
        public static EnterVoucherMaintenanceData EnterVoucherMaintenanceRequiredFields(VendorEntity vendorEntity,string payee) => new(vendorEntity, payee);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.InvoiceNumberField, VendorEntity.InvoiceNumber));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.InvoiceDate, VendorEntity.InvoiceDate));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.Payee, PayeeName));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
