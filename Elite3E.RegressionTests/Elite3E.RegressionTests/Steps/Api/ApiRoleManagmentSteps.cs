using Elite3E.RestServices.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps.Api
{

    [Binding]
    public class ApiRoleManagmentSteps
    {
        private readonly FeatureContext _featureContext;

        public ApiRoleManagmentSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }
       
        [StepDefinition(@"I create a user with details")]
        public async Task GivenICreateUserRoles(Table table)
        {
            var userEntity = table.CreateInstance<UserRoleManagementEntity>();
            var userRoleList = new List<UserRole>();
            bool isRolesBeingAdded = table.Rows.Any(r => r.Keys.Contains("UserRoleList"));

            //Creating a user without Roles
            if ((!isRolesBeingAdded) || userRoleList.Count.Equals(0))
            {
                UserRoleData data = new UserRoleData();
                if (await data.SearchAndCreateUser(userEntity))
                {
                    await data.SubmitUserCreation(userEntity);
                }
            }
            else
            {
                //Creating a user with Roles
               
                foreach (var role in table.Rows.Select(r => r["UserRoleList"]).ToList()[0].Split(","))
                {
                    userRoleList.Add(new UserRole() { UserRoleAlias = role.Trim() });
                }

                userEntity.UserRole = userRoleList;

                UserRoleData data = new UserRoleData();
                if (await data.SearchAndCreateUser(userEntity))
                {
                    await data.AddRoleToUser(userEntity);
                    await data.SubmitUserCreation(userEntity);
                }
            }
        }
    }
}

