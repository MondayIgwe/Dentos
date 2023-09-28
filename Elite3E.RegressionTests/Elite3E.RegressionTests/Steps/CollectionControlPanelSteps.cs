using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Boa.Constrictor.Screenplay;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Collection;
using Elite3E.Infrastructure.Selenium;
using FluentAssertions;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class CollectionControlPanelSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        public CollectionControlPanelSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];

        }

        [Then(@"I verify the sections in collection control panel")]
        public void ThenIVerifyTheSectionsInCollectionControlPanel()
        {
            if(_actor.DoesElementExist(CommonLocator.Close))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.Close));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.DoesElementExist(CollectionControlPanelLocator.ManageCollectionsByDiv).Should().Be(true);
                _actor.DoesElementExist(CollectionControlPanelLocator.CurrencyTypeDiv).Should().Be(true);
                _actor.DoesElementExist(CollectionControlPanelLocator.CurrencyConversionDateTypeDiv).Should().Be(true);
                _actor.DoesElementExist(CollectionControlPanelLocator.DefaultEmailAddrDiv).Should().Be(true);
                _actor.DoesElementExist(CollectionControlPanelLocator.PayorAndBillingOfficeDiv).Should().Be(true);
            }
            else
            {
                _actor.DoesElementExist(CollectionControlPanelLocator.ManageCollectionsBy).Should().Be(true);
                _actor.DoesElementExist(CollectionControlPanelLocator.CurrencyType).Should().Be(true);
                _actor.DoesElementExist(CollectionControlPanelLocator.CurrencyConversionDateType).Should().Be(true);
                _actor.DoesElementExist(CollectionControlPanelLocator.DefaultEmailAddr).Should().Be(true);
                _actor.DoesElementExist(CollectionControlPanelLocator.PayorAndBillingOffice).Should().Be(true);
            }

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.OfficeCollectionGroupLink));
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.OfficeCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.PracticeGroupCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.DepartmentCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.SectionCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.FeeEarnerCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.PayerCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.ClientCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.MatterCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.DepartmentCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.FeeEarnerOfficeCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.ClientOfficeCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.ClientTypeCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.MatterTypeCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.PayorOfficeCollectionGroupLink)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CollectionControlPanelLocator.OfficeCollectionGroupLink)).Should().Be(true);

        }

        [When(@"I enter the value '([^']*)' in payor and billing office field")]
        public void WhenIEnterTheValueInPayorAndBillingOfficeField(string officeValue)
        {
            _actor.AttemptsTo(SendKeys.To(CollectionControlPanelLocator.PayorAndBillingOffice,officeValue));
        }


        [Then(@"I verify the fields in payor office collection group link child form")]
        public void ThenIVerifyTheFieldsInPayorOfficeCollectionGroupLinkChildForm()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.PayerOfficeCollectionGroupLink));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(StepConstants.PayerOfficeCollectionGroupLink, LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.DoesElementExist(CollectionControlPanelLocator.Payer).Should().Be(true);
            _actor.DoesElementExist(CollectionControlPanelLocator.Office).Should().Be(true);
            _actor.DoesElementExist(CollectionControlPanelLocator.CollectionGroup).Should().Be(true);
            _actor.DoesElementExist(CollectionControlPanelLocator.ManageCollectionsByLevelTye).Should().Be(true);
            _actor.DoesElementExist(CollectionControlPanelLocator.InvoiceAccumulated).Should().Be(true);
            _actor.DoesElementExist(CollectionControlPanelLocator.SortString).Should().Be(true);
            _actor.DoesElementExist(CollectionControlPanelLocator.Active).Should().Be(true);
            _actor.DoesElementExist(CollectionControlPanelLocator.StartDate).Should().Be(true);
            _actor.DoesElementExist(CollectionControlPanelLocator.EndDate).Should().Be(true);
        }

       

    }
}
