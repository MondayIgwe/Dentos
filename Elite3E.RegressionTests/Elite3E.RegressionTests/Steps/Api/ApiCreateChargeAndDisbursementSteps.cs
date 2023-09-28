using System.Collections.Generic;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using TechTalk.SpecFlow;
using Elite3E.RestServices.Entity;
using Elite3E.RegressionTests.StepHelpers;
using TechTalk.SpecFlow.Assist;
using System.Linq;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiCreateChargeAndDisbursementSteps
    {
        private readonly FeatureContext _featureContext;

        public ApiCreateChargeAndDisbursementSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"the cost type group exists")]
        public async Task GivenTheCostTypeGroupExists(Table table)
        {
            foreach (var row in table.Rows)
            {
                var costTypeGroupEntity = new ApiCostTypeGroupEntity()
                {
                    Code = row[ColumnNames.Code],
                    Description = row[ColumnNames.Description],
                    CostTypeGroupExcludeOrIncludeListOption = row[ColumnNames.CostTypeGroupExcludeOrIncludeList],
                    CostTypeGroupIsExcludeOrIncludeListValue = 1
                };

                _featureContext[StepConstants.CostTypeGroupEntityContext] = costTypeGroupEntity;

                var costTypeData = new CostTypeData();
                await costTypeData.SearchDisbursementTypeGroupAndAddDisbursementTypeDataAsync(costTypeGroupEntity,
                    null);
            }
        }

        [Given(@"the below disbursement type added to the group")]
        public async Task GivenTheBelowDisbursementTypeAddedToTheGroup(Table table)
        {

            var disbursementTypeEntityList = new List<ApiDisbursementTypeEntity>();

            foreach (var row in table.Rows)
            {
                disbursementTypeEntityList.Add(new ApiDisbursementTypeEntity()
                {
                    Code = row[ColumnNames.Code],
                    Description = row[ColumnNames.Description],
                    TransactionTypeAlias = row[ColumnNames.TransactionType],
                    IsHardDisbursementOrSoftDisbursementOption = row[ColumnNames.HardOrSoftDisbursement],
                    IsHardDisbursementOrSoftDisbursementValue = 1
                });
            }

            var costTypeGroupEntity =
                (ApiCostTypeGroupEntity)_featureContext[StepConstants.CostTypeGroupEntityContext];

            _featureContext[StepConstants.CostTypeGroupEntityContext] = costTypeGroupEntity;

            var costTypeData = new CostTypeData();
            await costTypeData.SearchDisbursementTypeGroupAndAddDisbursementTypeDataAsync(costTypeGroupEntity,
                disbursementTypeEntityList);

        }

        [StepDefinition(@"the charge type group exists")]
        public async Task GivenTheChargeTypeGroupExists(Table table)
        {
            foreach (var row in table.Rows)
            {
                var chargeTypeGroupEntity = new ApiChargeTypeGroupEntity()
                {
                    ChargeTypeGroupCode = row[ColumnNames.Code],
                    ChargeTypeGroupDescription = row[ColumnNames.Description],
                    ChargeTypeGroupExcludeOrIncludeListOption = row[ColumnNames.ChargeTypeGroupExcludeOrIncludeList],
                    ChargeTypeGroupIsExcludeOrIncludeListValue = 1
                };

                _featureContext[StepConstants.ChargeTypeGroupEntityContext] = chargeTypeGroupEntity;

                var chargeTypeData = new ChargeTypeData();
                await chargeTypeData.SearchChargeTypeGroupAndAddChargeTypeDataAsync(chargeTypeGroupEntity, null);
            }

        }
        [Given(@"the below disbursement types are available")]
        public async Task GivenTheBelowDisbursementTypesAreAvailable(Table table)
        {
            var costTypeData = new CostTypeData();
            foreach (var row in table.Rows)
            {
                var disbursementType = new ApiDisbursementTypeEntity()
                {
                    Code = row[ColumnNames.Code],
                    Description = row[ColumnNames.Description],
                    TransactionTypeAlias = row[ColumnNames.TransactionType],
                    IsHardDisbursementOrSoftDisbursementOption = row[ColumnNames.HardOrSoftDisbursement],
                    IsHardDisbursementOrSoftDisbursementValue = 1
                };

                await costTypeData.SearchAndCreateDisbursementTypeDataAsync(disbursementType);
            }
        }

        [Given(@"the below Charge types are available")]
        public async Task GivenTheBelowChargeTypesAreAvailable(Table table)
        {
            var chargeTypeData = new ChargeTypeData();

            foreach (var row in table.Rows)
            {
                var chargeTypeEntity = new ApiChargeTypeEntity()
                {
                    ChargeCode = row[ColumnNames.Code],
                    Description = row[ColumnNames.Description],
                    TransactionTypeAlias = row[ColumnNames.TransactionType],
                    CategoryInput = row[ColumnNames.Category],
                    Active = "1"
                    //ChargeTypeGroupDescription = row[ColumnNames.GroupName],
                };

                await chargeTypeData.SearchAndCreateChargeTypeDataAsync(chargeTypeEntity);
            }
        }


        [StepDefinition(@"the below charge type added to the group")]
        public async Task GivenTheBelowChargeTypeAddedToTheGroup(Table table)
        {
            var chargeTypeEntityList = new List<ApiChargeTypeEntity>();

            foreach (var row in table.Rows)
            {
                chargeTypeEntityList.Add(new ApiChargeTypeEntity()
                {
                    ChargeCode = row[ColumnNames.Code],
                    Description = row[ColumnNames.Description],
                    TransactionTypeAlias = row[ColumnNames.TransactionType],
                    CategoryInput = row[ColumnNames.Category],
                    Active = "1"
                    //ChargeTypeGroupDescription = row[ColumnNames.GroupName],
                });
            }

            var chargeTypeGroupEntity =
                (ApiChargeTypeGroupEntity)_featureContext[StepConstants.ChargeTypeGroupEntityContext];

            var chargeTypeData = new ChargeTypeData();
            await chargeTypeData.SearchChargeTypeGroupAndAddChargeTypeDataAsync(chargeTypeGroupEntity, chargeTypeEntityList);
        }

        [Given(@"I add a charge modify with api")]
        public async Task GivenIAddAChargeModifyWithApi(Table table)
        {
            var chargeModifyEntity = table.CreateInstance<ApiChargeModifyEntity>();

            //Matter numbers are generated at runtime, if one is not provided, set to default.
            if (string.IsNullOrEmpty(chargeModifyEntity.MatterNumber))
                chargeModifyEntity.MatterNumber = (_featureContext.Any(x => x.Key.Equals(StepConstants.MatterNumberContext))) ? _featureContext[StepConstants.MatterNumberContext].ToString() : chargeModifyEntity.MatterNumber;

            await new ChargeModifyData().CreateChargeModifyAsync(chargeModifyEntity);
        }
    }
}
