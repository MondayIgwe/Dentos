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
using Elite3E.PageObjects.Interaction.ProcessInteraction.CoA;
using Elite3E.PageObjects.PageLocators;
using FluentAssertions;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.Infrastructure.Constant;
using OpenQA.Selenium;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class COASteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public COASteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [When(@"I add the coa local account")]
        public void WhenIAddTheCoaLocalAccount(Table table)
        {
            var units = table.CreateSet<CoALocalAccountEntity>().ToList();
            foreach (var coALocalAccount in units)
            {
                _featureContext[StepConstants.CoALocalContext] = coALocalAccount;
                var coaNaturalAccount = _featureContext[StepConstants.CoALocalNaturalContext].ToString();
                coALocalAccount.Natural = coaNaturalAccount;

                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SearchProcess.ByName(Process.LocalAccount));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(QuickFind.Search(coALocalAccount.Unit));

                if (!_actor.DoesElementExist(CommonLocator.SearchFirstRow))
                {
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.True());
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.AttemptsTo(EnterCoALocalAccount.With(coALocalAccount));
                }
                else
                {
                    if (_actor.DoesElementExist(CommonLocator.CloseDialogue)) {
                        _actor.AttemptsTo(JScript.ClickOn(CommonLocator.CloseDialogue));
                        _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    }
                    _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }


            }
        }

        [Then(@"the data is saved")]
        public void ThenTheDataIsSaved()
        {
            var searchText = ((CoALocalAccountEntity)_featureContext[StepConstants.CoALocalContext]).Unit;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.LocalAccount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(searchText));

            var expectedLocalAccount = ((CoALocalAccountEntity)_featureContext[StepConstants.CoALocalContext]);
            var actualLocalAccount = _actor.AsksFor(GetCoALocalAccount.Data());

            actualLocalAccount.Natural.Should().BeEquivalentTo(expectedLocalAccount.Natural);
            actualLocalAccount.Unit.Should().BeEquivalentTo(expectedLocalAccount.Unit);
            actualLocalAccount.FirmDescription.Should().BeEquivalentTo(expectedLocalAccount.FirmDescription);

            if (!string.IsNullOrEmpty(expectedLocalAccount.LocalDescription))
            {
                actualLocalAccount.LocalDescription.Should().BeEquivalentTo(expectedLocalAccount.LocalDescription);
            }
            else
            {
                actualLocalAccount.LocalDescription.Should().NotBeNull();
            }


        }

        [When(@"I search the local account")]
        public void WhenISearchTheLocalAccount()
        {
            var searchText = ((CoALocalAccountEntity)_featureContext[StepConstants.CoALocalContext]).FirmDescription;

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.LocalAccount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(searchText));
        }


        [Then(@"the local description field is correctly named")]
        public void ThenTheLocalDescriptionFieldIsCorrectlyNamed()
        {
            var actualLabel = _actor.AsksFor(Text.Of(CoALocalLocators.LocalDescriptionLabel));

            actualLabel.Should().Be(StepConstants.LocalDescriptionLabel);
        }

        [Then(@"the natural code is displayed")]
        public void ThenTheNaturalCodeIsDisplayed()
        {
            var actualNaturalValue = _actor.AsksFor(Text.Of(CoALocalLocators.FirstNaturalValue));
            var expectedNaturalValue = _featureContext[StepConstants.CoALocalNaturalContext].ToString();
            actualNaturalValue.Trim().Should().BeEquivalentTo(expectedNaturalValue);
        }

        [When(@"I add local description")]
        public void WhenIAddLocalDescription()
        {
            var desc = "auto_" + Guid.NewGuid();

            _featureContext[StepConstants.CoALocalDescriptionContext] = desc;
            _actor.AttemptsTo(SendKeys.To(CoALocalLocators.LocalDescription, desc));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [Then(@"the local description is saved")]
        public void ThenTheLocalDescriptionIsSaved()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.LocalAccount));

            var searchText = ((CoALocalAccountEntity)_featureContext[StepConstants.CoALocalContext]).FirmDescription;

            _actor.AttemptsTo(QuickFind.Search(searchText));

            var expectedDesc = _featureContext[StepConstants.CoALocalDescriptionContext].ToString();
            var actualDesc = _actor.AsksFor(ValueAttribute.Of(CoALocalLocators.LocalDescription));

            expectedDesc.Should().BeEquivalentTo(actualDesc);
        }

        [Given(@"I add the coa legacy")]
        [When(@"I add the coa legacy")]
        public void WhenIAddTheCoaLegacy(Table table)
        {
            var coALegacy = table.CreateInstance<CoALegacyEntity>();
            _featureContext[StepConstants.CoALegacyContext] = coALegacy;
            _featureContext[StepConstants.CoALegacyChart] = coALegacy.FirmDescription;

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.CoALegacy));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(coALegacy.Code));

            if (!_actor.DoesElementExist(CommonLocator.SearchFirstRow))
            {
                _actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.True());
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.LegacyChart));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(EnterCoALegacy.With(coALegacy));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                //add Legacy Details
                _actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.Detail, ChildProcessMenuAction.Add));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(CoALocalLocators.LegacyNatural, coALegacy.Natural + Keys.Tab));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(CoALocalLocators.LegacyFirmDescription, coALegacy.FirmDescription));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.Submit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else
            {
                _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"the coa legacy data is saved")]
        public void ThenTheCoaLegacyDataIsSaved()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.CoALegacy));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var searchText = ((CoALegacyEntity)_featureContext[StepConstants.CoALegacyContext]).FirmDescription;
            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var expectedLegacy = ((CoALegacyEntity)_featureContext[StepConstants.CoALegacyContext]);

            var actualLegacy = _actor.AsksFor(GetCoALegacy.Data());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actualLegacy.Code.Should().BeEquivalentTo(expectedLegacy.Code);
            actualLegacy.FirmDescription.Should().BeEquivalentTo(expectedLegacy.FirmDescription);

            if (string.IsNullOrEmpty(expectedLegacy.LocalDescription))
            {
                string.IsNullOrEmpty(expectedLegacy.LocalDescription).Should().BeTrue();
            }
            else
            {
                actualLegacy.LocalDescription.Should().BeEquivalentTo(expectedLegacy.LocalDescription);
            }
        }

        [When(@"I add local desrciption to coa legacy")]
        public void WhenIAddLocalDesrciptionToCoaLegacy()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var desc = "auto_" + Guid.NewGuid();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.CoALegacyDescriptionContext] = desc;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CoALegacyLocator.LocalDescription, desc));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the local description is saved for coa legacy")]
        public void ThenTheLocalDescriptionIsSavedForcoaLegacy()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.CoALegacy));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var searchText = ((CoALegacyEntity)_featureContext[StepConstants.CoALegacyContext]).FirmDescription;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var expectedDesc = _featureContext[StepConstants.CoALegacyDescriptionContext].ToString();
            var actualDesc = _actor.AsksFor(ValueAttribute.Of(CoALegacyLocator.LocalDescription));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            expectedDesc.Should().BeEquivalentTo(actualDesc);
        }

        [Then(@"the local description field is correctly named in coa legacy")]
        public void ThenTheLocalDescriptionFieldIsCorrectlyNamedInCoaLegacy()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var actualLabel = _actor.AsksFor(Text.Of(CoALegacyLocator.LocalDescriptionLabel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actualLabel.Should().Be(StepConstants.LocalDescriptionLabel);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search a coa mapping ""(.*)""")]
        public void WhenISearchACoaMapping(string searchText)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.CoAMapping));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(searchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the correct columns are dispalyed in mapping detail")]
        public void ThenTheCorrectColumnsAreDispalyedInMappingDetail(Table table)
        {
            var expectedColumnNames = table.Rows.Select(r => r[ColumnNames.GridColumns]);
            var actualColumnNames = _actor.AsksFor(ChildProcessGrid.GetGridColumnsHeaderText(ChildProcessConstants.MappingDetail));
            actualColumnNames.Should().Contain(expectedColumnNames);
            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add a new coa unit")]
        public void GivenIAddANewCoaUnit(Table table)
        {
            var units = table.CreateSet<CoALegacyEntity>().ToList();
            foreach (var coaEntity in units)
            {
                coaEntity.GlUnit = StepHelper.GetRandomString(4, 4);
                var coaLegacyChart = _featureContext[StepConstants.CoALegacyChart].ToString();
                var coaRegion = _featureContext[StepConstants.CoARegionContext].ToString();
                _featureContext[StepConstants.CoAUnitContext] = coaEntity.LocalDescription;

                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SearchProcess.ByName(Process.CoAUnit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(QuickFind.Search(coaEntity.Code));

                if (!_actor.DoesElementExist(CommonLocator.SearchFirstRow))
                {
                    _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Code, coaEntity.Code));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, coaEntity.LocalDescription));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.AttemptsTo(SendKeys.To(CoALocalLocators.GlUnit, coaEntity.GlUnit));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.AttemptsTo(Dropdown.SelectOptionByName(CoALocalLocators.LegacyChart, coaLegacyChart));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.AttemptsTo(Dropdown.SelectOptionByName(CoALocalLocators.CoaRegion, coaRegion));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    _actor.AttemptsTo(Click.On(CommonLocator.Submit));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
                else
                {
                    _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
            }
        }

        [Given(@"I add a new CoA Natural Account")]
        public void GivenIAddANewCoANaturalAccount(Table table)
        {
            var coALegacy = table.CreateInstance<CoALegacyEntity>();

            _featureContext[StepConstants.CoALocalNaturalContext] = coALegacy.Natural;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.CoANatural));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(coALegacy.Natural));

            if (!_actor.DoesElementExist(CommonLocator.SearchFirstRow))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(CoALocalLocators.NaturalAccountInput, coALegacy.Natural));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(CoALocalLocators.FirmDescription, coALegacy.Description));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.Submit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else
            {
                _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }


        [Given(@"I add a CoA mapping")]
        public void GivenIAddACoAMapping()
        {
            string unit = _featureContext[StepConstants.CoAUnitContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.CoAMapping));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CoALocalLocators.UnitInput, unit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CoALocalLocators.ImportLegacyChart));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add a new Region")]
        public void GivenIAddANewRegion(Table table)
        {
            _featureContext[StepConstants.CoARegionContext] = table.Rows[0][ColumnNames.Description];
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.CoaRegion));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(table.Rows[0][ColumnNames.Description]));

            if (!_actor.DoesElementExist(CommonLocator.SearchFirstRow))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Code, table.Rows[0][ColumnNames.Code]));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, table.Rows[0][ColumnNames.Description]));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.Submit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else
            {
                _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"I click clone chart")]
        public void WhenIClickCloneChart(Table table)
        {
            var units = table.Rows.Select(r => r[ColumnNames.Units]);
            var coaLegacyChart = _featureContext[StepConstants.CoALegacyChart].ToString();
            var coaRegion = _featureContext[StepConstants.CoARegionContext].ToString();
            var glunit = StepHelper.GetRandomString(4, 4);
            var code = StepHelper.GetRandomString(6, 6);
            var description = StepHelper.GetRandomString(6, 6);
            _featureContext[StepConstants.CoAUnitContext] = code;

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.CoAUnit));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Code, code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, description));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CoALocalLocators.GlUnit, glunit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CoALocalLocators.LegacyChart, coaLegacyChart));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CoALocalLocators.CoaRegion, coaRegion));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var unit in units)
            {
                _actor.AttemptsTo(Click.On(CoALocalLocators.CloneChart));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.SearchTextBox, unit));
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSearchButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeRadioButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ChargeSelectButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"the unit should be created and modelled")]
        public void ThenTheUnitShouldBeCreatedAndModelled()
        {
            var coaCode = _featureContext[StepConstants.CoAUnitContext].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.CoAUnit));
            _actor.AttemptsTo(QuickFind.Search(coaCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateEntry(coaCode)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }
    }
}
