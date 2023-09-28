using Elite3E.RestServices.Entity;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiDisbursementTypeSteps
    {
        private readonly FeatureContext _featureContext;
        public readonly CostTypeData _costTypeData = new();

        public ApiDisbursementTypeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [StepDefinition(@"I create a soft cost disbursement type with details")]
        public async Task GivenICreateASoftDisbursementTypeWithDetails(Table table)
        {
            string code = table.Rows.Select(r => r["Code"]).ToList()[0];
            string description = table.Rows.Select(r => r["Description"]).ToList()[0];

            var disbursementTypeEntity = new ApiDisbursementTypeEntity();
            disbursementTypeEntity.Code = code;
            disbursementTypeEntity.Description = description;           
            await _costTypeData.SearchAndCreateSoftDisbursmentTypeDataAsync(disbursementTypeEntity);
        }

        [StepDefinition(@"I create a hard cost disbursement type with details")]
        public async Task GivenICreateAHardDisbursementTypeWithDetails(Table table)
        {
            string code = table.Rows.Select(r => r["Code"]).ToList()[0];
            string description = table.Rows.Select(r => r["Description"]).ToList()[0];
            string TransactionTypeAlias = (table.Rows.Any(r => r.Keys.Any(x => x.Equals("TransactionTypeAlias")))) ? table.Rows.Select(r => r["TransactionTypeAlias"]).ToList()[0] : null;

            var disbursementTypeEntity = new ApiDisbursementTypeEntity();
            disbursementTypeEntity.Code = code;
            disbursementTypeEntity.Description = description;
            disbursementTypeEntity.TransactionTypeAlias = TransactionTypeAlias;
            await _costTypeData.SearchAndCreateHardDisbursmentTypeDataAsync(disbursementTypeEntity);
        }

        [Given(@"I search and create disbursement type with barrister flag")]
        public async Task GivenISearchAndCreateDisbursementTypeWithBarristerFlag(Table table)
        {
            string code = table.Rows.Select(r => r["Code"]).ToList()[0];
            string description = table.Rows.Select(r => r["Description"]).ToList()[0];
            string TransactionTypeAlias = (table.Rows.Any(r => r.Keys.Any(x => x.Equals("TransactionTypeAlias")))) ? table.Rows.Select(r => r["TransactionTypeAlias"]).ToList()[0] : null;
            string barristerFlag = table.Rows.Select(r => r["IsBarristerFlag"]).ToList()[0];
            string hardOrSoftDisbursement = table.Rows.Select(r => r["HardOrSoftDisbursement"]).ToList()[0];
            var disbursementTypeEntity = new ApiDisbursementTypeEntity();
            disbursementTypeEntity.Code = code;
            disbursementTypeEntity.Description = description;
            disbursementTypeEntity.TransactionTypeAlias = TransactionTypeAlias;
            disbursementTypeEntity.IsBarristerFlag = barristerFlag;
            disbursementTypeEntity.IsHardDisbursementOrSoftDisbursementOption = hardOrSoftDisbursement;
            disbursementTypeEntity.TransactionTypeAlias = TransactionTypeAlias;
            disbursementTypeEntity.Code = table.Rows.Select(r => r["DisbursementCode"]).ToList()[0];
            await _costTypeData.SearchAndCreateDisbursmentTypeWithBarristerDataAsync(disbursementTypeEntity);
        }
    }
}

