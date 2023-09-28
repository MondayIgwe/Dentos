using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity.GenearlJournal;
using Elite3E.Infrastructure.Enums;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GenearlJournal.GJCategoriesSetup;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.ProcessInteraction.GeneralJournal;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.WorkFlowDashBoard;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WorkFlowDashbord;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GenearlJournal;
using Elite3E.PageObjects.PageLocators;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class GeneralJournalSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public GeneralJournalSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I add the general journal details:")]
        public void WhenIAddTheGeneralJournalDetails(Table table)
        {
            var generalJournal = table.CreateInstance<GeneralJournalEntity>();
            _featureContext[StepConstants.JournalName] = generalJournal.Journal;

            _actor.AttemptsTo(GeneralJournal.With(generalJournal));

        }

        [When(@"I add general journal detail child form")]
        public void WhenIAddGeneralJournalDetailChildForm(Table table)
        {
            var details = table.CreateInstance<GeneralJournalDetailEntity>();

            _actor.AttemptsTo(GeneralJournalDetail.AddChildForm(details));
        }

        [StepDefinition(@"I search for '(.*)'")]
        public void WhenISearchFor(string dashBoard)
        {
            _actor.AttemptsTo(SearchProcess.ByName(dashBoard, false));
        }

        [When(@"I open the general journal approval and '(.*)'")]
        public void WhenIOpenTheGeneralJournalApprovalAndPerformAction(RibbonAction action)
        {
            var journalName = _featureContext[StepConstants.JournalName].ToString();

            _actor.AttemptsTo(WorkFlowAction.GeneralJournalAction(journalName, action));

        }


        [Then(@"verify the gl sequence number created")]
        public void ThenVerifyTheGlSequenceNumberCreated()
        {
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));

            Console.WriteLine("Generated Message: " + message);

            var sequenceNumber = message.Split(" ")[5];
            _featureContext[StepConstants.GJSequenceNumber] = sequenceNumber;

        }

        [Then(@"verify the status is '(.*)'")]
        public void ThenVerifyTheStatusIs(string gJStatus)
        {
            var entry = _featureContext[StepConstants.GJSequenceNumber].ToString();

            var status = _actor.AsksFor(GeneralJournalEntryStatus.Value(entry));

            status.Should().Contain(gJStatus);

        }

        [Then(@"I verify the category has '(.*)' checkbox")]
        public void IVerifyTheCategoryHasCheckbox(string checkboxLabel)
        {
            _actor.WaitsUntil(Appearance.Of(GJJournalCategoriesLocators.CheckBox(checkboxLabel)), IsEqualTo.True());
        }

        [Then(@"I '(.*)' the '(.*)' checkbox")]
        public void ISelectTheCheckbox(CheckBox action, string chekboxLabel)
        {
            _actor.AttemptsTo(GeneralJournalCategories.Select(chekboxLabel, action));
        }

        [Then(@"verify the gj request is shown at reject grid")]
        public void ThenVerifyTheGjRequestIsShownAtRejectGrid()
        {
            var journalName = _featureContext[StepConstants.JournalName].ToString();
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(journalName, GlobalConstants.GJRequest));

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.DoesElementExist(WorkFlowDashBoardLocators.RejectGridJournal(journalName)).Should().BeTrue();
        }

        [Then(@"verify the gj category field is present in simple entry form")]
        public void ThenVerifyTheGjCategoryFieldIsPresentInSimpleEntryForm(Table table)
        {
            _actor.DoesElementExist(CommonLocator.FieldLabel(table.Rows[0][ColumnNames.FieldName])).Should().Be(true);

        }

        [Given(@"I navigate to cash receipt target")]
        public void GivenINavigateToCashReceiptTarget()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.CashReceiptTarget));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify GL Book value is '([^']*)'")]
        public void ThenIVerifyGLBookValueIs(string glBook)
        {
            var actualGLBookValue = _actor.GetElementText(GeneralJournalLocators.GLBookValue);
            actualGLBookValue.Should().NotBeNullOrEmpty();
            actualGLBookValue.Should().BeEquivalentTo(glBook);
        }

        [Given(@"I add new cash receipt target")]
        public void GivenIAddNewCashReceiptTarget(Table table)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(GeneralJournalLocators.DescriptionInput,table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.Currency, table.Rows[0][ColumnNames.Currency]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }



    }
}
