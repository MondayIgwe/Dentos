using Boa.Constrictor.Screenplay;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using System;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps
{
    [Binding]
    public class LookUpDataSteps
    {

        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        public readonly CostTypeData _costTypeData = new();

        public LookUpDataSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }

        [Given(@"I search for '(.*)' transaction type")]
        public void GivenISearchForTransactionType(string transactionDescription)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.TransactionType));

            var transactionType = new TransactionTypeEntity { Description = transactionDescription };

            _featureContext[StepConstants.TransactionTypeContext] = transactionType;

            _actor.AttemptsTo(QuickFind.Search(transactionType.Description));

        }

        [Given(@"I search for '(.*)' charge type")]
        public async Task GivenISearchForChargeType(string chargeTypeDescription)
        {
            var chargeTypeData = new ChargeTypeData();
            var description =  await chargeTypeData.SerchAndCreateChargeTypeDataAsync(chargeTypeDescription);

            if (description != null)
            {
                _actor.AttemptsTo(SearchProcess.ByName(Process.ChargeType));

                var chargeType = new ChargeTypeEntity() { Description = chargeTypeDescription };

                _featureContext[StepConstants.ChargeTypeContext] = chargeType;

                _actor.AttemptsTo(QuickFind.Search(chargeType.Description));
            }
            else
            {
                throw new Exception("Error occured in Api while creating Charge Type");
            }

        }

        [Given(@"I search for '(.*)' disbursement type")]
        public async Task GivenISearchForDisbursementType(string disbursementTypeDescription)
        {
            var disbursementTypeEntity = new ApiDisbursementTypeEntity();
            disbursementTypeEntity.Description = disbursementTypeDescription;
            var description = await _costTypeData.SearchAndCreateHardDisbursmentTypeDataAsync(disbursementTypeEntity);

            if (description != null)
            {
                _actor.AttemptsTo(SearchProcess.ByName(Process.DisbursementType));
                var disbursementType = new DisbursementTypeEntity() { Description = disbursementTypeDescription };
                _featureContext[StepConstants.DisbursementTypeContext] = disbursementType;
                _actor.AttemptsTo(QuickFind.Search(disbursementType.Description));
            }
            else
            {
                throw new Exception("Error occured in Api while creating Charge Type");
            }
        }


        [Given(@"I search for '(.*)' receipt type")]
        public async Task GivenISearchForReceiptType(string receiptTypeDescription)
        {
            var data = new ReceiptTypeData();
            var apiReceiptType = new ApiReceiptTypeEntity()
            {
                Description = receiptTypeDescription,
            };

            var description = await data.ReceiptTypeAsync(apiReceiptType);

            if (description != null)
            {
                _actor.AttemptsTo(SearchProcess.ByName(Process.ReceiptType));

                var receiptType = new ReceiptTypeEntity() { Description = description };

                _featureContext[StepConstants.ReceiptTypeContext] = receiptType;

                _actor.AttemptsTo(QuickFind.Search(description));
            }
            else
            {
                throw new Exception("Error occured in Api while creating Receipt Type");
            }

        }


        [Given(@"I search for '(.*)' time type")]
        public void GivenISearchForTimeType(string timeTypeDescription)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.TimeType));

            var timeType = new TimeTypeEntity() { Description = timeTypeDescription };

            _featureContext[StepConstants.TimeTypeContext] = timeType;

            _actor.AttemptsTo(QuickFind.Search(timeType.Description));

        }


        [Given(@"I search for '(.*)' unallocated type")]
        public void GivenISearchForUnallocatedType(string unallocatedTypeDescription)
        {
            _actor.AttemptsTo(SearchProcess.ByName(Process.UnallocatedType));

            var unallocatedType = new UnallocatedTypeEntity() { Description = unallocatedTypeDescription };

            _featureContext[StepConstants.UnallocatedTypeContext] = unallocatedType;

            _actor.AttemptsTo(QuickFind.Search(unallocatedType.Description));

        }

    }
}
