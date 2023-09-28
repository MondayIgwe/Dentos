using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.RateType
{
    public interface IRateTypeService
    {
        Task<IRestResponse> AddRateTypeDataCode(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddRateTypeDataDescription(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddRateTypeDataCurrency(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> GetEffectiveInformationAsync(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddRateTypeDataTimekeeperCheckbox(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddRateTypeDataDisbursementCheckbox(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddRateTypeDataStandardCheckbox(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddRateTypeDataFirmDefaultCheckbox(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddRateTypeDataValidForTimekeeperCheckboxes(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddRateTypeDataValidForMatterCheckboxes(string sessionId, string processItemId, ApiRateTypeEntity rateType);
        Task<IRestResponse> GetRateDetailsAsync(string sessionId, string processItemId, string rowId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddEffectiveInformationAsync(string sessionId, string processItemId, string rowId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddRateDetailsAsync(string sessionId, string processItemId, string rowId, string childRowId, ApiRateTypeEntity rateType);
        Task<IRestResponse> AddSearchResultToWorklistAsync(string sessionId, string processItemId, string BatchAddRowKey);
    }
}
