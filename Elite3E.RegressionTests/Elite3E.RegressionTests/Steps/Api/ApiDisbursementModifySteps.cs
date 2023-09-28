using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.RestServices.Services;
using RestSharp;
using System;
using System.Linq;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiDisbursementModifySteps
    {
        private readonly FeatureContext _featureContext;
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public ILookUpService _lookUpService = new LookUpService();
        public IRestResponse _response;
        public DisbursementModifyData _disbursementModifyService = new DisbursementModifyData();

        public ApiDisbursementModifySteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I add a disbursement modify with api")]
        public async Task GivenIAddADisbursementModifyWithApi(Table table)
        {
            var disbursementModifyEntity = table.CreateInstance<ApiDisbursementModifyEntity>();

            //Matter numbers are generated at runtime, if one is not provided, set to default.
            if (string.IsNullOrEmpty(disbursementModifyEntity.MatterNumber))
                disbursementModifyEntity.MatterNumber = (_featureContext.Any(x => x.Key.Equals(StepConstants.MatterNumberContext))) ? _featureContext[StepConstants.MatterNumberContext].ToString() : disbursementModifyEntity.MatterNumber;
            

            if (string.IsNullOrEmpty(disbursementModifyEntity.DisbursementType))
                disbursementModifyEntity.DisbursementType = (_featureContext.Any(x => x.Key.Equals(StepConstants.DisbursementTypeContext))) ? _featureContext[StepConstants.DisbursementTypeContext].ToString() : StepArgumentExtension.ReplaceDynamicValues("{Auto}+10");
            
            disbursementModifyEntity.WorkRate = (string.IsNullOrEmpty(disbursementModifyEntity.WorkRate)) ? "0.1000" : disbursementModifyEntity.WorkRate;

            await _disbursementModifyService.CreateDisbursementModifyAsync(disbursementModifyEntity);
        }

    }
}
