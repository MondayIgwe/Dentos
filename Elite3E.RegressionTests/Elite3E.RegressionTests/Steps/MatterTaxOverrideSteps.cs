using System;
using System.Globalization;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Matter;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public sealed class MatterTaxOverrideSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public MatterTaxOverrideSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I add the matter tax override")]
        public void WhenIAddTheMatterTaxOverride(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterTaxOverride));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var matterTaxOverride = table.CreateInstance<MatterTaxOverrideEntity>();

            _featureContext[StepConstants.MatterTaxOverrideContext] = matterTaxOverride;

            _actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(EnterMatterTaxOverride.With(matterTaxOverride));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I search for the matter")]
        public void WhenISearchForTheMatter()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search for the created matter")]
        public void WhenISearchForTheCreatedMatter()
        {
            _actor.AttemptsTo(QuickFind.Search(_featureContext[StepConstants.MatterNumberContext].ToString()));
        }


        [When(@"I try to add duplicate tax override details")]
        [When(@"tax override details")]
        public void WhenTaxOverrideDetails(Table table)
        {
            var taxOverride = table.CreateInstance<TaxOverrideEntity>();
            _featureContext[StepConstants.TaxOverrideContext] = taxOverride;
            _actor.AttemptsTo(EnterTaxOverride.With(taxOverride));
        }

        [When(@"I search for the tax matter override")]
        public void WhenISearchForTheTaxMatterOverride()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterTaxOverride));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var searchText = ((MatterTaxOverrideEntity) _featureContext[StepConstants.MatterTaxOverrideContext])
                .Description;
            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"the matter tax override details are saved")]
        public void ThenTheMatterTaxOverrideDetailsAreSaved()
        {
            var actualMatterTaxData = _actor.AsksFor(GetMatterTaxOverride.Data());
            var expectedMatterTaxData = (MatterTaxOverrideEntity)_featureContext[StepConstants.MatterTaxOverrideContext];
            actualMatterTaxData.Code.Should().BeEquivalentTo(expectedMatterTaxData.Code);
            actualMatterTaxData.Description.Should().BeEquivalentTo(expectedMatterTaxData.Description);
            actualMatterTaxData.Default.Should().BeEquivalentTo(expectedMatterTaxData.Default);
            actualMatterTaxData.Active.Should().BeEquivalentTo(expectedMatterTaxData.Active);

            var dateFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            
            actualMatterTaxData.StartDate.Should().BeEquivalentTo(Convert.ToDateTime(expectedMatterTaxData.StartDate, dateFormat).ToShortDateString());
            actualMatterTaxData.EndDate.Should().BeEquivalentTo(Convert.ToDateTime(expectedMatterTaxData.EndDate, dateFormat).ToShortDateString());
        }

        [Then(@"the tax override details are saved")]
        public void ThenTheTaxOverrideDetailsAreSaved()
        {
            var actualTaxOverride = _actor.AsksFor(TaxOverrideGrid.Data());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var expectedTaxOverride = (TaxOverrideEntity)_featureContext[StepConstants.TaxOverrideContext];
            actualTaxOverride.Country.Should().BeEquivalentTo(expectedTaxOverride.Country);
            actualTaxOverride.TaxAreaOverride.Should().BeEquivalentTo(expectedTaxOverride.TaxAreaOverride);
        }
        
        [When(@"I search for the matter ""(.*)""")]
        public void WhenISearchForTheMatter(string matterNumber)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
        }

        [Then(@"I can add the tax override")]
        public void ThenICanAddTheTaxOverride()
        {
            var matterTaxOverride = (MatterTaxOverrideEntity)_featureContext[StepConstants.MatterTaxOverrideContext]  ;
            _actor.AttemptsTo(EnterMatterTaxOverrideForMatter.From(matterTaxOverride.Code));
        }

        [Then(@"the matter tax override is saved for the matter")]
        public void ThenTheMatterTaxOverrideIsSavedForTheMatter()
        {
            var actualMatterTaxOverride = _actor.AsksFor(GetMatterTaxOverrideForMatter.Value());
            var expectedMatterTaxOverride = ((MatterTaxOverrideEntity)_featureContext[StepConstants.MatterTaxOverrideContext])
                .Description;

            actualMatterTaxOverride.Should().BeEquivalentTo(expectedMatterTaxOverride);
        }

        [Then(@"I can save the matter without the matter tax override")]
        public void ThenICanSaveTheMatterWithoutTheMatterTaxOverride()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(MatterLocator.MatterTaxOverride), IsEqualTo.True());
            _actor.AttemptsTo(ScrollToElement.At(MatterLocator.BillionInformationHeader));
            _actor.AttemptsTo(Click.On(MatterLocator.MatterTaxOverride));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.PressKeyWithActions("delete");
            _actor.PressKeyWithActions("tab");

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.GetElementText(MatterLocator.MatterTaxOverride).Equals(string.Empty).Should().BeTrue();

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Submit), IsEqualTo.False());
        }
    }
}
