using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Delegation;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Delegation;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators;
using System.Linq;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Audit;
using OpenQA.Selenium;
using FluentAssertions;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    internal class DelegateSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public DelegateSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I remove existing workflow delegation records")]
        public void GivenIRemoveExistingWorkflowDelegationRecords(Table table)
        {
            var userNameList = table.Rows.Select(r => r["UserName"]).ToList();

            foreach (var userName in userNameList)
            {
                _actor.AttemptsTo(SearchProcess.ByName(StepConstants.WorkflowDelegation, false));
                _actor.AttemptsTo(QuickFind.Search(userName));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                //Selecting correct option if result list is shown
                if (_actor.DoesElementExist(AuditLocators.FindResultRows, 5))
                {
                    var resultList = _actor.FindAll(AuditLocators.FindResultRows);
                    var row = resultList.FirstOrDefault(x => x.FindElement(By.XPath(".//div[@col-id='NxFwkUserRel.BaseUserName']")).Text.Equals(userName));
                    row.Should().NotBeNull();

                    row.FindElement(By.XPath(".//input[@type='checkbox']")).Click();
                    _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
                    _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)), IsEqualTo.False());
                }

                if(_actor.DoesElementExist(UpdateDelegationRightsLocator.NoRecordsSearchResult,5))
                {
                    _actor.AttemptsTo(Click.On(UpdateDelegationRightsLocator.CloseSearchResults));
                    _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
                    _actor.WaitsUntil(Appearance.Of(CommonLocator.Cancel), IsEqualTo.False());
                    continue;
                }

                _actor.WaitsUntil(Appearance.Of(AuditLocators.ArchitypeAddButton("Delegation", "Delete")), IsEqualTo.True());
                _actor.AttemptsTo(Click.On(AuditLocators.ArchitypeAddButton("Delegation", "Delete")));
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
                _actor.WaitsUntil(Appearance.Of(CommonLocator.Submit), IsEqualTo.False());
            }
        }


        [When(@"I add the update delegation rights and grant all workflows")]
        [When(@"I add the update delegation rights and grant some workflows")]
        [When(@"I add the update delegation rights and revoke all workflows")]
        [When(@"I add the update delegation rights and revoke some workflows")]
        public void WhenIFillTheRelevantFields_UpdateDelegationRights(Table table)
        {
            var delegationEntity = table.CreateInstance<UpdateDelegationEntity>();

            _actor.AttemptsTo(SendKeys.To(UpdateDelegationRightsLocator.DelegateUserToGetRightsInput, delegationEntity.DelegationUserWithoutRoles));
            _actor.AttemptsTo(DateControl.SelectDate("Effective Date", delegationEntity.EffectiveDate));

            _actor.AttemptsTo(UpdateDelegationRights.With(delegationEntity));
        }

    }
}
