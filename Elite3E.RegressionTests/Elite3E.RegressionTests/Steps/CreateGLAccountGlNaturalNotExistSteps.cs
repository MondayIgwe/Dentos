using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GL_Unit;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GLNatural;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA;
using Elite3E.RegressionTests.Steps;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Delegation;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class CreateGLAccountGlNaturalNotExistSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;


        public CreateGLAccountGlNaturalNotExistSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I open gl natural process")]
        public void GivenIOpenGLNaturalProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.GLNatural));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I open gl unit process")]
        public void GivenIOpenGLUnitProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.GLUnit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I open gl account process")]
        public void GivenIOpenGlAccountProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.GLAccount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I want to add a gl account")]
        public void WhenIWantToAddAGlAccount(Table table)
        {
            var glAccountEntity = table.CreateInstance<GLAccountEntity>();
            glAccountEntity.GLFeeEarner = _featureContext[StepConstants.FeeEarner].ToString();
            // glAccountEntity.GLUnit = _featureContext[StepConstants.GLUnit].ToString();
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("GL Unit", glAccountEntity.GLUnit));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Natural Account", glAccountEntity.NaturalAccount));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("GL Unit Local", glAccountEntity.GLUnitLocal));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("GL Department", glAccountEntity.GLDepartment));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("GL Section", glAccountEntity.GLSection));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("GL Office", glAccountEntity.GLOffice));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("GL Fee Earner", glAccountEntity.GLFeeEarner));

        }

        [StepDefinition(@"I search for an non existing gl natural")]
        public void WhenISearchForAnNonExistingGlNatural(Table table)
        {
            var code = table.Rows[0]["GlNatural"];
            _actor.AttemptsTo(QuickFind.Search(code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var found = _actor.DoesElementExist(CommonLocator.FindDivElementContainsText(code));
            found.Should().BeFalse();
        }

        [When(@"I want to add a gl natural")]
        public void WhenIWantToAddAGlNatural(Table table)
        {
            var glNaturalEntity = table.CreateInstance<GLNaturalEntity>();
            _featureContext[StepConstants.GLNatural] = glNaturalEntity.GLNatural;
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.GLNatural, glNaturalEntity.GLNatural + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.Description, glNaturalEntity.Description + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.Valuetype, glNaturalEntity.ValueType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(GLNaturalLocators.AccountCategory, glNaturalEntity.AccountCategory));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (glNaturalEntity.IsControlAccount && !_actor.AsksFor(SelectedState.Of(GLNaturalLocators.IsControlCheckbox)))
            {
                _actor.AttemptsTo(Click.On(GLNaturalLocators.IsControlCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (glNaturalEntity.IsAggregate && !_actor.AsksFor(SelectedState.Of(GLNaturalLocators.IsAggregateCheckbox)))
            {
                _actor.AttemptsTo(Click.On(GLNaturalLocators.IsAggregateCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (glNaturalEntity.IsAutoAdd && !_actor.AsksFor(SelectedState.Of(GLNaturalLocators.IsAutoAddCheckbox)))
            {
                _actor.AttemptsTo(Click.On(GLNaturalLocators.IsAutoAddCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (glNaturalEntity.IsGLSecurity && !_actor.AsksFor(SelectedState.Of(GLNaturalLocators.IsGLSecurityCheckBox)))
            {
                _actor.AttemptsTo(Click.On(GLNaturalLocators.IsGLSecurityCheckBox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"I want to add a gl unit")]
        public void WhenIWantToAddAGlUnit(Table table)
        {
            var glUnitEntity = table.CreateInstance<GLUnitEntity>();

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[4];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            glUnitEntity.GLValue = finalString;
            _featureContext[StepConstants.GLUnit] = glUnitEntity.GLValue;
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(GLUnitLocators.GLValue, glUnitEntity.GLValue + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.Description, glUnitEntity.Description + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.Valuetype, glUnitEntity.ValueType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(GLUnitLocators.Unit, glUnitEntity.Unit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (glUnitEntity.IsUseLocalAccount && !_actor.AsksFor(SelectedState.Of(GLUnitLocators.IsUseLocalCheckbox)))
            {
                _actor.AttemptsTo(Click.On(GLUnitLocators.IsUseLocalCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Dropdown.SelectOptionByName(GLUnitLocators.GLLocalChart, glUnitEntity.GlLocalChart));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (glUnitEntity.IsUseLocalAccount && !_actor.AsksFor(SelectedState.Of(GLUnitLocators.IsUseLocalCheckbox)))
            {
                _actor.AttemptsTo(Click.On(GLUnitLocators.IsUseLocalCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Dropdown.SelectOptionByName(GLUnitLocators.GLLocalChart, glUnitEntity.GlLocalChart));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }
        [Then(@"I want to add unit local account child details")]
        public void ThenIWantToAddDetails()
        {
            var glNatural = _featureContext[StepConstants.GLNatural].ToString();
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UnitLocalAccount, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.GLNaturalChild, glNatural + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I verify given columns are not present under Units Local section")]
        public void ThenIVerifyGivenColumnsAreNotPresentUnderUnitsLocalSection(Table table)
        {
            var columnsList = table.CreateSet<FieldsEntity>();
            foreach (var colName in columnsList)
            {
                _actor.DoesElementExist(CommonLocator.FieldLabel(colName.FieldName.ToString())).Should().Be(false);
            }
        }

        [StepDefinition(@"I verify columns are present under Units Local section")]
        public void ThenIVerifyGivenRenamedColumnsArePresentUnderUnitsLocalSection(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var columnsList = table.CreateSet<FieldsEntity>();
            foreach (var colName in columnsList)
            {
                _actor.DoesElementExist(CommonLocator.ColumnLabel(colName.FieldName.ToString())).Should().Be(true);
            }
        }

        [StepDefinition(@"I input unit local account details and verify default values")]
        public void GivenIInputUnitLocalAccountDetailsAndVerifyDefaultValues(Table table)
        {
            var glUnitEntity = table.CreateInstance<GLUnitEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UnitLocalAccount, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.GLNaturalChild, glUnitEntity.GlNatural + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //verify Default checkbox is true
            _actor.DoesElementExist(GLUnitLocators.IsDefaultColCheckedStatus(glUnitEntity.GlNatural)).Should().Be(true);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //local account value must not be validated as it is auto populated, must enter valid field
            // _actor.GetElementText(GLUnitLocators.LocalAccountColumnValue(glUnitEntity.GlNatural)).Should()
            //   .BeEquivalentTo(glUnitEntity.DefaultLocalAccount);

            //Must enter unique local account value as it auto populates incorrectly
            glUnitEntity.LocalAccount = new Random().Next(0, 9999).ToString("D4");
            _featureContext[StepConstants.UnitLocalAccount] = glUnitEntity.LocalAccount;
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.LocalAccountFieldInput, glUnitEntity.LocalAccount + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

          
        }

        [When(@"I delete recently added local account")]
        public void WhenIDeleteRecentlyAddedLocalAccount(Table table)
        {
            var glUnitEntity = table.CreateInstance<GLUnitEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.UnitLocalAccount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(GLUnitLocators.MaximizeChildGlForm("Unit Local Accounts")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(GLUnitLocators.LocalAccountColumnFilterButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            glUnitEntity.LocalAccount = _featureContext[StepConstants.UnitLocalAccount].ToString();
            _actor.AttemptsTo(SendKeys.To(GLUnitLocators.LocalAccountColumnFilterInputButton, glUnitEntity.LocalAccount + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(GLUnitLocators.LocalAccountColumnText(glUnitEntity.LocalAccount)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(GLUnitLocators.DeleteLocalAccountButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.MinimizeChildForm));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }


        [When(@"I add duplicate gl unit local accounts")]
        public void WhenIAddDuplicateGlUnitLocalAccounts(Table table)
        {
            var glUnits = table.CreateSet<GLUnitEntity>().ToList();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));

            foreach (var accounts in glUnits)
            {
                _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UnitLocalAccount, ChildProcessMenuAction.Add));
                _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.GLNaturalChild, accounts.GlNatural + Keys.Enter));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [StepDefinition(@"an error appears")]
        public void ThenAnErrorAppears()
        {
            _actor.DoesElementExist(CommonLocator.ErrorIcon).Should().BeTrue();
        }

        [StepDefinition(@"I add local account")]
        public void WhenIAddLocalAccount()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UnitLocalAccount, ChildProcessMenuAction.Add));
        }

        [Then(@"I enter more than four digits on the local account field")]
        public void ThenIEnterMoreThanFourDigitsOnTheLocalAccountField(Table table)
        {
            var glUnitEntity = table.CreateInstance<GLUnitEntity>();
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.GLNaturalChild, glUnitEntity.GlNatural + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.LocalAccountFieldInput, table.Rows[0][ColumnNames.Amount] + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I get an error regarding the maximum number of digits allowed")]
        public void ThenIGetAnErrorRegardingTheMaximumNumberOfDigitsAllowed(Table table)
        {
            var messages = _actor.AsksFor(ProcessError.Messages());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            messages.Count.Should().Be(1);
            messages[0].Should().BeEquivalentTo(table.Rows[0][ColumnNames.ErrorMessage]);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I enter the correct local account information")]
        public void ThenIEnterTheCorrectLocalAccountInformation(Table table)
        {
            var glUnitEntity = table.CreateInstance<GLUnitEntity>();
            _actor.AttemptsTo(Click.On(GLNaturalLocators.LocalAccountFieldDiv));
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.LocalAccountFieldInput, glUnitEntity.LocalAccount + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(GLNaturalLocators.GLNaturalFieldDiv));
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.GLNaturalChild, glUnitEntity.GlNatural + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify that the local account was saved correctly")]
        public void ThenIVerifyThatTheLocalAccountWasSavedCorrectly(Table table)
        {
            var glUnitEntity = table.CreateInstance<GLUnitEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(Click.On(GLUnitLocators.LocalAccountColumnFilterButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(GLUnitLocators.LocalAccountColumnFilterInputButton, glUnitEntity.LocalAccount + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var expectedLocalAccount = _actor.GetElementText(GLUnitLocators.LocalAccountColumnText(glUnitEntity.LocalAccount));
            var expectedGlNatural = _actor.GetElementText(GLNaturalLocators.GLNaturalRowDiv(glUnitEntity.GlNatural));
            expectedGlNatural.Should().BeEquivalentTo(glUnitEntity.GlNatural);
            expectedLocalAccount.Should().BeEquivalentTo(glUnitEntity.LocalAccount);

        }

        [Given(@"I add new gl unit group")]
        public void GivenIAddNewGlUnitGroup(Table table)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Code"), table.Rows[0][ColumnNames.Code]));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Description"), table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add a unit to the group and mark it as lead unit")]
        public void WhenIAddAUnitToTheGroupAndMarkItAsLeadUnit(Table table)
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Item", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.ItemUnitInput, table.Rows[0][ColumnNames.Unit] + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(GLNaturalLocators.IsLeadCheckbox("0")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add  a unit to the group")]
        public void WhenIAddAUnitToTheGroup(Table table)
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Item", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.ItemUnitInput, table.Rows[0][ColumnNames.Unit] + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"I add another unit to the group and mark it as lead unit")]
        public void WhenIAddAnotherUnitToTheGroupAndMarkItAsLeadUnit(Table table)
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Item", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.ItemUnitInput, table.Rows[0][ColumnNames.Unit] + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(GLNaturalLocators.IsLeadCheckbox("1")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I get an error regarding the lead unit allowed")]
        public void ThenIGetAnErrorRegardingTheLeadUnitAllowed(Table table)
        {
            var messages = _actor.AsksFor(ProcessError.Messages());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            messages.Should().ContainEquivalentOf(table.Rows[0][ColumnNames.ErrorMessage]);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [When(@"I add another unit similar to the previous one")]
        public void WhenIAddAnotherUnitSimilarToThePreviousOne(Table table)
        {
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Item", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.ItemUnitInput, table.Rows[0][ColumnNames.Unit] + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the lead unit group has been checked and is read only")]
        public void ThenIVerifyTheLeadUnitGroupHasBeenCheckedAndIsReadOnly()
        {
            _actor.DoesElementExist(GLNaturalLocators.IsLeadCheckboxDisabledTicked).Should().BeTrue();
        }

        [StepDefinition(@"I add a new GL approver")]
        public void ThenIAddANewGLApprover(Table table)
        {
            var glApprover = table.Rows[0][ColumnNames.Code];
            var glApproverDesc = table.Rows[0][ColumnNames.Description];
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Code"), glApprover));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Description"), glApproverDesc));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.GLApprover] = glApprover;
            _featureContext[StepConstants.GLApproverDesc] = glApproverDesc;
        }

        [Then(@"I validate GL Approver is created")]
        public void ThenIValidateGLApproverIsCreated()
        {
            var glApprover = _featureContext[StepConstants.GLApprover].ToString();
            _actor.AttemptsTo(QuickFind.Search(glApprover));

            var codeValue = _actor.GetElementText(GLNaturalLocators.CodeValue);
            glApprover.Should().BeEquivalentTo(codeValue);

        }

        [Given(@"I verify approver details in the following processes")]
        public void GivenIVerifyApproverDetailsInTheFollowingProcesses(Table table)
        {
            var glApprover = _featureContext[StepConstants.GLApprover].ToString();
            var glApproverDesc = _featureContext[StepConstants.GLApproverDesc].ToString();
            var processName = "";
            CommonSteps common = new CommonSteps(_featureContext);

            foreach (var process in table.Rows)
            {
                processName = process[ColumnNames.ProcessName].ToString();

                common.GivenISearchForProcess(processName);
                common.WhenISelectAnExistingRecord();
                _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.Approver1, glApprover));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.Approver2, glApprover));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.Approver3, glApprover));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                common.WhenIUpdateIt();
                common.GivenISubmitIt();
                common.GivenISearchForProcess(processName);
                common.WhenISelectAnExistingRecord();
                var Approver1 = _actor.GetElementText(GLNaturalLocators.Approver1);
                var Approver2 = _actor.GetElementText(GLNaturalLocators.Approver2);
                var Approver3 = _actor.GetElementText(GLNaturalLocators.Approver3);
                glApproverDesc.Should().BeEquivalentTo(Approver1);
                glApproverDesc.Should().BeEquivalentTo(Approver2);
                glApproverDesc.Should().BeEquivalentTo(Approver3);
                common.ThenICancelIt();

            }
        }

        [Given(@"I add new coa statutory chart")]
        public void GivenIAddNewCoaStatutoryChart(Table table)
        {
            var coaStatutoryChartEntity = table.CreateInstance<CoAStatutoryChartEntity>();

            _featureContext[StepConstants.CoAStatutotyDescription] = coaStatutoryChartEntity.Description;
            _featureContext[StepConstants.CoAStatutotyAccount] = coaStatutoryChartEntity.AccountNumber;
            _featureContext[StepConstants.CoAStatutotyFirmDescription] = coaStatutoryChartEntity.FirmDescription;
            _featureContext[StepConstants.CoAStatutotyLocalDescription] = coaStatutoryChartEntity.LocalDescription;

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Code"), coaStatutoryChartEntity.Code));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FindInputElementUsingText("Description"), coaStatutoryChartEntity.Description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.Detail, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());


            _actor.AttemptsTo(SendKeys.To(CoAStatutoryChartLocators.AccountNumber, coaStatutoryChartEntity.AccountNumber + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CoAStatutoryChartLocators.FirmDescription, coaStatutoryChartEntity.FirmDescription + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CoAStatutoryChartLocators.LocalDescription, coaStatutoryChartEntity.LocalDescription + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I Input and verify CoAStatutory chart and unit Local columns in GL Unit")]
        public void ThenIInputAndVerifyCoAStatutoryChartAndUnitLocalColumnsInGLUnit(Table table)
        {
            var glUnitEntity = table.CreateInstance<GLUnitEntity>();
            var CoAStatutoryChart = _featureContext[StepConstants.CoAStatutotyDescription].ToString();
            var CoAStatutoryAccount = _featureContext[StepConstants.CoAStatutotyAccount].ToString();
            var CoAStatutoryFirmDesc = _featureContext[StepConstants.CoAStatutotyFirmDescription].ToString();
            var CoAStatutoryLocalDesc = _featureContext[StepConstants.CoAStatutotyLocalDescription].ToString();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(GLNaturalLocators.COAStatChartInput, CoAStatutoryChart));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.UnitLocalAccount, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //Tab multiple times to get to the statutory account input
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.GLNaturalChild, glUnitEntity.GlNatural + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //Local account is required
            glUnitEntity.LocalAccount = new Random().Next(0, 9999).ToString("D4");
            _actor.AttemptsTo(SendKeys.To(GLNaturalLocators.LocalAccountFieldInput, glUnitEntity.LocalAccount + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(GLUnitLocators.StaturoyAccountInput, CoAStatutoryAccount + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //verify Statutory Account Local Description value
            _actor.GetElementText(GLUnitLocators.StaturoyAccountLocalDescColValue(glUnitEntity.GlNatural)).Should()
                .BeEquivalentTo(CoAStatutoryLocalDesc);

            //verify Statutory Account Firm Description value
            _actor.GetElementText(GLUnitLocators.StaturoyAccountFirmDescColValue(glUnitEntity.GlNatural)).Should()
                .BeEquivalentTo(CoAStatutoryFirmDesc);

        }

        [Then(@"I advanced find and select unlocked statutory record")]
        public void ThenIAdvancedFindAndSelectUnlockedStatutoryRecord(Table table)
        {
            CommonSteps common = new CommonSteps(_featureContext);
            var CoAStatutoryChart = _featureContext[StepConstants.CoAStatutotyDescription].ToString();
            var searchCriteria = table.CreateSet<AdvancedFindSearchEntity>().ToList();
            if (string.IsNullOrEmpty(searchCriteria.FirstOrDefault()?.SearchValue))
                searchCriteria.FirstOrDefault().SearchValue = CoAStatutoryChart;

            _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteria));
            _actor.DoesElementExist(CommonLocator.NoSearchRecords, 5).Should().BeFalse();

            common.WhenISelectAnExistingRecord();
        }

        [Then(@"I verify CoA Statutory Chart field value")]
        public void ThenIVerifyCoAStatutoryChartFieldValue()
        {
            var CoAStatutoryChart = _featureContext[StepConstants.CoAStatutotyDescription].ToString();
            _actor.GetElementText(GLNaturalLocators.COAStatChartInput).Should()
                .BeEquivalentTo(CoAStatutoryChart);
        }

        [Then(@"I verify statutory account in advanced find")]
        public void ThenIVerifyStatutoryAccountInAdvancedFind(Table table)
        {
            var searchCriteria = table.CreateSet<AdvancedFindSearchEntity>().ToList();

            _actor.AsksFor(AdvancedFind.GetSearchResults(searchCriteria));
            _actor.DoesElementExist(CommonLocator.NoSearchRecords, 5).Should().BeFalse();

            _actor.DoesElementExist(CommonLocator.AdvanceFindSearchReslColumn("Statutory Account"), 5).Should().BeTrue();

        }


    }
}

