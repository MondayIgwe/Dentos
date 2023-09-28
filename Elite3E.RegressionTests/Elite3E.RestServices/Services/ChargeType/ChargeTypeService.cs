using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Elite3E.RestServices.Services.QueryInfo;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.ChargeType
{
    public class ChargeTypeService : IChargeTypeService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        private ILookUpService LookUpService = new LookUpService();
        private IQueryInfoService QueryInfoService = new QueryInfoService();

        public async Task<IRestResponse> GetQueryInfoResponse(string sessionId, string processItemId, ApiChargeTypeEntity chargeTypeEntity)
        {
            var body = JsonConvert.SerializeObject(new QueryInfoResquestModel()
            {
                ArchetypeId = "TransactionType",
                Path = "/objects/ChrgType/rows/" + chargeTypeEntity.ChargeTypeId + "/attributes/TransactionType/aliasValue",
                Type = 0
            }, JsonHelper.Settings);

            return await QueryInfoService.GetQueryInfoAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetLookupSearchTransactionType(string sessionId, string processItemId, ApiChargeTypeEntity chargeTypeEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "TransactionType",
                ActionPath = "/objects/ChrgType/rows/" + chargeTypeEntity.ChargeTypeId + "/attributes/TransactionType/aliasValue",
                Text = chargeTypeEntity.TransactionTypeAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddChargeTypeAsync(string sessionId, string processItemId, ApiChargeTypeEntity chargeTypeEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ChrgType/rows/" + chargeTypeEntity.ChargeTypeId + "/attributes/Code/value",
                        Value = chargeTypeEntity.ChargeCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ChrgType/rows/" + chargeTypeEntity.ChargeTypeId + "/attributes/Description/value",
                        Value = chargeTypeEntity.Description
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ChrgType/rows/" + chargeTypeEntity.ChargeTypeId + "/attributes/ChrgCatList/value",
                        Value = chargeTypeEntity.CategoryValue
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ChrgType/rows/" + chargeTypeEntity.ChargeTypeId + "/attributes/TransactionType/value",
                        Value = chargeTypeEntity.TransactionTypeValue,
                        Alias = chargeTypeEntity.TransactionTypeAlias,
                        Id = "TransactionType"
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ChrgType/rows/" + chargeTypeEntity.ChargeTypeId + "/attributes/IsActive/value",
                        Value = chargeTypeEntity.Active
                    },
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
