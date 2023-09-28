using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.UDFValidationList;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Audit;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF;
using Elite3E.RegressionTests.StepHelpers;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class UDFValidationListSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;


        public UDFValidationListSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the UDF Validation List process")]
        public void GivenINavigateToTheUDFValidationListProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.UDFValidationList));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add a new UDF Validation List")]
        public void GivenIAddANewUDFValidationList()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I complete all the mandatory fields")]
        public void ThenICompleteAllTheMandatoryFields(Table table)
        {
            var entity = table.CreateInstance<UDFEntity>();
            _actor.AttemptsTo(SendKeys.To(UDFLocators.code, entity.Code));
            _actor.AttemptsTo(SendKeys.To(UDFLocators.description, entity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.UDFList] = entity;
        }
     
        [When(@"I add the List items child form and complete all the mandatory fields")]
        public void WhenIAddTheListItemsChildFormAndCompleteAllTheMandatoryFields(Table table)
        {
            var udfEntities = table.CreateSet<UDFEntity>().ToList();
            _actor.AttemptsTo(UDFValidationListData.With(udfEntities));
        }

        [Given(@"I create a udf validation list with child forms")]
        public void GivenICreateAUdfValidationListWithChildForms(Table table)
        {
            var udfEntities = table.CreateSet<UDFEntity>().ToList();

            foreach (var entity in udfEntities)
            {
                GivenINavigateToTheUDFValidationListProcess();
                _actor.AttemptsTo(QuickFind.Search(entity.Code));

                if (!_actor.DoesElementExist(CommonLocator.FindDivElementContainsText(StepConstants.NoRecordsFoundMessage)) || _actor.DoesElementExist(AuditLocators.FindResultRows, 5))
                {
                    //Code Exists. Skipping
                    if(_actor.DoesElementExist(CommonLocator.Close,1))
                        _actor.AttemptsTo(Click.On(CommonLocator.Close));
                    if (_actor.DoesElementExist(CommonLocator.Cancel, 1))
                        _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
                    return;
                }

                //Click add
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                //Input Code and Desc of Validation List
                _actor.AttemptsTo(SendKeys.To(UDFLocators.code, entity.Code));
                _actor.AttemptsTo(SendKeys.To(UDFLocators.description, entity.Description));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                //Creating List for Task to Create Child Form
                var childFormEntityList = new List<UDFEntity>()
                {
                    new UDFEntity()
                    {
                        Code = entity.ValueCode,
                        Description = entity.ValueDescription
                    }
                };
                //Creating Child form using Task
                _actor.AttemptsTo(UDFValidationListData.With(childFormEntityList));

                //Submit
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.WaitsUntil(Appearance.Of(CommonLocator.Submit), IsEqualTo.False());
            }

        }



    }
}
