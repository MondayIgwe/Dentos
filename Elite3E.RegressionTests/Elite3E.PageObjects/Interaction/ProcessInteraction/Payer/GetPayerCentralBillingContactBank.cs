using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payee;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payer
{
    public class GetPayerCentralBillingContactBank : IQuestion<PayerEntity>
    {
        private GetPayerCentralBillingContactBank()
        {
        }

        public static GetPayerCentralBillingContactBank Data() =>
            new GetPayerCentralBillingContactBank();

        public PayerEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(PayerLocator.CentralBillingCOntact));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var contactname = driver.FindElement(PayerLocator.GetPayerInput("EntityPerson").Query).GetAttribute("value");
            actor.AttemptsTo(Click.On(PayerLocator.ClickPayerDiv("BillingContactType")));
            var contacttype = driver.FindElement(PayerLocator.GetPayerInput("BillingContactType").Query).GetAttribute("value");
            actor.AttemptsTo(Click.On(PayerLocator.ClickPayerDiv("EntityPersonEmail")));
            var email = driver.FindElement(PayerLocator.GetPayerInput("EntityPersonEmail").Query).GetAttribute("value");

            var payor = new PayerEntity()
            {
                Email = email,
                ContactType = contacttype,
                ContactName = contactname
            };

            return payor;
        }
    }
}
