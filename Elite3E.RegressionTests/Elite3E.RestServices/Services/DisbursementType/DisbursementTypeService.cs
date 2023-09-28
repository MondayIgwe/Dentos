using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.DisbursementType
{
    public class DisbursementTypeService : IDisbursementTypeService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        private ILookUpService LookUpService = new LookUpService();
        // private IQueryInfoService QueryInfoService = new QueryInfoService();


        public async Task<IRestResponse> AddDisbursementTypeDataDataAsync(string sessionId, string processItemId, ApiDisbursementTypeEntity disbursementType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/CostType/rows/" + disbursementType.DisbursementTypeId + "/attributes/Code/value",
                        Value = disbursementType.Code
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/CostType/rows/" + disbursementType.DisbursementTypeId + "/attributes/Description/value",
                        Value = disbursementType.Description
                    },
                      new()
                    {
                        Op = "replace",
                        Path = "/objects/CostType/rows/" + disbursementType.DisbursementTypeId + "/attributes/"+disbursementType.IsHardDisbursementOrSoftDisbursementOption+"/value",
                        Value = disbursementType.IsHardDisbursementOrSoftDisbursementValue
                    },
                       new()
                    {
                        Op = "replace",
                        Path = "/objects/CostType/rows/" + disbursementType.DisbursementTypeId + "/attributes/TransactionType/value",
                        Value = disbursementType.TransactionTypeValue,
                        Alias = disbursementType.TransactionTypeAlias,
                        Id = "TransactionType"
                    },
                }
            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBarristerFlag(string sessionId, string processItemId, ApiDisbursementTypeEntity disbursementType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/CostType/rows/" + disbursementType.DisbursementTypeId + "/attributes/IsBarrister_ccc/value",
                        Value = disbursementType.IsBarristerFlag
                    }
                }
            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
        public async Task<IRestResponse> GetLookupSearchTransactionType(string sessionId, string processItemId, ApiDisbursementTypeEntity disbursementTypeEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "TransactionType",
                ActionPath = "/objects/CostType/rows/" + disbursementTypeEntity.DisbursementTypeId + "/attributes/TransactionType/aliasValue",
                Text = disbursementTypeEntity.TransactionTypeAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetDisbursementTypeAdvancedSearchList(string sessionId, string processItemId, ApiDisbursementTypeEntity disbursementTypeEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
               
                Select = new Select()
                {
                    Where = new Where()
                    {
                        Operator = "And",
                        Predicates = new List<Predicate>
                        {
                            new Predicate()
                            {
                                Attribute = "IsBarrister_ccc",
                                Operator = "IsEqualTo",
                                Value = disbursementTypeEntity.IsBarristerFlag
                            },
                            new Predicate()
                            {
                                Attribute = "Description",
                                Operator = "IsEqualTo",
                                Value = disbursementTypeEntity.Description.String
                            }
                        }
                    },
                    Archetype = "CostType",
                    ArchetypeType = 1
                },
                Toprows = 100,
                ProcessItemId = processItemId,
            }, JsonHelper.Settings);

            return await LookUpService.GetAdvancedWorkListBarristerFlagAsync(sessionId, processItemId, body);
        }
    }
}
