using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.PayeeMaintenance
{
    public interface IPayeeMaintenanceService
    {
        Task<IRestResponse> AddPayeeMaintenanceDataAsync(string sessionId, string processItemId, ApiPayeeEntity payeeEntity);
        Task<IRestResponse> GetVendorSearchList(string sessionId, string processItemId, ApiPayeeEntity entity);
        Task<IRestResponse> GetEntitySearchList(string sessionId, string processItemId, ApiPayeeEntity entity);

    }
}
