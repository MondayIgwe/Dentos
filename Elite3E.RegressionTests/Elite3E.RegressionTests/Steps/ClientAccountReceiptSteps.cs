using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.PrintReceipt;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountReceipt;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountReceiptRequest;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
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
    public class ClientAccountReceiptSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ClientAccountReceiptSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the client account receipt process")]
        public void GivenINavigateToTheClientAccountReceiptProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountReceipt));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Given(@"I add a new client account receipt")]
        public void GivenIAddANewClientAccountReceipt(Table table)
        {
            var clientAccountReceiptEntity = table.CreateInstance<ClientAccountReceiptEntity>();
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, Process.ClientAccountReceipt));
            _actor.AttemptsTo(DateControl.SelectDate("Transaction Date", clientAccountReceiptEntity.TransactionDate));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientAccountReceiptLocators.ClientAccountReceiptType, clientAccountReceiptEntity.ClientAccountReceiptType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Client Account Acct", clientAccountReceiptEntity.ClientAccountAcct));
            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptLocators.DocumentNumber, clientAccountReceiptEntity.DocumentNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.DocumentNumber] = clientAccountReceiptEntity.DocumentNumber;
        }

        [When(@"I add client account receipt detail child form data")]
        public void WhenIAddClientAccountReceiptDetailChildFormData(Table table)
        {
            var clientAccountReceiptEntity = table.CreateInstance<ClientAccountReceiptEntity>();
            clientAccountReceiptEntity.MatterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.ClientAccountReceiptDetail, GlobalConstants.Form));
            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptLocators.Amount, clientAccountReceiptEntity.Amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Matter #", clientAccountReceiptEntity.MatterNumber));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientAccountReceiptLocators.TrustIntendedUse, clientAccountReceiptEntity.IntendedUse));
            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptLocators.ReasonComment, clientAccountReceiptEntity.Reason));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I open the client account receipt record")]
        public void ThenIOpenTheClientAccountReceiptRecord()
        {
            var documentNumber = _featureContext[StepConstants.DocumentNumber].ToString();
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptFinanceApprovalFilterIcon));
            _actor.AttemptsTo(SendKeys.To(ClientAccountReceiptRequestLocators.ClientAccountReceiptFinanceApprovalInput, documentNumber + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(ClientAccountReceiptRequestLocators.ClientAccountReceiptRecordOpenButton), IsEqualTo.True(), 1);
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ClientAccountReceiptRecordOpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify that the details of the receipt are correct")]
        public void ThenIVerifyThatTheDetailsOfTheReceiptAreCorrect()
        {
            var expectedMatterNo = _featureContext[StepConstants.MatterNumberContext].ToString();
            var actualMatterNo = _actor.GetElementText(ClientAccountReceiptLocators.MatterNo);
            expectedMatterNo.Should().BeEquivalentTo(actualMatterNo);
        }

        [When(@"I tick the aml checks checkbox and approve")]
        public void WhenITickTheAmlChecksCheckboxAndApprove()
        {
            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.AMLChecksCompleteCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ClientAccountReceiptRequestLocators.ApprovedCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I select existing receipt")]
        public void GivenISelectExistingReceipt()
        {
            var documentNumber = _featureContext[StepConstants.DocumentNumber].ToString();
            _actor.AttemptsTo(QuickFind.Search(documentNumber));
        }

        [Then(@"I print the receipt")]
        public void ThenIPrintTheReceipt(Table table)
        {
            var printEntity = table.CreateInstance<PrintEntity>();
            _actor.AttemptsTo(PrintReceipt.With(printEntity));
        }

        [When(@"I click the gl postings buttons")]
        public void WhenIClickTheGlPostingsButtons()
        {
            _actor.AttemptsTo(Click.On(ClientAccountReceiptLocators.GlPostingsButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I save the journal manager number")]
        public void WhenISaveTheJournalManagerNumber()
        {
            var journalManagerNumber = _actor.GetElementText(ClientAccountReceiptLocators.JournalManagerDiv);
            _featureContext[StepConstants.JournalManager] = journalManagerNumber;
        }

        [Then(@"I validate the status is posted")]
        public void ThenIValidateTheStatusIsPosted()
        {
            var status = _actor.GetElementText(ClientAccountReceiptLocators.PostStatusDiv);
            status.Should().BeEquivalentTo(status);
        }

        [StepDefinition(@"I close the dialog")]
        public void ThenICloseTheDialog()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I validate the gl postings for unit '([^']*)'")]
        public void ThenIValidateTheGlPostingsFforUnit(string operatingUnit)
        {
            var glMaskedValues = _actor.AsksFor(TextList.For(EntryAndModifyProcessLocators.GLMaskedValues));
            glMaskedValues.Any(item => item.StartsWith(operatingUnit));
            _featureContext[StepConstants.GLMaskedValues] = glMaskedValues;
        }

        [StepDefinition(@"I reverse the client account receipt")]
        public void ThenIReverseTheClientAccountReceipt(Table table)
        {
            _actor.AttemptsTo(Click.On(ClientAccountReceiptLocators.ReversedCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate("Reversed On", table.Rows[0][ColumnNames.ReversalDate]));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Reverse Reason", table.Rows[0][ColumnNames.Reason]));
        }

        [StepDefinition(@"I verify that the reversed client account receipt is not available")]
        public void WhenIVerifyThatTheReversedClientAccountReceiptIsNotAvailable()
        {
            _actor.DoesElementExist(CommonLocator.NoSearchRecords).Should().BeTrue();
        }




    }

}