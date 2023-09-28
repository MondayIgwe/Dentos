using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Proforma
{
    public class GetProformaBillingContactDetails : IQuestion<ProformaGenerationEntity>
    {
        private GetProformaBillingContactDetails()
        {
        }

        public static GetProformaBillingContactDetails Data() =>
            new GetProformaBillingContactDetails();

        public ProformaGenerationEntity RequestAs(IActor actor)
        {

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(ProformaEditChargeLocator.BillingContact));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var payer = actor.GetDriver().FindElement(ProformaEditChargeLocator.GetProformaInput("Payor").Query).GetAttribute("value");
            actor.AttemptsTo(Click.On(ProformaEditChargeLocator.ClickProformaDiv("EntityPerson")));
            var contactname = actor.GetDriver().FindElement(ProformaEditChargeLocator.GetProformaInput("EntityPerson").Query).GetAttribute("value");
            actor.AttemptsTo(Click.On(ProformaEditChargeLocator.ClickProformaDiv("BillingContactType")));
            var contacttype = actor.GetDriver().FindElement(ProformaEditChargeLocator.GetProformaInput("BillingContactType").Query).GetAttribute("value");
            actor.AttemptsTo(Click.On(ProformaEditChargeLocator.ClickProformaDiv("EntityPersonEmail")));
           
            var email = actor.GetDriver().FindElement(ProformaEditChargeLocator.GetProformaInput("EntityPersonEmail").Query).GetAttribute("value");

            var proforma = new ProformaGenerationEntity()
            {
                Payer = payer,
                ContactType = contacttype,
                ContactName = contactname,
                Email=email
            };

            return proforma;
        }



    }
}
