using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.BillingGroup
{
   public interface IBillingGroupService
    {
        Task<IRestResponse> AddBillingGroupAsync(string sessionId, string processItemId, ApiBillingGroupEntity billingGroupEntity);
        Task<IRestResponse> GetBillingGroupMatterAsync(string sessionId, string processItemId, ApiBillingGroupEntity billingGroupEntity);
        Task<IRestResponse> AddBillingGroupMatterAsync(string sessionId, string processItemId, ApiBillingGroupEntity billingGroupEntity);
    }
}
