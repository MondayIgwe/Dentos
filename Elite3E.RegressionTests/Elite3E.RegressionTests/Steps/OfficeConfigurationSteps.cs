using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxRate;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ProformaTrust;
using System.Collections.Generic;
using System.Linq;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Timekeeper;
using Elite3E.Infrastructure.Selenium;
using FluentAssertions;
using Elite3E.Infrastructure.Configuration;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.OfficeConfiguration;
using OpenQA.Selenium;
using Elite3E.PageObjects.Interaction.ProcessInteraction.OfficeConfiguration;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class OfficeConfigurationSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public OfficeConfigurationSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];

        }

        [When(@"I open the office configuration process")]
        [Given(@"I open the office configuration process")]
        public void WhenIOpenTheOfficeConfigurationProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.OfficeConfiguration));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the new required fields are displayed")]
        public void ThenTheNewRequiredFieldsAreDisplayed()
        {
            _actor.DoesElementExist(ProformaTrustLocators.ClientAccountReceiptTypeDefault).Should().BeTrue();
            _actor.DoesElementExist(ProformaTrustLocators.ClientAccountAcctDefault).Should().BeTrue();
        }

        [When(@"I try to update the form with no details the <ErrorMessage> is displayed for required fields")]
        public void WhenITryToUpdateTheFormWithNoDetailsTheErrorMessageIsDisplayedForRequiredFields(Table table)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var messages = _actor.AsksFor(ProcessError.Messages());
            messages.Count.Should().Be(4);

            foreach (var rows in table.Rows)
            {
                messages.Should().Contain(rows.Values);
            }

        }

        [When(@"the client account receipt approval required checkbox is displayed")]
        public void WhenTheClientAccountReceiptApprovalRequiredCheckboxIsDisplayed()
        {
            _actor.DoesElementExist(ProformaTrustLocators.ClientAccountReceiptApprovalRequiredCheckbox).Should().BeTrue();
        }

        [When(@"the client account receipt approval required checkbox can be set to false or true")]
        public void WhenTheClientAccountReceiptApprovalRequiredCheckboxCanBeSetToFalseOrTrue()
        {
            _actor.AttemptsTo(Checkbox.SetStatus(ProformaTrustLocators.ClientAccountReceiptApprovalRequiredCheckbox, true));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(ProformaTrustLocators.ClientAccountReceiptApprovalRequiredCheckboxChecked).Should().BeTrue();
        }

        [When(@"I try to add a duplicate office")]
        public void WhenITryToAddADuplicateOffice(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var officeConfigurationEntity = table.CreateInstance<OfficeConfigurationEntity>();
            string officeConfigCreated = _featureContext[StepConstants.OfficeConfig].ToString();
            string payee = _featureContext[StepConstants.PayeeNameContext].ToString();
            string clientAccountLabel = "Client Account Acct Default";

            _actor.WaitsUntil(Existence.Of(ProformaTrustLocators.Office), IsEqualTo.True());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaTrustLocators.Office, officeConfigCreated));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.TrustDisbursementType, officeConfigurationEntity.TrustDisbursementType)); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.DaysToDispatch, officeConfigurationEntity.DaysToDispatch)); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.Payee, payee));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ProformaTrustLocators.TimeKeeperLeaver, officeConfigurationEntity.TimeKeeperLeaver)); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (_actor.DoesElementExist(ProformaTrustLocators.ClientAccountReceiptTypeDefault) &&
            _actor.DoesElementExist(ProformaTrustLocators.ClientAccountAcctDefault))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaTrustLocators.ClientAccountReceiptTypeDefault, officeConfigurationEntity.ClientAccountReceiptType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle(clientAccountLabel, officeConfigurationEntity.ClientAccountDefault));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(Click.On(CommonLocator.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"click on submit")]
        public void WhenClickOnSubmit()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(TaxRatesLocator.Submit), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(TaxRatesLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(CommonLocator.Homepage), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I verify required fields on office configuration")]
        public void WhenIVerifyRequiredFieldsOnOfficeConfiguration(Table table)
        {
            List<string> requiredFields = table.Rows.Select(r => r["Required Fields"]).ToList();
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.OfficeConfigurationRequiredFields), IsEqualTo.True());
            List<string> foundRequiredFields = _actor.GetElementTextList(TimeKeeperLeaverChecklistLocators.OfficeConfigurationRequiredFields);
            requiredFields.ForEach(childForm => foundRequiredFields.Any(x => x.Trim().Equals(childForm.Trim())).Should().BeTrue());
        }

        [StepDefinition(@"I cancel office configuration")]
        public void ThenICloseTimekeeperLeaverChecklist()
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Cancel), IsEqualTo.True());
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.Cancel));

            if (_actor.DoesElementExist(TimeKeeperLeaverChecklistLocators.CancelDialogYesButton))
                _actor.AttemptsTo(Click.On(TimeKeeperLeaverChecklistLocators.CancelDialogYesButton));
        }

        [Given(@"click on submit")]
        public void GivenClickOnSubmit()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(TaxRatesLocator.Submit), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(TaxRatesLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I verify the proforma approver checkbox exists")]
        public void GivenIVerifyTheProformaApproverCheckboxExists()
        {
            _actor.DoesElementExist(ProformaTrustLocators.ProformerApproverCheckbox).Should().BeTrue();
        }

        [StepDefinition(@"I open existing office configuration record")]
        public void GivenIOpenExistingOfficeConfigurationRecord(Table table)
        {
            var officeRecord = table.Rows[0][ColumnNames.Office].ToString();

            _actor.AttemptsTo(QuickFind.Search(officeRecord));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I set the proforma approver checkbox to '([^']*)'")]
        public void WhenISetTheProformaApproverCheckboxTo(string status)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            if (status.Equals("True", System.StringComparison.CurrentCultureIgnoreCase))
            {
                _actor.AttemptsTo(Checkbox.SetStatus(ProformaTrustLocators.ProformerApproverCheckbox, true));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else
            {
                _actor.AttemptsTo(Checkbox.SetStatus(ProformaTrustLocators.ProformerApproverCheckbox, false));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"I validate the proforma approver checkbox is set to '([^']*)'")]
        public void ThenIValidateTheProformaApproverCheckboxIsSetTo(string status)
        {
            if (status.Equals("True", System.StringComparison.CurrentCultureIgnoreCase))
            {
                _actor.DoesElementExist(ProformaTrustLocators.ProformerApproverChecked).Should().BeTrue();
            }
            else
            {
                _actor.DoesElementExist(ProformaTrustLocators.ProformerApproverChecked).Should().BeFalse();
            }
        }
        
        [When(@"I update office configuration")]
        public void WhenIUpdateOfficeConfiguration(Table table)
        {
            var officeConfigurationEntity = table.CreateInstance<OfficeConfigurationEntity>();
            _actor.AttemptsTo(UpdateOfficeConfiguration.With(officeConfigurationEntity));
            _featureContext[StepConstants.CoverLetterNarrative] = officeConfigurationEntity.CoverLetterNarrative;
            _featureContext[StepConstants.InvoiceNarrative] = officeConfigurationEntity.InvoiceNarrative;
            _featureContext[StepConstants.GovernmentSystemTemplate] = officeConfigurationEntity.GovtSysTemplate;
        }

    }
}
