using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Extensions;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Proforma;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class EntryAndModifyProcessesSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public EntryAndModifyProcessesSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I add a charge entry")]
        public void WhenIAddAChargeEntry(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var chargeEntries = table.CreateSet<ChargeEntryEntity>().ToList();
            
            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeEntry));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            var clickAdd = false;
            foreach (var chargeType in chargeEntries)
            {
                if (clickAdd)
                {
                    _actor.AttemptsTo(JScript.ClickOn(EntryAndModifyProcessLocators.AddEntry));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }

                _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.Matter, matterNumber));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Dropdown.SelectOptionByName(EntryAndModifyProcessLocators.ChargeTypeDropDown,chargeType.ChargeType));
               
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.ChargeEntryWorkAmountInput, chargeType.Amount));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                if (!string.IsNullOrEmpty(chargeType.TaxCode))
                    _actor.AttemptsTo(Lookup.SearchAndSelectSingle(GlobalConstants.TaxCode, chargeType.TaxCode));

                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                if (!string.IsNullOrEmpty(chargeType.WorkCurrency))
                {
                    _actor.AttemptsTo(Dropdown.SelectOptionByName(EntryAndModifyProcessLocators.WorkCurrency, chargeType.WorkCurrency));
                }

                clickAdd = true;
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.PostAll));
            _actor.WaitsUntil(Appearance.Of(CommonLocator.PostAll), IsEqualTo.False());
        }

        [Then(@"I verify the selected charge types are added to the matter")]
        public void ThenIVerifyTheSelectedChargeTypesAreAddedToTheMatter(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var chargeTypeGroupCodes = table.Rows.Select(r => r[ColumnNames.ChargeTypeCode]);

            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeModify));
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            
            foreach (var chargeTypeCode in chargeTypeGroupCodes)
            {
                _actor.WaitsUntil(Existence.Of(EntryAndModifyProcessLocators.ValidateEntry(chargeTypeCode)), IsEqualTo.True());
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Given(@"I try to modify the charge types")]
        [Given(@"I try to modify the disbursement types")]

        public void GivenITryToModifyAnEntry()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectAllTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"only the charge types set on the charge group is available")]
        public void ThenOnlyChargetypesSetOnChargeGroupIsAvailable(Table table)
        {
            var expectedChargeTypes = table.Rows.Select(r => r[ColumnNames.ChargeType]);

            //_actor.AttemptsTo(JScript.ClickOn(EntryAndModifyProcessLocators.ChargeTypeDropDownArrow));

            var atcaulChargeTyeps = _actor.AsksFor(GetAllDropdownValues.For(EntryAndModifyProcessLocators.ChargeTypeDropDownArrow, CommonLocator.DropDownOptions));


            expectedChargeTypes.ToList().Should().BeEquivalentTo(atcaulChargeTyeps);


            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [StepDefinition(@"I add a disbursement entry")]
        public void GivenIAddADisbursementEntry(Table table)
        {
            var matterNumber =  _featureContext[StepConstants.MatterNumberContext].ToString();

            var disbursementEntries = table.CreateSet<DisbursementEntryEntity>().ToList();

            foreach (var disbursement in disbursementEntries)
            {
                disbursement.MatterNumber = matterNumber;
            }

            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementEntry));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(JScript.ClickOn(EntryAndModifyProcessLocators.CarryOverCheckBox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(PostDisbursement.EntryWith(disbursementEntries));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.PostAllButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify all cost types are added to the matter")]
        public void ThenIVerifyAllCostTypesAreAddedToTheMatter(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var disbursementTypeCode = table.Rows.Select(r => r[ColumnNames.DisbursementTypeCode]);

            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementModify));

            _actor.AttemptsTo(QuickFind.Search(matterNumber));

            foreach (var entry in disbursementTypeCode)
            {
                _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateEntry(entry)), IsEqualTo.True());
            }
        }

        [Then(@"I verify the new disbursement types")]
        public void ThenIVerifyTheNewDisbursementTypes(Table table)
        {
            var group = table.Rows.Select(r => r[ColumnNames.Disbursement]);
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.DisbursementTypeSearchIcon));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput,Keys.Control + "A"));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, Keys.Control));
            //driver.FindElement(CommonLocator.SearchByInput.Query).Clear();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var entry in group)
            {
                _actor.WaitsUntil(Existence.Of(EntryAndModifyProcessLocators.ValidateDisbursementEntry(entry)), IsEqualTo.True());
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(Click.On(CommonLocator.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Given(@"I add a voucher disbursement")]
        public void GivenIAddAVoucherDisbursement(Table table)
        {
            var voucher = table.CreateInstance<EntryAndModifyProcessEntity>();
            voucher.InvoiceNumber = voucher.InvoiceNumber + "_" + StepHelper.GetRandomString(2, 2);
            _featureContext[StepConstants.InvoiceNumberContext] = voucher.InvoiceNumber;
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            

            _actor.AttemptsTo(SearchProcess.ByName(Process.VoucherMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(AddVoucher.With(voucher, matterNumber));
        }

        [Then(@"the new voucher is added to the matter")]
        public void ThenTheNewVoucherIsAddedToTheMatter()
        {
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.VoucherMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Existence.Of(EntryAndModifyProcessLocators.ValidateEntry(invoiceNumber)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Then(@"I select matter and choose '(.*)' via disbursement modify and save details")]
        public void ThenISelectMatterAndChooseViaDisbursementModifyAndSaveDetails(string billablePurge)
        {            
            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementModify));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var matterNo = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(QuickFind.Search(matterNo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.BillablePurgeField, billablePurge));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [StepDefinition(@"I generate the proforma")]
        public void ThenIGenerateTheProforma(Table table)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaGeneration));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var proformaGenerationEntity = table.CreateInstance<ProformaGenerationEntity>();


            _featureContext[StepConstants.ProformaRunContext] = proformaGenerationEntity;

            _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.Description, proformaGenerationEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.InvoiceDate, proformaGenerationEntity.InvoiceDate));

            var IncludeOtherProformas = _actor.AsksFor(SelectedState.Of(ProformaGenerationLocator.GetIncludeOtherProformas));

            if ((!proformaGenerationEntity.IncludeOtherProformas.ToBoolean() && IncludeOtherProformas) || (proformaGenerationEntity.IncludeOtherProformas.ToBoolean() && !IncludeOtherProformas))
            {
                _actor.AttemptsTo(Click.On(ProformaGenerationLocator.SetIncludeOtherProformas));
            }

            if (!string.IsNullOrEmpty(proformaGenerationEntity.ProformaStatus))
                _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.ProformaStatus, proformaGenerationEntity.ProformaStatus));

            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ProformaGeneration));

            _actor.AttemptsTo(ProformaGeneration.AddMatter(matterNumber));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Generate));
        }

        [Given(@"I select the matter on proforma edit")]
        public void AndISelectThematterOnProformaEdit()
        {            
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaEdit, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var matterNo = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(QuickFind.Search(matterNo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add anticipated cost on disbursement details and save the details")]
        public void WhenIAddAntichargetedCostOnDisbursementDetailsAndSaveTheDetails(Table table)
        {
            var billablePurge = table.CreateInstance<EntryAndModifyProcessEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.DisbursementDetails));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(BillablePurge.With(billablePurge));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"the Billable Purge field should be grayed out on disbursement modify")]
        public void ThenTheBillablePurgeFieldShouldBeGrayedOutOnDisbursementModify()
        {
            var matterNo = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementModify));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());            

            _actor.AttemptsTo(QuickFind.Search(matterNo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.FirstEntry(matterNo)));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.PurgeTypeLabel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [When(@"I try to post the charge entry")]
        public void WhenITryToPostTheChargeEntry(Table table)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeEntry));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var chargeType = table.CreateInstance<EntryAndModifyProcessEntity>();

            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.Matter, matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.WorkDate, chargeType.WorkDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.ChargeTypeInput, chargeType.ChargeType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.ChargeEntryWorkAmountInput, chargeType.WorkAmount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.PostAll));
        }

        [When(@"I try to update the charge modify")]
        public void WhenITryToUpdateTheChargeModify(Table table)
        {
            var chargeType = table.CreateInstance<EntryAndModifyProcessEntity>();

            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeModify));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.Matter, matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.WorkDate, chargeType.WorkDate));

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.ChargeTypeInput, chargeType.ChargeType));
           
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.ChargeModifyWorkAmountInput, chargeType.WorkAmount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(JScript.ClickOn(EntryAndModifyProcessLocators.ChargeModifyUpdateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if(!string.IsNullOrEmpty(chargeType.WorkCurrency))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(EntryAndModifyProcessLocators.WorkCurrency, chargeType.WorkCurrency));
            }
        }

        [Then(@"I verify the sections in charge modify")]
        public void ThenIVerifyTheSectionsInChargeModify()
        {
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkDate).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.Matter).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.ChargeTypeInput).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkAmount).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkCurrency).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WIPAmt).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.Language).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.Office).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I get the cost index of the disbursement card")]
        public void WhenIGetTheCostIndexOfTheDisbursementCard()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.CardIndex] = _actor.AsksFor(Text.Of(EntryAndModifyProcessLocators.CostIndex));
        }
        [When(@"I get the time index of the time card")]
        public void WhenIGetTheTimeIndexOfTheTimeCard()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.CardIndex] = _actor.AsksFor(Text.Of(EntryAndModifyProcessLocators.TimeIndex));
        }

        [When(@"I get the charge index of the charge card")]
        public void WhenIGetTheChargeIndexOfTheChargeCard()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.CardIndex] = _actor.AsksFor(Text.Of(EntryAndModifyProcessLocators.ChargeIndex));
        }

        [Then(@"I get the receipt index of the receipt")]
        public void ThenIGetTheReceiptIndexOfTheReceipt()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.CardIndex] = _actor.AsksFor(Text.Of(EntryAndModifyProcessLocators.ReceiptIndex));
        }



    }
}
