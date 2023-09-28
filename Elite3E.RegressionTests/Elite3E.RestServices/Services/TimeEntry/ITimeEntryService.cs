using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.TimeEntry
{
    public interface ITimeEntryService
    {
        Task<IRestResponse> GetFeeEranerDeatilsAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry);
        Task<IRestResponse> AddFeeEranerAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry);
        Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry);
        Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry);
        Task<IRestResponse> AddTimeTypeAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry);
        Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry);
        Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry);
        Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry);
    }
}
