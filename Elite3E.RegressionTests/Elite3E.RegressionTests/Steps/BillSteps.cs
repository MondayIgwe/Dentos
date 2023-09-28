using System;
using System.Linq;
using TechTalk.SpecFlow;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using FluentAssertions;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank;
using Elite3E.Infrastructure.Extensions;
using System.Threading;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.PageLocators;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.ProcessInteraction.BankAccountClientAccount;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bill;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class BillSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public BillSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the '([^']*)' report process")]
        public void GivenINavigateToTheReportProcess(string process)
        {
            _actor.AttemptsTo(SearchProcess.ByName(process, false));
            _actor.AttemptsTo(Click.On(BillingLocators.TImeBillHeader(process)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the presentation fields and their values")]
        public void ThenIVerifyThePresentationFieldsAndTheirValues(Table table)
        {
            RemoveAttribute();

            foreach (var attributes in table.Rows)
            {
                var columnIndex = 0;
                var elementName = attributes[ColumnNames.Name];
                var attributeName = attributes[ColumnNames.AttributeField];

                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("Add Field")));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(BillingLocators.AttributeInput(columnIndex, elementName), attributeName + Keys.Enter));
                _actor.DoesElementExist(BillingLocators.AttributeNameText(attributeName)).Should().BeTrue();
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                RemoveAttribute();
            }
        }

        public void RemoveAttribute()
        {
            if (_actor.DoesElementExist(BillingLocators.RemoveFieldIcon))
            {
                while (_actor.DoesElementExist(BillingLocators.RemoveFieldIcon))
                {
                    _actor.AttemptsTo(Click.On(BillingLocators.RemoveFieldIcon));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
            }
        }

    }
}


