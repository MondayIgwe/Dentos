using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.ClientGroupType
{
    public interface IClientGroupService
    {
        Task<IRestResponse> AddClientTypeGroupDataAsync(string sessionId, string processItemId, ApiClientGroupTypeEntity clientGroup);
    }
}
