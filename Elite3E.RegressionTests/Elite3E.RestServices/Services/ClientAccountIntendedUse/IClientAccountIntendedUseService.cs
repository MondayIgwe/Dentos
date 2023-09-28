using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.ClientAccountIntendedUse
{
    public interface IClientAccountIntendedUseService
    {
        Task<IRestResponse> AddCLientAccountDataAsync(string sessionId, string processItemId, ApiClientIntendedUseEntity client);
    }
}
