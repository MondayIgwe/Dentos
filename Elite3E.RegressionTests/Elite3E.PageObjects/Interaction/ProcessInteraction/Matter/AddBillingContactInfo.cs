using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    /// <summary>
    /// Adds existing Billing Contact Info
    /// </summary>
    public class AddBillingContactInfo : ITask
    {
        public MatterEntity matterEntity { get; }

        private AddBillingContactInfo(MatterEntity MatterEntity) =>
            matterEntity = MatterEntity;

        public static AddBillingContactInfo With(MatterEntity matterEntity) =>
            new AddBillingContactInfo(matterEntity);

        public void PerformAs(IActor _actor)
        {
            _actor.AttemptsTo(SendKeys.To(MatterLocator.ContactPayerInput, matterEntity.Payer + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(MatterLocator.BillingContactSearchIcon("EntityPerson")));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, matterEntity.EntityPerson + Keys.Enter));
            _actor.AttemptsTo(Click.On(CommonLocator.QuickFindExactSearchResults(matterEntity.EntityPerson)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(MatterLocator.BillingContactSearchIcon("BillingContactType")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, matterEntity.ContactType + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.QuickFindExactSearchResults(matterEntity.ContactType)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(MatterLocator.BillingContactSearchIcon("EntityPersonEmail")));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, matterEntity.Email + Keys.Tab));
            _actor.AttemptsTo(Click.On(CommonLocator.QuickFindExactSearchResults(matterEntity.Email)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

          
        }

    }
}
