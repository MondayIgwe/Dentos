using Elite3E.RestServices.Entity;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ProfileRolesTest
    {
        
        [Test]
        public async Task SearchAndVerifyProfileRoles()
        {
            ApiProfileEntity profileEntity = new ApiProfileEntity()
            {
                Profile = "PROFILE: Accounts Payable Services"
            };

            List<string> roles = await new ProfileData().SearchAndVerifyProfileRoles(profileEntity);
            roles.Should().NotBeNullOrEmpty();
        }

    }
}
