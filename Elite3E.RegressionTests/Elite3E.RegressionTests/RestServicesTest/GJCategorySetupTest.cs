using Elite3E.RestServices.Entity;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.Infrastructure.Extensions;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class GJCategorySetupTest
    {
        [Test]
        public async Task SearchAndCreateGJCategory()
        {
            var gjCategoryEntity = new GJCategoryEntity()
            {
                GJCategoryCode = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                GJCategoryDescription = "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                IsRequireApprovalCheckboxAlias = "Yes"
            };

            await new GJCategoryData().CreateGJCategoryAsync(gjCategoryEntity);
        }

        [Test]
        public async Task SearchAndDontCreateGJCategory()
        {
            var gjCategoryEntity = new GJCategoryEntity()
            {
                GJCategoryCode = "PYRLL",
                GJCategoryDescription = "Payroll"
            };

            await new GJCategoryData().CreateGJCategoryAsync(gjCategoryEntity);
        }
    }
}
