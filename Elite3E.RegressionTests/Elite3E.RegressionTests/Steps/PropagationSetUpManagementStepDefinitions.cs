using System;
using System.Collections.Generic;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.SetUpPropagation;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.SetUpPropagation;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class PropagationSetUpManagementStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public PropagationSetUpManagementStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I have a process to select '([^']*)'")]
        public void GivenIHaveAProcessToSelect(string nxUnit)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.SetupPropagation));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(SetUpPropagationLocators.ProcessInput, nxUnit));
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            new Actions(driver).SendKeys(Keys.Tab).Build().Perform();

            // ToDo: Need to revisit once the regions are up to date with FT 
            if(ApplicationConfigurationBuilder.Instance.Region.Equals("Us"))
            {
                if (_actor.DoesElementExist(SetUpPropagationLocators.Role))
                    _actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.Role, "0:AD:G:System Administrator"));
            }
            else
            {
                if (_actor.DoesElementExist(SetUpPropagationLocators.Role))
                    _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Role", "0:AD:G:System Administrator"));
            }  
        }

        [When(@"I add a child three e instance")]
        [Then(@"I add another child three e instance")]
        public void WhenIAddAChildThreeEInstance(Table table)
        {
            var menuItems = new List<string>()
            {
                StepConstants._3Instances
            };

            _actor.AttemptsTo(AddChildProcess.ByName(menuItems));
            var setupPropagation = table.CreateInstance<SetupPropagationEntity>();
            _actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.Instance, setupPropagation.Instance));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.ControlSource, setupPropagation.ControlSource));

        }

        [Given(@"I use advanced find to search and check propagation exists")]
        public void GivenIUseAdvancedFindToSearchAndCheckPropagationExists(Table table)
        {
            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            _featureContext[StepConstants.SetUpPropagationCount] =
                _actor.AskingFor(AddOrSelectAnSetUpPropagationProcess.GetProcessResult(searchCriteriaCol));

        }

        [When(@"I add new Setup Propagation")]
        public void GivenIAddNewSetupPropagation(Table table)
        {
            var setUpPropagation = table.CreateInstance<SetupPropagationEntity>();
            _actor.AttemptsTo(CreateOrUpdateOrDeletePropagation.With(setUpPropagation, (bool)_featureContext[StepConstants.SetUpPropagationCount]));
        }

        [When(@"I add 3E instances")]
        public void WhenIAdd3EInstances(Table table)
        {
            var instance = table.CreateInstance<SetupPropagationEntity>();
            _actor.AttemptsTo(SearchAndAddOrUpdateInstances.With(instance));
        }

        [StepDefinition(@"I want to '([^']*)' items")]
        public void ThenIWantToItems(string decision)
        {
            if (decision == "exclude")
            {
                bool state = _actor.AsksFor(SelectedState.Of(SetUpPropagationLocators.ExcludeList));
                if (!state)
                    _actor.AttemptsTo(Click.On(SetUpPropagationLocators.ExcludeList));
            }
            else
            {
                bool state = _actor.AsksFor(SelectedState.Of(SetUpPropagationLocators.IncludeList));
                if (!state)
                    _actor.AttemptsTo(Click.On(SetUpPropagationLocators.IncludeList));
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("Select")));
            if (_actor.DoesElementExist(CommonLocator.ButtonElementContainsText("Select")))
                _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ButtonElementContainsText("Select")));
            foreach (var item in GlobalConstants.IncludeList)
            {
                _actor.AttemptsTo(SendKeys.To(CommonLocator.SelectItemSearch, item));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.SelectItemSearch));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(DoubleClick.On(CommonLocator.Searchbtn));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.Searchbtn));
                _actor.AttemptsTo(Click.On(SetUpPropagationLocators.SelectOption));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(" OK ")));
       //         _actor.WaitsUntil(Appearance.Of(SetUpPropagationLocators.AddedItems(item)), IsEqualTo.True());

            }
        }

        [Then(@"I want to '([^']*)' items having '([^']*)'")]
        public void ThenIWantToItemsHaving(string decision, string value)
        {
            if (decision == "exclude")
            {
                bool state = _actor.AsksFor(SelectedState.Of(SetUpPropagationLocators.ExcludeList));
                if (!state)
                    _actor.AttemptsTo(Click.On(SetUpPropagationLocators.ExcludeList));
            }
            else
            {
                bool state = _actor.AsksFor(SelectedState.Of(SetUpPropagationLocators.IncludeList));
                if (!state)
                    _actor.AttemptsTo(Click.On(SetUpPropagationLocators.IncludeList));
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("Select")));
            if (_actor.DoesElementExist(CommonLocator.ButtonElementContainsText("Select")))
                _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ButtonElementContainsText("Select")));

                _actor.AttemptsTo(SendKeys.To(CommonLocator.SelectItemSearch, value));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(" Search ")));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(SetUpPropagationLocators.SelectOption));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(" OK ")));
                _actor.WaitsUntil(Appearance.Of(SetUpPropagationLocators.AddedItems(value)), IsEqualTo.True());
        }


        [Then(@"I want to submit and get an error message")]
        public void ThenIWantToSubmitAndGetAnErrorMessage()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            message.Should().Contain(GlobalConstants.SetUpPropagationDuplicateMessage);
        }

        [Then(@"I want to submit")]
        public void ThenIWantToSubmit()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.WaitsUntil(Appearance.Of(SetUpPropagationLocators.Instance), IsEqualTo.False());
        }

        [StepDefinition(@"I want to delete the existing record")]
        public void ThenIWantToDeleteTheRecord()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.SetupPropagation));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(DeleteProcess.ClickDelete());
        }

    }
}
