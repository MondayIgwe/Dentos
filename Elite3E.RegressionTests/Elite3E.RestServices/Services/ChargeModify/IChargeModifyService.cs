using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.ChargeModify
{
    public interface IChargeModifyService
    {
        Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify);
        Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify);
        Task<IRestResponse> AddChargeTypeAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify);
        Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify);
        Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify);
        Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify);
        Task<IRestResponse> AddAmountAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify);
        Task<IRestResponse> AddCurrencyAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify);
    }
}
