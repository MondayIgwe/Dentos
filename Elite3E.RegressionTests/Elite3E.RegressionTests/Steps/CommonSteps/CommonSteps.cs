using System;
using System.Linq;
using System.Threading;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Region;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.AdditionalChildForms;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Collection;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Instance;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceManager;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ChildForm;
using Elite3E.RestServices.Services.MatterService;
using Elite3E.RestServices.Services.UserRoleManagement;
using FluentAssertions;
using OpenQA.Selenium;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class CommonSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        public IMatterService _matterService = new MatterService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IOpenChildFormService _childFormService = new OpenChildFormService();
        public IProcessDataService _processDataService = new ProcessDataService();
        private IUserRoleManagementService userRoleManagementService = new UserRoleManagementService();
        public IRestResponse _response;
        public CommonSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [StepDefinition(@"I submit it")]
        [StepDefinition(@"submit it")]
        public void GivenISubmitIt()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I terminate the process")]
        [StepDefinition(@"I terminate it")]
        public void WhenITerminateTheProcess()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Terminate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I validate submit was successful")]
        public void ValidateSubmitSuccessful()
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Submit), IsEqualTo.False());
        }

        [StepDefinition(@"I validate terminate was successfull")]
        public void WhenIValidateTerminateWasSuccessfull()
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Terminate), IsEqualTo.False());
        }

        [StepDefinition(@"I validate close was successfull")]
        public void WhenIValidateCloseWasSuccessfull()
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.CLOSE), IsEqualTo.False());
        }

        [StepDefinition(@"I validate post all was successful")]
        public void ValidatePostAllSuccessful()
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.PostAll), IsEqualTo.False(), 60);
        }


        [StepDefinition(@"I update it")]
        public void WhenIUpdateIt()
        {
            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ProcessUpdateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I search for process '(.*)'")]
        public void GivenISearchForProcess(string processName)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(processName));
        }

        [StepDefinition(@"I search for process '(.*)' without add button")]
        public void GivenISearchForProcessWithoutAddButton(string processName)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(processName, false));
        }


        [StepDefinition(@"add additional postings")]
        public void GivenAddAdditionalPostings(Table table)
        {
            var additionalPostings = table.CreateInstance<AdditionalPostingsEntity>();
            _featureContext[StepConstants.AdditionalPostingNarrativeContext] = additionalPostings;
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.AdditionalPostings));

            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(StepConstants.AdditionalPostings, LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(AdditionalPostingsLocator.GlType, additionalPostings.GlType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(additionalPostings.PostingStage))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(AdditionalPostingsLocator.PostingStageList, additionalPostings.PostingStage));
            if (!string.IsNullOrEmpty(additionalPostings.Value))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(AdditionalPostingsLocator.Value, additionalPostings.Value));


            var debit = additionalPostings.Debit.Split("-");
            _actor.AttemptsTo(SendKeys.To(AdditionalPostingsLocator.DebitGlUnit, debit[0] + Keys.Tab));
            _actor.AttemptsTo(SendKeys.To(AdditionalPostingsLocator.DebitGlNatural, debit[1] + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var credit = additionalPostings.Debit.Split("-");
            _actor.AttemptsTo(SendKeys.To(AdditionalPostingsLocator.CreditGlUnit, credit[0] + Keys.Tab));
            _actor.AttemptsTo(SendKeys.To(AdditionalPostingsLocator.CreditGlNatural, credit[1] + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(additionalPostings.Narrative))
                _actor.AttemptsTo(SendKeys.To(AdditionalPostingsLocator.NarrativeTextBox, additionalPostings.Narrative));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Given(@"I add the narrative '(.*)'")]
        public void GivenIAddTheNarrative(string narrative)
        {
            _featureContext[StepConstants.AdditionalPostingNarrativeContext] = narrative;

            _actor.AttemptsTo(SendKeys.To(AdditionalPostingsLocator.NarrativeTextBox, narrative + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [StepDefinition(@"the narrative is saved")]
        public void ThenTheNarrativeIsSaved()
        {
            var expectedNarrative = _featureContext[StepConstants.AdditionalPostingNarrativeContext].ToString();
            _actor.WaitsUntil(Appearance.Of(AdditionalPostingsLocator.NarrativeTextBox), IsEqualTo.True());
            _actor.AsksFor(ValueAttribute.Of(AdditionalPostingsLocator.NarrativeTextBox)).Should().BeEquivalentTo(expectedNarrative);
        }

        [StepDefinition(@"I delete additional postings")]
        public void WhenIDeleteAdditionalPostings()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ChildFormAction(StepConstants.AdditionalPostings, LocatorConstants.DeleteButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"""(.*)"" is mandatory")]
        public void ThenIsMandatory(string fieldName)
        {
            var messages = _actor.AsksFor(ProcessError.Messages());
            messages.Count.Should().Be(1);
            messages[0].Should().StartWith(fieldName);
            messages[0].Should().Contain(StepConstants.MandatoryFieldErrorMessage);
        }

        [StepDefinition(@"I perform quick find for '(.*)'")]
        public void ISearchAndSelectOf(string searchFor)
        {
            _actor.AttemptsTo(QuickFind.Search(searchFor));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Given(@"I add a new process of '([^']*)'")]
        [Given(@"I add a new '(.*)'")]
        public void GivenIAddANew(string processName)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(processName));

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I want to delete the it")]
        public void ThenIWantToDeleteTheRegion()
        {
            _actor.AttemptsTo(DeleteProcess.ClickDelete());
        }

        [Then(@"I want to delete the region i added initially")]
        public void ThenIWantToDeleteTheRegionIAddedInitially()
        {
            var regionCode = ((RegionEntity)_featureContext[StepConstants.RegionEntity]).Code; ;
            _actor.AttemptsTo(SearchProcess.ByName(Process.Region));
            _actor.AttemptsTo(QuickFind.Search(regionCode));

            _actor.AttemptsTo(DeleteProcess.ClickDelete());
        }

        [Then(@"I verify that an error message '([^']*)' is generated")]
        public void ThenIVerifyThatAnErrorMessageIsGeneratedToShowProductionInstanceTypeExists(string message)
        {
            var actualMessages = _actor.AsksFor(ProcessError.Messages());
            actualMessages.Count.Should().Be(1);
            actualMessages[0].Should().Contain(message);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Then(@"I verify that no error message is generated")]
        public void ThenIVerifyThatNoErrorMessageIsGenerated()
        {
            _actor.DoesElementExist(CommonLocator.ErrorIcon).Should().BeFalse();
            _actor.DoesElementExist(CommonLocator.ErrorMessages).Should().BeFalse();
        }


        [Then(@"I want to delete the instance type i added initially")]
        public void ThenIWantToDeleteTheInstanceTypeIAddedInitially()
        {
            var instanceTypeCode = ((InstanceTypeEntity)_featureContext[StepConstants.InstanceTypeEntity]).Code;
            _actor.AttemptsTo(SearchProcess.ByName(Process.InstanceType));
            _actor.AttemptsTo(QuickFind.Search(instanceTypeCode));
            _actor.AttemptsTo(DeleteProcess.ClickDelete());
        }

        [Given(@"I have added a production instance type")]
        public void GivenIHaveAddedAProductionInstanceType(Table table)
        {
            var instanceTypeEntity = table.CreateInstance<InstanceTypeEntity>();
            _featureContext[StepConstants.InstanceTypeEntity] = instanceTypeEntity;
            _actor.AttemptsTo(SearchProcess.ByName(Process.InstanceType));
            _actor.AttemptsTo(QuickFind.Search(instanceTypeEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!_actor.DoesElementExist(CommonLocator.FindDivElementContainsText(instanceTypeEntity.Code)))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(AddInstanceTypeProcess.AddInstanceTypeDetails(instanceTypeEntity));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.WaitsUntil(Appearance.Of(InstanceTypeLocators.GetIsProductionCheckbox), IsEqualTo.False());
            }
            else
            {

                if (_actor.DoesElementExist(CommonLocator.Close))
                    _actor.AttemptsTo(Click.On(CommonLocator.Close));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            }

        }
        [When(@"I want to create a new region process")]
        [Given(@"I have an existing region")]
        public void WhenIWantToCreateANewRegionProcess(Table table)
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var regionEntity = table.CreateInstance<RegionEntity>();
            _featureContext[StepConstants.RegionEntity] = regionEntity;
            _actor.AttemptsTo(SearchProcess.ByName(Process.Region));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchButtonId)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            GlobalConstants.RegionDropdownValues.AddRange(driver.FindElements(CommonLocator.DescriptionColumn.Query).Select(ele => ele.Text).ToList());
            _actor.AttemptsTo(QuickFind.Search(regionEntity.Code));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!_actor.DoesElementExist(CommonLocator.FindDivElementContainsText(regionEntity.Code)))
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(AddRegionProcess.AddRegionDetails(regionEntity));
                _actor.WaitsUntil(Appearance.Of(CommonLocator.GetActiveCheckbox), IsEqualTo.False());
            }
            else
            {
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            }
        }

        [Given(@"I verify the login is successful")]
        public void GivenIVerifyTheLoginIsSuccessful()
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.Homepage), IsEqualTo.True(), 90);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I select an existing record")]
        public void WhenISelectAnExistingRecord()
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SearchButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SelectFirstUnlockedRow.Select());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I select an existing record if present")]
        public void WhenISelectAnExistingRecordIfPresent()
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SearchButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            try
            {
                _actor.AttemptsTo(SelectFirstUnlockedRow.Select());
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            catch
            {
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }


        [StepDefinition(@"I click add")]
        public void ThenIClickAdd()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
        }

        [StepDefinition(@"I add start date")]
        public void WhenIAddStartDate(Table table)
        {
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StartDate, table.Rows[0][ColumnNames.StartDate], 1));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I add end date")]
        public void WhenIAddEndDate(Table table)
        {
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.EndDate, table.Rows[0][ColumnNames.EndDate]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        //this step can be used to validate the GL posting status in disbursement modify/entry , time modify
        [StepDefinition(@"I validate the gl postings for operating unit '([^']*)'")]
        public void WhenIValidateTheGlPostingsForOperatingUnit(string operatingUnit)
        {
            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.GlPostingsButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            int timeout = 24;
            int counter = 0;
            string invoiceStatus = String.Empty;

            while (counter < timeout)
            {
                _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.PostingsInformationHeader), IsEqualTo.True());
                if (_actor.DoesElementExist(EntryAndModifyProcessLocators.GlPostingsStatus))
                {
                    invoiceStatus = _actor.GetElementText(EntryAndModifyProcessLocators.GlPostingsStatus);
                    if (!string.IsNullOrEmpty(invoiceStatus))
                    {
                        invoiceStatus.Should().NotBeEquivalentTo("Waiting for Period to open");
                        invoiceStatus.Should().NotBeEquivalentTo("Error");
                        if (invoiceStatus.Equals("Posted", StringComparison.CurrentCultureIgnoreCase))
                            break;
                    }
                }
                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.CloseGlPostingsButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                Thread.Sleep(TimeSpan.FromSeconds(10));

                _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.GlPostingsButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                counter++;
            }

            _actor.DoesElementExist(EntryAndModifyProcessLocators.GlPostingsStatus).Should().BeTrue();
            _featureContext[StepConstants.JournalManager] = _actor.AsksFor(Text.Of(EntryAndModifyProcessLocators.JournalManager)).Trim();
            invoiceStatus = _actor.GetElementText(EntryAndModifyProcessLocators.GlPostingsStatus);
            invoiceStatus.Should().NotBeNullOrEmpty();
            invoiceStatus.Should().BeEquivalentTo("Posted");
            var glMaskedValues = _actor.AsksFor(TextList.For(EntryAndModifyProcessLocators.GLMaskedValues));
            glMaskedValues.Any(item => item.StartsWith(operatingUnit));
            _featureContext[StepConstants.GLMaskedValues] = glMaskedValues;
            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.CloseGlPostingsButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I locate the submitted entry in '([^']*)' process")]
        public void WhenILocateTheSubmittedEntryInProcess(string process)
        {
            var matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(process));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(matterNumber));
        }
        [Then(@"I cancel it")]
        public void ThenICancelIt()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [StepDefinition(@"I search for a process '([^']*)' and select a pie chart '([^']*)'")]
        public void GivenISearchForAProcessAndSelectAPieChart(string process, string chartName)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SearchIcon));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchInput, process));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ProcessPieChart(chartName)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [When(@"I quick search by ""(.*)"" and verify its not present")]
        public void WhenIQuickSearchAndVerifyItsNotPresent(String Account)
        {
            ISearchAndSelectOf(Account);
            _actor.DoesElementExist(CommonLocator.NoSearchRecords).Should().Be(true);
            _actor.AttemptsTo(Click.On(CommonLocator.CloseButton));
        }
        [StepDefinition(@"I can submit and stay")]
        public void ThenICanSubmitAndStay()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.SubmitStay));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I navigate to the home page")]
        public void InavigateToTheHomePage()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.HomeDashboard));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"Verify the given fields are not present")]
        public void ThenVerifyTheGivenFieldsAreNotPresent(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));

            var fieldList = table.CreateSet<FieldsEntity>();
            foreach (var field in fieldList)
            {
                _actor.DoesElementExist(CommonLocator.FieldLabel(field.FieldName.ToString())).Should().Be(false);
            }
        }

        [StepDefinition(@"Verify the given fields are present")]
        public void ThenVerifyTheGivenFieldsArePresent(Table table)
        {
            var fieldList = table.CreateSet<FieldsEntity>();
            foreach (var field in fieldList)
            {
                _actor.DoesElementExist(CommonLocator.FieldLabel(field.FieldName.ToString())).Should().Be(true);
            }
        }

        [Then(@"I set status")]
        public void ThenISetStatusTo(Table table)
        {
            var status = table.Rows[0][ColumnNames.Status];
            var office = table.Rows[0][ColumnNames.Office];
            var email = table.Rows[0][ColumnNames.Email];
            // switch to stacked view 
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, null));

            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.StatusDropDown, status));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.FindInputElementUsingContainsText(LocatorConstants.Office), office));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ProcessUpdateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (_actor.DoesElementExist(CommonLocator.Warning))
                _actor.AttemptsTo(Click.On(CommonLocator.Warning));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(CommonLocator.EmailAddrDiv, email));

        }
        [Then(@"I want to clone it")]
        public void ThenIWantToCloneIt()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.Button(LocatorConstants.CloneButton)));
        }


        [Then(@"I want to view only '([^']*)' invoices")]
        public void GivenIWantToViewOnlyInvoices(string status)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (status == LocatorConstants.Doubtful)
                _actor.AttemptsTo(Click.On(InvoiceManagerLocators.IsShowDoubtfulCheckbox));
            else
                _actor.AttemptsTo(Click.On(InvoiceManagerLocators.IsShowDisputedCheckbox));

        }
        [Then(@"I want to search for results")]
        public void ThenIWantToSearchForResults()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.InvoiceManagerSearch));
        }
        [Then(@"I verify that the quick find include following")]
        public void ThenIVerifyThatTheQuickFindIncludeFollowing(Table table)
        {
            var fieldList = table.CreateSet<FieldsEntity>();
            foreach (var field in fieldList)
            {
                _actor.DoesElementExist(CommonLocator.FindDivElementContainsExactText(field.FieldName.ToString())).Should().BeTrue();
            }
        }

        [Then(@"I verify below query attributes in advanced find")]
        public void ThenIVerifyBelowQueryAttributesInAdvancedFind(Table table)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.FindDivElementContainsText("Advanced Find")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var attributesList = table.CreateSet<AdvancedFindSearchEntity>();

            foreach (var attribute in attributesList)
            {
                _actor.DoesElementExist(CommonLocator.AdvanceFindSearchAttribute(attribute.SearchAttribute.ToString().ToString())).Should().BeTrue();
            }
        }

        [When(@"I click add button on child form '([^']*)'")]
        public void WhenIClickAddButtonOnChildForm(string childFormName)
        {
            //get section name
            var sectionName = _actor.GetElementText(CommonLocator.processSectionName);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, sectionName));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.FindChildElementUsingText(childFormName)));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(childFormName, LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }




        [Then(@"I want to see only '([^']*)' invoices")]
        public void ThenIWantToSeeOnlyInvoices(string status)
        {
            var resultsFound = false;
            var IsStatusUpdated = false;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            resultsFound = _actor.DoesElementExist(CommonLocator.ButtonElementContainsText("Matter Group Enquiry"), 30);
            if (status == LocatorConstants.Doubtful)
                IsStatusUpdated = _actor.DoesElementExist(CommonLocator.ExactSpanText(LocatorConstants.Doubtful), 30);
            else
                IsStatusUpdated = _actor.DoesElementExist(CommonLocator.ExactSpanText(LocatorConstants.Disputed), 30);
            resultsFound.Should().BeTrue();
            IsStatusUpdated.Should().BeTrue();

        }
        [When(@"I want to open it")]
        public void WhenIWantToOpenIt()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.SearchResultSelect));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("Open Item")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [Then(@"I verify following attributes in advanced find query")]
        public void ThenIVerifyFollowingAttributesInAdvancedFindQuery(Table table)
        {
            var fieldList = table.CreateSet<FieldsEntity>();
            _actor.AttemptsTo(Click.On(CommonLocator.AdvancedFindTab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            foreach (var field in fieldList)
            {
                _actor.DoesElementExist(CommonLocator.AdvancedFindInputAttribute(field.FieldName)).Should().BeTrue();
            }

        }

        [Then(@"I verify the query result attributes")]
        public void ThenIVerifyTheQueryResultAttributes(Table table)
        {
            var fieldList = table.CreateSet<FieldsEntity>();
            _actor.AttemptsTo(Click.On(CommonLocator.AdvancedFindTab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.SearchButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            foreach (var field in fieldList)
            {
                _actor.DoesElementExist(CommonLocator.AdvancedFindResultAttributeHeader(field.FieldName)).Should().BeTrue();
            }
        }

        [When(@"I set the status as '([^']*)'")]
        public void WhenISetTheStatusAs(string status)
        {
            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.StatusDropDown, status));
        }

        [StepDefinition(@"I verify value in the given input field is not present")]
        public void ThenIVerifyValueInTheGivenInputFieldIsNotPresent(Table table)
        {
            var fieldList = table.CreateSet<FieldsEntity>();
            foreach (var field in fieldList)
            {
                _actor.DoesElementExist(CommonLocator.FieldLabel(field.FieldName.ToString())).Should().Be(true);
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Clear.On(MatterLocator.ToTaxArea));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            }
        }

    }
}
