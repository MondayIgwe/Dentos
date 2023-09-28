using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.UDF;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Selenium;
using OpenQA.Selenium;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class UDFSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public UDFSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }



        [Given(@"I navigate to the new UDF process")]
        public void GivenINavigateToTheNewUDFProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.UDF));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Given(@"I add a new UDF record")]
        public void GivenIAddANewUDFRecord()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I fill the UDF form record")]
        public void WhenIFillTheUDFFormRecord(Table table)
        {
            var udfEntity = table.CreateInstance<UDFEntity>();
            _actor.AttemptsTo(UDFData.EnterData(udfEntity));
        }

        [Then(@"all the UDF fields should exist along with the dropdown values")]
        public void ThenAllTheUDFFieldsShouldExistAlongWithTheDropdownValues()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(UDFLocators.validationList)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(UDFLocators.archetype)).Should().Be(true);
            var actualValues = _actor.AsksFor(GetAllDropdownValues.For(UDFLocators.type, CommonLocator.DropDownOptions));
            var expectedValues = GlobalConstants.UDFTypeDropdownValues.Where(x => !actualValues.Contains(x)).ToList();
            expectedValues.Should().BeEmpty();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I select the Validation List from the dropdown")]
        public void WhenISelectTheValidationListFromTheDropdown(Table table)
        {
            var udfEntity = table.CreateInstance<UDFEntity>();

            _actor.AttemptsTo(SendKeys.To(UDFLocators.validationListInput, udfEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (_actor.DoesElementExist(UDFLocators.activity(udfEntity.Code)))
            {
                _actor.AttemptsTo(Click.On(UDFLocators.activity(udfEntity.Code)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"I select the Validation List from the dropdown")]
        public void WhenISelectTheValidationListFromTheDropdown()
        {
            var udfEntity = (UDFEntity)_featureContext[StepConstants.UDFList];

            _actor.AttemptsTo(SendKeys.To(UDFLocators.validationListInput, udfEntity.Code + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (_actor.DoesElementExist(UDFLocators.activity(udfEntity.Code)))
            {
                _actor.AttemptsTo(Click.On(UDFLocators.activity(udfEntity.Code)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }


        [Then(@"I get error message about the duplicate code")]
        public void ThenIGetErrorMessageAboutTheDuplicateCode()
        {
            var messages = _actor.AsksFor(ProcessError.Messages());
            messages.Count.Should().Be(1);
            messages.Should().Contain(StepConstants.UDFDuplicateCodeErrorMessage);
        }

        [Then(@"the error should occur about the validation list field")]
        public void ThenTheErrorShouldOccurAboutTheValidationListField()
        {
            var messages = _actor.AsksFor(ProcessError.Messages());
            messages.Count.Should().Be(1);
            messages.Should().Contain(StepConstants.ValidationListRequired);
        }

        [Then(@"the error should occur about the archetype field")]
        public void ThenTheErrorShouldOccurAboutTheArchetypeField()
        {
            var messages = _actor.AsksFor(ProcessError.Messages());
            messages.Count.Should().Be(1);
            messages.Should().Contain(StepConstants.ArchetypeRequired);
        }

        [Then(@"I provide the archetype")]
        public void ThenIProvideTheArchetype(Table table)
        {
            _actor.AttemptsTo(SendKeys.To(UDFLocators.archetypeInput, table.Rows[0][ColumnNames.Archetype]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I select the Validation List from the dropdown")]
        public void ThenISelectTheValidationListFromTheDropdown()
        {
            _actor.AttemptsTo(SendKeys.To(UDFLocators.validationListInput, GlobalConstants.Activity));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(UDFLocators.activity("Activity")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }
}
