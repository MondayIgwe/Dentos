using RestSharp;

namespace Elite3E.RestServices.Services
{
    public interface IProcessDataService
    {
        Task<IRestResponse> AddDataAsync(string sessionId, string processItemId, string requestBody);
        Task<IRestResponse> AddDataAsync(string sessionId, string processItemId, string requestBody, string urlExtension = null);
        Task<IRestResponse> UpdateDataAsync(string sessionId, string processItemId, string requestBody);
        Task<IRestResponse> PostActionDataAsync(string sessionId, string processItemId, string requestBody);
        Task<IRestResponse> GetRowKeyByProcessItemIdAsync(string sessionId, string processItemId);
        Task<IRestResponse> ReleaseFormAsync(string sessionId, string processItemId, string requestBody);
        Task<IRestResponse> SubmitFormAsync(string sessionId, string processItemId, string requestBody);
        Task<IRestResponse> PostAllFormAsync(string sessionId, string processItemId, string requestBody);
        Task<IRestResponse> PostOutPutProcessAsync(string sessionId, string processItemId, string requestBody, string urlExtension = null);
		Task<IRestResponse> GetDataAsync(string sessionId, string processItemId, string parameter);
        Task<IRestResponse> UpdateAsync(string sessionId, string requestBody);
        Task<IRestResponse> PostAsync(string sessionId, string urlExtension, string requestBody);
    }
}
