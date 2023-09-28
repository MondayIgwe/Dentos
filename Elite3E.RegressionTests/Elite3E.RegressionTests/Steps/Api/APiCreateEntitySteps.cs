using Elite3E.RegressionTests.StepHelpers;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.RestServices.Entity;
using System.Linq;
using System;
using Elite3E.Infrastructure.Entity;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class APiCreateEntitySteps
    {
        private readonly FeatureContext _featureContext;
        private readonly EntityData _entityData = new();

        public APiCreateEntitySteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I create a person entity '([^']*)'")]
        public async Task GivenICreateAPersonEntity(string personEntityName)
        {
            var entityName = await _entityData.SearchOrCreateAnEntityPerson(personEntityName);
            _featureContext[StepConstants.Entity] = entityName;
        }

        [Given(@"I create a person entity")]
        public async Task GivenICreateAPersonEntity(Table table)
        {
            var entity = table.CreateInstance<PersonEntity>();
            var apiEntity = new ApiEntity()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };

            var entityName = await _entityData.SearchOrCreateAnEntityPerson(apiEntity);
            _featureContext[StepConstants.Entity] = entityName;
        }

        [StepDefinition(@"I create an entity organisation with details")]
        public async Task ThenICreateAnEntityOrganisationWithDetails(Table table)
        {
            var entity = table.CreateInstance<OrganisationEntity>();
            var apiEntity = new ApiEntity()
            {
                OrganisationName = entity.OrganisationName,
                EntityType = entity.EntityType,
                SiteType = entity.SiteType,
                SiteDescription = entity.Description,
                Street = entity.Street,
                LanguagValue = entity.Language
            };

            var entityName = await _entityData.SearchOrCreateAnEntity(apiEntity);
            _featureContext[StepConstants.Entity] = entityName.OrganisationName.String;
        }


    }
}
