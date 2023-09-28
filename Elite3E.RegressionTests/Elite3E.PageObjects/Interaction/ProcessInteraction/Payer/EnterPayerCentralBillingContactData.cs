using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payer
{
    public class EnterPayerCentralBillingContactData : ITask
    {
        public PayerEntity PayerEntity { get; }

        private EnterPayerCentralBillingContactData(PayerEntity payerEntity) =>
            PayerEntity = payerEntity;

        public static EnterPayerCentralBillingContactData With(PayerEntity payerEntity) =>
            new EnterPayerCentralBillingContactData(payerEntity);

        public void PerformAs(IActor _actor)
        {
            _actor.AttemptsTo(Click.On(PayerLocator.CentralBillingCOntact));
            _actor.AttemptsTo(Click.On(PayerLocator.CreateContact));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Contact Type", PayerEntity.ContactType));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.FirstName, PayerEntity.FirstName));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.LastName, PayerEntity.LastName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(PayerLocator.EmailSalutation, PayerEntity.Email));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.Email, PayerEntity.Email + Keys.Tab));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.Okay));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
