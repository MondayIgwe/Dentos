using System;
using System.Linq;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Entity.ProformaEdit;
using Elite3E.Infrastructure.Extensions;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Matter;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Proforma;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Purge;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WorkFlowDashbord;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountDisbursement;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RegressionTests.Hooks;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using FluentAssertions;
using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Action = System.Action;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public sealed class PurgeSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public PurgeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];

        }

        [Given(@"I add the transaction type")]
        public void WhenIAddTheTransactionType(Table table)
        {


            _actor.AttemptsTo(SearchProcess.ByName(Process.TransactionType));

            var transactionType = table.CreateInstance<TransactionTypeEntity>();

            _featureContext[StepConstants.TransactionTypeContext] = transactionType;

            _actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(EnterTransactionType.With(transactionType));
        }

        [Given(@"add the transaction type standard postings")]
        public void WhenAddTheTransactionTypeStandardPostings(Table table)
        {
            var transactionTypeStandardPosting = table.CreateInstance<TransactionTypeStandardPostingEntity>();

            _featureContext[StepConstants.TransactionTypeStandardPostingContext] = transactionTypeStandardPosting;

            _actor.AttemptsTo(EnterTransactionTypeStandardPosting.With(transactionTypeStandardPosting));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }


        [When(@"I search for the saved transaction type")]
        public void WhenISearchForTheSearchTheTransactionType()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.TransactionType));

            var transactionType = (TransactionTypeEntity)_featureContext[StepConstants.TransactionTypeContext];

            var searchText = transactionType.Description;

            _actor.AttemptsTo(QuickFind.Search(searchText));

        }

        [When(@"I search for the saved disbursement type")]
        public void WhenISearchForTheSearchTheDisbursementType()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementType));

            var disbursementType = (DisbursementTypeEntity)_featureContext[StepConstants.DisbursementTypeContext];

            var searchText = disbursementType.Description;

            _actor.AttemptsTo(QuickFind.Search(searchText));

        }


        [Then(@"the transaction type is saved")]
        public void ThenTheTransactionTypeIsSaved()
        {
            var expectedTransactionType = (TransactionTypeEntity)_featureContext[StepConstants.TransactionTypeContext];

            var expectedTransactionTypeStandardPosting = (TransactionTypeStandardPostingEntity)_featureContext[StepConstants.TransactionTypeStandardPostingContext];

            var actualTransactionType = _actor.AsksFor(GetTransactionType.Data());
            var actualExpectedTransactionTypeStandardPosting = _actor.AsksFor(GetTransactionTypeStandardPosting.Data(StepConstants.StandardPostings));

            expectedTransactionType.TransactionType.Should().BeEquivalentTo(actualTransactionType.TransactionType);
            expectedTransactionType.Description.Should().BeEquivalentTo(actualTransactionType.Description);
            expectedTransactionType.CurrencyType.Should().BeEquivalentTo(actualTransactionType.CurrencyType);
            expectedTransactionType.Anticipated.Should().BeEquivalentTo(actualTransactionType.Anticipated);

            expectedTransactionTypeStandardPosting.GlType.Should().BeEquivalentTo(actualExpectedTransactionTypeStandardPosting.GlType);
            var expectedARValue = expectedTransactionTypeStandardPosting.Ar.Replace(" ","");
            expectedARValue.Should().BeEquivalentTo(actualExpectedTransactionTypeStandardPosting.Ar);
        }

        [When(@"I add the disbursement")]
        public void WhenIAddTheDisbursement(Table table)
        {


            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementType));

            var disbursementType = table.CreateInstance<DisbursementTypeEntity>();

            _featureContext[StepConstants.DisbursementTypeContext] = disbursementType;

            _actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(EnterDisbursementType.With(disbursementType));
        }

        [When(@"I add the transaction type to the disbursement")]
        public void WhenIAddTheTransactionTypeToTheDisbursement(Table table)
        {
            var transactionType = table.Rows[0][ColumnNames.TransactionType];

            var disbursementType = (DisbursementTypeEntity)_featureContext[StepConstants.DisbursementTypeContext];

            disbursementType.TransactionType = transactionType;

            _featureContext[StepConstants.DisbursementTypeContext] = disbursementType;

            _actor.AttemptsTo(SendKeys.To(DisbursementTypeLocator.TransactionType, transactionType));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

        }


        [Then(@"the disbursement type is saved")]
        public void ThenTheDisbursementTypeIsSaved()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementType));

            var expectedDisbursementType = (DisbursementTypeEntity)_featureContext[StepConstants.DisbursementTypeContext];

            _actor.AttemptsTo(QuickFind.Search(expectedDisbursementType.Description));

            var actualDisbursementType = _actor.AsksFor(GetDisbursementType.Data());

            expectedDisbursementType.Code.Should().BeEquivalentTo(actualDisbursementType.Code);
            expectedDisbursementType.Description.Should().BeEquivalentTo(actualDisbursementType.Description);
            expectedDisbursementType.HardDisbursement.Should().BeEquivalentTo(actualDisbursementType.HardDisbursement);
            expectedDisbursementType.TransactionType.Should().BeEquivalentTo(actualDisbursementType.TransactionType);
        }

        [When(@"I view the purge disbursement without permissions")]
        public void WhenIViewThePurgeDisbursementWithoutPermissions()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PurgeDetail, false));
        }

        [Then(@"exclude anticipated is selected and disabled")]
        public void ThenExcludeAnticipatedIsSelectedAndDisabled()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var ExcludeAnticipated = _actor.AsksFor(GetPurgeDetailExcludeAnticipated.Element());

            ExcludeAnticipated.Enabled.Should().BeFalse();
            ExcludeAnticipated.Selected.Should().BeTrue();

            new CommonHooks(_featureContext, null).CancelProcess();
        }

        [Then(@"exclude anticipated is selected and enabled")]
        public void ThenExcludeAnticipatedIsSelectedAndEnabled()
        {
            var ExcludeAnticipated = _actor.AsksFor(GetPurgeDetailExcludeAnticipated.Element());

            ExcludeAnticipated.Enabled.Should().BeTrue();
            ExcludeAnticipated.Selected.Should().BeTrue();
        }

        [Given(@"I cancel the proxy")]
        public void GivenICancelTheProxy()
        {
            new CommonHooks(_featureContext, null).CancelProxyImpersonation();
        }


        [When(@"I view the purge disbursement")]
        public void WhenIViewThePurgeDisbursement()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PurgeDetail, false));
        }

        [Given(@"I select the matters for purging")]
        public void GivenISelectTheMattersForPurging()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PurgeDetail, false));

            _actor.WaitsUntil(Appearance.Of(CommonLocator.FindInputElementUsingText("Start Date")), IsEqualTo.True(), 8);

            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(PurgeDetail.AddMatters(matterNumber));
        }

        [Given(@"add the purge detail")]
        public void GivenAddThePurgeDetail(Table table)
        {
            var purgeDetail = table.CreateInstance<PurgeDetailEntity>();
            _actor.AttemptsTo(EnterPurgeDetail.With(purgeDetail));
        }

        [When(@"I calculate the purge detail")]
        public void WhenICalculateThePurgeDetail()
        {
            _actor.AttemptsTo(Click.On(PurgeDetailLocator.CalculateButton));

            _actor.WaitsUntil(Appearance.Of(PurgeDetailLocator.CalculateButton), IsEqualTo.True(), 8);
        }

        [Given(@"I include anticipated")]
        public void GivenIIncludeAnticipated()
        {
            _actor.AttemptsTo(PurgeDetailAnticipated.Exclude(false));
        }


        [Then(@"the purged item count is correct")]
        public void ThenThePurgedItemCountIsCorrect(Table table)
        {
            var expectedMatterCount = table.Rows[0][ColumnNames.Matters];
            var expectedDisbursementCount = table.Rows[0][ColumnNames.Disbursement];

            var actualMatterCount = _actor.AsksFor(Text.Of(PurgeDetailLocator.PurgedMatter));
            var actualDisbursementCount = _actor.AsksFor(Text.Of(PurgeDetailLocator.PurgedDisbursement));
        }

        //To remove the step
        [When(@"I view the matter ""(.*)""")]
        [Given(@"I view the matter ""(.*)""")]
        public void GivenIViewTheMatter(string matterNo)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        //To remove the step
        [When(@"clone it")]
        [Given(@"clone it")]
        public void GivenCloneIt(Table table)
        {

            var matterEntity = table.CreateInstance<MatterEntity>();
            _actor.AttemptsTo(FlyOutButtonMenu.Click(StepConstants.Add, StepConstants.Clone));

            _actor.AttemptsTo(EnterMatter.With(matterEntity));

            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var matterNumber = message.Split(" ")[4];

            _featureContext[StepConstants.MatterNumberContext] = matterNumber;

            Console.WriteLine("Matter Number Generated : " + matterNumber);
        }

        [StepDefinition(@"I post the disbursement")]
        public async Task GivenIPostTheDisbursement(Table table)
        {
            var disbursementEntries = table.CreateSet<DisbursementEntryEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            // check or create disbursement type 

            // Get Disbursement Type
            foreach (var entry in disbursementEntries)
            {
                var disbursementType = new ApiDisbursementTypeEntity()
                {
                    Description = entry.DisbursementType
                };
                entry.DisbursementType = await new CostTypeData().SearchAndCreateHardDisbursmentTypeDataAsync(disbursementType);
                _featureContext[StepConstants.AmountNumberContext] = entry.WorkAmount;
                _actor.AttemptsTo(EnterDisbursementEntry.With(disbursementEntries.ToList(), matterNumber));
            }
        }
        [When(@"I try to post the unavailable disbursement type")]
        public async Task WhenITryToPostTheUnavailableDisbursementType(Table table)
        {
            var disbursementEntries = table.CreateSet<DisbursementEntryEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            // check or create disbursement type 

            // Get Disbursement Type
            foreach (var entry in disbursementEntries)
            {
                var disbursementType = new ApiDisbursementTypeEntity()
                {
                    Description = entry.DisbursementType
                };
                entry.DisbursementType = await new CostTypeData().SearchAndCreateHardDisbursmentTypeDataAsync(disbursementType);
                _featureContext[StepConstants.AmountNumberContext] = entry.WorkAmount;
                _actor.AttemptsTo(EnterDisbursementEntryForUnavailableDisbursementType.With(disbursementEntries.ToList(), matterNumber));
            }
        }



        [StepDefinition(@"I validate the disbursement is posted with no errors")]
        [StepDefinition(@"I validate the post all was successful")]
        public void WhenIValidateTheDisbursementIsPostedWithNoErrors()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.PostAll), IsEqualTo.False(), 1);
        }

        [When(@"I save the Remittance Account ""(.*)""")]
        public void WhenISaveTheRemittanceAccount(string remittanceAccountName)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            var matterNo = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(QuickFind.Search(matterNo));
            _featureContext[StepConstants.RemittanceAccountContext] = remittanceAccountName;
            _actor.AttemptsTo(SendKeys.To(MatterLocator.RemittanceAccount, remittanceAccountName));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"the remittance account is saved")]
        public void ThenTheRemittanceAccountIsSaved()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var matterNo = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(QuickFind.Search(matterNo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var actualRemittanceAccount = _actor.AsksFor(Text.Of(MatterLocator.RemittanceAccount));
            var expectedRemittanceAccount = _featureContext[StepConstants.RemittanceAccountContext].ToString();
        }

        [When(@"I submit the disbursement modify")]
        public void WhenISubmitTheDisbursementModify(Table table)
        {
            var disbursementModifiers = table.CreateSet<DisbursementModifyEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementModify));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(EnterDisbursementModify.With(disbursementModifiers.ToList(), matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"the disbursement is not available")]
        public void ThenTheDisbursementIsNotAvailable()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementModify));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var actualNoRecordsFound = _actor.AsksFor(Text.Of(CommonLocator.FindDivElementContainsText(StepConstants.NoRecordsFoundMessage)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actualNoRecordsFound.Should().BeEquivalentTo(StepConstants.NoRecordsFoundMessage);
        }

        [Then(@"purge type is disabled on disbursement modify")]
        public void ThenPurgeTypeIsDisabledOnDisbursementModify()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var purgeTypeState = _actor.AsksFor(Field.IsAvailable(DisbursementModifyLocator.PurgeTypeDisabled));
            purgeTypeState.Should().BeTrue();
        }

        [When(@"I add the disbursement modify")]
        public void WhenIAddTheDisbursementModify(Table table)
        {
            var disbursementModifiers = table.CreateSet<DisbursementModifyEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementModify));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(EnterDisbursementModify.With(disbursementModifiers.ToList(), matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [StepDefinition(@"I can generate the proforma")]
        public void ThenICanGenerateTheProforma(Table table)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaGeneration));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var proformaGenerationEntity = table.CreateInstance<ProformaGenerationEntity>();
            _featureContext[StepConstants.ProformaRunContext] = proformaGenerationEntity;
            _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.Description, proformaGenerationEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());


            // TEMP FIX
            //_actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.ChangeStatusTo, proformaGenerationEntity.ProformaStatus));

            var includeOtherProformas = _actor.AsksFor(SelectedState.Of(ProformaGenerationLocator.GetIncludeOtherProformas));
            if ((!proformaGenerationEntity.IncludeOtherProformas.ToBoolean() && includeOtherProformas) || (proformaGenerationEntity.IncludeOtherProformas.ToBoolean() && !includeOtherProformas))
            {
                _actor.AttemptsTo(Click.On(ProformaGenerationLocator.SetIncludeOtherProformas));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            // _actor.AttemptsTo(Click.On(ProformaGenerationLocator.SetIncludeOtherProformas));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(proformaGenerationEntity.InvoiceDate))
                _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.InvoiceDate, proformaGenerationEntity.InvoiceDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(proformaGenerationEntity.ChangeStatusTo))
                _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.ChangeStatusTo, proformaGenerationEntity.ChangeStatusTo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(proformaGenerationEntity.ProformaStatus))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaGenerationLocator.ProformaStatus, proformaGenerationEntity.ProformaStatus));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView, null));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(proformaGenerationEntity.BillingGroup))
            {
                _featureContext[StepConstants.BillingGroup] = proformaGenerationEntity.BillingGroup;

                _actor.AttemptsTo(ProformaGenerationUsingBillingGroup.With(proformaGenerationEntity.BillingGroup));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.GetDriver().FindElement(CommonLocator.Description.Query).SendKeys(Keys.Tab);
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else
            {
                _actor.AttemptsTo(ProformaGeneration.AddMatter(matterNumber));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.GetDriver().FindElement(CommonLocator.Description.Query).SendKeys(Keys.Tab);
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Clear Print Options: To avoid Template issues on QA
            //if (ApplicationConfigurationBuilder.Instance.Region == Infrastructure.Enums.Regions.Qa)
            //{
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.ClearPrintOptionsButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            // }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Generate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (_actor.DoesElementExist(CommonLocator.InformationMessage, 10))
            {
                var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
                message.Should().NotBeNullOrEmpty();
                message.Should().NotContain("Invalid template information specified.");
            }
        }

        [Then(@"I verify the sections in proforma genration")]
        public void ThenIVerifyTheSectionsInProformaGenration()
        {
            _actor.DoesElementExist(ProformaGenerationLocator.Description).Should().Be(true);
            _actor.DoesElementExist(ProformaGenerationLocator.ProformaStatus).Should().Be(true);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ProformaGeneration));
            _actor.AsksFor(Field.IsAvailable(ProformaGenerationLocator.DataOverride)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaGenerationLocator.ProformaGeneration)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaGenerationLocator.CardPredicate)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaGenerationLocator.ProformaSortingOptions)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaGenerationLocator.ProformaTemplateOptions)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [StepDefinition(@"I can generate an open proforma")]
        public void ThenICanGenerateAnOpenProforma(Table table)
        {
            var proformaGenerationEntity = table.CreateInstance<ProformaGenerationEntity>();
            _featureContext[StepConstants.ProformaRunContext] = proformaGenerationEntity;

            _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.Description, proformaGenerationEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var includeOtherProformas = _actor.AsksFor(SelectedState.Of(ProformaGenerationLocator.GetIncludeOtherProformas));
            if ((!proformaGenerationEntity.IncludeOtherProformas.ToBoolean() && includeOtherProformas) || (proformaGenerationEntity.IncludeOtherProformas.ToBoolean() && !includeOtherProformas))
            {
                _actor.AttemptsTo(Click.On(ProformaGenerationLocator.SetIncludeOtherProformas));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(proformaGenerationEntity.InvoiceDate))
                _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.InvoiceDate, proformaGenerationEntity.InvoiceDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(proformaGenerationEntity.ChangeStatusTo))
                _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.ChangeStatusTo, proformaGenerationEntity.ChangeStatusTo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(proformaGenerationEntity.ProformaStatus))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaGenerationLocator.ProformaStatus, proformaGenerationEntity.ProformaStatus));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            //_actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView, null));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProformaGeneration.AddMatter(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Generate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (_actor.DoesElementExist(CommonLocator.InformationMessage, 10))
            {
                var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
                message.Should().NotBeNullOrEmpty();
                message.Should().NotContain("Invalid template information specified.");
            }
        }

        [StepDefinition(@"I view the performa edit")]
        public void GivenIViewThePerformaEdit()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaEdit, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            // var searchText = "100030176";

            var searchText = ((ProformaGenerationEntity)_featureContext[StepConstants.ProformaRunContext]).Description;
            _featureContext[StepConstants.ProformaDescription] = searchText;

            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)), IsEqualTo.False());
        }

        [Given(@"I add a new Billing Contact info in Proforma")]
        public void GivenIAddANewBillingContactInfoInProforma(Table table)
        {
            var proformaEntity = table.CreateInstance<ProformaGenerationEntity>();
            proformaEntity.Email = table.Rows[0]["Email"] + "@proforma.com";
            _featureContext[StepConstants.ProformaRunContext] = proformaEntity;

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ProformaEdit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(EnterProformaBillingContactDetails.With(proformaEntity));
        }

        [Then(@"the details should be saved correctly in the Proforma")]
        public void ThenTheDetailsShouldBeSavedCorrectlyInTheProforma()
        {
            //Old way of opening the Proforma Edit
            /** _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaEdit, false));
             _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

             var searchText = _featureContext[StepConstants.ProformaDescription].ToString();
             _actor.AttemptsTo(QuickFind.Search(searchText));
             _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());**/

            var proformaEntity = (ProformaGenerationEntity)_featureContext[StepConstants.ProformaRunContext];
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ProformaEdit));
            var actualProforma = _actor.AsksFor(GetProformaBillingContactDetails.Data());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            string BillingContact = "Billing Contact";

            actualProforma.Payer.Should().BeEquivalentTo(proformaEntity.Payer);
            actualProforma.ContactName.Should().Contain(proformaEntity.FirstName + " " + proformaEntity.LastName);
            actualProforma.ContactType.Should().BeEquivalentTo(proformaEntity.ContactType);
            actualProforma.Email.Should().BeEquivalentTo(proformaEntity.Email);

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(BillingContact, ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add period end date at proforma edit")]
        public void GivenIAddPeriodEndDateAtProformaEdit(Table table)
        {
            var proformaEdit = table.CreateInstance<ProformaEditEntity>();
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaEdit, false));
            var searchText = ((ProformaGenerationEntity)_featureContext[StepConstants.ProformaRunContext]).Description;
            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.PeriodEndDate, proformaEdit.PeriodEndDate));
        }


        [StepDefinition(@"add the disbursement on the proforma edit")]
        public void GivenAddTheDisbursementOnTheProformaEdit(Table table)
        {
            var proformaEditDisbursementEntity = table.CreateInstance<ProformaEditDisbursementEntity>();
            _featureContext[StepConstants.DisbursementChangeReason] = proformaEditDisbursementEntity.Reason;
            _actor.AttemptsTo(EnterDisbursementDetails.With(proformaEditDisbursementEntity));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Given(@"select and purge the disbursement on proforma edit")]
        public void GivenSelectAndPurgeTheDisbursementOnProformaEdit()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var reason = _featureContext[StepConstants.DisbursementChangeReason].ToString();
            _actor.AttemptsTo(ScrollToElement.At(EntryAndModifyProcessLocators.DisbursementChangeReason));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.DisbursementChangeReason, reason + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView, StepConstants.DisbursementDetails));

            _actor.WaitsUntil(Appearance.Of(ProformaEditDisbursementLocator.ViewDisbursementGrid), IsEqualTo.True(), 8);

            _actor.AttemptsTo(Click.On(ProformaEditDisbursementLocator.ViewDisbursementGrid));

            _actor.AttemptsTo(Click.On(ProformaEditDisbursementLocator.SelectFirstRow));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(PEPurge.Purge());

            _actor.AttemptsTo(Click.On(ProformaEditLocator.CloseChildFormButton));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Given(@"I attempt to bill it without printing")]
        [StepDefinition(@"I bill it without printing")]
        [StepDefinition(@"bill it without printing")]
        public void GivenBillItWithoutPrinting()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.BillNoPrint));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I navigate to proforma status process")]
        public void GivenINavigateToProformaStatusProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaStatus));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can verify that the matter note is set to true")]
        public void ThenICanVerifyThatTheMatterNoteIsSetToTrue()
        {
            _actor.DoesElementExist(ProformaEditLocator.CreateMatterNoteCheckbox).Should().BeTrue();
        }

        [Then(@"a negative entry is generated")]
        public void ThenANegativeEntryIsGenerated()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.DisbursementDetails));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.DisbursementDetails, GlobalConstants.Grid));

            try
            {
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ExpandChildProcess(GlobalConstants.DisbursementDetails)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            catch (Exception e)
            {
                Console.Write("Error: " + e.Message);
            }

            _actor.AsksFor(Text.Of(ProformaEditDisbursementLocator.BillQuantity)).Should().Be("-1.00000");
        }

        [When(@"I can procexecute the proforma")]
        public void WhenICanProcexecuteTheProforma()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.ProcExclude));
        }

        [When(@"I update the proforma")]
        public void WhenIUpdateTheProforma()
        {
            _actor.AttemptsTo(MainProcessMenu.ClickOn(MainProcessMenuAction.Update));
        }

        [Then(@"no errors are displayed")]
        public void ThenNoErrorsAreDisplayed()
        {
            Action noErrorMessagesException = () => { _actor.AsksFor(ProcessError.Messages()); };

            noErrorMessagesException.Should().Throw<Exception>();
        }

        [Given(@"I cancel the process")]
        public void GivenICancelTheProcess()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Given(@"add the charge type on the proforma edit")]
        public void GivenAddTheChargeTypeOnTheProformaEdit(Table table)
        {
            var proformaEditChargeEntity = table.CreateInstance<ProformaEditChargeEntity>();
            _actor.AttemptsTo(EnterChargeDetails.With(proformaEditChargeEntity));
        }

        [When(@"I close charge details")]
        public void WhenICloseChargeDetails()
        {
            _actor.AttemptsTo(Click.On(ProformaEditLocator.CloseChildFormButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"update the proforma edit")]
        public void WhenUpdateTheProformaEdit()
        {
            _actor.AttemptsTo(Click.On(ProformaEditLocator.UpdateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"error messages displayed should contain")]
        public void ThenErrorMessagesAreDisplayed(Table table)
        {
            var expectedMessages = table.Rows.Select(r => r[ColumnNames.ErrorMessage]).ToList();
            var actualMessages = _actor.AsksFor(ProcessError.Messages());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actualMessages.Should().Contain(expectedMessages);
        }

        [When(@"can generate edit the proforma")]
        public void WhenCanGenerateEditTheProforma(Table table)
        {
            var proformaGenerationEntity = table.CreateInstance<ProformaGenerationEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _featureContext[StepConstants.ProformaRunContext] = proformaGenerationEntity;

            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaGeneration));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.Description, proformaGenerationEntity.Description));

            if (!string.IsNullOrEmpty(proformaGenerationEntity.ProformaStatus))
            {
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.WaitsUntil(Appearance.Of(ProformaGenerationLocator.ProformaStatus), IsEqualTo.True());
                _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.ProformaStatus, proformaGenerationEntity.ProformaStatus));
            }

            if (!string.IsNullOrEmpty(proformaGenerationEntity.ChangeStatusTo))
            {
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.ChangeStatusTo, proformaGenerationEntity.ChangeStatusTo));
            }

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView, null));

            //Clear Print Options: To avoid Template issues on QA
            if (ApplicationConfigurationBuilder.Instance.Region == Infrastructure.Enums.Regions.Qa)
            {
                _actor.AttemptsTo(Click.On(ProformaGenerationLocator.ClearPrintOptionsButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(ProformaGeneration.AddMatter(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Generate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            // workaround to resolve 'Generate/Edit' button not being available due to customisation
            _actor.AttemptsTo(SearchProcess.ByName(Process.MultiProformaEdit, false));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ProformaList));
            _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.MatterInput, matterNumber + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.PopulateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ProformaGenerationLocator.ProformaListIsSelected), IsEqualTo.True());
            _actor.AttemptsTo(JScript.ClickOn(ProformaGenerationLocator.ProformaListIsSelected));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ProformaGenerationLocator.EditProforma), IsEqualTo.True());
            _actor.AttemptsTo(JScript.ClickOn(ProformaGenerationLocator.EditProforma));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can bill the submatters")]
        public void ThenICanBillTheSubMatters(Table table)
        {
            var proformaGenerationEntity = table.CreateInstance<ProformaGenerationEntity>();
            _featureContext[StepConstants.ProformaRunContext] = proformaGenerationEntity;

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaGenerationLocator.ToTaxArea, proformaGenerationEntity.ToTaxArea));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Split));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.XProfomaEditBillNoPrint));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            message.Should().Contain("Auto-generated Invoice Number");
            message.Should().Contain("Auto-generated Tax Invoice Number");

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.WaitsUntil(Appearance.Of(ProformaGenerationLocator.EditProforma), IsEqualTo.True());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Then(@"Proforma edit should show following errors:")]
        public void ThenProformaEditShouldShowFollowingErrors(Table table)
        {
            var expectedErrors = table.Rows.Select(r => r[StepConstants.Error]);
            var messages = _actor.AsksFor(ProcessError.Messages());

            foreach (var error in expectedErrors)
            {
                messages[0].Should().Contain(error);
            }
        }

        [StepDefinition(@"I open the proforma workflow task")]
        public void ThenIOpenTheProformaWorkflowTask()
        {
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.ProformaWorkflowTask));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I open the proforma for submission")]
        public void GivenIOpenTheProformaForSubmission()
        {
            //filter by matter number 
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.MatterNumberContext].ToString(), "Start Workflow"));
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.OpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [StepDefinition(@"I open the proforma linked to submatter1 for submission")]
        public void ThenIOpenTheProformaLinkedToSubmatter1ForSubmission()
        {
            //filter by matter number 
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.SubMatterNumberContextOne].ToString(), "Start Workflow"));
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.OpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I open the proforma linked to submatter2 for submission")]
        public void ThenIOpenTheProformaLinkedToSubmatter2ForSubmission()
        {
            //filter by matter number 
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.SubMatterNumberContextTwo].ToString(), "Start Workflow"));
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.OpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I open the proforma for billing")]
        public void WhenIOpenTheProformaForBilling()
        {
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.MatterNumberContext].ToString(), "Bill submitted - no approval required"));
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.OpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I open the proforma linked to submatter1 for billing")]
        public void ThenIOpenTheProformaLinkedToSubmatter1ForBilling()
        {
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.SubMatterNumberContextOne].ToString(), "Bill submitted - no approval required"));
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.OpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [StepDefinition(@"I open the proforma linked to submatter2 for billing")]
        public void ThenIOpenTheProformaLinkedToSubmatter2ForBilling()
        {
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.SubMatterNumberContextTwo].ToString(), "Bill submitted - no approval required"));
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.OpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I split the proforma")]
        public void ThenISplitTheProforma()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Split));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I open the split proforma")]
        public void ThenIOpenTheSplitProforma()
        {
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.MatterNumberContext].ToString(), "Split Proforma"));
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.OpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [StepDefinition(@"I close the proforma")]
        public void ThenICloseTheProforma()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.CloseProforma));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify that the invoice type has been saved correctly")]
        public void ThenIVerifyThatTheInvoiceTypeHasBeenSavedCorrectly()
        {
            var actualInvoiceType=_actor.GetElementText(ProformaGenerationLocator.InvoiceTypeInput);
            var expectedInvoiceType = _featureContext[StepConstants.InvoiceType].ToString();
            actualInvoiceType.Should().BeEquivalentTo(expectedInvoiceType);
        }

        [StepDefinition(@"I verify that the update payer button does not exist")]
        public void ThenIVerifyThatTheUpdatePayerButtonDoesNotExist()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.BillToProformaPayerCard));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(ProformaGenerationLocator.UpdatePayorButton).Should().BeFalse();
        }

        [Given(@"when I click update payer the payer is updated based on the matter")]
        public void GivenWhenIClickUpdatePayerThePayerIsUpdatedBasedOnTheMatter()
        {
            var expectedPayer = _featureContext[StepConstants.PayerContext].ToString();
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.UpdatePayorButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var actualPayer = _actor.GetElementText(ProformaGenerationLocator.ProformaPayerInput);
            actualPayer.Should().BeEquivalentTo(expectedPayer);
        }


        [Given(@"I validate that the payer button exists")]
        public void GivenIValidateThatThePayerButtonExists()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.ProformaPayerCard));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(ProformaGenerationLocator.UpdatePayorButton).Should().BeTrue();
        }


        [StepDefinition(@"I open the proforma finance first step task")]
        public void WhenIOpenTheProformaFinanceFirstStepTask()
        {
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.MatterNumberContext].ToString(), "Finance First Step"));
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.OpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I verify that the ebilling flag is set to true")]
        public void ThenIVerifyThatTheEbillingFlagIsSetToTrue()
        {
            _actor.DoesElementExist(MatterLocator.EbillingCheckedCheckbox).Should().BeTrue();
        }

        [StepDefinition(@"I add apply adjustment details")]
        public void WhenIAddApplyAdjustmentDetails(Table table)
        {
            var proformaEntity = table.CreateInstance<ProformaEditEntity>();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.ApplyAdjustment, GlobalConstants.Form));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.ApplyAdjustment, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(proformaEntity.Percentage))
            {
                _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.PercentageInput, proformaEntity.Percentage + Keys.Enter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _featureContext[ColumnNames.Percentage] = proformaEntity.Percentage;
            }

            if (!string.IsNullOrEmpty(proformaEntity.Amount))
            {
                _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText(StepConstants.Amount),
                    proformaEntity.Amount + Keys.Enter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _featureContext[ColumnNames.Amount] = proformaEntity.Amount.ToString();
            }

            if (!string.IsNullOrEmpty(proformaEntity.AdjustmentMethod))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.FindInputElementUsingText(StepConstants.AdjustmentMethod), proformaEntity.AdjustmentMethod));
                /*_actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText(StepConstants.AdjustmentMethod),
                    proformaEntity.AdjustmentMethod + Keys.Enter));*/
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(proformaEntity.AdjustmentType))
            {
                _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText(StepConstants.AdjustmentType),
                    proformaEntity.AdjustmentType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!proformaEntity.ApplyAdjustmentAmountToAdjustedCards)
            {
                _actor.AttemptsTo(Checkbox.SetStatus(CommonLocator.getCheckBox(StepConstants.ApplyAdjustmentAmountToAdjustedCards), true));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"I select the invoice type")]
        public void WhenISelectTheInvoiceType(Table table)
        {
            var proformaEntity = table.CreateInstance<ProformaEditEntity>();
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaGenerationLocator.InvoiceTypeInput, proformaEntity.InvoiceType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.InvoiceType] = proformaEntity.InvoiceType;
                
        }

        [StepDefinition(@"I transfer the time cards to another matter")]
        public void ThenITransferTheTimeCardsToAnotherMatter()
        {
            var subMatterNumber = _featureContext[StepConstants.SubMatterNumberContextOne].ToString();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.FeeDetails));
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.ReloadButton(StepConstants.FeeDetails)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.ReloadActionsButton(StepConstants.Transfer)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(ProformaGenerationLocator.TransferMatter).Should().BeTrue();
            _actor.AttemptsTo(SendKeys.To(ProformaGenerationLocator.TransferMatter, subMatterNumber + Keys.Tab));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.Ok));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [StepDefinition(@"I verify that the adjustment details have been saved correctly")]
        public void WhenIVerifyThatTheAdjustmentDetailsHaveBeenSavedCorrectly()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.ApplyAdjustment, GlobalConstants.Form));
            var actualPercentage = _actor.GetElementText(ProformaGenerationLocator.PercentageInput);
            var expectedPercentage = _featureContext[ColumnNames.Percentage].ToString();
            actualPercentage.Should().ContainEquivalentOf(expectedPercentage);
        }

    }
}
