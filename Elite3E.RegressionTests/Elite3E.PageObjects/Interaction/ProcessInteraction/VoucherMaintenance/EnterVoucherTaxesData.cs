using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.VoucherMaintenance
{
    public class EnterVoucherTaxesData : ITask
    {
        public string VoucherEntity { get; }

        private EnterVoucherTaxesData(string voucherEntity) =>
            VoucherEntity = voucherEntity;

        public static EnterVoucherTaxesData EnterVoucherTaxesInfo(string voucherEntity) => new(voucherEntity);
       
        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(Click.On(VendorLocators.VoucherTaxCard));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(VendorLocators.DivInput));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(VendorLocators.InputAmount, VoucherEntity));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
