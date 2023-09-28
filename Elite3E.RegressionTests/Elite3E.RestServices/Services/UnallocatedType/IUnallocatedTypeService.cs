using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.UnallocatedType
{
    public interface IUnallocatedTypeService
    {
       Task<IRestResponse> CreateUnallocatedTypeAsync(string sessionId, string processItemId, ApiUnallocatedTypeEntity unallocatedType);
    }
}
