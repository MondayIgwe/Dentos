using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction._3EInstance;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator._3EInstance;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class _3EInstancesSetupManagementStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        public _3EInstancesSetupManagementStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
            
        }

        [Given(@"I have an existing three e instance")]
        [When(@"I want to create a three e instance")]
        public void WhenIWantToCreateAThreeEInstance(Table table)
        {
            var instanceEntity = table.CreateInstance<_3EInstanceEntity>();
            _featureContext[StepConstants._3InstancesCode] = instanceEntity.Code;

            instanceEntity.Region = (string.IsNullOrEmpty(instanceEntity.Region)) ? ((RegionEntity)_featureContext[StepConstants.RegionEntity]).Description : instanceEntity.Region;
            instanceEntity.InstanceType = (string.IsNullOrEmpty(instanceEntity.InstanceType)) ? ((InstanceTypeEntity)_featureContext[StepConstants.InstanceTypeEntity]).Description: instanceEntity.InstanceType;

            _actor.AttemptsTo(SearchProcess.ByName(Process._3EInstance));
            _actor.AttemptsTo(QuickFind.Search(instanceEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
          if (!_actor.DoesElementExist(CommonLocator.FindDivElementContainsText(instanceEntity.Code)))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());            
                _actor.AttemptsTo(Add3EInstanceProcess.Add3EInstanceDetails(instanceEntity));
            }
            else
            {

                if (_actor.DoesElementExist(CommonLocator.Close))
                    _actor.AttemptsTo(Click.On(CommonLocator.Close));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            }
        }       

        [When(@"I want to create another three e instance")]
        public void WhenIWantToCreateAnotherThreeEInstance(Table table)
        {
            var instanceEntity = table.CreateInstance<_3EInstanceEntity>();
            instanceEntity.Region = ((RegionEntity)_featureContext[StepConstants.RegionEntity]).Description;
            instanceEntity.InstanceType = ((InstanceTypeEntity)_featureContext[StepConstants.InstanceTypeEntity]).Description;

            _actor.AttemptsTo(SearchProcess.ByName(Process._3EInstance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Add3EInstanceProcess.Add3EInstanceDetails(instanceEntity));

        }
        [Then(@"I want to verify and delete created three e instance")]
        public void ThenIWantToVerifyAndDeleteCreatedThreeEInstance()
        {
            var instanceTypeCode = _featureContext[StepConstants._3InstancesCode].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process._3EInstance));
            _actor.AttemptsTo(QuickFind.Search(instanceTypeCode));
            bool state = _actor.AsksFor(SelectedState.Of(CommonLocator.GetIsDisableIntegrationCheckbox));
            state.Should().BeTrue();
            _actor.AttemptsTo(DeleteProcess.ClickDelete());
        }

        [Then(@"I want to edit and update the three e instance")]
        public void ThenIWantToEditAndUpdateTheThreeEInstance()
        {
            var instanceTypeCode = _featureContext[StepConstants._3InstancesCode].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process._3EInstance));
            _actor.AttemptsTo(QuickFind.Search(instanceTypeCode));
            _actor.AttemptsTo(Click.On(CommonLocator.GetIsDisableIntegrationCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ParentProcessUpdateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.WaitsUntil(Appearance.Of(CommonLocator.ParentProcessUpdateButton), IsEqualTo.False());
        }
        [Then(@"I want to delete the three e instance i added initially")]
        public void ThenIWantToDeleteCreatedThreeEInstance()
        {
            var instanceTypeCode = _featureContext[StepConstants._3InstancesCode].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process._3EInstance));
            _actor.AttemptsTo(QuickFind.Search(instanceTypeCode));
            _actor.AttemptsTo(DeleteProcess.ClickDelete());          
        }
    }
}
