using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.GlobalVendor
{
    public interface IGlobalVendorService
    {
        Task<IRestResponse> AddGlobalVendorAsync(string sessionId, string processItemId, ApiGlobalVendorEntity globalVendorEntity);
    }
}
