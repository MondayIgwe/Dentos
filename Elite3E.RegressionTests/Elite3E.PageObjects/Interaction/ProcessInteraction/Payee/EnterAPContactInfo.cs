using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payee;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payee
{
    public class EnterAPContactInfo : ITask
    {

        public PayerEntity PayeeEntity { get; }

        private EnterAPContactInfo(PayerEntity payeeEntity) =>
            PayeeEntity = payeeEntity;

        public static EnterAPContactInfo With(PayerEntity payeeEntity) =>
            new EnterAPContactInfo(payeeEntity);

        public void PerformAs(IActor _actor)
        {
            _actor.AttemptsTo(Click.On(PayeeLocator.APContactCard));
            _actor.AttemptsTo(Click.On(PayeeLocator.CreateAPContact));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(PayerLocator.ContactType, PayeeEntity.ContactType));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.FirstName, PayeeEntity.FirstName));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.LastName, PayeeEntity.LastName));
           //_actor.AttemptsTo(SendKeys.To(PayerLocator.ContactName, PayeeEntity.ContactName));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.EmailSalutation, PayeeEntity.Email));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.Email, PayeeEntity.Email + Keys.Tab));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.Okay));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
