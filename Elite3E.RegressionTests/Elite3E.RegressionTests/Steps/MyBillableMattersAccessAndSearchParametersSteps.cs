using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.MyBillableMatters;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using Elite3E.Infrastructure.Entity;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.MattersMetric;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class MyBillableMattersAccessAndSearchParametersSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        private IWebDriver driver;

        public MyBillableMattersAccessAndSearchParametersSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
            driver = _actor.Using<BrowseTheWeb>().WebDriver;

        }

        [StepDefinition(@"I navigate to my billable matters process")]
        public void INavigateToMyBillableMattersProcess()
        {
            // check if process is open 
            if (_actor.DoesElementExist(MyBillableMattersLocators.MyBillableMattersDashboard))
                _actor.AttemptsTo(DoubleClick.On(MyBillableMattersLocators.ExistMyBillableMattersDashboard));
            else
                _actor.AttemptsTo(SearchProcess.ByName(Process.MyBillableMatters, false));

        }
        [Then(@"I verify that the process has separate buttons for search,proforma options and info only options")]
        public void ThenIVerifyThatTheProcessHasSeparateButtonsForSearchProformaOptionsAndInfoOnlyOptions()
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementContainsText(" Search ")), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementContainsText(" Info Only Options ")), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementContainsText(" Proforma Options ")), IsEqualTo.True());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Close));
        }

        [Then(@"I enter details for the client")]
        public void WhenISearchForTheClient()
        {
            string client = _featureContext[StepConstants.ClientNumber].ToString();
            // remove fee earner show show its not required

            _actor.GetElementText(MyBillableMattersLocators.MetricRunDate).Should().NotBeNullOrEmpty();

            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.MatterInput, Keys.Backspace));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.ClientInput, Keys.Backspace));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.FeeEarnerInput, Keys.Backspace));
            driver.FindElement(MyBillableMattersLocators.FeeEarnerInput.Query).SendKeys(Keys.Tab);

            //search using client
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.ClientInput, client));
            bool billingState = _actor.AsksFor(SelectedState.Of(MyBillableMattersLocators.GetIsBillingCheckbox));
            billingState.Should().BeTrue();
        }
        [Then(@"I enter details for the fee earner")]
        public void WhenISearchForAFeeEarner()
        {
            string feeEarner = _featureContext[StepConstants.FeeEarnerName].ToString();
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.MatterInput, String.Empty));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.ClientInput, String.Empty));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.FeeEarnerInput, String.Empty));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.FeeEarnerInput, feeEarner));

        }

        [StepDefinition(@"I enter details and search for my billable matters")]
        public void WhenISearchForMyBillableMatters(Table table)
        {
            var entity = table.CreateInstance<MyBillableMattersEntity>();
            string matter = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.MatterInput, String.Empty));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.ClientInput, String.Empty));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.FeeEarnerInput, String.Empty));

            if (!string.IsNullOrEmpty(entity.FeeEarner))
                _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.FeeEarnerInput, entity.FeeEarner));

            if (!string.IsNullOrEmpty(matter))
                _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.MatterInput, matter));

            if (entity.WIPExcludeZeroCheckbox != null)
                _actor.AttemptsTo(Checkbox.SetStatus(MyBillableMattersLocators.GetIsExcludeZeroWIPCheckbox, (bool)entity.WIPExcludeZeroCheckbox));

            if (entity.ARExcludeZeroCheckbox != null)
                _actor.AttemptsTo(Checkbox.SetStatus(MyBillableMattersLocators.GetIsExcludeZeroARCheckbox, (bool)entity.ARExcludeZeroCheckbox));

            if (entity.ClientAccountExcludeZeroCheckbox != null)
                _actor.AttemptsTo(Checkbox.SetStatus(MyBillableMattersLocators.GetIsExcludeZeroTrustCheckbox, (bool)entity.ClientAccountExcludeZeroCheckbox));

            _actor.AttemptsTo(DoubleClick.On(MyBillableMattersLocators.Search));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I validate my billable matter boa balance is more than zero")]
        public void WhenIValidateMyBillableMatterBoaBalanceIsMoreThanZero()
        {
            string matter = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.scrollBarHorizontalToElement(MyBillableMattersLocators.MyMattersHorizontalScrollBar, MyBillableMattersLocators.MyMattersBOALabel);
            _actor.WaitsUntil(Appearance.Of(MyBillableMattersLocators.MyMattersBOAGridValue), IsEqualTo.True());

            _actor.GetElementText(MyBillableMattersLocators.MyMattersBOAGridValue).Should().NotBeEquivalentTo("0.00", "Matter (" + matter + ") BOA Balance should not be 0.00");
        }


        [Then(@"I enter details for the matter")]
        public void WhenISearchForTheMatter()
        {
            string matter = _featureContext[StepConstants.MatterNumberContext].ToString();
            // remove fee earner show show its not required
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.MatterInput, String.Empty));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.ClientInput, String.Empty));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.FeeEarnerInput, String.Empty));
            //search using matter
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.MatterInput, matter));
            bool billingState = _actor.AsksFor(SelectedState.Of(MyBillableMattersLocators.GetIsBillingCheckbox));
            billingState.Should().BeTrue();
        }

        [Then(@"I want to search for matters with '([^']*)' and see results displayed")]
        public void ThenIWantToSearchForMattersWithAndSeeResultsDisplayed(string searchOption)
        {
            if (searchOption == "AR")
            {
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroTrustCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroWIPCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (searchOption == "WIP")
            {
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroARCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroTrustCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            SearchAndValidate();
        }

        private void SearchAndValidate()
        {
            int max = 2;
            int counter = 0;

            while (counter < max)
            {
                _actor.AttemptsTo(DoubleClick.On(MyBillableMattersLocators.Search));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                if (_actor.DoesElementExist(CommonLocator.ButtonElementContainsText(" Enquiry "), 30))
                    break;
                counter++;
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I want to search and see results displayed for '([^']*)' Fee Earner")]
        public void ThenIWantToSearchAndSeeResultsDisplayedForFeeEarner(string feeEarnerRole)
        {
            if (feeEarnerRole == "Supervising")
            {
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsSupervisingCheckbox));
            }
            if (feeEarnerRole == "Responsible")
            {
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsResponsibleCheckbox));
            }
            if (_actor.AsksFor(SelectedState.Of(MyBillableMattersLocators.GetIsExcludeZeroARCheckbox)))
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroARCheckbox));
            if (_actor.AsksFor(SelectedState.Of(MyBillableMattersLocators.GetIsExcludeZeroWIPCheckbox)))
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroWIPCheckbox));
            if (_actor.AsksFor(SelectedState.Of(MyBillableMattersLocators.GetIsExcludeZeroTrustCheckbox)))
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroTrustCheckbox));

            SearchAndValidate();
        }

        [Then(@"I want to search and see results displayed")]
        public void ThenIMustBeAbleToSearchForClientsCorrectResultsMustBeDisplayed()
        {
            if (_actor.AsksFor(SelectedState.Of(MyBillableMattersLocators.GetIsExcludeZeroARCheckbox)))
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroARCheckbox));
            if (_actor.AsksFor(SelectedState.Of(MyBillableMattersLocators.GetIsExcludeZeroWIPCheckbox)))
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroWIPCheckbox));
            if (_actor.AsksFor(SelectedState.Of(MyBillableMattersLocators.GetIsExcludeZeroTrustCheckbox)))
                _actor.AttemptsTo(JavaScriptClick.On(MyBillableMattersLocators.GetIsExcludeZeroTrustCheckbox));

            SearchAndValidate();
        }

        [Then(@"I search using default fee earner")]
        public void ThenISearchUsingDefaultFeeEarner()
        {
            _actor.AttemptsTo(DoubleClick.On(MyBillableMattersLocators.Search));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"My Billable Matters process returns no results")]
        public void ThenMyBillableMattersProcessReturnsNoResults()
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementContainsText(" Enquiry ")), IsEqualTo.False());
        }

        [Then(@"I click the proforma options button")]
        public void ThenIClickTheProformaOptionsButton()
        {
            _actor.AttemptsTo(DoubleClick.On(CommonLocator.ButtonElementContainsText(" Info Only Options ")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }
        [Given(@"I navigate to my matters metric process")]
        public void GivenINavigateToMyMattersMetricProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.MyMattersMetric, false));
        }

        [Given(@"I search or add new my matters metric")]
        public void GivenISearchForMetricOrAddNew(Table table)
        {
            var MattersMatricInfo = table.CreateInstance<MattersMetricEntity>();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(MattersMatricInfo.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (_actor.DoesElementExist(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                string tableName = MattersMatricInfo.TableName;
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.WaitsUntil(Appearance.Of(CommonLocator.MetricTableNameInput), IsEqualTo.True());
                _actor.AttemptsTo(SendKeys.To(CommonLocator.MetricTableNameInput, tableName));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(SendKeys.To(CommonLocator.Description, MattersMatricInfo.Description));

                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(MattersMatricLocators.RequestedMattersIcon));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(MatterLocator.AdvancedLookupSearchCriteria(MattersMatricInfo.MatterNumberIndex)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(Click.On(MatterLocator.DropDownSelection(MattersMatricInfo.SearchCriteria)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(MatterLocator.AdvancedLookupSearchText(MattersMatricInfo.MatterNumberIndex), MattersMatricInfo.SearchValue));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(" SUBMIT ")));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Save));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ScrollToElement.At(CommonLocator.ButtonElementContainsText(" Run Metric ")));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(" Run Metric ")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var message = _actor.GetElementText(CommonLocator.InformationMessage, 120);
            message.Should().ContainEquivalentOf("created successfully for");
        }

        [StepDefinition(@"I search or filter by matter")]
        public void WhenISearchOrFilterByMatter()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var feeEarner = _featureContext[StepConstants.FeeEarnerName].ToString();

            _actor.GetDriver().FindElement(MyBillableMattersLocators.FeeEarnerInput.Query).Clear();
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Fee Earner", feeEarner));
            //_actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.FeeEarnerInput, feeEarner + Keys.Enter));
            DisableExcludeMattersFilter();

           // _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Matter", matterNumber));
            _actor.AttemptsTo(Click.On(MyBillableMattersLocators.ProformaSearch));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(MyBillableMattersLocators.MyMatterSearchFilter));
            _actor.AttemptsTo(SendKeys.To(MyBillableMattersLocators.MyMatterSearchInput, matterNumber + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(MyBillableMattersLocators.MyMatterResultSelect));
           
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(MyBillableMattersLocators.CloseMyMatterIcon));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(MyBillableMattersLocators.Generate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        public void DisableExcludeMattersFilter()
        {
            _actor.AttemptsTo(Checkbox.SetStatus(MyBillableMattersLocators.GetIsExcludeZeroWIPCheckbox, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Checkbox.SetStatus(MyBillableMattersLocators.GetIsExcludeZeroARCheckbox, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Checkbox.SetStatus(MyBillableMattersLocators.ClientAccountCheckbox, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I verify proforma has been generated")]
        public void WhenIVerifyProformaHasBeenGenerated()
        {
            var message = _actor.GetDriver().FindElement(CommonLocator.ToastMessage.Query).Text;
            message.Should().Contain("Generated proforma");
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I close my billable matter")]
        public void ThenICloseMyBillableMatter()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.CloseProforma));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }

}

