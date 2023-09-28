using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.PhaseTaskActivity;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ActivityList;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Audit;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.PhaseList;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaskList;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ModifyPhaseTaskStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ModifyPhaseTaskStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the phase list process")]
        public void GivenINavigateToThePhaseListProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PhaseList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I search or create a new phase list")]
        public void GivenISearchOrCreateANewPhaseList(Table table)
        {
            var phaseListEntity = table.CreateInstance<PhaseListEntity>();
            _actor.AttemptsTo(QuickFind.Search(phaseListEntity.PhaseListCode));
            _featureContext[StepConstants.PhaseListCode] = phaseListEntity.PhaseListCode;

            if (_actor.DoesElementExist(CommonLocator.NoSearchRecords))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, Process.PhaseList));
                _actor.AttemptsTo(SendKeys.To(PhaseListLocators.PhaseListCode, phaseListEntity.PhaseListCode));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(SendKeys.To(PhaseListLocators.Description, phaseListEntity.Description));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }

        [Given(@"I navigate to the firm phase list process")]
        public void GivenINavigateToTheFirmPhaseListProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FirmPhaseList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I search or create a new firm phase list")]
        public void GivenISearchOrCreateANewFirmPhaseList(Table table)
        {
            var phaseListEntity = table.CreateInstance<FirmListEntity>();
            var firmType = Process.FirmPhaseList;
              _featureContext[StepConstants.FirmPhaseListCode] = phaseListEntity.FirmListCode;

            _actor.AttemptsTo(EnterFirmListData.EnterFirmTypeListData(phaseListEntity, firmType));

        }

        [Then(@"I select a phase list to modify")]
        public void ThenISelectAPhaseListToModify()
        {
            var phaseListCode = _featureContext[StepConstants.PhaseListCode].ToString();
            _actor.AttemptsTo(QuickFind.Search(phaseListCode));
        }

        [Then(@"I update the '([^']*)' of the phase list")]
        public void ThenIUpdateTheOfThePhaseList(string description)
        {
            _featureContext[StepConstants.Description] = description;
            _actor.AttemptsTo(SendKeys.To(PhaseListLocators.Description, description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I add a phase list")]
        public void ThenIAddAPhaseList(Table table)
        {
            foreach (var rows in table.Rows)
            {
                _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.Phase, ChildProcessMenuAction.Add));
                _actor.AttemptsTo(SendKeys.To(PhaseListLocators.PhaseCode, rows[ColumnNames.Code] + Keys.Tab));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(SendKeys.To(PhaseListLocators.PhaseDescription, rows[ColumnNames.Phase] + Keys.Enter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }

        [When(@"I update the firm phase as per the requirement")]
        public void WhenIUpdateTheFirmPhaseAsPerTheRequirement()
        {
            var firmPhase = _featureContext[StepConstants.FirmPhaseListCode].ToString();

            for (int i = 2; i < 5; i++)
            {
                _actor.AttemptsTo(Click.On(PhaseListLocators.FirmPhaseDiv(i)));
                _actor.AttemptsTo(SendKeys.To(PhaseListLocators.FirmPhaseInput, firmPhase + Keys.Tab));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"I update the first phase with a different firm code")]
        public void WhenIUpdateTheFirstPhaseWithADifferentFirmCode(Table table)
        {
            var phaseListEntity = table.CreateInstance<PhaseListEntity>();

            _actor.AttemptsTo(Click.On(PhaseListLocators.FirmPhaseDiv(2)));
            _actor.AttemptsTo(SendKeys.To(PhaseListLocators.FirmPhaseInput, phaseListEntity.FirmCode + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.FirmCode] = phaseListEntity.FirmCode;
        }

        [When(@"I verify that the firm code is updated")]
        public void WhenIVerifyThatTheFirmCodeIsUpdated()
        {
            var firmCode = _featureContext[ColumnNames.FirmCode].ToString();
            _actor.GetElementText(PhaseListLocators.FirmPhaseCode(firmCode)).Should().BeEquivalentTo(firmCode);
        }

        [Then(@"I validate that the initial modification is saved")]
        public void ThenIValidateThatTheInitialModificationIsSaved()
        {
            var savedDescription = _featureContext[StepConstants.Description].ToString();
            _actor.GetElementText(PhaseListLocators.Description).Should().BeEquivalentTo(savedDescription);
        }

        [Then(@"I verify that the phases contain the same firm codes")]
        public void ThenIVerifyThatThePhasesContainTheSameFirmCodes()
        {
            var firmPhaseListCode = _featureContext[StepConstants.FirmPhaseListCode].ToString();
            List<string> firmCodes = _actor.GetElementTextList(PhaseListLocators.FirmPhaseCode(firmPhaseListCode));
            firmCodes.Count().Should().Be(3);
            firmCodes.All(x => x.Equals(firmPhaseListCode));

        }

        [Given(@"I navigate to the task list process")]
        public void GivenINavigateToTheTaskListProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.TaskList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I navigate to the firm task list process")]
        public void GivenINavigateToTheFirmTaskListProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FirmTaskList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I create a new task list")]
        public void GivenICreateANewTaskList(Table table)
        {
            var taskListEntity = table.CreateInstance<TaskListEntity>();

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, Process.TaskList));
            _actor.AttemptsTo(SendKeys.To(PhaseListLocators.PhaseListCode, taskListEntity.TaskListCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _featureContext[StepConstants.TaskListCode] = taskListEntity.TaskListCode;

            _actor.AttemptsTo(SendKeys.To(PhaseListLocators.Description, taskListEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I add a task list")]
        public void ThenIAddATaskList(Table table)
        {
            foreach (var rows in table.Rows)
            {
                _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.Task, ChildProcessMenuAction.Add));
                _actor.AttemptsTo(SendKeys.To(TaskListLocators.FirmTaskListCode, rows[ColumnNames.Code] + Keys.Tab));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(SendKeys.To(TaskListLocators.FirmTaskListDescription, rows[ColumnNames.Task] + Keys.Tab));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(SendKeys.To(TaskListLocators.FirmTask, rows[ColumnNames.FirmTask] + Keys.Enter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Given(@"I search or create a new firm task list")]
        public void GivenISearchOrCreateANewFirmTaskList(Table table)
        {
            var taskListEntity = table.CreateInstance<FirmListEntity>();
            var firmType = Process.FirmTaskList;
            _featureContext[StepConstants.FirmTaskListCode] = taskListEntity.FirmListCode;

            _actor.AttemptsTo(EnterFirmListData.EnterFirmTypeListData(taskListEntity, firmType));
        }

        [Then(@"I select a task list to modify")]
        public void ThenISelectATaskListToModify()
        {
            var taskListCode = _featureContext[StepConstants.TaskListCode].ToString();
            _actor.AttemptsTo(QuickFind.Search(taskListCode));
        }

        [Then(@"I verify that data is saved correctly")]
        public void ThenIVerifyThatDataIsSavedCorrectly()
        {
            var taskCode = _featureContext[StepConstants.TaskListCode].ToString();
            _actor.GetElementText(TaskListLocators.TaskListCodeDiv).Should().BeEquivalentTo(taskCode);

            var firmTaskListCode = _featureContext[StepConstants.FirmTaskListCode].ToString();
            List<string> firmCodes = _actor.GetElementTextList(PhaseListLocators.FirmPhaseCode(firmTaskListCode));
            firmCodes.Count().Should().Be(3);
            firmCodes.All(x => x.Equals(taskCode));
        }

        [Given(@"I navigate to the firm activity list process")]
        public void GivenINavigateToTheFirmActivityListProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FirmActivityList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I search or create a new firm activity list")]
        public void GivenISearchOrCreateANewFirmActivityList(Table table)
        {
            var activityListEntity = table.CreateInstance<FirmListEntity>();
            _featureContext[StepConstants.FirmActivityListCode] = activityListEntity.FirmListCode;
            var firmType = Process.FirmActivityList;

            _actor.AttemptsTo(EnterFirmListData.EnterFirmTypeListData(activityListEntity, firmType));
        }

        [Given(@"I navigate to the activity list process")]
        public void GivenINavigateToTheActivityListProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ActivityList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I create a new activity list")]
        public void GivenICreateANewActivityList(Table table)
        {
            var activityListEntity = table.CreateInstance<ActivityListEntity>();

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, Process.ActivityList));
            _actor.AttemptsTo(SendKeys.To(PhaseListLocators.PhaseListCode, activityListEntity.ActivityListCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _featureContext[StepConstants.ActivityListCode] = activityListEntity.ActivityListCode;

            _actor.AttemptsTo(SendKeys.To(PhaseListLocators.Description, activityListEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Then(@"I add an activity list")]
        public void ThenIAddAnActivityList(Table table)
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.Activities, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(SendKeys.To(ActivityListLocators.FirmActivityListCode, table.Rows[0][ColumnNames.Code] + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ActivityListLocators.FirmActivityListDescription, table.Rows[0][ColumnNames.Task] + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ActivityListLocators.FirmActivity, table.Rows[0][ColumnNames.FirmActivity] + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I select an activity list to modify")]
        public void ThenISelectAnActivityListToModify()
        {
            var activityListCode = _featureContext[StepConstants.ActivityListCode].ToString();
            _actor.AttemptsTo(QuickFind.Search(activityListCode));
        }

        [Then(@"I verify that activity list data is saved correctly")]
        public void ThenIVerifyThatActivityListDataIsSavedCorrectly()
        {
            var activityCode = _featureContext[StepConstants.ActivityListCode].ToString();
            _actor.GetElementText(ActivityListLocators.ActivityListCodeDiv).Should().BeEquivalentTo(activityCode);

            var firmActivityListCode = _featureContext[StepConstants.FirmActivityListCode].ToString();
            List<string> firmCodes = _actor.GetElementTextList(PhaseListLocators.FirmPhaseCode(firmActivityListCode));
            firmCodes.All(x => x.Equals(firmActivityListCode));
        }

        [Then(@"I verify the audit button in the task childform")]
        public void ThenIVerifyTheAuditButtonInTheTaskChildform()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, Process.TaskList));
            _actor.AttemptsTo(Click.On(AuditLocators.AuditChildformButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Close));
        }


    }

}

