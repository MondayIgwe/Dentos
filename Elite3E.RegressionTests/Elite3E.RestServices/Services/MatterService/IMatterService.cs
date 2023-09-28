using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.MatterService
{
    public interface IMatterService
    {
        Task<IRestResponse> AddMatterAsync(string processItemId, string sessionId, ApiMatterEntity matter);
        Task<IRestResponse> GetEffectiveDatedRowInformationAsync(string processItemId, string sessionId, ApiMatterEntity matter);
        Task<IRestResponse> GetMatterRateAsync(string processItemId, string sessionId, ApiMatterEntity matter);
        Task<IRestResponse> AddEffectiveDatedInformationAsync(string processItemId, string sessionId, string rowKey, ApiMatterEntity matter);
        Task<IRestResponse> AddEffectiveDatedInformationLiteAsync(string processItemId, string sessionId, string rowKey, ApiMatterEntity matter);
        Task<IRestResponse> AddMatterRateAsync(string processItemId, string sessionId, string rowKey, ApiMatterEntity matter);
        Task<IRestResponse> GetNewBillingSiteAsync(string processItemId, string sessionId, ApiMatterEntity matter);
        Task<IRestResponse> AddNewBillingSiteAsync(string processItemId, string sessionId, ApiMatterEntity matter);
        Task<IRestResponse> PostBillingSiteDataAsync(string processItemId, string sessionId, ApiMatterEntity matter);
        Task<IRestResponse> GetBillingGroupFormAsync(string processItemId, string sessionId, ApiMatterEntity matter);
        Task<IRestResponse> SelectCostTypeGroupAsync(string processItemId, string sessionId, ApiMatterEntity matter);
        Task<IRestResponse> SelectChargeTypeGroupAsync(string processItemId, string sessionId, ApiMatterEntity matter);
        Task<IRestResponse> SelectBillingGroupAsync(string processItemId, string sessionId, string rowId, ApiMatterEntity matter);
        Task<IRestResponse> GetMatterClientAsync(string sessionId, string processItemId, ApiMatterEntity matter);
        Task<IRestResponse> GetMatterRateAsync(string sessionId, string processItemId, string matterRateRowId, ApiMatterEntity matter);
        Task<IRestResponse> GetMatterPayeeAsync(string sessionId, string processItemId, string payeeRowId, string addPayeeDetailRowId, ApiMatterEntity matter);
        Task<IRestResponse> GetMatterChargeTypeGroupAsync(string sessionId, string processItemId, ApiMatterEntity matter);
        Task<IRestResponse> GetMatterCostTypeGroupAsync(string sessionId, string processItemId, ApiMatterEntity matter);
        Task<IRestResponse> GetMatterBillingGroupAsync(string sessionId, string processItemId, string billingGroupRowId, ApiMatterEntity matter);
        Task<IRestResponse> GetMatterPayerFormAsync(string processItemId, string sessionId, ApiMatterEntity matterDetails);
        Task<IRestResponse> GetMatterPayerDetailsFormAsync(string processItemId, string sessionId, ApiMatterEntity matter, string rowId);
        Task<IRestResponse> AddMatterPayorAsync(string processItemId, string sessionId, string rowId, string payorRowId, ApiMatterEntity matter);
    }
}
