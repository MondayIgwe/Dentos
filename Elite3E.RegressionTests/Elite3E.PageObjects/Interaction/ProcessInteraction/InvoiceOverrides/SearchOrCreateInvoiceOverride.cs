using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.InvoiceDistributionMethod;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceDistributionMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.InvoiceOverrides
{
    public class SearchOrCreateInvoiceOverride :ITask
    {
        public InvoiceOverrideEntity InvoiceOverrideEntity { get; }

        private SearchOrCreateInvoiceOverride(InvoiceOverrideEntity invoiceOverrideEntity)
        {
            InvoiceOverrideEntity = invoiceOverrideEntity;
        }

        public static SearchOrCreateInvoiceOverride With(InvoiceOverrideEntity invoiceOverrideEntity) => new(invoiceOverrideEntity);

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
                actor.AttemptsTo(SendKeys.To(InvoiceDistributionMethodLocator.Code, InvoiceOverrideEntity.Code));
                actor.AttemptsTo(SendKeys.To(InvoiceDistributionMethodLocator.Description, InvoiceOverrideEntity.Description));
                actor.AttemptsTo(SendKeys.To(InvoiceDistributionMethodLocator.NextInvoiceNumber, InvoiceOverrideEntity.NextInvoiceNumber));
                actor.AttemptsTo(SendKeys.To(InvoiceDistributionMethodLocator.NextCreditNoteNumber, InvoiceOverrideEntity.NextCreditNoteNumber));
                actor.AttemptsTo(SendKeys.To(InvoiceDistributionMethodLocator.NextTaxInvoiceNumber, InvoiceOverrideEntity.NextTaxInvoiceNumber));
                actor.AttemptsTo(SendKeys.To(InvoiceDistributionMethodLocator.NextCreditNoteTaxNumber, InvoiceOverrideEntity.NextCreditNoteTaxNumber));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }
    }
}
