using RestSharp;

namespace Elite3E.RestServices.Services
{
    public interface ILookUpService
    {
        Task<IRestResponse> GetLookUpListAsync(string sessionId, string lookUpName);
        Task<IRestResponse> GetLookUpAsync(string sessionId, string processItemId, string body);
        Task<IRestResponse> GetAdvancedLookUpAsync(string sessionId, string processItemId, string body);
        Task<IRestResponse> GetAdvancedWorkListBarristerFlagAsync(string sessionId, string processItemId, string searchText = null);
        Task<IRestResponse> GetWorkListAsync(string sessionId, string processItemId, string searchText = null);
        Task<IRestResponse> GetChildDropDownLookUpListAsync(string sessionId, string processItemId, string lookUpName, string parameter);
    }
}
