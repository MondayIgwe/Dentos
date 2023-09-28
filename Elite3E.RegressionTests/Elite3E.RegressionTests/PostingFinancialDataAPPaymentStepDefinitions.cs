using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;
using Elite3E.RegressionTests.StepHelpers;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class PostingFinancialDataAPPaymentStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        public PostingFinancialDataAPPaymentStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Then(@"I enter more vendor details")]
        public void ThenIEnterMoreVendorDetails(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();

            if (!string.IsNullOrEmpty(vendorEntity.VendorType))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.VendorTypeDropDown, vendorEntity.VendorType));
            if (!string.IsNullOrEmpty(vendorEntity.VendorCategory))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.VendorCategoryDropDown, vendorEntity.VendorCategory));         
        }
        [Then(@"I want to add a new site")]
        public void ThenIWantToAddANewSite(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();
            _actor.AttemptsTo(Click.On(VendorLocators.AddEditSiteButton("NewSite")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.Description, vendorEntity.Description + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.SiteType, vendorEntity.SiteType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VendorLocators.AddressStreet, vendorEntity.Street));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(PurgeLocator.OkButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [Then(@"I enter the voucher created previously")]
        public void ThenIEnterTheVoucherCreatedPreviously()
        {
            var vendor = _featureContext[StepConstants.VendorNameContext].ToString();
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Vendor", vendor));
        }

        [Then(@"I set status to approved")]
        public void ThenISetStatusToApproved()
        {
            _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.PayeeStatusDropDown, "Approved"));
        }

    }
}
