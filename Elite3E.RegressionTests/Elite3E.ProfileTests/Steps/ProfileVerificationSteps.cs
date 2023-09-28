using System.Collections.Generic;
using System.Linq;
using Elite3E.ProfileTests.Constants;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Entity;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Elite3E.ProfileTests.Steps
{
    [Binding]
    public class ProfileVerificationSteps
    {
        private readonly FeatureContext _featureContext;

        public ProfileVerificationSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [When(@"I search the profile ""(.*)""")]
        public void WhenISearchTheProfile(string profile)
        {
            ProfileData profileData = new ProfileData();
            ApiProfileEntity profileEntity = new ApiProfileEntity();
            profileEntity.Profile = profile;
            _featureContext[ProfileConstants.ProfileRolesContext] = profileData.SearchAndVerifyProfileRoles(profileEntity).Result;
        }

        [Then(@"the below roles are available")]
        public void ThenTheBelowRolesAreAvailable(Table table)
        {
            var actualRoles = ((List<string>)_featureContext[ProfileConstants.ProfileRolesContext]);
            var expectedRoles = table.Rows.Select(r => r[ProfileConstants.Roles]);
            var actualCont = ((List<string>)_featureContext[ProfileConstants.ProfileRolesContext]).Count();
            var expectedCount = table.Rows.Select(r => r[ProfileConstants.Roles]).Count();

            actualCont.Should().Be(expectedCount);
            actualRoles.Should().BeEquivalentTo(expectedRoles);

        }

        [Then(@"the are no roles available for this profile")]
        public void ThenTheAreNoRolesAvailableForThisProfile()
        {
            var actualRoles = ((List<string>)_featureContext[ProfileConstants.ProfileRolesContext]);
            actualRoles.Should().BeNullOrEmpty();

            var actualCont = ((List<string>)_featureContext[ProfileConstants.ProfileRolesContext]).Count();
            actualCont.Should().Be(0);
        }


    }
}
