using TechTalk.SpecFlow;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Client;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Selenium;
using FluentAssertions;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Audit;
using System.Linq;
using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Delegation;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class AuditSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public AuditSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I verify single audit button on a process form")]
        public void GivenIVerifyAuditButtonOnAnIndividualProcessForm(Table table)
        {
            var entityList = table.CreateSet<AuditEntity>();

            foreach(var entity in entityList)
            {
                GivenISearchForProcessAndSelectARecord(entity.Process);
                GivenIVerifyAuditButtonAtFormFor(entity.Process, entity.Form, entity.Archetype);
                ThenICancelTheProcess();
            }
        }

        [StepDefinition(@"I verify multiple audit buttons on a process form")]
        public void GivenIVerifyAuditButtonOnTheSameProcessForm(Table table)
        {
            var entityList = table.CreateSet<AuditEntity>();
            
            GivenISearchForProcessAndSelectARecord(entityList.ToList()[0].Process);
            foreach (var entity in entityList)
            {   
                GivenIVerifyAuditButtonAtFormFor(entity.Process, entity.Form, entity.Archetype);                
            }
            ThenICancelTheProcess();
        }


        public void GivenISearchForProcessAndSelectARecord(string process)
        {
            _actor.AttemptsTo(SearchProcess.ByName(process, false));
            //This code is not needed anymore we might use it later
            //if (process.Equals("Invoices"))
            //{
            //    _actor.AttemptsTo(Click.On(InvoicesLocator.InvoiceMasterProcess));
            //    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            //};
            GivenISelectSearchResultThatIsNotLocked("30");
        }

        [StepDefinition(@"I advanced find and select unlocked record")]
        public void GivenIAdvancedFindAndSelectUnlockedRecord(Table table)
        {
            var searchCriteriaCol = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteriaCol));
            _actor.DoesElementExist(UpdateDelegationRightsLocator.NoRecordsSearchResult, 5).Should().BeFalse();
            GivenISelectSearchResultThatIsNotLocked("30");
        }


        public void GivenISelectSearchResultThatIsNotLocked(string numOfResults)
        {
            _actor.AttemptsTo(SendKeys.To(AuditLocators.SearchResultRowCountInput, numOfResults));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if(!_actor.DoesElementExist(AuditLocators.FindResultRows,5))
            {
                if (_actor.DoesElementExist(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton), 10))
                {
                    _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.False());
                }
                return;
            }

            var resultList = _actor.FindAll(AuditLocators.FindResultRows);
            //removing all locked records
            var updatedList = resultList.Where(x =>
            {
                try
                {
                    if (x.FindElement(By.XPath(".//e3e-locked-column//mat-icon")) == null)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception e)
                {
                    Console.Write("Error: " + e.Message);
                    return true;
                }
            }
            ).ToList();


            updatedList.ElementAt(0).FindElement(By.XPath(".//input[@type='checkbox']")).Click();
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)), IsEqualTo.False());
        }

        [StepDefinition(@"I verify audit button in process '([^']*)'")]
        public void GivenIVerifyAuditButtonAtFormFor(string process)
        {
            GivenIVerifyAuditButtonAtFormFor(process, null, null);
        }

        public void GivenIVerifyAuditButtonAtFormFor(string process, string form, string archetype)
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Cancel), IsEqualTo.True());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, form));

            bool isProcessMenu = (string.IsNullOrEmpty(form) && string.IsNullOrEmpty(archetype)) ? true : false;

            if(string.IsNullOrEmpty(archetype))
            {
                //If there is no Form or Archetype var provided, we are using the Main Process Manu (blue)
                //Else, we'll use the Form
                //NOTE, if you want to use the form, only provide process and form. Not archetype
                archetype = (string.IsNullOrEmpty(form)) ? process.Split(" ")[0] : form;
            }

            if (!isProcessMenu && !archetype.Contains("Master Invoice"))
            {
                if (_actor.GetElementTextList(AuditLocators.ArchitypeAddButton(archetype, "Add")).Count > 1)
                {//If there are Forms with similar names e.g. in matter we have Rate Exception and Rate Exception Group, add this to make the name unique for the locator.
                    archetype += "  (";
                }

                if (_actor.DoesElementExist(AuditLocators.ArchitypeExpandMoreButton(archetype), 2))
                {
                    _actor.AttemptsTo(Click.On(AuditLocators.ArchitypeExpandMoreButton(archetype)));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }

                //Forms will not have the audit button available unless there is a form added.
             //   _actor.AttemptsTo(ScrollToElement.At(AuditLocators.ArchitypeAddButton(archetype, "Add")));
                if (!_actor.DoesElementExist(AuditLocators.ArchitypeAddButton(archetype, "Audit"), 2))
                {
                    _actor.AttemptsTo(Click.On (AuditLocators.ArchitypeAddButton(archetype, "Add")));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    if (_actor.DoesElementExist(AuditLocators.FindResultRows, 2))
                    {
                        GivenISelectSearchResultThatIsNotLocked("30");
                        return;
                    }
                }
            }

            _actor.AttemptsTo(ScrollToElement.At(AuditLocators.ArchitypeAddButton(archetype, "Audit")));
            _actor.WaitsUntil(Appearance.Of(AuditLocators.ArchitypeAddButton(archetype, "Audit")), IsEqualTo.True());

            _actor.AttemptsTo(JScript.ClickOn(AuditLocators.ArchitypeAddButton(archetype, "Audit")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(AuditLocators.AuditHeader), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(AuditLocators.AuditCloseButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        public void ThenICancelTheProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }
}
