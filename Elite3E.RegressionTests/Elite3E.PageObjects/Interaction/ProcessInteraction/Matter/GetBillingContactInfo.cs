using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class GetBillingContactInfo : IQuestion<MatterEntity>
    {
        private GetBillingContactInfo()
        {
        }

        public static GetBillingContactInfo Data() =>
            new GetBillingContactInfo();

        public MatterEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(MatterLocator.BillingContactCard));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var payor = driver.FindElement(MatterLocator.GetInput("Payor").Query).GetAttribute("value");
            actor.AttemptsTo(Click.On(MatterLocator.ClickDiv("ContactType")));
            var contacttype = driver.FindElement(MatterLocator.GetInput("ContactType").Query).GetAttribute("value");
            actor.AttemptsTo(Click.On(MatterLocator.ClickDiv("EntityPerson")));
            var contactname = driver.FindElement(MatterLocator.GetInput("EntityPerson").Query).GetAttribute("value");

            var matter = new MatterEntity()
            {
                Payer = payor,
                ContactType =contacttype,
                ContactName=contactname
            };

            return matter;
        }
    }
}
