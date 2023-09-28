using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.GlobalVendor
{
    public class GlobalVendorService : IGlobalVendorService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public async Task<IRestResponse> AddGlobalVendorAsync(string sessionId, string processItemId, ApiGlobalVendorEntity globalVendorEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/GlobalVendor_ccc/rows/" + globalVendorEntity.GlobalVendorId+ "/attributes/Code/value",
                        Value = globalVendorEntity.Code
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/GlobalVendor_ccc/rows/" + globalVendorEntity.GlobalVendorId+ "/attributes/Description/value",
                        Value = globalVendorEntity.Description
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
