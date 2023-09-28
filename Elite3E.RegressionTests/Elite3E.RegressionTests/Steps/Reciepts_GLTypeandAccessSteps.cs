using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Reciepts;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Linq;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Receipts;
using Elite3E.PageObjects.PageLocators;
using Elite3E.Infrastructure.Constant;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class Reciepts_GLTypeandAccessSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public Reciepts_GLTypeandAccessSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I navigate to the GL Type and Access process")]
        public void WhenINavigateToTheGLTypeAndAccessProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.GLType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I select the  ""(.*)"" gl type'")]
        public void WhenISelectTheGlType(string searchText)
        {
            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I  use the search button and search for ""(.*)""  gl type")]
        public void WhenIUseTheSearchButtonAndSearchForGlType(string searchText)
        {
            _actor.AttemptsTo(Click.On(GLRecieptsLocators.SearchBtn));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search for the gl type")]

        public void WhenISearchForTheGlType(Table table)
        {
            _actor.AttemptsTo(QuickFind.Search(table.Rows[0][ColumnNames.SearchType]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"Confirm checkbox enabled")]
        public void ThenConfirmCheckboxEnabled()
        {
            if (!_actor.AsksFor(SelectedState.Of(GLRecieptsLocators.SetRecieptbox)))
            {
                _actor.AttemptsTo(Click.On(GLRecieptsLocators.GetRecieptbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"Confirm checkbox disabled")]
        public void ThenConfirmCheckboxDisabled()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(SelectedState.Of(GLRecieptsLocators.GetRecieptbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I confirm and set receipts checkbox")]
        public void ThenIConfirmAndSetReceiptsCheckbox()
        {
            if (!_actor.AsksFor(SelectedState.Of(GLRecieptsLocators.GetRecieptbox)))
            {
                _actor.AttemptsTo(Click.On(GLRecieptsLocators.SetRecieptbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"I confirm remove the default receipts box")]
        public void WhenIConfirmRemoveTheDefaultReceiptsBox()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(GLRecieptsLocators.advancedsearchresult), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(GLRecieptsLocators.advancedsearchresult));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Receipts.Select());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I confirm  receipts checkbox exist")]
        public void WhenIConfirmReceiptsCheckboxExist()
        {
            _actor.AsksFor(SelectedState.Of(GLRecieptsLocators.GetRecieptbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I click on the search button")]
        public void WhenIClickOnTheSearchButton()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(GLRecieptsLocators.SearchBtn));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can verify that the receipt default checkbox exist and is select it")]
        public void ThenICanVerifyThatTheReceiptDefaultCheckboxExistAndIsSelectIt()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(GLRecieptsLocators.GetRecieptbox), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(GLRecieptsLocators.GetRecieptbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"click submit")]
        public void ThenClickSubmit()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(CommonLocator.Homepage), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I navigate to the Receipts Apply/Reverse Payments process")]
        public void WhenINavigateToTheReceiptsApplyReversePaymentsProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ReceiptsApplyReversePayments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I navigate to the receipts apply/reverse process")]
        public void GivenINavigateToTheReceiptsApplyReverseProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ReceiptsApplyReversePayments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I navigate to the invoices child form")]
        public void WhenINavigateToTheInvoicesChildForm()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, "Receipts - Apply / Reverse Payments"));
            _actor.AttemptsTo(ChildProcessView.SwitchToView("Invoices", "Simple"));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.Invoices, ChildProcessMenuAction.Add));
        }


        [When(@"I select the general ledger child form")]
        public void WhenISelectTheGeneralLedgerChildForm()
        {
            var menuItems = new List<string>()
            {
                StepConstants.GeneralLedger
            };

            _actor.AttemptsTo(AddChildProcess.ByName(menuItems));
        }

        [Then(@"I can verify that the Gl type exist\.")]
        public void ThenICanVerifyThatTheGlTypeIsAll_Books_()
        {
            _actor.AsksFor(SelectedState.Of(GLRecieptsLocators.Gltypeinput));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can verify the Gl type exist\.")]
        public void ThenICanVerifyTheGlTypeExist_()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(SelectedState.Of(GLRecieptsLocators.Gltypeinput));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search using advanced find in GLType process")]
        public void WhenISearchUsingAdvancedFindInGLTypeProcess(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.GLType));
            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            var Receipt = _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteriaCol));
            _featureContext[StepConstants.SearchedPayors] = Receipt;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"Gltype  is ""(.*)""")]
        public void ThenGltypeIs(string value)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(ValueAttribute.Of(GLRecieptsLocators.Gltypeinput)).Should().BeEquivalentTo(value);
        }

        [Then(@"Gltype  is not blank")]
        public void ThenGltypeIsNotBlank()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            string text = _actor.AsksFor(ValueAttribute.Of(GLRecieptsLocators.Gltypeinput));
            text.Should().NotBeEmpty();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
