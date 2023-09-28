using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.DisbursementModify
{
    public interface IDisbursementModify
    {
        Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementEntry);
        Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementEntry);
        Task<IRestResponse> AddDisbursementTypeAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementEntry);
        Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementEntry);
        Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementEntry);
        Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementEntry);
        Task<IRestResponse> AddWorkRateAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementEntry);
        Task<IRestResponse> AddCurrencyAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementEntry);
        Task<IRestResponse> GetDisbursementTypeAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementEntry);
        Task<IRestResponse> AddWorkDateAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementModify);
        Task<IRestResponse> AddWorkQuantityAsync(string sessionId, string processItemId, ApiDisbursementModifyEntity disbursementModify);
    }
}
