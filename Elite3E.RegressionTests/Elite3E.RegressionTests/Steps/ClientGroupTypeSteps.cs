using FluentAssertions;
using Elite3E.PageObjects.PageLocators;
using Boa.Constrictor.Screenplay;
using TechTalk.SpecFlow;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientGroup;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ClientGroupTypeSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ClientGroupTypeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"The client group type process is open")]
        public void GivenTheClientGroupTypeProcessIsOpen()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientGroupType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I open the Client Group Type")]
        public void WhenIOpenTheClientGroupType()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientGroupType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"add the required fields")]
        public void WhenAddTheRequiredFields(Table table)
        {
            _actor.WaitsUntil(Existence.Of(ClientGroupLocators.Code), IsEqualTo.True());
            _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.Code, table.Rows[0][ColumnNames.Code]));
            
            _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.Description, table.Rows[0][ColumnNames.Description]));

            var description = table.Rows[0][ColumnNames.Description];
            _featureContext[StepConstants.Description] = description;
        }


        [When(@"Add a new record")]
        public void WhenAddANewRecord()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"validate the active box is checked")]
        public void WhenValidateTheActiveBoxIsChecked()
        {
            _actor.AsksFor(SelectedState.Of(ClientGroupLocators.GetActive)).Should().BeTrue();

        }

        [When(@"I search the client group type")]
        public void WhenISearchTheClientGroupType()
        {
            var description = _featureContext[StepConstants.Description].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(description));
        }

        [Then(@"I can submit the Client Group type")]
        public void ThenICanSubmitTheClientGroupType()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
        }

        [Then(@"I can view the client group and delete")]
        public void ThenICanViewTheClientGroupAndDelete()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.DeleteButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));

        }
    }
}
