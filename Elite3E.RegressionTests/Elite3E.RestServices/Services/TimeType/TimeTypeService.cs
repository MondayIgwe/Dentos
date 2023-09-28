using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.TimeType
{
    public class TimeTypeService : ITimeTypeService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddTimeTypeDeatilsAsync(string sessionId, string processItemId, ApiTimeTypeEntity timeType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeType/rows/" + timeType.Id + "/attributes/Code/value",
                        Value = timeType.Code
                    },
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeType/rows/" + timeType.Id + "/attributes/Description/value",
                        Value = timeType.Description
                    },
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeType/rows/" + timeType.Id + "/attributes/Currency/value",
                        Value = timeType.CurrencyCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeType/rows/" + timeType.Id + "/attributes/TransactionType/value",
                        Value = timeType.TransactionTypeCode,
                        Alias = timeType.TransactionType,
                        Id = "TransactionType"
                    }

                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetTransactionTypeDeatilsAsync(string sessionId, string processItemId, ApiTimeTypeEntity timeType)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "TransactionType",
                ActionPath = "/objects/TimeType/rows/" + timeType.Id + "/attributes/TransactionType/aliasValue",
                Text = timeType.TransactionType,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
