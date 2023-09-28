using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Proforma
{
    public class EnterProformaBillingContactDetails : ITask
    {
        public ProformaGenerationEntity ProformaEntity { get; }

        private EnterProformaBillingContactDetails(ProformaGenerationEntity proformaEntity) =>
            ProformaEntity = proformaEntity;

        public static EnterProformaBillingContactDetails With(ProformaGenerationEntity proformaEntity) =>
            new EnterProformaBillingContactDetails(proformaEntity);

        public void PerformAs(IActor _actor)
        {
            _actor.AttemptsTo(Click.On(ProformaEditChargeLocator.BillingContact));
            _actor.AttemptsTo(Click.On(ProformaEditChargeLocator.CreateContact));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaEditChargeLocator.Payer, ProformaEntity.Payer));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.ContactType, ProformaEntity.ContactType));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.FirstName, ProformaEntity.FirstName));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.LastName, ProformaEntity.LastName));
         //   _actor.AttemptsTo(SendKeys.To(PayerLocator.ContactName, ProformaEntity.ContactName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(PayerLocator.EmailSalutation, ProformaEntity.Email));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(PayerLocator.Email, ProformaEntity.Email + Keys.Tab));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.Okay));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }


    }
}
