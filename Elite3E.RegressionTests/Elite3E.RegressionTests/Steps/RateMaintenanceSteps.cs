using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.PageLocators;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using Elite3E.Infrastructure.Selenium;
using TechTalk.SpecFlow;
using Boa.Constrictor.Screenplay;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.RateMaintenance;
using FluentAssertions;
using Elite3E.Infrastructure.Entity;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Entity;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class RateMaintenanceSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public RateMaintenanceSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the rate maintenance process")]
        public void GivenINavigateToTheRateMaintenanceProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.RateMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add a new rate maintenance record")]
        public void GivenIAddANewRateMaintenanceRecord(Table table)
        {
            var rateEntity = table.CreateInstance<RateMaintenanceEntity>();
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(RateMaintenanceLocators.Code, rateEntity.Code));
            _actor.AttemptsTo(SendKeys.To(RateMaintenanceLocators.Description, rateEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.RateCode] = rateEntity.Code;

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Rate Type 1", rateEntity.RateType));
            _actor.AttemptsTo(SendKeys.To(RateMaintenanceLocators.Formula, rateEntity.Formula));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(RateMaintenanceLocators.RateType1Value,rateEntity.RateTypeValue + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I test the formula")]
        public void GivenITestTheFormula(Table table)
        {
            _actor.AttemptsTo(JScript.ClickOn(RateMaintenanceLocators.TestFormulaButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.ExceptionRate] = table.Rows[0][ColumnNames.TestResult].ToString();
            _actor.GetElementText(RateMaintenanceLocators.TestResultDiv).Should().BeEquivalentTo(table.Rows[0][ColumnNames.TestResult]);
        }

        [Given(@"I add new rate exception group")]
        public void GivenIAddNewRateExceptionGroup(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.RateExceptionGroup));
            _actor.AttemptsTo(Click.On(RateMaintenanceLocators.AddNewRateGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(RateMaintenanceLocators.Description, table.Rows[0][ColumnNames.Description]));

            _featureContext[StepConstants.Description] = table.Rows[0][ColumnNames.Description];
            _actor.AttemptsTo(Dropdown.SelectOptionByName(RateMaintenanceLocators.RateExceptionList, table.Rows[0][ColumnNames.RateExceptionList]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if(!string.IsNullOrEmpty(table.Rows[0]["StartDate"]))
                if(!string.IsNullOrEmpty(table.Rows[0]["StartDate"]))
               _actor.AttemptsTo(DateControl.SelectDate(CommonLocator.StartDate, table.Rows[0]["StartDate"]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify that the exception group is correct")]
        public void ThenIVerifyThatTheExceptionGroupIsCorrect()
        {
            var rateExceptionDescription = _featureContext[StepConstants.Description].ToString();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.RateExceptionGroup));
            _actor.GetElementText(RateMaintenanceLocators.RateExceptionGroupInput).Should().BeEquivalentTo(rateExceptionDescription);
        }

        [When(@"I verify that the exception rate is correct")]
        public void WhenIVerifyThatTheExceptionRateIsCorrect()
        {
            var rateException = _featureContext[StepConstants.ExceptionRate].ToString();
            _actor.GetElementText(RateMaintenanceLocators.RateDiv).Should().BeEquivalentTo(rateException);
        }

        [Given(@"I remove the exception rate group")]
        public void GivenIRemoveTheExceptionRateGroup()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.RateExceptionGroup));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.RateExceptionGroup, ChildProcessMenuAction.Delete));
        }


        [When(@"I add rate exception detail")]
        public void WhenIAddRateExceptionDetail(Table table)
        {
            var rateTypeCode = _featureContext[StepConstants.RateCode].ToString();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.RateExceptionDetail));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.RateExceptionDetail, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(SendKeys.To(RateMaintenanceLocators.Rate, rateTypeCode + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(RateMaintenanceLocators.Rate, rateTypeCode + Keys.Enter));
            if(!string.IsNullOrEmpty(table.Rows[0]["Office"]))
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.Office, table.Rows[0]["Office"]));
        }

        [StepDefinition(@"I submit the rate exception group")]
        public void WhenISubmitTheRateExceptionGroup()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(RateMaintenanceLocators.ClientMaintenanceHeader).Should().BeTrue();
        }
    }
}