using TechTalk.SpecFlow;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Client;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Selenium;
using FluentAssertions;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Audit;
using System.Linq;
using Elite3E.PageObjects.Interaction.ProcessInteraction.Client;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.InvoiceManager;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter_Notes;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Customer;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.MatterGroupEnquiry;
using System.Data;
using OpenQA.Selenium;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountReceiptRequest;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Time;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class CreateNewClientSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        private bool isEBillingUpdateRequired = false;
        private string _childForm;
        private CreditDetailsEntity _creditDetailsEntity;
        private ClientDefaultsEntity _clientDefaultsEntity;
        private ClientEntity _clientEntity;
        private MatterNoteEntity notesEntity;

        public CreateNewClientSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
            _clientEntity = new ClientEntity();
        }

        [Given(@"the entity person process is opened")]
        public void GivenTheEntityPersonProcessIsOpened()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.EntityPerson));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"the client maintenace process is opened")]
        public void GivenTheClientMaintenaceProcessIsOpened()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientMaintenance));
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I enter the  person entity details")]
        public void WhenIEnterThePersonEntityDetails(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.EntityType, table.Rows[0][ColumnNames.EntityType]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientLocators.FirstName, table.Rows[0][ColumnNames.FirstName])); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientLocators.LastName, table.Rows[0][ColumnNames.LastName])); ;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.FormatCode, table.Rows[0][ColumnNames.FormatCode]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.TabbedView, StepConstants.Relationships));

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Relationship, table.Rows[0][ColumnNames.Relationship]));

            var firstname = table.Rows[0][ColumnNames.FirstName];
            var lastname = table.Rows[0][ColumnNames.LastName];
            _featureContext[StepConstants.Entity] = firstname;
            _featureContext[StepConstants.EntityLastName] = lastname;


        }

        [When(@"enter site details")]
        public void WhenEnterSiteDetails(Table table)
        {

            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Sites", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ScrollToElement.At(ClientLocators.SiteDesc));
            _actor.AttemptsTo(SendKeys.To(ClientLocators.SiteDesc, table.Rows[0][ColumnNames.Description]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.SiteType, table.Rows[0][ColumnNames.SiteType]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Country, table.Rows[0][ColumnNames.Country]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Language, table.Rows[0][ColumnNames.Language]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(ClientLocators.Street));
            _actor.AttemptsTo(SendKeys.To(ClientLocators.Street, table.Rows[0][ColumnNames.Street]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var InvSite = table.Rows[0][ColumnNames.Description];
            _featureContext[StepConstants.Description] = InvSite;
        }

        [When(@"I select the new entity")]
        public void WhenISelectTheNewEntity()
        {
            var entityFirstLastName = _featureContext[StepConstants.Entity].ToString();
            var entityLastName = _featureContext[StepConstants.EntityLastName].ToString();
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Entity", entityFirstLastName + " " + entityLastName));
            _featureContext[StepConstants.Client] = entityFirstLastName + " " + entityLastName;
        }

        [When(@"set the global vendor ""(.*)""")]
        public void WhenSetTheGlobalVendor(string globalVendor)
        {
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.GlobalVendor, globalVendor));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"enter the client details")]
        public void WhenEnterTheClientDetails(Table table)
        {
            _actor.AttemptsTo(SendKeys.To(ClientLocators.OpeningFeeEarner,
                table.Rows[0][ColumnNames.OpeningFeeEarner]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.DateOpened, table.Rows[0][ColumnNames.DateOpened]));

            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.DateOpened, table.Rows[0][ColumnNames.DateOpened]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.Status, table.Rows[0][ColumnNames.Status]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Status, table.Rows[0][ColumnNames.Status]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StatusDate, table.Rows[0][ColumnNames.StatusDate]));

        }


        [Then(@"I can enter the effective dates information an save")]
        public void ThenICanEnterTheEffectiveDatesInformationAnSave(Table table)
        {

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.EffectiveDatedInformation));

            // _actor.AttemptsTo(Click.On(ChildFormNavigationLocators.NavigateToEffectiveDatedInformation));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.BillingFeeEarner, table.Rows[0][ColumnNames.BillingFeeEarner]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.ResponsibleFeeEarner, table.Rows[0][ColumnNames.ResponsibleFeeEarner]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.SupervisorTimeKeeper, table.Rows[0][ColumnNames.SupervisorFeeEarner]));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Office, table.Rows[0][ColumnNames.Office]));

            var invSite = _featureContext[StepConstants.Description].ToString();

            _actor.AttemptsTo(SendKeys.To(ClientLocators.InvoiceSite, invSite));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.UpdateButton)));

            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
            var Client = message.Split(" ")[4];

            _featureContext[StepConstants.ClientNumber] = Client;
        }

        [StepDefinition(@"I search for the client")]
        public void WhenISearchForTheClient()
        {
            var clientNumber = _featureContext[StepConstants.Client].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(QuickFind.Search(clientNumber));
        }

        [StepDefinition(@"I enter client details")]
        public void WhenIAddClientDetails(Table table)
        {
            var entity = _featureContext[StepConstants.Client];
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Entity", entity.ToString()));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.Status, table.Rows[0][ColumnNames.Status]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Status, table.Rows[0][ColumnNames.Status]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StatusDate, table.Rows[0][ColumnNames.StatusDate]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.Country, table.Rows[0][ColumnNames.Country]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Country, table.Rows[0][ColumnNames.Country]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.ClientCurrency, table.Rows[0][ColumnNames.Currency]));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Opening Fee Earner", table.Rows[0][ColumnNames.OpeningFeeEarner]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.Language, table.Rows[0][ColumnNames.Language]));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Language, table.Rows[0][ColumnNames.Language]));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Invoice Site", table.Rows[0][ColumnNames.InvoiceSite]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.DateOpened, table.Rows[0][ColumnNames.DateOpened]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.GetDriver().FindElement(EntryAndModifyProcessLocators.Narrative.Query).SendKeys(table.Rows[0][ColumnNames.Narrative]);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.UpdateButton)));

        }


        [When(@"edit the client details")]
        public void WhenEditTheClientDetails(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Client));
            var client = table.CreateInstance<ClientEntity>();
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!string.IsNullOrEmpty(client.DateOpened))
                _actor.AttemptsTo(SendKeys.To(ClientLocators.DateOpened, client.DateOpened));

            if (!string.IsNullOrEmpty(client.Status))
                _actor.AttemptsTo(SendKeys.To(ClientLocators.Status, client.Status));

            if (!string.IsNullOrEmpty(client.StatusDate))
                _actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StatusDate, client.StatusDate));

            if (!string.IsNullOrEmpty(client.GlobalClientNumber))
            {
                _actor.AttemptsTo(SendKeys.To(ClientLocators.GlobalClientNumber, client.GlobalClientNumber));
                _featureContext[StepConstants.GlobalClientNumber] = client.GlobalClientNumber;
            }
        }

        [When(@"delete the client")]
        [Then(@"delete the client")]
        public void WhenDeleteTheClient()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.DeleteButton)));
        }

        [Then(@"I can submit the new entity details")]
        public void ThenICanSubmitTheNewEntityDetails()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I can submit the new client details")]
        public void ThenICanSubmitTheNewClientDetails()
        {
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I fill client credit details")]
        public void ThenIfillclientcreditdetails(Table table)
        {
            var entity = table.CreateInstance<ClientCreditDetailsEntity>();

            //Validating Form above is entity.FormAbove
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CreditDetails));

            //Is form present
            _actor.GetElementTextList(CommonLocator.ChildFormLabels).Any(x => x.Contains(entity.FormAbove, System.StringComparison.CurrentCultureIgnoreCase)).Should().BeTrue("Child Form Not Found: " + entity.FormAbove);

            //Is form index correct
            var childForms = _actor.GetElementTextList(ClientLocators.FormNavigationCards);
            int index = childForms.FindIndex(x => x.Contains(StepConstants.CreditDetails)) - 1;
            childForms[index].Should().Contain(entity.FormAbove);

            //Adding Credit Details
            _actor.AttemptsTo(Click.On(ClientLocators.ChildFormAddButton(StepConstants.CreditDetails, "Add")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.WaitsUntil(Appearance.Of(ClientLocators.CreditDetails_RiskScore), IsEqualTo.True());
            _actor.AttemptsTo(SendKeys.To(ClientLocators.CreditDetails_RiskScore, entity.RiskScore));
            _actor.AttemptsTo(SendKeys.To(ClientLocators.CreditDetails_CreditScoreRating, entity.CreditScoreRating));
            _actor.AttemptsTo(SendKeys.To(ClientLocators.CreditDetails_CreditLimit, entity.CreditLimit));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.CreditDetails_Currency, entity.Currency));
            _actor.AttemptsTo(SendKeys.To(ClientLocators.CreditDetails_AMLRisk, entity.AMLRisk));
            _actor.AttemptsTo(SendKeys.To(ClientLocators.CreditDetails_FinanceRiskScore, entity.FinanceRiskScore));

        }

        [Then(@"I validate client credit details audit")]
        public void ThenIValidateClientCreditDetailsAudit(Table table)
        {
            var entity = table.CreateInstance<ClientCreditDetailsEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CreditDetails));
            ValidateText(ClientLocators.CreditDetails_RiskScore, entity.RiskScore);
            ValidateText(ClientLocators.CreditDetails_CreditScoreRating, entity.CreditScoreRating);
            ValidateText(ClientLocators.CreditDetails_CreditLimit, entity.CreditLimit);
            ValidateText(ClientLocators.CreditDetails_Currency, entity.Currency);
            ValidateText(ClientLocators.CreditDetails_AMLRisk, entity.AMLRisk);
            ValidateText(ClientLocators.CreditDetails_FinanceRiskScore, entity.FinanceRiskScore);

            _actor.AttemptsTo(Click.On(ClientLocators.ChildFormAddButton(StepConstants.CreditDetails, "Audit")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(ClientLocators.AuditCreditDetailsHeader), IsEqualTo.True());

            _actor.AttemptsTo(Click.On(ClientLocators.AuditCreditDetailsExpand));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.FindAll(ClientLocators.AuditCreditDetailsRows).Count.Should().BeGreaterThanOrEqualTo(2);

            _actor.AttemptsTo(Click.On(AuditLocators.AuditCloseButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        private void ValidateText(IWebLocator loc, string text)
        {
            _actor.GetElementText(loc).Should().Be(text);
        }

        [Then(@"I update the '([^']*)' child forms with the details:")]
        [Then(@"I add another '([^']*)' child forms with the details:")]
        public void ThenIUpdateTheChildFormsWithTheDetails(string childForm, Table table)
        {
            _childForm = childForm;
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Client));
            _actor.AttemptsTo(JScript.ClickOn(ClientLocators.ClientForm(_childForm)));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(_childForm, LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (childForm == StepConstants.CreditDetails)
            {
                _creditDetailsEntity = table.CreateInstance<CreditDetailsEntity>();
                _clientEntity.CreditDetailsEntity = _creditDetailsEntity;
                _actor.AttemptsTo(UpdateClientMaintenance.EditClientMaintenance(childForm, _clientEntity));
            }
            else if (childForm == StepConstants.ClientDefaults)
            {
                _clientDefaultsEntity = table.CreateInstance<ClientDefaultsEntity>();
                _clientEntity.ClientDefaultsEntity = _clientDefaultsEntity;
                _actor.AttemptsTo(UpdateClientMaintenance.EditClientMaintenance(childForm, _clientEntity));
            }
        }

        [Then(@"I update the '([^']*)' child forms with the details:")]
        public void ThenIUpdateTheChildFormsWithTheDetails(string childForm)
        {
            _childForm = childForm;
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Client));
            _actor.GetElementTextList(CommonLocator.ChildFormLabels).Any(x => x.Contains(_childForm, System.StringComparison.CurrentCultureIgnoreCase)).Should().BeTrue("Child Form Not Found: " + _childForm);

            _actor.AttemptsTo(JScript.ClickOn(ClientLocators.ClientForm(_childForm)));

            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(_childForm, LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!_actor.AsksFor(SelectedState.Of(ClientLocators.IsEBillingUpdateRequiredCheckbox)))
                isEBillingUpdateRequired = true;
            _actor.AttemptsTo(JScript.ClickOn(ClientLocators.IsEBillingUpdateRequiredCheckbox));
        }

        [Then(@"I verify that the '([^']*)' field is correctly edited")]
        public void ThenIVerifyThatFieldIsCorrectlyEdited(string field)
        {
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Client));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            switch (field)
            {
                case StepConstants.GlobalClientNumber:
                    {
                        var globalClientNumber = _featureContext[StepConstants.GlobalClientNumber].ToString();
                        string savedGlobalClientNumber = _actor.GetElementText(ClientLocators.GlobalClientNumber);
                        savedGlobalClientNumber.Should().BeEquivalentTo(globalClientNumber);
                        break;
                    }
                case StepConstants.BillingAccrualUpdateRequired:
                    _actor.AttemptsTo(JScript.ClickOn(ClientLocators.ClientForm(_childForm)));
                    _actor.AsksFor(SelectedState.Of(ClientLocators.IsEBillingUpdateRequiredCheckbox)).Should().Be(isEBillingUpdateRequired);
                    break;
                case StepConstants.CreditDetails:
                    _actor.AttemptsTo(JScript.ClickOn(ClientLocators.ClientForm(_childForm)));

                    _actor.GetElementText(ClientLocators.RiskScore).Should().BeEquivalentTo(_creditDetailsEntity.RiskScore);
                    _actor.GetElementText(ClientLocators.CreditScoreRating).Should().BeEquivalentTo(_creditDetailsEntity.CreditScoreRating);
                    _actor.GetElementText(ClientLocators.CreditLimit).Should().BeEquivalentTo(_creditDetailsEntity.CreditLimit);
                    _actor.GetElementText(ClientLocators.AMLRisk).Should().BeEquivalentTo(_creditDetailsEntity.AMLRisk);
                    _actor.GetElementText(ClientLocators.FinanceRiskScore).Should().BeEquivalentTo(_creditDetailsEntity.FinanceRiskScore);
                    break;
                case StepConstants.ClientDefaults:
                    _actor.GetElementText(ClientLocators.PTAFees1).Should().BeEquivalentTo(_clientDefaultsEntity.PTAFees1);
                    _actor.GetElementText(ClientLocators.PTACost1).Should().BeEquivalentTo(_clientDefaultsEntity.PTACost1);
                    _actor.GetElementText(ClientLocators.PTACharge1).Should().BeEquivalentTo(_clientDefaultsEntity.PTACharge1);
                    _actor.GetElementText(ClientLocators.PTAFees2).Should().BeEquivalentTo(_clientDefaultsEntity.PTAFees2);
                    _actor.GetElementText(ClientLocators.PTACost2).Should().BeEquivalentTo(_clientDefaultsEntity.PTACost2);
                    _actor.GetElementText(ClientLocators.PTACharge2).Should().BeEquivalentTo(_clientDefaultsEntity.PTACharge2);
                    break;
            }
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [When(@"I change the status of the client")]
        public void WhenIChangeTheStatusOfTheClient(Table table)
        {
            var client = table.CreateInstance<ClientEntity>();
            if (!string.IsNullOrEmpty(client.Status))
            {
                _featureContext[StepConstants.Status] = client.Status;
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.Status, client.Status));
            }
        }

        [Then(@"I validate the status was updated")]
        public void ThenIValidateTheStatusWasUpdated()
        {
            var status = _featureContext[StepConstants.Status].ToString();
            _actor.GetElementText(ClientLocators.Status).Should().BeEquivalentTo(status);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.GetElementText(ClientLocators.DateClosed).Should().NotBeNullOrEmpty();
        }

        [StepDefinition(@"I verify the sections in client maintenance")]
        public void WhenIVerifyTheSectionsInClientMaintenance()
        {
            _actor.DoesElementExist(ClientLocators.Entity).Should().Be(true);
            _actor.DoesElementExist(ClientLocators.ClientName).Should().Be(true);
            _actor.DoesElementExist(ClientLocators.OpeningFeeEarner).Should().Be(true);
            _actor.DoesElementExist(ClientLocators.DateOpened).Should().Be(true);
            _actor.DoesElementExist(ClientLocators.Status).Should().Be(true);
            _actor.DoesElementExist(ClientLocators.StatusDate).Should().Be(true);
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.CreditDetails));
            _actor.AsksFor(Field.IsAvailable(ClientLocators.CreditDetails)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.EffectiveDatedInformation)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.ClientGroup)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.TimeTypeGroup)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.BillingRulesValidationList)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.ClientDefaults)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.BillingContacts)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.RateExceptionGroup)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.RateException)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.TemplateOption)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.MaskOverrideValues)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.FlatFees)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.ProformaAdjustments)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.AlternativeBillingArrangements)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientLocators.UDF)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I want to add client note")]
        public void ThenIWantToAddClientNote(Table table)
        {
            var notesEntity = table.CreateInstance<MatterNoteEntity>();

            _featureContext[StepConstants.ClientMatterNotesContext] = notesEntity;
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("Add Client Note")));

            _actor.AttemptsTo(DateControl.SelectDate("Date Entered", notesEntity.DateEntered));

            _actor.AttemptsTo(Dropdown.SelectOptionByName(NotesLocator.MatterNoteType, notesEntity.NoteType));
            _actor.AttemptsTo(SendKeys.To(NotesLocator.Note, notesEntity.Note));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _featureContext[StepConstants.ClientMatterNotesContext] = notesEntity.Note;
            _actor.AttemptsTo(Click.On(CommonLocator.SaveNote));

        }

        [StepDefinition(@"I want to view Client note")]
        public void ThenIWantToViewClientNote()
        {
            if (_actor.DoesElementExist(CommonLocator.ButtonElementContainsText("View Client Note")))
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("View Client Note")));
            else
            {
                _actor.AttemptsTo(Click.On(InvoiceManagerLocators.MatterGroupInquiryDropdown));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("View Client Notes")));
            }
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var note = _actor.DoesElementExist(CommonLocator.FindDivElementContainsExactText(_featureContext[StepConstants.ClientMatterNotesContext].ToString()));
            note.Should().Be(true);
            if (_actor.DoesElementExist(CommonLocator.CloseDialogue))
            {
                _actor.AttemptsTo(JScript.ClickOn(CommonLocator.CloseDialogue));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }
        [StepDefinition(@"I view an existing client")]
        public void GivenIViewAnExistingClient()
        {
            var clientNumber = _featureContext[StepConstants.Client].ToString();

            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientMaintenance));
            _actor.AttemptsTo(QuickFind.Search(clientNumber));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I add flat fees for the client")]
        public void WhenIAddFlatFeesForTheClient(Table table)
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Client));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Flat Fees", ChildProcessMenuAction.Add));

            _actor.AttemptsTo(Click.On(ClientLocators.FlatFeeTimeTypeDropdownIcon));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(ClientLocators.FlatFeeTimeTypeDropdownOption(table.Rows[0][ColumnNames.TimeType])));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }


        [Then(@"I add a note to a client")]
        public void ThenIAddANoteToAClient(Table table)
        {
            notesEntity = table.CreateInstance<MatterNoteEntity>();
            notesEntity.EntryUser = (string.IsNullOrEmpty(notesEntity.EntryUser)) ? _featureContext[StepConstants.LoggedInUser].ToString() : notesEntity.EntryUser;
            _featureContext[StepConstants.ClientNotesContext] = notesEntity;
            _featureContext[StepConstants.ClientNote] = notesEntity.Note;
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientNotes));
            _actor.AttemptsTo(EnterClientNotes.With(notesEntity));
        }

        [Then(@"I verify the client notes in Client Notes process")]
        public void ThenIVerifyTheClientNotesInClientNotesProcess()
        {
            var client = _featureContext[StepConstants.Client].ToString();
            notesEntity = (MatterNoteEntity)_featureContext[StepConstants.ClientNotesContext];
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientNotes));
            _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchTextBox, client));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            try
            {
                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SelectFirstUnlockedRow.Select());
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.SelectButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            catch
            {
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }


            _actor.AsksFor(ValueAttribute.Of(NotesLocator.ClientNoteType)).Should().Contain(notesEntity.NoteType);
            _actor.AsksFor(ValueAttribute.Of(NotesLocator.ClientNote)).Should().Contain(notesEntity.Note);
            _actor.AsksFor(ValueAttribute.Of(NotesLocator.NextActionOwner)).Should().Contain(notesEntity.ActionOwner);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
        }

        [Given(@"I remove the client notes")]
        public void GivenIRemoveTheClientNotes()
        {
            var client = _featureContext[StepConstants.Client].ToString();
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientMaintenance));
            _actor.AttemptsTo(QuickFind.Search(client));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientNotes));
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.ClientNotes, ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }

        [When(@"I run the metric")]
        public void WhenIRunTheMetric()
        {
            _actor.AttemptsTo(DoubleClick.On(CommonLocator.FindDivElementContainsText("MxCollectionInvoice_ccc")));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(JScript.ClickOn(NotesLocator.RunMetricButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search for collection productivity report")]
        public void WhenISearchForCollectionProductivityReport()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.CollectionProductivityReport, false));
            _actor.AttemptsTo(Click.On(CommonLocator.SearchResult(Process.CollectionProductivityReport)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I run the report")]
        public void WhenIRunTheReport()
        {
            _actor.AttemptsTo(Click.On(CommonLocator.RunReport));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [Then(@"I verify the correct fields are displayed")]
        public void ThenIVerifyTheCorrectFieldsAreDisplayed(Table table)
        {
            var list = _actor.GetElementText(MatterGroupLocator.GridLoc);
            list.Should().ContainEquivalentOf(table.Rows[0][ColumnNames.Payer]);
            list.Should().ContainEquivalentOf(table.Rows[0]["Field1"]);
            list.Should().ContainEquivalentOf(table.Rows[0]["Field2"]);
            list.Should().ContainEquivalentOf(table.Rows[0]["Field3"]);
        }

        [Given(@"I search for collection item report")]
        public void GivenISearchForCollectionItemReport()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.CollectionItemReport, false));
            _actor.AttemptsTo(Click.On(CommonLocator.SearchResult(Process.CollectionItemReport)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"I verify the column headings")]
        public void ThenIVerifyTheColumnHeadings(Table table)
        {
            var list = _actor.GetElementText(MatterGroupLocator.GridLoc);
            list.Should().ContainEquivalentOf(table.Rows[0]["Field1"]);
            list.Should().ContainEquivalentOf(table.Rows[0]["Field2"]);
            list.Should().ContainEquivalentOf(table.Rows[0]["Field3"]);
            list.Should().ContainEquivalentOf(table.Rows[0]["Field4"]);

        }


        [When(@"I add a new client datails")]
        public void WhenIAddANewClientDatails(Table table)
        {
            if (!string.IsNullOrEmpty(""))
                _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Entity", ""));
        }


    }
}
