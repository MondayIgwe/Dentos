using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.MatterGroupEnquiry;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System.Data;
using System.Linq;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class MatterGroupEnquirySteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public MatterGroupEnquirySteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
            
        }

        [StepDefinition(@"I search in matter group enquiry")]
        public void WhenISearchInMatterGroupEnquiry(Table table)
        {
            var searchPhrase = table.Rows.Select(r => r["SearchPhrase"]).ToList()[0];
            var searchValue = table.Rows.Select(r => r["SearchValue"]).ToList()[0];

            if(searchPhrase.Equals("Matter" , System.StringComparison.CurrentCultureIgnoreCase))
                searchValue = (string.IsNullOrEmpty(searchValue)) ? _featureContext[StepConstants.MatterNumberContext].ToString() : searchValue;

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle(searchPhrase, searchValue));
        }

        [Then(@"I validate matter notes in matter group enquiry")]
        public void ThenIValidateMatterNotesInMatterGroupEnquiry()
        {
            var notesEntity = (MatterNoteEntity)_featureContext[StepConstants.ClientMatterNotesContext];
            var matter = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.WaitsUntil(Appearance.Of(MatterGroupLocator.ClientInformationDropDown), IsEqualTo.True());
            _actor.AttemptsTo(Click.On(MatterGroupLocator.ClientInformationDropDown));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(MatterGroupLocator.ClientInformationDropDownOptions("Client/Matter Notes")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Hover.Over(MatterGroupLocator.ClientInformationFullScreen));
            _actor.AttemptsTo(JScript.ClickOn(MatterGroupLocator.ClientInformationFullScreen));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.FindAll(MatterGroupLocator.GridLoc).Count().Should().Be(1);
            DataTable dataTable = _actor.FindTable(MatterGroupLocator.GridLoc);            
            dataTable.Rows.Should().NotBeNull();

            //var Matrow = dataTable.Select("Matter= "+matter).FirstOrDefault();
            var Matrow = dataTable.AsEnumerable().Where(x => x["Matter"].Equals(matter)).FirstOrDefault();
            Matrow["Date Entered"].ToString().Should().BeEquivalentTo(notesEntity.DateEntered);
            Matrow["Note Type"].ToString().Should().BeEquivalentTo(notesEntity.NoteType);
            Matrow["Note"].ToString().Should().BeEquivalentTo(notesEntity.Note);
        }

        [Then(@"I validate the transaction entries for '([^']*)'")]
        public void ThenIValidateTheTransactionEntriesFor(string transactionType)
        {
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();
            _actor.AttemptsTo(Click.On(MatterGroupLocator.ARHistoryDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(MatterGroupLocator.TransactionEntry(transactionType, invoiceNumber)), IsEqualTo.True());
        }
    }
}
