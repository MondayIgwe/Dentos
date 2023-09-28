using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Helper;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.RegressionTests.Extensions;
using Elite3E.RegressionTests.Hooks;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class LifeCycle
    {
        [BeforeFeature()]
        public static void Setup(FeatureContext featureContext)
        {
            var logger = new ConsoleLogger();
            var url = string.Empty;

            try
            {
                var actor = new Actor("chrome", logger);

                // Output the URL to the logging to confirm the Elite 3E instance being used.
                WriteLogMessage($"Starting Feature '{featureContext.FeatureInfo.Title}'. BaseUrl: {ApplicationConfigurationBuilder.Instance.BaseUrl}.", true);

                // Set the default timeouts.
                if (ApplicationConfigurationBuilder.Instance.DefaultTimeout.HasValue)
                {
                    var defaultTimeout = ApplicationConfigurationBuilder.Instance.DefaultTimeout.Value;
                    actor.Can(SetTimeouts.To(defaultTimeout, 0));
                }

                var browser = new Web(BrowserTypes.HeadLessChrome).Browser;

                actor.Can(BrowseTheWeb.With(browser));
                featureContext[StepHelpers.StepConstants.ActorInstance] = actor;

                url = UrlHelper.GetBaseUrl();
                actor.AttemptsTo(Navigate.ToUrl(url));
                actor.WaitsUntil(Appearance.Of(CommonLocator.Entity), IsEqualTo.True(), 90);
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            catch (Exception ex)
            {
                logger.Error($"Failed to setup test: {ex.Message}");

                // Output to pipeline logs showing the current test progress.
                // ***Note***: Output the URL for debug purposes only, otherwise, you will see the username/password in logs.
                //TestContext.Progress.WriteLine($"Test using URL: {url}");

                // Take a screenshot to understand why it failed.
                featureContext.TakeScreenShot();

                // Throw the exception to continue as previously to ensure it is logged correctly.
                throw;
            }
        }

        [AfterFeature()]
        public static void TearDown(FeatureContext featureContext)
        {
            new CommonHooks(featureContext, null).CancelProcess();
            var actor = (Actor)featureContext[StepHelpers.StepConstants.ActorInstance];
            actor.AttemptsTo(QuitWebDriver.ForBrowser());

            // Output a message that the feature has completed.
            WriteLogMessage($"Completed Feature '{featureContext.FeatureInfo.Title}'.", true);
        }

        /// <summary>
        /// Writes a log line to the output that will appear in a pipeline. Can highlight it to be able to see 
        /// the start and end of a task easily for instance.
        /// </summary>
        /// <param name="message">The message to output.</param>
        /// <param name="isHighlightMessage">Whether to highlight the message.</param>
        /// <param name="highlightMessageText">The text to surround the message to be able to highlight it in logs.</param>
        public static void WriteLogMessage(string message, bool isHighlightMessage = false, string highlightMessageText = "****************")
        {
            var highlightMessageStartText = isHighlightMessage ? $"{highlightMessageText} " : string.Empty;
            var highlightMessageEndText = isHighlightMessage ? $" {highlightMessageText}" : string.Empty;

            TestContext.Progress.WriteLine($"{highlightMessageStartText}{message}{highlightMessageEndText}");
        }
    }
}
