using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceDistributionMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.InvoiceDistributionMethod
{
    public class CreateInvoiceDistributionMethod : ITask
    {
        public InvoiceDistributionMethodEntity InvoiceDistributionMethodEntity { get; }

        private CreateInvoiceDistributionMethod(InvoiceDistributionMethodEntity invoiceDistributionEntity)
        {
            InvoiceDistributionMethodEntity = invoiceDistributionEntity;
        }

        public static CreateInvoiceDistributionMethod With(InvoiceDistributionMethodEntity invoiceDistributionEntity) => new(invoiceDistributionEntity);

        public void PerformAs(IActor actor)
        {
            var searchItems = new AdvancedFindSearchEntity()
            {
                SearchColumn = "Code",
                SearchOperator = "Equals",
                SearchValue = InvoiceDistributionMethodEntity.Code
            };
            List<AdvancedFindSearchEntity> searchList = new List<AdvancedFindSearchEntity>();
            searchList.Add(searchItems);
            int rowCount = actor.AskingFor(SearchOrCreate.AdvancedSearch(searchList));
            if (!(rowCount > 0))
            {
                actor.AttemptsTo(SendKeys.To(InvoiceDistributionMethodLocator.Code, InvoiceDistributionMethodEntity.Code));
                actor.AttemptsTo(SendKeys.To(InvoiceDistributionMethodLocator.Description, InvoiceDistributionMethodEntity.Description));
                switch (InvoiceDistributionMethodEntity.DisptchOption.ToString().ToUpper())
                {
                    case "AUTO DISPATCH":
                        actor.AttemptsTo(Click.On(InvoiceDistributionMethodLocator.AutoDispatch));
                        break;
                    case "RTK DISPATCH":
                        actor.AttemptsTo(Click.On(InvoiceDistributionMethodLocator.RTKDispatch));
                        break;
                    case "FINANCE DISPATCH":
                        actor.AttemptsTo(Click.On(InvoiceDistributionMethodLocator.FinanceDispatch));
                        break;
                }
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }
    }
}
