using Boa.Constrictor.Screenplay;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.RegressionTests.StepHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Linq;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.JournalRegister;
using FluentAssertions;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public  class JournalRegisterSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public JournalRegisterSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I search for the entry in journal register")]
        public void GivenISearchForTheEntryInJournalRegister()
        {
            //_featureContext[StepConstants.JournalManager] = "70747";
             var journalManager = _featureContext[StepConstants.JournalManager].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.JournalRegister,false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"verify the posting details in journal register")]
        public void ThenVerifyThePostingDetailsInJournalRegister(Table table)
        {
           var journalManager = _featureContext[StepConstants.JournalManager].ToString();
            var searchCriteria = table.CreateInstance<AdvancedFindSearchEntity>();
            searchCriteria.SearchValue = journalManager;
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var searchCriteriaList = new List<AdvancedFindSearchEntity>();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.AdvanceFindBaseSearchAttribute), IsEqualTo.True());
            var columnSearch = driver.FindElement(CommonLocator.AdvanceFindBaseSearchAttribute.Query);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            columnSearch.SendKeys(Keys.Control + "a");
            columnSearch.SendKeys(Keys.Delete);
            columnSearch.SendKeys(searchCriteria.SearchColumn.Trim());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            columnSearch.SendKeys(Keys.Tab);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            driver.FindElement(CommonLocator.AdvanceFindBaseSearchOperator.Query).SendKeys(searchCriteria.SearchOperator + Keys.Tab);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            driver.FindElement(CommonLocator.AdvanceFindBaseSearchValue.Query).SendKeys(journalManager);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.RunReport),IsEqualTo.True());
            _actor.AttemptsTo(Click.On(CommonLocator.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var maskedAliasValues =  _actor.AsksFor(TextList.For(JournalRegisterLocator.MaskedAliasValues));
            List<string> glMaskedValues = (List<string>)_featureContext[StepConstants.GLMaskedValues];
            maskedAliasValues.Any(item => glMaskedValues.Contains(item));
        }
    }
}
