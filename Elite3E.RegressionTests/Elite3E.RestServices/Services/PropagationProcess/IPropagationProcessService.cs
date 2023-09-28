using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.PropagationProcess
{
    public interface IPropagationProcessService
    {
        Task<IRestResponse> AddPropagationProcessAsync(string processItemId, string sessionId, SetupProcessEntity setupProcessEntity);
        Task<IRestResponse> GetPropagationProcess (string sessionId, string processItemId, SetupProcessEntity setupProcessEntity); 
    }
}
