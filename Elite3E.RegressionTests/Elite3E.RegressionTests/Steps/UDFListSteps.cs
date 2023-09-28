using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.UDFList;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class UDFListSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public UDFListSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the UDF List process")]
        public void GivenINavigateToTheUDFListProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.UDFList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add a new UDF List and fill all the mandatory fields")]
        public void WhenIAddANewUDFListAndFillAllTheMandatoryFields(Table table)
        {
            var udfEntity = table.CreateInstance<UDFEntity>();
            _featureContext[StepConstants.UDFList] = udfEntity;
            _actor.AttemptsTo(EnterUDFListData.With(udfEntity));
        }

        [When(@"I add a new Attributes record providing the '(.*)'")]
        public void WhenIAddANewAttributesRecordProvidingThe(string udf)
        {
            var udfEntity = (UDFEntity)_featureContext[StepConstants.UDFList];
            udfEntity.UDF = udf;
            _featureContext[StepConstants.UDFLabel] = udf;
            _featureContext[StepConstants.UDFDesc] = udfEntity.Description;
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.Attributes, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(UDFLocators.udfInput, udfEntity.UDF));
            _actor.AttemptsTo(Click.On(UDFLocators.listHeader));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I submit the UDF list")]
        public void WhenISubmitTheUDFList()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the isPrintTemplate column should exist")]
        public void ThenTheIsPrintTemplateColumnShouldExist()
        {
            _actor.AttemptsTo(MainProcessMenu.ClickOn(MainProcessMenuAction.Update));
            _actor.WaitsUntil(Appearance.Of(CommonLocator.ErrorIcon), IsEqualTo.False(), 2);
            _actor.WaitsUntil(Existence.Of(UDFLocators.isPrintTemplateCol), IsEqualTo.True());
            _actor.WaitsUntil(Existence.Of(UDFLocators.isPrintTemplateCheckBox), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            ((UDFEntity)_featureContext[StepConstants.UDFList]).UDF = _actor.GetElementText(UDFLocators.udfInput);
            
        }

        [Then(@"the udf details should be saved correctly")]
        public void ThenTheDetialsShouldBeSavedCorrectly()
        {
            var udfEntity = (UDFEntity)_featureContext[StepConstants.UDFList];
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.UDFList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(udfEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var codeText = driver.FindElement(UDFValidationListLocators.codeValueElement.Query).Text.Trim();
            var udfText = driver.FindElement(UDFValidationListLocators.udfValueElement.Query).Text.Trim();

            codeText.Should().BeEquivalentTo(udfEntity.Code); 
            _actor.AsksFor(ValueAttribute.Of(UDFLocators.description)).Should().BeEquivalentTo(udfEntity.Description);
            udfText.Should().BeEquivalentTo(udfEntity.UDF);

        }
        [Then(@"I add a UDF list")]
        public void ThenIAddAUDFList()
        {
           
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            var udfEntity = (UDFEntity)_featureContext[StepConstants.UDFList];
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(UDFLocators.UDFList, udfEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.UDF, GlobalConstants.Grid));

        }

        [Then(@"Text should be updated from '([^']*)' to '([^']*)'")]
        public void ThenTextShouldBeNot(string newValue, string oldValue)
        {              
            _actor.WaitsUntil(Existence.Of(UDFLocators.isPrintTemplateCol), IsEqualTo.True());
            _actor.WaitsUntil(Existence.Of(UDFLocators.isPrintTemplateCheckBox), IsEqualTo.True());
            var getCurrentRoleText = _actor.GetElementText(UDFLocators.PrintTemplateLabel);
            getCurrentRoleText.Should().BeEquivalentTo(newValue);
            getCurrentRoleText.Should().NotBeEquivalentTo(oldValue);
        }
    }
}
