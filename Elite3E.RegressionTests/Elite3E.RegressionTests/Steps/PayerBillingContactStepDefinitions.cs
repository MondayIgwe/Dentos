using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Payer;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;


namespace Elite3E.RegressionTests
{
    [Binding]
    public class PayerBillingContactStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public PayerBillingContactStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I add a new Central Billing Contact info")]
        public void GivenIAddANewCentralBillingContactInfo(Table table)
        {
            var payerEntity = table.CreateInstance<PayerEntity>();
            payerEntity.Email = table.Rows[0]["Email"] + "@payer.com";
            _featureContext[StepConstants.Payer] = payerEntity;

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Payer));
            _actor.AttemptsTo(EnterPayerCentralBillingContactData.With(payerEntity));
        }

        [StepDefinition(@"I reopen an existing Payer")]
        public void WhenIReopenAnExistingPayer()
        {
            string payer = _featureContext[StepConstants.PayerContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.Payor));
            _actor.AttemptsTo(QuickFind.Search(payer));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the details should be saved correctly")]
        public void ThenTheDetailsShouldBeSavedCorrectly()
        {
            string payer = _featureContext[StepConstants.PayerContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.Payor));
            _actor.AttemptsTo(QuickFind.Search(payer));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var payerEntity = (PayerEntity)_featureContext[StepConstants.Payer];
            var actualPayer = _actor.AsksFor(GetPayerCentralBillingContactBank.Data());
            string payerCentralBilling = "Central Billing Contacts";

            actualPayer.ContactName.Should().Contain(payerEntity.FirstName + " " + payerEntity.LastName);
            actualPayer.ContactType.Should().BeEquivalentTo(payerEntity.ContactType);
            actualPayer.Email.Should().BeEquivalentTo(payerEntity.Email);

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(payerCentralBilling, ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I update the payer tax information")]
        public void GivenIUpdateThePayerTaxInformation(Table table)
        {
            var payerEntity = table.CreateInstance<PayerEntity>();
            var site = _actor.GetElementText(PayerLocator.Site);

            _actor.AttemptsTo(SendKeys.To(PayerLocator.TaxIDOneInput, payerEntity.TaxIDOne));
            _actor.AttemptsTo(SendKeys.To(PayerLocator.TaxIDTwoInput, payerEntity.TaxIDTwo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //save tax info
            _featureContext[StepConstants.TaxID1] = payerEntity.TaxIDOne;
            _featureContext[StepConstants.TaxID2] = payerEntity.TaxIDTwo;
            _featureContext[StepConstants.Sites] = site;
        }

        [StepDefinition(@"I verify the payer information on a proforma")]
        public void ThenIVerifyThePayerInformationOnAProforma()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(ChildProcessView.SwitchToView("Proforma Payers", GlobalConstants.Form));
            var expectedTaxIDOne = _featureContext[StepConstants.TaxID1].ToString();
            var expectedTaxIDTwo = _featureContext[StepConstants.TaxID2].ToString();
            var expectedSite = _featureContext[StepConstants.Sites].ToString();
            var actualSite = _actor.GetElementText(PayerLocator.ProfPayerSite);
            var actualTaxIDOne = _actor.GetElementText(PayerLocator.TaxIDOneDiv).Trim();
            var actualTaxIDTwo = _actor.GetElementText(PayerLocator.TaxIDTwoDiv).Trim();

            expectedTaxIDOne.Should().BeEquivalentTo(actualTaxIDOne);
            expectedTaxIDTwo.Should().BeEquivalentTo(actualTaxIDTwo);
            expectedSite.Should().BeEquivalentTo(actualSite);

        }

        [Then(@"I verify the udf information is correct")]
        public void ThenIVerifyTheUdfInformationIsCorrect()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(ChildProcessView.SwitchToView("UDF", GlobalConstants.Form));
            var expectedUDFString = _featureContext[StepConstants.UDFDesc].ToString();
            var expectedUDFDate = _featureContext[StepConstants.UDFLabel].ToString();
            var actualUDFString = _actor.GetElementText(MatterLocator.UDFStringInput);
            var actualUDFDate = _actor.GetElementText(MatterLocator.UDFDateInput);

            expectedUDFString.Should().BeEquivalentTo(actualUDFString);
            expectedUDFDate.Should().BeEquivalentTo(actualUDFDate);

        }






    }
}
