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
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxRate;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.RateTypeGlobalChange;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TimeRateRecalculate;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.Infrastructure.Constant;
using System.Data;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.MatterGroupEnquiry;
using System.Linq;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class RateTypeGlobalChangeStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public RateTypeGlobalChangeStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the rate type maintenance process")]
        public void GivenINavigateToTheRateTypeMaintenanceProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.RateTypeMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I add a new rate type maintenance record")]
        public void GivenIAddANewRateTypeMaintenanceRecord(Table table)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.RateTypes));

            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.Code, table.Rows[0][ColumnNames.Code]));
            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.Description, table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(TaxRatesLocator.CurrencyDefault, table.Rows[0][ColumnNames.DefaultCurrency]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var rateType = table.Rows[0][ColumnNames.Description];
            _featureContext[StepConstants.Description] = rateType;

        }

        [StepDefinition(@"I tick the standard rate checkbox")]
        public void GivenITickTheStandardRateCheckbox()
        {
            _actor.AttemptsTo(Click.On(RateMaintenanceLocators.StandardRateCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add rate type date detail")]
        public void WhenIAddRateTypeDateDetail(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.RateTypeDateDetail));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.RateTypeDateDetail, ChildProcessMenuAction.Add));
        }
        [When(@"I add clients using rate exception")]
        public void WhenIAddClientsUsingRateException()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientsRateException));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.ClientsRateException, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var clientNumber = _featureContext[StepConstants.ClientNumber].ToString();

            _actor.AttemptsTo(QuickFind.Search(clientNumber));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectAllTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [When(@"I enter ratetype global change details")]
        public void WhenIEnterRatetypeGlobalChangeDetails(Table table)
        {
            var ratetTypeGlobal = table.CreateInstance<RateTypeGlobalEntity>();
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Rate Type", ratetTypeGlobal.RateType));
            _actor.AttemptsTo(SendKeys.To(RateTypeGlobalChangeLocators.BasedOnDate, ratetTypeGlobal.BasedOnDate));
            _actor.AttemptsTo(SendKeys.To(RateTypeGlobalChangeLocators.EffecctiveDate, ratetTypeGlobal.EffectiveDate));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Reason Type", ratetTypeGlobal.Reasontype));
            _actor.AttemptsTo(SendKeys.To(RateTypeGlobalChangeLocators.DefaultRate, ratetTypeGlobal.DefaultRate));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(RateTypeGlobalChangeLocators.DefaultCurrency, ratetTypeGlobal.DefaultCurrency));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(RateTypeGlobalChangeLocators.RoundingMethod, ratetTypeGlobal.RoundingMethod));

        }
        [Then(@"I add rate detail currencies")]
        public void ThenIAddRateDetailCurrencies(Table table)
        {
            var ratetTypeGlobal = table.CreateInstance<RateTypeGlobalEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.RateDetailCurrencies));
          
            _actor.AttemptsTo(SendKeys.To(RateTypeGlobalChangeLocators.ChangeAmount, ratetTypeGlobal.ChangeAmount));
            _actor.AttemptsTo(Click.On(CommonLocator.Button(" Preview ")));

        }
        [Given(@"I fill in the time rate calculation required fields")]
        public void GivenIFillInTheTimeRateCalculationRequiredFields(Table table)
        {
            var timeRateRecalculateEntity = table.CreateInstance<TimeRateRecalculateEntity>();
            timeRateRecalculateEntity.UseRate = _featureContext[StepConstants.RateCode].ToString();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TimeRateRecalculateLocators.RequestedTimecardsIcon));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(MatterLocator.AdvancedLookupSearchCriteria(timeRateRecalculateEntity.MatterNumberIndex)));
            _actor.AttemptsTo(Click.On(MatterLocator.DropDownSelection(timeRateRecalculateEntity.SearchCriteria)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(MatterLocator.AdvancedLookupSearchText(timeRateRecalculateEntity.MatterNumberIndex), timeRateRecalculateEntity.SearchValue));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(" SUBMIT ")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(CommonLocator.StartDate, timeRateRecalculateEntity.StartDate)); _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            

            bool state = _actor.AsksFor(SelectedState.Of(TimeRateRecalculateLocators.WorkRateCheckbox));

            if (!state)            
                _actor.AttemptsTo(Click.On(TimeRateRecalculateLocators.WorkRateCheckbox));
                
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Use Rate", timeRateRecalculateEntity.UseRate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(" Calculate ")));

        }

        [Given(@"I preview by matter number")]
        public void GivenIPreviewByMatterNumber(Table table)
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TimeRateRecalculateLocators.PreviewDropDown));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(" Preview (TimeCards)")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            DataTable dataTable = _actor.FindTable(CommonLocator.GridLoc(matter));
            dataTable.Rows.Should().NotBeNull();

            //var Matrow = dataTable.Select("Matter= "+matter).FirstOrDefault();100620 Rate FeeEarner
            var Matrow = dataTable.AsEnumerable().Where(x => x["New Rate"].Equals(table.Rows[0]["New Rate"])).FirstOrDefault();
            Matrow["New Rate"].ToString().Should().BeEquivalentTo(table.Rows[0]["New Rate"]);

            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ParticleCLose));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Then(@"I view '([^']*)'")]
        public void ThenIView(string button)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(button)));
        }

        [Then(@"I validate rate is correct")]
        public void ThenIValidateRateIsCorrect(Table table)
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            DataTable dataTable = _actor.FindTable(CommonLocator.GridLoc(matter));
            dataTable.Rows.Should().NotBeNull();

            var Matrow = dataTable.AsEnumerable().Where(x => x["New Rate"].Equals(table.Rows[0]["New Rate"])).FirstOrDefault();
            Matrow["New Rate"].ToString().Should().BeEquivalentTo(table.Rows[0]["New Rate"]);
        }
    }
}
