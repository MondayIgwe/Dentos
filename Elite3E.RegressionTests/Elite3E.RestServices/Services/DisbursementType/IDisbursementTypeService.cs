using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.DisbursementType
{
    public interface IDisbursementTypeService
    {
        Task<IRestResponse> AddDisbursementTypeDataDataAsync(string sessionId, string processItemId, ApiDisbursementTypeEntity disbursementType);
        Task<IRestResponse> GetLookupSearchTransactionType(string sessionId, string processItemId, ApiDisbursementTypeEntity disbursementType);
        Task<IRestResponse> AddBarristerFlag(string sessionId, string processItemId, ApiDisbursementTypeEntity disbursementType);
        Task<IRestResponse> GetDisbursementTypeAdvancedSearchList(string sessionId, string processItemId, ApiDisbursementTypeEntity disbursementType);
    }
}
