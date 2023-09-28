using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountDisbursement;
using Elite3E.RegressionTests.StepHelpers;
using UploadFile = Elite3E.PageObjects.Interaction.CommonInteraction.UploadFile;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.WorkFlowDashBoard;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WorkFlowDashbord;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Selenium;
using FluentAssertions;
using System.Linq;
using System;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class ClientAccountDisbursementRequestSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        public ClientAccountDisbursementRequestSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }
        [StepDefinition(@"I create the client account disbursement request")]
        public void ThenICreateAClientAccountDisbursementRequest(Table table)
        {
            var clientAccountDisbursementRequest = table.CreateInstance<ClientAccountDisbursementEntity>();
            _featureContext[StepConstants.PaymentAmount] = clientAccountDisbursementRequest.Amount;

            _actor.AttemptsTo(SendKeys.To(ClientAcctDisbursementLocator.DisbursementType, clientAccountDisbursementRequest.DisbursementType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientAcctDisbursementLocator.ClientAccountAcct, clientAccountDisbursementRequest.ClientAccountAcct));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAcctDisbursementLocator.Matter, clientAccountDisbursementRequest.Matter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAcctDisbursementLocator.TrustIntendedUse, clientAccountDisbursementRequest.IntendedUse));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAcctDisbursementLocator.Amount, clientAccountDisbursementRequest.Amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientAcctDisbursementLocator.PaymentName, clientAccountDisbursementRequest.PaymentName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
   
        }
        [Then(@"I validate feilds are not available")]
        public void ThenIValidateFeildsAreNotAvailable()
        {
            var elementExist = _actor.DoesElementExist(CommonLocator.ValidateText("Paid"));
            elementExist.Should().Be(false);
            elementExist = _actor.DoesElementExist(CommonLocator.ValidateText("GL Type"));
            elementExist.Should().Be(false);
            elementExist = _actor.DoesElementExist(CommonLocator.ValidateText("Lock Matter"));
            elementExist.Should().Be(false);
            elementExist = _actor.DoesElementExist(CommonLocator.ValidateText("Client Account Cheque Index"));
            elementExist.Should().Be(false);
            elementExist = _actor.DoesElementExist(CommonLocator.ValidateText("Lock Matter"));
            elementExist.Should().Be(false);
            elementExist = _actor.DoesElementExist(CommonLocator.ValidateText("Group Index"));
            elementExist.Should().Be(false);
            elementExist = _actor.DoesElementExist(CommonLocator.ValidateText("GL Posting"));
            elementExist.Should().Be(false);
            elementExist = _actor.DoesElementExist(CommonLocator.ValidateText("Bank Group Name"));
            elementExist.Should().Be(false);
            elementExist = _actor.DoesElementExist(CommonLocator.ValidateText("Document"));
            elementExist.Should().Be(false);
        }

        [Then(@"I validate feilds are available")]
        public void ThenIValidteNeFeilds()
        {
            var elementExist = _actor.DoesElementExist(ClientAcctDisbursementLocator.IsPaymentInfoVerified);
            elementExist.Should().Be(true);
            elementExist = _actor.DoesElementExist(ClientAcctDisbursementLocator.FinalPayment);
            elementExist.Should().Be(true);
            elementExist = _actor.DoesElementExist(ClientAcctDisbursementLocator.IsRefund);
            elementExist.Should().Be(true);
            elementExist = _actor.DoesElementExist(ClientAcctDisbursementLocator.IsClientApprovalObtained);
            elementExist.Should().Be(true);
            elementExist = _actor.DoesElementExist(ClientAcctDisbursementLocator.VerificationMethod);
            elementExist.Should().Be(true);
        }


        [Then(@"I want to click approve")]
        public void ThenIWantToClickApproveApprove()
        {
            var elementExist = _actor.DoesElementExist(ClientAcctDisbursementLocator.IsApproved);
            elementExist.Should().Be(true);
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.IsApproved));
        }
        [Given(@"I search for process with option")]
        public void GivenISearchForProcessWithOption(Table table)
        {
            var searchPhrase = table.Rows.Select(r => r["search"]).ToList()[0];
            var ProcessName = table.Rows.Select(r => r["option"]).ToList()[0];

            var browser = _actor.Using<BrowseTheWeb>().WebDriver;
            //Searches for a process.
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(CommonLocator.SearchIcon), IsEqualTo.True(), 60);

            _actor.AttemptsTo(Click.On(CommonLocator.SearchIcon));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(CommonLocator.SearchInput), IsEqualTo.True(), 60);

            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchInput, searchPhrase));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var resultProcesses = browser.FindElements(CommonLocator.SearchResults.Query);

            var found = resultProcesses.FirstOrDefault(ele => ele.Text.Trim().Contains(ProcessName));
            found.Click();
        }

        [StepDefinition(@"I open the request")]
        public void ThenIOpenTheRequest()
        {

            var PaymentAmount = _featureContext[StepConstants.PaymentAmount].ToString();
            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.ClientAccountDisbursement));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(PaymentAmount, GlobalConstants.TrustDisbursementRequestEntryFinanceApproval));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.OpenButton));

        }

        [StepDefinition(@"I want to approve it")]
        public void ThenIWantToApproveIt()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Approve));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I confirm IsPaymentInformationVerified")]
        public void ThenIConfirmIsPaymentInformationVerified(Table table)
        {
            var clientAccountDisbursementRequest = table.CreateInstance<ClientAccountDisbursementEntity>();
            bool IsPaymentInfoVerified = _actor.AsksFor(SelectedState.Of(ClientAcctDisbursementLocator.IsPaymentInfoVerified));

            if (clientAccountDisbursementRequest.IsPaymentInformationVerified && !IsPaymentInfoVerified)
            {
                _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.IsPaymentInfoVerified));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"I confirm IsClientApprovalObtained")]
        public void ThenIConfirmIsClientApprovalObtained(Table table)
        {
            var clientAccountDisbursementRequest = table.CreateInstance<ClientAccountDisbursementEntity>();
            bool IsClientApprovalObtained = _actor.AsksFor(SelectedState.Of(ClientAcctDisbursementLocator.IsClientApprovalObtained));

            if (clientAccountDisbursementRequest.IsClientApprovalObtained && !IsClientApprovalObtained)
            {
                _actor.AttemptsTo(Click.On(ClientAcctDisbursementLocator.IsClientApprovalObtained));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"I want to submit and get an error message")]
        public void ThenIWantToSubmitAndGetAnErrorMessage(Table table)
        {
            var expectedMessage = table.Rows.Select(r => r["message"]).ToList()[0];
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            message.Should().Contain(expectedMessage);
        }
        

    }
}
