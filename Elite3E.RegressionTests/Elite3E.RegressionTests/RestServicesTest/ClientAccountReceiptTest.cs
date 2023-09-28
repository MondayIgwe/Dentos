using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class ClientAccountReceiptTest
    {

        [Test]
        public async Task CreateClientAccountReceipt()
        {
            var clientAccountReceipt = new ApiClientAccountReceiptEntity()
            {
                ClientAccountReceiptType = "Cash",
                ClientAccountAcct = "Singapore - SCB Bank Trust Acc - GBP",
                DocumentNumber = "Receipt_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+8"),
                Amount = new Random().Next(200, 1900),
                MatterNumber = "100000006"
            };

            await new ClientAccountReceiptData().SearchAndCreateAClientAccountReceiptDataAsync(clientAccountReceipt);

        }
    }
}
