using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.Payer
{
    public interface IPayerService
    {
        Task<IRestResponse> AddPayerDataAsync(string sessionId, string processItemId, ApiPayerEntity payorEntity);
        Task<IRestResponse> GetEntitySearchList(string sessionId, string processItemId, ApiPayerEntity payorEntity);
        Task<IRestResponse> GetSiteSearchList(string sessionId, string processItemId, ApiPayerEntity payorEntity);
        Task<IRestResponse> AddSiteData(string sessionId, string processItemId, ApiPayerEntity payorEntity);
    }
}
