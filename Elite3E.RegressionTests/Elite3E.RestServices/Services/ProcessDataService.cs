using Elite3E.RestServices.Builders;
using RestSharp;

namespace Elite3E.RestServices.Services
{
    public class ProcessDataService : IProcessDataService
    {
        public async Task<IRestResponse> UpdateDataAsync(string sessionId, string processItemId, string requestBody)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource("data", Method.PATCH)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> AddDataAsync(string sessionId, string processItemId, string requestBody)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource("data", Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> AddDataAsync(string sessionId, string processItemId, string requestBody, string urlExtension = null)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> UpdateAsync(string sessionId, string requestBody)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource("data", Method.PATCH)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> PostAsync(string sessionId, string urlExtension, string requestBody)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }
        
        public async Task<IRestResponse> GetDataAsync(string sessionId, string processItemId,string parameter)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource("data", Method.GET)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithParameter("path", parameter)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> PostActionDataAsync(string sessionId, string processItemId, string requestBody)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource("data/action/Ok", Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> GetRowKeyByProcessItemIdAsync(string sessionId, string processItemId)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource("process/"+ processItemId, Method.GET)
                .WithHeader("X-3E-SessionId", sessionId)
                .ExecuteRequestAsync();

        }
        public async Task<IRestResponse> ReleaseFormAsync(string sessionId, string processItemId, string requestBody)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource("process/output/Release", Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> SubmitFormAsync(string sessionId, string processItemId, string requestBody)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource("process/output/Submit", Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> PostAllFormAsync(string sessionId, string processItemId, string requestBody)
        {
            return await new RestClientBuilder()
                .Create()
                .ForResource("process/output/Post", Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> PostOutPutProcessAsync(string sessionId, string processItemId, string requestBody, string urlExtension = null)
        {
           
            return await new RestClientBuilder()
                .Create()
                .ForResource("process/output/" + urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(requestBody)
                .ExecuteRequestAsync();
        }
    }
}
