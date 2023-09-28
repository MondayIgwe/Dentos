using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.TimeModify
{
    public interface ITimeModifyService
    {
        Task<IRestResponse> GetFeeEranerDeatilsAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
        Task<IRestResponse> AddFeeEranerAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
        Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
        Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeEtimeModifyntry);
        Task<IRestResponse> AddTimeTypeAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
        Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
        Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
        Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
        Task<IRestResponse> AddWorkingHoursAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
        Task<IRestResponse> AddWorkDateAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
        Task<IRestResponse> AddCurrencyAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify);
    }
}
