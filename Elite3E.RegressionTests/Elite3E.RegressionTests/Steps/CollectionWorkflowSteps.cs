using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Collection;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using System;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class CollectionWorkflowSteps
    {
        private readonly FeatureContext _featureContext;
        private readonly Actor _actor;

        public CollectionWorkflowSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }
        [When(@"I search for '([^']*)' workflow")]
        public void WhenISearchForWorkflow(string workflow)
        {
            _actor.AttemptsTo(QuickFind.Search(workflow));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I validate '([^']*)' checkbox is available")]
        public void WhenIValidateCheckboxIsAvailable(string checkbox)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, ""));

            if (_actor.DoesElementExist(CommonLocator.FindChildElementUsingText(GlobalConstants.WorkflowStep)))
            {
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.WorkflowStep));
                _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.WorkflowStep, ChildProcessMenuAction.Add));
                _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.WorkflowStep, "Form"));
            }
            else if (_actor.DoesElementExist(CommonLocator.FindChildElementUsingText(GlobalConstants.CollectionStep)))
            {
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.CollectionStep));
                _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.CollectionStep, ChildProcessMenuAction.Add));
                _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.CollectionStep, "Form"));
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(CollectionWorkflowLocators.IsInvoiceAttachCheckBox).Should().Be(true);
        }

        [Then(@"validate if Include Invoice Attachment checkbox check box is editable")]
        public void ThenValidateIfCheckBoxIsEditable()
        {
            bool initialState = _actor.AsksFor(SelectedState.Of(CollectionWorkflowLocators.IsInvoiceAttachCheckBox));
            _actor.AttemptsTo(Click.On(CollectionWorkflowLocators.IsInvoiceAttachCheckBox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            bool currentState = _actor.AsksFor(SelectedState.Of(CollectionWorkflowLocators.IsInvoiceAttachCheckBox));
            initialState.Should().NotBe(currentState);
        }

        [Then(@"I verify the language field in advanced find")]
        public void ThenIVerifyTheLanguageFieldInAdvancedFind(Table table)
        {
            var searchCriteria = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteria));
            _actor.DoesElementExist(CommonLocator.NoSearchRecords, 5).Should().BeFalse();

        }

        [Given(@"I select an existing client in collection item")]
        public void GivenISelectAnExistingClientInCollectionItem(Table table)
        {
            var clientName = table.Rows[Index.Start][ColumnNames.Client];
            _actor.AttemptsTo(QuickFind.Search(clientName));

            if (_actor.DoesElementExist(CommonLocator.QuickFindSearchResults(clientName)))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.SearchFirstRow));
                _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"I verify ebh fields are present in form view")]
        public void ThenIVerifyEbhFieldsArePresentInFormView()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayerCollectionItem));
            _actor.AttemptsTo(ChildProcessView.SwitchToView("Invoice in Collection", GlobalConstants.Form));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(CollectionWorkflowLocators.ebhInvoiceCommentDiv).Should().BeTrue();
            _actor.DoesElementExist(CollectionWorkflowLocators.ebhInvoiceStatusDiv).Should().BeTrue();
            _actor.DoesElementExist(CollectionWorkflowLocators.ebhInvoicesStatusDateDiv).Should().BeTrue();
        }

        [Then(@"I verify ebh fields are present in grid view")]
        public void ThenIVerifyEbhFieldsArePresentInGridView()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayerCollectionItem));
            _actor.AttemptsTo(ChildProcessView.SwitchToView("Invoice in Collection", GlobalConstants.Grid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(CommonLocator.ExactSpanText("eBH Invoice Status Date")).Should().BeTrue();
            _actor.DoesElementExist(CommonLocator.ExactSpanText("eBH Invoice Status")).Should().BeTrue();
            _actor.DoesElementExist(CommonLocator.ExactSpanText("eBH Invoice Comment")).Should().BeTrue();

        }

        [Then(@"I verify the custom field collection language exists on the main form")]
        public void ThenIVerifyTheCustomFieldCollectionLanguageExistsOnTheMainForm()
        {
            _actor.DoesElementExist(CollectionWorkflowLocators.CustomLanguageFieldInput).Should().BeTrue();
        }

        [Then(@"I verify the custom field collection language exists on the child form colelction step")]
        public void ThenIVerifyTheCustomFieldCollectionLanguageExistsOnTheChildFormColelctionStep()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayerCollectionItem));
            _actor.AttemptsTo(ChildProcessView.SwitchToView("Collection Step", GlobalConstants.Form));
            _actor.DoesElementExist(CollectionWorkflowLocators.CustomLanguageFieldCollectionStepInput).Should().BeTrue();
        }

        [Then(@"I verify that the collection office and the collector inputs are required fields")]
        public void ThenIVerifyThatTheCollectionOfficeAndTheCollectorInputsAreRequiredFields(Table table)
        {
            _actor.GetDriver().FindElement(CollectionWorkflowLocators.CollectorInput.Query).Clear();
            _actor.GetDriver().FindElement(CollectionWorkflowLocators.CollectionOfficeInput.Query).Clear();

            _actor.AttemptsTo(Click.On(CommonLocator.Update));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var messages = _actor.AsksFor(ProcessError.Messages());
            messages.Should().Contain(table.Rows[Index.Start][ColumnNames.ErrorMessage]);

        }

        [Then(@"I verify stock office field is read only")]
        public void ThenIVerifyStockOfficeFieldIsReadOnly()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayerCollectionItem));
            _actor.AttemptsTo(ChildProcessView.SwitchToView("Collection Step", GlobalConstants.Form));
            _actor.DoesElementExist(CollectionWorkflowLocators.BillingOfficeDiv).Should().BeTrue();
        }

        [Then(@"I verify the email override body field exists")]
        public void ThenIVerifyTheEmailOverrideBodyFieldExists()
        {
            _actor.DoesElementExist(CollectionWorkflowLocators.EmailBodyTextarea).Should().BeTrue();
        }




    }
}
