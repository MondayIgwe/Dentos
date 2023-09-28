using Elite3E.RegressionTests.RestServicesTest;
using Elite3E.RestServices.Entity;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;


namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiChargeTypeSteps
    {
        private readonly FeatureContext _featureContext;

        public ApiChargeTypeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [StepDefinition(@"I create a charge type with details")]
        public async Task GivenICreateAChargeTypeWithDetails(Table table)
        {
            //var chargeTypeEntity = table.CreateInstance<ApiChargeTypeEntity>();
            var chargeTypeEntity = new ApiChargeTypeEntity()
            {
                ChargeCode = table.Rows.Select(r => r["ChargeCode"]).ToList()[0],
                Description = table.Rows.Select(r => r["Description"]).ToList()[0],
                CategoryInput = table.Rows.Select(r => r["CategoryInput"]).ToList()[0],
                TransactionTypeAlias = table.Rows.Select(r => r["TransactionTypeAlias"]).ToList()[0],
                Active = table.Rows.Select(r => r["Active"]).ToList()[0]
            };

            ChargeTypeTest test = new ChargeTypeTest();
            if (!await test.DoesChargeTypeExist(chargeTypeEntity.Description.String))
            {
                await test.CreateChargeTypeTask(chargeTypeEntity);
            }
        }

    }
}

