using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Linq;
using FluentAssertions;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiReceiptTypeSteps
    {
        private readonly FeatureContext _featureContext;
        public ApiReceiptTypeSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [StepDefinition(@"I create a receipt type with details:")]
        public async Task GivenICreateAReceiptTypeWithDetailsAsync(Table table)
        {
            var receiptType = table.CreateInstance<ApiReceiptTypeEntity>();
            receiptType.Code = (string.IsNullOrEmpty(receiptType.Code.String)) ? table.Rows.Select(r => r["Code"]).ToList()[0] : receiptType.Code;
            receiptType.Description = (string.IsNullOrEmpty(receiptType.Description.String)) ? table.Rows.Select(r => r["Description"]).ToList()[0] : receiptType.Description;

            //var receiptType = new ApiReceiptTypeEntity()
            //{
            //    Code = table.Rows.Select(r => r["Code"]).ToList()[0],
            //    Description = table.Rows.Select(r => r["Description"]).ToList()[0],
            //    BankAccountDisplayName = table.Rows.Select(r => r["BankAccountDisplayName"]).ToList()[0],
            //    CurrencyTypeDescription = table.Rows.Select(r => r["CurrencyTypeDescription"]).ToList()[0],
            //    ToleranceAmount = table.Rows.Select(r => r["ToleranceAmount"]).ToList()[0],
            //    TolerancePercentage = table.Rows.Select(r => r["TolerancePercentage"]).ToList()[0],
            //};

            receiptType.Code.String.Should().NotBeNullOrEmpty();
            receiptType.Description.String.Should().NotBeNullOrEmpty();

            var data = new ReceiptTypeData();
            await data.ReceiptTypeAsync(receiptType);
            _featureContext[StepConstants.ReceiptTypeContext] = receiptType.Code.String;
        }
    }
}
