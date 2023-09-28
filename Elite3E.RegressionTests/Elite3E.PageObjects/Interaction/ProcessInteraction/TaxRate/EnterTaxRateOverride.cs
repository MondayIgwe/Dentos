using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using OpenQA.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxRate;
using Elite3E.PageObjects.Interaction.CommonInteraction;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.TaxRate
{
    public class EnterTaxRateOverride : ITask
    {
        public TaxRateEntity TaxRateEntity { get; }

        private EnterTaxRateOverride(TaxRateEntity taxRateEntity) =>
            TaxRateEntity = taxRateEntity;
        
        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(TaxRatesLocator.CutOffDate, TaxRateEntity.StartDate + Keys.Tab));
           
            if (!string.IsNullOrEmpty(TaxRateEntity.StartDate))
                actor.AttemptsTo(SendKeys.To(TaxRatesLocator.CutOffDate, TaxRateEntity.StartDate + Keys.Tab));            
        }
    }
}
