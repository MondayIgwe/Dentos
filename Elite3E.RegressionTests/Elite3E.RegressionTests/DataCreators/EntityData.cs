using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.Entity;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class EntityData
    {
        private readonly IProcessService _process = new ProcessService();
        private readonly ISessionService _session = new SessionService();
        private readonly IEntityService _entityService = new EntityService();
        private readonly ILookUpService _lookUpService = new LookUpService();
        private IRestResponse _response;

        public async Task<string> SearchOrCreateAnEntityPerson(string personFullName = null)
        {
            ApiEntity personEntity = new ApiEntity();
            if (string.IsNullOrEmpty(personFullName) || personFullName.Contains(" "))
            {
                personEntity.FirstName = (string.IsNullOrEmpty(personFullName)) ? "Test_FirstName" + new Random().Next() : personFullName.Split(" ")[0];
                personEntity.LastName = (string.IsNullOrEmpty(personFullName)) ? "Test_LastName" + new Random().Next() : personFullName.Split(" ")[1];
                personEntity.EntityName = personFullName;
            }
            else
            {
                personEntity.FirstName = personFullName;
                personEntity.LastName = String.Empty;
            }

            var entityFullName = await SearchOrCreateAnEntity(personEntity);
            return entityFullName.FormattedName;
        }
        public async Task<string> SearchOrCreateAnEntityPerson(ApiEntity personEntity)
        {
            personEntity.FirstName = string.IsNullOrEmpty(personEntity.FirstName.String) ? "Test_FirstName" + new Random().Next() : personEntity.FirstName;
            personEntity.LastName = string.IsNullOrEmpty(personEntity.LastName.String) ? "Test_LastName" + new Random().Next() : personEntity.LastName;

            var entityFullName = await SearchOrCreateAnEntity(personEntity);
            return entityFullName.FormattedName;
        }
        public async Task<string> SearchOrCreateAnEntityOrganisation(string organisationName = null)
        {
            ApiEntity orgEntity = new ApiEntity();
            orgEntity.OrganisationName = (string.IsNullOrEmpty(organisationName)) ? "Org" + new Random().Next() : organisationName;
            var entityFullName = await SearchOrCreateAnEntity(orgEntity);
            return orgEntity.OrganisationName.String;
        }

        // For organistion set entity.OrganisationName
        // For person set entity.FirstName and entity.LastName
        public async Task<ApiEntity> SearchOrCreateAnEntity(ApiEntity entity)
        {
            entity = DefaultRegionalValues.GetApiEntityDefaultValues(entity);

            //Get Session Id

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.EntityProcessName);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            if (!string.IsNullOrEmpty(entity.FirstName.String))
            {
                entity.FormattedName = string.IsNullOrEmpty(entity.EntityName) ? ($"{entity.FirstName.String} {entity.LastName.String}").Trim() : entity.EntityName;

                _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, entity.FormattedName);
                var existingEntity = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

                if (existingEntity.Rows != null && existingEntity.Rows.Any(s => s.Attributes.DisplayName.Equals(entity.FormattedName)))
                {
                    _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                    _response.IsSuccessful.Should().BeTrue();

                    Console.WriteLine("The Given Person Entity Description Exists : " + entity.FormattedName);
                    return entity;
                }
                _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.EntityProcessName, ApiConstants.EntityPerson);
                _response.IsSuccessful.Should().BeTrue();

                var entityId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
                entity.Should().NotBeNull();
                Console.WriteLine("Entity Id: " + entityId);
                entity.EntityId = entityId;

                entity.FormatCode = await LookUp.GetLookUpKeyValue(sessionId, "NameFormat", entity.FormatCode.String);
                entity.EntityType = await LookUp.GetLookUpKeyValue(sessionId, "EntityType", entity.EntityType.String);
                entity.PersonType = await LookUp.GetLookUpKeyValue(sessionId, "PersonType", entity.PersonType);

                _response = await _entityService.AddEntityPersonDataAsync(processItemId, sessionId, entity);
                _response.IsSuccessful.Should().BeTrue();
            }


            if (!string.IsNullOrEmpty(entity.OrganisationName.String))
            {
                _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, entity.OrganisationName.String);

                var existingEntity = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

                if (existingEntity.Rows != null && existingEntity.Rows.Any(s => s.Attributes.DisplayName.Equals(entity.OrganisationName.String)))
                {
                    _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                    _response.IsSuccessful.Should().BeTrue();

                    Console.WriteLine("The Given Organisation Entity Description Exists : " + entity.OrganisationName.String);
                    return entity;
                }

                _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.EntityProcessName, ApiConstants.EntityOrganisation);
                _response.IsSuccessful.Should().BeTrue();

                var entityId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
                AssertionExtensions.Should((string)entityId).NotBeNull();
                Console.WriteLine("Entity Organisation Id: " + entityId);
                entity.EntityId = entityId;
                _response = await _entityService.AddEntityOrganisationNameAsync(processItemId, sessionId, entity);
                _response.IsSuccessful.Should().BeTrue();

                _response = await _entityService.AddEntityOrganisationTypeAsync(processItemId, sessionId, entity);
                _response.IsSuccessful.Should().BeTrue();

            }
            _response = await _entityService.AddEntityRelationshipAsync(processItemId, sessionId, entity);
            _response.IsSuccessful.Should().BeTrue();
            var rowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 2);
            rowId.Should().NotBeNull();
            Console.WriteLine("Relation ship  Row Id: " + rowId);

            _response = await _entityService.GetEntitySiteRowIdAsync(processItemId, sessionId, rowId, entity);
            _response.IsSuccessful.Should().BeTrue();

            var childState = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content);

            var siteRowId = childState.DataStateChanges.FirstOrDefault(x => x.Path.Contains("currentRowId")).Value.String;

            rowId.Should().NotBeNull();
            Console.WriteLine("Site row Id: " + siteRowId);

            entity.CountryCode = await LookUp.GetLookUpKeyValue(sessionId, "Country", entity.Country);
            entity.LanguagValue = await LookUp.GetLookUpKeyValue(sessionId, "Language", entity.LanguageKey);
            entity.SiteType = await LookUp.GetLookUpKeyValue(sessionId, "SiteType", entity.SiteType.String);

            _response = await _entityService.AddEntitySiteAsync(processItemId, sessionId, rowId, siteRowId, entity);
            _response.IsSuccessful.Should().BeTrue();

            //Site City
            if (!string.IsNullOrEmpty(entity.SiteDescription.String))
            {
                _response = await _entityService.AddEntityAddressCityAsync(processItemId, sessionId, rowId, siteRowId, entity);
                _response.IsSuccessful.Should().BeTrue();
            };

            //Site Post Code
            if (!string.IsNullOrEmpty(entity.PostCode.String))
            {
                _response = await _entityService.AddEntityAddressPostCodeAsync(processItemId, sessionId, rowId, siteRowId, entity);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Site Address Organisation 
            if (!string.IsNullOrEmpty(entity.AddressOrganisation.String))
            {
                _response = await _entityService.AddEntityAddressOrganisationAsync(processItemId, sessionId, rowId, siteRowId, entity);
                _response.IsSuccessful.Should().BeTrue();
            }


            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.EntityProcessName);
            _response.Content.Should().Contain("responseType\":1");
            if (_response.IsSuccessful)
            {
                Console.WriteLine("Entity Person Created : " + entity.FormattedName);
                Console.WriteLine("Entity Organistion Created : " + entity.OrganisationName);
                return entity;
            }

            Console.WriteLine("Creation of Entity failed: " + _response.StatusCode);
            return null;
        }


    }
}
