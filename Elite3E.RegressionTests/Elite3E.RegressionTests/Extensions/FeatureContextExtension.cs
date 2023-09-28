using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Configuration;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Extensions
{
    public static class FeatureContextExtension
    {
        /// <summary>
        /// Takes a screenshot during the current test to understand failures of a test.
        /// </summary>
        /// <param name="featureContext">The FeatureContext for the current test used to build the name of the screenshot image.</param>
        /// <param name="scenarioContext">The ScenarioContext for the current test used to build the name of the screenshot image.</param>
        public static void TakeScreenShot(this FeatureContext featureContext, ScenarioContext scenarioContext = null)
        {
            var actor = (Actor)featureContext[StepHelpers.StepConstants.ActorInstance];
            var title = featureContext.FeatureInfo.Title;

            if (scenarioContext != null)
            {
                // Add the scenario detail to the title of the screenshot if available.
                title = $"{title}_{scenarioContext.ScenarioInfo.Title}";
            }

            var fileName = $"{ApplicationConfigurationBuilder.Instance.ScreenShotFilePath}\\{title}.jpeg";
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fileName);
        }
    }
}
