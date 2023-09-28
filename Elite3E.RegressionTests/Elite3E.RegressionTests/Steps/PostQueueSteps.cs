using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.PostQueue;
using Elite3E.RegressionTests.DataCreators;
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
    public class PostQueueSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public PostQueueSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the post queue process")]
        public void GivenINavigateToThePostQueueProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.PostQueue, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I click the show button")]
        public void WhenIClickTheShowButton()
        {
            _actor.AttemptsTo(Click.On(PostQueueLocators.ShowButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I verify the client account receipt is not present with a failed status")]
        public void WhenIVerifyTheClientAccountReceiptIsNotPresentWithAFailedStatus(Table table)
        {
            var expectedUsername =  new UserRoleData().GetLoggedInUserName().Result;
            var expectedPostSource = table.Rows[0][ColumnNames.PostSource].ToString();
            var actualUser = _actor.GetElementText(PostQueueLocators.PostUserDiv);
            var actualPostSource = _actor.GetElementText(PostQueueLocators.PostSourceDiv);
            var actualPostStatus = _actor.GetElementText(PostQueueLocators.PostMgrStatusDiv);

            if (expectedUsername.Equals(actualUser) && expectedPostSource.Equals(actualPostSource))
            {
                actualPostStatus.Should().NotBeEquivalentTo("Failed");
            }//else the post status is Posted successfully
        }
 

        [When(@"I verify the cost/time/charge/receipt card is not present in the post queue")]
        public void WhenIVerifyTheCostTimeChargeReceiptCardIsNotPresentInThePostQueue()
        {
            var index = _featureContext[StepConstants.CardIndex].ToString();
            _actor.AttemptsTo(QuickFind.Search(index.Trim()));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(CommonLocator.NoSearchRecords).Should().BeTrue();
        }

    }
}
