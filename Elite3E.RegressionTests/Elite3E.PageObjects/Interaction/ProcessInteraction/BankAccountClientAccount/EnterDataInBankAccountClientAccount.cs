using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.BankAccountClientAccount
{
    public class EnterDataInBankAccountClientAccount :ITask
    {
        public BankAccountClientAccountEntity BankAccountEntity { get; }


        private EnterDataInBankAccountClientAccount(BankAccountClientAccountEntity bankAccountEntity) => BankAccountEntity = bankAccountEntity;

        public static EnterDataInBankAccountClientAccount With(BankAccountClientAccountEntity bankAccountEntity) => new(bankAccountEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.AccountName, BankAccountEntity.AccountName));
            actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.Description, BankAccountEntity.Description));
            actor.AttemptsTo(Dropdown.SelectOptionByName(BankAccountClientAccountLocators.MoneyType, BankAccountEntity.MoneyType));
            if(!String.IsNullOrEmpty(BankAccountEntity.BankGroupName))
            {
                    actor.AttemptsTo(Lookup.SearchAndSelectSingle("Bank Group", BankAccountEntity.BankGroupName));
            }
            actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.AccountNumber, BankAccountEntity.AccountNumber));
            actor.AttemptsTo(Dropdown.SelectOptionByName(BankAccountClientAccountLocators.Office, BankAccountEntity.Office));
            actor.AttemptsTo(Dropdown.SelectOptionByName(BankAccountClientAccountLocators.Currency, BankAccountEntity.Currency));
            actor.AttemptsTo(Dropdown.SelectOptionByName(BankAccountClientAccountLocators.Language, BankAccountEntity.Language));
            actor.AttemptsTo(Lookup.SearchAndSelectSingle("GL Type", BankAccountEntity.GLType));
            actor.AttemptsTo(Click.On(BankAccountClientAccountLocators.CashGlAccountSearch));
            actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, BankAccountEntity.CashGLAccount));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(CommonLocator.Record(BankAccountEntity.CashGLAccount)));
            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(BankAccountClientAccountLocators.ContraGlAccountSearch));
            actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, BankAccountEntity.ContraGLAccount));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(CommonLocator.Record(BankAccountEntity.ContraGLAccount)));
            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
