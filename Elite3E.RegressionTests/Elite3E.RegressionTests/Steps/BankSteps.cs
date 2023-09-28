using System;
using System.Linq;
using TechTalk.SpecFlow;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using FluentAssertions;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank;
using Elite3E.Infrastructure.Extensions;
using System.Threading;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.PageLocators;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.ProcessInteraction.BankAccountClientAccount;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class BankSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public BankSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I try to add an account for bank account office")]
        public void WhenITryToAddAnAccountForBankAccountOffice()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountOffice));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the account identifier is available")]
        public void ThenTheAccountIdentifierIsAvailable()
        {
            var available = _actor.AsksFor(Field.IsAvailable(CommonLocator.FindInputElementUsingText(LocatorConstants.AccountIdentifier)));
            available.Should().BeTrue();
        }

        [Given(@"I navigate to bank account client account")]
        public void GivenINavigateToBankAccountClientAccount()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountClientAccount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify that the quick find include '([^']*)'")]
        public void ThenIVerifyThatTheQuickFindInclude(string text)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.FindDivElementContainsText("Quick Find")));
            _actor.DoesElementExist(CommonLocator.FindDivElementContainsExactText(text)).Should().BeTrue();
        }

       
        [Then(@"I verify the '([^']*)' field exists on the advanced find")]
        public void ThenIVerifyTheFieldExistsOnTheAdvancedFind(string parameter)
        {
            _actor.AttemptsTo(Click.On(MatterLocator.AdvancedFindTab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(BankAccountClientAccountLocators.CurrencyInputAttribute(parameter)).Should().BeTrue();
        }


        [When(@"I try to add an account for bank group maintenance")]
        public void WhenITryToAddAnAccountForBankGroupMaintenance()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankGroupMaintenance));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I try to add an account for bank account client account")]
        public void WhenITryToAddAnAccountForBankAccountClientAccount()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountClientAccount));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"a bank account office exists ""(.*)""")]
        public void GivenABankAccountOfficeExists(string bankName)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountOffice));

            _actor.AttemptsTo(QuickFind.Search(bankName));

            if (!_actor.AsksFor(SelectedState.Of(BankAccountOfficeLocators.GetRemittanceAccount)))
            {
                _actor.AttemptsTo(Click.On(BankAccountOfficeLocators.SetRemittanceAccount));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!_actor.AsksFor(SelectedState.Of(BankAccountOfficeLocators.GetUltimateRemittanceAccount)))
            {
                _actor.AttemptsTo(Click.On(BankAccountOfficeLocators.SetUltimateRemittanceAccount));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Given(@"I view the bank account office ""(.*)""")]
        public void GivenIViewTheBankAccountOffice(string bankName)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountOffice));

            _actor.AttemptsTo(QuickFind.Search(bankName));
        }

        [Given(@"clone the bank account office")]
        public void GivenCloneTheBankAccountOffice(Table table)
        {
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.Button(LocatorConstants.CloneButton)));

            Thread.Sleep(TimeSpan.FromSeconds(4));

            var officeBankAccount = table.CreateInstance<OfficeBankAccount>();

            _actor.WaitsUntil(Appearance.Of(BankAccountOfficeLocators.GetRemittanceAccount), IsEqualTo.True(), 0);

            _actor.AttemptsTo(SendKeys.To(BankAccountOfficeLocators.AccountName, officeBankAccount.AccountName));

            _actor.AttemptsTo(SendKeys.To(BankAccountOfficeLocators.Description, officeBankAccount.Description));

            var remittanceAccountSelected = _actor.AsksFor(SelectedState.Of(BankAccountOfficeLocators.GetRemittanceAccount));

            if ((!officeBankAccount.RemittanceAccount.ToBoolean() && remittanceAccountSelected) || (officeBankAccount.RemittanceAccount.ToBoolean() && !remittanceAccountSelected))
            {
                _actor.AttemptsTo(Click.On(BankAccountOfficeLocators.SetRemittanceAccount));
            }
        }


        [When(@"I try to add a duplicate ultimate remittance account")]
        public void WhenITryToAddADuplicateUltimateRemittanceAccount()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [When(@"I try to add a duplicate remittance account")]
        public void WhenITryToAddADuplicateRemittanceAccount()
        {
            // Sleep is required for the first message to disappear
            Thread.Sleep(TimeSpan.FromSeconds(7));

            _actor.AttemptsTo(Click.On(BankAccountOfficeLocators.SetUltimateRemittanceAccount));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }


        [Then(@"the message ""(.*)"" is displayed")]
        public void ThenTheMessageIsDisplayed(string expectedMessage)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var actualmessage = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            expectedMessage.Should().BeEquivalentTo(actualmessage);
            _actor.AttemptsTo(Click.On(BankAccountOfficeLocators.SetRemittanceAccount));
        }

        [When(@"I view bank account client account ""(.*)""")]
        public void WhenIViewBankAccountClientAccount(string searchText)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountClientAccount));
            _featureContext[StepConstants.SearchTextContext] = searchText;
            _actor.AttemptsTo(QuickFind.Search(searchText));
        }

        [When(@"I view bank account office ""(.*)""")]
        public void WhenIViewBankAccountOffice(string searchText)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountOffice));
            _featureContext[StepConstants.SearchTextContext] = searchText;
            _actor.AttemptsTo(QuickFind.Search(searchText));
        }

        [When(@"I view bank group maintenance ""(.*)""")]
        public void WhenIViewBankGroupMaintenance(string searchText)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankGroupMaintenance));
            _featureContext[StepConstants.SearchTextContext] = searchText;
            _actor.AttemptsTo(QuickFind.Search(searchText));
        }

        [Then(@"the account identifier is not mandatory on bank account client account")]
        public void ThenTheAccountIdentifierIsNotMandatoryOnBankAccountClientAccount()
        {
            _actor.AttemptsTo(Clear.On(CommonLocator.FindInputElementUsingText(LocatorConstants.AccountIdentifier)));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountClientAccount));
            var searchText = _featureContext[StepConstants.SearchTextContext].ToString();
            _actor.AttemptsTo(QuickFind.Search(searchText));

            var accountIdentifier = _actor.AsksFor(Text.Of(CommonLocator.FindInputElementUsingText(LocatorConstants.AccountIdentifier)));

            string.IsNullOrEmpty(accountIdentifier).Should().BeTrue();

        }

        [Then(@"the account identifier is not mandatory on  bank account office")]
        public void ThenTheAccountIdentifierIsNotMandatoryOnBankAccountOffice()
        {
            _actor.AttemptsTo(Clear.On(CommonLocator.FindInputElementUsingText(LocatorConstants.AccountIdentifier)));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountOffice));
            var searchText = _featureContext[StepConstants.SearchTextContext].ToString();
            _actor.AttemptsTo(QuickFind.Search(searchText));

            var accountIdentifier = _actor.AsksFor(Text.Of(CommonLocator.FindInputElementUsingText(LocatorConstants.AccountIdentifier)));

            string.IsNullOrEmpty(accountIdentifier).Should().BeTrue();
        }

        [Then(@"the account identifier is not mandatory on bank group maintenance")]
        public void ThenTheAccountIdentifierIsNotMandatoryOnBankGroupMaintenance()
        {
            _actor.AttemptsTo(Clear.On(CommonLocator.FindInputElementUsingText(LocatorConstants.AccountIdentifier)));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

            _actor.AttemptsTo(SearchProcess.ByName(Process.BankGroupMaintenance));
            var searchText = _featureContext[StepConstants.SearchTextContext].ToString();
            _actor.AttemptsTo(QuickFind.Search(searchText));

            var accountIdentifier = _actor.AsksFor(Text.Of(CommonLocator.FindInputElementUsingText(LocatorConstants.AccountIdentifier)));

            string.IsNullOrEmpty(accountIdentifier).Should().BeTrue();
        }

        [Given(@"I view the full bank reconcilliation for ""(.*)""")]
        public void GivenIViewTheFullBankReconcilliationFor(string bankName)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FullBankReconciliation));
            _actor.AttemptsTo(QuickFind.Search(bankName));
        }

        [When(@"I view bank/cash unreconciled transactions")]
        public void WhenIViewBankCashUnreconciledTransactions()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView,""));
            _actor.AttemptsTo(Click.On(FullBankReconciliationLocator.BankCashUnreconciledTransactions));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"recon comments column exists")]
        public void ThenReconCommentsColumnExists()
        {
         
            _actor.scrollToElementInView(FullBankReconciliationLocator.GridHeaderReconComment);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(FullBankReconciliationLocator.GridHeaderReconComment)).Should().Be(true);
        }

        [Given(@"I view the fbr unreconciled items report")]
        public void GivenIViewTheFbrUnreconciledItemsReport()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FbrUnreconciledItemsReport, false));
        }


        [When(@"I set the fbr unreconciled items report")]
        public void WhenISetTheFbrUnreconciledItemsReport(Table table)
        {
            var fbrItemsReportEntity = table.CreateInstance<FbrItemsReportEntity>();

            _actor.AttemptsTo(SendKeys.To(FbrItemsReportLocator.BankGroup, fbrItemsReportEntity.BankGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(FbrItemsReportLocator.BankStatement, fbrItemsReportEntity.BankStatement));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(FbrItemsReportLocator.GlBook, fbrItemsReportEntity.GlBook));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"run the report")]
        public void WhenRunTheReport()
        {
            _actor.AttemptsTo(FlyOutButtonMenu.Click(StepConstants.RunMetric, StepConstants.RunReport));
        }

        [Then(@"reconciliation comments column exists on the report")]
        public void ThenReconciliationCommentsColumnExistsOnTheReport()
        {
            _actor.ScrollIntoElement(CommonLocator.FindDivElementContainsText(StepConstants.ReconciliationComment), 4, "pagedown");

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(CommonLocator.FindDivElementContainsText(StepConstants.ReconciliationComment))).Should().BeTrue();
        }

        [Given(@"I view the fbr Reconciled items report")]
        public void GivenIViewTheFbrReconciledItemsReport()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FbrReconciledItemsReport, false));
        }


        [When(@"I set the fbr reconciled items report")]
        public void WhenISetTheFbrReconciledItemsReport(Table table)
        {


            var fbrItemsReportEntity = table.CreateInstance<FbrItemsReportEntity>();

            _actor.AttemptsTo(SendKeys.To(FbrItemsReportLocator.BankGroup, fbrItemsReportEntity.BankGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(FbrItemsReportLocator.BankStatement, fbrItemsReportEntity.BankStatement));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(FbrItemsReportLocator.WorkSheetId, fbrItemsReportEntity.WorkSheetId));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.ReconciledAsOfDate, fbrItemsReportEntity.ReconciledAsOfDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [When(@"run the report for reconciled items")]
        public void WhenRunTheReportForReconciledItems()
        {

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"I try to add a bank group maintenance")]
        public void WhenITryToAddABankGroupMaintenance()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankGroupMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the bank reconciliation frequency available are")]
        public void ThenTheBankReconciliationFrequencyAvailableAre(Table table)
        {
            var expectedValues = table.Rows.Select(r => r[ColumnNames.Frequency]);
            var actualValues = _actor.AsksFor(GetAllDropdownValues.For(BankGroupMaintenanceLocator.BankReconciliationFrequencyDropDown, CommonLocator.DropDownOptions));

            expectedValues.Should().BeEquivalentTo(actualValues);
        }

        [Then(@"I verify the sections in bank account client account")]
        public void ThenIVerifyTheSectionsInBankAccountClientAccount()
        {
            _actor.DoesElementExist(BankAccountClientAccountLocators.Bank).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.AccountName).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.Description).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.MoneyType).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.Status).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.AccountNumber).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.Office).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.Language).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.Currency).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.GLType).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.CashGLAccount).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.ContraGLAccount).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.AnchorMask).Should().Be(true);
            _actor.DoesElementExist(BankAccountClientAccountLocators.Bank).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Client));
            _actor.AsksFor(Field.IsAvailable(BankAccountClientAccountLocators.PowerAndEstatesInformation)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(BankAccountClientAccountLocators.MatterClientAccount)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(BankAccountClientAccountLocators.RestrictAccountUsersAndRoles)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [When(@"I enter the required data and submit the bank account client account")]
        public void WhenIEnterTheRequiredDataAndSubmitTheBankAccountClientAccount(Table table)
        {
            var bankAccountEntity = table.CreateInstance<BankAccountClientAccountEntity>();
            var entityName = _featureContext[StepConstants.Entity].ToString();
            bankAccountEntity.BankName = String.IsNullOrEmpty(bankAccountEntity.BankName) ? "Bank_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : bankAccountEntity.BankName;
            bankAccountEntity.ABARoutingNumber = String.IsNullOrEmpty(bankAccountEntity.ABARoutingNumber) ? "auto_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : bankAccountEntity.ABARoutingNumber;
            bankAccountEntity.AccountName = String.IsNullOrEmpty(bankAccountEntity.AccountName) ? "auto_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : bankAccountEntity.AccountName;
            bankAccountEntity.Description = String.IsNullOrEmpty(bankAccountEntity.Description) ? "desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : bankAccountEntity.Description;
            bankAccountEntity.AccountNumber = String.IsNullOrEmpty(bankAccountEntity.AccountNumber) ? StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : bankAccountEntity.AccountNumber;
            _actor.AttemptsTo(EnterDataInBankBranchMaintenance.With(bankAccountEntity, entityName));
            if (!String.IsNullOrEmpty(bankAccountEntity.IsNewBankGroup) && bankAccountEntity.IsNewBankGroup.ToUpper().Equals("NO"))
            {
                bankAccountEntity.BankGroupName = _featureContext[StepConstants.BankGroupName].ToString();
            }
            //submit bank branch maintenance
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(EnterDataInBankAccountClientAccount.With(bankAccountEntity));
            //submit bank account client account
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _featureContext[StepConstants.BankGroupName] = bankAccountEntity.AccountName;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"I verify and submit bank group maintenance")]
        public void ThenIVerifyAndSubmitBankGroupMaintenance(Table table)
        {
            var bankAccountEntity = table.CreateInstance<BankAccountClientAccountEntity>();
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankGroupMaintenance));
            var bankGroup = _featureContext[StepConstants.BankGroupName].ToString();
            _actor.AttemptsTo(QuickFind.Search(bankGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(BankGroupMaintenanceLocator.ModuleType, bankAccountEntity.ModuleType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(BankGroupMaintenanceLocator.ReconciliationRuleSet, bankAccountEntity.ReconciliationRuleSet));
            _actor.AttemptsTo(Click.On(BankGroupMaintenanceLocator.BankToBank));
            _actor.AttemptsTo(Click.On(BankGroupMaintenanceLocator.CashToCash));
            _actor.AttemptsTo(Click.On(BankGroupMaintenanceLocator.ReconcileAcrossStatements));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I locate the bank statement in Full Bank Reconciliation")]
        public void GivenILocateTheBankStatementInFullBankReconciliation()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FullBankReconciliation));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I enter details in full bank reconciliation process with '([^']*)'")]
        public void WhenIEnterDetailsInFullBankReconciliationProcessWith(string worksheetID)
        {
            var _bankStatement = (CreateBankStatement)_featureContext[StepConstants.BankStatementNumber];
            _bankStatement.WorksheetId = StepArgumentExtension.ReplaceDynamicValues(worksheetID);
            _actor.AttemptsTo(SendKeys.To(FullBankReconciliationLocator.BankGroup, _bankStatement.BankGroup));
            _actor.AttemptsTo(SendKeys.To(FullBankReconciliationLocator.WorksheetId, _bankStatement.WorksheetId));
            _actor.AttemptsTo(Click.On(CommonLocator.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(FullBankReconciliationLocator.LoadTransactions));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(FullBankReconciliationLocator.BankTransactionsHeader), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(CommonLocator.GridFilterButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            //verify deposit transaction
            _actor.AttemptsTo(SendKeys.To(CommonLocator.GridFilterInput, _bankStatement.DepositReference + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(FullBankReconciliationLocator.CashType("Deposit")).Should().BeTrue();
            _actor.DoesElementExist(FullBankReconciliationLocator.Amount(_bankStatement.Deposit)).Should().BeTrue();
            _actor.AttemptsTo(Click.On(FullBankReconciliationLocator.ClearCheckbox));
            //verify withdrawal transaction
            _actor.AttemptsTo(SendKeys.To(CommonLocator.GridFilterInput, _bankStatement.WithdrawalReference + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(FullBankReconciliationLocator.CashType("Withdrawal")).Should().BeTrue();
            _actor.DoesElementExist(FullBankReconciliationLocator.Amount(_bankStatement.Withdrawal)).Should().BeTrue();
            _actor.AttemptsTo(Click.On(FullBankReconciliationLocator.ClearCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.CloseFilter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(FullBankReconciliationLocator.MatchButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Reconcile));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [StepDefinition(@"I advanced find and verify bank reconciled items report")]
        public void GivenIAdvancedFindAndVerifyBankReconciledItemsReport(Table table)
        {
            var bankStatement = (CreateBankStatement)_featureContext[StepConstants.BankStatementNumber];
            var searchList = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            foreach (var row in searchList)
            {
                if (row.SearchColumn.Equals("Worksheet ID"))
                {
                    row.SearchValue = bankStatement.WorksheetId;
                }
            }
            _actor.AttemptsTo(AdvancedFindInBankReconciledItemsReport.GetSearchResults(searchList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(FullBankReconciliationLocator.GroupsHeader));
            _actor.ScrollIntoElement(FullBankReconciliationLocator.ReconciledTransaction(bankStatement.DepositReference, bankStatement.Deposit), 4, "pagedown");
            _actor.DoesElementExist(FullBankReconciliationLocator.ReconciledTransaction(bankStatement.DepositReference, bankStatement.Deposit)).Should().BeTrue();
            _actor.ScrollIntoElement(FullBankReconciliationLocator.ReconciledTransaction(bankStatement.WithdrawalReference, bankStatement.Withdrawal), 4, "pagedown");
            _actor.DoesElementExist(FullBankReconciliationLocator.ReconciledTransaction(bankStatement.WithdrawalReference, bankStatement.Withdrawal)).Should().BeTrue();

        }


        [StepDefinition(@"I close the full bank reconciliation for the bank statement")]
        public void GivenICloseTheFullBankReconciliationForTheBankStatement()
        {
            var bankStatement = (CreateBankStatement)_featureContext[StepConstants.BankStatementNumber];
            _actor.AttemptsTo(SearchProcess.ByName(Process.FullBankReconciliation));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(bankStatement.StatementNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(FullBankReconciliationLocator.ClosedCheckbox));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }


        [Then(@"I verify there are no items in unreconciled items report for GL book '([^']*)'")]
        public void ThenIVerifyThereAreNoItemsInUnreconciledItemsReportForGLBook(string glType)
        {
            var bankStatement = (CreateBankStatement)_featureContext[StepConstants.BankStatementNumber];
            _actor.AttemptsTo(SearchProcess.ByName(Process.UnreconciledItemsReport, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(FbrItemsReportLocator.BankStatement, bankStatement.StatementNumber));
            _actor.AttemptsTo(SendKeys.To(FbrItemsReportLocator.BankAccount, bankStatement.BankGroup));
            _actor.AttemptsTo(SendKeys.To(FbrItemsReportLocator.GlBook, glType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Text.Of(FbrItemsReportLocator.UnpostedReceipts)).Should().BeEquivalentTo("0.00");
            _actor.AsksFor(Text.Of(FbrItemsReportLocator.UnPostedPayments)).Should().BeEquivalentTo("0.00");

        }


        [When(@"I add bank account client account by filling all details")]
        public void WhenIAddBankAccountClientAccount(Table table)
        {
            var bankActClientActDetails = table.CreateInstance<BankAccountClientAccountEntity>();

            //Enter bank name
            if (!string.IsNullOrEmpty(bankActClientActDetails.Bank))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.Bank, bankActClientActDetails.Bank));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            //Enter Account name
            if (!string.IsNullOrEmpty(bankActClientActDetails.AccountName))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.AccountName, bankActClientActDetails.AccountName));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter description
            if (!string.IsNullOrEmpty(bankActClientActDetails.Description))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.Description, bankActClientActDetails.Description));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter money type
            if (!string.IsNullOrEmpty(bankActClientActDetails.MoneyType))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.MoneyType, bankActClientActDetails.MoneyType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter bank account type
            if (!string.IsNullOrEmpty(bankActClientActDetails.BankAccountType))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.BankAccountType, bankActClientActDetails.BankAccountType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter bank group
            if (!string.IsNullOrEmpty(bankActClientActDetails.BankGroup))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.BankGroup, bankActClientActDetails.BankGroup));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter Status
            if (!string.IsNullOrEmpty(bankActClientActDetails.Status))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.Status, bankActClientActDetails.Status));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter Account Number
            if (!string.IsNullOrEmpty(bankActClientActDetails.AccountNumber))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.AccountNumber, bankActClientActDetails.AccountNumber));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter Office
            if (!string.IsNullOrEmpty(bankActClientActDetails.Office))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.Office, bankActClientActDetails.Office));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter Currency
            if (!string.IsNullOrEmpty(bankActClientActDetails.Currency))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.Currency, bankActClientActDetails.Currency));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter Language
            if (!string.IsNullOrEmpty(bankActClientActDetails.Language))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.Language, bankActClientActDetails.Language));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter GL Type
            if (!string.IsNullOrEmpty(bankActClientActDetails.GLType))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.GLType, bankActClientActDetails.GLType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Enter Cash GL Account
            if (!string.IsNullOrEmpty(bankActClientActDetails.CashGLAccount))
            {
                _actor.AttemptsTo(Click.On(BankAccountClientAccountLocators.SearchIconBasedOnLabel("Cash GL Account")));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, bankActClientActDetails.CashGLAccount + Keys.Enter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.QuickFindSearchResults(bankActClientActDetails.CashGLAccount)));
                _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            }

            //Enter Contra GL Account
            if (!string.IsNullOrEmpty(bankActClientActDetails.ContraGLAccount))
            {
                _actor.AttemptsTo(Click.On(BankAccountClientAccountLocators.SearchIconBasedOnLabel("Contra GL Account")));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, bankActClientActDetails.ContraGLAccount + Keys.Enter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.QuickFindSearchResults(bankActClientActDetails.ContraGLAccount)));
                _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            }

            //Enter Anchor Mask
            if (!string.IsNullOrEmpty(bankActClientActDetails.AnchorMask))
            {
                _actor.AttemptsTo(SendKeys.To(BankAccountClientAccountLocators.AnchorMask, bankActClientActDetails.AnchorMask));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.UpdateButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I want to add quick reconciliation details")]
        public void WhenIWantToAddQuickReconciliationDetails(Table table)
        {
            var bankReconciliationEntity = table.CreateInstance<QuickBankReconciliationEntity>();
            _featureContext[StepConstants.GLProjectCode] = bankReconciliationEntity.StatementNumber;

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Bank Group", bankReconciliationEntity.BankGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StatementDate, bankReconciliationEntity.StatementDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText(GlobalConstants.StatementNumber), bankReconciliationEntity.StatementNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //Entering these values as a work around for not getting errors
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Deposits"), bankReconciliationEntity .Deposits+ Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Deposits"), bankReconciliationEntity.DepositsOverWrite+ Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //Verify remaining deposits and withdrawals are zero
            _actor.GetElementTextList(CommonLocator.FindDivElementContainsName("OpenDeposits")).Should().BeEquivalentTo("0.00");
            _actor.GetElementTextList(CommonLocator.FindDivElementContainsName("OpenWithdrawals")).Should().BeEquivalentTo("0.00");

            //Click on Reconcile
            if (!_actor.AsksFor(SelectedState.Of(CommonLocator.FindInputElementUsingText("Reconcile"))))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.FindInputElementUsingText("Reconcile")));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }


    }
}
