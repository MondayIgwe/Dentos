using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.Payer
{
    public class PayerService : IPayerService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddPayerDataAsync(string sessionId, string processItemId, ApiPayerEntity payorEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new Changes()
                    {
                        Op = "replace",
                        Path = "/objects/Payor/rows/"+payorEntity.PayerId+"/attributes/Entity/value",
                        Value = payorEntity.EntityCode,
                        Id="Entity",
                        Alias= payorEntity.EntityName

                    },
                      new Changes()
                    {
                        Op = "replace",
                        Path = "/objects/Payor/rows/"+payorEntity.PayerId+"/attributes/DisplayName/value",
                        Value = payorEntity.PayerName,
                    },
                       new Changes()
                    {
                        Op = "replace",
                        Path = "/objects/Payor/rows/"+payorEntity.PayerId+"/attributes/Site/value",
                        Value = payorEntity.SiteCode,
                        Id="Site",
                        Alias=payorEntity.Site

                    },
                       new Changes()
                    {
                        Op = "replace",
                        Path = "/objects/Payor/rows/"+payorEntity.PayerId+"/attributes/TaxArea/value",
                        Value = payorEntity.TaxAreaCode,

                    }

                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetEntitySearchList(string sessionId, string processItemId, ApiPayerEntity payorEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Entity",
                Path = "/objects/Payor/rows/" + payorEntity.PayerId + "/attributes/Entity/aliasValue",
                Text = payorEntity.EntityName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetSiteSearchList(string sessionId, string processItemId, ApiPayerEntity payorEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Site",
                Path = "/objects/Payor/rows/" + payorEntity.PayerId + "/attributes/Site/aliasValue",
                Text = payorEntity.Site,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddSiteData(string sessionId, string processItemId, ApiPayerEntity payorEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                       new Changes()
                    {
                        Op = "replace",
                        Path = "/objects/Payor/rows/"+payorEntity.PayerId+"/attributes/Site/value",
                        Value = payorEntity.SiteCode,
                        Id="Site",
                        Alias=payorEntity.Site
                    },
                }

            },
                JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }


    }

}

