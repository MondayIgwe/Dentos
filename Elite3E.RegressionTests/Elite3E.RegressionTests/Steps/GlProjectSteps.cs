using System;
using System.Linq;
using System.Threading;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Enums;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GlDetails;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class GlProjectSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public GlProjectSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I want to add a gl project")]
        public void WhenIWantToAddAGlProject(Table table)
        {
            var glProjectEntity = table.CreateInstance<GLProjectEntity>();

            _featureContext[StepConstants.GLProjectCode] = glProjectEntity.GLProjectCode;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Code"), glProjectEntity.GLProjectCode));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Description"), glProjectEntity.Description));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Project Number"), glProjectEntity.ProjectNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the gl project is created")]
        public void ThenIVerifyTheGlProjectIsCreated()
        {
            var expectedGlProjectCode = _featureContext[StepConstants.GLProjectCode].ToString();
            
            _actor.AttemptsTo(SearchProcess.ByName(Process.GLProject));
            _actor.AttemptsTo(QuickFind.Search(expectedGlProjectCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var actualGlProjectCode = _actor.GetElementText(CommonLocator.FindDivElementContainsName("Code"));
            actualGlProjectCode.Should().NotBeNullOrEmpty();
            actualGlProjectCode.Should().BeEquivalentTo(expectedGlProjectCode);
        }

    }
}