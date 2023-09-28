using Elite3E.RestServices.Entity;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Services.GjCategorySetup
{
    public class GjCategorySetupService : IGjCategorySetupService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public async Task<IRestResponse> AddGJCategoryAsync(string sessionId, string processItemId, GJCategoryEntity gjCategoryEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/GJCategory/rows/" + gjCategoryEntity.GJCategorySetupId + "/attributes/CategoryCode/value",
                        Value = gjCategoryEntity.GJCategoryCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/GJCategory/rows/" + gjCategoryEntity.GJCategorySetupId + "/attributes/Description/value",
                        Value = gjCategoryEntity.GJCategoryDescription
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/GJCategory/rows/" + gjCategoryEntity.GJCategorySetupId + "/attributes/IsRequireApproval_ccc/value",
                        Value = gjCategoryEntity.IsRequireApprovalCheckboxValue
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
