using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Entity.FeeEarnerMaintenance;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Customer;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GetRateTest;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.MatterGlobalChange;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class AddNewFeeEarnerStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public AddNewFeeEarnerStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to Fee Earner Detail page")]
        public void GivenINavigateToFeeEarnerDetailPage()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerDetail, false));
            _actor.AttemptsTo(Click.On(FeeEarnerMaintenanceLocator.FeeReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I input the FE number")]
        public void GivenIInputTheFENumber()
        {
            var feeEarner = _featureContext[StepConstants.FeeEarnerAssignedNumber].ToString();
            _actor.AttemptsTo(SendKeys.To(FeeEarnerMaintenanceLocator.FENumberInput, feeEarner));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I run the report")]
        public void GivenIRunTheReport()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can verify that all the information is correct")]
        public void ThenICanVerifyThatAllTheInformationIsCorrect()
        {
            var office = _featureContext[StepConstants.OfficeConfig].ToString();

            List<string> results = _actor.GetElementTextList(FeeEarnerMaintenanceLocator.ReportList);
            results.Exists(x => x.Equals(office)).Should().BeTrue();
        }

        [Given(@"I navigate to Fee Earner page and I select an existing Fee Earner")]
        public void GivenINavigateToFeeEarnerPageAndISelectAnExistingFeeEarner()
        {
            var feeEarner = _featureContext[StepConstants.FeeEarnerAssignedNumber].ToString();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(feeEarner));
        }

        [Given(@"I amend the applicable name and office")]
        public void GivenIAmendTheApplicableNameAndOffice(Table table)
        {
            _actor.AttemptsTo(Click.On(FeeEarnerMaintenanceLocator.DisplayName));
            _actor.AttemptsTo(SendKeys.To(FeeEarnerMaintenanceLocator.DisplayName, table.Rows[0][ColumnNames.DisplayName]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.FeeEarnerDisplayName] = table.Rows[0][ColumnNames.DisplayName];

            var office = table.Rows[0][ColumnNames.Office];
            if (!string.IsNullOrEmpty(office))
            {
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EffectiveDatedInformation));
                _featureContext[StepConstants.UpdatedOffice] = office;
                _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.FindInputElementUsingText(LocatorConstants.Office), office));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }

        [Given(@"I navigate to the Matter Global Change process")]
        public void GivenINavigateToTheMatterGlobalChangeProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterGlobalChange, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I populate the required fields")]
        public void WhenIPopulateTheRequiredFields()
        {
            var matterNo = _featureContext[StepConstants.MatterNumberContext].ToString();
            var feeEarnerNo = _featureContext[StepConstants.FeeEarnerAssignedNumber].ToString();
            var office = _featureContext[StepConstants.UpdatedOffice].ToString();

            _actor.AttemptsTo(Click.On(MatterGlobalChange.RequestedMatterSearch));
            _actor.AttemptsTo(SendKeys.To(MatterGlobalChange.MatterNumber, matterNo));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.Submitdialog));

            _actor.AttemptsTo(SendKeys.To(MatterGlobalChange.BillingTimeKeeper, feeEarnerNo));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.FindInputElementUsingText(LocatorConstants.Office), office));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I click the Calculate button")]
        public void WhenIClickTheCalculateButton()
        {
            _actor.scrollToElementInView(MatterGlobalChange.CalculateButton);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(MatterGlobalChange.CalculateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var count = _actor.GetElementText(MatterGlobalChange.CalculateDiv);
            count.Should().BeEquivalentTo("1");
        }

        [Then(@"I click the Preview button")]
        public void ThenIClickThePreviewButton()
        {
            _actor.AttemptsTo(Click.On(MatterGlobalChange.PreviewButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var isVisible = _actor.DoesElementExist(MatterGlobalChange.MatterListing);
            isVisible.Should().BeTrue();

            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ParticleCLose));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Given(@"I view the fee earner")]
        public void GivenIViewTheFeeEarner()
        {
            var feeEarner = _featureContext[StepConstants.FeeEarnerAssignedNumber].ToString();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(feeEarner));
        }

        [Then(@"I submit the fee earner")]
        public void ThenISubmitTheFeeEarner()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var feeearner = message.Split(" ")[5];

            _featureContext[StepConstants.FeeEarnerAssignedNumber] = feeearner;
        }

        [Given(@"I verify that the timekeeper is assigned to the correct entity")]
        public void GivenIVerifyThatTheTimekeeperIsAssignedToTheCorrectEntity()
        {
            var actualFENumber = _actor.GetElementText(FeeEarnerMaintenanceLocator.FENumber);
            var expectedFENumber = _featureContext[StepConstants.FeeEarnerAssignedNumber].ToString();
            actualFENumber.Should().BeEquivalentTo(expectedFENumber);
        }

        [When(@"I navigate to the GetRate Test process")]
        public void WhenINavigateToTheGetRateTestProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.GetRateTest, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I fill in the GetRate Test required fields")]
        public void WhenIFillInTheGetRateTestRequiredFields(Table table)
        {
            var feeEarnerEntity = table.CreateInstance<FeeEarnerEntity>();
            string matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var feeEarner = _featureContext[StepConstants.FeeEarnerAssignedNumber].ToString();

            _actor.AttemptsTo(DateControl.SelectDate("Work Date", feeEarnerEntity.WorkDate));
            _actor.AttemptsTo(SendKeys.To(GetRateTestLocators.TimeKeeper, feeEarner));
            _actor.AttemptsTo(SendKeys.To(GetRateTestLocators.Matter, matterNumber));
            _actor.AttemptsTo(SendKeys.To(GetRateTestLocators.TransactionType, feeEarnerEntity.TransactionType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I click Get Rate button")]
        public void WhenIClickGetRateButton()
        {
            _actor.AttemptsTo(Click.On(GetRateTestLocators.GetRateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I verify that all information is correct following the HR change request")]
        public void WhenIVerifyThatAllInformationIsCorrectFollowingTheHRChangeRequest(Table table)
        {
            _actor.scrollToElementInView(GetRateTestLocators.MessageLog);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var expectedFeeEarnerDisplayName = _featureContext[StepConstants.FeeEarnerDisplayName].ToString();
            var actualFeeEarnerDisplayName = _actor.GetElementText(GetRateTestLocators.FeeEarnerName);

            var actualMessageLog = _actor.GetDriver().FindElement(GetRateTestLocators.MessageLog.Query).GetAttribute("value");
            var feeEarnerEffectiveDatedInfoEntity = (EffectiveDatedInformationEntity)_featureContext[StepConstants.FeeEarnerEffectiveDatedInformation];
            var defaultRate = _featureContext[StepConstants.DefaultRate].ToString();

            actualFeeEarnerDisplayName.Should().BeEquivalentTo(expectedFeeEarnerDisplayName);
            actualMessageLog.Should().Contain("Office: " + feeEarnerEffectiveDatedInfoEntity.Office);
            actualMessageLog.Should().Contain("Title: " + feeEarnerEffectiveDatedInfoEntity.Title);
            actualMessageLog.Should().Contain("Department: " + table.Rows[0][ColumnNames.Department]);
            actualMessageLog.Should().Contain("Section: " + table.Rows[0][ColumnNames.Section]);
            actualMessageLog.Should().Contain("Rate Class: " + table.Rows[0][ColumnNames.RateClass]);
            actualMessageLog.Should().Contain("Standard Rate: " + defaultRate);
            actualMessageLog.Should().Contain("WorkDate: " + table.Rows[0][ColumnNames.WorkDate]);

        }

        [Given(@"I navigate to the Matter DrillDown process")]
        public void GivenINavigateToTheMatterDrillDownProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterDrillDown, false));
            _actor.AttemptsTo(Click.On(FeeEarnerMaintenanceLocator.MatterDRillDown));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I populate the Matter Number")]
        public void ThenIPopulateTheMatterNumber()
        {
            var matterNo = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(SendKeys.To(MatterGlobalChange.MatterDrillNumber, matterNo));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can run the report")]
        public void ThenICanRunTheReport()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can verify that the original office and the updated office are displayed")]
        public void ThenICanVerifyThatTheOriginalOfficeAndTheUpdatedOfficeAreDisplayed()
        {
            var expectednewOffice = _featureContext[StepConstants.UpdatedOffice].ToString();
            var expectedoldOffice = _featureContext[StepConstants.OfficeConfig].ToString();

            //_actor.scrollToElementInView(FeeEarnerMaintenanceLocator.NewOfficeName);
            //List<string> actualResults = _actor.GetElementTextList(FeeEarnerMaintenanceLocator.MessgaeLogText);
            _actor.scrollToElementInView(FeeEarnerMaintenanceLocator.WithholdingTaxAttribute);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.scrollToElementInView(FeeEarnerMaintenanceLocator.StartDateLabel);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var actualnewOffice = _actor.GetElementText(FeeEarnerMaintenanceLocator.NewOfficeName);
            var actualoldOffice = _actor.GetElementText(FeeEarnerMaintenanceLocator.OldOfficeName);

            actualnewOffice.Should().BeEquivalentTo(expectednewOffice);
            actualoldOffice.Should().BeEquivalentTo(expectedoldOffice);
            //actualResults.Exists(x => x.Equals(expectednewOffice)).Should().BeTrue();
            //actualResults.Exists(x => x.Equals(expectedoldOffice)).Should().BeTrue();

        }
       
    }
}
