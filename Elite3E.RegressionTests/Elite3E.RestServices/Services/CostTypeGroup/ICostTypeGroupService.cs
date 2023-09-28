using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.CostTypeGroup
{
    public interface ICostTypeGroupService
    {
        Task<IRestResponse> AddCostTypeGroupDataAsync(string sessionId, string processItemId, ApiCostTypeGroupEntity costTypeGroup);
        Task<IRestResponse> AddCostTypeDetail(string sessionId, string processItemId, ApiCostTypeGroupEntity costTypeGroupEntity);
        Task<IRestResponse> SelectCostTypeDetail(string sessionId, string processItemId, string chargeTypeRowId, ApiCostTypeGroupEntity costTypeGroupEntity);
        Task<IRestResponse> SelectCostTypeGroup(string sessionId, string processItemId, ApiCostTypeGroupEntity costTypeGroupEntity);
        Task<IRestResponse> GetDisbursementTypeValue(string sessionId, string processItemId, ApiCostTypeGroupEntity costTypeGroupEntity, string costTypeRowId, string disbursementType);
    }
}
