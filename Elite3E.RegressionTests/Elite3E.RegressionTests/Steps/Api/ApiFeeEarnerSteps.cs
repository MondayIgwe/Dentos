using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiFeeEarnerSteps
    {
        private readonly FeatureContext _featureContext;

        public ApiFeeEarnerSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [StepDefinition(@"I create a fee earner with details")]
        public async Task GivenICreateAFeeEarnerWithDetails(Table table)
      {
            var feeEarnerEntity = table.CreateInstance<ApiFeeEarnerEntity>();            

            var entity = new ApiEntity()
            {
                FirstName = feeEarnerEntity.EntityName.Split(" ")[0],
                LastName = feeEarnerEntity.EntityName.Split(" ")[1],
                FormattedName = feeEarnerEntity.EntityName
            };

            var entityRespose = await new FeeEarnerData().SearchAndCreateFeeEranerData(feeEarnerEntity, entity);
            
            _featureContext[StepConstants.FeeEarner] = entityRespose.FeeEarnerNumber;
            _featureContext[StepConstants.FeeEarnerName] = feeEarnerEntity.EntityName;
        }       

        [StepDefinition(@"I create a fee earner without details")]
        public async Task GivenICreateAFeeEarnerWithoutDetails()
        {
            var feeEarnerEntity = new ApiFeeEarnerEntity();
            feeEarnerEntity.EntityName = (string.IsNullOrEmpty(feeEarnerEntity.EntityName)) ? StepArgumentExtension.ReplaceDynamicValues("{Auto}+7") + " " + StepArgumentExtension.ReplaceDynamicValues("{Auto}+7") : feeEarnerEntity.EntityName;            
            //Don't set Rate type. Use Standard

            var entity = new ApiEntity()
            {
                FirstName = feeEarnerEntity.EntityName.Split(" ")[0],
                LastName = feeEarnerEntity.EntityName.Split(" ")[1],
                FormattedName = feeEarnerEntity.EntityName
            };

            var entityRespose = await new FeeEarnerData().SearchAndCreateFeeEranerData(feeEarnerEntity, entity);
            
            _featureContext[StepConstants.FeeEarner] = entityRespose.FeeEarnerNumber;
            _featureContext[StepConstants.FeeEarnerName] = feeEarnerEntity.EntityName;
        }
    }
}
