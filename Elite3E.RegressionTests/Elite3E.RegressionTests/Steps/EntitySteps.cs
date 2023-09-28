using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Entity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.PageLocators;
using FluentAssertions;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Client;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class EntitySteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public EntitySteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I save the below person entity")]
        public void WhenISaveTheBelowPersonEntity(Table table)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.Entity));

            _actor.AttemptsTo(AddEntity.OfTypePerson(EntityMaintenancePopupLocator.EntityPersonRadioButton));

            _actor.AttemptsTo(AddPersonEntity.With(table.CreateInstance<PersonEntity>()));
        }

        [Then(@"the entity details are saved correctly")]
        public void ThenTheEntityDetailsAreSavedCorrectly()
        {

            var entityName = _featureContext[StepConstants.Entity].ToString();


            _actor.AttemptsTo(SearchProcess.ByName(Process.EntityPerson));
            _actor.AttemptsTo(QuickFind.Search(entityName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var expectedValue = _actor.AsksFor(Text.Of(PersonEntityLocator.EntityFirstNameValue));

            entityName.Should().BeEquivalentTo(expectedValue);

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I enter the entity details")]
        public void WhenIEnterTheEntityDetails(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.EntityType, table.Rows[0][ColumnNames.EntityType]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientLocators.FirstName, table.Rows[0][ColumnNames.FirstName])); 
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientLocators.LastName, table.Rows[0][ColumnNames.LastName])); 
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.FormatCode, table.Rows[0][ColumnNames.FormatCode]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView, StepConstants.Relationships));

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Relationship, table.Rows[0][ColumnNames.Relationship]));

            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Sites", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ScrollToElement.At(ClientLocators.SiteDesc));
            _actor.AttemptsTo(SendKeys.To(ClientLocators.SiteDesc, table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.SiteType, table.Rows[0][ColumnNames.SiteType]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Country, table.Rows[0][ColumnNames.Country]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(PersonEntityLocator.Language, table.Rows[0][ColumnNames.Language]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ClientLocators.Street));
            _actor.AttemptsTo(SendKeys.To(ClientLocators.Street, table.Rows[0][ColumnNames.Street]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var firstname = table.Rows[0][ColumnNames.FirstName];
            _featureContext[StepConstants.Entity] = firstname;
        }

        [Given(@"I add a person entity")]
        public void GivenIAddAPersonEntity(Table table)
        {
            var personEntity = table.CreateInstance<PersonEntity>();
            _featureContext[StepConstants.Entity] = personEntity.FirstName;

            _actor.AttemptsTo(SearchProcess.ByName(Process.EntityPerson));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(AddPersonEntity.With(personEntity));

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Relationships));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.Sites, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(PersonEntityLocator.SiteType, personEntity.SiteType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(PersonEntityLocator.Description, personEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(PersonEntityLocator.Street, personEntity.Street));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(PersonEntityLocator.Country, personEntity.Country));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(PersonEntityLocator.DefaultSite));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Given(@"I navigate to the Entity Person process")]
        public void GivenINavigateToTheEntityPersonProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.EntityPerson));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the Contact Email child form should exist along with the fields")]
        public void ThenTheContactEmailChildFormShouldExistAlongWithTheFields()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EntityPerson));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(PersonEntityLocator.ContactEmail)).Should().Be(true);
            _actor.AttemptsTo(Click.On(PersonEntityLocator.ContactEmail));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Contact Emails", ChildProcessMenuAction.Add));

            _actor.AsksFor(Field.IsAvailable(PersonEntityLocator.EmailAddress)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(PersonEntityLocator.EmailType)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(PersonEntityLocator.EmailDescription)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(PersonEntityLocator.StartDate)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(PersonEntityLocator.EndDate)).Should().Be(true);

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I open an existing entity person record")]
        public void WhenIOpenAnExistingEntityPersonRecord()
        {
            string entityPerson = _featureContext[StepConstants.Entity].ToString();
            _actor.AttemptsTo(QuickFind.Search(entityPerson));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add a new Contact Email child form record")]
        public void WhenIAddANewContactEmailChildFormRecord(Table table)
        {
            var personEntity = table.CreateInstance<PersonEntity>();
            personEntity.Email = table.Rows[0]["Email"] + "@yahoo.com";
            _featureContext[StepConstants.EntityPerson] = personEntity;
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EntityPerson));
            _actor.AttemptsTo(EnterContactEmailEntity.With(personEntity));
        }

        [Then(@"the Contact Email record should be saved")]
        public void ThenTheContactEmailRecordShouldBeSaved()
        {
            var entityPerson = _featureContext[StepConstants.Entity].ToString();
            var entityContactEmail = (PersonEntity)_featureContext[StepConstants.EntityPerson];

            _actor.AttemptsTo(SearchProcess.ByName(Process.EntityPerson));
            _actor.AttemptsTo(QuickFind.Search(entityPerson));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(PersonEntityLocator.ContactEmail));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            GetContactEmailValues(entityContactEmail);
        }

        public void GetContactEmailValues(PersonEntity entityContactEmail)
        {
            
            string childForm = "Contact Email";
            var actualContactEmailData = _actor.AsksFor(GetContactEmailEntityData.Data());

            if (!actualContactEmailData.Email.Equals(entityContactEmail.Email))
            {
                if (_actor.DoesElementExist(CommonLocator.ViewLastRecordOnGrid(childForm)))
                {
                    _actor.AttemptsTo(Click.On(CommonLocator.ViewLastRecordOnGrid(childForm)));
                    actualContactEmailData = _actor.AsksFor(GetContactEmailEntityData.Data());
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
            }

            actualContactEmailData.Email.Should().BeEquivalentTo(entityContactEmail.Email);
            actualContactEmailData.EmailDescription.Should().BeEquivalentTo(entityContactEmail.EmailDescription);
            actualContactEmailData.StartDate.Should().BeEquivalentTo(entityContactEmail.StartDate);
            actualContactEmailData.EndDate.Should().BeEquivalentTo(entityContactEmail.EndDate);

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(childForm, ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }
}