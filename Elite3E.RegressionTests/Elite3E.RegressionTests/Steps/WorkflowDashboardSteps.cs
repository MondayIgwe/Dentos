using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.RegressionTests.StepHelpers;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.WorkFlowDashBoard;
using FluentAssertions;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.Infrastructure.Selenium;
using System.Threading;
using System;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WorkFlowDashbord;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.Infrastructure.Entity;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    internal class WorkflowDashboardSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;


        public WorkflowDashboardSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I proxy as user '([^']*)'")]
        public void GivenIProxyAsUser(string user)
        {
            _featureContext[StepConstants.User] = user;
            _actor.AttemptsTo(ProxyAs.User(user));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
       
        [StepDefinition(@"I cancel proxy")]
        public void GivenICancelProxy()
        {
            _actor.AttemptsTo(ProxyUser.CancelProxy());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I validate user workflow rights have been added '([^']*)'")]
        [Then(@"I validate user workflow rights have been updated '([^']*)'")]
        public void ThenIValidateUserRightsHaveBeenAdded(string roles)
        {
            List<string> rolesList = roles.Split(',').Select(x => x.Trim()).ToList();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(WorkFlowDashBoardLocators.WorkflowInboxHeader()), IsEqualTo.True());

            //If there are no rights present, navigate to dash and back for it to refresh.
            if (!_actor.DoesElementExist(WorkFlowDashBoardLocators.WorkflowInboxRights()))
            {
                NavigateToDashboardAndBackToWorkFlowDashToRefresh(true);
            }

            _actor.WaitsUntil(Appearance.Of(WorkFlowDashBoardLocators.WorkflowInboxRights()), IsEqualTo.True());

            List<string> workFlowInboxList = _actor.GetDriver().FindElements(WorkFlowDashBoardLocators.WorkflowInboxRights().Query).Select(x => x.Text.Trim()).ToList();

            if (workFlowInboxList.Count != rolesList.Count)
            {
                int timeout = 3;
                int counter = 0;

                while (counter < timeout)
                {
                    workFlowInboxList = null;

                    _actor.AttemptsTo(Click.On(CommonLocator.SideNavMenuButtons("dashboard")));
                    _actor.AttemptsTo(SearchProcess.ByName("Workflow Dashboard", false));
                    _actor.WaitsUntil(Appearance.Of(WorkFlowDashBoardLocators.WorkflowInboxRights()), IsEqualTo.True());

                    workFlowInboxList = _actor.GetDriver().FindElements(WorkFlowDashBoardLocators.WorkflowInboxRights().Query).Select(x => x.Text.Trim()).ToList();
                    if (workFlowInboxList.Count == rolesList.Count)
                        break;

                    counter++;
                }
            }

            workFlowInboxList.Count().Should().Be(rolesList.Count);

            foreach (string role in rolesList)
            {
                workFlowInboxList.Any(x => x.Contains(role)).Should().BeTrue();
            }

        }

        [Then(@"I validate user workflow rights have been removed")]
        public void ThenIValidateUserRightsHaveBeenRemoved()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(WorkFlowDashBoardLocators.WorkflowInboxHeader()), IsEqualTo.True());

            if (_actor.DoesElementExist(WorkFlowDashBoardLocators.WorkflowInboxRights()))
            {
                NavigateToDashboardAndBackToWorkFlowDashToRefresh(false);
            }

            _actor.WaitsUntil(Appearance.Of(WorkFlowDashBoardLocators.WorkflowInboxRights()), IsEqualTo.False());
        }

        [StepDefinition(@"I click on workflow inbox option '([^']*)'")]
        public void ThenIClickOnWorkflowInboxOption(string option)
        {
            _actor.WaitsUntil(Appearance.Of(WorkFlowDashBoardLocators.WorkflowInboxRights()), IsEqualTo.True());
            var ele = _actor.FindAll(WorkFlowDashBoardLocators.WorkflowInboxRights()).Where(x => x.Text.Equals(option)).FirstOrDefault();
            ele.Should().NotBeNull();
            ele.Click();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [StepDefinition(@"I click open on workflow inbox timekeeper leaver and at section '([^']*)'")]
        public void ThenIClickOpenOnWorkflowInboxTimekeeperLeaverAndAtSection(string checkListHeader)
        {
            string feeEarner = _featureContext[StepConstants.FeeEarnerName].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(feeEarner, checkListHeader));
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Record(feeEarner)), IsEqualTo.True());
            _actor.AttemptsTo(Click.On(CommonLocator.Record(feeEarner)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.InboxTimekeeperLeaverCheckedOpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I validate timekeeper is no longer present in workflow inbox")]
        public void ThenIValidateTimekeeperIsNoLongerPresentInWorkflowInbox()
        {
            string feeEarner = _featureContext[StepConstants.FeeEarnerName].ToString();
            _actor.WaitsUntil(Appearance.Of(WorkFlowDashBoardLocators.WorkflowInboxHeader()), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Record(feeEarner)), IsEqualTo.False());
        }

        [Given(@"I open the billing workflow task and send the invoice")]
        public void GivenIOpenTheBillingWorkflowTaskAndSendTheInvoice()
        {
            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.BillingWorkflowTask));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //filter by invoice number 
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.InvoiceNumberContext].ToString(), "Set Dispatch Date"));
            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.SetDispatchOpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Sent));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I open the Invoice Dispatch workflow task")]
        public void GivenIOpenTheInvoiceDispatchWorkflowTaske()
        {
            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.InvoiceDispatchWorkflowTask));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
    
        }

        [StepDefinition(@"I send the invoice routed to the timekeeper")]
        public void GivenISendTheInvoiceRoutedToTheTimekeeper()
        {
            //filter by invoice number 
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.InvoiceNumberContext].ToString(), "Route to Responsible Timekeeper"));
            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.RouteRTKOpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Sent));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [StepDefinition(@"I send the invoice routed to finance team")]
        public void GivenISendTheInvoiceRoutedToFinanceTeam()
        {
            //filter by invoice number 
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.InvoiceNumberContext].ToString(), "Route to Finance Support"));
            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.RouteFinanceOpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Sent));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [StepDefinition(@"I send the invoice routed to finance team when dispatch method not set")]
        public void GivenISendTheInvoiceRoutedToFinanceTeamWhenDispatchMethodNotSet()
        {
            //filter by invoice number 
            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.InvoiceNumberContext].ToString(), "Invoice Dispatch Method not set"));
            _actor.AttemptsTo(Click.On(WorkFlowDashBoardLocators.RouteNotSetFinanceOpenButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Sent));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        private void NavigateToDashboardAndBackToWorkFlowDashToRefresh(bool isElementSupposedToBePresent)
        {
            int timeout = 3;
            int counter = 0;

            while (counter < timeout)
            {
                _actor.AttemptsTo(Click.On(CommonLocator.SideNavMenuButtons("dashboard")));
                _actor.AttemptsTo(SearchProcess.ByName("Workflow Dashboard", false));
                _actor.WaitsUntil(Appearance.Of(WorkFlowDashBoardLocators.WorkflowInboxHeader()), IsEqualTo.True());

                if (isElementSupposedToBePresent)
                {
                    if (_actor.DoesElementExist(WorkFlowDashBoardLocators.WorkflowInboxRights()))
                        break;
                }
                else
                {
                    if (!_actor.DoesElementExist(WorkFlowDashBoardLocators.WorkflowInboxRights()))
                        break;
                }
                Thread.Sleep(TimeSpan.FromSeconds(10));
                counter++;
            }
        }
    }
}
