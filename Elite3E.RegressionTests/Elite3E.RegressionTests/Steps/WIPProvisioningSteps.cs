using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WIPProvision;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class WIPProvisioningSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public WIPProvisioningSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the WIP process")]
        [When(@"I navigate to the WIP process")]
        public void GivenINavigateToTheWIPProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.WIPProvisioning, false));
        }

        [When(@"I add the WIP form required fields")]
        public void WhenIAddTheWIPFormRequiredFields(Table table)
        {
            var wipProvisionEntity = table.CreateInstance<WIPProvisionEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            wipProvisionEntity.Matter = matterNumber;
            _featureContext[StepConstants.WIPProvisioning] = wipProvisionEntity;
            _actor.AttemptsTo(EnterWIPProvisionData.With(wipProvisionEntity));
        }

        [Then(@"the new fields should be visible and populated on the WP Amounts child form")]
        public void ThenTheNewFieldsShouldBeVisibleAndPopulatedOnTheWPAmountsChildForm(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));

            var wipEntity = (WIPProvisionEntity)_featureContext[StepConstants.WIPProvisioning];
            _actor.AttemptsTo(Click.On(CommonLocator.Button(GlobalConstants.Load)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessView.SwitchToView("WP Amount", GlobalConstants.Grid));

            var actualWIP = _actor.AsksFor(GetWIPAmountData.Data());
            string expectedClient = table.Rows[0][ColumnNames.Clientname];
            string expectedCurrency = table.Rows[0][ColumnNames.Currency];
            string expectedOffice = table.Rows[0][ColumnNames.Office];

            actualWIP.Matter.Should().BeEquivalentTo(wipEntity.Matter);
            actualWIP.client.Should().Contain(expectedClient);
            actualWIP.currency.Should().BeEquivalentTo(expectedCurrency);
            actualWIP.office.Should().Contain(expectedOffice);
        }

        [Then(@"clicking the Export button")]
        public void ThenClickingTheExportButton()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Export));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the excel sheet is saved to my local machine")]
        public void ThenTheExcelSheetIsSavedToMyLocalMachine()
        {
            string fileName = "WIP Provisioning";
            bool isFIleDownloaded = _actor.AsksFor(DownloadFile.isFIleDownloaded(fileName));
            isFIleDownloaded.Should().Be(true);
        }


    }
}
