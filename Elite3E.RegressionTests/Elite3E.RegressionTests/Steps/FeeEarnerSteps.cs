using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Entity.FeeEarnerMaintenance;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Delegation;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.FeeEarner;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class FeeEarnerSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public FeeEarnerSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"add a fee earner")]
        [Given(@"I add a fee earner")]
        public void GivenAddAFeeEarner(Table table)
        {
            var feeEarnerEntity = table.CreateInstance<FeeEarnerMaintenanceEntity>();
            var entity = _featureContext[StepConstants.Entity].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));

            _actor.AttemptsTo(SendKeys.To(FeeEarnerLocators.Entity, entity));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(FeeEarnerLocators.Status, feeEarnerEntity.Status));

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EffectiveDatedInformation));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(FeeEarnerLocators.Office, feeEarnerEntity.Office));

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.FeeEarnerRates));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.FeeEarnerRates, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ValidateEntry(feeEarnerEntity.RateType)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.EffectiveDatedRates, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(FeeEarnerLocators.Currency, feeEarnerEntity.Currency));
            _actor.AttemptsTo(SendKeys.To(FeeEarnerLocators.DefaultRate, feeEarnerEntity.DefaultRate));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var feeEarner = message.Split(" ")[5];

            _featureContext[StepConstants.FeeEarner] = feeEarner;
        }

        [Then(@"I verify the sections in fee earner maintenance")]
        public void ThenIVerifyTheSectionsInFeeEarnerMaintenance()
        {
            _actor.DoesElementExist(FeeEarnerLocators.Entity).Should().Be(true);
            _actor.DoesElementExist(FeeEarnerLocators.DisplayName).Should().Be(true);
            _actor.DoesElementExist(FeeEarnerLocators.Status).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EffectiveDatedInformation));
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.EffectiveDatedInformation)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.PartnerPoints)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.FeeEarnerRates)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.FeeEarnerPracticeGroup)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.Teams)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.FeeEarnerSchool)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.FeeEarnerAccreditation)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.FeeEarnerGLNaturalAccounts)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.HRData)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.FTEData)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.MaskOverrideValues)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.FeeEarnerObjective)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.FeeEarnerNotes)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(FeeEarnerLocators.UDF)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        //Need to be revisted and make this step redundant
        [StepDefinition(@"I '([^']*)' a workflow user '([^']*)' to fee earner '([^']*)'")]
        public async Task ThenIAWorkflowUserToFeeEarner(string addOrRemove, string FeeEarnerUser, string FeeEarnerName)
        {
            string username = !string.IsNullOrEmpty(FeeEarnerUser) ? FeeEarnerUser : await new UserRoleData().GetLoggedInUserName();
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));
            _actor.AttemptsTo(QuickFind.Search(FeeEarnerName));
            _actor.WaitsUntil(Appearance.Of(FeeEarnerLocators.PageTitle), IsEqualTo.True());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(ScrollToElement.At(FeeEarnerLocators.WorkflowUserLookup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            AddOrRemoveWorkflowUser(addOrRemove, username);
        }

        //Need to be revisted and make this step redundant
        [StepDefinition(@"I '([^']*)' a workflow user '([^']*)' to fee earner maintenance '([^']*)'")]
        public async Task ThenIAWorkflowUserToFeeEarnerMaintenanceAsync(string addOrRemove, string FeeEarnerUser, string FeeEarnerName)
        {
            string username = !string.IsNullOrEmpty(FeeEarnerUser) ? FeeEarnerUser : await new UserRoleData().GetLoggedInUserName();
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));
            _actor.AttemptsTo(QuickFind.Search(FeeEarnerName));
            _actor.WaitsUntil(Appearance.Of(FeeEarnerLocators.PageTitle), IsEqualTo.True());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(ScrollToElement.At(FeeEarnerLocators.WorkflowUserLookup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            AddOrRemoveWorkflowUser(addOrRemove, username);
        }

        [StepDefinition(@"I add a workflow user to a FeeEarner")]
        public void ThenIAddAWorkflowUserToAFeeEarner(Table table)
        {
            var feeEarner = table.CreateInstance<FeeEarnerEntity>();
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));
            _actor.AttemptsTo(QuickFind.Search(feeEarner.Name));
            _actor.WaitsUntil(Appearance.Of(FeeEarnerLocators.PageTitle), IsEqualTo.True());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            string currentVal = _actor.AsksFor(ValueAttribute.Of(FeeEarnerLocators.WorkflowUserLookup));
            if (string.IsNullOrEmpty(currentVal))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Workflow User", feeEarner.Name));
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        private void AddOrRemoveWorkflowUser(string addOrRemove, string username)
        {
            if (addOrRemove.Equals("Add", System.StringComparison.CurrentCultureIgnoreCase))
            {
                string currentVal = _actor.GetElementText(FeeEarnerLocators.WorkflowUserLookup);
                if (string.IsNullOrEmpty(currentVal))
                {
                    _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Workflow User", username));
                }
            }
            else
            {
                _actor.AttemptsTo(Clear.On(FeeEarnerLocators.WorkflowUserLookup));
                _actor.PressKeyWithActions("tab");
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.GetElementText(FeeEarnerLocators.WorkflowUserLookup).Should().BeEmpty();
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            Thread.Sleep(TimeSpan.FromSeconds(10));
            //This change doesn't seem to affect immediately.
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Submit), IsEqualTo.False());

        }

        [StepDefinition(@"I remove workflow user from fee earner")]
        public async Task ThenIRemoveWorkflowUserFromFeeEarner(Table table)
        {
            string username = !string.IsNullOrEmpty(table.Rows[0][ColumnNames.FeeEarnerUser]) ? table.Rows[0][ColumnNames.FeeEarnerUser] : await new UserRoleData().GetLoggedInUserName();

            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));

            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            searchCriteriaCol[0].SearchValue = username;

            _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteriaCol));
            if (_actor.DoesElementExist(UpdateDelegationRightsLocator.NoRecordsSearchResult, 3))
            {
                if (_actor.DoesElementExist(CommonLocator.Close))
                    _actor.AttemptsTo(Click.On(CommonLocator.Close));
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                return;
            }

            _actor.FindAll(CommonLocator.GetAllTheRowsFromSearchResults, 20)[0].Click();
            _actor.AttemptsTo(Click.On(CommonLocator.LookupSelectButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            AddOrRemoveWorkflowUser("remove", username);

        }
        [Given(@"I update the fee earner email body template")]
        public void GivenIUpdateTheFeeEarnerEmailBodyTemplate(Table table)
        {
            var feeEarnerEntity = table.CreateInstance<FeeEarnerMaintenanceEntity>();
            var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));
            _actor.AttemptsTo(QuickFind.Search(feeEarner));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CoverLetterNarrative));
             if(_actor.DoesElementExist(FeeEarnerLocators.CoverLetterNarrativeLanguage))
            {
                _actor.AttemptsTo(Dropdown.SelectOptionByName(FeeEarnerLocators.CoverLetterNarrativeLanguage, feeEarnerEntity.Language));
                _actor.GetDriver().FindElement(FeeEarnerLocators.EnteredTimeKeeperCoverLetterNarrative.Query).Clear();
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.GetDriver().FindElement(FeeEarnerLocators.TimeKeeperCoverLetterNarrative.Query).SendKeys(feeEarnerEntity.CoverLetterNarrative);
            }
            else
            {
                _actor.AttemptsTo(ChildProcessMenu.ClickOn("Cover Letter Narrative", ChildProcessMenuAction.Add));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Dropdown.SelectOptionByName(FeeEarnerLocators.CoverLetterNarrativeLanguage, feeEarnerEntity.Language));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.GetDriver().FindElement(FeeEarnerLocators.TimeKeeperCoverLetterNarrative.Query).SendKeys(feeEarnerEntity.CoverLetterNarrative);
            }
            
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Submit), IsEqualTo.False());

        }

        [Given(@"I remove the email body in fee earner maintenance")]
        public void GivenIRemoveTheEmailBodyInFeeEarnerMaintenance()
        {
            var feeEarner = _featureContext[StepConstants.FeeEarner].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));
            _actor.AttemptsTo(QuickFind.Search(feeEarner));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CoverLetterNarrative));
            if(_actor.DoesElementExist((FeeEarnerLocators.CoverLetterNarrativeLanguage)))
            {
                _actor.AttemptsTo(ChildProcessMenu.ClickOn("Cover Letter Narrative", ChildProcessMenuAction.Delete));
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            }
            else
            {
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }

        [StepDefinition(@"I '([^']*)' the '([^']*)' to the fee earner collaborator")]
        public void GivenITheToTheFeeEarnerCollaborator(string addOrRemove, string user)
        {
            var feeEarnerName = _featureContext[StepConstants.FeeEarnerName].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.FeeEarnerMaintenance));
            _actor.AttemptsTo(QuickFind.Search(feeEarnerName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (addOrRemove.Equals("Add", System.StringComparison.CurrentCultureIgnoreCase))
            {
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Workflow Collaborators", user));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else
            {
                string value = _actor.GetElementText(FeeEarnerLocators.WorkflowCollaboratorsInput);
                if (!string.IsNullOrEmpty(value))
                {
                    _actor.AttemptsTo(SendKeys.To(FeeEarnerLocators.WorkflowCollaboratorsInput, "" + Keys.Enter));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
            };

            _actor.AttemptsTo(Click.On(CommonLocator.Update));
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementContainsText(Process.FeeEarnerMaintenance)), IsEqualTo.False());
        }


    }
}
