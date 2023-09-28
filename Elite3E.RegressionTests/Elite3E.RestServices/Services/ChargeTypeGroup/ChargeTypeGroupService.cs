using Elite3E.RestServices.Entity;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Services.ChargeTypeGroup
{
    public class ChargeTypeGroupService : IChargeTypeGroupService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddChargeTypeDetail(string sessionId, string processItemId, ApiChargeTypeGroupEntity chargeTypeGroupEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/ChrgTypeGroup_ccc/rows/" + chargeTypeGroupEntity.ChargeTypeGroupId + "/childObjects/ChrgTypeDetail_ccc/rows/-",
                        Value = new ValueClass
                        {
                            SubclassId = "ChrgTypeDetail_ccc"
                        }
                    },
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> SelectChargeTypeGroup(string sessionId, string processItemId, ApiChargeTypeGroupEntity chargeTypeGroupEntity)
        {
            var body = JsonConvert.SerializeObject(new AddBatchModel()
            {
                Path = "/objects/ChrgTypeGroup_ccc/rows/-",
                ItemIDs = new List<System.Guid>(){
                    {new(chargeTypeGroupEntity.ChargeTypeGroupId) }
                }

            }, JsonHelper.Settings); ;

            var urlExtension = "data/batchadd";
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> GetChargeTypeValue(string sessionId, string processItemId, ApiChargeTypeGroupEntity chargeTypeGroupEntity,
            string chargeTypeRowId, string chargeType)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "ChargeType",
                Path = "/objects/ChrgTypeGroup_ccc/rows/" + chargeTypeGroupEntity.ChargeTypeGroupId + "/childObjects/ChrgTypeGroup_ccc/rows/" + chargeTypeRowId + "/attributes/ChrgType/aliasValue",
                Text = chargeType,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddChargeTypeGroupAsync(string sessionId, string processItemId, ApiChargeTypeGroupEntity chargeTypeGroupEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ChrgTypeGroup_ccc/rows/" + chargeTypeGroupEntity.ChargeTypeGroupId + "/attributes/Code/value",
                        Value = chargeTypeGroupEntity.ChargeTypeGroupCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ChrgTypeGroup_ccc/rows/" + chargeTypeGroupEntity.ChargeTypeGroupId + "/attributes/Description/value",
                        Value = chargeTypeGroupEntity.ChargeTypeGroupDescription
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ChrgTypeGroup_ccc/rows/" + chargeTypeGroupEntity.ChargeTypeGroupId + "/attributes/"+chargeTypeGroupEntity.ChargeTypeGroupExcludeOrIncludeListOption+"/value",
                        Value = chargeTypeGroupEntity.ChargeTypeGroupIsExcludeOrIncludeListValue
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> SelectChargeTypeDetail(string sessionId, string processItemId, string chargeTypeRowId, ApiChargeTypeGroupEntity chargeTypeGroupEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ChrgTypeGroup_ccc/rows/" + chargeTypeGroupEntity.ChargeTypeGroupId + "/childObjects/ChrgTypeDetail_ccc/rows/" + chargeTypeRowId + "/attributes/ChrgType/value",
                        Value = chargeTypeGroupEntity.ChargeTypeDetailId
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
