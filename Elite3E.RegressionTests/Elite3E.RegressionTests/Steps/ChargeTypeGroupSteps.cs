using System;
using System.Collections.Generic;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.ChargeTypeGroup;
using Elite3E.PageObjects.Interaction.ProcessInteraction.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CostTypeGroup;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ChargeTypeGroupSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ChargeTypeGroupSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I clone the matter")]
        [Given(@"clone the matter")]
        public void GivenICloneMatter(Table table)
        {
            var matterGen = table.CreateInstance<EntryAndModifyProcessEntity>();
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(QuickFind.Search(matterGen.MatterNumber));

            _actor.AttemptsTo(CloneMatter.With(matterGen));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(MatterLocator.MatterName, matterGen.MatterName));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            try
            {
                matterGen.FeeEarner = _featureContext[StepConstants.FeeEarner].ToString();

                _actor.AttemptsTo(SendKeys.To(MatterLocator.FeeEarner, matterGen.FeeEarner));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);

                var feeEarnerName = driver.FindElement(MatterLocator.FeeEarnerReadOnlyTextBox.Query).Text;

                _featureContext[StepConstants.FeeEarnerName] = feeEarnerName;
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var matterNumber = message.Split(" ")[4];

            _featureContext[StepConstants.MatterNumberContext] = matterNumber;
            Console.WriteLine("Matter Number Generated : " + matterNumber);
        }

        [When(@"I add new charge type group to matter")]
        public void WhenIAddNewChargeTypeGroupToMatter(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var group = table.Rows.Select(r => r[ColumnNames.ChargeTypeGroup]);

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView,StepConstants.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(AddChargeTypeGroupsOnMatter.With(group.ToList()));
        }

        [Then(@"I verify the charge type group is linked to the matter")]
        public void ThenIVerifyChargeTypeGroupIslinkedToMatter(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var chargeTypeGroup= table.Rows.Select(r => r[ColumnNames.ChargeTypeGroup]).FirstOrDefault();

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView,StepConstants.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            // Click the column code in the Grid to make the Cost type as span
            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ClickFirstRowCodeChargeTypeGrid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            
            _actor.WaitsUntil(Existence.Of(ChargeTypeGroupLocators.ValidateChargeTypeGroup(chargeTypeGroup)), IsEqualTo.True());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));

            //_actor.AttemptsTo(ValidateChargeTypeGroupsOnMatter.With(group));
        }

        [Then(@"I verify the charge type group is linked")]
        public void ThenIVerifyChargeTypeGroupIsLinked()
        {
            var chargeType = _featureContext[StepConstants.ChargeTypeGroupContext].ToString();

            // Click the column code in the Grid to make the Cost type as span
            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ClickFirstRowCodeChargeTypeGrid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Existence.Of(ChargeTypeGroupLocators.ValidateChargeTypeGroup(chargeType)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Given(@"I create a new charge type group via matter maintenance")]
        public void GivenICreateANewChargeTypeGroupViaMatterMaintenance(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var chargeTypeEntity = table.CreateInstance<ChargeTypeGroupEntity>();
            chargeTypeEntity.Code = chargeTypeEntity.Code + "_" + StepHelper.GetRandomString(3, 3);
            chargeTypeEntity.Description = chargeTypeEntity.Description + "_" + StepHelper.GetRandomString(5, 5);

            _featureContext[StepConstants.ChargeTypeGroupContext] = chargeTypeEntity.Description;

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.NewChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ExcludeListCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Code, chargeTypeEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, chargeTypeEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());            

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Given(@"I remove a charge type group from a matter")]
        public void GivenIRemoveAChargeTypeGroupFromAMatter(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var chargeTypeEntity = table.CreateInstance<ChargeTypeGroupEntity>();

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            // Click the column code in the Grid to make the Cost type as span
            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ClickFirstRowCodeChargeTypeGrid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ValidateChargeTypeGroup(chargeTypeEntity.ChargeTypeGroup)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.ChargeTypeGroup, ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the charge type group is removed from the matter")]
        public void ThenIVerifyTheChargeTypeGroupIsRemovedFromTheMatter(Table table)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            var chargeTypeEntity = table.CreateInstance<ChargeTypeGroupEntity>();

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            // Click the column code in the Grid to make the Cost type as span
            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ClickFirstRowCodeChargeTypeGrid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

           Action act = () => _actor.WaitsUntil(Existence.Of(ChargeTypeGroupLocators.ValidateChargeTypeGroup(chargeTypeEntity.ChargeTypeGroup)), IsEqualTo.True(),1);
           act.Should().Throw<WaitingException>();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Given(@"I remove all cost and charge types from the matter")]
        public void GivenIRemoveAllCostAndChargeTypesFromTheMatter()
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.MatterMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ExistingChargeTypeGroups));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.DeleteChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CostTypeGroupLocators.ExistingCostTypeGroups));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CostTypeGroupLocators.DeleteCostTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"an error occurs")]
        public void ThenAnErrorOccurs(Table table)
        {
            var error = table.Rows.Select(r => r[ColumnNames.MandatoryField]);
            var actualMessages = _actor.AsksFor(ProcessError.Messages());

            actualMessages.Should().Equal(error);
        }

        [Given(@"I add a new charge type group")]
        public void GivenIAddANewChargeTypeGroup(Table table)
        {
            var chargeTypeGroup = table.CreateInstance<ChargeTypeGroupEntity>();

            chargeTypeGroup.Code = chargeTypeGroup.Code + "_" + StepHelper.GetRandomString(3, 5);
            chargeTypeGroup.Description = chargeTypeGroup.Description + "_" + StepHelper.GetRandomString(5, 5);
            _featureContext[StepConstants.DynamicChargeTypeGroup] = chargeTypeGroup.Description;
            _featureContext[StepConstants.ChargeTypeGroup] = chargeTypeGroup.Code;

            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Code, chargeTypeGroup.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, chargeTypeGroup.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.ExcludeListCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"the new group is added to the child object in charge type")]
        public void ThenTheNewChargeTypeIsAddedToTheChildObjectInChargeType()
        {
            var chargeTypeGroup = _featureContext[StepConstants.ChargeTypeGroup].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(chargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Existence.Of(EntryAndModifyProcessLocators.ValidateEntry(chargeTypeGroup)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Given(@"I select a charge type group")]
        public void GivenISelectAChargeTypeGroup()
        {
            var chargeTypeGroup = _featureContext[StepConstants.ChargeTypeGroup].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(chargeTypeGroup));
        }

        [StepDefinition(@"I can add a charge type to the group")]
        [Then(@"I can add more than one charge type to the group")]
        [When(@"I add a duplicate charge type to a group")]
        public void ThenICanAddAChargeTypeToTheGroup(Table table)
        {
            var chargeTypeDetails = table.Rows.Select(r => r[ColumnNames.ChargeTypeDetails]);

            _actor.AttemptsTo(EnterChargeTypeGroup.With(chargeTypeDetails.ToList()));
        }
        
        [Then(@"I cancel the process")]
        public void ThenICancelTheProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (_actor.DoesElementExist(CommonLocator.Close,2))
                _actor.AttemptsTo(Click.On(CommonLocator.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }
        
        [Then(@"I close the process")]
        public void ThenICloseTheProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptToClick(CommonLocator.ActionClose);
        }

        [When(@"I remove a charge type from a group")]
        public void WhenIRemoveAChargeTypeFromAGroup(Table table)
        {
            var chargeTypeGroup = table.CreateInstance<ChargeTypeGroupEntity>();
            string section = "Charge Type Detail";

            _actor.AttemptsTo(JScript.ClickOn(EntryAndModifyProcessLocators.ValidateDropDownSelection(chargeTypeGroup.ChargeType)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(section, ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"I verify the charge type is removed")]
        public void ThenIVerifyTheChargeTypeIsRemoved(Table table)
        {
            var chargeTypeGroup = _featureContext[StepConstants.ChargeTypeGroup].ToString();
            var chargeTypeEntity = table.CreateInstance<ChargeTypeGroupEntity>();

            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(chargeTypeGroup));

            var i = _actor.DoesElementExist(EntryAndModifyProcessLocators.ValidateDropDownSelection(chargeTypeEntity.ChargeType));
            i.Should().BeFalse();

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the charge type is available")]
        public void ThenTheChargeTypeIsAvailable(Table table)
        {
            var chargeType =table.Rows.Select(r => r[ColumnNames.ChargeType]).FirstOrDefault();
            _actor.WaitsUntil(Existence.Of(EntryAndModifyProcessLocators.ValidateDropDownSelection(chargeType)), IsEqualTo.True(), 1);
        }


        [Given(@"I add new charge types to a group")]
        public void GivenIAddNewChargeTypesToAGroup(Table table)
        {
            var chargeTypeGroup = _featureContext[StepConstants.ChargeTypeGroup].ToString();
            var group = table.Rows.Select(r => r[ColumnNames.ChargeTypeDetails]);

            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var chargeType in group)
            {
                _actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.SelectChargeType(chargeType)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var chargeType in group)
            {
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ValidateEntry(chargeType)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ChargeTypeDetail));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.ChargeTypeDetail, ChildProcessMenuAction.Add));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.SearchTextBox, chargeTypeGroup));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSearchButton));
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeRadioButton));
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSelectButton));
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"the new charge types are added to the charge type group")]
        [Then(@"I verify the charge types in the group")]
        public void ThenTheNewChargeTypesAreAddedToTheChargeTypeGroup(Table table)
        {
            var chargeTypeGroup = _featureContext[StepConstants.ChargeTypeGroup].ToString();
            var group = table.Rows.Select(r => r[ColumnNames.ChargeTypeDetails]);

            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(chargeTypeGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var chargeType in group)
            {
                _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateDropDownSelection(chargeType)), IsEqualTo.True());
            }

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [When(@"I try to add the excluded charge types on proforma edit")]
        public void WhenITryToAddTheExcludedChargeTypesOnProformaEdit(Table table)
        {
            var chargeTypes = table.Rows.Select(r => r[ColumnNames.ChargeTypes]);

            _featureContext[StepConstants.ExcludedChargeTypes] = chargeTypes;

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ChargeDetails));

            //check to see if childform is expanded or not, if expanded move to the next step
            if (_actor.DoesElementExist(EntryAndModifyProcessLocators.ExpandChildProcess(StepConstants.ChargeDetails)))
            {
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ExpandChildProcess(StepConstants.ChargeDetails)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.ChargeDetails, GlobalConstants.FormFull));
            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.AddNewButton(StepConstants.ChargeDetails)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //_actor.AttemptsTo(Click.On(ChargeTypeGroupLocators.NewChargeTypeInput));
            //_actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the excluded charge types are not available")]
        public void ThenTheExcludedChargeTypesAreNotAvailable()
        {
            var chargeTypes = (IEnumerable<string>) _featureContext[StepConstants.ExcludedChargeTypes];

            var availableChargeTypes =
                _actor.AsksFor(GetAllDropdownValues.For(ChargeTypeGroupLocators.ExistingChargeTypeDropDown, CommonLocator.DropDownOptions));
            availableChargeTypes.Should().NotContain(chargeTypes);

        }

        [When(@"I search for the saved charge type")]
        public void WhenISearchForTheSavedChargeType()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeType));

            var chargeType = (ChargeTypeEntity)_featureContext[StepConstants.ChargeTypeContext];

            var searchText = chargeType.Description;

            _actor.AttemptsTo(QuickFind.Search(searchText));
        }

        [Then(@"I verify the sections in charge type group")]
        public void ThenIVerifyTheSectionsInChargeTypeGroup()
        {
            _actor.DoesElementExist(ChargeTypeGroupLocators.Description).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ChargeTypeGroupLocators.ChargeTypeDetail)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


    }
}


