using Elite3E.RestServices.Builders;
using RestSharp;

namespace Elite3E.RestServices.Services.QueryInfo
{
    public class QueryInfoService : IQueryInfoService
    {
        public async Task<IRestResponse> GetQueryInfoAsync(string sessionId, string processItemId, string body)
        {
            var urlExtension = "find/queryinfo";
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(body)
                .ExecuteRequestAsync();
        }
    }
}
