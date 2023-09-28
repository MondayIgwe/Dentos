using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payee
{
    public class GetAPContactInfo : IQuestion<PayerEntity>
    {
        private GetAPContactInfo()
        {
        }

        public static GetAPContactInfo Data() =>
            new GetAPContactInfo();

        public PayerEntity RequestAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(PayeeLocator.APContactCard));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var contactname = actor.GetDriver().FindElement(PayeeLocator.GetPayeeInput("EntityPerson").Query).GetAttribute("value");
            actor.AttemptsTo(Click.On(MatterLocator.ClickDiv("BillingContactType")));
            var contacttype = actor.GetDriver().FindElement(PayeeLocator.GetPayeeInput("BillingContactType").Query).GetAttribute("value");
            actor.AttemptsTo(Click.On(MatterLocator.ClickDiv("EntityPersonEmail")));
           
            var email = actor.GetDriver().FindElement(PayeeLocator.GetPayeeInput("EntityPersonEmail").Query).GetAttribute("value");

            var payee = new PayerEntity()
            {
                Email = email,
                ContactType = contacttype,
                ContactName = contactname
            };

            return payee;
        }
    }
}
