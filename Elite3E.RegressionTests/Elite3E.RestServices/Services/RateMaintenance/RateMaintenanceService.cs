using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.RateMaintenance
{
    public class RateMaintenanceService : IRateMaintenance
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddRateAsync(string sessionId, string processItemId, ApiRateMaintenanceEntity rateEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Rate/rows/"+rateEntity.RateId+"/attributes/Code/value",
                        Value = rateEntity.Code
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Rate/rows/"+rateEntity.RateId+"/attributes/Description/value",
                        Value = rateEntity.Description
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Rate/rows/"+rateEntity.RateId+"/attributes/RateTypeOne/value",
                        Value = rateEntity.RateTypeValue,
                        Alias= rateEntity.RateTypeDescription,
                        Id= "RateTypeOne"
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetRateTypeOneSearchList(string sessionId, string processItemId, ApiRateMaintenanceEntity rate)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "RateType",
                ActionPath = "/objects/Rate/rows/" + rate.RateId + "/attributes/RateTypeOne/aliasValue",
                Text = rate.RateTypeDescription,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
