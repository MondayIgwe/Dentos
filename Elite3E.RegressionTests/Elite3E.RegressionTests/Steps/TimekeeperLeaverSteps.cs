using Boa.Constrictor.Screenplay;
using Elite3E.Infrastructure.Selenium;
using Elite3E.RegressionTests.StepHelpers;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Timekeeper;
using FluentAssertions;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Helper;
using System.IO;
using Elite3E.Infrastructure.Extensions;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class TimekeeperLeaverSteps
    {

        private readonly FeatureContext _featureContext;
        private readonly Actor _actor;
        

        public TimekeeperLeaverSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
            
        }

        [StepDefinition(@"I verify the following buttons")]
        public void WhenIVerifyRibbonButtons(Table table)
        {
            List<string> ButtonsList = table.Rows.Select(r => r["Buttons"]).ToList();

            _actor.WaitsUntil(Appearance.Of(CommonLocator.RibbonButtonElements), IsEqualTo.True());
            List<string> buttonsOnScreen = _actor.GetElementTextList(CommonLocator.RibbonButtonElements);
            buttonsOnScreen.ForEach(button => ButtonsList.Any(x => x.ToLower().Equals(button.ToLower())).Should().BeTrue());
        }

        [When(@"I upload an attachment to the timekeeper leaver checklist")]
        public void WhenIAddAnAttachmentToTheTimekeeperLeaverChecklist()
        {
            string attachmentName = "Delete_"+ StepArgumentExtension.ReplaceDynamicValues("{Auto}+25")+".txt";
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.MultiAttachmentsButtons("Attachments")), IsEqualTo.True());

            //Creating txt file for upload
            SystemIOHelper.CreateTextFile(SystemIOHelper.DIR_RESOURCES, attachmentName);
            SystemIOHelper.WriteToTextFile(SystemIOHelper.DIR_RESOURCES, attachmentName, StepArgumentExtension.ReplaceDynamicValues("{Auto}+500"));

            _actor.AttemptsTo(Click.On(TimeKeeperLeaverChecklistLocators.MultiAttachmentsButtons("Attachments")));
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.AddFile), IsEqualTo.True());
            _actor.AttemptsTo(JScript.ClickOn(TimeKeeperLeaverChecklistLocators.AddFile));

            string fileNameWithPath = Path.Combine(SystemIOHelper.DIR_RESOURCES, attachmentName);
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.UploadButton), IsEqualTo.True());

            _actor.ChangeElementVisibility(TimeKeeperLeaverChecklistLocators.InputFile, true);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.InputFile), IsEqualTo.True());
            _actor.GetDriver().FindElement(TimeKeeperLeaverChecklistLocators.InputFile.Query).SendKeys(fileNameWithPath);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //_actor.ChangeElementVisibility(TimeKeeperLeaverChecklistLocators.InputFile, false);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(TimeKeeperLeaverChecklistLocators.MultiAttachmentsButtons("OK")));
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.UploadedFileValidation(attachmentName)), IsEqualTo.True());
            _actor.AttemptsTo(JScript.ClickOn(TimeKeeperLeaverChecklistLocators.CloseAttachmentsButton));

            string attachmentsNumber = _actor.AsksFor(Text.Of(TimeKeeperLeaverChecklistLocators.AttachmentsNumber));
            attachmentsNumber.Should().Be("1");

            SystemIOHelper.DeleteFile(SystemIOHelper.DIR_RESOURCES, attachmentName);

        }

        [StepDefinition(@"I verify timekeeper leaver child form exists")]
        public void ThenIVerifyTimekeeperLeaverChildFormExists(Table table)
        {
            List<string> ChildFormList = table.Rows.Select(r => r["Child Form"]).ToList();
            _actor.AttemptsTo(Click.On(TimeKeeperLeaverChecklistLocators.ChildVerticalMenuButton));
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.ChildVerticalMenuOptionLabels), IsEqualTo.True());
            List<string> childFormsAvailble = _actor.GetElementTextList(TimeKeeperLeaverChecklistLocators.ChildVerticalMenuOptionLabels);
            ChildFormList.ForEach(childForm => childFormsAvailble.Any(x => x.Trim().Equals(childForm.Trim())).Should().BeTrue());
        }

        [StepDefinition(@"I close timekeeper leaver checklist")]
        public void ThenICloseTimekeeperLeaverChecklist()
        {
            _actor.WaitsUntil(Appearance.Of(CommonLocator.RibbonActionClose), IsEqualTo.True());
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.RibbonActionClose));

            if (_actor.DoesElementExist(TimeKeeperLeaverChecklistLocators.CancelDialogYesButton))
                _actor.AttemptsTo(Click.On(TimeKeeperLeaverChecklistLocators.CancelDialogYesButton));
        }

        [When(@"I start a timekeeper leaver workflow")]
        public void WhenIStartATimekeeperLeaverWorkflow(Table table)
        {
            string leaverDate = table.Rows.Select(r => r["LeaverDate"]).ToList()[0];
            string leadFinanceHRClerk = table.Rows.Select(r => r["Lead Finance HR Clerk"]).ToList()[0];
            string leaverNumber = _featureContext[StepConstants.FeeEarner].ToString();
           
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Leaver", leaverNumber));
            _actor.AttemptsTo(DateControl.SelectDate("Leaver Date", leaverDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Lead Finance HR Clerk", leadFinanceHRClerk));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.LeaverClerkInput).Should().NotBeNullOrEmpty();
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.ReadOnlyFeeEarnerName).Should().NotBeNullOrEmpty();
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.ReadOnlyOffice).Should().NotBeNullOrEmpty();
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.ReadOnlyDesc).Should().NotBeNullOrEmpty();
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.ReadOnlySection).Should().NotBeNullOrEmpty();
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.ReadOnlyTitle).Should().NotBeNullOrEmpty();
        }

        [StepDefinition(@"I validate timekeeper leaver section '([^']*)' is readonly")]
        public void WhenIValidateTimekeeperLeaverSectionIsReadonly(string section)
        {
            string sectionNumber;

            if (section.ToLower().Equals("Initial check by Finance".ToLower()))
            {
                sectionNumber = "1";
                ValidateLeaverSection(sectionNumber, true);
                _actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.NoFurtherActionUserCheckbox(""), "class").Contains("disabled").Should().Be(true);
            }
            else if (section.ToLower().Equals("Checks by Leaver or Legal Assistant".ToLower()))
            {
                sectionNumber = "2";
                ValidateLeaverSection(sectionNumber, true);
            }
            else if (section.ToLower().Equals("Final Checks".ToLower()))
            {
                sectionNumber = "3";
                ValidateLeaverSection(sectionNumber, true);
                _actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.LeaverReadyToDepartCheckbox(""), "class").Contains("disabled").Should().Be(true);
            }
        }

        [StepDefinition(@"I validate timekeeper leaver section '([^']*)' is editable")]
        public void WhenIValidateTimekeeperLeaverSectionIsEditable(string section)
        {
            string sectionNumber;

            if (section.ToLower().Equals("Initial check by Finance".ToLower()))
            {
                sectionNumber = "1";
                ValidateLeaverSection(sectionNumber, false);
                _actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.NoFurtherActionUserCheckbox(""), "class").Contains("disabled").Should().Be(false);
            }
            else if (section.ToLower().Equals("Checks by Leaver or Legal Assistant".ToLower()))
            {
                sectionNumber = "2";
                ValidateLeaverSection(sectionNumber, false);
            }
            else if (section.ToLower().Equals("Final Checks".ToLower()))
            {
                sectionNumber = "3";
                ValidateLeaverSection(sectionNumber, false);

                _actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.LeaverReadyToDepartCheckbox(""), "class").Contains("disabled").Should().Be(false);
            }
        }

        private void ValidateLeaverSection(string sectionNumber, bool isReadOnly)
        {
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.TimeChecksCheckBox(sectionNumber, "")), IsEqualTo.True());
            

            _actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.TimeChecksCheckBox(sectionNumber, ""),"class").Contains("disabled").Should().Be(isReadOnly);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.TimeChecksComment(sectionNumber), "disabled")).Should().Be(false);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.TimeChecksUser(sectionNumber), "disabled")).Should().Be(false);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.TimeChecksDate(sectionNumber), "disabled")).Should().Be(false);

            _actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.MatterChecksCheckBox(sectionNumber, ""),"class").Contains("disabled").Should().Be(isReadOnly);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.MatterChecksComment(sectionNumber), "disabled")).Should().Be(false);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.MatterChecksUser(sectionNumber), "disabled")).Should().Be(false);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.MatterChecksDate(sectionNumber), "disabled")).Should().Be(false);

            _actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.ExpenseChecksCheckBox(sectionNumber, ""),"class").Contains("disabled").Should().Be(isReadOnly);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.ExpenseChecksComment(sectionNumber), "disabled")).Should().Be(false);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.ExpenseChecksUser(sectionNumber), "disabled")).Should().Be(false);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.ExpenseChecksDate(sectionNumber), "disabled")).Should().Be(false);

            _actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.WorkFlowChecksCheckBox(sectionNumber, ""),"class").Contains("disabled").Should().Be(isReadOnly);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.WorkFlowChecksComment(sectionNumber), "disabled")).Should().Be(false);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.WorkFlowChecksUser(sectionNumber), "disabled")).Should().Be(false);
            string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.WorkFlowChecksDate(sectionNumber), "disabled")).Should().Be(false);
        }

        [StepDefinition(@"I input data into timekeeper leaver section")]
        public void ThenIInputDataIntoTimekeeperLeaverSection(Table table)
        {
            var tkLeaverEntity = table.CreateInstance<TimekeeperLeaverEntity>();

            string sectionNumber;
            string dummyData = StepArgumentExtension.ReplaceDynamicValues("{Auto}+30");

            if (tkLeaverEntity.Section.ToLower().Equals("Initial check by Finance".ToLower()))
            {
                sectionNumber = "1";
                InputLeaverSectionData(sectionNumber,dummyData, tkLeaverEntity.User, tkLeaverEntity.LeaverDate);
                if(!string.IsNullOrEmpty(tkLeaverEntity.NoFurtherActionRequired))
                {
                    bool isChecked = (tkLeaverEntity.NoFurtherActionRequired.ToLower().Equals("yes")) ? true : false;
                    _actor.AttemptsTo(Checkbox.SetStatus(TimeKeeperLeaverChecklistLocators.NoFurtherActionUserCheckbox("//input[@type='checkbox']"), isChecked));
                    string.IsNullOrEmpty(_actor.GetElementAttribute(TimeKeeperLeaverChecklistLocators.NextActionUserInput, "disabled")).Should().Be(!isChecked);
                }
                else if(!string.IsNullOrEmpty(tkLeaverEntity.NextActionUser))
                {
                    _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Next Action User", tkLeaverEntity.NextActionUser));
                }
            }
            else if (tkLeaverEntity.Section.ToLower().Equals("Checks by Leaver or Legal Assistant".ToLower()))
            {
                sectionNumber = "2";
                InputLeaverSectionData(sectionNumber, dummyData, tkLeaverEntity.User, tkLeaverEntity.LeaverDate);
            }
            else if (tkLeaverEntity.Section.ToLower().Equals("Final Checks".ToLower()))
            {
                sectionNumber = "3";
                InputLeaverSectionData(sectionNumber, dummyData, tkLeaverEntity.User, tkLeaverEntity.LeaverDate);
                if (!string.IsNullOrEmpty(tkLeaverEntity.LeaverReadyToDepart))
                {
                    bool isChecked = (tkLeaverEntity.LeaverReadyToDepart.ToLower().Equals("yes")) ? true : false;
                    _actor.AttemptsTo(Checkbox.SetStatus(TimeKeeperLeaverChecklistLocators.LeaverReadyToDepartCheckbox("//input[@type='checkbox']"), isChecked));
                }
            }
        }

        private void InputLeaverSectionData(string sectionNumber, string dummyData, string user, string leaverDate)
        {
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.TimeChecksCheckBox(sectionNumber, "")), IsEqualTo.True());

            _actor.AttemptsTo(Checkbox.SetStatus(TimeKeeperLeaverChecklistLocators.TimeChecksCheckBox(sectionNumber, "//input[@type='checkbox']"), true));
            _actor.AttemptsTo(SendKeys.To(TimeKeeperLeaverChecklistLocators.TimeChecksComment(sectionNumber), dummyData));
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.TimeChecksUser(sectionNumber)).Should().BeEquivalentTo(user);
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.TimeChecksDate(sectionNumber)).Should().BeEquivalentTo(leaverDate);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Checkbox.SetStatus(TimeKeeperLeaverChecklistLocators.MatterChecksCheckBox(sectionNumber, "//input[@type='checkbox']"), true));
            _actor.AttemptsTo(SendKeys.To(TimeKeeperLeaverChecklistLocators.MatterChecksComment(sectionNumber), dummyData));
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.MatterChecksUser(sectionNumber)).Should().BeEquivalentTo(user);
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.MatterChecksDate(sectionNumber)).Should().BeEquivalentTo(leaverDate);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Checkbox.SetStatus(TimeKeeperLeaverChecklistLocators.ExpenseChecksCheckBox(sectionNumber, "//input[@type='checkbox']"), true));
            _actor.AttemptsTo(SendKeys.To(TimeKeeperLeaverChecklistLocators.ExpenseChecksComment(sectionNumber), dummyData));
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.ExpenseChecksUser(sectionNumber)).Should().BeEquivalentTo(user);
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.ExpenseChecksDate(sectionNumber)).Should().BeEquivalentTo(leaverDate);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Checkbox.SetStatus(TimeKeeperLeaverChecklistLocators.WorkFlowChecksCheckBox(sectionNumber, "//input[@type='checkbox']"), true));
            _actor.AttemptsTo(SendKeys.To(TimeKeeperLeaverChecklistLocators.WorkFlowChecksComment(sectionNumber), dummyData));
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.WorkFlowChecksUser(sectionNumber)).Should().BeEquivalentTo(user);
            _actor.GetElementText(TimeKeeperLeaverChecklistLocators.WorkFlowChecksDate(sectionNumber)).Should().BeEquivalentTo(leaverDate);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

        [StepDefinition(@"I perform timekeeper leaver reassign '([^']*)'")]
        public void WhenIPerformTimekeeperLeaverReassign(string reassginTo)
        {
            _actor.WaitsUntil(Appearance.Of(TimeKeeperLeaverChecklistLocators.ReassignToInput), IsEqualTo.True());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Reassign To", reassginTo));

            var ribbonButtons = _actor.FindAll(CommonLocator.RibbonButtonElements);
            ribbonButtons.Where(x => x.Text.Contains("Reassign")).FirstOrDefault().Click();
        }

        [StepDefinition(@"I perform timekeeper leaver ribbon option '([^']*)'")]
        public void WhenIPerformTimekeeperLeaverRibbonOption(string option)
        {//Save or Terminate
            var ribbonButtons = _actor.FindAll(CommonLocator.RibbonButtonElements);
            ribbonButtons.Where(x => x.Text.ToLower().Contains(option.ToLower())).FirstOrDefault().Click();
        }

        [When(@"I validate timekeeper leaver history entires are '([^']*)'")]
        public void WhenIValidateTimekeeperLeaverHistoryEntiresAre(string historyCount)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView, StepConstants.WorkflowHistory));
            _actor.FindAll(TimeKeeperLeaverChecklistLocators.WorkflowHistoryChildIndexes).Count().Should().Be(int.Parse(historyCount));
        }






    }
}
