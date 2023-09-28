using Boa.Constrictor.Screenplay;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.OperatingUnit
{
    public  class UpdateOperatingUnit : ITask
    {
        public string OperatingUnit { get; }

        private UpdateOperatingUnit(string operatingUnit) =>
            OperatingUnit = operatingUnit;

        public static UpdateOperatingUnit ChangeTheUserUnit(string operatingUnit) => new(operatingUnit);
        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WorkList.View(WorkListViewEntity.Folder));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Dropdown.SelectOptionByName(ReceiptLocator.OperatingUnit, OperatingUnit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(WorkList.View(WorkListViewEntity.Modelling));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
