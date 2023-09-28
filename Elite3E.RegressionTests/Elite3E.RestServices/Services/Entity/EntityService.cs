using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.Entity
{
    public class EntityService : IEntityService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public async Task<IRestResponse> AddEntityPersonDataAsync(string processItemId, string sessionId, ApiEntity entity)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+ entity.EntityId +"/attributes/EntityType/value",
                        Value = entity.EntityType
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+ entity.EntityId +"/attributes/PersonType/value",
                        Value = entity.PersonType
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+ entity.EntityId +"/attributes/FirstName/value",
                        Value = entity.FirstName
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+ entity.EntityId +"/attributes/LastName/value",
                        Value = entity.LastName
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+ entity.EntityId +"/attributes/NameFormat/value",
                        Value = entity.FormatCode
                    }

                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);

        }

        public async Task<IRestResponse> AddEntityOrganisationNameAsync(string processItemId, string sessionId, ApiEntity entity)
        {


            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/" + entity.EntityId + "/attributes/OrgName/value",
                        Value = entity.OrganisationName
                    }

                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEntityOrganisationTypeAsync(string processItemId, string sessionId, ApiEntity entity)
        {


            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/" + entity.EntityId + "/attributes/EntityType/value",
                        Value = entity.EntityType
                    }

                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEntityRelationshipAsync(string processItemId, string sessionId, ApiEntity entity)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Entity/childStates/Relate/sortAttributes",
                        Value = new ValueClass
                        {
                            IsDefault = new IsDefault()
                            {
                                Id = "IsDefault",
                                Direction = -1
                            },
                            Description = new Description()
                            {
                                Id = "Description",
                                Direction = 1,
                                Order = 1
                            }
                        }
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Entity/childStates/Relate/clearUISort",
                        Value = true
                    }
                },
                Index = "0",
                Path = "/objects/Entity/rows/" + entity.EntityId + "/childObjects/Relate/rows"

            }, JsonHelper.Settings);

            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body);
        }


        public async Task<IRestResponse> GetEntitySiteRowIdAsync(string processItemId, string sessionId,string rowId,  ApiEntity entity)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/Entity/rows/" + entity.EntityId + "/childObjects/Relate/rows/"+ rowId +"/childObjects/Site/rows/-",
                        Value = new ValueClass
                        {
                            SubclassId = "Site"
                        }
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
        public async Task<IRestResponse> GetEntityRelationshipIdRowIdAsync(string processItemId, string sessionId , ApiEntity entity)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/Entity/rows/"+entity.EntityId+"/childObjects/Relate/rows/-",
                        Value = new ValueClass
                        {
                            SubclassId = "Relate"
                        }
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
        

        public async Task<IRestResponse> AddEntitySiteAsync(string processItemId, string sessionId, string rowId, string siteRowId, ApiEntity entity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+entity.EntityId+"/childObjects/Relate/rows/"+rowId+"/childObjects/Site/rows/"+siteRowId+"/attributes/Description/value",
                        Value = entity.SiteDescription
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+entity.EntityId+"/childObjects/Relate/rows/"+rowId+"/childObjects/Site/rows/"+siteRowId+"/attributes/SiteType/value",
                        Value = entity.SiteType
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+entity.EntityId+"/childObjects/Relate/rows/"+rowId+"/childObjects/Site/rows/"+siteRowId+"/attributes/Country/value",
                        Value = entity.CountryCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+entity.EntityId+"/childObjects/Relate/rows/"+rowId+"/childObjects/Site/rows/"+siteRowId+"/attributes/Street/value",
                        Value = entity.Street
                    },
                      new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+entity.EntityId+"/childObjects/Relate/rows/"+rowId+"/childObjects/Site/rows/"+siteRowId+"/attributes/Language/value",
                        Value = entity.LanguagValue
                    }
                }
            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEntityAddressCityAsync(string processItemId, string sessionId, string rowId, string siteRowId, ApiEntity entity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+entity.EntityId+"/childObjects/Relate/rows/"+rowId+"/childObjects/Site/rows/"+siteRowId+"/attributes/City/value",
                        Value = entity.City
                    }

                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEntityAddressPostCodeAsync(string processItemId, string sessionId, string rowId, string siteRowId, ApiEntity entity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+entity.EntityId+"/childObjects/Relate/rows/"+rowId+"/childObjects/Site/rows/"+siteRowId+"/attributes/ZipCode/value",
                        Value = entity.PostCode
                    }

                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEntityAddressOrganisationAsync(string processItemId, string sessionId, string rowId, string siteRowId, ApiEntity entity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Entity/rows/"+entity.EntityId+"/childObjects/Relate/rows/"+rowId+"/childObjects/Site/rows/"+siteRowId+"/attributes/OrgName/value",
                        Value = entity.AddressOrganisation
                    }

                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
