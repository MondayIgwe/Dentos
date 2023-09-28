using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.PayeeMaintenance
{
    public class PayeeMaintenanceService : IPayeeMaintenanceService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddPayeeMaintenanceDataAsync(string sessionId, string processItemId, ApiPayeeEntity payeeEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Payee/rows/"+payeeEntity.PayeeId+"/attributes/Name/value",
                        Value = payeeEntity.Name,

                    },
                      new()
                    {
                        Op = "replace",
                        Path = "/objects/Payee/rows/"+payeeEntity.PayeeId+"/attributes/Vendor/value",
                        Value = payeeEntity.VendorValue,
                        Alias = payeeEntity.VendorName,
                        Id = "Vendor"
                    },
                       new()
                    {
                        Op = "replace",
                        Path = "/objects/Payee/rows/"+payeeEntity.PayeeId+"/attributes/Entity/value",
                        Value = payeeEntity.EntityValue,
                        Alias = payeeEntity.EntityName,
                        Id = "Entity"
                    },
                       new()
                    {
                        Op = "replace",
                        Path = "/objects/Payee/rows/"+payeeEntity.PayeeId+"/attributes/Terms/value",
                        Value = payeeEntity.PaymentTermvalue,
                    },
                     new()
                    {
                        Op = "replace",
                        Path = "/objects/Payee/rows/"+payeeEntity.PayeeId+"/attributes/Office/value",
                        Value = payeeEntity.OfficeCode,
                    },
                      new()
                    {
                        Op = "replace",
                        Path = "/objects/Payee/rows/"+payeeEntity.PayeeId+"/attributes/PayeeStatus/value",
                        Value = payeeEntity.StatusCode,
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetEntitySearchList(string sessionId, string processItemId, ApiPayeeEntity entity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Entity",
                Path = "/objects/Payee/rows/" + entity.PayeeId + "/attributes/Entity/aliasValue",
                Text = entity.EntityName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetVendorSearchList(string sessionId, string processItemId, ApiPayeeEntity entity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Vendor",
                Path = "/objects/Payee/rows/" + entity.PayeeId + "/attributes/Vendor/aliasValue",
                Text = entity.VendorName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
