using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.InvoiceDistributionMethod;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceType;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.InvoiceOverride
{
    public class CreateInvoiceType: ITask
    {
        public InvoiceOverrideEntity InvoiceOverrideEntity { get; }

        private CreateInvoiceType(InvoiceOverrideEntity invoiceOverrideEntity)
        {
            InvoiceOverrideEntity = invoiceOverrideEntity;
        }

        public static CreateInvoiceType With(InvoiceOverrideEntity invoiceOverrideEntity) => new(invoiceOverrideEntity);

        public void PerformAs(IActor actor)
        {
            var searchItems = new AdvancedFindSearchEntity()
            {
                SearchColumn = "Code",
                SearchOperator = "Equals",
                SearchValue = InvoiceOverrideEntity.Code
            };
            List<AdvancedFindSearchEntity> searchList = new List<AdvancedFindSearchEntity>();
            searchList.Add(searchItems);
            int rowCount = actor.AskingFor(SearchOrCreate.AdvancedSearch(searchList));
            if (!(rowCount > 0))
            {
                actor.AttemptsTo(SendKeys.To(InvoiceTypeLocator.InvCode, InvoiceOverrideEntity.Code));
                actor.AttemptsTo(SendKeys.To(InvoiceTypeLocator.InvDescription, InvoiceOverrideEntity.Description));
                actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.InvoiceOverrideUnitOffice));

                actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.InvoiceOverrideUnitOffice, ChildProcessMenuAction.Add));
                if (!string.IsNullOrEmpty(InvoiceOverrideEntity.Unit))
                {
                    actor.AttemptsTo(Dropdown.SelectOptionByName(InvoiceTypeLocator.Unit, InvoiceOverrideEntity.Unit));
                }

                if (!string.IsNullOrEmpty(InvoiceOverrideEntity.Office))
                {
                    actor.AttemptsTo(Click.On(InvoiceTypeLocator.OfficeCell));
                    actor.AttemptsTo(Dropdown.SelectOptionByName(InvoiceTypeLocator.Office, InvoiceOverrideEntity.Office));
                }
                else
                {
                    actor.AttemptsTo(Click.On(InvoiceTypeLocator.OfficeCell));
                    actor.AttemptsTo(SendKeys.To(InvoiceTypeLocator.Office,""));
                }
                actor.AttemptsTo(Click.On(InvoiceTypeLocator.InvoiceOverrideCell));
                actor.AttemptsTo(Dropdown.SelectOptionByName(InvoiceTypeLocator.InvoiceOverrideValue, InvoiceOverrideEntity.OverrideValue));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }
    }
}
