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
    public class ProformaEditTest
    {
        [Test]
        public async Task SearchAndBillProforma()
        {
            //You can provide either Matter Number or Matter Name

            List<ApiProformaEntity> list = new List<ApiProformaEntity>
            {
                new ApiProformaEntity(){ MatterName = "auto_f8ca14c5-73bf-4ee6-a5a3-6b0f3dcb1f52" },//expected error
                new ApiProformaEntity(){ MatterNumber = "110050014" },//Expected Skip
                new ApiProformaEntity(){ MatterNumber = "110050006" }
            };

            foreach (var proformaEditEntity in list)
            {
                await new ProformaEditData().SearchAndBillProformaAsync(proformaEditEntity);
            }
        }
    }
}
