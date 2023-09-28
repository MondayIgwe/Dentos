using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.MatterBalances;
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
    public class MatterBalanceInMatterCurrencySteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public MatterBalanceInMatterCurrencySteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];

        }

        [StepDefinition(@"I verify the balance for the receiving fee earner matter")]
        public void ThenIVerifyTheBalanceForTheReceivingFeeEarnerMatter()
        {
           var matter= _featureContext[StepConstants.SubMatterNumberContextTwo].ToString();
            var amount = _featureContext[StepConstants.AmountNumberContext].ToString();
            var amountWithDecimal = Convert.ToDecimal(amount).ToString("#.00");
            _actor.AttemptsTo(SendKeys.To(MatterBalancesInMatterCurrency.MatterInput, matter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(MatterBalancesInMatterCurrency.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(MatterBalancesInMatterCurrency.TrustAmount),IsEqualTo.True(),1);
            var trustAmount = _actor.AsksFor(Text.Of(MatterBalancesInMatterCurrency.TrustAmount));
            trustAmount.Should().Contain(amountWithDecimal);
        }

    }
}
