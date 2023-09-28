using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountTransfer;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.ClientAccountTransfer
{
    public class ClientAccountTransfer : ITask
    {
        public ClientAccountTransferEntity ClientTransferEntity { get; }

        private ClientAccountTransfer(ClientAccountTransferEntity clientAccountTransferEntity) =>
             ClientTransferEntity = clientAccountTransferEntity;

        public static ClientAccountTransfer With(ClientAccountTransferEntity clientAccountTransferEntity) =>
            new ClientAccountTransfer(clientAccountTransferEntity);
        public void PerformAs(IActor _actor)
        {
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientAccountTransferLocator.TransferType, ClientTransferEntity.TransferType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, "Transfer From"));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.FromAccount, ClientTransferEntity.FromAccount));
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.FromMatter, ClientTransferEntity.FromMatter));
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.FromIntendedUse, ClientTransferEntity.IntendedUse));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.FromAmount, ClientTransferEntity.Amount));
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.ToAccount, ClientTransferEntity.ToAccount));
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.ToMatter, ClientTransferEntity.ToMatter));
            _actor.AttemptsTo(SendKeys.To(ClientAccountTransferLocator.ToIntendedUse, ClientTransferEntity.IntendedUse + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.UpdateButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }
    }
}
