using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.UserRoleManagement;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WorkFlowDashbord;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Time;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UserRoleManagement;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class TimeModifySteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;


        public TimeModifySteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];

        }

        [When(@"I retrieve the current username")]
        public string WhenIRetrieveTheCurrentUsername()
        {
            _actor.AttemptsTo(Hover.Over(CommonLocator.ThreeEIcon));
            _actor.AttemptsTo(Click.On(CommonLocator.ThreeEIcon));
            _actor.WaitsUntil(Appearance.Of(TimeModifyLocators.User), IsEqualTo.True());

            string user = _actor.GetElementText(TimeModifyLocators.User.Query);
            user.Should().NotBeNullOrEmpty();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.CloseSideMenu));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _featureContext[StepConstants.LoggedInUser] = user;
            return user;
        }


        [StepDefinition(@"add a user fee earner map")]
        public void WhenAddAUserFeeEarnerMap()
        {
            var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();
            var feeEarnerName = _featureContext[StepConstants.FeeEarnerName].ToString();
            string user = WhenIRetrieveTheCurrentUsername();
            _actor.AttemptsTo(SearchProcess.ByName(Process.UserRoleManagement));
            _actor.AttemptsTo(QuickFind.Search(user));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.UserFeeEarnerMap));

            _actor.AttemptsTo(EnterUserFeeEarnerMapping.SearchAndSelectIfFound(feeEarner, feeEarnerName));

            var timeKeeperList = _actor.GetDriver().FindElements(TimeModifyLocators.UserFeeEarnerMapTimekeeperNumbers.Query).Select(x => x.Text);
            if (timeKeeperList.Any(x => x.Equals(feeEarner)))
            {
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
                return;
            }
            _actor.AttemptsTo(Click.On(UserRoleManagementLocators.CloseFeeEarnerMapChildForm));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UserFeeEarnerMap, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(Click.On(MatterLocator.WarningField));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TimeModifyLocators.FeeEarner, feeEarner));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [StepDefinition(@"add a user '([^']*)' fee earner")]
        public void AddAUserToFeeEarner(string username)
        {
            var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();
            var feeEarnerName = _featureContext[StepConstants.FeeEarnerName].ToString();
           
            _actor.AttemptsTo(SearchProcess.ByName(Process.UserRoleManagement));
            _actor.AttemptsTo(QuickFind.Search(username));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.UserFeeEarnerMap));

            _actor.AttemptsTo(EnterUserFeeEarnerMapping.SearchAndSelectIfFound(feeEarner, feeEarnerName));

            var timeKeeperList = _actor.GetDriver().FindElements(TimeModifyLocators.UserFeeEarnerMapTimekeeperNumbers.Query).Select(x => x.Text);
            if (timeKeeperList.Any(x => x.Equals(feeEarner)))
            {
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
                return;
            }
            _actor.AttemptsTo(Click.On(UserRoleManagementLocators.CloseFeeEarnerMapChildForm));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UserFeeEarnerMap, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(Click.On(MatterLocator.WarningField));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TimeModifyLocators.FeeEarner, feeEarner));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"I select the unbilled time card")]
        public void ThenISelectTheUnbilledTimeCard()
        {
            var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.WfTimeModifyForIndividuals, false));

            _actor.AttemptsTo(QuickFind.Search(feeEarner));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (_actor.DoesElementExist(TimeModifyLocators.TimeCardSearchResultCardNumbers))
            {
                int latestTimekeeperNumber = _actor.GetDriver().FindElements(TimeModifyLocators.TimeCardSearchResultCardNumbers.Query).Select(x => int.Parse(x.Text)).Max();
                //_actor.AttemptsTo(Checkbox.SetStatus(TimeModifyLocators.TimeCardSearchResultCheckBox(latestTimekeeperNumber.ToString()),true));
                _actor.AttemptsTo(Click.On(TimeModifyLocators.TimeCardSearchResultCheckBox(latestTimekeeperNumber.ToString())));
                _actor.AttemptsTo(Click.On(TimeModifyLocators.TimeCardSearchResultSelectButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                if (_actor.DoesElementExist(CommonLocator.InformationMessage, 10))
                {
                    var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
                    message.Should().NotBeNullOrEmpty();
                    message.Should().NotContain("Cannot add worklist records to this collection");
                }
            }

            _featureContext[StepConstants.TimeCard] = _actor.GetDriver().FindElement(TimeModifyLocators.TimeCard.Query).Text;

            _actor.WaitsUntil(Appearance.Of(TimeModifyLocators.InternalComments), IsEqualTo.True());
        }

        [Given(@"I enter an internal comment")]
        public void GivenIEnterAnInternalComment(Table table)
        {
            var timeModifyEntity = table.CreateInstance<TimeModifyEntity>();
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            driver.FindElement(TimeModifyLocators.InternalComments.Query).SendKeys(timeModifyEntity.InternalComment);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"I confirm the workflow is created for approval")]
        public void ThenIConfirmTheWorkflowIsCreatedForApproval()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.WorkflowDashboard, false));
            _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateEntry(StepConstants.TimeModifyWorkflow)), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ValidateEntry(StepConstants.TimeModifyWorkflow)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I select the open workflow")]
        public void GivenISelectTheOpenWorkflow()
        {
            var timeCard = _featureContext[StepConstants.TimeCard].ToString();

            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(timeCard, GlobalConstants.TimeModifyRequest));

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ValidateEntry(timeCard)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TimeModifyLocators.OpenWorkflow));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        // Remove this once fixed

        [When(@"I enter a time entry")]
        public void WhenEnterATimeEntry(Table table)
        {
            var timeModifyEntity = table.CreateInstance<TimeModifyEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            //var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            _actor.AttemptsTo(SearchProcess.ByName(Process.TimeModify));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.Matter, matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.FeeEarner, timeModifyEntity.FeeEarnerName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(TimeEntryLocator.TimeTypeDropDown, timeModifyEntity.TimeType));
            _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.Hours, timeModifyEntity.Hours));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TimeEntryLocator.InternalComments));
            driver.FindElement(TimeEntryLocator.Narrative.Query).SendKeys(timeModifyEntity.Narrative);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.PostAll));
            _actor.WaitsUntil(Appearance.Of(CommonLocator.PostAll), IsEqualTo.False());
        }


        [StepDefinition(@"I submit a time modify")]
        public void WhenSubmitATimeModify(Table table)
        {
            var timeModifyEntity = table.CreateInstance<TimeModifyEntity>();
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            //var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.TimeModify));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.Matter, matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.FeeEarner, timeModifyEntity.FeeEarnerName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(TimeEntryLocator.TimeTypeDropDown, timeModifyEntity.TimeType));
            _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.Hours, timeModifyEntity.Hours));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(TimeEntryLocator.InternalComments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            int attempts = 0;
            int max = 3;

            while (attempts < max)
            {
                _actor.GetDriver().FindElement(TimeEntryLocator.Narrative.Query).SendKeys(timeModifyEntity.Narrative);
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                var currentValue = _actor.GetDriver().FindElement(TimeEntryLocator.Narrative.Query).FindElement(By.XPath(".//p")).Text;
                if (currentValue.Contains(timeModifyEntity.Narrative))
                {
                    break;
                }
                attempts++;
            }

            //work Rate 
            _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.WorkRate, timeModifyEntity.WorkRate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(timeModifyEntity.WorkAmount))
            {
                _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.WorkAmount, timeModifyEntity.WorkAmount));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(timeModifyEntity.WorkCurrency))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(TimeEntryLocator.WorkCurrencyDropDown, timeModifyEntity.WorkCurrency));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(timeModifyEntity.TaxCode))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("TaxCode", timeModifyEntity.TaxCode));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            //Validate Work Hours
            if (!string.IsNullOrEmpty(timeModifyEntity.Hours))
            {// IF this is failing, check to make sure you're not entering WorkAmount as well as Work Hours
                var hrs = _actor.GetElementText(TimeEntryLocator.Hours);
                hrs.Should().Contain(timeModifyEntity.Hours);
            }

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Submit), IsEqualTo.False());
        }

        [Then(@"the changes are reflected")]
        public void ThenTheChangesAreReflected(Table table)
        {
            var timeModifyEntity = table.CreateInstance<TimeModifyEntity>();
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var timeCard = _featureContext[StepConstants.TimeCard].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.WfTimeModifyForIndividuals, false));
            _actor.AttemptsTo(QuickFind.Search(timeCard));

            var expectedText = driver.FindElement(TimeModifyLocators.InternalComments.Query).GetAttribute("value");
            expectedText.Should().BeEquivalentTo(timeModifyEntity.InternalComment);

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Close));
        }

        [When(@"I select the purge type")]
        public void WhenISelectThePurgeType(Table table)
        {
            var timeModifyEntity = table.CreateInstance<TimeModifyEntity>();

            _actor.AttemptsTo(Dropdown.SelectOptionByName(TimeModifyLocators.PurgeType, timeModifyEntity.PurgeType));
            _actor.WaitsUntil(Appearance.Of(TimeModifyLocators.PurgeTypeReason), IsEqualTo.True());
            _actor.AttemptsTo(SendKeys.To(TimeModifyLocators.PurgeTypeReason, timeModifyEntity.PurgeTypeReason));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I edit a narrative")]
        public void WhenIEditANarrative(Table table)
        {
            var timeModifyEntity = table.CreateInstance<TimeModifyEntity>();
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            driver.FindElement(TimeModifyLocators.GetNarrative.Query).Clear();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            driver.FindElement(TimeEntryLocator.Narrative.Query).SendKeys(timeModifyEntity.Narrative);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"the change is reflected")]
        public void ThenTheChangeIsReflected(Table table)
        {
            var timeModifyEntity = table.CreateInstance<TimeModifyEntity>();
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var timeCard = _featureContext[StepConstants.TimeCard].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.WfTimeModifyForIndividuals, false));
            _actor.AttemptsTo(QuickFind.Search(timeCard));

            var expectedText = driver.FindElement(TimeModifyLocators.GetNarrative.Query).Text;
            expectedText.Should().BeEquivalentTo(timeModifyEntity.Narrative);

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Close));
        }

        [StepDefinition(@"remove a user fee earner map")]
        public void ThenRemoveAUserFeeEarnerMap()
        {
             var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();
            var feeEarnerName = _featureContext[StepConstants.FeeEarnerName].ToString();
            string user = WhenIRetrieveTheCurrentUsername();
            _actor.AttemptsTo(SearchProcess.ByName(Process.UserRoleManagement));
            _actor.AttemptsTo(QuickFind.Search(user));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.UserFeeEarnerMap));
            _actor.AttemptsTo(EnterUserFeeEarnerMapping.SearchAndSelectIfFound(feeEarner,feeEarnerName));
            
            _actor.AttemptsTo(Click.On(UserRoleManagementLocators.CloseFeeEarnerMapChildForm));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

           _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UserFeeEarnerMap, ChildProcessMenuAction.Delete));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
        [StepDefinition(@"remove a fee earner '([^']*)' from the user")]
        public void RemoveFeeEarnerFromUser(string username)
        {
            var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();
            var feeEarnerName = _featureContext[StepConstants.FeeEarnerName].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.UserRoleManagement));
            _actor.AttemptsTo(QuickFind.Search(username));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.UserFeeEarnerMap));
            _actor.AttemptsTo(EnterUserFeeEarnerMapping.SearchAndSelectIfFound(feeEarner, feeEarnerName));

            _actor.AttemptsTo(Click.On(UserRoleManagementLocators.CloseFeeEarnerMapChildForm));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UserFeeEarnerMap, ChildProcessMenuAction.Delete));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [StepDefinition(@"I quick search by matter number")]
        public void WhenIQuickSearchByMatterNumber()
        {
            _actor.AttemptsTo(QuickFind.Search(_featureContext[StepConstants.MatterNumberContext].ToString()));
        }

        [Then(@"I validate entry is posted by")]
        public void ThenIValidateTimeModifyPostedBy()
        {
            _actor.AttemptsTo(ScrollToElement.At(TimeModifyLocators.PostedBy));

            string user = _featureContext[StepConstants.LoggedInUser].ToString();
            string postedBy = _actor.GetElementText(TimeModifyLocators.PostedBy);
            postedBy.Should().BeEquivalentTo(user);
        }

        [Then(@"I verify the sections in disbursement modify")]
        public void ThenIVerifyTheSectionsInDisbursementModify()
        {
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkDate).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.DisbursementType).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.Matter).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.TimeKeeper).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkQuantity).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.DisbursementCurrency).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.RefCurrency).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkAmount).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WorkRate).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.RefRate).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WIPAmt).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WIPRate).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WIPQty).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.DisbursementDetailsNarrative).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.Language).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.Office).Should().Be(true);
            _actor.DoesElementExist(EntryAndModifyProcessLocators.WIPQty).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }



        [Then(@"I validate entry is posted by '([^']*)'")]
        public void ThenIValidateTimeModifyPostedBy(string proxyUser)
        {
            string user = _featureContext[StepConstants.LoggedInUser].ToString();
            string postedBy = _actor.GetElementText(TimeModifyLocators.PostedBy);

            string expectedValue = proxyUser + " (" + user + ")";
            postedBy.Should().BeEquivalentTo(expectedValue);
        }

        [When(@"I locate the submitted time modify")]
        public void WhenILocateTheSubmittedTimeModify()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.TimeModify));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
        }
        [When(@"I amend the work amount in time modify to '([^']*)'")]
        public void WhenIAmendTheWorkAmountInTimeModifyTo(string amount)
        {
            _actor.AttemptsTo(SendKeys.To(TimeEntryLocator.WorkAmount, amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
