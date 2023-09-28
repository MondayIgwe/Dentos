using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class CreateBankStatementSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        private CreateBankStatement _bankStatement = new();

        public CreateBankStatementSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }


        [Given(@"I create a new bank statement")]
        public void GivenICreateANewBankStatement(Table table)
        {
             _bankStatement = table.CreateInstance<CreateBankStatement>();
            _actor.AttemptsTo(SearchProcess.ByName(Process.CreateBankStatement));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.BankGroup, _bankStatement.BankGroup));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.StatementNumber, _bankStatement.StatementNumber));
            _actor.AttemptsTo(DateControl.SelectDate("Statement Date", _bankStatement.StatementDate));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.Description, _bankStatement.Description));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.BeginningBalance, _bankStatement.BeginningBalance));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.Deposit, _bankStatement.Deposit));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.Withdrawals, _bankStatement.Withdrawal));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.EndingBalance, _bankStatement.EndingBalance));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.BankTransactions));
            //Add deposit transaction
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Bank Transactions", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.Reference, _bankStatement.DepositReference +Keys.Tab));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.Amount, _bankStatement.Deposit + Keys.Tab));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.ClearDate, _bankStatement.ClearDate + Keys.Tab));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.BankStmtDescription, _bankStatement.Description + Keys.Tab));
            //Add withdrawal transaction
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Bank Transactions", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.Reference, _bankStatement.WithdrawalReference+Keys.Tab));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.Amount, _bankStatement.Withdrawal + Keys.Tab));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.ClearDate, _bankStatement.ClearDate + Keys.Tab));
            _actor.AttemptsTo(SendKeys.To(CreateBankStatementLocator.BankStmtDescription, _bankStatement.Description + Keys.Tab));
            _actor.AttemptsTo(Click.On(CreateBankStatementLocator.CalculateEndingBalance));
            _actor.AttemptsTo(Click.On(CommonLocator.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.BankStatementNumber] = _bankStatement;
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"I validate the transactions in the bank statement")]
        public void ThenIValidateTheTransactionsInTheBankStatement()
        {

            _actor.GetDriver().FindElement(CreateBankStatementLocator.BeginningBalance.Query).GetAttribute("value").Replace(",","").Should().Contain(_bankStatement.BeginningBalance);
            _actor.GetDriver().FindElement(CreateBankStatementLocator.Deposit.Query).GetAttribute("value").Replace(",","").Should().Contain(_bankStatement.Deposit);
            _actor.GetDriver().FindElement(CreateBankStatementLocator.Withdrawals.Query).GetAttribute("value").Replace(",","").Should().Contain(_bankStatement.Withdrawal);
            _actor.GetDriver().FindElement(CreateBankStatementLocator.EndingBalance.Query).GetAttribute("value").Replace(",", "").Should().Contain(_bankStatement.EndingBalance);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.BankTransactions));

            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.BankTransactions, GlobalConstants.Grid));

            _actor.AttemptsTo(Click.On(CommonLocator.GridFilterButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            //verify deposit transaction
            _actor.AttemptsTo(SendKeys.To(CommonLocator.GridFilterInput, _bankStatement.DepositReference + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(CreateBankStatementLocator.TrnAmount(_bankStatement.Deposit)).Should().BeTrue();
            //verify withdrawal transaction
            _actor.AttemptsTo(SendKeys.To(CommonLocator.GridFilterInput, _bankStatement.WithdrawalReference + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(CreateBankStatementLocator.TrnAmount(_bankStatement.Withdrawal)).Should().BeTrue();
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Submit), IsEqualTo.False());
        }

        [Given(@"I locate the bank statement")]
        public void GivenILocateTheBankStatement()
        {
            _bankStatement = (CreateBankStatement)_featureContext[StepConstants.BankStatementNumber];
            _actor.AttemptsTo(SearchProcess.ByName(Process.CreateBankStatement));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(_bankStatement.StatementNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
