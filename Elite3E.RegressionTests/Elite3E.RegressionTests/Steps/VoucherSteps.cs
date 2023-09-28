using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;
using Elite3E.RegressionTests.StepHelpers;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class VoucherSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public VoucherSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the voucher maintenance process")]
        public void GivenINavigateToTheVoucherMaintenanceProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.VoucherMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"I add a voucher direct form")]
        public void WhenIAddAVoucherDirectForm(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.VoucherDirectGL));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.VoucherDirectGL, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.VoucherDirectGL, GlobalConstants.Form));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VendorLocators.TaxCode, vendorEntity.VoucherGLTaxCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(VendorLocators.VoucherDirectGLAmount, vendorEntity.VoucherAmount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(VendorLocators.GLUnit, vendorEntity.GLUnit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VendorLocators.GLNatural, vendorEntity.GLNatural));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if(String.IsNullOrEmpty(vendorEntity.GLUnitLocal))
            {
                _actor.AttemptsTo(SendKeys.To(VendorLocators.GLUnitLocal, vendorEntity.GLUnitLocal));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            _actor.AttemptsTo(SendKeys.To(VendorLocators.GLDepartment, vendorEntity.GLDepartment));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VendorLocators.GLSection, vendorEntity.GLSection));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VendorLocators.GLOffice, vendorEntity.GLOffice));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VendorLocators.GLTimekeeper, vendorEntity.GLTimekeeper));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.DeTaxButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherDisbursementType, vendorEntity.DisbursementType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ProcessUpdateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
