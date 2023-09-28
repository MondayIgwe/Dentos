using Elite3E.RestServices.Entity;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;


namespace Elite3E.RegressionTests.RestServicesTest
{
    public class FeeEarnerTest
    {
        [Test]
        public async Task CreateAFeeEarner()
        {
            var feeEarner = new ApiFeeEarnerEntity()
            {
                Office = "London (EU)",
                Department = "UKME_Accounts",
                Section = "UKME_Accounts",
                Title = "Full Interest Partner",
                RateClass = "Premium",
                //RateTypeDescription = "Standard",
                EffectiveRate = "100",
                EffectiveRateCurrencyDescription = "GBP - British Pound"
            };
            var entity = new ApiEntity()
            {
                FirstName = "Test_FirstName" + new Random().Next(),
                LastName = "Test_LastName" + new Random().Next()
            };

            await new FeeEarnerData().SearchAndCreateFeeEranerData(feeEarner, entity);

        }
    }
}
