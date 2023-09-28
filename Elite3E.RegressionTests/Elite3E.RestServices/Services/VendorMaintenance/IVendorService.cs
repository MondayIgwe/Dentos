using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.VendorMaintenance
{
    public interface IVendorService
    {
        Task<IRestResponse> AddVendorDataAsync(string sessionId, string processItemId, ApiVendorEntity vendor);
        Task<IRestResponse> GetVendorEntitySearchList(string sessionId, string processItemId, ApiVendorEntity vendor);

    }
}
