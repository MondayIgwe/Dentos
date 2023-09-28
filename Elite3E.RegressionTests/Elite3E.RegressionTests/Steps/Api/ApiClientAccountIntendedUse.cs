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

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiClientAccountIntendedUse
    {
        private readonly FeatureContext _featureContext;
        public IClientAccountIntendedUseService _clientAccountUse = new ClientAccountIntendedUseService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IProcessDataService _processDataService = new ProcessDataService();
        public IRestResponse _response;
        private ClientAccountIntendedUseData _clientAccountIntendedUseData = new();


        public ApiClientAccountIntendedUse(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I create a new ClientAccount Intended Use APi")]
        public async Task GivenICreateANewClientAccountIntendedUseAPi(Table table)
        {
            var clientAccountDetails = table.CreateInstance<ApiClientIntendedUseEntity>();
            clientAccountDetails.Code = table.Rows.Select(r => r["Code"]).ToList()[0];
            clientAccountDetails.Description = table.Rows.Select(r => r["Description"]).ToList()[0];
            _featureContext[StepConstants.Description] = clientAccountDetails.Description.String;
            await CreateClientAccountIntendedUse(clientAccountDetails);
        }

        private async Task CreateClientAccountIntendedUse(ApiClientIntendedUseEntity clientDetails)
        {
            // check and create client account Data
            string clientCodeCreated = await _clientAccountIntendedUseData.SearchAndCreateAClientAccountIntendedUseDataAsync(clientDetails);

            if (!string.IsNullOrEmpty(clientCodeCreated))
            {
                _featureContext[StepConstants.ClientAccountCodeContext] = clientCodeCreated;
            }
            else
            {
                throw new Exception("Error occured in Api while creating Client Account Intended Use");
            }
        }

    }
}
