using Elite3E.RestServices.Entity;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.DataCreators.DefaultData;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class DepartmentTest
    {
        [Test]
        public async Task SearchAndCreateDepartmentRequiredVars()
        {
            var departmentEntity = new ApiDepartmentEntity()
            {
                DepartmentCode = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                Description = "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                GLDepartmentAlias = "Default"
            };

            await new DepartmentData().SearchAndCreateDepartmentAsync(departmentEntity);
        }

        [Test]
        public async Task SearchAndCreateDepartmentAllVars()
        {
            var departmentEntity = DefaultRegionalValues.GetDynamicDepartmentDefaultValues();

            await new DepartmentData().SearchAndCreateDepartmentAsync(departmentEntity);
        }

        [Test]
        public async Task SearchAndDontCreateDepartment()
        {
            var departmentEntity = new ApiDepartmentEntity()
            {
                DepartmentCode = "1000",
                Description = "Default",
                GLDepartmentAlias = "Default",
                DepartmentGroupAlias = "Business Services",
                IsDefaultCheckBoxAlias = "No",
                IsActiveCheckBoxAlias = "No",
                StartDate = "2022-02-05", //(yyyy-MM-dd)
                EndDate = "2022-02-10" //(yyyy-MM-dd)
            };

            await new DepartmentData().SearchAndCreateDepartmentAsync(departmentEntity);
        }

        [Test]
        public async Task SearchAndDontCreateDepartment2()
        {
            var departmentEntity = new ApiDepartmentEntity()
            {
                DepartmentCode = "0000",
                Description = "Nothing",
                GLDepartmentAlias = "Default",
                DepartmentGroupAlias = "Business Services",
                IsDefaultCheckBoxAlias = "No",
                IsActiveCheckBoxAlias = "No",
                StartDate = "2022-02-05", //(yyyy-MM-dd)
                EndDate = "2022-02-10" //(yyyy-MM-dd)
            };

            await new DepartmentData().SearchAndCreateDepartmentAsync(departmentEntity);
        }
    }
}
