using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Collection_Details;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Collection;
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
    public class CollectionDetailsSteps
    {
        private readonly FeatureContext _featureContext;
        private readonly Actor _actor;

        public CollectionDetailsSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the collection items process")]
        public void GivenINavigateToTheCollectionItemsProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.CollectionItem,false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"I run the report using invoice in collection")]
        public void WhenIRunTheReportUsingInvoiceInCollection(Table table)
        {
            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();

            _actor.AttemptsTo(Click.On(CollectionDetailsLocator.InvoiceCollection));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(CollectionDetailsAdvancedSearch.GetSearchResults(searchCriteriaCol));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I search for Collection Details report")]
        public void GivenISearchForCollectionDetailsReport()
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SearchIcon));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchInput, "Collection Details"));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CollectionDetailsLocator.CollectionDetails));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the payer name '([^']*)' in the report")]
        public void ThenIVerifyThePayerNameInTheReport(string payerName)
        {
            _actor.AsksFor(Text.Of(CollectionDetailsLocator.Description)).Should().Contain(payerName);
        }

        [Then(@"I verify the fields in the Collection Details Report")]
        public void ThenIVerifyTheFieldsInTheCollectionDetailsReport()
        {
            _actor.DoesElementExist(CollectionDetailsLocator.ParameterHeader).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.CollectionItemLookup).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.CollectionStatus).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.Payer).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.Clients).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.Matters).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.Invoices).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.BillTimekeeper).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.RespTimekeeper).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.SupvTimekeeper).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.IncludeInactiveCollectionItemsCheckbox).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.IncludePaidCollectionInvoicesCheckbox).Should().BeTrue();
            _actor.DoesElementExist(CollectionDetailsLocator.IncludeReversedCollectionInvoicesCheckbox).Should().BeTrue();

        }

        [Then(@"I verify the collection total header in the report")]
        public void ThenIVerifyTheCollectionTotalHeaderInTheReport()
        {
            _actor.DoesElementExist(CollectionDetailsLocator.CollectionTotalHeader).Should().BeTrue();
        }


    }
}
