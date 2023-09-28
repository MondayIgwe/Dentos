using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.ChargeEntry
{
    public interface IChargeEntryService
    {
        Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry);
        Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry);
        Task<IRestResponse> AddChargeTypeAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry);
        Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry);
        Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry);
        Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry);
        Task<IRestResponse> AddAmountAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry);
        Task<IRestResponse> AddCurrencyAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry);
    }
}
