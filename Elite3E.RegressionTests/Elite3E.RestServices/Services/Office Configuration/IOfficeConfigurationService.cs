using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.Office_Configuration
{
    public interface IOfficeConfigurationService
    {
        Task<IRestResponse> AddOfficeConfigurationDataAsync(string sessionId, string processItemId, ApiOfficeConfiguration officeConfig);
        Task<IRestResponse> GetLookupSearchPayee(string sessionId, string processItemId, ApiOfficeConfiguration officeConfi);
        Task<IRestResponse> GetLookupTimekeeperLeaver(string sessionId, string processItemId, ApiOfficeConfiguration officeConfig);
    }
}
