using Elite3E.RestServices.Builders;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services
{
    public class LookUpService : ILookUpService
    {
        public async Task<IRestResponse> GetLookUpListAsync(string sessionId, string lookUpName)
        {
            var urlExtension = "lookuplist/" + lookUpName;
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.GET)
                .WithHeader("X-3E-SessionId", sessionId)
                .ExecuteRequestAsync();
        }
        public async Task<IRestResponse> GetLookUpAsync(string sessionId, string processItemId, string body)
        {
            var urlExtension = "find/lookup/quick";
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(body)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> GetAdvancedLookUpAsync(string sessionId, string processItemId, string body)
        {
            var urlExtension = "find/lookup/advanced";
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(body)
                .ExecuteRequestAsync();
        }
        public async Task<IRestResponse> GetAdvancedWorkListBarristerFlagAsync(string sessionId, string processItemId, string body)
        {
            var urlExtension = "find/worklist/advanced";
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(body)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> GetWorkListAsync(string sessionId, string processItemId, string searchText = null)
        {
            var urlExtension = "find/worklist/quick";

            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                Text = searchText,
                Toprows = 100,
                ProcessItemId = processItemId
            }, JsonHelper.Settings);

            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(body)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> GetChildDropDownLookUpListAsync(string sessionId,string processItemId, string lookUpName,string parameter)
        {
            var urlExtension = "lookuplist/" + lookUpName;
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.GET)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithParameter("path", parameter)
                .ExecuteRequestAsync();
        }

    }
}
