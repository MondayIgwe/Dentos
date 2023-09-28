using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Services.PayeeMaintenance;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Services;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiPayeeSteps
    {
        private readonly FeatureContext _featureContext;
        public IPayeeMaintenanceService _clientAccountUse = new PayeeMaintenanceService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IProcessDataService _processDataService = new ProcessDataService();
        public IRestResponse _response;
        private PayeeMaintenanceData _payeeMaintenanceData = new();

        public ApiPayeeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [StepDefinition(@"I create a new Payee with the Api")]
        public async Task GivenICreateANewPayeeWithTheApi(Table table)
        {
            var payeeDetails = table.CreateInstance<ApiPayeeEntity>();
            payeeDetails.Name = table.Rows.Select(r => r["PayeeName"]).ToList()[0];
            payeeDetails.Name = (string.IsNullOrEmpty(payeeDetails.Name.String)) ? "Payee" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+7") : payeeDetails.Name;
            await CreatePayeeMaintenance(payeeDetails);
        }

        private async Task CreatePayeeMaintenance(ApiPayeeEntity payeeDetails)
        {
            // check and create client account Data
            string payeeNumber = await _payeeMaintenanceData.SearchAndCreatePayeeMaintenanceDataAsync(payeeDetails);

            if (!string.IsNullOrEmpty(payeeNumber))
            {
                _featureContext[StepConstants.PayeeNameContext] = payeeDetails.EntityName;
                _featureContext[StepConstants.Payee] = payeeNumber;
            }
            else
            {
                throw new Exception("Error occurred in Api while creating Payee Maintenance");
            }
        }
    }
}
