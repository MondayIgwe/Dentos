using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.RegressionTests.StepHelpers;
using System;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Notification;
using FluentAssertions;
using System.Linq;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    internal class NotificationSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public NotificationSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];

        }

        [Then(@"I validate a notification task '([^']*)'")]
        public void ThenIValidateANotificationTask(string task)
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            string businessObjectFound = _actor.AsksFor(Text.Of(NotificationTaskManagerLocator.BusinessObjectInput));

            businessObjectFound = String.IsNullOrEmpty(businessObjectFound) ?
                 driver.FindElement(NotificationTaskManagerLocator.BusinessObjectInput.Query).GetAttribute("value") : businessObjectFound;

            businessObjectFound.Should().Be(task);
        }

        [StepDefinition(@"I start a notification task '([^']*)'")]
        public void ThenIStartANotificationTask(string task)
        {
            _actor.AttemptsTo(SendKeys.To(NotificationStartTaskLocator.TaskInput, task));
            _actor.AttemptsTo(Click.On(NotificationStartTaskLocator.TaskDropdownOptions(task)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(NotificationStartTaskLocator.GoButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(NotificationStartTaskLocator.ConfirmationToast), IsEqualTo.True());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            driver.Navigate().Refresh();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I validate notification task '([^']*)' is complete")]
        public void WhenIValidateNotificationTaskIsComplete(string task)
        {
            _actor.WaitsUntil(Appearance.Of(NotificationTaskAndLogLocators.QueuedTasksHeader), IsEqualTo.True());
            _actor.AttemptsTo(ScrollToElement.At(NotificationTaskAndLogLocators.QueuedTasksHeader));

            if(_actor.DoesElementExist(NotificationTaskAndLogLocators.TaskInQueue(task), 30))
            {
                int timeout = 10;
                int counter = 0;

                while (counter < timeout)
                {
                    _actor.AttemptsTo(ScrollToElement.At(NotificationTaskAndLogLocators.RefreshButton));
                    _actor.AttemptsTo(Click.On(NotificationTaskAndLogLocators.RefreshButton));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    _actor.AttemptsTo(ScrollToElement.At(NotificationTaskAndLogLocators.QueuedTasksHeader));

                    if (!_actor.DoesElementExist(NotificationTaskAndLogLocators.TaskInQueue(task), 5))
                        break;

                    counter++;
                }
            }

            _actor.DoesElementExist(NotificationTaskAndLogLocators.TaskInQueue(task),1).Should().BeFalse();

            _actor.AttemptsTo(ScrollToElement.At(NotificationTaskAndLogLocators.ViewPreviouslyRunTasksButton));
            _actor.AttemptsTo(Click.On(NotificationTaskAndLogLocators.ViewPreviouslyRunTasksButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(NotificationTaskAndLogLocators.PreviouslyRunTasksHeader), IsEqualTo.True());

            var row = _actor.GetElementTextList(NotificationTaskAndLogLocators.PreviouslyRunTaskRow(task));
            row.Should().NotBeNullOrEmpty();
            row.Any(x => x.Contains(task)).Should().BeTrue();
            row.Any(x => x.Contains("Completed",StringComparison.CurrentCultureIgnoreCase)).Should().BeTrue();

            _actor.AttemptsTo(ScrollToElement.At(CommonLocator.Close));
            _actor.AttemptsTo(Click.On(CommonLocator.Close));      
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }



    }
}
