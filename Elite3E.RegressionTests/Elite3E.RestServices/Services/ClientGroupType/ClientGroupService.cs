using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.ClientGroupType
{
    public class ClientGroupService : IClientGroupService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();

        public async Task<IRestResponse> AddClientTypeGroupDataAsync(string sessionId, string processItemId, ApiClientGroupTypeEntity clientGroup)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ClientGroupType_ccc/rows/" + clientGroup.Id + "/attributes/Code/value",
                        Value = clientGroup.GroupCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ClientGroupType_ccc/rows/" + clientGroup.Id + "/attributes/Description/value",
                        Value = clientGroup.GroupDescription
                    }
                }
            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

    }


}

