using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.GjCategorySetup
{
    public interface IGjCategorySetupService
    {
        Task<IRestResponse> AddGJCategoryAsync(string sessionId, string processItemId, GJCategoryEntity gjCategoryEntity);
    }
}
