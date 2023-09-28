using System.Linq;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Extensions;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.DirectCheque;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class DirectChequeSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public DirectChequeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I add a new direct cheque")]
        public void WhenIAddANewDirectCheque()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.DirectCheque));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I update the client refund")]
        public async Task IssueAClientRefund(Table table)
        {
            var clientRefund = _actor.AsksFor(SelectedState.Of(DirectChequeLocators.GetClientRefundCheckbox));

            var clientRefundEntity = table.CreateInstance<ClientRefundEntity>();
            clientRefundEntity.ReceiptType = table.Rows.Select(r => r["Receipt Type"]).ToList()[0];
            clientRefundEntity.DocumentNumber = table.Rows.Select(r => r["Document Number"]).ToList()[0];
            //clear the receipt type field to avoid sort string issue 
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            driver.FindElement(DirectChequeLocators.ReceiptType.Query).SendKeys(Keys.Control + "a");
            driver.FindElement(DirectChequeLocators.ReceiptType.Query).SendKeys(Keys.Delete);
            if (string.IsNullOrEmpty(clientRefundEntity.Client))
            {
                //This is just a client name if client is null or empty
                clientRefundEntity.Client = _featureContext[StepConstants.Entity].ToString();
            }
            if (!clientRefund)
            {
                _actor.AttemptsTo(Click.On(DirectChequeLocators.SetClientRefundCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (clientRefundEntity.ClientRefund.ToBoolean())
            {
                clientRefundEntity.Client = (string.IsNullOrEmpty(clientRefundEntity.Client)) ? _featureContext[StepConstants.ClientNumber].ToString() : clientRefundEntity.Client;

                if (!string.IsNullOrEmpty(clientRefundEntity.Client))
                {
                    _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.Client, clientRefundEntity.Client));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }

                if (!string.IsNullOrEmpty(clientRefundEntity.ReceiptType))
                {

                    // check the receipt  Type exists else create one
                    var receiptType = new ApiReceiptTypeEntity();
                    var data = new ReceiptTypeData();
                    receiptType.Description = clientRefundEntity.ReceiptType;
                    receiptType.Code = clientRefundEntity.ReceiptType;
                    await data.ReceiptTypeAsync(receiptType);

                    _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.ReceiptType, clientRefundEntity.ReceiptType));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }

                if (!string.IsNullOrEmpty(clientRefundEntity.DocumentNumber))
                {
                    _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.DocumentNumber, clientRefundEntity.DocumentNumber));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _featureContext[StepConstants.DocumentNumber] = clientRefundEntity.DocumentNumber;
                }
                else
                {
                    var docNum = _featureContext[StepConstants.DocumentNumber].ToString();
                    _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.DocumentNumber, docNum));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
            }
        }

        [StepDefinition(@"I verify that the client refund form has been populated")]
        public void ThenIVerifyThatTheClientRefundFormHasBeenPopulated()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.ClientRefund));
            string payor = _actor.GetElementText(DirectChequeLocators.PayerClientRefundDiv);
            payor.Should().NotBeNullOrEmpty();
        }

        [StepDefinition(@"I update the amount for the client refund")]
        public void ThenIUpdateTheAmountForTheClientRefund(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.ClientRefund));
            _actor.WaitsUntil(Appearance.Of(DirectChequeLocators.ClientRefundAmount), IsEqualTo.True());
            _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.ClientRefundAmount, table.Rows[0][ColumnNames.Amount] + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I verify that the direct cheque has been created")]
        public void WhenIVerifyThatTheDirectChequeHasBeenCreated()
        {
            var chequeNumber = _featureContext[StepConstants.ChequeNumber].ToString();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.DirectCheque));
            _actor.AttemptsTo(QuickFind.Search(chequeNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.GetElementText(DirectChequeLocators.ReadChequeNumber).Should().BeEquivalentTo(chequeNumber);
        }

        [StepDefinition(@"I verify that the cheque number and the cheque date have been auto populated")]
        public void ThenIVerifyThatTheChequeNumberAndTheChequeDateHaveBeenAutoPopulated()
        {
            _actor.GetElementText(DirectChequeLocators.WriteChequeNumber).Should().NotBeNullOrEmpty();
            _actor.GetElementText(DirectChequeLocators.ChequeDate).Should().NotBeNullOrEmpty();
        }


        [When(@"I advanced find and select direct Cheque")]
        public void GivenIAdvancedFindAndSelectDirectCheque(Table table)
        {
            var documentNumber = _featureContext[StepConstants.DocumentNumber].ToString();

            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();

            foreach (var col in searchCriteriaCol)
            {
                col.SearchValue = documentNumber;
            }

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteriaCol));
            driver.FindElement(CommonLocator.SearchResultsCheckBox.Query).Click();
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
        }


        [When(@"I verify client refund child form")]
        public void AddClientRefundChildForm(Table table)
        {
            var clientRefundEntity = table.CreateInstance<ClientRefundEntity>();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView, null));

            if (!_actor.DoesElementExist(DirectChequeLocators.ChildForm(clientRefundEntity.ClientRefundChildForm)))
            {
                _actor.AttemptsTo(Click.On(DirectChequeLocators.ChildFormElipsis));
                _actor.AttemptsTo(Checkbox.SetStatus(DirectChequeLocators.ChildFormOptionCheckbox(clientRefundEntity.ClientRefundChildForm), true));
                _actor.WaitsUntil(Appearance.Of(DirectChequeLocators.ChildForm(clientRefundEntity.ClientRefundChildForm)), IsEqualTo.True());
            }

            _actor.AttemptsTo(Click.On(DirectChequeLocators.ChildForm(clientRefundEntity.ClientRefundChildForm)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (_actor.DoesElementExist(DirectChequeLocators.ChildFormAmountDiv))
                _actor.AttemptsTo(Click.On(DirectChequeLocators.ChildFormAmountDiv));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.ChildFormAmount, clientRefundEntity.Amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ProformaEditLocator.CloseChildFormButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.Office, clientRefundEntity.Office));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I update the direct cheque")]
        public void UpdateTheDirectCheque(Table table)
        {

            var clientRefundEntity = table.CreateInstance<ClientRefundEntity>();

            _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.BankAccount, clientRefundEntity.BankAccount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _featureContext[StepConstants.BankAccountContext] = clientRefundEntity.BankAccount;

            _actor.AsksFor(ValueAttribute.Of(DirectChequeLocators.ChequeDate)).Trim().Should().NotBeNullOrEmpty();
            _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.TransactionType, clientRefundEntity.TransactionType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(clientRefundEntity.ChequeNumber))
            {
                _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.WriteChequeNumber, clientRefundEntity.ChequeNumber));
                _featureContext[StepConstants.ChequeNumber] = clientRefundEntity.ChequeNumber;
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.Amount, clientRefundEntity.Amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(DirectChequeLocators.OfficeDropDown));
            _actor.AttemptsTo(Click.On(ReceiptLocator.DropDownSelection(clientRefundEntity.Office)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.ChequeTemplate, clientRefundEntity.ChequeTemplate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(clientRefundEntity.ChequePrinter))
            {
                _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.ChequePrinter, clientRefundEntity.ChequePrinter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            string payee = _featureContext[StepConstants.PayeeNameContext].ToString();

            _actor.AttemptsTo(SendKeys.To(DirectChequeLocators.Payee, payee));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _featureContext[StepConstants.Payee] = payee;
        }

        [Then(@"I verify the direct cheque number")]
        public void ThenIVerifyTheDirectChequeNumber()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var chequeNumber = driver.FindElement(DirectChequeLocators.ReadChequeNumber.Query).Text.Trim();

            chequeNumber.Should().NotBeNullOrEmpty();

            _featureContext[StepConstants.ChequeNumber] = chequeNumber;

        }


        [Then(@"the mandatory error messages are displayed")]
        public void MandatoryErrorMessagesAreDisplayed(Table table)
        {
            var fieldNames = table.Rows.Select(r => r[ColumnNames.MandatoryField]);
            var actualMessages = _actor.AsksFor(ProcessError.Messages());

            actualMessages.Count.Should().BeGreaterOrEqualTo(fieldNames.Count());

            foreach (var fieldName in fieldNames)
            {
                var errorMessage = $"{fieldName}: {StepConstants.MandatoryFieldErrorMessage}.";
                actualMessages.Should().Contain(errorMessage);
            }
        }

        [Then(@"I verify the sections in direct cheque")]
        public void ThenIVerifyTheSectionsInDirectCheque()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientRefund));
            _actor.AsksFor(Field.IsAvailable(DirectChequeLocators.ChildForm1099)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(DirectChequeLocators.ClientRefund)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }
}
