using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.VendorPayeeMaintenance
{
    public class VendorPayeeService: IVendorPayeeService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddVendorPayeeDataAsync(string sessionId, string processItemId, ApiVendorEntity vendor)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NewVendorPayee/rows/"+vendor.VendorId+"/attributes/Entity/value",
                        Value = vendor.Entity,
                        Alias = vendor.EntityName,
                        Id = "Entity"
                    },
                      new()
                    {
                        Op = "replace",
                        Path = "/objects/NewVendorPayee/rows/"+vendor.VendorId+"/attributes/GlobalVendor_ccc/value",
                        Value = vendor.GlobalVendor
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetVendorPayeeEntitySearchList(string sessionId, string processItemId, ApiVendorEntity vendor)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Entity",
                Path = "/objects/NewVendorPayee/rows/" + vendor.VendorId + "/attributes/Entity/aliasValue",
                Text = vendor.EntityName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);

        }


    }
}

