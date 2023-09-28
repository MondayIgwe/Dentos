using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.BankAccountClientAccount
{
    public class EnterDataInBankBranchMaintenance :ITask
    {
        public BankAccountClientAccountEntity BankAccountEntity { get; }
        private string EntityName { get; }

        private EnterDataInBankBranchMaintenance(BankAccountClientAccountEntity bankAccountEntity, string entityName)
        {
            BankAccountEntity = bankAccountEntity;
            EntityName = entityName;
        }

        public static EnterDataInBankBranchMaintenance With(BankAccountClientAccountEntity bankAccountEntity,string entityName) => new(bankAccountEntity,entityName);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(Click.On(BankAccountClientAccountLocators.EditDropDown));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(BankAccountClientAccountLocators.NewButton));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.EntityName, EntityName));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.Name, BankAccountEntity.BankName));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.ABARoutingNumber, BankAccountEntity.ABARoutingNumber));
            actor.AttemptsTo(Click.On(BankAccountClientAccountLocators.PositivePayClientAccountCheckbox));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.PositivePayClientTemplate, BankAccountEntity.PositivePayTemplate));
        }
    }
}
