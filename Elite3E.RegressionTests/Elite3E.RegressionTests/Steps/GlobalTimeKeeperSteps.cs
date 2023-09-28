using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System;
using Elite3E.PageObjects.Interaction.ProcessInteraction.GlobalTimeKeeper;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class GlobalTimeKeeperSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public GlobalTimeKeeperSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }


        [Given(@"I navigate to the Global Timekeeper process")]
        public void GivenINavigateToTheGlobalTimekeeperProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.GlobalTimekeeper));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the process should exist")]
        public void ThenTheProcessShouldExist()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(MatterLocator.GlobalTimekkeper)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I add a new record with all the details")]
        public void ThenIAddANewRecordWithAllTheDetails(Table table)
        {
            var timekeeperEntity = table.CreateInstance<GlobalTImekeeperEntity>();
            timekeeperEntity.TimekeeperNumber = new Random().Next().ToString();
            _featureContext[StepConstants.GlobalTimeKeeper] = timekeeperEntity;

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(MatterLocator.TimekeeperNumber).Should().BeTrue();

            _actor.AttemptsTo(SendKeys.To(MatterLocator.TimekeeperNumber, timekeeperEntity.TimekeeperNumber));
            _actor.AttemptsTo(SendKeys.To(MatterLocator.TimekeeperName, timekeeperEntity.TimekeeperName));
            _actor.AttemptsTo(SendKeys.To(MatterLocator.TimekeeperOffice, timekeeperEntity.TimekeeperOffice));
            _actor.AttemptsTo(SendKeys.To(MatterLocator.TimekeeperTitle, timekeeperEntity.TimekeeperTitle));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify that the timekeeper index exists")]
        public void ThenIVerifyThatTheTimekeeperIndexExists()
        {
            _actor.DoesElementExist(MatterLocator.TimeKeeperIndexDiv).Should().BeTrue() ;
        }


        [When(@"I reopen the record the information is stored correctly")]
        public void WhenIReopenTheRecordTheInformationIsStoredCorrectly()
        {
            var timeKeeperEntity = (GlobalTImekeeperEntity)_featureContext[StepConstants.GlobalTimeKeeper];

            _actor.AttemptsTo(SearchProcess.ByName(Process.GlobalTimekeeper));
            _actor.AttemptsTo(QuickFind.Search(timeKeeperEntity.TimekeeperNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var actualtGlobalValues = _actor.AsksFor(GetGlobalTimekeeperValues.Data());
            actualtGlobalValues.TimekeeperNumber.Should().BeEquivalentTo(timeKeeperEntity.TimekeeperNumber);
            actualtGlobalValues.TimekeeperName.Should().BeEquivalentTo(timeKeeperEntity.TimekeeperName);
            actualtGlobalValues.TimekeeperOffice.Should().BeEquivalentTo(timeKeeperEntity.TimekeeperOffice);
            actualtGlobalValues.TimekeeperTitle.Should().BeEquivalentTo(timeKeeperEntity.TimekeeperTitle);

            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        } 

    }
}
