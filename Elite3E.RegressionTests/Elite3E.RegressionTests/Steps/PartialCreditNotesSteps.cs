using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.PartialCreditNotes;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.OverrideSetSystemOptions;
using Elite3E.Infrastructure.Entity;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class PartialCreditNotesSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public PartialCreditNotesSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I verify the auto populated fields")]
        public void ThenIVerifyTheAutoPopulatedFields()
        {
            var expectedReceiptType = _featureContext[StepConstants.ReceiptTypeContext].ToString();
            var expectedProformaStatus = _featureContext[StepConstants.ProfStatusContext].ToString();
            var expectedAdjType = _featureContext[StepConstants.AdjustmentTypeDescriptionContext].ToString();

            var actualReceiptType = _actor.GetElementText(PartialCreditNotesLocators.ReceiptTypeInput);
            var actualProfStatus = _actor.GetElementText(PartialCreditNotesLocators.ProformaStatusInput);
            var actualAdjType = _actor.GetElementText(PartialCreditNotesLocators.CreditNoteAdjustmentTypeInput);

            actualReceiptType.Should().BeEquivalentTo(expectedReceiptType);
            actualProfStatus.Should().BeEquivalentTo(expectedProformaStatus);
            actualAdjType.Should().BeEquivalentTo(expectedAdjType);
        }

        [Then(@"the credit note options should be set to the override this")]
        public void ThenTheCreditNoteOptionsShouldBeSetToTheOverrideThis(Table table)
        {
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.GroupType("Billing")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards("Allow_Hidden_Cards")));
            _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards("OverrideProformaBillTemplate"), 6, "pagedown");
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var options in table.Rows)
            {
                var optionName = options[ColumnNames.OptionName];
                _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards(optionName), 6, "pagedown");
                _actor.DoesElementExist(OverrideSetSystemOptionsLocators.HiddenCards(optionName)).Should().BeTrue();
                _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards(optionName)));

                var defaultValue = _actor.GetElementText(PartialCreditNotesLocators.SystemDefaultDiv(options[ColumnNames.SystemDefault]));
                defaultValue.Should().BeEquivalentTo(options[ColumnNames.SystemDefault]);

                var optionDefaultValue = _actor.GetElementText(PartialCreditNotesLocators.ValueBasedOnColNOption(optionName, ColumnNames.UnitOverride));
                if (string.IsNullOrEmpty(optionDefaultValue))
                {
                    optionDefaultValue = _actor.GetElementText(PartialCreditNotesLocators.ValueBasedOnColNOption(optionName, ColumnNames.FirmOverrideCol));
                }

                if (optionName.Contains("Prof_ccc"))
                {
                    _featureContext[StepConstants.ProfStatusContext] = optionDefaultValue;
                }
                else if (optionName.Contains("Adj_ccc"))
                {
                    _featureContext[StepConstants.AdjustmentTypeContext] = optionDefaultValue;
                }
                else if (optionName.Contains("Receipt_ccc"))
                {
                    _featureContext[StepConstants.ReceiptTypeContext] = optionDefaultValue;
                }
            }
        }

        [Given(@"I add invoice to partial credit notes")]
        public void GivenIAddInvoiceToPartialCreditNotes()
        {
           var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Invoice", invoiceNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
