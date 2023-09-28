using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    /// <summary>
    /// Creates a New Billing Contact Info
    /// </summary>
    public class EnterBillingContactInfo : ITask
    {
        public MatterEntity MatterEntity { get; }

        private EnterBillingContactInfo(MatterEntity matterEntity) =>
            MatterEntity = matterEntity;

        public static EnterBillingContactInfo With(MatterEntity matterEntity) =>
            new EnterBillingContactInfo(matterEntity);

        public void PerformAs(IActor _actor)
        {
            _actor.AttemptsTo(Click.On(MatterLocator.BillingContactCard));
            _actor.AttemptsTo(Click.On(MatterLocator.CreateContact));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(MatterLocator.PayorField), IsEqualTo.True());
            //SearchAndSelectSingle finds a hidden Payer input fiels which is not the exact field we are looking for 
            _actor.AttemptsTo(SendKeys.To(PayerLocator.BillingContactPayor, MatterEntity.Payer));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Contact Type", MatterEntity.ContactType));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.FirstName, MatterEntity.FirstName));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.LastName, MatterEntity.LastName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            //_actor.AttemptsTo(SendKeys.To(PayerLocator.ContactName, MatterEntity.ContactName));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.EmailSalutation, MatterEntity.Email));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.Email, MatterEntity.Email + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.Okay));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());          
        }
    }
}
