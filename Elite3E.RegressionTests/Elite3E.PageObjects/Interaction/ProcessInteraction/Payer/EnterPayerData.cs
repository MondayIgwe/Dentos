using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payer
{
    public class EnterPayerData : ITask
    {
        public PayerEntity PayerEntity { get; }

        private EnterPayerData(PayerEntity payerEntity) =>
            PayerEntity = payerEntity;

        public static EnterPayerData With(PayerEntity payerEntity) => new(payerEntity);

        public void PerformAs(IActor actor)
        {
            if (!string.IsNullOrEmpty(PayerEntity.Entity))
                actor.AttemptsTo(Lookup.SearchAndSelectSingle("Entity", PayerEntity.Entity));
            if (!string.IsNullOrEmpty(PayerEntity.PayerName))
                actor.AttemptsTo(SendKeys.To(PayerLocator.PayerName, PayerEntity.PayerName));
            if (!string.IsNullOrEmpty(PayerEntity.Site))
                actor.AttemptsTo(Lookup.SearchAndSelectSingle("Site", PayerEntity.Site));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(PayerEntity.TaxArea))
                actor.AttemptsTo(Dropdown.SelectOptionByName(PayerLocator.TaxArea, PayerEntity.TaxArea));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
