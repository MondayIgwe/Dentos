using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.CostTypeGroup
{
    public class CostTypeGroupService : ICostTypeGroupService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddCostTypeDetail(string sessionId, string processItemId, ApiCostTypeGroupEntity costTypeGroupEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/CostTypeGroup_ccc/rows/" + costTypeGroupEntity.CostTypeId + "/childObjects/CostTypeDetail_ccc/rows/-",
                        Value = new ValueClass
                        {
                            SubclassId = "CostTypeDetail_ccc"
                        }
                    },
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddCostTypeGroupDataAsync(string sessionId, string processItemId, ApiCostTypeGroupEntity costTypeGroup)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/CostTypeGroup_ccc/rows/" + costTypeGroup.CostTypeId + "/attributes/Code/value",
                        Value = costTypeGroup.Code
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/CostTypeGroup_ccc/rows/" + costTypeGroup.CostTypeId + "/attributes/Description/value",
                        Value = costTypeGroup.Description
                    },
                      new()
                    {
                        Op = "replace",
                        Path = "/objects/CostTypeGroup_ccc/rows/"+costTypeGroup.CostTypeId+"/attributes/"+costTypeGroup.CostTypeGroupExcludeOrIncludeListOption+"/value",
                        Value = costTypeGroup.CostTypeGroupIsExcludeOrIncludeListValue
                    }
                }
            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetDisbursementTypeValue(string sessionId, string processItemId, ApiCostTypeGroupEntity costTypeGroupEntity,string costTypeRowId, string disbursementType)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "CostType",
                Path = "/objects/CostTypeGroup_ccc/rows/" + costTypeGroupEntity.CostTypeId + "/childObjects/CostTypeDetail_ccc/rows/" + costTypeRowId + "/attributes/CostType/aliasValue",
                Text = disbursementType,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> SelectCostTypeDetail(string sessionId, string processItemId, string costTypeRowId, ApiCostTypeGroupEntity costTypeGroupEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/CostTypeGroup_ccc/rows/" + costTypeGroupEntity.CostTypeId + "/childObjects/CostTypeDetail_ccc/rows/" + costTypeRowId + "/attributes/CostType/value",
                        Value = costTypeGroupEntity.CostTypeDetailId
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> SelectCostTypeGroup(string sessionId, string processItemId, ApiCostTypeGroupEntity costTypeGroupEntity)
        {
            var body = JsonConvert.SerializeObject(new AddBatchModel()
            {
                Path = "/objects/CostTypeGroup_ccc/rows/-",
                ItemIDs = new List<System.Guid>(){
                    {new(costTypeGroupEntity.CostTypeId) }
                }

            }, JsonHelper.Settings); ;

            var urlExtension = "data/batchadd";
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body,urlExtension);
        }
    }


}

