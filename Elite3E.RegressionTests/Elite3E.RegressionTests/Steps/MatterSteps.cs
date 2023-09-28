using System.Collections.Generic;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.DirectCheque;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using Elite3E.RestServices.Entity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Matter;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.Infrastructure.Selenium;
using OpenQA.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter_Notes;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Client;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class MatterSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        private ClientMaintenanceData _clientMaintenanceData = new();
        private CostTypeData _costTypeData = new();
        private ChargeTypeData _chargeTypeData = new();
        private MatterNoteEntity notesEntity;
        public MatterSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I create a new matter")]
        public void GivenICreateANewMatter()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I update the matter")]
        public async Task UpdateTheMatter(Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();

            if (_featureContext.ContainsKey(StepConstants.FeeEarnerAssignedNumber))
            {
                var feeEarner = _featureContext[StepConstants.FeeEarnerAssignedNumber].ToString();
                _actor.AttemptsTo(SendKeys.To(MatterLocator.FeeEarner, feeEarner));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else if (!string.IsNullOrEmpty(matterEntity.OpeningFeeEarner))
            {
                var feeEarner = matterEntity.OpeningFeeEarner;
                _actor.AttemptsTo(SendKeys.To(MatterLocator.FeeEarner, feeEarner));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(matterEntity.Client))
            {
                var clientEntity = new ApiClientMaintenanceEntity();
                clientEntity.EntityName = matterEntity.Client;

                // check client Data
                await _clientMaintenanceData.ClientData(clientEntity);

                _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.Client, matterEntity.Client));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            }
            else
            {
                var clientEntity = _featureContext[StepConstants.ClientNumber].ToString();
                _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.Client, clientEntity));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(matterEntity.MatterType))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.MatterType, matterEntity.MatterType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(matterEntity.MatterAttribute))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.MatterAttribute, matterEntity.MatterAttribute));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(matterEntity.MatterCategory))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle(GlobalConstants.MatterCategory, matterEntity.MatterCategory));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(matterEntity.Language))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.Language, matterEntity.Language));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(matterEntity.Status))
            {
                _actor.AttemptsTo(Click.On(MatterLocator.Status));
                _actor.AttemptsTo(Click.On(ReceiptLocator.DropDownSelection(matterEntity.Status)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.PressKeyWithActions("tab");
            }

            if (!string.IsNullOrEmpty(matterEntity.CloseDate))
            {
                _actor.AttemptsTo(DateControl.SelectDate("Close Date", matterEntity.CloseDate));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(matterEntity.CloseType))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.CloseType, matterEntity.CloseType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(matterEntity.OpenDate))
            {
                _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.OpenDate, matterEntity.OpenDate));
            }

            if (!string.IsNullOrEmpty(matterEntity.MatterName))
            {
                _actor.AttemptsTo(SendKeys.To(MatterLocator.MatterName, matterEntity.MatterName));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(matterEntity.MatterCurrencyMethod))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.MatterCurrencyMethod, matterEntity.MatterCurrencyMethod));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"I verify the matter status")]
        public void ThenIVerifyTheMatterStatus(Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();

            _actor.GetElementText(MatterLocator.MatStatus).Should().BeEquivalentTo(matterEntity.Status);

            matterEntity.CloseDate = (string.IsNullOrEmpty(matterEntity.CloseDate)) ? string.Empty : matterEntity.CloseDate;
            _actor.GetElementText(MatterLocator.CloseDate).Should().BeEquivalentTo(matterEntity.CloseDate);

            matterEntity.CloseType = (string.IsNullOrEmpty(matterEntity.CloseType)) ? string.Empty : matterEntity.CloseType;
            _actor.GetElementText(MatterLocator.CloseType).Should().BeEquivalentTo(matterEntity.CloseType);
        }


        [When(@"I update the effective dated information")]
        public void WhenIUpdateTheEffectiveDatedInfomartion(Table table)
        {

            var matterEntity = table.CreateInstance<MatterEntity>();


            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EffectiveDatedInformation));

            _actor.AttemptsTo(Click.On(ChildFormNavigationLocators.NavigateToEffectiveDatedInformation));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(MatterLocator.Office));
            _actor.AttemptsTo(Click.On(ReceiptLocator.DropDownSelection(matterEntity.Office)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I update the matter rates")]
        public void WhenIUpdateTheMatterRates(Table table)
        {

            var matterEntity = table.CreateInstance<MatterEntity>();

            // to validate Rate

            _actor.AttemptsTo((Click.On(ChildFormNavigationLocators.NavigateToMatterRates)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(MatterLocator.Rate, matterEntity.Rate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add a new cost type group")]
        public async Task WhenIAddANewCostTypeGroup(Table table)
        {

            var matterEntity = table.CreateInstance<MatterEntity>();

            await _costTypeData.SearchaAndCreateCostTypeGroupDataAsync(matterEntity.CostTypeGroup);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(StepConstants.CostTypeGroup, LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, matterEntity.CostTypeGroup));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Record(matterEntity.CostTypeGroup)));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add a new charge type group")]
        public async Task WhenIAddANewChargeTypeGroup(Table table)
        {

            var matterEntity = table.CreateInstance<MatterEntity>();
            await _costTypeData.SearchaAndCreateCostTypeGroupDataAsync(matterEntity.ChargeTypeGroup);
            _actor.AttemptsTo(Click.On(CommonLocator.ChildFormAction(StepConstants.ChargeTypeGroup, LocatorConstants.AddButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.Record(matterEntity.ChargeTypeGroup)));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [StepDefinition(@"I add new charge type group")]
        public void WhenIAddNewChargeTypeGroup(Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();

            var chargeTypeGroup = new List<string>
            {
                matterEntity.ChargeTypeGroup
            };
            _actor.AttemptsTo(AddChargeTypeGroupsOnMatter.With(chargeTypeGroup));
        }

        [Then(@"verify the matter number is generated")]
        public void VerifyTheMatterNumberIsGenerated()
        {
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var matterNumber = message.Split(" ")[4];

            _featureContext[StepConstants.MatterNumberContext] = matterNumber;
        }

        [StepDefinition(@"I view an existing matter")]
        public void ViewAnExistingMatter()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.WaitsUntil(Appearance.Of(MatterLocator.MatterName), IsEqualTo.True());

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I modify the Matter WIP")]
        public void GivenIModifyTheMatterWIP(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.MatterWIP, table.Rows[0][ColumnNames.MatterWip]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add flat fees for the matter")]
        public void WhenIAddFlatFeesForTheMatter(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Matter));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Flat Fees", ChildProcessMenuAction.Add));

            _actor.AttemptsTo(Click.On(ClientLocators.FlatFeeTimeTypeDropdownIcon));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientLocators.FlatFeeTimeTypeDropdownOption(table.Rows[0][ColumnNames.TimeType])));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"I edit the existing matter")]
        public void EditAnExistingMatter(Table table)
        {

            var matterEntity = table.CreateInstance<MatterEntity>();

            if (!string.IsNullOrEmpty(matterEntity.OpenDate))
            {
                _actor.AttemptsTo(SendKeys.To(MatterLocator.OpenDate, matterEntity.OpenDate));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(matterEntity.InvoiceDistributionMethod))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.InvoiceDistributionMethod, matterEntity.InvoiceDistributionMethod));
            }
            if (!string.IsNullOrEmpty(matterEntity.InvoiceOverride))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.InvoiceOverride, matterEntity.InvoiceOverride));
            }
            if (!string.IsNullOrEmpty(matterEntity.MatterAttribute))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.MatterAttribute, matterEntity.MatterAttribute));
            }
            if (!string.IsNullOrEmpty(matterEntity.PresentationCurrency))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.PresentationCurrency, matterEntity.PresentationCurrency));
                _featureContext[StepConstants.PresCurrency] = matterEntity.PresentationCurrency;
            }
            if (!string.IsNullOrEmpty(matterEntity.PresentationCurrency))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.PresentationCurrency, matterEntity.PresentationCurrency));
                _featureContext[StepConstants.PresCurrency] = matterEntity.PresentationCurrency;
                _featureContext[StepConstants.PresExchangeRate] = matterEntity.PresentationExchangeRate;
            }

            if (!string.IsNullOrEmpty(matterEntity.RemittanceAccount))
            {
                _actor.AttemptsTo(SendKeys.To(MatterLocator.RemittanceAccount, matterEntity.RemittanceAccount));
                _featureContext[StepConstants.RemittanceAccountContext] = matterEntity.RemittanceAccount;
            }
            if (!string.IsNullOrEmpty(matterEntity.Language))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.Language, matterEntity.Language));
            }
            if (!string.IsNullOrEmpty(matterEntity.UDFValue))
            {
                var UDFLabel = _featureContext[StepConstants.UDFLabel].ToString();
                var udfDescription = _featureContext[StepConstants.UDFDesc].ToString();
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle(GlobalConstants.UDFList, udfDescription));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.UDF));
                _actor.AsksFor(Text.Of(MatterLocator.UDFLabel)).Should().Contain(UDFLabel);
                _actor.AttemptsTo(SendKeys.To(MatterLocator.UDFDecimalValue, matterEntity.UDFValue));
            }
        }

        [When(@"I delete an existing matter")]
        public void DeleteAnExistingMatter()
        {
            _actor.AttemptsTo(Click.On(MatterLocator.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I navigate to the matter maintenance process")]
        public void GivenINavigateToTheMatterMaintenanceProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
        }

        [When(@"I update the advanced find search terms")]
        public void WhenIUpdateTheAdvancedFindSearchTerms(Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();

            _actor.AttemptsTo(Click.On(CommonLocator.SearchButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(MatterLocator.AdvancedFindTab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(MatterLocator.AdvancedSearchCriteria(matterEntity.MatterDisplayNameIndex)));
            _actor.AttemptsTo(Click.On(MatterLocator.DropDownSelection(matterEntity.SearchCriteria)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(MatterLocator.AdvancedSearchText(matterEntity.MatterDisplayNameIndex), matterEntity.MatterDisplayText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(MatterLocator.AdvancedSearchCriteria(matterEntity.ClientDisplayNameIndex)));
            _actor.AttemptsTo(Click.On(MatterLocator.DropDownSelection(matterEntity.SearchCriteria)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(MatterLocator.AdvancedSearchText(matterEntity.ClientDisplayNameIndex), matterEntity.ClientDisplayText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"a reduced number of matters is returned")]
        public void ThenAReducedNumberOfMattersIsReturned()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.SearchButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(MatterLocator.SearchResults), IsEqualTo.True());
        }

        [When(@"I add submatters")]
        public void WhenIAddSubmatters(Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            matterEntity.Submatter1 = (string.IsNullOrEmpty(matterEntity.Submatter1)) ? _featureContext[StepConstants.SubMatterNumberContextOne].ToString() : matterEntity.Submatter1;
            matterEntity.Submatter2 = (string.IsNullOrEmpty(matterEntity.Submatter2)) ? _featureContext[StepConstants.SubMatterNumberContextTwo].ToString() : matterEntity.Submatter2;

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(QuickFind.Search(matterNumber));

            _actor.AttemptsTo(Click.On(MatterLocator.MasterMatterCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(MatterLocator.AddSubmatters));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.MatterSplitEffectiveDates));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.SplitType, matterEntity.SplitType));
            _actor.AttemptsTo(SendKeys.To(MatterLocator.SplitDescription, matterEntity.SplitDescription));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.MatterSplitDetail, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.MatterSplitDetail, GlobalConstants.Form));

            _actor.AttemptsTo(SendKeys.To(MatterLocator.Submatter, matterEntity.Submatter1));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(MatterLocator.SplitPercentage, matterEntity.SplitPercentage));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.MatterSplitDetail, ChildProcessMenuAction.Add));

            _actor.AttemptsTo(SendKeys.To(MatterLocator.Submatter, matterEntity.Submatter2));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(MatterLocator.SplitPercentage, matterEntity.SplitPercentage));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

            _actor.WaitsUntil(Appearance.Of(MatterLocator.MatterMaintenanceHeading), IsEqualTo.True());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"the Additional Fields should exist")]
        public void ThenTheAdditionalFieldsShouldExist()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AsksFor(Field.IsAvailable(MatterLocator.AdditionalMatterNumberField)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.CostMarkUpField)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.GrossMarkUpField)).Should().Be(true);

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }




        [Then(@"the Additional Child Forms should exist")]
        public void ThenTheAdditionalChildFormsShouldExist()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Matter));
            _actor.AsksFor(Field.IsAvailable(MatterLocator.ClientRelationshipCreditForm)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.RelationshipEnhancementCredit)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.ProjectManagementCreditForm)).Should().Be(true);

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Then(@"I verify sections in matter maintenance")]
        public void GivenIVerifySectionsInMatterMaintenance()
        {
            _actor.DoesElementExist(MatterLocator.Client).Should().Be(true);
            _actor.DoesElementExist(MatterLocator.OpeningFE).Should().Be(true);
            _actor.DoesElementExist(MatterLocator.MatterName).Should().Be(true);
            _actor.DoesElementExist(MatterLocator.Language).Should().Be(true);
            _actor.DoesElementExist(MatterLocator.MatterStatus).Should().Be(true);
            _actor.DoesElementExist(MatterLocator.OpenDate).Should().Be(true);
            _actor.DoesElementExist(MatterLocator.MatterCurrencyMethod).Should().Be(true);
            _actor.DoesElementExist(MatterLocator.AdditionalMatterNumberField).Should().Be(true);
            _actor.DoesElementExist(MatterLocator.CostMarkUpField).Should().Be(true);
            _actor.DoesElementExist(MatterLocator.GrossMarkUpField).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Matter));
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MatterAdditionalInformation)).Should().Be(true);
            _actor.AsksFor(Count.Of(MatterLocator.MatterAdditionalInformation)).Should().Be(1);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.BillingGroup)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.TimeTypeGroup)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.CostTypeGroup)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.ChargeTypeGroup)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.ClientRelationshipCreditForm)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.EffectiveDatedInformation)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.ClientRelationshipCreditForm)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.RelationshipEnhancementCredit)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.ProjectManagementCreditForm)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MatterRatesChildForm)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.RateExceptionGroup)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.RateException)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MatterDisbursementTypeSummarisationOverrides)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.ProformaAdjustments)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.Sites)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.WestlawKeyNumbers)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MatterTemplateOption)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MaskOverrideValues)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MatterBudget)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MatterPhaseExceptions)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.FlatFees)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.IndustryGroupsMatter)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.PracticeTeamsMatter)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.Case)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MatterPayer)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MatterTaxArticle)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.MatterNotes)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.AlternativeBillingArrangements)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.UDF)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(MatterLocator.BillingContacts)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"the '([^']*)' form values should be saved")]
        public void ThenTheFormValuesShouldBeSaved(string childForm)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var matter = (MatterEntity)_featureContext[StepConstants.MatterDetail];

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var actualStartDate = _actor.AsksFor(GetMatterCreditData.Data(childForm));
            actualStartDate.StartDate.Should().BeEquivalentTo(matter.StartDate);
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(childForm, LocatorConstants.DeleteButton)));
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I update the '([^']*)' child forms with the details:")]
        public void WhenIUpdateTheChildFormsWithTheDetails(string childForm, Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();
            _featureContext[StepConstants.MatterDetail] = matterEntity;
            _actor.AttemptsTo(EnterMatterCreditDetails.With(matterEntity, childForm));

        }

        [StepDefinition(@"I reopen a saved Matter")]
        public void GivenIReopenASavedMatter()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }



        [When(@"I update the matter with the details:")]
        public void WhenIUpdateTheMatterWithTheDetails(Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();
            _featureContext[StepConstants.MatterDetail] = matterEntity;

            _actor.AttemptsTo(SendKeys.To(MatterLocator.AdditionalMatterNumberField, matterEntity.AdditionalMatterNumber));
            _actor.AttemptsTo(SendKeys.To(MatterLocator.CostMarkUpField, matterEntity.CostMarkUp));
            _actor.AttemptsTo(SendKeys.To(MatterLocator.GrossMarkUpField, matterEntity.GrossMarkUp));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I selct billing rules optout")]
        public void WhenISelctBillingRulesOptout()
        {
            _actor.AttemptsTo(Checkbox.SetStatus(MatterLocator.ClientBillingRulesOptOutCheckBox, true));
        }


        [Then(@"the values should be saved")]
        public void ThenTheValuesShouldBeSaved()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var matter = (MatterEntity)_featureContext[StepConstants.MatterDetail];

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var actualtGlobalValues = _actor.AsksFor(GetMatterAdditionalFields.Data());

            actualtGlobalValues.AdditionalMatterNumber.Should().BeEquivalentTo(matter.AdditionalMatterNumber);
            actualtGlobalValues.CostMarkUp.Should().BeEquivalentTo(matter.CostMarkUp);
            actualtGlobalValues.GrossMarkUp.Should().BeEquivalentTo(matter.GrossMarkUp);

            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I add a Matter Payer")]
        public void GivenIAddAMatterPayer(Table table)
        {
            string payer = _featureContext[StepConstants.PayerContext].ToString();
            string date = table.Rows[0][GlobalConstants.StartDate];
            //_actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.MatterPayor));
            _actor.AttemptsTo(EnterMatterPayer.With(payer, date));
        }

        [StepDefinition(@"I add a new Billing Contact info")]
        public void GivenIAddANewBillingContactInfo(Table table)
        {
            string payer = table.Rows[0][ColumnNames.PayerName].ToString();
            var matterEntity = table.CreateInstance<MatterEntity>();
            matterEntity.Email = table.Rows[0]["Email"] + "@mater.com";

            _featureContext[StepConstants.Email] = matterEntity.Email;
            matterEntity.Payer = payer;
            _featureContext[StepConstants.MatterDetail] = matterEntity;
            _featureContext[StepConstants.Email] = matterEntity.Email;

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Matter));
            _actor.AttemptsTo(EnterBillingContactInfo.With(matterEntity));
            _actor.DoesElementExist(MatterLocator.BillingContactEntityPerson(matterEntity.FirstName + " " + matterEntity.LastName));
        }

        [When(@"I add volume discount group to the matter")]
        public void WhenIAddVolumeDiscountGroupToTheMatter()
        {
            var email = _featureContext[StepConstants.Email].ToString();
            var volumeDiscountGroup = _featureContext[StepConstants.VolumeDiscountGroup].ToString();
            _actor.AttemptsTo(SendKeys.To(MatterLocator.EmailToBill, email));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.VolumeDiscountGroup, volumeDiscountGroup));
            //   _actor.AttemptsTo(Click.On(MatterLocator.IsLeadVolumeDiscountMatter));
        }

        [When(@"I add a Billing Contact info")]
        public void WhenIAddABillingContactInfo(Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Matter));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Billing Contacts", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(AddBillingContactInfo.With(matterEntity));

        }

        [Then(@"the details should be saved correctly on the matter")]
        public void ThenTheDetailsShouldBeSavedCorrectlyOnTheMatter()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var matterEntity = (MatterEntity)_featureContext[StepConstants.MatterDetail];
            var actualMatter = _actor.AsksFor(GetBillingContactInfo.Data());

            actualMatter.Payer.Should().BeEquivalentTo(matterEntity.Payer);
            actualMatter.ContactType.Should().BeEquivalentTo(matterEntity.ContactType);
            actualMatter.ContactName.Should().BeEquivalentTo(matterEntity.FirstName + " " + matterEntity.LastName);

            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Billing Contacts", ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Matter Payer", ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the client billing rules opt out checkbox is available")]
        public void ThenIVerifyTheClientBillingRulesOptOutCheckboxIsAvailable()
        {
            _actor.AsksFor(Field.IsAvailable(MatterLocator.ClientBillingRulesOptOutCheckBox)).Should().Be(true);
        }

        [StepDefinition(@"I add a note to a matter")]
        public void WhenIAddANoteToAMatter(Table table)
        {
            notesEntity = table.CreateInstance<MatterNoteEntity>();
            notesEntity.EntryUser = (string.IsNullOrEmpty(notesEntity.EntryUser)) ? _featureContext[StepConstants.LoggedInUser].ToString() : notesEntity.EntryUser;

            _featureContext[StepConstants.MatterNotesContext] = notesEntity;
            _featureContext[StepConstants.ClientMatterNotesContext] = notesEntity;
            _featureContext[StepConstants.MatterNote] = notesEntity.Note;
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.MatterNotes));
            _actor.AttemptsTo(EnterMatterNote.With(notesEntity));
        }

        [When(@"I add Client Reference child form")]
        public void WhenIAddClientReferenceChildForm(Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientReference));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.ClientReference, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(MatterLocator.ClientReferenceNumber, matterEntity.ClientReference));
            _actor.AttemptsTo(SendKeys.To(MatterLocator.ClientReferenceStartDate, matterEntity.StartDate.ToString()));
            _featureContext[StepConstants.ClientReference] = matterEntity.ClientReference;
        }

        [StepDefinition(@"I set the bill subject to client approval to true")]
        public void GivenISetTheBillSubjectToClientApprovalToTrue()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Matter));
            _actor.AttemptsTo(Checkbox.SetStatus(MatterLocator.BillSubjectToClientApprovalCheckbox, true));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I set the ebilling flag to true")]
        public void GivenISetTheEbillingFlagToTrue()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Matter));
            _actor.AttemptsTo(Checkbox.SetStatus(MatterLocator.EbillingCheckbox, true));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I set the matter attribute")]
        public void ThenISetTheMatterAttribute()
        {
            var matterAttributeCode = _featureContext[ColumnNames.Description].ToString();
            _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.MatterAttribute, matterAttributeCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the matter notes in Matter Notes process")]
        public void ThenIVerifyTheMatterNotesInMatterNotesProcess()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var matterNotes = _featureContext[StepConstants.MatterNotesContext].ToString();
            notesEntity = (MatterNoteEntity)_featureContext[StepConstants.MatterNotesContext];
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterNotes));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            try
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SelectFirstUnlockedRow.Select());
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            catch
            {
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.DoesElementExist(NotesLocator.MatterNoteType).Should().BeTrue();
            _actor.AsksFor(ValueAttribute.Of(NotesLocator.MatterNoteType)).Should().Contain(notesEntity.NoteType);
            _actor.AsksFor(ValueAttribute.Of(NotesLocator.Note)).Should().Contain(notesEntity.Note);
            _actor.AsksFor(ValueAttribute.Of(NotesLocator.NextActionOwner)).Should().Contain(notesEntity.ActionOwner);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [StepDefinition(@"I set '([^']*)' checkbox to '([^']*)'")]
        public void GivenISetCheckboxTo(string name, bool state)
        {
            _actor.AsksFor(Field.IsAvailable(MatterLocator.QuickBillCheckBox)).Should().Be(true);
            _actor.AttemptsTo(Checkbox.SetStatus(MatterLocator.QuickBillCheckBox, state));
        }

        [Given(@"I confirm '([^']*)' checkbox is set to '([^']*)'")]
        public void GivenIConfirmCheckboxIsSetTo(string p0, bool expectedState)
        {
            bool state = _actor.AsksFor(SelectedState.Of(MatterLocator.QuickBillCheckBox));
            state.Should().Be(expectedState);
        }

        [When(@"I update EmailBillToField as '([^']*)'")]
        public void WhenIUpdateEmailBillToFieldAs(string emailId)
        {
            _actor.AttemptsTo(SendKeys.To(MatterLocator.MatterBillEmailTo, emailId));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I capture the udf information")]
        public void WhenICaptureTheUdfInformation(Table table)
        {
            var matterEntity = table.CreateInstance<MatterEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(ChildProcessView.SwitchToView("UDF", GlobalConstants.Form));

            _actor.AttemptsTo(SendKeys.To(MatterLocator.UDFStringInput, matterEntity.UDFString + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(MatterLocator.UDFDateInput, matterEntity.UDFDate + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _featureContext[StepConstants.UDFDesc] = matterEntity.UDFString;
            _featureContext[StepConstants.UDFLabel] = matterEntity.UDFDate;
        }


    }
}
