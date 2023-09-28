using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiMatterSteps
    {

        private readonly FeatureContext _featureContext;


        public ApiMatterSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        // Create Matter
        [StepDefinition(@"I create a submatter 1 with details:")]
        public async Task WhenICreateASubMatter1WithDetails(Table table)
        {
            var matterDetails = table.CreateInstance<ApiMatterEntity>();
            var matterNumberAndClientNumber = await new MatterMaintenanceData().CreateMatter(matterDetails);

            _featureContext[StepConstants.SubMatterNumberContextOne] = matterNumberAndClientNumber.MatterNumber;
            _featureContext[StepConstants.TransferingBillingTimekeeper] = matterDetails.FeeEarnerFullName;
            _featureContext[StepConstants.ClientNumber] = matterNumberAndClientNumber.ClientNumber;
        }

        // Create Matter
        [StepDefinition(@"I create a submatter 2 with details:")]
        public async Task WhenICreateASubMatter2WithDetails(Table table)
        {
            var matterDetails = table.CreateInstance<ApiMatterEntity>();
            var matterNumberAndClientNumber = await new MatterMaintenanceData().CreateMatter(matterDetails);

            _featureContext[StepConstants.RecievingBillingTimekeeper] = matterDetails.FeeEarnerFullName;
            _featureContext[StepConstants.SubMatterNumberContextTwo] = matterNumberAndClientNumber.MatterNumber;
            _featureContext[StepConstants.ClientNumber] = matterNumberAndClientNumber.ClientNumber;
        }

        // Create Matter
        [StepDefinition(@"I create a matter with details:")]
        public async Task WhenICreateAMatterWithDetails(Table table)
        
        {
            var matterDetails = table.CreateInstance<ApiMatterEntity>();
            if (string.IsNullOrEmpty(matterDetails.Client))
            {
                //This is just a client name if client is null or empty
                matterDetails.Client = _featureContext[StepConstants.Entity].ToString();
            }

            //This is required for Charge & Cost Type Groups which are generated through UI
            if (_featureContext.ContainsKey(StepConstants.DynamicChargeTypeGroup))
            {
                matterDetails.ChargeTypeGroupName = _featureContext[StepConstants.DynamicChargeTypeGroup].ToString();
            }
            if (_featureContext.ContainsKey(StepConstants.DynamicCostTypeGroup))
            {
                matterDetails.CostTypeGroupName = _featureContext[StepConstants.DynamicCostTypeGroup].ToString();
            }
            var matterNumberAndClientNumber = await new MatterMaintenanceData().CreateMatter(matterDetails);

            _featureContext[StepConstants.MatterNumberContext] = matterNumberAndClientNumber.MatterNumber;
            _featureContext[StepConstants.ClientNumber] = matterNumberAndClientNumber.ClientNumber;
        }
    }
}
