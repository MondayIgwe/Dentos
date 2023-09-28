using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxRate;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.Interaction.ProcessInteraction.RateMaintenance;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.Infrastructure.Constant;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class TaxRateSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public TaxRateSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I search rate type maintenace procees")]
        public void WhenISearchRateTypeMaintenaceProcees()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.RateTypeMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var rateType = _featureContext[StepConstants.Description].ToString();
            _actor.AttemptsTo(QuickFind.Search(rateType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
          
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.Applyfilter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(Fiscal_InvoicingLocators.ShowButton));
           
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TaxRatesLocator.SEARCH));
        }

        [When(@"I search for rate type")]
        public void WhenISearchForRateType()
        {
            var ratetype = _featureContext[StepConstants.Description].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.RateTypeMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            _actor.AttemptsTo(QuickFind.Search(ratetype));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search rate type maintenace")]
        public void WhenISearchRateTypeMaintenace()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.RateTypeMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var ratetype = _featureContext[StepConstants.Description].ToString();
            _actor.AttemptsTo(QuickFind.Search(ratetype));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add a new rate type record")]
        public void WhenIAddANewRateTypeRecord()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.RateTypeMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"enter the rate type information")]
        public void WhenEnterTheRateTypeInformation(Table table)
        {
            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.Code, table.Rows[0][ColumnNames.Code]));

            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.Description, table.Rows[0][ColumnNames.Description]));

            _actor.AttemptsTo(Dropdown.SelectOptionByName(TaxRatesLocator.CurrencyDefault, table.Rows[0][ColumnNames.DefaultCurrency]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!_actor.AsksFor(SelectedState.Of(TaxRatesLocator.GetRateChkBx)))
            {
                _actor.AttemptsTo(Click.On(TaxRatesLocator.SetRateChkBx));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!_actor.AsksFor(SelectedState.Of(TaxRatesLocator.GetTitleChkBx)))
            {
                _actor.AttemptsTo(Click.On(TaxRatesLocator.SetTitleChkBx));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!_actor.AsksFor(SelectedState.Of(TaxRatesLocator.GetOfficeChkBx)))
            {
                _actor.AttemptsTo(Click.On(TaxRatesLocator.SetOfficeChkBx));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            var ratetype = table.Rows[0][ColumnNames.Description];
            _featureContext[StepConstants.Description] = ratetype;
        }

        [When(@"additional  effective dates")]
        public void WhenAdditionalEffectiveDates(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StartDate, table.Rows[0][ColumnNames.StartDate], 2));
        }
        
        [StepDefinition(@"add the effective dates")]
        public void WhenAddTheEffectiveDates(Table table)
        {
            var effectiveDatedRaetes = table.CreateInstance<EffectiveDatedRaetesEntity>();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EffectiveDatedRates));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.EffectiveDatedRates, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            try
            {
                _actor.WaitsUntil(Appearance.Of(CommonLocator.OpenGridFlyOutMenu(StepConstants.EffectiveDatedRates,
                    GlobalConstants.Form)), IsEqualTo.True(), 1);
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);

                _actor.AttemptsTo(Click.On(CommonLocator.OpenGridFlyOutMenu(StepConstants.EffectiveDatedRates,
                    GlobalConstants.Grid)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.GridFlyOutButtonClick(GlobalConstants.Form)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StartDate, effectiveDatedRaetes.StartDate, 2));

            if(!string.IsNullOrEmpty(effectiveDatedRaetes.ReasonType))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Reason Type", effectiveDatedRaetes.ReasonType));
            }
            if (!string.IsNullOrEmpty(effectiveDatedRaetes.Description))
            {
                _actor.AttemptsTo(SendKeys.To(CommonLocator.Description,effectiveDatedRaetes.Description));
            }
            if (!string.IsNullOrEmpty(effectiveDatedRaetes.DefaultRate))
            {
                _actor.AttemptsTo(SendKeys.To(CommonLocator.DefaultRate, effectiveDatedRaetes.DefaultRate));
            }
        }

        [StepDefinition(@"add rate type details")]
        public void WhenAddRateTypeDetails(Table table)
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.RateTypeDateDetail, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.Amount, table.Rows[0][ColumnNames.Amount]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(TaxRatesLocator.EffDateCurrency, table.Rows[0][ColumnNames.Currency]));
            if (!string.IsNullOrEmpty(table.Rows[0][ColumnNames.Title]))
                _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.Title, table.Rows[0][ColumnNames.Title]));
            if (!string.IsNullOrEmpty(table.Rows[0][ColumnNames.Office]))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(TaxRatesLocator.Office, table.Rows[0][ColumnNames.Office]));
        }

        [When(@"I  add mulitple effective dates")]
        public void WhenIAddMulitpleEffectiveDates()
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Effective Dated Rates", ChildProcessMenuAction.Add));
        }
        
        [Given(@"I enter a start date")]
        public void GivenIEnterAStartDate(Table table)
        {
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.Date, table.Rows[0][ColumnNames.StartDate]));
        }

        [When(@"I enter a effective date")]
        public void WhenIEnterAEffectiveDate(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.NewRateSet));

            _actor.WaitsUntil(Existence.Of(CommonLocator.FindInputElementUsingText(LocatorConstants.EffStart)), IsEqualTo.True());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StartDate, table.Rows[0][ColumnNames.StartDate], 2));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());        
            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.UpdateButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I enter a future start date")]
        public void WhenIEnterAFutureStartDate(Table table)
        {
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.Date, table.Rows[0][ColumnNames.FutureStartDate]));
        }

        [When(@"I select the create new rates button")]
        public void WhenISelectTheCreateNewRatesButton()
        {
            _actor.AttemptsTo(Click.On(TaxRatesLocator.CreateNewRateSet));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can adjust the rates values")]
        public void ThenICanAdjustTheRatesValues()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TaxRatesLocator.RateDetailsAmt));
        }

        [StepDefinition(@"I select the create new rate button")]
        public void ThenISelectTheCreateNewRateButton()
        {
            _actor.WaitsUntil(Existence.Of(TaxRatesLocator.CreateButtonForm), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(TaxRatesLocator.CreateButtonForm));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"i  adjust the current effective rate record")]
        public void WhenIAdjustTheCurrentEffectiveRateRecord()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TaxRatesLocator.EffectiveRateTab));
        }

        [When(@"I enter new rate amount ""(.*)""")]
        public void WhenEnterNewRateAmount(string amount)
        {
            _actor.AttemptsTo(EnterRate.Value(amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        
        [Then(@"I can submit")]
        public void ThenICanSubmit()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete()); 
        }

        [When(@"I select effective dated rates")]
        public void WhenISelectEffectiveDatedRates()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EffectiveDatedRates));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can run rates report")]
        public void ThenICanRunRatesReport()
        {
            _actor.AttemptsTo(Click.On(TaxRatesLocator.RatesReport));
            
            _actor.WaitsUntil(Appearance.Of(TaxRatesLocator.Ratereptbl), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TaxRatesLocator.ExportReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [When(@"I select the new rate type and delete")]
        public void WhenISelectTheNewRateTypeAndDelete_()
        {
            _actor.AttemptsTo(Click.On(TaxRatesLocator.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add a new rate type")]
        public void WhenIAddANewRateType(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.RateTypeMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.Code, table.Rows[0][ColumnNames.Code]));
            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.Description, table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(TaxRatesLocator.CurrencyDefault, table.Rows[0][ColumnNames.DefaultCurrency]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!_actor.AsksFor(SelectedState.Of(TaxRatesLocator.GetRateChkBx)))
            {
                _actor.AttemptsTo(Click.On(TaxRatesLocator.SetRateChkBx));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!_actor.AsksFor(SelectedState.Of(TaxRatesLocator.GetTitleChkBx)))
            {
                _actor.AttemptsTo(Click.On(TaxRatesLocator.SetTitleChkBx));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!_actor.AsksFor(SelectedState.Of(TaxRatesLocator.GetOfficeChkBx)))
            {
                _actor.AttemptsTo(Click.On(TaxRatesLocator.SetOfficeChkBx));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            var rateType = table.Rows[0][ColumnNames.Description];
            _featureContext[StepConstants.Description] = rateType;
        }

        [Given(@"I search for a rate type")]
        public void GivenISearchForARateType()
        {
            var rateType = _featureContext[StepConstants.Description].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.RateTypeMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            _actor.AttemptsTo(QuickFind.Search(rateType));
        }

        [When(@"I create a new rate set")]
        public void WhenICreateANewRateSet()
        {
            _actor.AttemptsTo(Click.On(TaxRatesLocator.CreateNewRateSet));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I select future")]
        public void WhenISelectFuture()
        {
            _actor.AttemptsTo(FutureDate.Select());

            _actor.AttemptsTo(Click.On(TaxRatesLocator.CreateButtonForm));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add a new title and set rate")]
        public void WhenIAddANewTitleAndSetRate(Table table)
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.NewRateSetDetails, ChildProcessMenuAction.Add));

            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.NewRateAmount, table.Rows[0][ColumnNames.Amount]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.NewRateCurrency, table.Rows[0][ColumnNames.Currency]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.NewRateTitle, table.Rows[0][ColumnNames.Title]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TaxRatesLocator.NewRateOffice, table.Rows[0][ColumnNames.Office]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(TaxRatesLocator.CreateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}