using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.RateMaintenance
{
    public interface IRateMaintenance
    {
        Task<IRestResponse> AddRateAsync(string sessionId, string processItemId, ApiRateMaintenanceEntity rateEntity);
        Task<IRestResponse> GetRateTypeOneSearchList(string sessionId, string processItemId, ApiRateMaintenanceEntity rate);

    }
}


