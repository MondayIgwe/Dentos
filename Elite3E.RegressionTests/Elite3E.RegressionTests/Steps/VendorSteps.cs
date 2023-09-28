using TechTalk.SpecFlow;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using FluentAssertions;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Client;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Fiscal_Invoicing;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChargeTypeGroup;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CostTypeGroup;
using UploadFile = Elite3E.PageObjects.Interaction.CommonInteraction.UploadFile;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.Infrastructure.Selenium;
using OpenQA.Selenium;
using TechTalk.SpecFlow.UnitTestProvider;
using NUnit.Framework;
using System.Linq;
using Elite3E.PageObjects.Interaction.ProcessInteraction.VendorPayeeMaintenance;
using Elite3E.PageObjects.Interaction.ProcessInteraction.VoucherMaintenance;
using System.Collections.Generic;
using Elite3E.PageObjects.Interaction.ProcessInteraction.OperatingUnit;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Extensions;
using Elite3E.PageObjects.Interaction.ProcessInteraction.VendorPayeeMaintenance;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.VendorPayeeMerge;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class CreateNewVendorSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        private readonly IUnitTestRuntimeProvider _unitTestRuntimeProvider;

        public CreateNewVendorSteps(FeatureContext featureContext, IUnitTestRuntimeProvider unitTestRuntimeProvider = null)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
            _unitTestRuntimeProvider = unitTestRuntimeProvider;
        }


        [Given(@"the vendor maintenace process is opened")]
        public void GivenTheVendorMaintenaceProcessIsOpened()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I select the entity")]
        public void WhenISelectTheEntity()
        {
            var firstname = _featureContext[StepConstants.Entity].ToString();
            _actor.AttemptsTo(SendKeys.To(ClientLocators.Entity, firstname));
        }

        [StepDefinition(@"I can verify that there are no duplicates")]
        public void WhenICanVerifyThatThereAreNoDuplicates()
        {
            _actor.AttemptsTo(Click.On(VendorLocators.CheckDuplicates));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var actualMessage = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var expectedMessgae = "No duplicates found";
            actualMessage.Should().Contain(expectedMessgae);
            _actor.WaitsUntil(Appearance.Of(CommonLocator.InformationMessage), IsEqualTo.False());
        }

        [When(@"enter the  vendor details")]
        public void WhenEnterTheVendorDetails(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.Name, table.Rows[0][ColumnNames.Name])); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientLocators.SortName, table.Rows[0][ColumnNames.SortName])); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.UpdateButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can submit the new vendor details")]
        public void ThenICanSubmitTheNewVendorDetails()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var vendor = message.Split(" ")[4];

            _featureContext[StepConstants.Vendor] = vendor;
            _featureContext[StepConstants.VendorNameContext] = vendor;
        }

        [When(@"I search for the vendor")]
        public void WhenISearchForTheVendor()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorMaintenance));
        }

        [Then(@"I can confirm the vendor is created")]
        public void ThenICanConfirmTheVendorIsCreated()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var vendornum = _featureContext[StepConstants.Vendor].ToString();

            _actor.AttemptsTo(QuickFind.Search(vendornum));
        }

        [Then(@"I verify the sections in vendor maintenance")]
        public void ThenIVerifyTheSectionsInVendorMaintenance()
        {
            _actor.DoesElementExist(VendorLocators.Name).Should().Be(true);
            _actor.DoesElementExist(VendorLocators.VendorNumber).Should().Be(true);
            _actor.DoesElementExist(VendorLocators.VendorStatus).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.UDF));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AsksFor(Field.IsAvailable(VendorLocators.UDF)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I create a new vendor via payee maintenance")]
        public void GivenICreateANewMatter()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorPayeeMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I update the vendor")]
        public void UpdateTheVendor(Table table)
        {

            var vendorEntity = table.CreateInstance<VendorEntity>();

            if (!string.IsNullOrEmpty(vendorEntity.Entity))
            {
                _actor.AttemptsTo(SendKeys.To(VendorLocators.Entity, vendorEntity.Entity));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(vendorEntity.Status))
            {
                _actor.AttemptsTo(Click.On(VendorLocators.StatusDropDown));
                _actor.AttemptsTo(Click.On(ReceiptLocator.DropDownSelection(vendorEntity.Status)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"I create the vendor")]
        public void WhenICreateTheVendor(Table table)
        {

            var vendorEntity = table.CreateInstance<VendorEntity>();

            if (!string.IsNullOrEmpty(vendorEntity.Entity))
            {
                _actor.AttemptsTo(SendKeys.To(VendorLocators.Entity, vendorEntity.Entity));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(vendorEntity.Status))
            {
                _actor.AttemptsTo(Click.On(VendorLocators.StatusDropDown));
                _actor.AttemptsTo(Click.On(ReceiptLocator.DropDownSelection(vendorEntity.Status)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(vendorEntity.GlobalVendorValue))
            {
                _actor.AttemptsTo(Click.On(VendorLocators.GlobalVendorDropDown));
                _actor.AttemptsTo(Click.On(ReceiptLocator.DropDownSelection(vendorEntity.GlobalVendorValue)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"a vendor number is assigned")]
        public void VerifyTheMatterNumberIsGenerated()
        {
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var vendorNumber = message.Split(" ")[4];

            _featureContext[StepConstants.MatterNumberContext] = vendorNumber;
        }

        [Given(@"I add a new global vendor")]
        public void GivenIAddANewGlobalVendor(Table table)
        {

            var vendorEntity = table.CreateInstance<VendorEntity>();
            vendorEntity.Code = vendorEntity.Code + "_" + StepHelper.GetRandomString(5, 5);
            vendorEntity.Description = vendorEntity.Description + "_" + StepHelper.GetRandomString(5, 5);
            _featureContext[StepConstants.Vendor] = vendorEntity.Description;

            _actor.AttemptsTo(SearchProcess.ByName(Process.GlobalVendor));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Code, vendorEntity.Code + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, vendorEntity.Description + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var activeCheckbox = _actor.AsksFor(SelectedState.Of(VendorLocators.GetActiveCheckbox));

            if (!activeCheckbox)
            {
                _actor.AttemptsTo(Click.On(VendorLocators.SetActiveCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"I verify the global vendor was created")]
        public void ThenIVerifyTheGlobalVendorWasCreated()
        {
            var vendorDescription = _featureContext[StepConstants.Vendor].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.GlobalVendor));
            _actor.AttemptsTo(QuickFind.Search(vendorDescription));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateEntry(vendorDescription)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the global vendor was updated")]
        public void ThenIVerifyTheGlobalVendorWasUpdated()
        {
            var vendorDescription = _featureContext[StepConstants.Vendor].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.GlobalVendor));
            _actor.AttemptsTo(QuickFind.Search(vendorDescription));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateEntry(vendorDescription)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I edit the global vendor")]
        public void GivenIEditTheGlobalVendor(Table table)
        {

            var vendorEntity = table.CreateInstance<VendorEntity>();
            vendorEntity.Description = vendorEntity.Description + "_" + StepHelper.GetRandomString(5, 5);
            _featureContext[StepConstants.Vendor] = vendorEntity.Description;

            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, vendorEntity.Description + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(VendorLocators.CodeFieldDisabled), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I select an existing vendor")]
        public async Task GivenISelectAnExistingVendor(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();
            var vendorData = new VendorMaintenanceData();
            vendorEntity.Vendor = await vendorData.CreateVendorAsync(vendorEntity.Vendor);
            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorMaintenance));
            _actor.AttemptsTo(QuickFind.Search(vendorEntity.Vendor));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I select a global vendor")]
        public void WhenISelectAGlobalVendor()
        {
            var globalVendor = _featureContext[StepConstants.Vendor].ToString();

            _actor.WaitsUntil(Appearance.Of(VendorLocators.GlobalVendorDropDown), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(VendorLocators.GlobalVendorDropDown));
            _actor.AttemptsTo(Click.On(ReceiptLocator.DropDownSelection(globalVendor)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the vendor is updated with the global vendor")]
        public void ThenIVerifyTheVendorIsUpdatedWithTheGlobalVendor(Table table)
        {

            var vendorEntity = table.CreateInstance<VendorEntity>();
            var globalVendor = _featureContext[StepConstants.Vendor].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorMaintenance));
            _actor.AttemptsTo(QuickFind.Search(vendorEntity.Vendor));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var value = _actor.AskingFor(ValueAttribute.Of(VendorLocators.GlobalVendorField)).Should().BeEquivalentTo(globalVendor);

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I create a new disbursement type")]
        public void GivenICreateANewDisbursementType(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();
            vendorEntity.Code = vendorEntity.Code + "_" + StepHelper.GetRandomString(3, 3);
            vendorEntity.Description = vendorEntity.Description + "_" + StepHelper.GetRandomString(5, 5);

            _featureContext[StepConstants.Vendor] = vendorEntity.Description;

            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementType));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Code, vendorEntity.Code + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ChargeTypeGroupLocators.Description, vendorEntity.Description + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VendorLocators.TransactionType, vendorEntity.TransactionType + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var activeCheckbox = _actor.AsksFor(SelectedState.Of(VendorLocators.GetActiveCheckbox));

            if (!activeCheckbox)
            {
                _actor.AttemptsTo(Click.On(VendorLocators.SetActiveCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            var hardDisbursementCheckbox = _actor.AsksFor(SelectedState.Of(VendorLocators.GetHardDisbursementCheckbox));

            if (!hardDisbursementCheckbox)
            {
                _actor.AttemptsTo(Click.On(VendorLocators.SetHardDisbursementCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            var barristerCheckbox = _actor.AsksFor(SelectedState.Of(VendorLocators.GetBarristerCheckbox));

            if (!barristerCheckbox)
            {
                _actor.AttemptsTo(Click.On(VendorLocators.SetBarristerCheckbox));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [Then(@"I verify the disbursement type is created")]
        public void ThenIVerifyTheDisbursementTypeIsCreated()
        {
            var disbursementType = _featureContext[StepConstants.Vendor].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementType));
            _actor.AttemptsTo(QuickFind.Search(disbursementType));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateEntry(disbursementType)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I select an existing payee")]
        public void GivenISelectAnExistingPayee()
        {
            /*  var vendorEntity = table.CreateInstance<VendorEntity>();
              CreatePayeeMaintenance payee = new CreatePayeeMaintenance();
              vendorEntity.Payee = await payee.SearchOrCreatePayee(vendorEntity.Payee);*/

            var payee = _featureContext[StepConstants.PayeeNameContext].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.PayeeMaintenance));
            _actor.AttemptsTo(QuickFind.Search(payee));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add an attachment")]
        public void WhenIAddAnAttachment(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();

            _actor.AttemptsTo(Click.On(VendorLocators.Attachments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(VendorLocators.AddFile));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(UploadFile.Upload(vendorEntity.File));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CostTypeGroupLocators.OkButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.ValidateFile(vendorEntity.File)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(VendorLocators.CloseButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"the attachment number is shown")]
        public void ThenTheAttachmentNumberIsShown(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();

            _actor.WaitsUntil(Appearance.Of(VendorLocators.ValidateAttachment(vendorEntity.NumberOfAttachments)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        //[Given(@"I add a new voucher")]
        //public void GivenIAddANewVoucher(Table table)
        //{
        //    var vendorEntity = table.CreateInstance<VendorEntity>();
        //    string matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
        //    string payee = _featureContext[StepConstants.PayeeNameContext].ToString();

        //    _featureContext[StepConstants.InvoiceNumberContext] = vendorEntity.InvoiceNumber;
        //    _featureContext[StepConstants.Payee] = vendorEntity.Payee;

        //    vendorEntity.InvoiceNumber = vendorEntity.InvoiceNumber + "_" + StepHelper.GetRandomString(3, 10);
        //    vendorEntity.Payee = (string.IsNullOrEmpty(vendorEntity.Payee)) ? _featureContext[StepConstants.PayeeNameContext].ToString() : vendorEntity.Payee;

        //    _actor.AttemptsTo(SearchProcess.ByName(Process.VoucherMaintenance));
        //    _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
        //    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        //    //change user unit
        //    _actor.AttemptsTo(UpdateOperatingUnit.With(vendorEntity.OperationUnit));

        //    //invoice required field
        //    _actor.AttemptsTo(EnterVoucherMaintenanceData.EnterVoucherMaintenanceRequiredFields(vendorEntity, payee));
        //}

        [Given(@"I add a new voucher with voucher default information")]
        public void GivenIAddANewVoucherWithVoucherDefaultInformation(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();
            string matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();
            string payee = _featureContext[StepConstants.PayeeNameContext].ToString();
            vendorEntity.InvoiceNumber = vendorEntity.InvoiceNumber + "_" + StepHelper.GetRandomString(3, 10);
            vendorEntity.Payee = (string.IsNullOrEmpty(vendorEntity.Payee)) ? _featureContext[StepConstants.PayeeNameContext].ToString() : vendorEntity.Payee;

            _featureContext[StepConstants.InvoiceNumberContext] = vendorEntity.InvoiceNumber;
            _featureContext[StepConstants.Payee] = vendorEntity.Payee;
            _featureContext[StepConstants.InvoiceDate] = vendorEntity.InvoiceDate;

            _actor.AttemptsTo(SearchProcess.ByName(Process.VoucherMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //change user unit
            if (!string.IsNullOrEmpty(vendorEntity.OperationUnit))
                _actor.AttemptsTo(UpdateOperatingUnit.ChangeTheUserUnit(vendorEntity.OperationUnit));

            //invoice required field
            _actor.AttemptsTo(EnterVoucherMaintenanceData.EnterVoucherMaintenanceRequiredFields(vendorEntity, payee));

            if (!string.IsNullOrEmpty(vendorEntity.TransactionType))
            {
                _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.TransactionTypeInput, vendorEntity.TransactionType));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(vendorEntity.VoucherStatus))
            {
                _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherStatus, vendorEntity.VoucherStatus));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            if (!string.IsNullOrEmpty(vendorEntity.Amount))
            {
                _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.InvoiceAmount, vendorEntity.Amount));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }

        [Given(@"I add disbursement card details for voucher")]
        public void GivenIAddDisbursementCardDetailsForVoucher(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();
            string matterNumber = _featureContext[StepConstants.MatterNumberContext].ToString();

            _featureContext[StepConstants.TaxCodeContext] = vendorEntity.InputTaxCode;
            //Disbursement card info
            if (!string.IsNullOrEmpty(vendorEntity.DisbursementType))
            {
                //Change process view and child form default view
                _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.DisbursementCard));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(ChildProcessMenu.ClickOn("Disbursement Card", ChildProcessMenuAction.Add));
                _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.DisbursementCard, GlobalConstants.Form));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.WaitsUntil(Appearance.Of(VendorLocators.PayDate), IsEqualTo.True());
                _actor.AsksFor(ValueAttribute.Of(VendorLocators.PayDate)).Trim().Should().NotBeNullOrEmpty();
                _actor.AsksFor(ValueAttribute.Of(VendorLocators.CurrencyDate)).Trim().Should().NotBeNullOrEmpty();

                _actor.AttemptsTo(EnterDisbursementCardData.EnterDisbursementCardDetails(vendorEntity, matterNumber));

                _featureContext[StepConstants.VoucherAmount] = vendorEntity.VoucherAmount;


            }
        }

        [When(@"I edit the disbursement card in the voucher")]
        public void WhenIEditTheDisbursementCardInTheVoucher(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.DisbursementCard));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.DisbursementCard, GlobalConstants.Grid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(VendorLocators.DisbursementType(vendorEntity.DisbursementType)));
            _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.DisbursementCard, GlobalConstants.Form));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!string.IsNullOrEmpty(vendorEntity.WorkAmount))
            {
                _actor.AttemptsTo(SendKeys.To(VendorLocators.WorkAmount, vendorEntity.WorkAmount));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.DisbursementCard, GlobalConstants.Grid));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [StepDefinition(@"I update the status to ""([^""]*)""")]
        public void GivenIUpdateTheStatusTo(string approved)
        {
            _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.VoucherStatusDropDown, approved));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I add voucher direct-gl information")]
        public void GivenIAddVoucherDirect_GlInformation(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();

            //Change process view and child form default view
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.VoucherDirectGL));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(GlobalConstants.VoucherDirectGL, LocatorConstants.AddButton)));
            _actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.VoucherDirectGL, GlobalConstants.Form));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //enter amount
            _actor.AttemptsTo(SendKeys.To(EntryAndModifyProcessLocators.VoucherDirectGLAmount, vendorEntity.Amount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //enter tax code
            _actor.AttemptsTo(SendKeys.To(VendorLocators.TaxCode, vendorEntity.VoucherGLTaxCode));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //enter GL Account

            // _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Expense GL Account", vendorEntity.GLAccount));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.SearchExpenseGLAccount));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            //_actor.AttemptsTo(QuickFind.Search(vendorEntity.GLAccount));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, vendorEntity.GLAccount + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.QuickFindSearchResults(vendorEntity.GLAccount)));
            _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());



        }


        [StepDefinition(@"I click on Tax button in disbursement card and validate tax record is added in voucher taxes")]
        public void ClickOnTaxNVerifyInVoucherTaxes()
        {
            _actor.AttemptsTo(Click.On(VendorLocators.TaxButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //verifying tax record is added
            _actor.WaitsUntil(Appearance.Of(VendorLocators.TaxCodeInVoucherTaxes(_featureContext[StepConstants.TaxCodeContext].ToString())), IsEqualTo.True());

            //store the tax amount from tax record section
            var taxInputAmount = _actor.AsksFor(Text.Of(VendorLocators.TaxInputAmount));
            _featureContext[StepConstants.TaxInputAmount] = taxInputAmount;
        }


        [Given(@"I enter the barrister fields in voucher maintenance")]
        public void GivenIEnterTheBarristerFieldsInVoucherMaintenance(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();
            //enter barrister values
            _actor.AttemptsTo(EnterBarristerFieldsData.EnterBarristerData(vendorEntity));
        }

        [Given(@"I validate the barrister fields are not mandatory")]
        public void GivenIValidateTheBarristerFieldsAreNotMandatory()
        {
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerGender_Field), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerGender_Required), IsEqualTo.False());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerSeniority_Field), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerSeniority_Required), IsEqualTo.False());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerName_Field), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerName_Required), IsEqualTo.False());
        }

        [Given(@"I validate the barrister fields are mandatory")]
        public void GivenIValidateTheBarristerFieldsAreMandatory()
        {
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerGender_Field), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerGender_Required), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerSeniority_Field), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerSeniority_Required), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerName_Field), IsEqualTo.True());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.BarristerName_Required), IsEqualTo.True());
        }

        [Given(@"I add input amount ""([^""]*)"" in voucher tax card")]
        public void GivenIAddInputAmountInVoucherTaxCard(string inputAmount)
        {
            _actor.AttemptsTo(EnterVoucherTaxesData.EnterVoucherTaxesInfo(inputAmount));
        }

        [Then(@"I verify the voucher is created")]
        public void ThenIVerifyTheVoucherIsCreated()
        {
            var invoiceNumber = _featureContext[StepConstants.InvoiceNumberContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.VoucherMaintenance));
            _actor.AttemptsTo(QuickFind.Search(invoiceNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateEntry(invoiceNumber)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            var voucherAccountNumber = driver.FindElement(EntryAndModifyProcessLocators.VoucherNumber.Query).Text.Trim();
            voucherAccountNumber.Should().NotBeNullOrEmpty();
            _featureContext[StepConstants.VoucherAccountNumber] = voucherAccountNumber;
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [When(@"I quick search by voucher number")]
        public void WhenIQuickSearchByVoucherNumber()
        {
            _actor.AttemptsTo(QuickFind.Search(_featureContext[StepConstants.VoucherAccountNumber].ToString()));
        }

        [When(@"I quick search by voucher number and verify its not present")]
        public void WhenIQuickSearchByVoucherNumberAndVerifyItsNotPresent()
        {
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, _featureContext[StepConstants.VoucherAccountNumber].ToString()));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)), IsEqualTo.True());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(CommonLocator.NoSearchRecords).Should().Be(true);
            _actor.AttemptsTo(Click.On(CommonLocator.CloseButton));

        }


        [Then(@"I verify the sections in voucher maintenance")]
        public void ThenIVerifyTheSectionsInVoucherMaintenance()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(VendorLocators.InvoiceNumber).Should().Be(true);
            _actor.DoesElementExist(VendorLocators.TermsCode).Should().Be(true);
            _actor.DoesElementExist(VendorLocators.PayDate).Should().Be(true);
            _actor.DoesElementExist(VendorLocators.APGLAccount).Should().Be(true);
            _actor.DoesElementExist(VendorLocators.VoucherStatus).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.VoucherDirectGL));
            _actor.AsksFor(Field.IsAvailable(VendorLocators.VoucherDirectGL)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(VendorLocators.DisbursementCard)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(VendorLocators.VoucherTaxes)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(VendorLocators.VoucherWithholdingTax)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(VendorLocators.Voucher1099)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(VendorLocators.ChequesAppliedAgainstVoucher)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I create a cheque on the voucher using pay now")]
        public void ThenICreateAChequeOnTheVoucherUsingPayNow(Table table)
        {
            var entity = table.CreateInstance<ClientRefundEntity>();
            _actor.AttemptsTo(Click.On(VendorLocators.PayNowButton));
            _actor.WaitForBackgroundProcess();

            _actor.WaitsUntil(Appearance.Of(VendorLocators.PayNowTitle), IsEqualTo.True());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Bank Account", entity.BankAccount));
            _actor.AttemptsTo(SendKeys.To(VendorLocators.PayNow_ChequeNumber, entity.ChequeNumber));

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Cheque Template", entity.ChequeTemplate));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Cheque Printer", entity.ChequePrinter));

            _actor.AttemptsTo(Click.On(VendorLocators.PayNow_OK));
            _actor.WaitForBackgroundProcess();
            _actor.WaitsUntil(Appearance.Of(VendorLocators.PayNowTitle), IsEqualTo.False());

            _actor.GetElementAttribute(VendorLocators.CreateChequeCheckbox, "aria-checked").Should().BeEquivalentTo("true");
            _actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.VoucherStatusDropDown, entity.VoucherStatus));
        }

        [Given(@"I navigate to the vendor/payee maintenance and I add a new record")]
        public void GivenINavigateToTheVendorPayeeMaintenanceAndIAddANewRecord()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorPayeeMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I create a new vendor with new entity")]
        public void GivenICreateANewVendorWithNewEntity(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorPayeeMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I add new entity")]
        public void ThenIAddNewEntity(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();
            vendorEntity.OrganisationName = vendorEntity.OrganisationName + "_" + StepHelper.GetRandomString(5, 5);
            _actor.AttemptsTo(EnterEntityInformation.EnterEntityData(vendorEntity));
        }

        [Then(@"I add a new payee")]
        public void ThenIAddANewPayee(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();

            _actor.AttemptsTo(EnterPayeeInformation.EnterPayeeData(vendorEntity));
            if(!string.IsNullOrEmpty(vendorEntity.TaxCertificateDate))
            {
                _featureContext[GlobalConstants.TaxCertificateDate] = vendorEntity.TaxCertificateDate;
            }
            else
            {
                _featureContext[GlobalConstants.TaxCertificateDate] = string.Empty;
            }
        }

        [Then(@"I submit the vendor/payee maintenance")]
        public void ThenISubmitTheVendorPayeeMaintenance()
        {
            //actor.AttemptsTo(JScript.ClickOn(VendorLocators.UpdateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            string message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));

            var vendorNumber = message.Split(" ")[4];
            _featureContext[StepConstants.VendorContext] = vendorNumber;
        }


        [Then(@"I add a new vendor information")]
        public void ThenIAddANewVendorInformation(Table table)
        {
            var vendorEntity = table.CreateInstance<VendorEntity>();

            _actor.AttemptsTo(EnterVendorInformation.EnterVendorData(vendorEntity));
        }

        [Then(@"I verify the vendor is created")]
        public void ThenIVerifyTheVendorIsCreated()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var vendorNumber = _featureContext[StepConstants.VendorContext].ToString();
            var taxCertificateDate = _featureContext[GlobalConstants.TaxCertificateDate].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorPayeeMaintenance));
            _actor.AttemptsTo(QuickFind.Search(vendorNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());


            if (_actor.DoesElementExist(VendorLocators.RowVendor("1").Query))
            {
                _actor.AttemptsTo(Click.On(VendorLocators.RowVendor("0")));
                _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.WaitsUntil(Appearance.Of(EntryAndModifyProcessLocators.ValidateEntry(vendorNumber)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Payee));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(VendorLocators.PaymentTerms), IsEqualTo.True());
            if (!string.IsNullOrEmpty(taxCertificateDate))
            {
                _actor.AskingFor(ValueAttribute.Of(VendorLocators.TaxCertificateDate)).Should().NotBeEmpty();
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Given(@"I open vendor record")]
        public void GivenIOpenVendorRecord()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var vendorNumber = _featureContext[StepConstants.VendorContext].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorMaintenance));
            _actor.AttemptsTo(QuickFind.Search(vendorNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.OldVendorContext] = vendorNumber;
        }

        [When(@"I clone and update vendor")]
        public void WhenICloneAndUpdateVendor()
        {
            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ProcessCloneButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ProcessUpdateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Given(@"I navigate to vendor/payee merge and I add a new record")]
        public void GivenINavigateToVendorPayeeMergeAndIAddANewRecord()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.VendorPayeeMerge));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add vendor/payee merge details")]
        public void ThenIAddVendorPayeeMergeDetails(Table table)
        {
            var FromvendorNumber = _featureContext[StepConstants.OldVendorContext].ToString();
            var TovendorNumber = _featureContext[StepConstants.VendorContext].ToString();

            var vendorPayeeMergeEntity = table.CreateInstance<VendorPayeeMergeEntity>();

            _actor.AttemptsTo(SendKeys.To(VendorPayeeMergeLocators.Comments, vendorPayeeMergeEntity.Comments));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Checkbox.SetStatus(CommonLocator.getCheckBox(StepConstants.VendorMerge), vendorPayeeMergeEntity.VendorMerge.ToBoolean()));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Lookup.SearchAndSelectSingle(StepConstants.FromVendor, FromvendorNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle(StepConstants.ToVendor, TovendorNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(EntryAndModifyProcessLocators.ProcessUpdateButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }



    }
}
