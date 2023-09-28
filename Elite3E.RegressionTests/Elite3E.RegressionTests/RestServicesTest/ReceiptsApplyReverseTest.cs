using Elite3E.RestServices.Entity;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using System.Collections.Generic;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class ReceiptsApplyReverseTest
    {
        [Test]
        public async Task AddReceiptWithInvoice()
        {
            var entity = new ApiReceiptsApplyReverseEntity()
            {
                ReceiptTypeAlias = "AEABUHSBC01GBP",
                DocumentNumber = "Doc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                Narrative = StepArgumentExtension.ReplaceDynamicValues("{Auto}+15"),
                InvoiceNumber = "3000-200000983"
            };

            await new ReceiptsApplyReverseData().AddReceiptWithInvoice(entity);
        }
    }
}
