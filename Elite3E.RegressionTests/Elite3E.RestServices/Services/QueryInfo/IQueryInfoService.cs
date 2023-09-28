using RestSharp;

namespace Elite3E.RestServices.Services.QueryInfo
{
    public interface IQueryInfoService
    {
        Task<IRestResponse> GetQueryInfoAsync(string sessionId, string processItemId, string body);
    }
}
