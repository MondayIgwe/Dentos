using RestSharp;

namespace Elite3E.RestServices.Services
{
    public interface ISessionService
    {
        Task<IRestResponse> GetSessionResponseAsync();
    }
}
