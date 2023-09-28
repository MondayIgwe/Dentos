using System.Collections.Generic;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.DirectCheque;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using Elite3E.RestServices.Entity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Matter;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.Infrastructure.Selenium;


namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class MatterAttributeSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;


        public MatterAttributeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I add new matter attribute")]
        public void GivenIAddNewMatterAttribute(Table table)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(MatterLocator.MatterAttributeCodeInput, table.Rows[0][ColumnNames.Code]));
            _actor.AttemptsTo(SendKeys.To(MatterLocator.MatterAttributeDescriptionInput, table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _featureContext[ColumnNames.Description] = table.Rows[0][ColumnNames.Description].ToString();
        }

        [StepDefinition(@"I set the finance first step checkbox to true")]
        public void GivenISetTheFinanceFirstStepCheckboxToTrue()
        {
            _actor.AttemptsTo(Checkbox.SetStatus(MatterLocator.MatterAttributeFinanceFirstStepCheckbox, true));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


    }




}
