using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Time;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class GenerateabillSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public GenerateabillSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Then(@"the invoice is generated")]
        public void ThenTheInvoiceIsGenerated()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var invoiceNumber = message.Split(":")[1].Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0];

            _featureContext[StepConstants.InvoiceNumberContext] = invoiceNumber;
            Console.WriteLine("Invoice Number: " + invoiceNumber);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"add  time entry")]
        public void WhenAddTimeEntry(Table table)
        {
            var timeEntry = table.CreateInstance<TimeEntryEntity>();
            timeEntry.Matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            //If entity is null and feature context is not null, attempt feature context. Else, use entity.
            timeEntry.FeeEarner = (string.IsNullOrEmpty(timeEntry.FeeEarner) && (_featureContext[StepConstants.FeeEarnerName] != null)) ? _featureContext[StepConstants.FeeEarnerName].ToString() : timeEntry.FeeEarner;

            if (!string.IsNullOrEmpty(timeEntry.FeeEarner))
                _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.FeeEarner, timeEntry.FeeEarner));

            if (!string.IsNullOrEmpty(timeEntry.Matter))
                _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.Matter, timeEntry.Matter));

            if (!string.IsNullOrEmpty(timeEntry.TimeType))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(TimeEntryLocator.TimeType, timeEntry.TimeType));

            if (!string.IsNullOrEmpty(timeEntry.Hours))
                _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.Hours, timeEntry.Hours));
                
            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.TaxCode, table.Rows[0][ColumnNames.TaxCode])); 
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(timeEntry.Narrative))
            {
                var driver = _actor.Using<BrowseTheWeb>().WebDriver;
                driver.FindElement(TimeEntryLocator.Narrative.Query).SendKeys(timeEntry.Narrative);
            }
        }
    }
}
