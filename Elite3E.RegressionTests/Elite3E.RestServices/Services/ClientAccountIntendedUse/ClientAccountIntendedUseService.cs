using Elite3E.RestServices.Entity;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Services.ClientAccountIntendedUse
{
    public class ClientAccountIntendedUseService : IClientAccountIntendedUseService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();

        public async Task<IRestResponse> AddCLientAccountDataAsync(string sessionId, string processItemId, ApiClientIntendedUseEntity client)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/TrustIntendedUse/rows/" + client.ClientAccountId  + "/attributes/Code/value",
                        Value = client.Code
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/TrustIntendedUse/rows/" + client.ClientAccountId  + "/attributes/Description/value",
                        Value = client.Description
                    }
                }
            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);

        }
    }
}
