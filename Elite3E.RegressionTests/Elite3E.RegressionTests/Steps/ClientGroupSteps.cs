using FluentAssertions;
using Elite3E.PageObjects.PageLocators;
using Boa.Constrictor.Screenplay;
using TechTalk.SpecFlow;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientGroup;
using System.Collections.Generic;
using System.Linq;
using Elite3E.Infrastructure.Entity;
using TechTalk.SpecFlow.Assist;
using Elite3E.Infrastructure.Entity.FeeEarnerMaintenance;
using Elite3E.RestServices.Entity;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class CreateClientGroupsSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public CreateClientGroupsSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"The client group process is open")]
        public void GivenTheClientGroupProcessIsOpen()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        
        [StepDefinition(@"I open the Client Group process")]
        public void WhenIOpenTheClientGroupProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientGroup));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [StepDefinition(@"I open the Client maintenance process")]
        public void WhenIOpenTheClientMaintenanceProcess()
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.ClientMaintenance));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"search for the first client")]
        public void WhenSearchForTheFirstClient()
        {
            var Client = _featureContext[StepConstants.Client].ToString();
            _actor.AttemptsTo(QuickFind.Search(Client));
        }


        [When(@"delete the client group for the client")]
        public void WhenDeleteTheClientGroupForTheClient()
        {
            var clientGroup = (List<string>) _featureContext[StepConstants.Description];

            _actor.WaitsUntil(Appearance.Of(ClientGroupLocators.SelectExistingClient(clientGroup.FirstOrDefault())),IsEqualTo.True());
            _actor.AttemptsTo(Click.On(ClientGroupLocators.SelectExistingClient(clientGroup.FirstOrDefault())));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.ClientGroup,ChildProcessMenuAction.Delete));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"add a client group")]
        public void WhenAddAClientGroup()
        {
            
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.ClientGroup, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var description = (List<string>)_featureContext[StepConstants.Description];

            _actor.AttemptsTo(Click.On(ClientGroupLocators.SearchInputText));
            _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.SearchInputText, description.FirstOrDefault()));
            _actor.AttemptsTo(Click.On(ClientGroupLocators.SearchButton)); 
            _actor.AttemptsTo(Click.On(ClientGroupLocators.ClientGroupSelectFirstRecordDesdcription));
  
            _actor.AttemptsTo(Click.On(ClientGroupLocators.Select));
        }
        
        [When(@"select the client group")]
        public void WhenSelectTheClientGroup()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            driver.FindElement(ClientGroupLocators.ClientGroupSelectFirstRecord.Query).Click();
            var descriptionText = driver.FindElement(ClientGroupLocators.ClientGroupSelectFirstRecordDesdcription.Query).Text;
            
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(LocatorConstants.SelectButton)));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        
        [When(@"open the client child form")]
        public void WhenOpenTheClientChildForm()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.ClientGroup));
        }

        [When(@"Add a another client record")]
        [When(@"Add a new client record")]
        [When(@"Add a client record")]
        public async Task WhenAddAClientRecord(Table table)
        {
            var clientGroup = table.CreateInstance<ClientGroupEntity>();
            var clientGroupData = new ClientGroupData();
            await clientGroupData.SearchAndCreateClientGroupType(clientGroup.GroupType);

            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Existence.Of(ClientGroupLocators.Code), IsEqualTo.True());

            if (!string.IsNullOrEmpty(clientGroup.GroupName))
                _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.Code, clientGroup.GroupName));

            if (!string.IsNullOrEmpty(clientGroup.Description))
            {
                _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.Description, clientGroup.Description));

                var descriptions = new List<string>();

                if (_featureContext.ContainsKey(StepConstants.Description))
                    descriptions = (List<string>)_featureContext[StepConstants.Description];

                descriptions.Add(clientGroup.Description);
                _featureContext[StepConstants.Description] = descriptions;
            }

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!string.IsNullOrEmpty(clientGroup.GroupType))
                _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientGroupLocators.GroupType, clientGroup.GroupType));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"add a new fee earner")]
        public async Task WhenAddANewFeeEarner(Table table)
        {
            var feeEarnersList = table.CreateSet<FeeEarnerEntity>();

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.FeeEarner));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            
            var feeEarnerDataEntity = new ApiFeeEarnerEntity();

            foreach(var feeEarner in feeEarnersList)
            { 
            
                feeEarnerDataEntity.EntityName = feeEarner.Name;

                feeEarner.Id = await FeeEarnerData.GetFeeEarnerNumber(feeEarnerDataEntity);

                _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(StepConstants.FeeEarner, LocatorConstants.AddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                             

                _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.FeeOwner, feeEarner.Id));
                var driver = _actor.Using<BrowseTheWeb>().WebDriver;
                new Actions(driver).SendKeys(Keys.Tab).Build().Perform();

                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                if (feeEarner.IsResponsible == true)
                {
                    _actor.AttemptsTo(Click.On(ClientGroupLocators.IsResponsible(feeEarner.Name)));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }

                if (feeEarner.IsOwner == true)
                {
                    _actor.AttemptsTo(Click.On(ClientGroupLocators.IsOwner(feeEarner.Name)));
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }           
        }

        [When(@"more than one fee owner error is displayed")]
        public void WhenMoreThanOneFeeOwnerErrorIsDisplayed()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));

            var actualValue = "Only one Owner can be selected.";

            var expectedValue = message;

            expectedValue.Should().BeEquivalentTo(actualValue);

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"more than one responsible fee owner earner is displayed")]
        public void WhenMoreThanOneResponsibleFeeOwnerEarnerIsDisplayed()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));

            var actualValue = "Only one Responsible can be selected.";
            
            var expectedValue = message;

            expectedValue.Should().BeEquivalentTo(actualValue);

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I unseelect the responsible for '(.*)' fee earner")]
        public async Task WhenIUnseelectTheResponsibleForFeeEarner(string feeEarnerName)
        {
             var feeEarnerDataEntity = new ApiFeeEarnerEntity();

            feeEarnerDataEntity.EntityName = feeEarnerName;
            var feeEarnerNumber = await FeeEarnerData.GetFeeEarnerNumber(feeEarnerDataEntity);

            _actor.AttemptsTo(Checkbox.SetStatus(ClientGroupLocators.GetResponsibelForFeeNumber(feeEarnerNumber), false));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"I unseelect the owner for '(.*)' fee earner")]
        public async Task WhenIUnseelectTheOwnerForFeeEarner(string feeEarnerName)
        {
            var feeEarnerDataEntity = new ApiFeeEarnerEntity();

            feeEarnerDataEntity.EntityName = feeEarnerName;
            var feeEarnerNumber = await FeeEarnerData.GetFeeEarnerNumber(feeEarnerDataEntity);

            _actor.AttemptsTo(Checkbox.SetStatus(ClientGroupLocators.GetOwnerForFeeNumber(feeEarnerNumber), false));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"add a new fee earner record")]
        public void WhenAddANewFeeEarnerRecord(Table table)
        {
            
            var menuItems = new List<string>()
            {
                StepConstants.FeeEarner
            };

            _actor.AttemptsTo(AddChildProcess.ByName(menuItems));
            _actor.AttemptsTo(Click.On(ClientGroupLocators.InputSearch));
            _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.FeeOwner, table.Rows[0][ColumnNames.FeeEarner]));
        }


        [When(@"select the owner and responsible checkboxes")]
        public void WhenSelectTheOwnerAndResponsibleCheckboxes()
        {
           
            _actor.AttemptsTo(Click.On(ClientGroupLocators.IsResponsibleChkBox));
            
        }
        
        [When(@"add a another fee owner")]
        public void WhenAddAAnotherFeeOwner(Table table)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Fee Earner", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
           
            _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.FeeOwner, table.Rows[0][ColumnNames.FeeEarner]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

      
        [When(@"add a relevant client")]
        public void WhenAddARelevantClient(Table table)
        {
           
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Client));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(StepConstants.Client, LocatorConstants.AddButton)));

         
            _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.Client, table.Rows[0][ColumnNames.Client]));

        }

        [When(@"add the first client")]
        public async Task WhenAddTheFirstClient(Table table)
        {
            var client = new ApiClientMaintenanceEntity();
            client.EntityName = table.Rows[0][ColumnNames.Client];
            // check the data exists or create the required data 
            var clientMaintenanceData = new ClientMaintenanceData();

            // check client Data
            await clientMaintenanceData.ClientData(client);

            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.Client));

            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(StepConstants.Client, LocatorConstants.AddButton)));

            _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.Client, table.Rows[0][ColumnNames.Client]));
             
            _featureContext[StepConstants.Client] = table.Rows[0][ColumnNames.Client];
        }

        [When(@"add a second relevant client with same client name")]
        public async Task WhenAddASecondRelevantClientWithSameClientName(Table table)
        {
            var client = new ApiClientMaintenanceEntity();
            client.EntityName = table.Rows[0][ColumnNames.Client];
            // check the data exists or create the required data 
            var clientMaintenanceData = new ClientMaintenanceData();

            // check client Data
             await clientMaintenanceData.ClientData(client);

           // Thread.Sleep(TimeSpan.FromSeconds(5));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(StepConstants.Client, LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.Client, table.Rows[0][ColumnNames.Client] + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
          

            var secondClient = table.Rows[0][ColumnNames.Client];
            _featureContext[StepConstants.Client] = secondClient;
        }


        [When(@"add a second client")]
        [When(@"add a second with same client name")]
        public async Task WhenAddASecondWithSameClientName(Table table)
        {
            var client = new ApiClientMaintenanceEntity();
            client.EntityName = table.Rows[0][ColumnNames.Client];
            // check the data exists or create the required data 
            var clientMaintenanceData = new ClientMaintenanceData();

            // check client Data
            await clientMaintenanceData.ClientData(client);

            _actor.AttemptsTo(ChildProcessMenu.ClickOn("Client", ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(ClientGroupLocators.Client, table.Rows[0][ColumnNames.Client]));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            _featureContext[StepConstants.Client] = table.Rows[0][ColumnNames.Client];
        }


        [Then(@"duplicate client  error is displayed")]
        public void ThenDuplicateClientErrorIsDisplayed()
        {

            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));

            var actualValue = "There must be a record where Responsible is selected.";

            var expectedValue = message;

            expectedValue.Should().BeEquivalentTo(actualValue);
        }

        [Given(@"I search the client group and delete it")]
        public void WhenISearchTheClientGroupAndDeleteIt()
        {
            var descriptions = (List<string>)_featureContext[StepConstants.Description];

            foreach (var description in descriptions)
            {
                _actor.AttemptsTo(SearchProcess.ByName(Process.ClientGroup));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(QuickFind.Search(description));

                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.DeleteButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(CommonLocator.Submit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        [When(@"I can verify the client  updates")]
        public void WhenICanVerifyTheClientUpdates()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var client = _featureContext[StepConstants.Client].ToString();

            _actor.WaitsUntil(Appearance.Of(ClientGroupLocators.SelectExistingClient(client)), IsEqualTo.False());

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.DeleteButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search the client group")]
        public void WhenISearchTheClientGroup()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var description = (List<string>) _featureContext[StepConstants.Description];
            _actor.AttemptsTo(QuickFind.Search(description.FirstOrDefault()));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I search for the client group")]
        public void WhenISearchForTheClientGroup()
        {
            var description = (List<string>)_featureContext[StepConstants.Description];
            _actor.AttemptsTo(QuickFind.Search(description.FirstOrDefault()));

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [Then(@"I can verify the client")]
        public void WhenICanVerifyTheClient()
        {
           
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var clientnum = _featureContext[StepConstants.ClientNumber].ToString();
            _actor.WaitsUntil(Appearance.Of(ClientGroupLocators.SelectExistingClient(clientnum)), IsEqualTo.True());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Button(LocatorConstants.DeleteButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"search for the existing client ""(.*)""")]
        public void WhenSearchForTheExistingClient(string ClientNum)
        {
            _actor.AttemptsTo(QuickFind.Search(ClientNum));
            _featureContext[StepConstants.ClientNumber] = ClientNum;
        }


        [When(@"search for the existing client")]
        public void WhenSearchForTheExistingClient()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText(LocatorConstants.SerachButton)));
            var driver = _actor.Using<BrowseTheWeb>().WebDriver;
            driver.FindElement(ClientGroupLocators.ClientGroupSelectFirstRecord.Query).Click();
            var clientName = driver.FindElement(ClientGroupLocators.ClientGroupSelectFirstRecordName.Query).Text;
            _featureContext[StepConstants.ClientNumber] = clientName;
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Click.On(CommonLocator.FindButtonTagElementContainsText(LocatorConstants.SelectTitleButton)));

        }



        [When(@"I search the client group with same client")]
        public void WhenISearchTheClientGroupWithSameClient()
        {
            var newClient = _featureContext[StepConstants.Description].ToString();
            _actor.AttemptsTo(QuickFind.Search(newClient));
        }


        [Then(@"submit the client group")]
        public void ThenSubmitTheClientGroup()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Click.On(CommonLocator.Submit));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"the owner checkboox is unchecked an error is displayed")]
        public void WhenTheOwnerCheckbooxIsUncheckedAnErrorIsDisplayed()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));

            var actualValue = "There must be a record where Owner is selected.";

            var expectedValue = message;

            expectedValue.Should().BeEquivalentTo(actualValue);

            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


        [When(@"I can select the owner checkbox")]
        public void WhenICanSelectTheOwnerCheckbox()
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(Checkbox.SetStatus(ClientGroupLocators.IsOwnerChkBox, true));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"the responsible checkbox is unchecked an error is displayed")]
        public void WhenTheResponsibleCheckboxIsUncheckedAnErrorIsDisplayed()

        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));

            var actualValue = "There must be a record where Responsible is selected.";

            var expectedValue = message;

            expectedValue.Should().BeEquivalentTo(actualValue);
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

        [When(@"I can select the responsible checkbox")]
        public void WhenICanSelectTheResponsibleCheckbox()
        {
            _actor.AttemptsTo(Checkbox.SetStatus(ClientGroupLocators.IsResponsibleChkBox, true));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
        [Then(@"I verify the sections in client group")]
        public void ThenIVerifyTheSectionsInClientGroup()
        {
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.FeeEarner));
            _actor.AsksFor(Field.IsAvailable(ClientGroupLocators.FeeEarner)).Should().Be(true);
            _actor.AsksFor(Field.IsAvailable(ClientGroupLocators.ClientChildForm)).Should().Be(true);
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }


    }
}
