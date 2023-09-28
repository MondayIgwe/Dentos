using Elite3E.RestServices.Entity;
using NUnit.Framework;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class ReceiptTypeTest
    {
        [Test]
        public async Task ReceiptTypeTestAsync()
        {
            var receiptType = new ApiReceiptTypeEntity();
            var data = new ReceiptTypeData();
            await data.ReceiptTypeAsync(receiptType);
        }       
    }
}
