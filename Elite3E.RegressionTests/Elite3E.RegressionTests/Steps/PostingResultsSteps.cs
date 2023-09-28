using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
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
    public  class PostingResultsSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public PostingResultsSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I search for the entry in posting results process")]
        public void GivenISearchForTheEntryInPostingResultsProcess()
        {
            var journalManager = _featureContext[StepConstants.JournalManager].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.PostingResults,false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(journalManager));
        }
        [Then(@"verify the posting status is posted")]
        public void ThenVerifyThePostingStatusIsPosted()
        {
            _actor.AsksFor(Text.Of(EntryAndModifyProcessLocators.GlPostingsStatus)).Equals("Posted");
            
        }


    }
}
