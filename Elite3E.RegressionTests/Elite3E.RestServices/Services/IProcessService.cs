using RestSharp;

namespace Elite3E.RestServices.Services
{
    public interface IProcessService
    {
        Task<IRestResponse> GetProcessItemIdAsync(string sessionId, string processName);
        Task<IRestResponse> AddNewProcessAsync(string sessionId, string processId, string processName);
        Task<IRestResponse> AddNewProcessAsync(string sessionId, string processId, string process, string processName);
        Task<IRestResponse> PostReleaseProcessAsync(string sessionId, string processItemId, string processName);
        Task<IRestResponse> PostSubmitProcessAsync(string sessionId, string processItemId, string processName);
        Task<IRestResponse> PostAllProcessAsync(string sessionId, string processItemId, string processName);
        Task<IRestResponse> PostCancelProcessAsync(string sessionId, string processItemId);
        Task<IRestResponse> AddNewSubProcessAsync(string sessionId, string processId,string instanceId, string process, string subProcessName);
        Task<IRestResponse> GetPresentationItemIdAsync(string sessionId, string nxOpenProcesses);
        Task<IRestResponse> GetPageItemIdAsync(string sessionId, string NxOpenProcessPO);
    }
}
