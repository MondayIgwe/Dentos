using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.FeeEarner
{
    public interface IFeeEarnerService
    {
        Task<IRestResponse> AddFeeEarnerDataAsync(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> AddEffectiveDatedInformationAsync(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> UpdateEffectiveDateInformationAsync(string sessionId, string processItemId, string rowId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> GetFeeEarnerEntitySearchList(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> GetFeeEarnerRateTypeSearchList(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> GetWorkflowUserQuickResponse(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> GetFeeEarnerRateTypeAdvancedSearchList(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> AddFeeEarnerRatesAsync(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> GetEffectiveDatedRatesInformationAsync(string sessionId, string processItemId, string childRowId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> AddEffectiveDatedRatesAsync(string sessionId, string processItemId, string rowId, string childRowId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> AddEDIStartDateAsync(string sessionId, string processItemId, string rowId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> AddFeeEarnerRateStartDateAsync(string sessionId, string processItemId, string rowId, string childRowId, ApiFeeEarnerEntity feeEarner);
        Task<IRestResponse> AddWorkflowUserDataAsync(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner);
    }
}
