using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Entity.FeeEarnerMaintenance;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Customer;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Time;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using UploadFile = Elite3E.PageObjects.Interaction.CommonInteraction.UploadFile;
using System.Threading.Tasks;
using Elite3E.RestServices.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.RegressionTests.DataCreators;
using OpenQA.Selenium;
using Elite3E.RegressionTests.DataCreators.DefaultData;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class TimeEntrySteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public TimeEntrySteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I view the time entry setup process")]
        public void WhenIViewTheTimeEntrySetupProcess()
        {
            //Use Time Modify Process, as there currently is an issue with the Time Entry Process
            _actor.AttemptsTo(SearchProcess.ByName(Process.TimeModify, true));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"add the time entry")]
        public async Task WhenAddTheTimeEntry(Table table)
        {
            var timeEntry = table.CreateInstance<TimeEntryEntity>();
            if (string.IsNullOrEmpty(timeEntry.Matter))
                timeEntry.Matter = _featureContext[StepConstants.MatterNumberContext].ToString();

                if (!string.IsNullOrEmpty(timeEntry.FeeEarner))
            {
                var feeEarnerDataEntity = new ApiFeeEarnerEntity();
                feeEarnerDataEntity.EntityName = timeEntry.FeeEarner;
                timeEntry.FeeEarner = await FeeEarnerData.GetFeeEarnerNumber(feeEarnerDataEntity);
                _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.FeeEarner, timeEntry.FeeEarner));
            }

            if (!string.IsNullOrEmpty(timeEntry.Matter))
                _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.Matter, timeEntry.Matter));

            if (!string.IsNullOrEmpty(timeEntry.TimeType))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(TimeEntryLocator.TimeType, timeEntry.TimeType));

            if (!string.IsNullOrEmpty(timeEntry.Hours))
                _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.Hours, timeEntry.Hours));

            if (!string.IsNullOrEmpty(timeEntry.Narrative))
            {
                var driver = _actor.Using<BrowseTheWeb>().WebDriver;
                driver.FindElement(TimeEntryLocator.Narrative.Query).SendKeys(timeEntry.Narrative);
            }

            if (!string.IsNullOrEmpty(timeEntry.Phase1))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Phase 1", timeEntry.Phase1));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(timeEntry.Task1))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Task 1", timeEntry.Task1));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(timeEntry.Activity1))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Activity 1", timeEntry.Activity1));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"I can post all the time entries")]
        public void ThenICanPostAllTheTimeEntries()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.PostAll));
        }

        [When(@"I view the view the fee earner maintenance")]
        public void WhenIViewTheViewTheFeeEarnerMaintenance(Table table)
        {
            var feeEarnerMaintenance = table.CreateInstance<FeeEarnerMaintenanceEntity>();

            if (string.IsNullOrEmpty(feeEarnerMaintenance.Entity))
            {
                feeEarnerMaintenance.Entity = _featureContext[StepConstants.Entity].ToString();
            }

            _featureContext[StepConstants.SearchTextContext] = feeEarnerMaintenance.Entity;

            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText(LocatorConstants.Entity), feeEarnerMaintenance.Entity));

            _actor.AttemptsTo(SendKeys.To(FeeEarnerMaintenanceLocator.LocalLanguageName, feeEarnerMaintenance.LocalLanguageName));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _featureContext[StepConstants.LocalLanguageNameContext] = feeEarnerMaintenance.LocalLanguageName;

        }

        [Then(@"I verify the section in time modify")]
        public void ThenIVerifyTheSectionInTimeModify()
        {
            _actor.DoesElementExist(EntryAndModifyProcessLocators.TimeKeeper).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.Matter).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkDate).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.TimeType).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkHours).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkRate).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkAmount).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkCurrency).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WIPHours).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WIPRate).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WIPAmt).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.DisbursementDetailsNarrative).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.Language).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.Office).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkType).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkHours).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"signature is available")]
        public void ThenSignatureIsAvailable()
        {
            _actor.WaitsUntil(Appearance.Of(FeeEarnerMaintenanceLocator.SignatureInput), IsEqualTo.True(), 0);
        }

        [Then(@"I can upload a signature '(.*)'")]
        public void ThenICanUploadASignature(string fileName)
        {
            _actor.AttemptsTo(UploadFile.Upload(fileName));

            _featureContext[StepConstants.SignatureFileName] = fileName;
        }

        [When(@"I add effective dated information")]
        public void WhenIAddEffectiveDatedInformation(Table table)
        {
            var effectiveDatedInformation = table.CreateInstance<EffectiveDatedInformationEntity>();
            _featureContext[StepConstants.FeeEarnerEffectiveDatedInformation] = effectiveDatedInformation;
            _featureContext[StepConstants.OfficeConfig] = effectiveDatedInformation.Office;

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EffectiveDatedInformation));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.FindInputElementUsingText(LocatorConstants.Office), effectiveDatedInformation.Office));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.FindInputElementUsingTextAndIndex(LocatorConstants.Title, 2), effectiveDatedInformation.Title));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }


        [When(@"I add fee earner entry")]
        public async Task WhenIAddFeeEarnerEntry(Table table)
        {
            var feeEarnerRates = table.CreateInstance<FeeEarnerRatesEntity>();

            // to verfiy the rate type maintence exists 
            var rateType = DefaultRegionalValues.GetFeeEarnerRateTypeDefaultValues();
            await new RateTypeMaintenanceData().CreateRateTypeAsync(rateType);

            _actor.AttemptsTo(SearchAndAddToChildProcess.With(StepConstants.FeeEarnerRates, rateType.RateTypeDescription));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(FeeEarnerMaintenanceLocator.EffectiveDatedRates, LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(FeeEarnerMaintenanceLocator.DefaultRate, feeEarnerRates.DefaultRates));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(feeEarnerRates.DefaultCurrency))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(FeeEarnerMaintenanceLocator.CurrencyDropdown, feeEarnerRates.DefaultCurrency));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
           

            _featureContext[StepConstants.DefaultRate] = feeEarnerRates.DefaultRates;
        }

        [When(@"I add Rate Details")]
        public void WhenIAddRateDetails()
        {
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(FeeEarnerMaintenanceLocator.RateDetail, LocatorConstants.AddButton)));
            _actor.AttemptsTo(JScript.ClickOn(FeeEarnerMaintenanceLocator.RateDiv));
            _actor.AttemptsTo(SendKeys.To(FeeEarnerMaintenanceLocator.Rate, "120"));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Given(@"I add partner points")]
        public void WhenIAddPartnerPoints(Table table)
        {
            var partnerPoints = table.CreateInstance<PartnerPointsEntity>();
            _featureContext[StepConstants.PartnerPointsContext] = partnerPoints;

            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(FeeEarnerMaintenanceLocator.PartnerPoints,
                LocatorConstants.AddButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(
                FeeEarnerMaintenanceLocator.PartnerPointsLabel(FeeEarnerMaintenanceLocator.PartnerPoints),
                partnerPoints.PartnerPoints));
            _actor.AttemptsTo(SendKeys.To(
                FeeEarnerMaintenanceLocator.PartnerPointsLabel(FeeEarnerMaintenanceLocator.PartnerPointsValue),
                partnerPoints.PartnerPointsValue));
            _actor.AttemptsTo(SendKeys.To(
                FeeEarnerMaintenanceLocator.PartnerPointsLabel(FeeEarnerMaintenanceLocator.BudgetPartnerPoints),
                partnerPoints.BudgetPartnerPoints));
            _actor.AttemptsTo(SendKeys.To(
                FeeEarnerMaintenanceLocator.PartnerPointsLabel(FeeEarnerMaintenanceLocator.BudgetPartnerPointsValue),
                partnerPoints.BudgetPartnerPointsValue));

            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText(LocatorConstants.FixedProfitShare),
                partnerPoints.FixedProfitShare));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText(LocatorConstants.ManagementValue),
                partnerPoints.ManagementValue));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText(LocatorConstants.EconomicValue),
                partnerPoints.EconomicValue));
            _actor.AttemptsTo(SendKeys.To(
                CommonLocator.FindInputElementUsingText(LocatorConstants.NetOperatingEfficiency),
                partnerPoints.NetOperatingEfficiency + Keys.Enter));

        }


        [When(@"I delete the fee earner maintenance")]
        public void WhenIDeleteTheFeeEarnerMaintenance()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ParentProcessDeleteButton));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }


        [Then(@"the partner points is saved")]
        public void ThenThePartnerPointsIsSaved()
        {
            var expectedPartnerPoints = (PartnerPointsEntity)_featureContext[StepConstants.PartnerPointsContext];

            _actor.AsksFor(ValueAttribute.Of(FeeEarnerMaintenanceLocator.PartnerPointsLabel(FeeEarnerMaintenanceLocator.PartnerPointsValue))).Should().BeEquivalentTo(expectedPartnerPoints.PartnerPointsValue);
            _actor.AsksFor(ValueAttribute.Of(FeeEarnerMaintenanceLocator.PartnerPointsLabel(FeeEarnerMaintenanceLocator.BudgetPartnerPoints))).Should().BeEquivalentTo(expectedPartnerPoints.BudgetPartnerPoints);
            _actor.AsksFor(ValueAttribute.Of(FeeEarnerMaintenanceLocator.PartnerPointsLabel(FeeEarnerMaintenanceLocator.PartnerPoints))).Should().BeEquivalentTo(expectedPartnerPoints.PartnerPoints);

            _actor.AsksFor(ValueAttribute.Of(CommonLocator.FindInputElementUsingText(LocatorConstants.FixedProfitShare))).Should().BeEquivalentTo(expectedPartnerPoints.FixedProfitShare);

            _actor.AsksFor(ValueAttribute.Of(CommonLocator.FindInputElementUsingText(LocatorConstants.ManagementValue))).Should().BeEquivalentTo(expectedPartnerPoints.ManagementValue);
            _actor.AsksFor(ValueAttribute.Of(CommonLocator.FindInputElementUsingText(LocatorConstants.EconomicValue))).Should().BeEquivalentTo(expectedPartnerPoints.EconomicValue);
            _actor.AsksFor(ValueAttribute.Of(CommonLocator.FindInputElementUsingText(LocatorConstants.NetOperatingEfficiency))).Should().BeEquivalentTo(expectedPartnerPoints.NetOperatingEfficiency);

        }

        [Then(@"local language is saved")]
        public void ThenLocalLanguageIsSaved()
        {
            var expectedLocalLanguageName = _featureContext[StepConstants.LocalLanguageNameContext].ToString();
            var searchText = _featureContext[StepConstants.SearchTextContext].ToString();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(searchText));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(ValueAttribute.Of(FeeEarnerMaintenanceLocator.LocalLanguageName)).Should().BeEquivalentTo(expectedLocalLanguageName);
        }

        [Then(@"signature is saved")]
        public void ThenSignatureIsSaved()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var expectedSignatureFileName = _featureContext[StepConstants.SignatureFileName].ToString();
            _actor.AsksFor(Text.Of(FeeEarnerMaintenanceLocator.GetSignatureText)).Trim().Should()
                .BeEquivalentTo(expectedSignatureFileName);
        }

        [When(@"I search for the saved time type")]
        public void WhenISearchForTheSavedTimeType()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.TimeType));

            var timeType = (TimeTypeEntity)_featureContext[StepConstants.TimeTypeContext];

            var searchText = timeType.Description;

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(searchText));
        }

        [When(@"enter a time entry for split billing")]
        public void WhenEnterATimeEntryForSplitBilling(Table table)
        {
            var timeTypeEntity = table.CreateInstance<TimeTypeEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.TimeEntry));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.Matter, matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.FeeEarner, feeEarner));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(TimeEntryLocator.TimeTypeDropDown, timeTypeEntity.TimeType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.Hours, timeTypeEntity.Hours1));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TimeEntryLocator.InternalComments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.GetDriver().FindElement(TimeEntryLocator.Narrative.Query).SendKeys(timeTypeEntity.Narrative);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Tax Code", timeTypeEntity.TaxCode));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(MainProcessMenu.ClickOn(MainProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.Hours, timeTypeEntity.Hours2));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TimeEntryLocator.InternalComments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.GetDriver().FindElement(TimeEntryLocator.Narrative.Query).SendKeys(timeTypeEntity.Narrative);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Tax Code", timeTypeEntity.TaxCode));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.PostAll));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
