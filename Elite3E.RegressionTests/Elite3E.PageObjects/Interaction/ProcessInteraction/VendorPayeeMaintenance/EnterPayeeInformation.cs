using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.VendorPayeeMaintenance
{
    public class EnterPayeeInformation : ITask
    {
        public VendorEntity VendorEntity { get; }


        private EnterPayeeInformation(VendorEntity vendorEntity)
        {
            VendorEntity = vendorEntity;
        }

        public static EnterPayeeInformation EnterPayeeData(VendorEntity vendorEntity) => new(vendorEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.Payee));
            actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.Payee, ChildProcessMenuAction.Add));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //Check for Payee Duplicates
            actor.AttemptsTo(JScript.ClickOn(VendorLocators.CheckPayeeDuplicates));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var message = actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            message.Contains(VendorEntity.Message).Equals(true);
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.PaymentTerms, VendorEntity.PaymentTerms));
            actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.Office, VendorEntity.Office));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if(!String.IsNullOrEmpty(VendorEntity.TaxCertificateDate))
            {
                actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.TaxCertificateDate, VendorEntity.TaxCertificateDate));
            }
        }
    }
}
