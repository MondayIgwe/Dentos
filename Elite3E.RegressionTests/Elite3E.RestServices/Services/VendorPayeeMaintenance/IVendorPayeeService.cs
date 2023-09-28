using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.VendorPayeeMaintenance
{
    public interface IVendorPayeeService
    {
        Task<IRestResponse> AddVendorPayeeDataAsync(string sessionId, string processItemId, ApiVendorEntity vendor);
        Task<IRestResponse> GetVendorPayeeEntitySearchList(string sessionId, string processItemId, ApiVendorEntity vendor);

    }
}
