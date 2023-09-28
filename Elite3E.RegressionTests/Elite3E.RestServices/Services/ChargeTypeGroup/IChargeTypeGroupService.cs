using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.ChargeTypeGroup
{
    public interface IChargeTypeGroupService
    {
        Task<IRestResponse> AddChargeTypeGroupAsync(string sessionId, string processItemId, ApiChargeTypeGroupEntity chargeTypeGroupEntity);
        Task<IRestResponse> SelectChargeTypeDetail(string sessionId, string processItemId, string chargeTypeRowId, ApiChargeTypeGroupEntity chargeTypeGroupEntity);
        Task<IRestResponse> AddChargeTypeDetail(string sessionId, string processItemId, ApiChargeTypeGroupEntity chargeTypeGroupEntity);
        Task<IRestResponse> SelectChargeTypeGroup(string sessionId, string processItemId, ApiChargeTypeGroupEntity chargeTypeGroupEntity);
        Task<IRestResponse> GetChargeTypeValue(string sessionId, string processItemId, ApiChargeTypeGroupEntity chargeTypeGroupEntity, string chargeTypeRowId, string descriptionString);
    }
}
