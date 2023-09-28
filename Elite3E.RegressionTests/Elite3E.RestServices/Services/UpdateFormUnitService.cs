using Elite3E.RestServices.Builders;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Elite3E.RestServices.Models.RequestModels.OpenProcess;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services
{
    public class UpdateFormUnitService : IUpdateFormUnitService
    {
        public async Task<IRestResponse> GetUnitsAnync(string sessionId, string processItemId)
        {
            var urlExtension = "process/folder";
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.GET)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithHeader("X-3E-SessionId", sessionId)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> PatchNewUnitAsync(string sessionId, string processItemId, string unitToUpdate)
        {
            var body = JsonConvert.SerializeObject(new Changes()
            {
                Path = "/unit",
                Value = unitToUpdate
            }, JsonHelper.Settings);

            var urlExtension = "process/folder";
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.PATCH)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(body)
                .ExecuteRequestAsync();
        }
    }
}
