using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.DisbursementEntry
{
    public interface IDisbursementEntry
    {
        Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry);
        Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry);
        Task<IRestResponse> AddDisbursementTypeAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry);
        Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry);
        Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry);
        Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry);
        Task<IRestResponse> AddWorkRateAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry);
        Task<IRestResponse> AddCurrencyAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry);
        Task<IRestResponse> GetDisbursementTypeAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry);
    }
}
