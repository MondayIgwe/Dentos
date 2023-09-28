using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.WorkFlowDashbord;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class TimekeeperWorkflowSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public TimekeeperWorkflowSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I verify that the proforma workflow task exists")]
        public void GivenIVerifyThatTheProformaWorkflowTaskExists()
        {
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.ProformaWorkflowTask));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.MatterNumberContext].ToString(), "Start Workflow"));
            _actor.DoesElementExist(ProformaGenerationLocator.WorkflowDiv(_featureContext[StepConstants.MatterNumberContext].ToString())).Should().BeTrue();
        }

        [StepDefinition(@"I verify the proforma task exists")]
        public void WhenIVerifyTheProformaTaskExists()
        {
            _actor.AttemptsTo(Click.On(ProformaGenerationLocator.ProformaWorkflowTask));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(WorkFlowDashBoardFilter.Search(_featureContext[StepConstants.MatterNumberContext].ToString(), "Bill submitted - no approval required"));
            _actor.DoesElementExist(ProformaGenerationLocator.WorkflowDiv(_featureContext[StepConstants.MatterNumberContext].ToString())).Should().BeTrue();
        }


        [StepDefinition(@"I verify that there are no workflow tasks visible")]
        public void WhenIVerifyThatThereAreNoWorkflowTasksVisible()
        {
            _actor.DoesElementExist(ProformaGenerationLocator.ProformaWorkflowTask).Should().BeFalse();
        }

        [StepDefinition(@"I can verify that the proforma task has been proceeded to the next stage")]
        public void WhenICanVerifyThatTheProformaTaskHasBeenProceededToTheNextStage()
        {
            _actor.DoesElementExist(ProformaGenerationLocator.ProformaBillHeader).Should().BeTrue();
        }

        [StepDefinition(@"I set the return to billing fee earner to false")]
        public void ThenISetTheReturnToBillingFeeEarnerToFalse()
        {
            _actor.DoesElementExist(ProformaGenerationLocator.ReturnToBTKText).Should().BeTrue();
            _actor.AttemptsTo(Checkbox.SetStatus(ProformaGenerationLocator.ReturnToBTKCheckbox, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


    }
}
