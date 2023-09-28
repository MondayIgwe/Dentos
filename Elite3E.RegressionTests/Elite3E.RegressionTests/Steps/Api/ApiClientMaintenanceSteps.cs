using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Services.ClientAccountIntendedUse;
using RestSharp;
using System;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Services;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Linq;
using Elite3E.Infrastructure.Extensions;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiClientMaintenanceSteps
    {
        private readonly FeatureContext _featureContext;
        public IClientAccountIntendedUseService _clientAccountUse = new ClientAccountIntendedUseService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IProcessDataService _processDataService = new ProcessDataService();
        public IRestResponse _response;
        private ClientAccountIntendedUseData _clientAccountIntendedUseData = new();


        public ApiClientMaintenanceSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [StepDefinition(@"I search or create a client")]
        public async Task GivenISearchOrCreateAClient(Table table)
        {
            var client = table.CreateInstance<ApiClientMaintenanceEntity>();

            if (!string.IsNullOrEmpty(client.EntityName) && !client.EntityName.Contains(" "))
            {
                client.EntityName = client.EntityName + " " + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10");
            }

            // check the data exists or create the required data 
            var clientMaintenanceData = new ClientMaintenanceData();

            // check for client Data and get client Number          

            client = await clientMaintenanceData.CheckClientExitsElseCreateClient(client);
            _featureContext[StepConstants.Client] = client.EntityName;
            _featureContext[StepConstants.ClientNumber] = client.ClientNumber;
        }

        [Given(@"I search or create a client with fee earner")]
        public async Task GivenISearchOrCreateAClientWithFeeEarner(Table table)
        {
            var client = table.CreateInstance<ApiClientMaintenanceEntity>();
            var clientMaintenanceData = new ClientMaintenanceData();
            client = await clientMaintenanceData.ClientData(client);

            _featureContext[StepConstants.Client] = client.EntityName;
            _featureContext[StepConstants.ClientNumber] = client.ClientNumber;
        }

        [Given(@"I search or create a client with billing rules")]
        public async Task GivenISearchOrCreateAClientWithBillingRules(Table table)
        {        
            var client = table.CreateInstance<ApiClientMaintenanceEntity>();

            // check the data exists or create the required data 
            var clientMaintenanceData = new ClientMaintenanceData();

            // check for client Data and get client Number          

            client = await clientMaintenanceData.CheckClientExitsElseCreateClient(client);
            _featureContext[StepConstants.Client] = client.EntityName;
            _featureContext[StepConstants.ClientNumber] = client.ClientNumber;

            //Add Billing Rules to the client 

            await clientMaintenanceData.AddBillingRulesValidationListToAnExistingClientAsync(client);
           
        }


    }
}
