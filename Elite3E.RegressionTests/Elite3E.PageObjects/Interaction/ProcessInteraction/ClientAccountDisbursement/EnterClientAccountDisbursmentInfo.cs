using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ClientAccountDisbursement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.ClientAccountDisbursement
{
    public class EnterClientAccountDisbursmentInfo : ITask
    {
        public ClientAccountDisbursementEntity ClientAccountDisbursementEntity { get; }

        private EnterClientAccountDisbursmentInfo(ClientAccountDisbursementEntity clientAccountDisbursementEntity) =>
             ClientAccountDisbursementEntity = clientAccountDisbursementEntity;

        public static EnterClientAccountDisbursmentInfo With(ClientAccountDisbursementEntity clientAccountDisbursementEntity) =>
            new EnterClientAccountDisbursmentInfo(clientAccountDisbursementEntity);

        public void PerformAs(IActor _actor)
        {
            int randomNumber = new Random().Next(999);
            
            ClientAccountDisbursementEntity.DocumentNumber += randomNumber;

            _actor.AttemptsTo(SendKeys.To(ClientAccountDisbursementLocators.TransactionDateInput, ClientAccountDisbursementEntity.TransactionDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientAccountDisbursementLocators.DisbursementTypeInput, ClientAccountDisbursementEntity.DisbursementType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Client Account Acct", ClientAccountDisbursementEntity.ClientAccountAcct));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Matter #", ClientAccountDisbursementEntity.MatterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientAccountDisbursementLocators.IntendedUseDropdown, ClientAccountDisbursementEntity.IntendedUse));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientAccountDisbursementLocators.AmountInput, ClientAccountDisbursementEntity.Amount));
            if(!String.IsNullOrEmpty(ClientAccountDisbursementEntity.UseDetails))
            {
                ClientAccountDisbursementEntity.UseDetails += randomNumber;
                _actor.AttemptsTo(SendKeys.To(ClientAccountDisbursementLocators.UseDetailsInput, ClientAccountDisbursementEntity.UseDetails));
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientAccountDisbursementLocators.DocumentNumberInput, ClientAccountDisbursementEntity.DocumentNumber));
            _actor.AttemptsTo(SendKeys.To(ClientAccountDisbursementLocators.PaymentNameInput, ClientAccountDisbursementEntity.PaymentName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
