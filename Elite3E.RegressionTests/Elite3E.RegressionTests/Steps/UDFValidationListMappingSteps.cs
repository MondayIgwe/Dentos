using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.UDFValidationListMapping;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class UDFValidationListMappingSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public UDFValidationListMappingSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the UDF Validation List Mapping process")]
        public void GivenINavigateToTheUDFValidationListMappingProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.UDFValidationLisMapping));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add a new UDF Validation List Mapping")]
        public void GivenIAddANewUDFValidationListMapping()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.UDFValidationListMapping));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"all the required fields should exist")]
        public void ThenAllTheRequiredFieldsShouldExist()
        {
            bool fieldsExist = _actor.AsksFor(UDFValidationListMappingFields.doFieldsExist());
            fieldsExist.Should().Be(false);
        }

        [When(@"I complete all the required fields")]
        public void WhenICompleteAllTheRequiredFields(Table table)
        {
            _actor.AttemptsTo(SendKeys.To(UDFLocators.code, table.Rows[0][ColumnNames.Code]));
            _actor.AttemptsTo(SendKeys.To(UDFLocators.description, table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(UDFValidationListMappingLocators.parentList, table.Rows[0][ColumnNames.ParentList]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(UDFValidationListMappingLocators.childList, table.Rows[0][ColumnNames.ChildList]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }


        [When(@"I add the Mapping on the child form and complete all the mandatory fields")]
        public void WhenIAddTheMappingOnTheChildFormAndCompleteAllTheMandatoryFields(Table table)
        {
            var udfMappingEntity = table.CreateInstance<UDFValidationListMappingEntity>();
            _actor.AttemptsTo(EnterUDFValidationListMappingData.With(udfMappingEntity));
        }
        
      
    }
}
