using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Enums;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.InvoiceDistributionMethod;
using Elite3E.PageObjects.Interaction.ProcessInteraction.InvoiceOverride;
using Elite3E.PageObjects.Interaction.ProcessInteraction.InvoiceOverrides;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Matter;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceDistributionMethod;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter_Notes;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class InvoiceDistributionMethodSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public InvoiceDistributionMethodSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I get the prefix for invoice override")]
        public void GivenIGetThePrefixForInvoiceOverride()
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
           var prefix =  driver.FindElement(InvoiceDistributionMethodLocator.NextInvoiceNumber.Query).GetAttribute("value").Split("-")[0];
            _featureContext[StepConstants.InvoicePrefix] = prefix;
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Given(@"I search or create invoice override")]
        public void GivenISearchOrCreateInvoiceOverride(Table table)
        {
            var invoiceOverride = table.CreateInstance<InvoiceOverrideEntity>();
            _actor.AttemptsTo(SearchProcess.ByName(Process.InvoiceOverrides));
            _actor.AttemptsTo(SearchOrCreateInvoiceOverride.With(invoiceOverride));
        }


        [Given(@"I search or create a matter attribute")]
        public void GivenISearchOrCreateAMatterAttribute(Table table)
        {
            var invoiceOverride = table.CreateInstance<InvoiceOverrideEntity>();
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterAttribute));
            _actor.AttemptsTo(SearchOrCreateMatterAttribute.With(invoiceOverride));
        }

        [Given(@"I search or create invoice type")]
        public void GivenISearchOrCreateInvoiceType(Table table)
        {
            var invoiceType = table.CreateInstance<InvoiceOverrideEntity>();
            _actor.AttemptsTo(SearchProcess.ByName(Process.InvoiceType));
            _actor.AttemptsTo(CreateInvoiceType.With(invoiceType));
        }
        [StepDefinition(@"I search or create invoice distribution method")]
        public void GivenISearchOrCreateInvoiceDistributionMethod(Table table)
        {
            var invoiceMethod = table.CreateInstance<InvoiceDistributionMethodEntity>();
            _actor.AttemptsTo(SearchProcess.ByName(Process.InvoiceDistributionMethod));
            _actor.AttemptsTo(CreateInvoiceDistributionMethod.With(invoiceMethod));
        }

        [Then(@"I verify the note type '([^']*)' has proforma flag set to true")]
        public void ThenIVerifyTheNoteTypeHasProformaFlagSetToTrue(string noteType)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.NoteType));
            _actor.AttemptsTo(QuickFind.Search(noteType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(SelectedState.Of(NotesLocator.ProformaCheckbox)).Should().BeTrue();
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

    }

}