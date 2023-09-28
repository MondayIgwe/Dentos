using Elite3E.RestServices.Entity;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class UserRoleManagementTest
    {
        
        [Test]
        public async Task CreateUserWithNoRoles()
        {
            UserRoleManagementEntity userRoleManagementEntity = new UserRoleManagementEntity()
            {
                UserName = "DummyUser2",
                DataRoleAlias = "Admin"
                //DefaultOperatingAlias = "Default"//Change to 'Firm Unit' for Staging
            };

            UserRoleData data = new UserRoleData();
            if (await data.SearchAndCreateUser(userRoleManagementEntity))
            {
                await data.SubmitUserCreation(userRoleManagementEntity);
            }
        }

        [Test]
        public async Task CreateUserWithRoles()
        {
            UserRoleManagementEntity userRoleManagementEntity = new UserRoleManagementEntity()
            {
                UserName = "Test_WithRole5",
                DataRoleAlias = "Admin",
                //DefaultOperatingAlias = "Default",
                UserRole = new List<UserRole> 
                { 
                    new() { UserRoleAlias = "0:AD:G:System Administrator"},
                    new() { UserRoleAlias = "WFTimeModifyLondonOfficeApprover_ccc"}
                }
            };

            UserRoleData data = new UserRoleData();
            if (await data.SearchAndCreateUser(userRoleManagementEntity))
            {
                await data.AddRoleToUser(userRoleManagementEntity);
                await data.SubmitUserCreation(userRoleManagementEntity);
            }
        }
    }
}