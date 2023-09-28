using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Instance;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class InstanceSetupManagementSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        public InstanceSetupManagementSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I want to create a new instance type process")]
        public void WhenIWantToCreateANewInstance(Table table)
        {
            var instanceTypeEntity = table.CreateInstance<InstanceTypeEntity>();
            _featureContext[StepConstants.InstanceTypeEntity] = instanceTypeEntity;
            _featureContext[StepConstants.InstanceTypeIsProd] = instanceTypeEntity.IsProduction.ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.InstanceType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(instanceTypeEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (_actor.DoesElementExist(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(AddInstanceTypeProcess.AddInstanceTypeDetails(instanceTypeEntity));
            }
        }

        [Then(@"I want to verify that the record has been added")]
        public void ThenIWantToVerifyThatTheRecordHasBeenAdded()
        {
            var instanceTypeCode = ((InstanceTypeEntity)_featureContext[StepConstants.InstanceTypeEntity]).Code;
            var instanceTypeIsProd = _featureContext[StepConstants.InstanceTypeIsProd].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.InstanceType));
            _actor.AttemptsTo(QuickFind.Search(instanceTypeCode));
            bool state = _actor.AsksFor(SelectedState.Of(InstanceTypeLocators.GetIsProductionCheckbox));

            if (bool.Parse(instanceTypeIsProd))
                state.Should().BeTrue();
            else
                state.Should().BeFalse();
        }

        [When(@"I want to create another production instance type")]
        public void WhenIWantToCreateAnotherProductionInstanceType(Table table)
        {
            var instanceTypeEntity = table.CreateInstance<InstanceTypeEntity>();
            var instanceTypeCode = ((InstanceTypeEntity)_featureContext[StepConstants.InstanceTypeEntity]).Code;
            instanceTypeEntity.Code = instanceTypeEntity.Code + "2";
            _actor.AttemptsTo(SearchProcess.ByName(Process.InstanceType));
            _actor.AttemptsTo(QuickFind.Search(instanceTypeCode));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ParentProcessAddButton));
            _actor.AttemptsTo(AddInstanceTypeProcess.AddInstanceTypeDetails(instanceTypeEntity));
        }
    }
}
