using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.VoucherMaintenance
{
    public class EnterBarristerFieldsData : ITask
    {
        public VendorEntity VendorEntity { get; }
        private EnterBarristerFieldsData(VendorEntity voucherEntity) =>
            VendorEntity = voucherEntity;
        public static EnterBarristerFieldsData EnterBarristerData(VendorEntity vendorEntity) => new(vendorEntity);

        public void PerformAs(IActor actor)
        {
            if (!string.IsNullOrEmpty(VendorEntity.BarristerGender))
            {
                actor.AttemptsTo(SendKeys.To(VendorLocators.BarristerGender_Field, VendorEntity.BarristerGender));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(VendorEntity.BarristerSeniority))
            {
                actor.AttemptsTo(SendKeys.To(VendorLocators.BarristerSeniority_Field, VendorEntity.BarristerSeniority));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(VendorEntity.BarristerName))
            {
                actor.AttemptsTo(SendKeys.To(VendorLocators.BarristerName_Field, VendorEntity.BarristerName));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            
        }
    }
}
