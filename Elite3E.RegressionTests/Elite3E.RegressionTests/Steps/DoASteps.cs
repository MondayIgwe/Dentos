using TechTalk.SpecFlow;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators;
using FluentAssertions;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.DoA;
using Elite3E.PageObjects.Interaction.ProcessInteraction.DoARole;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class DoASteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public DoASteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the DoA Role Type process")]
        public void GivenINavigateToTheDoARoleTypeProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.DoARoleType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add a new DoA Role Type")]
        public void GivenIAddANewDoARoleType()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.DoARoleType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"all the relevant fields should exist")]
        public void ThenAllTheRelevantFieldsShouldExist()
        {
            _actor.AsksFor(Field.IsAvailable(DoALocators.Code)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(DoALocators.Description)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(DoALocators.DoARolesCard)).Should().Be(true);
        }

        [When(@"I provide all the required data")]
        public void WhenIProvideAllTheRequiredData(Table table)
        {
            var doaEntity = table.CreateInstance<DoAEntity>();
            _actor.AttemptsTo(SendKeys.To(DoALocators.Code, doaEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(DoALocators.Description, doaEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.DoARoleContext] = doaEntity.Description;
        }

        [When(@"I add the DoA Roles")]
        public void WhenIAddTheDoARoles(Table table)
        {
            var doaEntity = table.CreateInstance<DoAEntity>();
            _featureContext[StepConstants.DoARoleType] = doaEntity;
            _actor.AttemptsTo(DoARole.EnterData(doaEntity,"0"));
        }

        [When(@"I delete the DoA Role")]
        public void WhenIDeleteTheDoARole()
        {
            var doaCode = _featureContext[StepConstants.DoARoleContext].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.DoARoleType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(doaCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.DoARoles, ChildProcessMenuAction.Delete));
        }

        [Then(@"I verify that the record has been deleted")]
        public void ThenIVerifyThatTheRecordHasBeenDeleted()
        {
            var doaCode = _featureContext[StepConstants.DoARoleContext].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.DoARoleType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(doaCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(DoALocators.DoANoRoles)).Should().Be(true);
            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add the duplicate DoA Roles")]
        public void WhenIAddTheDuplicateDoARoles(Table table)
        {
            var doaEntity = table.CreateInstance<DoAEntity>();
            int i = 0;
            do
            {
                _actor.AttemptsTo(DoARole.EnterData(doaEntity, i.ToString()));
                i++;
            } while (i < 2);
        }

        [Then(@"I should get an error")]
        public void ThenIShouldGetAnError()
        {
            //TODO: Still awaiting for this functionality error on 3E
            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"all the data is saved correctly")]
        public void WhenAllTheDataIsSavedCorrectly()
        {
            var doaEntity = (DoAEntity)_featureContext[StepConstants.DoARoleType];
            var doaCode = _featureContext[StepConstants.DoARoleContext].ToString();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.DoARoleType));
            var actualDoARoles = _actor.AsksFor(GetDoARoleData.Data(doaCode));
            actualDoARoles.Unit.Should().BeEquivalentTo(doaEntity.Unit);
            actualDoARoles.Office.Should().BeEquivalentTo(doaEntity.Office);
            actualDoARoles.Department.Should().BeEquivalentTo(doaEntity.Department);
            actualDoARoles.Role.Should().BeEquivalentTo(doaEntity.Role);

            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I navigate to the DoA Report process")]
        public void GivenINavigateToTheDoAReportProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.DoAReport, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"all the report  fields should exist")]
        public void ThenAllTheReportFieldsShouldExist()
        {
            _actor.AsksFor(Field.IsAvailable(DoALocators.DoAReportPage)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(DoALocators.DoARoleTypeDropdown)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(DoALocators.WorkflowReport)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(DoALocators.UnitReportDropdown)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(DoALocators.OfficeReportDropdown)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(DoALocators.DepartmentReportDropdown)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(DoALocators.RoleReportDropdown)).Should().Be(true);
        }

        [When(@"I provide some information and run the Report")]
        public void WhenIProvideSomeInformationAndRunTheReport(Table table)
        {
            var doaRole = _featureContext[StepConstants.DoARoleContext].ToString();
            var doaEntity = table.CreateInstance<DoAEntity>();

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle(GlobalConstants.Approval, doaRole));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(DoALocators.UnitReportDropdown, doaEntity.Unit));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(DoALocators.OfficeReportDropdown, doaEntity.Office));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(DoALocators.DepartmentReportDropdown, doaEntity.Department));
            _actor.AttemptsTo(SendKeys.To(DoALocators.RoleReportDropdown, doaEntity.Role));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"all the respective columns are shown with correct values")]
        public void WhenAllTheRespectiveColumnsAreShownWithCorrectValues()
        {
            //TODO: Report not generated in 3E currently
            _actor.AttemptsTo(Click.On(CommonLocator.HomeDashboard));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }
}
