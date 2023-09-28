using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.Client
{
    public interface IClientService
    {
        Task<IRestResponse> GetClientEntitySearchList(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> AddClientDataAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> AddEffectiveDatedInformationAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> GetClientBillingFeeEarnerSearchList(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> GetClientResponsibleFeeEarnerSearchList(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> GetClientSupervisingFeeEarnerSearchList(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> GetClientOpeningFeeEarnerSearchList(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> GetClientInvoiceSiteSearchList(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> UpdateEffectiveDateInformationAsync(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> AddClientInvoiceSiteDataAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> SelectTheClientAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> GetBillingRulesValidationListAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> AddBillingRulesValidationListAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> GetExistingBillingRulesValidationListAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> AddDateOpendAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client);
        Task<IRestResponse> AddEDIStartDateAsync(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client);
    }
}
