using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using TechTalk.SpecFlow;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.Infrastructure.Helper;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.OverrideSetSystemOptions;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.MyBillableMatters;
using Elite3E.Infrastructure.Entity;
using System.Threading;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.PartialCreditNotes;
using Elite3E.Infrastructure.Constant;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class ProformaGenerationSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public ProformaGenerationSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I navigate to the Billing dashboard")]
        public void GivenINavigateToTheBillingDashboard()
        {
            //_actor.AttemptsTo(Click.On(ProformaGenerationLocator.BillingDashboard));
            //_actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.Billing, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the Proforma Generation process should be named X Proforma Generation")]
        public void ThenTheProformaGenerationProcessShouldBeNamedXProformaGeneration(Table table)
        {
            var url = UrlHelper.GetBaseUrl() + "/process/" + table.Rows[0][ColumnNames.ProcessName];
            _actor.AttemptsTo(Navigate.ToUrl(url));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(ProformaGenerationLocator.XProformaGenerationCard)).Should().Be(true);
            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the X Proforma Generation Process should exist")]
        public void ThenTheXProformaGenerationProcessShouldExist()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(ProformaGenerationLocator.XProformaGeneration)).Should().Be(true);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.HomeDashboard));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the WF Proforma Generation process should exist")]
        public void ThenTheWFProformaGenerationProcessShouldExist(Table table)
        {
            var url = UrlHelper.GetBaseUrl() + "/process/" + table.Rows[0][ColumnNames.ProcessName];
            _actor.AttemptsTo(Navigate.ToUrl(url));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(ProformaGenerationLocator.WFProformaGenerationCard)).Should().Be(true);
            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I navigate to the Override/Set System Options process")]
        public void GivenINavigateToTheOverrideSetSystemOptionsProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.OverrideSetSystemOptions, false));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the ProStatus_ccc should be set to OVERRIDE THIS")]
        public void ThenTheProStatus_CccShouldBeSetToOVERRIDETHIS(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.GroupType("Billing")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards("Allow_Hidden_Cards")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var rowOptionName = "Default workflow proforma generation status";
            var rowOptionValue = "OVERRIDE THIS";

            _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards("ProfStatus_ccc"), 10, "pagedown");
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.OptionName(rowOptionName)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            string option = driver.FindElement(OverrideSetSystemOptionsLocators.OptionName(rowOptionName).Query).Text;
            string systemDefault = driver.FindElement(OverrideSetSystemOptionsLocators.OptionValue(rowOptionValue).Query).Text;

            option.Should().BeEquivalentTo(table.Rows[0][ColumnNames.OptionName]);
            systemDefault.Should().BeEquivalentTo(table.Rows[0][ColumnNames.SystemDefault]);

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the ProStatusOther_ccc should be set to OVERRIDE THIS")]
        public void ThenTheProStatusOther_CccShouldBeSetToOVERRIDETHIS(Table table)
        {
            var rowOptionName = "Default proforma status for closed matters in workflow proforma generation";
            var rowOptionValue = "OVERRIDE THIS";

            _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards("ProfStatusOther_ccc"), 3, "pagedown");
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.OptionNameOther(rowOptionName)));

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            string option = driver.FindElement(OverrideSetSystemOptionsLocators.OptionNameOther(rowOptionName).Query).Text;
            string systemDefault = driver.FindElement(OverrideSetSystemOptionsLocators.OptionNameOther(rowOptionValue).Query).Text;

            option.Should().BeEquivalentTo(table.Rows[0][ColumnNames.OptionName]);
            systemDefault.Should().BeEquivalentTo(table.Rows[0][ColumnNames.SystemDefault]);

            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [StepDefinition(@"I navigate to the Proforma Generation Process")]
        public void WhenINavigateToTheProformaGenerationProcess()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ProformaGeneration));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I click Add")]
        public void WhenIClickAdd()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"the status values are initialsed as per the System Override")]
        public void WhenTheStatusValuesAreInitialsedAsPerTheSystemOverride(Table table)
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            string expectedProfStatus = table.Rows[0][ColumnNames.ProfStatus];
            string expectedStatus = table.Rows[0][ColumnNames.ChangeStatusTo];
            string proformaStatus = driver.FindElement(ProformaGenerationLocator.ProformaStatus.Query).GetAttribute("value");
            string changeStatusTo = driver.FindElement(ProformaGenerationLocator.ChangeStatusTo.Query).GetAttribute("value");

            proformaStatus.Should().BeEquivalentTo(expectedProfStatus);
            changeStatusTo.Should().BeEquivalentTo(expectedStatus);

            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the MyMattersIsStartWF_ccc should be set to true")]
        public void ThenTheMyMattersIsStartWF_CccShouldBeSetToTrue(Table table)
        {
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.GroupType("Billing")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards("Allow_Hidden_Cards")));

            var rowOptionName = "Can the billing workflow be started via My Matters?";
            var rowOptionValue = "True";

            _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards("MyMattersIsStartWF_ccc"), 11, "pagedown");
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.WorkflowOption(rowOptionName)));

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            string option = driver.FindElement(OverrideSetSystemOptionsLocators.WorkflowOption(rowOptionName).Query).Text;
            string systemDefault = driver.FindElement(OverrideSetSystemOptionsLocators.WorkflowOption(rowOptionValue).Query).Text;

            option.Should().BeEquivalentTo(table.Rows[0][ColumnNames.OptionName]);
            systemDefault.Should().BeEquivalentTo(table.Rows[0][ColumnNames.SystemDefault]);

            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"the Start Workflow checkbox should be checked")]
        public void WhenTheStartWorkflowCheckboxShouldBeChecked()
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var isChecked = driver.FindElement(OverrideSetSystemOptionsLocators.StartWorkFlowCheckbox.Query).GetAttribute("checked");
            isChecked.Should().BeEquivalentTo("true");
        }

        [Then(@"the ProfGen_Attach_Proforma_ccc should be set to true")]
        public void ThenTheProfGen_Attach_Proforma_CccShouldBeSetToTrue(Table table)
        {
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.GroupType("Billing")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards("Allow_Hidden_Cards")));

            var rowOptionName = "On proforma generation, produce a PDF of the proforma and attach to the proforma";
            var rowOptionValue = "True";

            _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards("ProfGen_Assign_InvoiceNum"), 10, "pagedown");
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.ProfAttachmentOption(rowOptionName)));

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            string option = driver.FindElement(OverrideSetSystemOptionsLocators.ProfAttachmentOption(rowOptionName).Query).Text;
            string systemDefault = driver.FindElement(OverrideSetSystemOptionsLocators.ProfAttachmentOption(rowOptionValue).Query).Text;

            option.Should().BeEquivalentTo(table.Rows[0][ColumnNames.OptionName]);
            systemDefault.Should().BeEquivalentTo(table.Rows[0][ColumnNames.SystemDefault]);

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the ProfGen_Proforma_Template_ccc should be set to OVERRIDE THIS")]
        public void ThenTheProfGen_Proforma_Template_CccShouldBeSetToOVERRIDETHIS(Table table)
        {
            var rowOptionName = "On proforma generation, if a PDF is to be created this option specifies the template to use";
            var rowOptionValue = "OVERRIDE THIS";

            _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards("ProfGen_Assign_InvoiceNum"), 3, "pagedown");
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.ProfAttachmentTemplateOption(rowOptionName)));

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            string option = driver.FindElement(OverrideSetSystemOptionsLocators.ProfAttachmentTemplateOption(rowOptionName).Query).Text;
            string systemDefault = driver.FindElement(OverrideSetSystemOptionsLocators.ProfAttachmentTemplateOption(rowOptionValue).Query).Text;

            option.Should().BeEquivalentTo(table.Rows[0][ColumnNames.OptionName]);
            systemDefault.Should().BeEquivalentTo(table.Rows[0][ColumnNames.SystemDefault]);

            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"the format including the template are auto populated")]
        public void WhenTheFormatIncludingTheTemplateAreAutoPopulated()
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var template = driver.FindElement(ProformaGenerationLocator.TemplateName.Query).Text;
            var templateformat = driver.FindElement(ProformaGenerationLocator.TemplateNameFormat.Query).Text;

            template.Should().NotBeNullOrEmpty();
            templateformat.Should().BeEquivalentTo(" PDF ");

            _actor.AttemptsTo(JScript.ClickOn(ProformaGenerationLocator.TemplateOptions));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var availableTemplates =
                  _actor.AsksFor(GetAllDropdownValues.For(ProformaGenerationLocator.TemplateDropdown, ProformaGenerationLocator.DropdownOptions));
            availableTemplates.Should().Contain("Banner");

            _actor.AttemptsTo(Click.On(CommonLocator.DialogueCancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the MyMarttrsIsGenerateIndividualProformas_ccc should be set to true")]
        public void ThenTheMyMarttrsIsGenerateIndividualProformas_CccShouldBeSetToTrue(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.GroupType("Billing")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards("Allow_Hidden_Cards")));

            var rowOptionName = "In My Matters, enables the Create Individual Matter Proformas setting";
            var rowOptionValue = "True";

            _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards("MyMattersIsGenerateIndividualProformas_ccc"), 20, "pagedown");
            //_actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.MyMatterIsgeneratedOption(rowOptionName)));

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            string option = driver.FindElement(OverrideSetSystemOptionsLocators.MyMatterIsgeneratedOption(rowOptionName).Query).Text;
            string systemDefault = driver.FindElement(OverrideSetSystemOptionsLocators.MyMatterIsgeneratedOption(rowOptionValue).Query).Text;

            option.Should().BeEquivalentTo(table.Rows[0][ColumnNames.OptionName]);
            systemDefault.Should().BeEquivalentTo(table.Rows[0][ColumnNames.SystemDefault]);

            _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"the Individual documents checkbox is ticked")]
        public void WhenTheIndividualDocumentsCheckboxIsTicked()
        {
            _actor.AttemptsTo(JScript.ClickOn(MyBillableMattersLocators.ProformaOptions));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var isChecked = driver.FindElement(MyBillableMattersLocators.IndividualCheckbox.Query).GetAttribute("checked");
            isChecked.Should().BeEquivalentTo("true");

            _actor.AttemptsTo(Click.On(CommonLocator.Close));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [Then(@"the ProfGen_Proforma_Template_ccc should be set to true")]
        public void ThenTheProfGen_Proforma_Template_CccShouldBeSetToTrue(Table table)
        {
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.GroupType("Billing")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards("Allow_Hidden_Cards")));

            var optionName = table.Rows[0][ColumnNames.OptionName];
            _actor.ScrollIntoElement(OverrideSetSystemOptionsLocators.HiddenCards(optionName), 6, "pagedown");
            _actor.DoesElementExist(OverrideSetSystemOptionsLocators.HiddenCards(optionName)).Should().BeTrue();
            _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.HiddenCards(optionName)));

            //var rowOptionName = "On proforma generation, if a PDF is to be created this option specifies the template to use";
            //_actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.ProfAttachmentTemplateOption(rowOptionName)));
           
            
            var optionDefaultValue = _actor.GetElementText(PartialCreditNotesLocators.ValueBasedOnColNOption(optionName, ColumnNames.UnitOverride));

           // var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            //string option = driver.FindElement(OverrideSetSystemOptionsLocators.ProfAttachmentTemplateOption(rowOptionName).Query).Text;
            string systemDefault = _actor.GetElementText(PartialCreditNotesLocators.ValueBasedOnColNOption(optionName, ColumnNames.FirmOverrideCol));
            //string systemDefault = driver.FindElement(OverrideSetSystemOptionsLocators.ProfAttachmentTemplateOptionFirmOverride(rowOptionName).Query).Text;

           // option.Should().BeEquivalentTo(table.Rows[0][ColumnNames.OptionName]);
            if (string.IsNullOrEmpty(systemDefault) || !systemDefault.Contains("True"))
            {
                _actor.AttemptsTo(Click.On(PartialCreditNotesLocators.ValueBasedOnColNOption(optionName, ColumnNames.FirmOverrideCol)));
               // _actor.AttemptsTo(Click.On(OverrideSetSystemOptionsLocators.ProfAttachmentTemplateOptionFirmOverride(rowOptionName)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(OverrideSetSystemOptionsLocators.ProfAttachmentTemplateOptionFirmOverrideInput, table.Rows[0][ColumnNames.FirmOverride]));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.Submit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else
            {
                systemDefault.Should().BeEquivalentTo(table.Rows[0][ColumnNames.FirmOverride]);
                _actor.AttemptsTo(Click.On(CommonLocator.Cancel));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }

        [When(@"I generate a proforma and get the Attachment Error")]
        public void WhenIGenerateAProformaAndGetTheAttachmentError(Table table)
        {
            var client = table.Rows[0][ColumnNames.Client];
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.MyBillableMatters));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Client", client));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(MyBillableMattersLocators.ProformaSearch));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(MyBillableMattersLocators.FirstRowCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(MyBillableMattersLocators.InfoOnlyButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var message = driver.FindElement(CommonLocator.ToastMessage.Query).Text;
            message.Should().Contain("Error occured while retrieving the template");

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I get the Proforma Generation Attachment Error")]
        public void ThenIGetTheProformaGenerationAttachmentError()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var message = driver.FindElement(CommonLocator.ToastMessage.Query).Text;
            if (!string.IsNullOrEmpty(message))
            {
                message.Should().Contain("Generated proforma");
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the sections in the proforma edit")]
        public void ThenIVerifyTheSectionsInTheProformaEdit()
        {
            _actor.DoesElementExist(ProformaEditLocator.ProformaDate).Should().Be(true);
            _actor.DoesElementExist(ProformaEditLocator.BillFeeEarner).Should().Be(true);
            _actor.DoesElementExist(ProformaEditLocator.RespFeeEarner).Should().Be(true);
            _actor.DoesElementExist(ProformaEditLocator.SpvFeeEarner).Should().Be(true);
            _actor.DoesElementExist(ProformaEditLocator.CollFeeEarner).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ProformaEdit));
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.ProformaTotal)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.ProformaPayer)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.ProformaPayerLayer)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.ApplyAdjustment)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.FeeDetails)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.PresentationParagraph)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.DisbursementDetails)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.ChargeDetails)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.ApplyClientAccount)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.ApplyUnallocated)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.ApplyBOA)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.TemplateOptions)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.DateOverrides)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.ProformaTaxArticle)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.BillingRulesValidationMessages)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ProformaEditLocator.BillingContact)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [Then(@"I want to open the '([^']*)' child form")]
        public void ThenIWantToOpenTheChildForm(string p0)
        {
            _actor.AttemptsTo(ChildProcessView.SwitchToView(StepConstants.FeeDetails, GlobalConstants.FormFull));
        }
      
        [Then(@"I want to combine the time entries\.")]
        public void ThenIWantToCombineTheTimwEntries_()
        {
            throw new PendingStepException();
        }

    }
}
