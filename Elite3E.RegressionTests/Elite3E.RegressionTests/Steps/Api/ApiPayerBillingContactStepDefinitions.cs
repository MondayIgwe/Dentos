using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.Data;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.Payer;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests
{
    [Binding]
    public class ApiPayerBillingContactStepDefinitions
    {
        private readonly FeatureContext _featureContext;
        public IPayerService _payerMaintenanceService = new PayerService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IProcessDataService _processDataService = new ProcessDataService();
        public IRestResponse _response;
        private PayerData _payerData = new PayerData();

        public ApiPayerBillingContactStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }


        [StepDefinition(@"I create the Payer with Api")]
        public async Task GivenICreateThePayerWithApi(Table table)
        {
            var payerDetails = table.CreateInstance<ApiPayerEntity>();
            payerDetails.Entity = table.Rows.Select(r => r["Entity"]).ToList()[0];
            payerDetails.PayerName = table.Rows.Select(r => r["PayerName"]).ToList()[0];
            payerDetails.PayerName = (string.IsNullOrEmpty(payerDetails.PayerName)) ? "Payer" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+7") : payerDetails.PayerName;
            await CreatePayer(payerDetails);
        }


        private async Task CreatePayer(ApiPayerEntity payer)
        {
            // check and create client account Data
            payer.PayerName = await _payerData.CreateAPayer(payer);

            if (!string.IsNullOrEmpty(payer.PayerName))
            {
                _featureContext[StepConstants.PayerContext] = payer.PayerName;
            }
            else
            {
                throw new Exception("Error occured in Api while creating a Payer");
            }
        }
    }
}
