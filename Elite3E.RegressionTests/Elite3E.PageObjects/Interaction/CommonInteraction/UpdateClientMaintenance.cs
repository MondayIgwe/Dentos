using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class UpdateClientMaintenance : ITask
    {
        private ClientEntity _clientEntity;
        private string _childForm { get; }
        public UpdateClientMaintenance(string childForm, ClientEntity clientEntity)
        {
            _clientEntity = clientEntity;
            _childForm = childForm;
        }
        public static UpdateClientMaintenance EditClientMaintenance(string childForm, ClientEntity _clientEntity) =>
         new(childForm, _clientEntity);
        public void PerformAs(IActor _actor)
        {

            if (_childForm == "Credit Details")
            {
                _actor.AttemptsTo(SendKeys.To(ClientLocators.RiskScore, _clientEntity.CreditDetailsEntity.RiskScore));
                _actor.AttemptsTo(SendKeys.To(ClientLocators.CreditScoreRating, _clientEntity.CreditDetailsEntity.CreditScoreRating));
                _actor.AttemptsTo(SendKeys.To(ClientLocators.CreditLimit, _clientEntity.CreditDetailsEntity.CreditLimit));
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Currency, _clientEntity.CreditDetailsEntity.Currency));
                _actor.AttemptsTo(SendKeys.To(ClientLocators.AMLRisk, _clientEntity.CreditDetailsEntity.AMLRisk));
                _actor.AttemptsTo(SendKeys.To(ClientLocators.FinanceRiskScore, _clientEntity.CreditDetailsEntity.FinanceRiskScore));
            }
            else if (_childForm == "Client Defaults")
            {
                _actor.AttemptsTo(SendKeys.To(ClientLocators.ClientDefaultsEntityOffice, _clientEntity.ClientDefaultsEntity.Office));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ClientLocators.Department, _clientEntity.ClientDefaultsEntity.Department));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ClientLocators.PTAFees1, _clientEntity.ClientDefaultsEntity.PTAFees1));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ClientLocators.PTACost1, _clientEntity.ClientDefaultsEntity.PTACost1));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ClientLocators.PTACharge1, _clientEntity.ClientDefaultsEntity.PTACharge1));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ClientLocators.PTAFees2, _clientEntity.ClientDefaultsEntity.PTAFees2));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ClientLocators.PTACost2, _clientEntity.ClientDefaultsEntity.PTACost2));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ClientLocators.PTACharge2, _clientEntity.ClientDefaultsEntity.PTACharge2));
            }
        }
    }
}
