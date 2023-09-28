using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Delegation;
using Elite3E.Infrastructure.Constant;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Delegation
{
    public class UpdateDelegationRights : ITask
    {
        private UpdateDelegationRights(UpdateDelegationEntity updateDelegationEntity) =>
            UpdateDelegationEntity = updateDelegationEntity;

        public static UpdateDelegationRights With(UpdateDelegationEntity updateDelegationEntity) =>
            new(updateDelegationEntity);

        private UpdateDelegationEntity UpdateDelegationEntity { get; }

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.Delegator));
            actor.AttemptToClick(UpdateDelegationRightsLocator.AddDelegator, UpdateDelegationRightsLocator.DelegateUserToGiveRightsInput);
            //actor.AttemptsTo(Click.On(UpdateDelegationRightsLocator.AddDelegator));
            actor.AttemptsTo(SendKeys.To(UpdateDelegationRightsLocator.DelegateUserToGiveRightsInput, UpdateDelegationEntity.DelegationUserWithRoles + Keys.Tab));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Checkbox.SetStatus(UpdateDelegationRightsLocator.DelegatorAllWorkflowsCheckbox, UpdateDelegationEntity.DelegateAllWorkflowsCheckbox));

            if (UpdateDelegationEntity.DelegateAllWorkflowsCheckbox)
            {
                //If all rights are being given, return. Else, give specific rights.
                return;
            }

            actor.AttemptsTo(Click.On(UpdateDelegationRightsLocator.DelegatorWorkflowsRowExpander));

            if (UpdateDelegationEntity.WorkflowsToGrantAccessTo != null)
            {
                AddOrRemoveWorkFlows(actor, UpdateDelegationEntity.WorkflowsToGrantAccessTo, true);
            }

            if (UpdateDelegationEntity.WorkflowsToRevokeAccessTo != null)
            {
                AddOrRemoveWorkFlows(actor, UpdateDelegationEntity.WorkflowsToRevokeAccessTo, false);
            }
        }

        private void AddOrRemoveWorkFlows(IActor actor, List<string> workflowList, bool grantAccessCheckbox)
        {
                       

            foreach (var workflow in workflowList)
            {
                actor.AttemptToClick(UpdateDelegationRightsLocator.AddWorkflowRow, UpdateDelegationRightsLocator.WorkflowRowDropdownInput);

                actor.WaitsUntil(Appearance.Of(UpdateDelegationRightsLocator.WorkflowRowDropdownInput), IsEqualTo.True());
                actor.AttemptsTo(Dropdown.SelectOptionByName(UpdateDelegationRightsLocator.WorkflowRowDropdownInput, workflow));

                var driver = actor.Using<BrowseTheWeb>().WebDriver;
                new Actions(driver).SendKeys(Keys.Tab).Build().Perform();

                actor.AttemptsTo(Checkbox.SetStatus(UpdateDelegationRightsLocator.GrantWorkflowAccessCheckbox(workflow), grantAccessCheckbox));
            }
        }

        
        
    }
}
