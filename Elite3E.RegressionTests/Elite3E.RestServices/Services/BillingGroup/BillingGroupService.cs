using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.BillingGroup
{
    public class BillingGroupService : IBillingGroupService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();
        public async Task<IRestResponse> AddBillingGroupAsync(string sessionId, string processItemId,
            ApiBillingGroupEntity billingGroupEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/BillingGroup/rows/" + billingGroupEntity.Id + "/attributes/GroupName/value",
                        Value = billingGroupEntity.Name
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/BillingGroup/rows/" + billingGroupEntity.Id + "/attributes/Description/value",
                        Value = billingGroupEntity.Description
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/BillingGroup/rows/" + billingGroupEntity.Id + "/attributes/IsICB/value",
                        Value = billingGroupEntity.Icb
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/BillingGroup/rows/" + billingGroupEntity.Id +
                               "/attributes/ICBUnitDueFrom/value",
                        Value = billingGroupEntity.UnitDueFromValue,
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/BillingGroup/rows/" + billingGroupEntity.Id + "/attributes/ICBUnitDueTo/value",
                        Value = billingGroupEntity.UnitDueToValue
                    },
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetBillingGroupMatterAsync(string sessionId, string processItemId, ApiBillingGroupEntity billingGroupEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Matter",
                ActionPath = "/objects/BillingGroup/rows/" + billingGroupEntity.Id + "/childObjects/BillingGroupMatter/actions/AddByQuery",
                Text = billingGroupEntity.MatterNumber,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingGroupMatterAsync(string sessionId, string processItemId, ApiBillingGroupEntity billingGroupEntity)
        {
            var urlExtension = "data/action/AddByQuery";

            var body = JsonConvert.SerializeObject(new AddQueryModel()
            {
                Path = "/objects/BillingGroup/rows/" + billingGroupEntity.Id + "/childObjects/BillingGroupMatter/actions/AddByQuery",
                SelectedRows = new List<Guid>(billingGroupEntity.MatterKeyValue)

            }, JsonHelper.Settings); ;
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }
    }
}
