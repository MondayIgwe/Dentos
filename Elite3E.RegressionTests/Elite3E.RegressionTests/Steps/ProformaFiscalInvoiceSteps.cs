using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ProformaFiscalInvoiceSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ProformaFiscalInvoiceSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I open the proforma edit process and search for a matter")]
        public void WhenIOpenTheProformaEditProcessAndSearchForAMatter()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaEdit, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
        }


        //[When(@"I open the  Proforma edit process and search for a matter ""(.*)""")]
        //public void WhenIOpenTheProformaEditProcessAndSearchForAnMatter(string searchText)
        //{
        //    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        //    _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaEdit,false));
        //    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        //    _actor.AttemptsTo(QuickFind.Search(searchText));
        //}

        [When(@"I open the proforma edit & approval process and search for a matter")]
        public void WhenIOpenTheProformaEditApprovalProcessAndSearchForAMatter()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaEditApproval, false));
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
        }


        //[When(@"I open the   Proforma Edit & Approval  process and search for a matter ""(.*)""")]
        //public void WhenIOpenTheProformaEditApprovalProcessAndSearchForAMatter(string searchText)
        //{
        //    _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaEditApproval,false));
        //    _actor.AttemptsTo(QuickFind.Search(searchText));
        //}


        [Then(@"I can verify the Fiscal Invoice field is disabled")]
        public void ThenICanVerifyTheFiscalInvoiceFieldIsDisabled()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(Fiscal_InvoicingLocators.FiscalInvoicetext), IsEqualTo.True());
            // _actor.AsksFor(ValueAttribute.Of(GLRecieptsLocators.Gltypeinput)).Should().BeEquivalentTo(value);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var available = _actor.AsksFor(Field.IsAvailable(Fiscal_InvoicingLocators.FiscalInvoiceCheckBox));
            available.Should().BeFalse();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can verify the Fiscal Invoice field is enabled")]
        public void ThenICanVerifyTheFiscalInvoiceFieldIsEnabled()
        {
            _actor.WaitsUntil(Appearance.Of(Fiscal_InvoicingLocators.FiscalInvoicetext), IsEqualTo.True());
            // _actor.AsksFor(ValueAttribute.Of(GLRecieptsLocators.Gltypeinput)).Should().BeEquivalentTo(value);
            //_actor.AsksFor(EnabledState.Of(Fiscal_InvoicingLocators.FiscalInvoiceCheckBox));
            var available = _actor.AsksFor(Field.IsAvailable(Fiscal_InvoicingLocators.FiscalInvoiceCheckBox));
            available.Should().BeTrue();
        }


        [Then(@"I verify the warning '([^']*)' regarding the client approval")]
        public void ThenIVerifyTheWarningRegardingTheClientApproval(string message)
        {
            var warningMessage = _actor.GetElementText(ProformaEditLocator.BillSubjectToClientApprovalDiv);
            warningMessage.Should().ContainEquivalentOf(message);
        }

        [Then(@"I verify the warning '([^']*)' in proforma edit")]
        public void ThenIVerifyTheWarningInProformaEdit(string message)
        {
            var warningMessage = _actor.GetElementText(ProformaEditLocator.BillSubjectToClientApprovalDiv);
            warningMessage.Should().BeEquivalentTo(message);
        }


     
        [StepDefinition(@"I mark the cards as non-chargeable and ensure the bill amount reduces to '([^']*)'")]
        public void GivenIMarkTheCardsAsNon_ChargeableAndEnsureTheBillAmountReducesTo(string amount)
        {
            // Mark NC for disbursement card
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.DisbursementDetails));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.DisbursementDetails, GlobalConstants.FormFull));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ProformaEditChargeLocator.NoChargeCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(SelectedState.Of(ProformaEditChargeLocator.DisplayCheckbox)).Should().BeFalse();
            _actor.AsksFor(Text.Of(ProformaEditChargeLocator.DisbursementBillAmount)).Should().Contain(amount);
            // Mark NC for time card
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.FeeDetails));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.FeeDetails, GlobalConstants.FormFull));
            _actor.AttemptsTo(Click.On(ProformaEditLocator.FeeDetailsNoChargeCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(SelectedState.Of(ProformaEditLocator.FeeDetailsDisplayCheckbox)).Should().BeFalse();
            _actor.AsksFor(Text.Of(ProformaEditChargeLocator.FeeDetailsBillAmount)).Should().Contain(amount);
            // Mark NC for charge card
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ChargeDetails));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.ChargeDetails, GlobalConstants.FormFull));
            _actor.AttemptsTo(Click.On(CommonLocator.ViewLastRecordOnGrid(StepConstants.ChargeDetails)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ProformaEditDisbursementLocator.NoChargeCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(SelectedState.Of(ProformaEditDisbursementLocator.DisplayCheckbox)).Should().BeFalse();
            _actor.AsksFor(Text.Of(ProformaEditChargeLocator.ChargeBillAmount)).Should().Contain(amount);
        }


        [Given(@"I verify the narratives populated in proforma edit")]
        public void GivenIVerifyTheNarrativesPopulatedInProformaEdit()
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            var feeEarner = _featureContext[StepConstants.FeeEarnerName].ToString();
            var proformaCoverLetter = _featureContext[StepConstants.CoverLetterNarrative].ToString().Replace("@BillingTkprDisplayName@",feeEarner).Replace("@MatterNumber@", matter);
            var profornaInvoiceNarrative = _featureContext[StepConstants.InvoiceNarrative].ToString().Replace("@MatterNumber@",matter);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ScrollToElement.At(ProformaEditLocator.InvoiceNarrativeText));
            _actor.AsksFor(Text.Of(ProformaEditLocator.InvoiceNarrativeText)).Should().Contain(profornaInvoiceNarrative);
            _actor.AttemptsTo(ScrollToElement.At(ProformaEditLocator.CoverLetterNarrativeText));
            _actor.AsksFor(Text.Of(ProformaEditLocator.CoverLetterNarrativeText)).Should().Contain(proformaCoverLetter);
        }

        [StepDefinition(@"I verify the cover letter narrative")]
        public void GivenIVerifyTheCoverLetterNarrative()
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            var feeEarner = _featureContext[StepConstants.FeeEarnerName].ToString();
            var proformaCoverLetter = _featureContext[StepConstants.CoverLetterNarrative].ToString().Replace("@BillingTkprDisplayName@", feeEarner).Replace("@MatterNumber@", matter);
            _actor.AttemptsTo(ScrollToElement.At(ProformaEditLocator.CoverLetterNarrativeText));
            _actor.AsksFor(Text.Of(ProformaEditLocator.CoverLetterNarrativeText)).Should().Contain(proformaCoverLetter);
        }

        [Given(@"I verify the currency and bank details in proforma edit")]
        public void GivenIVerifyTheCurrencyAndBankDetailsInProformaEdit()
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            var account = _featureContext[StepConstants.RemittanceAccountContext].ToString();
            var presCurrency = _featureContext[StepConstants.PresCurrency].ToString();

            _actor.AttemptsTo(ScrollToElement.At(ProformaEditLocator.PresCurrency));
            _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.PresCurrency)).Should().Contain(presCurrency);
            if(_actor.AsksFor(Text.Of(ProformaEditLocator.PresCurrency)).Contains(presCurrency))
            {
                var presExchangeRate = _featureContext[StepConstants.PresExchangeRate].ToString();
                //open defect
                // _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.PresExchangeRate)).Should().Contain(presExchangeRate);
                _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.PresCurrency)).Should().Contain(presCurrency);
            }
            else
            {
                _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.PresCurrency)).Should().Contain(presCurrency);
                //open defect
               // _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.PresExchangeRate)).Should().NotBeNullOrEmpty();
            }
            if(!(string.IsNullOrEmpty(account)))
            {
                _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.AlternativeBankDetails)).Should().Contain(account);
            }
        }


        [When(@"I update the invoice type '([^']*)'")]
        public void WhenIUpdateTheInvoiceType(string invoiceType)
        {
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaEditLocator.InvoiceType, invoiceType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I verify the client and matter notes")]
        public void GivenIVerifyTheClientAndMatterNotes()
        {
            var matterNotes = _featureContext[StepConstants.MatterNote].ToString();
            var clientNotes = _featureContext[StepConstants.ClientNote].ToString();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientAndMatterNotes));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ProformaEditLocator.NoteTypeHeader));
            _actor.DoesElementExist(ProformaEditLocator.MatterNotes(matterNotes)).Should().BeTrue();
            _actor.DoesElementExist(ProformaEditLocator.ClientNotes(clientNotes)).Should().BeTrue();
            _actor.DoesElementExist(ProformaEditLocator.MatterNextActionOwner(matterNotes)).Should().BeTrue();
            _actor.DoesElementExist(ProformaEditLocator.ClientNextActionOwner(clientNotes)).Should().BeTrue();
            _actor.DoesElementExist(ProformaEditLocator.MatterNextActionDate(matterNotes)).Should().BeTrue();
            _actor.DoesElementExist(ProformaEditLocator.ClientNextActionDate(clientNotes)).Should().BeTrue();
        }

        [Given(@"I verify the client reference number")]
        public void GivenIVerifyTheClientReferenceNumber()
        {
            var clientReference = _featureContext[StepConstants.ClientReference].ToString();
            _actor.AttemptsTo(ScrollToElement.At(ProformaEditLocator.ClientReference));
            _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.ClientReference)).Should().Contain(clientReference);
        }

        [StepDefinition(@"I edit the proforma edit")]
        public void GivenIEditTheProformaEdit(Table table)
        {
            var proformaEntity = table.CreateInstance<ProformaGenerationEntity>();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.FeeDetails));
            if (!(string.IsNullOrEmpty(proformaEntity.PresCurrency)))
            {
                _featureContext[StepConstants.PresCurrency] = proformaEntity.PresCurrency;
                _featureContext[StepConstants.PresExchangeRate] = proformaEntity.PresExchangeRate;
                _actor.AttemptsTo(ScrollToElement.At(ProformaEditLocator.PresCurrency));
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaEditLocator.PresCurrency,proformaEntity.PresCurrency));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if(!(string.IsNullOrEmpty(proformaEntity.InvoiceType)))
            {
                _featureContext[StepConstants.InvoiceType] = proformaEntity.InvoiceType;
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaEditLocator.InvoiceType, proformaEntity.InvoiceType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if(!(string.IsNullOrEmpty(proformaEntity.BillingOffice)))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaEditLocator.BillingOffice, proformaEntity.BillingOffice));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!(string.IsNullOrEmpty(proformaEntity.FromTaxArea)))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ProformaEditLocator.FromTaxArea, proformaEntity.FromTaxArea));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!(string.IsNullOrEmpty(proformaEntity.AlternativeBankDetails)))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Alternative Bank Details", proformaEntity.AlternativeBankDetails));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }


        }

        [Then(@"I verify the details in proforma edit")]
        public void ThenIVerifyTheDetailsInProformaEdit(Table table)
        {
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            var feeEarner = _featureContext[StepConstants.FeeEarnerName].ToString();

            var proformaEntity = table.CreateInstance<ProformaGenerationEntity>();
            if(!(string.IsNullOrEmpty(proformaEntity.PresCurrency)))
            {
                _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.PresCurrency)).Should().Contain(proformaEntity.PresCurrency);
            }
            //defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/67253
            //if (!(string.IsNullOrEmpty(proformaEntity.PresExchangeRate)))
            //{
            //    _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.PresExchangeRate)).Should().Contain(proformaEntity.PresExchangeRate);
            //}

            if (!(string.IsNullOrEmpty(proformaEntity.CoverLetterNarrative)))
            {
                var proformaCoverLetter = proformaEntity.CoverLetterNarrative.Replace("@BillingTkprDisplayName@", feeEarner).Replace("@MatterNumber@", matter);
                _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.CoverLetterNarrativeText)).Should().Contain(proformaCoverLetter);
            }
            if (!(string.IsNullOrEmpty(proformaEntity.InvoiceNarrative)))
            {
                var profornaInvoiceNarrative = proformaEntity.InvoiceNarrative.Replace("@MatterNumber@", matter);
                _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.InvoiceNarrativeText)).Should().Contain(profornaInvoiceNarrative);
            }

            if (!(string.IsNullOrEmpty(proformaEntity.AlternativeBankDetails)))
            {
                _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.AlternativeBankDetails)).Should().Contain(proformaEntity.AlternativeBankDetails);
            }
            if (!(string.IsNullOrEmpty(proformaEntity.InvoiceType)))
            {
                _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.InvoiceType)).Should().Contain(proformaEntity.InvoiceType);
            }
            if (!(string.IsNullOrEmpty(proformaEntity.InvoiceDistributionMethod)))
            {
                _actor.AsksFor(ValueAttribute.Of(ProformaEditLocator.InvoiceDistributionMethod)).Should().Contain(proformaEntity.InvoiceDistributionMethod);
            }
        }

        [When(@"I bill the group proforma")]
        public void WhenIBillTheGroupProforma()
        {
            var billingGroup = _featureContext[StepConstants.BillingGroup].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.WFGroupProforma));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(billingGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(CommonLocator.BillGroup), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.BillGroup));
        }

    }
}
