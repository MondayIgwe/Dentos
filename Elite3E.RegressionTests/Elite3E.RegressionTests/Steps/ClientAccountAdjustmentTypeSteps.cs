using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountAdjustment;
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
    public class ClientAccountAdjustmentTypeSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ClientAccountAdjustmentTypeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Then(@"I verify EFT flag is set to true")]
        public void ThenIVerifyEFTFlagIsSetToTrue()
        {
            _actor.AsksFor(SelectedState.Of(ClientAccountAdjustmentLocator.EFTCheckbox)).Should().BeTrue();
        }

        [Then(@"I verify Deposit Flag is set to false")]
        public void ThenIVerifyDepositFlagIsSetToFalse()
        {
            _actor.AsksFor(SelectedState.Of(ClientAccountAdjustmentLocator.DepositCheckbox)).Should().BeFalse();
        }

    }
}
