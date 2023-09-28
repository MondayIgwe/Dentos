using System;
using System.Collections.Generic;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.CostTypeGroup;
using Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CostTypeGroup;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]

    public class CostTypeGroupSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        private readonly CostTypeGroupEntity _context;

        public CostTypeGroupSteps(FeatureContext featureContext, CostTypeGroupEntity context)
        {
            _featureContext = featureContext;
            _context = context;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I add new cost type group to matter")]
        public void WhenIAddNewCostTypeGroupToMatter(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var costTypeGroup = table.Rows[0][ColumnNames.CostTypeGroup];

            _featureContext[StepConstants.CostTypeGroupNameContext] = costTypeGroup;

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(AddCostTypeGroupsOnMatter.With(costTypeGroup));
        }

        [Then(@"I verify the cost type group is linked to the matter")]
        public void ThenIVerifyCostTypeGroupIslinkedToMatter()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var costTypeGroup = _featureContext[StepConstants.CostTypeGroupNameContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            // Click the column code in the Grid to make the Cost type as span
            _actor.AttemptsTo(Click.On(CostTypeGroupLocators.ClickFirstRowCodeCostTypeGrid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Existence.Of(CostTypeGroupLocators.ValidateCostTypeGroup(costTypeGroup)), IsEqualTo.True());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));

            //_actor.AttemptsTo(ValidateCostTypeGroupsOnMatter.With(group.ToList()));
        }

        [Then(@"I verify the cost type group is linked")]
        public void ThenIVerifyCostTypeGroupIsLinked()
        {
            var costType = _featureContext[StepConstants.CostTypeGroupNameContext].ToString();

            // Click the column code in the Grid to make the Cost type as span
            _actor.AttemptsTo(Click.On(CostTypeGroupLocators.ClickFirstRowCodeCostTypeGrid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Existence.Of(CostTypeGroupLocators.ValidateCostTypeGroup(costType)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I create a new cost type group via matter maintenance")]
        public void GivenICreateANewCostTypeGroupViaMatterMaintenance(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var costTypeEntity = table.CreateInstance<CostTypeGroupEntity>();
            costTypeEntity.Code = costTypeEntity.Code + "_" + StepHelper.GetRandomString(3, 3);
            costTypeEntity.Description = costTypeEntity.Description + "_" + StepHelper.GetRandomString(5, 5);

            _featureContext[StepConstants.CostTypeGroupNameContext] = costTypeEntity.Description;

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CostTypeGroupLocators.NewCostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Code, costTypeEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, costTypeEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ExcludeListCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Approve), IsEqualTo.False());
        }

        [Given(@"I remove a cost type group from a matter")]
        public void GivenIRemoveACostTypeGroupFromAMatter(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var costTypeEntity = table.CreateInstance<CostTypeGroupEntity>();

           _featureContext[StepConstants.CostTypeGroupNameContext] = costTypeEntity.CostTypeGroup;

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CostTypeGroupLocators.ValidateCostTypeGroup(costTypeEntity.CostTypeGroup)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.CostTypeGroup, ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the cost type group is removed from the matter")]
        public void ThenIVerifyTheCostTypeGroupIsRemovedFromTheMatter()
        {
            var matterNumber =_featureContext[StepConstants.MatterNumberContext].ToString();
            var costTypeGroup =_featureContext[StepConstants.CostTypeGroupNameContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            // Click the column code in the Grid to make the Cost type as span
            _actor.AttemptsTo(Click.On(CostTypeGroupLocators.ClickFirstRowCodeCostTypeGrid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

           Action act =() => _actor.WaitsUntil(Existence.Of(CostTypeGroupLocators.ValidateCostTypeGroup(costTypeGroup)), IsEqualTo.True(),1);
           act.Should().Throw<WaitingException>();
        }

        [Given(@"I add a new cost type group")]
        public void GivenIAddANewCostTypeGroup(Table table)
        {
            var costTypeGroup = table.CreateInstance<CostTypeGroupEntity>();

            costTypeGroup.Code = costTypeGroup.Code + "_" + StepHelper.GetRandomString(3, 5);
            costTypeGroup.Description = costTypeGroup.Description + "_" + StepHelper.GetRandomString(5, 5);
            _featureContext[StepConstants.DynamicCostTypeGroup] = costTypeGroup.Description;
            _featureContext[StepConstants.CostTypeGroup] = costTypeGroup.Code;

            _actor.AttemptsTo(SearchProcess.ByName(Process.CostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Code, costTypeGroup.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, costTypeGroup.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ExcludeListCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"the new group is added to the child object in cost type")]
        public void ThenTheNewCostTypeIsAddedToTheChildObjectInCostType()
        {
            var costTypeGroup = _featureContext[StepConstants.CostTypeGroup].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.CostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(costTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Existence.Of(EntryAndModifyProcessLocators.ValidateEntry(costTypeGroup)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Given(@"I select a cost type group")]
        public void GivenISelectACostTypeGroup()
        {
            var costTypeGroup = _featureContext[StepConstants.CostTypeGroup].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.CostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(costTypeGroup));
        }

        [StepDefinition(@"I can add a cost type to the group")]
        [Then(@"I can add more than one cost type to the group")]
        [When(@"I add a duplicate cost type to a group")]
        public void ThenICanAddACostTypeToTheGroup(Table table)
        {
            var costTypeDetails = table.Rows.Select(r => r[ColumnNames.CostTypeDetails]);

            _actor.AttemptsTo(EnterCostTypeGroup.With(costTypeDetails.ToList()));
        }

        [When(@"I remove a cost type from a group")]
        public void WhenIRemoveACostTypeFromAGroup(Table table)
        {
            var costTypeGroup = table.CreateInstance<CostTypeGroupEntity>();
            string section = "Cost Type Detail";

            _actor.AttemptsTo(JScript.ClickOn(EntryAndModifyProcessLocators.ValidateDropDownSelection(costTypeGroup.CostType)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(section, ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"I verify the cost type is removed")]
        public void ThenIVerifyTheCostTypeIsRemoved(Table table)
        {
            var costTypeGroup = _featureContext[StepConstants.CostTypeGroup].ToString();
            var costTypeEntity = table.CreateInstance<CostTypeGroupEntity>();

            _actor.AttemptsTo(SearchProcess.ByName(Process.CostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(costTypeGroup));

           Action act = () => _actor.WaitsUntil(Existence.Of(EntryAndModifyProcessLocators.ValidateDropDownSelection(costTypeEntity.CostType)), IsEqualTo.True(),1);
           act.Should().Throw<WaitingException>();
           
           _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the cost type is available")]
        public void ThenTheCostTypeIsAvailable(Table table)
        {
            var chargeType = table.Rows.Select(r => r[ColumnNames.CostType]).FirstOrDefault();
            _actor.WaitsUntil(Existence.Of(EntryAndModifyProcessLocators.ValidateDropDownSelection(chargeType)), IsEqualTo.True(), 1);
        }


        [Given(@"I add new cost types to a group")]
        public void GivenIAddNewCostTypesToAGroup(Table table)
        {
            var costTypeGroup = _featureContext[StepConstants.CostTypeGroup].ToString();
            var group = table.Rows.Select(r => r[ColumnNames.CostTypeDetails]);
            string search = "cou";

            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementType));
            _actor.AttemptsTo(QuickFind.Search(search));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var costType in group)
            {
                _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.SelectChargeType(costType)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CostTypeDetail));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var costType in group)
            {
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ValidateEntry(costType)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.CostTypeDetail, ChildProcessMenuAction.Add));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.SearchTextBox, costTypeGroup));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSearchButton));
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeRadioButton));
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSelectButton));
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"the new cost types are added to the cost type group")]
        [Then(@"I verify the cost types in the group")]
        public void ThenTheNewCostTypesAreAddedToTheCostTypeGroup(Table table)
        {
            var costTypeGroup = _featureContext[StepConstants.CostTypeGroup].ToString();
            var group = table.Rows.Select(r => r[ColumnNames.CostTypeDetails]);

            _actor.AttemptsTo(SearchProcess.ByName(Process.CostTypeGroup));
            _actor.AttemptsTo(QuickFind.Search(costTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var costType in group)
            {
                _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateDropDownSelection(costType)), IsEqualTo.True());
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }


        [When(@"I try to add the excluded disbursements on proforma edit")]
        public void WhenITryToAddTheExcludedDisbursementsOnProformaEdit(Table table)
        {
            var costTypes = table.Rows.Select(r => r[ColumnNames.DisbursementTypes]);
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            _featureContext[StepConstants.ExcludedDisbursementTypes] = costTypes;

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.DisbursementDetails));
            
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.DisbursementDetails, ChildProcessMenuAction.AddNew));
           
            //check to see if the expand more option is visible, if not, it is already expanded
            if (_actor.DoesElementExist(EntryAndModifyProcessLocators.ExpandChildProcess(GlobalConstants.DisbursementDetails)))
            {
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ExpandChildProcess(GlobalConstants.DisbursementDetails)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
           
        }

        [Then(@"the excluded disbursements are not available")]
        public void ThenTheExcludedDisbursementsAreNotAvailable()
        {
            var costTypes = (IEnumerable<string>)_featureContext[StepConstants.ExcludedDisbursementTypes];
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.NewDisbursementTypeFilter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.DisbursementTypeSearchIcon));

            foreach (var costType in costTypes)
            {
                _actor.FindOne(CommonLocator.SearchByInput).Clear();
                _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, costType + Keys.Enter));
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                Action act = () => _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateEntry(costType)), IsEqualTo.True(), 1);

                act.Should().Throw<WaitingException>();
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.CloseFilter));
        }

        [Then(@"I verify the sections in cost type group")]
        public void ThenIVerifyTheSectionsInCostTypeGroup()
        {
            _actor.DoesElementExist(CostTypeGroupLocators.Description).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(CostTypeGroupLocators.CostTypeDetail)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }
}

