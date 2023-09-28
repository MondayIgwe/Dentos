using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceManager;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class InvoiceManagerStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public InvoiceManagerStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }
        [Then(@"I want to see results with warning message")]
        public void ThenIWantToSeeResultsWithWarningMessage()
        {
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            message.Should().Contain("The system max row count");
        }

        [When(@"I search the results using '([^']*)' details")]
        public void WhenISearchTheResultsUsingPayerDetails(string lookup)
        {
            int max = 2;
            int counter = 0;
            if (lookup == StepConstants.Matter)
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle(lookup, _featureContext[StepConstants.MatterNumberContext].ToString()));
            else if (lookup == StepConstants.Payer)
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle(lookup, (_featureContext[StepConstants.PayerName].ToString())));
            else
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle(lookup, (_featureContext[StepConstants.Client].ToString())));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            while (counter < max)
            {
                _actor.AttemptsTo(Click.On(CommonLocator.InvoiceManagerSearch));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                if (_actor.DoesElementExist(CommonLocator.ButtonElementContainsText("Matter Group Enquiry"), 30))
                    break;
                counter++;
            }
            var totalRecords = _actor.GetElementText(CommonLocator.SearchResultCount);
            if (_actor.DoesElementExist(CommonLocator.InformationMessage))
            {
                var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
                var warning = message.Split(" ")[6].Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0];
                var IsWarningCorrect = int.Parse(warning) <= int.Parse(totalRecords);
                IsWarningCorrect.Should().BeTrue();
            }

        }
        [Then(@"I want to add a collection note")]
        public void ThenIWantToAddACollectionNote(Table table)
        {
            _actor.AttemptsTo(Click.On(InvoiceManagerLocators.FoundSearchCheckBox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("Add Collection Note")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(InvoiceManagerLocators.Comment, table.Rows[0][ColumnNames.Comment]));
            _featureContext[StepConstants.ClientMatterNotesContext] = table.Rows[0][ColumnNames.Comment];
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonContainsText("Save")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(CommonLocator.ButtonContainsText("Save")).Should().BeFalse();
        }

        [Then(@"I want to view the matter notes")]
        public void ThenIWantToViewTheMatterNotes()
        {
            _actor.AttemptsTo(Click.On(InvoiceManagerLocators.MatterGroupInquiryDropdown));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("View Matter Notes")));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var note = _actor.DoesElementExist(CommonLocator.FindDivElementContainsText(_featureContext[StepConstants.ClientMatterNotesContext].ToString()));
            note.Should().Be(true);
        }
        [Then(@"I get total number of invoices using client")]
        public void ThenIGetTotalNumberOfInvoices(Table table)
        {
            string totalRecords = "0";
            _actor.AttemptsTo(Click.On(MatterLocator.AdvancedFindTab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(MatterLocator.AdvancedSearchText(table.Rows[0][ColumnNames.IndexNumber]), _featureContext[StepConstants.Client].ToString()));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(AnticipatedTypeLocators.SearchButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (_actor.DoesElementExist(CommonLocator.SearchResultTotal))
                totalRecords = _actor.GetElementText(CommonLocator.SearchResultTotal).Split(" ")[1];

            _featureContext[StepConstants.InvoicesTotal] = totalRecords;
        }

        [Then(@"I want to see all outstanding invoices for the client")]
        public void ThenIWantToSeeAllOutshatndingInvoicesForTheClient()
        {
            var totalRecords = int.Parse(_actor.GetElementText(CommonLocator.SearchResultCount));
            var totalNoInInvoices = int.Parse(_featureContext[StepConstants.InvoicesTotal].ToString());
            totalRecords.Should().Be(totalNoInInvoices);
        }

    }
}
