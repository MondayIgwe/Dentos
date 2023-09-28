using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.VendorMaintenance
{
    public class VendorService : IVendorService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddVendorDataAsync(string sessionId, string processItemId, ApiVendorEntity vendor)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Vendor/rows/"+vendor.VendorId+"/attributes/Entity/value",
                        Value = vendor.Entity,
                        Alias = vendor.EntityName,
                        Id = "Entity"
                    },
                      new()
                    {
                        Op = "replace",
                        Path = "/objects/Vendor/rows/"+vendor.VendorId+"/attributes/GlobalVendor_ccc/value",
                        Value = vendor.GlobalVendorKey
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetVendorEntitySearchList(string sessionId, string processItemId, ApiVendorEntity vendor)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Entity",
                Path = "/objects/Vendor/rows/" + vendor.VendorId + "/attributes/Entity/aliasValue",
                Text = vendor.EntityName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);

        }


    }
}
