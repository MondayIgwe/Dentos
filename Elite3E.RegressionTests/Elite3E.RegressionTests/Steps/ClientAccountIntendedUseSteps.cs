using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ProformaTrust;
using FluentAssertions;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ClientAccountIntendedUseSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ClientAccountIntendedUseSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }


        [Then(@"I can set the allow for billing")]
        public void ThenICanSetTheAllowForBilling()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(SelectedState.Of(ProformaTrustLocators.SetAllowForBillingCheckbox));
            _actor.AttemptsTo(Click.On(ProformaTrustLocators.SetAllowForBillingCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can set the the allow disbursements")]
        public void GivenICanVerifyThatTheAllowDisbursementsCheckboxIsUncheckedAndSelectIt()
        {
            _actor.AsksFor(SelectedState.Of(ProformaTrustLocators.SetAllowDisbursement));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I open the client account intended use process")]
        public void Iopentheclientaccountintendeduseprocess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountIntendedUse));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"search client account intended use")]
        public void WhenSearchClientAccountIntendedUse()
        {
            var clientAccountCode = _featureContext[StepConstants.ClientAccountCodeContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(clientAccountCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can submit the record")]
        public void ThenICanSubmitTheRecord()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(CommonLocator.Submit), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"I can verify that all the checkbox selected")]
        public void ThenICanVerifyThatAllTheCheckboxSelected()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (_actor.AsksFor(SelectedState.Of(ProformaTrustLocators.GetAllowForBillingCheckbox)))
            {
                _actor.AttemptsTo(Click.On(ProformaTrustLocators.SetAllowForBillingCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!_actor.AsksFor(SelectedState.Of(ProformaTrustLocators.GetIsAllowDisbursement)))
            {
                _actor.AttemptsTo(Click.On(ProformaTrustLocators.SetAllowDisbursement));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

        }

        [Then(@"I can enter the form details")]
        public void ThenICanEnterTheFormDetails(Table table)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.Code, table.Rows[0][ColumnNames.Code]));
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.Description, table.Rows[0][ColumnNames.Description]));
        }

        [Given(@"I view the client account reciept process")]
        public void GivenIViewTheClientAccountRecieptProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientAccountReceipt));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add the required fields")]
        public void GivenIAddTheRequiredFields(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.TrustReceiptType, table.Rows[0][ColumnNames.TrustReceiptType]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.BankAcctTrust, table.Rows[0][ColumnNames.BankAcctTrust]));
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.DrawnBy, table.Rows[0][ColumnNames.DrawnBy]));
        }

        [Given(@"select Client Account Intendeded Use from client account reciept detail form")]
        public void GivenSelectClientAccountIntendededUseFromClientAccountRecieptDetailForm()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var clientAccountCode = _featureContext[StepConstants.ClientAccountCodeContext].ToString();
            var clientAccountDescription = _featureContext[StepConstants.Description].ToString();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAccReceiptDetail));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaTrustLocators.IntendedUse, clientAccountDescription));
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.Matter, matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can add the amount and trust receipt type")]
        public void ThenICanAddTheAmountAndTrustReceiptType(Table table)
        {
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.Amount, table.Rows[0][ColumnNames.Amount])); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ProformaTrustLocators.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.TrustReceiptType, table.Rows[0][ColumnNames.TrustReceiptType])); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.ClientAccountReceiptComments, table.Rows[0][ColumnNames.Reason])); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.UseDetails, table.Rows[0][ColumnNames.Reason])); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add the apply client account child form intended uses")]
        public void WhenIAddTheApplyClientAccountChildFormIntendedUses()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ApplyClientAccount));

            var clientAccountCode = _featureContext[StepConstants.ClientAccountCodeContext].ToString();
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(StepConstants.ApplyClientAccount, LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.IntendedUse, clientAccountCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ProformaTrustLocators.Reload));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ProformaTrustLocators.SelectAutoRecord));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ProformaTrustLocators.Select));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"I can verify the trust disbursement type and payee fields")]
        public void ThenICanVerifyTheTrustDisbursementTypeAndPayeeFields()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ApplyClientAccount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.ApplyClientAccount, GlobalConstants.Form));

            string payee = _featureContext[StepConstants.PayeeNameContext].ToString();
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.PayeeInput, payee));
            _actor.AskingFor(ValueAttribute.Of(ProformaTrustLocators.TrustDisbursementType)).Should().BeNullOrEmpty();
            _actor.AskingFor(ValueAttribute.Of(ProformaTrustLocators.Payee)).Should().NotBeNullOrEmpty();
        }

        [Then(@"I can verify the trust disbursement type")]
        public void ThenICanVerifyTheTrustDisbursementType()
        {
            _actor.AttemptsTo(Click.On(ProformaTrustLocators.CloseAlert));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ApplyClientAccount)); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.ApplyClientAccount, "Form"));

            //_actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChildFormGridDropDown));
            //_actor.AttemptsTo(Click.On(ProformaTrustLocators.Form));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            string payee = _featureContext[StepConstants.PayeeNameContext].ToString();
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.PayeeInput, payee));
            _actor.AskingFor(ValueAttribute.Of(ProformaTrustLocators.TrustDisbursementType)).Should().NotBeNullOrEmpty();
            _actor.AskingFor(ValueAttribute.Of(ProformaTrustLocators.Payee)).Should().NotBeNullOrEmpty();
        }

        [Then(@"I can confirm the Intended use option is enabled\.")]
        public void ThenICanConfirmTheIntendedUseOptionIsEnabled_()
        {
            if (!_actor.AsksFor(SelectedState.Of(ProformaTrustLocators.GetAllowForBillingCheckbox)))
            {
                _actor.AttemptsTo(Click.On(ProformaTrustLocators.SetAllowForBillingCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(CommonLocator.Submit), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can generate the group bill")]
        public void ThenICanGenerateTheGroupBill(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.GroupProforma));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.BillingGroup, table.Rows[0][ColumnNames.BillingGroup]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.Officegroup, table.Rows[0][ColumnNames.Officegroup]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.ProfDate, table.Rows[0][ColumnNames.ProfDate]));
            _actor.AttemptsTo(Click.On(CommonLocator.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(CommonLocator.Submit), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Given(@"I view the matter in performa edit")]
        public void GivenIViewTheMatterInPerformaEdit()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaEdit, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
        }

        [When(@"I cancel the process")]
        public void WhenICancelTheProcess()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Then(@"I verify the sections in client account receipt")]
        public void ThenIVerifyTheSectionsInClientAccountReceipt()
        {
            _actor.DoesElementExist(ProformaTrustLocators.DaysToClear).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAccReceiptDetail));
            _actor.AsksFor(Field.IsAvailable(ProformaTrustLocators.ClientAccountReceiptDetail)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }
}
