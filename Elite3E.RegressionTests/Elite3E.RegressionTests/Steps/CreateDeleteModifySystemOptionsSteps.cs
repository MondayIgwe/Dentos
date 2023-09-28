using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.OverrideSetSystemOptions;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class CreateDeleteModifySystemOptionsSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public CreateDeleteModifySystemOptionsSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I navigate to create/delete/modify system options process")]
        public void WhenINavigateToCreateDeleteModifySystemOptionsProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.CreateDeleteModifySystemOption));
        }

        [Then(@"I verify that '([^']*)' system override exist")]
        public void ThenIVerifyThatSystemOverrideExist(string systemOverride)
        {
            var systemOverrides = systemOverride.Split(',');
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.Options, GlobalConstants.Grid));
            _actor.AttemptsTo(Click.On(CommonLocator.ChildSearchIcon));
            foreach (var _override in systemOverrides)
            {
                _actor.AttemptsTo(DoubleClick.On(CommonLocator.ChildSearchInput));

                _actor.GetDriver().FindElement(CommonLocator.ChildSearchInput.Query).SendKeys(_override);
                _actor.GetDriver().FindElement(CommonLocator.ChildSearchInput.Query).SendKeys(Keys.Enter);
                _actor.GetDriver().FindElement(CommonLocator.ChildSearchInput.Query).SendKeys(Keys.Enter);
                var elementExist = _actor.DoesElementExist(CommonLocator.SearchResult(_override));
                elementExist.Should().BeTrue();
            }

        }

        [Then(@"I verify that '([^']*)' override is displayed")]
        public void ThenIVerifyThatOverrideIsDisplayed(string option)
        {
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.Options, GlobalConstants.Grid));
            _actor.AttemptsTo(Click.On(CommonLocator.ChildFilterIcon("Options")));
            _actor.GetDriver().FindElement(CommonLocator.ChildSearchInput.Query).SendKeys(option + Keys.Enter);
            _actor.DoesElementExist(CommonLocator.SearchResult(option)).Should().BeTrue();
        }


        [StepDefinition(@"I search for '([^']*)' override")]
        public void WhenISearchForOverride(string billing)
        {
            _actor.AttemptsTo(QuickFind.Search(billing));
        }

        [Then(@"I verify the description '([^']*)'")]
        public void ThenIVerifyTheDescription(string actualDescription)
        {
            var expectedDescription = _actor.GetElementTextList(OverrideSetSystemOptionsLocators.OptionDescriptionColumn);
            expectedDescription.Should().ContainEquivalentOf(actualDescription);
        }

        [Then(@"I verify that '([^']*)' option is displayed")]
        public void ThenIVerifyThatOptionIsDisplayed(string option)
        {
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.GroupType("Billing")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards("Allow_Hidden_Cards")));
            _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards(option), 3, "pagedown");
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.DoesElementExist(OverrideSetSystemOptionsLocators.HiddenCards(option)).Should().BeTrue();
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards(option)));
        }


    }
}
