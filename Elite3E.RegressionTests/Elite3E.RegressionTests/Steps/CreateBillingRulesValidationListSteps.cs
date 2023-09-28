using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Entity.BillingRules;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.BillingRulesValidationList;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.BillingRulesValidationList;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class CreateBillingRulesValidationListSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public CreateBillingRulesValidationListSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I add a new billing rules validation with details:")]
        public void WhenIAddANewBillingRulesValidationWithDetails(Table table)
        {
            var billingRulesData = table.CreateInstance<BillingRulesValidationListEntity>();           
            _actor.AttemptsTo(BillingRulesValidationList.AddDataWith(billingRulesData));
            _featureContext[StepConstants.BillingRulesValidationList] = billingRulesData;
        }

        [When(@"I add billing rules validation list rules with details:")]
        public void WhenIAddBillingRulesValidationListRulesWithDetails(Table table)
        {
            var rulesData = table.CreateInstance<BillingRulesValidationListRulesEntity>();
            _actor.AttemptsTo(BillingRulesValidationListRules.AddDataWith(rulesData));
        }

        [When(@"Add billing rules validation list")]
        public void WhenAddBillingRulesValidationList(Table table)
        {
            var client = table.CreateInstance<ClientEntity>();
            _actor.AttemptsTo(SearchAndAddToChildProcess.With(StepConstants.BillingRulesValidationList, client.BillingRulesValidationList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I delete billing rules validation list")]
        public void ThenIDeleteBillingRulesValidationList()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ChildFormAction(StepConstants.BillingRulesValidationList, LocatorConstants.DeleteButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"I verify the added billing rules validatiton list")]
        public void ThenIVerifyTheAddedBillingRulesValidatitonList()
        {
            var expectedBillingRule = _featureContext[StepConstants.BillingRulesValidationListDescription].ToString();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.BillingRulesValidationList));

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            var actual = driver.FindElement(CommonLocator.ChildFormBillingRulesRedecription.Query).Text;

            actual.Should().Be(expectedBillingRule);

        }

        [Then(@"I should be able to validate the new billing rule validation list")]
        public void ThenIShouldBeAbleToValidateTheNewBillingRuleValidationList()
        {
            var billingRulesValidationList =  (BillingRulesValidationListEntity)_featureContext[StepConstants.BillingRulesValidationList];
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.BillingRulesValidationList));
            _actor.AttemptsTo(QuickFind.Search(billingRulesValidationList.Description));
            _actor.AsksFor(ValueAttribute.Of(BillingRulesValidationListLocators.TextAreaDescription)).Should().BeEquivalentTo(billingRulesValidationList.Description);
        }
    }
}
