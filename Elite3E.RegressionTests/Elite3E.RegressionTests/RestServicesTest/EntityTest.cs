using Elite3E.RestServices.Services;
using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Services.Entity;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class EntityTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IEntityService _entityService = new EntityService();
        public ILookUpService _lookUpService = new LookUpService();
        private readonly EntityData _entityData = new();

        // Manaully run test to create random data 
        [Test]
        public async Task CreateAnEntityPerson()
        {
            await _entityData.SearchOrCreateAnEntityPerson();
        }

        [Test]
        public async Task CreateAnEntityPersonWithFullName()
        {
            await _entityData.SearchOrCreateAnEntityPerson("James Bond007");
        }

        // Manaully run test to create random data 
        [Test]
        public async Task CreateAnEntityOrganization()
        {
            await _entityData.SearchOrCreateAnEntityOrganisation();
        }

        [Test]
        public async Task CreateAnEntityOrganizationWithOrganizationName()
        {
            await _entityData.SearchOrCreateAnEntityOrganisation("Dentons Organisation Test");
        }

    }
}
