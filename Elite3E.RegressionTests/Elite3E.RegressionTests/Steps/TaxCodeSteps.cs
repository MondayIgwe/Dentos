using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.TaxCode;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class TaxCodeSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public TaxCodeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I add the tax code")]
        public void GivenIAddTheTaxCode(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.TaxCodes));

            var taxCode = table.CreateInstance<TaxCodeEntity>();
         
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.TaxCodeContext] = taxCode;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(EnterTaxCode.With(taxCode));

        }
        
        [When(@"I Search the tax code")]
        public void WhenISearchTheTaxCode()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.TaxCodes));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var searchText = ((TaxCodeEntity)_featureContext[StepConstants.TaxCodeContext])
                .Description;

            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"add the tax code tax")]
        public void GivenAddTheTaxCodeTax(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, string.Empty));

            var taxCodeTax = table.CreateInstance<TaxCodeTaxEntity>();
            _actor.AttemptsTo(EnterTaxCodeTaxData.With(taxCodeTax));
        }


        [Then(@"the tax code is saved")]
        public void ThenTheTaxCodeIsSaved()
        {
            var actualTaxCodeData = _actor.AsksFor(GetTaxCode.Data());
            var expectedTaxCodeData = (TaxCodeEntity)_featureContext[StepConstants.TaxCodeContext];

            actualTaxCodeData.Code.Should().BeEquivalentTo(expectedTaxCodeData.Code);
            actualTaxCodeData.Description.Should().BeEquivalentTo(expectedTaxCodeData.Description);

            if (string.IsNullOrEmpty(actualTaxCodeData.TaxToolRef))
            {
                string.IsNullOrEmpty(expectedTaxCodeData.TaxToolRef).Should().BeTrue();
            }
            else
            {
                actualTaxCodeData.TaxToolRef.Should().BeEquivalentTo(expectedTaxCodeData.TaxToolRef);
            }
            
        }

        [Given(@"I add the tax code tool ref")]
        public void GivenIAddTheTaxCodeToolRef(Table table)
        {
            var taxCode = table.CreateInstance<TaxCodeEntity>();
            var oldTaxCodeEntity = (TaxCodeEntity) _featureContext[StepConstants.TaxCodeContext];
            oldTaxCodeEntity.TaxToolRef = taxCode.TaxToolRef;

            _featureContext[StepConstants.TaxCodeContext] = oldTaxCodeEntity;

            _actor.AttemptsTo(EnterTaxCodeTaxToolRef.From(taxCode.TaxToolRef));

        }

        [Then(@"the tax code tool ref is saved")]
        public void ThenTheTaxCodeToolRefIsSaved()
        {
            var actualTaxCodeData = _actor.AsksFor(GetTaxCode.Data());           
            var expectedTaxCodeData = (TaxCodeEntity)_featureContext[StepConstants.TaxCodeContext];
            actualTaxCodeData.TaxToolRef.Should().BeEquivalentTo(expectedTaxCodeData.TaxToolRef);
        }


    }
}
