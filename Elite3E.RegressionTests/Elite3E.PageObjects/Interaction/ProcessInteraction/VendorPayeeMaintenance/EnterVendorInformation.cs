using Boa.Constrictor.Screenplay;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.VendorPayeeMaintenance
{
    public class EnterVendorInformation : ITask
    {
        public VendorEntity VendorEntity { get; }


        private EnterVendorInformation(VendorEntity vendorEntity)
        {
            VendorEntity = vendorEntity;
        }

        public static EnterVendorInformation EnterVendorData(VendorEntity vendorEntity) => new(vendorEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.GlobalVendor, VendorEntity.Vendor));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.VendorStatus, VendorEntity.Status));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
