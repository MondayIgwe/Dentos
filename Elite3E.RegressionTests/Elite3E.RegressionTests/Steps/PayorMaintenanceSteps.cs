using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Payer;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientGroup;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class PayorMaintenanceSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        public PayorMaintenanceSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I try to add a new payer")]
        public async Task WhenITryToAddANewPayer(Table table)
        {
            var payor = table.CreateInstance<PayerEntity>();

            payor.PayerName = payor.PayerName + "_" + Guid.NewGuid();

            _featureContext[StepConstants.PayerName] = payor.PayerName;

            payor.Entity = await new EntityData().SearchOrCreateAnEntityPerson(payor.Entity);

            _actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(EnterPayerData.With(payor));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

        }

        [When(@"I view the payor status")]
        public void WhenIViewThePayorStatus()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"it should have the status values")]
        public void ThenItShouldHaveTheStatusValues(Table table)
        {
            var statuses = table.CreateInstance<List<PayorStatusEntity>>();
            table.CompareToSet<PayorStatusEntity>(_actor.AsksFor(QuickFindPayorStatus.GridData()));
        }


        [When(@"I add additional tax area to the new payer")]
        public void WhenIAddAdditionalTaxAreaToTheNewPayer(Table table)
        {
            var payor = table.CreateInstance<PayerEntity>();

            _actor.AttemptsTo(EnterPayerData.With(payor));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [When(@"advanced find the payor")]
        public void WhenAdvancedFindThePayor(Table table)
        {
            table.Rows[0][ColumnNames.SearchValue] = _featureContext[StepConstants.PayerName].ToString();
            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            var payors = _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteriaCol));
            _featureContext[StepConstants.SearchedPayors] = payors;
        }

        [Then(@"the payor is found")]
        public void ThenThePayorIsFound()
        {
            var payors = (List<Object>)_featureContext[StepConstants.SearchedPayors];

            payors.Count.Should().Be(1);

            string[] payerInfo = ((IEnumerable)payors.First()).Cast<object>()
                .Select(x => x.ToString())
                .ToArray();

            var expectedPayer = _featureContext[StepConstants.PayerName].ToString();

            payerInfo.Should().Contain(expectedPayer);
        }


        [Given(@"I enter a new payer")]
        public async Task GivenIenteranewpayer(Table table)
        {
            var payor = table.CreateInstance<PayerEntity>();

            payor.PayerName = payor.PayerName + "_" + Guid.NewGuid();

            _featureContext[StepConstants.PayorContext] = payor;

            _featureContext[StepConstants.PayerName] = payor.PayerName;

            payor.Entity = await new EntityData().SearchOrCreateAnEntityPerson(payor.Entity);

            _actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(EnterPayerData.With(payor));
        }


        [When(@"I try to add a duplicate payor unit")]
        [When(@"I submit new payor unit")]
        public void WhenISubmitNewPayorUnit(Table table)
        {
            var payorUnits = table.CreateSet<PayerUnitEntity>().ToList();

            _featureContext[StepConstants.PayorUnitContext] = payorUnits;

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayorUnit));

            _actor.AttemptsTo(EnterPayerUnitData.With(payorUnits));

        }


        [When(@"search the new payer")]
        public void WhenSearchTheNewPayer()
        {
            _actor.AttemptsTo(QuickFind.Search(_featureContext[StepConstants.PayerName].ToString()));
        }

        [When(@"search for a payer")]
        public void WhenSearchForAPayer()
        {
            _actor.AttemptsTo(QuickFind.Search(_featureContext[StepConstants.PayerContext].ToString()));          
        }
        [StepDefinition(@"I select payer")]
        public void WhenISelectIt()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AttemptsTo(Click.On(CommonLocator.FindDivElementContainsExactText(_featureContext[StepConstants.PayerContext].ToString())));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(LocatorConstants.SelectButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"payor unit title is ""(.*)""")]
        public void ThenPayorUnitTitleIs(string value)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayorUnit));

            _actor.AsksFor(PayorUnitGrid.Title()).Should().BeEquivalentTo(value);
        }

        [Then(@"the payor is saved")]
        public void ThenThePayorIsSaved()
        {
            var expectedPayor = (PayerEntity)_featureContext[StepConstants.PayorContext];
            var actualPayor = _actor.AsksFor(GetPayor.Data());
            actualPayor.Entity.Should().BeEquivalentTo(expectedPayor.Entity);
            actualPayor.PayerName.Should().BeEquivalentTo(expectedPayor.PayerName);
            actualPayor.Site.Should().BeEquivalentTo(expectedPayor.Site);
            actualPayor.TaxArea.Should().BeEquivalentTo(expectedPayor.TaxArea);

        }

        [Then(@"the payor unit is saved")]
        public void ThenThePayorUnitIsSaved()
        {
            List<PayerUnitEntity> expectedPayorUnits = (List<PayerUnitEntity>)_featureContext[StepConstants.PayorUnitContext];
            List<PayerUnitEntity> actualPayorUnits = _actor.AsksFor(GetPayerUnit.Data());
            actualPayorUnits.Count.Should().Be(expectedPayorUnits.Count);

            actualPayorUnits.Should().BeEquivalentTo(expectedPayorUnits);
        }

        [Then(@"an error message ""(.*)"" is displayed")]
        public void ThenAnErrorMessageIsDisplayed(string message)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var messages = _actor.AsksFor(ProcessError.Messages());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            messages.Count.Should().Be(1);
            messages[0].Should().BeEquivalentTo(message);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Then(@"I verify the sections in payer")]
        public void ThenIVerifyTheSectionsInPayer()
        {
            _actor.DoesElementExist(PayerLocator.Entity).Should().Be(true);
            _actor.DoesElementExist(PayerLocator.PayerName).Should().Be(true);
            _actor.DoesElementExist(PayerLocator.Site).Should().Be(true);
            _actor.DoesElementExist(PayerLocator.TaxArea).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayorUnit));
            _actor.AsksFor(Field.IsAvailable(PayerLocator.PayorUnit)).Should().Be(true);
            _actor.AsksFor(Count.Of(PayerLocator.PayorUnit)).Should().Be(1);
            _actor.AsksFor(Field.IsAvailable(PayerLocator.CentralBillingCOntact)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(PayerLocator.BillingContacts)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


    }
}
