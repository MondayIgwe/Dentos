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
using FluentAssertions;
using System.Globalization;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiTimeModifySteps
    {
        private readonly FeatureContext _featureContext;
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public ILookUpService _lookUpService = new LookUpService();
        public IRestResponse _response;
        public TimeModifyData _timeModifyService = new TimeModifyData();

        public ApiTimeModifySteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I add a time modify with api")]
        public async Task GivenIAddATimeModifyWithApi(Table table)
        {

            var timeModifyEntity = table.CreateInstance<ApiTimeModifyEntity>();
            timeModifyEntity.Currency =timeModifyEntity.Currency;

            //IF no name is provided, check feature context, then assign random.
            if (string.IsNullOrEmpty(timeModifyEntity.FeeEranerName))
                timeModifyEntity.FeeEranerName = (_featureContext.Any(x => x.Key.Equals(StepConstants.FeeEarnerName))) ? _featureContext[StepConstants.FeeEarnerName].ToString() : StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") + " " + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10");

            //Matter numbers are generated at runtime, if one is not provided, set to default.
            if (string.IsNullOrEmpty(timeModifyEntity.MatterNumber))
                timeModifyEntity.MatterNumber = (_featureContext.Any(x => x.Key.Equals(StepConstants.MatterNumberContext))) ? _featureContext[StepConstants.MatterNumberContext].ToString() : timeModifyEntity.MatterNumber;
            

            if (string.IsNullOrEmpty(timeModifyEntity.TimeType))
                timeModifyEntity.TimeType = (_featureContext.Any(x => x.Key.Equals(StepConstants.TimeTypeContext))) ? _featureContext[StepConstants.TimeTypeContext].ToString() : StepArgumentExtension.ReplaceDynamicValues("{Auto}+10");

            if (!string.IsNullOrEmpty(timeModifyEntity.WorkDate))
                timeModifyEntity.WorkDate = DateTime.Parse(timeModifyEntity.WorkDate, new CultureInfo("en-US", true)).ToString("yyyy-MM-dd");
            
            await _timeModifyService.CreateTimeModifyAsync(timeModifyEntity);
        }

        [Given(@"I add a time type with api")]
        public async Task GivenIAddATimeTypeWithApi(Table table)
        {
            //change how we consume the table
            var timeTypeEntity = table.CreateInstance<ApiTimeTypeEntity>();
            timeTypeEntity.Code.Should().NotBeNull();
            timeTypeEntity.Description.Should().NotBeNull();

            timeTypeEntity.TransactionType = (string.IsNullOrEmpty(timeTypeEntity.TransactionType)) ? "Fixed - Capped Fees" : timeTypeEntity.TransactionType;
            timeTypeEntity.Currency = (string.IsNullOrEmpty(timeTypeEntity.Currency)) ? "GBP - British Pound" : timeTypeEntity.Currency;


            _featureContext[StepConstants.TimeTypeContext] = await new TimeTypeData().CreateTimeTypeAsync(timeTypeEntity);
        }

    }
}
