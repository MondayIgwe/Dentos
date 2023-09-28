using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Entity.VendorPayeeMaintenance;
using Elite3E.Infrastructure.Extensions;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Payee;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payee;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System.Linq;
using Elite3E.Infrastructure.Constant;
using Elite3E.RegressionTests.RestServicesTest.Common;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public sealed class PayeeSteps
    {

        private readonly FeatureContext _featureContext;
        private readonly Actor _actor;

        public PayeeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I add the bank account type")]
        public void WhenIAddTheBankAccountType(Table table)
        {


            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountType));

            var bankAccountTypeEntity = table.CreateInstance<BankAccountTypeEntity>();

            _featureContext[StepConstants.BankAccountTypeContext] = bankAccountTypeEntity;

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(BankAccountTypeLocator.CodeInput, bankAccountTypeEntity.Code));

            _actor.AttemptsTo(SendKeys.To(BankAccountTypeLocator.Description, bankAccountTypeEntity.Description));

            var intermediaryBankSelected = _actor.AsksFor(SelectedState.Of(BankAccountTypeLocator.GetIntermediaryBank));

            if ((!intermediaryBankSelected && bankAccountTypeEntity.IntermediaryBank.ToBoolean()) ||
                (intermediaryBankSelected && !bankAccountTypeEntity.IntermediaryBank.ToBoolean()))
            {
                _actor.AttemptsTo(Click.On(BankAccountTypeLocator.SetIntermediaryBank));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the bank account type is saved")]
        public void ThenTheBankAccountTypeIsSaved()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.BankAccountType));

            var bankAccountTypeEntity = (BankAccountTypeEntity)_featureContext[StepConstants.BankAccountTypeContext];

            _actor.AttemptsTo(QuickFind.Search(bankAccountTypeEntity.Description));

            _actor.AsksFor(Text.Of(BankAccountTypeLocator.GetCode)).Trim().Should().BeEquivalentTo(bankAccountTypeEntity.Code);

            _actor.AsksFor(ValueAttribute.Of(BankAccountTypeLocator.Description)).Should().BeEquivalentTo(bankAccountTypeEntity.Description);

            _actor.AsksFor(SelectedState.Of(BankAccountTypeLocator.GetIntermediaryBank)).Should().Be(bankAccountTypeEntity.IntermediaryBank.ToBoolean());
        }

        [Then(@"the intermediary bank field is correctly named")]
        public void ThenTheIntermediaryBankFieldIsCorrectlyNamed()
        {
            _actor.AsksFor(Text.Of(BankAccountTypeLocator.IntermediaryBankLabel)).Should().BeEquivalentTo(StepConstants.IntermediaryBank); ;
        }

        [Given(@"I add the vendor in vendor/payee maintenance")]
        public void GivenIAddTheVendorInVendorPayeeMaintenance(Table table)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorPayeeMaintenance));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.EntityInput, table.Rows[0][ColumnNames.Entity]));

            _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorPayeeMaintenanceLocator.GlobalVendor, table.Rows[0][ColumnNames.GlobalVendor]));

        }

        [Given(@"add the payee for the vendor")]
        public void GivenAddThePayeeForTheVendor(Table table)
        {
            var payeeDetailsEntity = table.CreateInstance<PayeeDetailsEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Payee));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.Payee, ChildProcessMenuAction.Add));

            _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorPayeeMaintenanceLocator.PaymentTerms, payeeDetailsEntity.PaymentTerms));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorPayeeMaintenanceLocator.Office, payeeDetailsEntity.Office));
        }
      
        [StepDefinition(@"add the payee bank")]
        public void GivenAddThePayeeBank(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayeeBank));
            var payeeBankEntity = table.CreateInstance<PayeeBankEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Payee));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.PayeeBank, ChildProcessMenuAction.Add));

            _featureContext[StepConstants.PayeeBankContext] = payeeBankEntity;

            var defaultBankSelected = _actor.AsksFor(SelectedState.Of(VendorPayeeMaintenanceLocator.GettDefaultBank));
            if ((!defaultBankSelected && payeeBankEntity.DefaultBank.ToBoolean()) ||
                (defaultBankSelected && !payeeBankEntity.DefaultBank.ToBoolean()))
            {
                _actor.AttemptsTo(Click.On(VendorPayeeMaintenanceLocator.SetDefaultBank));
            }

            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.Description, payeeBankEntity.Description));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.AccountNumber, payeeBankEntity.AccountNumber));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.BeneficiaryName, payeeBankEntity.BeneficiaryName));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.BankCode, payeeBankEntity.BankCode));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.BankAddress1, payeeBankEntity.Address1));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.BankAddress2, payeeBankEntity.Address2));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.BankAddress3, payeeBankEntity.Address3));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.ClearingCodeType, payeeBankEntity.ClearingCodeType));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.ClearingCode, payeeBankEntity.ClearingCode));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.PaymentReference, payeeBankEntity.PaymentReference));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"update the payee bank")]
        public void WhenUpdateThePayeeBank(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayeeBank));
            var payeeBankEntity = table.CreateInstance<PayeeBankEntity>();
            _featureContext[StepConstants.UpdatedPayeeBankContext] = payeeBankEntity;
            var oldPayeeBankEntity = _featureContext[StepConstants.PayeeBankContext];
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.Description, payeeBankEntity.Description));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.AccountNumber, payeeBankEntity.AccountNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"add the intermediary bank")]
        [Given(@"add the intermediary bank")]
        public void GivenAddTheIntermediaryBank(Table table)
        {
            var payeeIntermediaryBankEntity = table.CreateInstance<PayeeIntermediaryBankEntity>();

            _featureContext[StepConstants.PayeeIntermediaryBankContext] = payeeIntermediaryBankEntity;

            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.IbAddress1, payeeIntermediaryBankEntity.IbAddress1));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.IbAddress2, payeeIntermediaryBankEntity.IbAddress2));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.IbAddress3, payeeIntermediaryBankEntity.IbAddress2));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.IbClearingCodeType, payeeIntermediaryBankEntity.IbClearingCodeType));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.IbClearingCode, payeeIntermediaryBankEntity.IbClearingCode));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.SwiftCode, payeeIntermediaryBankEntity.SwiftCode));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.PayNow, payeeIntermediaryBankEntity.PayNowRegistrationNumber + Keys.Tab));
        }

        [StepDefinition(@"I add a payee bank")]
        public void ThenIAddAPayeeBank(Table table)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PayeeMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var payeeNumber = _featureContext[StepConstants.PayeeNameContext].ToString();
            _actor.AttemptsTo(QuickFind.Search(payeeNumber));

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Payee));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.PayeeBank, ChildProcessMenuAction.Add));

            _actor.AttemptsTo(Click.On(VendorPayeeMaintenanceLocator.IsDefaultBankCheckbox));
            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.Description, table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.AccountNumber, table.Rows[0][ColumnNames.AccountNumber]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"submit the vendor/payee")]
        public void WhenSubmitTheVendorPayee()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            _featureContext[StepConstants.VendorPayeeNumberContext] = message.Split(" ")[4];
        }

        [When(@"I submit payee")]
        public void WhenISubmitPayee()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            _featureContext[StepConstants.VendorPayeeNumberContext] = message.Split(" ")[12];
        }

        [Then(@"payee bank is saved")]
        public void ThenPayeeBankIsSaved()
        {
            VerifyPayeeBankDetails();
        }

        private void VerifyPayeeBankDetails()
        {
            var payeeBankEntity = (PayeeBankEntity)_featureContext[StepConstants.PayeeBankContext];

            _actor.AsksFor(SelectedState.Of(VendorPayeeMaintenanceLocator.GettDefaultBank)).Should()
                .Be(payeeBankEntity.DefaultBank.ToBoolean());

            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.Description)).Should().BeEquivalentTo(payeeBankEntity.Description);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.AccountNumber)).Should().BeEquivalentTo(payeeBankEntity.AccountNumber);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.BeneficiaryName)).Should().BeEquivalentTo(payeeBankEntity.BeneficiaryName);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.BankCode)).Should().BeEquivalentTo(payeeBankEntity.BankCode);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.BankAddress1)).Should().BeEquivalentTo(payeeBankEntity.Address1);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.BankAddress2)).Should().BeEquivalentTo(payeeBankEntity.Address2);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.BankAddress3)).Should().BeEquivalentTo(payeeBankEntity.Address3);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.ClearingCodeType)).Should().BeEquivalentTo(payeeBankEntity.ClearingCodeType);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.ClearingCode)).Should().BeEquivalentTo(payeeBankEntity.ClearingCode);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.PaymentReference)).Should().BeEquivalentTo(payeeBankEntity.PaymentReference);
        }

        [Then(@"intermediary bank is saved")]
        public void ThenIntermediaryBankIsSaved()
        {
            VerifyIntermediaryBankDetails();
        }

        private void VerifyIntermediaryBankDetails()
        {
            var payeeIntermediaryBankEntity =
                (PayeeIntermediaryBankEntity)_featureContext[StepConstants.PayeeIntermediaryBankContext];

            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.IbAddress1)).Should()
                .BeEquivalentTo(payeeIntermediaryBankEntity.IbAddress1);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.IbAddress2)).Should()
                .BeEquivalentTo(payeeIntermediaryBankEntity.IbAddress2);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.IbAddress3)).Should()
                .BeEquivalentTo(payeeIntermediaryBankEntity.IbAddress2);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.IbClearingCodeType)).Should()
                .BeEquivalentTo(payeeIntermediaryBankEntity.IbClearingCodeType);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.IbClearingCode)).Should()
                .BeEquivalentTo(payeeIntermediaryBankEntity.IbClearingCode);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.SwiftCode)).Should()
                .BeEquivalentTo(payeeIntermediaryBankEntity.SwiftCode);
            _actor.AsksFor(ValueAttribute.Of(VendorPayeeMaintenanceLocator.PayNow)).Should()
                .BeEquivalentTo(payeeIntermediaryBankEntity.PayNowRegistrationNumber);
        }

        [Given(@"I add the payee from payee maintenance")]
        public void GivenIAddThePayeeFromPayeeMaintenance(Table table)
        {
            var vendor = _featureContext[StepConstants.VendorNameContext].ToString();
            var payeeName = "";
            var payeeDetailsEntity = table.CreateInstance<PayeeDetailsEntity>();

            _actor.AttemptsTo(SearchProcess.ByName(Process.PayeeMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle(GlobalConstants.Vendor, vendor));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorPayeeMaintenanceLocator.PaymentTerms, payeeDetailsEntity.PaymentTerms));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorPayeeMaintenanceLocator.Office, payeeDetailsEntity.Office));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());


            if (!string.IsNullOrEmpty(payeeDetailsEntity.PayeeType))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorPayeeMaintenanceLocator.PayeeTypeInput, payeeDetailsEntity.PayeeType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(payeeDetailsEntity.Unit))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.Unit, payeeDetailsEntity.Unit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(payeeDetailsEntity.Currency))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.Currency, payeeDetailsEntity.Currency));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(payeeDetailsEntity.PayeeStatus))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorPayeeMaintenanceLocator.Status, payeeDetailsEntity.PayeeStatus));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            //US Only
            bool state = _actor.AsksFor(SelectedState.Of(VendorPayeeMaintenanceLocator.Is1099Checkbox));
            if (payeeDetailsEntity.Is1099 && !state)
            {
                _actor.AttemptsTo(Click.On(VendorPayeeMaintenanceLocator.Is1099Checkbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorPayeeMaintenanceLocator.Is1099DropDown, "Yes"));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(VendorPayeeMaintenanceLocator.IRSNameControl, vendor));
            }

           _featureContext[StepConstants.PayeeNameContext] = payeeName;
        }

        [Then(@"I add payee bank child form")]
        public void ThenIAddPayeeBankChildForm()
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UnitLocalAccount, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I search the payee maintenance")]
        public void WhenISearchThePayeeMaintenance()
        {
            var payeeNumber = _featureContext[StepConstants.VendorPayeeNumberContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.PayeeMaintenance));

            _actor.AttemptsTo(QuickFind.Search(payeeNumber));
        }


        [When(@"I search the vendor/payee maintenance")]
        public void WhenISearchTheVendorPayeeMaintenance()
        {
            var vendorPayeeNumber = _featureContext[StepConstants.VendorPayeeNumberContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorPayeeMaintenance));

            _actor.AttemptsTo(QuickFind.Search(vendorPayeeNumber));
        }

        [Given(@"I navigate to the Payee maintenance process")]
        public void GivenINavigateToThePayeeMaintenanceProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PayeeMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Given(@"I reopen an existing Payee")]
        public void GivenIReopenAnExistingPayee()
        {
            var payee = _featureContext[StepConstants.Payee].ToString();
            _actor.AttemptsTo(QuickFind.Search(payee));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add a new AP Contact info")]
        public void GivenIAddANewAPContactInfo(Table table)
        {
            var payeeEntity = table.CreateInstance<PayerEntity>();
            payeeEntity.Email = table.Rows[0]["Email"] + "@payee.com";
            _featureContext[StepConstants.PayeeNameContext] = payeeEntity;

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Payee));
            _actor.AttemptsTo(EnterAPContactInfo.With(payeeEntity));
        }

        [Then(@"the AP details should be saved correctly")]
        public void ThenTheAPDetailsShouldBeSavedCorrectly()
        {
            var payee = _featureContext[StepConstants.Payee].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.PayeeMaintenance));
            _actor.AttemptsTo(QuickFind.Search(payee));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var payerEntity = (PayerEntity)_featureContext[StepConstants.PayeeNameContext];
            var actualPayee = _actor.AsksFor(GetAPContactInfo.Data());

            actualPayee.ContactName.Should().Contain(payerEntity.FirstName + " " + payerEntity.LastName);
            actualPayee.ContactType.Should().BeEquivalentTo(payerEntity.ContactType);
            actualPayee.Email.Should().BeEquivalentTo(payerEntity.Email);

            _actor.AttemptsTo(ChildProcessMenu.ClickOn("AP Contact", ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Then(@"I verify the sections in payee maintenance")]
        public void ThenIVerifyTheSectionsInPayeeMaintenance()
        {
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.PayeeName).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.PayeeNum).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.PayeeType).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.PayeeStatus).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.VoucherStatus).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.PaymentTerms).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.Office).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.Unit).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.Currency).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayeeBank));
            _actor.AsksFor(Field.IsAvailable(VendorPayeeMaintenanceLocator.PayeeBank)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(VendorPayeeMaintenanceLocator.PayeeAccount)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(VendorPayeeMaintenanceLocator.PayeeEEOC)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(VendorPayeeMaintenanceLocator.APContact)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I run the report searching by payee number")]
        public void WhenIRunTheReportSearchingByPayeeNumber(Table table)
        {
            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            var payeeNumber = _featureContext[StepConstants.Payee].ToString();

            foreach (var col in searchCriteriaCol)
            {
                col.SearchValue = payeeNumber;
            }

            _actor.AttemptsTo(Click.On(VendorPayeeMaintenanceLocator.PayeePredicate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(AdvancedFindLookup.GetSearchResults(searchCriteriaCol));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the AU supplier data audit report values")]
        public void ThenIVerifyTheAUSupplierDataAuditReportValues()
        {
            var payeeBankEntity = (PayeeBankEntity)_featureContext[StepConstants.UpdatedPayeeBankContext];
            var oldPayeeBankEntity = (PayeeBankEntity)_featureContext[StepConstants.PayeeBankContext];
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.AUSupplierDataRecord("Description", "Before", oldPayeeBankEntity.Description)).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.AUSupplierDataRecord("Description", "After", payeeBankEntity.Description)).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.AUSupplierDataRecord("Account Number", "Before", oldPayeeBankEntity.AccountNumber)).Should().Be(true);
            _actor.DoesElementExist(VendorPayeeMaintenanceLocator.AUSupplierDataRecord("Account Number", "After", payeeBankEntity.AccountNumber)).Should().Be(true);
        }

        [Given(@"I add tax certification date to the payee")]
        public void GivenIAddTaxCertificationDateToThePayee(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.TaxCertificateDate, vendorEntity.TaxCertificateDate));
        }

    }
}

