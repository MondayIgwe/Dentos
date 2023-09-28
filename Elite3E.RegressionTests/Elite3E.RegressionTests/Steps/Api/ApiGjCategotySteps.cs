using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Services.GjCategorySetup;
using FluentAssertions;
using System.Threading.Tasks;
using Elite3E.RestServices.Services;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.RegressionTests.DataCreators;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiGjCategotySteps
    {
        private IProcessService _process = new ProcessService();
        private readonly IGjCategorySetupService gjCategorySetupService = new GjCategorySetupService();
        private ISessionService _session = new SessionService();
        private readonly FeatureContext _featureContext;

        public ApiGjCategotySteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I search and create a gj category with api")]
        public async Task GivenIHaveCreatedAn(Table table)
        {
            var gjCategoryEntity = table.CreateInstance<GJCategoryEntity>();

            gjCategoryEntity.GJCategoryDescription.Should().NotBeNullOrEmpty();

            if (string.IsNullOrEmpty(gjCategoryEntity.GJCategoryCode))
            {
                gjCategoryEntity.GJCategoryCode = (gjCategoryEntity.GJCategoryDescription.Length >= 16) ? gjCategoryEntity.GJCategoryDescription.Substring(0, 16) : gjCategoryEntity.GJCategoryDescription;
            }

            await new GJCategoryData().CreateGJCategoryAsync(gjCategoryEntity);
        }
    }
}
