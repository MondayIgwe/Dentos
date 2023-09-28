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
using Elite3E.RegressionTests.Data;
using Elite3E.Infrastructure.Entity.BillingRules;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiBillingRulesValidationbListSteps
    {
        private readonly FeatureContext _featureContext;
        public IClientAccountIntendedUseService _clientAccountUse = new ClientAccountIntendedUseService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IProcessDataService _processDataService = new ProcessDataService();
        public IRestResponse _response;
        private ClientAccountIntendedUseData _clientAccountIntendedUseData = new();


        public ApiBillingRulesValidationbListSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I search or create proforma time billing rule validation list")]
        public async Task GivenISearchOrCreateABillingRuleValidationList(Table table)
        {
            var billingRulesValidationList = table.CreateInstance<BillingRulesValidationListEntity>();
            await new BillingRulesValidationListData().SearchOrCreateBillingRulesValidationListForProformaTimeCheckAsync(billingRulesValidationList.Code, billingRulesValidationList.Description);
            _featureContext[StepConstants.BillingRulesValidationListDescription] = billingRulesValidationList.Description;
        }

        [Given(@"I search or create Cost and time billing rule validation list")]
        public async Task GivenISearchOrCreateCostAndTimeBillingRuleValidationList(Table table)
        {
            var billingRulesValidationList = table.CreateInstance<BillingRulesValidationListEntity>();
            await new BillingRulesValidationListData().SearchOrCreateBillingRulesValidationListForProformaPeriodCheckAsync(billingRulesValidationList.Code, billingRulesValidationList.Description);
            _featureContext[StepConstants.BillingRulesValidationListDescription] = billingRulesValidationList.Description;
        }


    }
}
