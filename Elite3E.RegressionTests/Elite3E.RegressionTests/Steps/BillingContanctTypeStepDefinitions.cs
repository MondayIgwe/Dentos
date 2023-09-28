using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.ContactType;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ContactType;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class BillingContanctTypeStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public BillingContanctTypeStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the Contact Type process")]
        public void GivenINavigateToTheContactTypeProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ContactType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the Contact Type process should exist along with the fields")]
        public void ThenTheContactTypeProcessShouldExistAlongWithTheFields()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(ContactTypeLocators.contactTypeHeader)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ContactTypeLocators.contactTypeCheckBox("IsSupplier_ccc"))).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ContactTypeLocators.contactTypeCheckBox("IsSupplierDispute_ccc"))).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ContactTypeLocators.contactTypeCheckBox("IsBillStatement_ccc"))).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"I add a new Contact Type record with all the details")]
        public void ThenIAddANewContactTypeRecordWithAllTheDetails(Table table)
        {
            var contactTypeEntity = table.CreateInstance<ContactTypeEntity>();
            _featureContext[StepConstants.ContactType] = contactTypeEntity;

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ContactTypeLocators.code, contactTypeEntity.code));
            _actor.AttemptsTo(SendKeys.To(ContactTypeLocators.description, contactTypeEntity.description));
            foreach (string checkbox in contactTypeEntity.Checkboxes)
            {
                _actor.AttemptsTo(Checkbox.SetStatus(ContactTypeLocators.contactTypeCheckBox(checkbox), true));
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I reopen the Contact Type record the information is stored correctly")]
        public void WhenIReopenTheContactTypeRecordTheInformationIsStoredCorrectly()
        {
            var contactTypeEntity = (ContactTypeEntity)_featureContext[StepConstants.ContactType];

            _actor.AttemptsTo(SearchProcess.ByName(Process.ContactType));
            _actor.AttemptsTo(QuickFind.Search(contactTypeEntity.code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var actualtGlobalValues = _actor.AsksFor(GetContactTypeValues.Data());
            actualtGlobalValues.code.Should().BeEquivalentTo(contactTypeEntity.code);
            actualtGlobalValues.description.Should().BeEquivalentTo(contactTypeEntity.description);
          
            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
