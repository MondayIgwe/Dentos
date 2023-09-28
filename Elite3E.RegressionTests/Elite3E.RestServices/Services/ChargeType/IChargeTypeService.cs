using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.ChargeType
{
    public interface IChargeTypeService
    {
        Task<IRestResponse> GetLookupSearchTransactionType(string sessionId, string processItemId, ApiChargeTypeEntity chargeTypeEntity);
        Task<IRestResponse> AddChargeTypeAsync(string sessionId, string processItemId, ApiChargeTypeEntity chargeTypeEntity);
        Task<IRestResponse> GetQueryInfoResponse(string sessionId, string processItemId, ApiChargeTypeEntity chargeTypeEntity);
    }
}
