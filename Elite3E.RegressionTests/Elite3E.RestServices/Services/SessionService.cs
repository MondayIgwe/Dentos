using Elite3E.RestServices.Builders;
using RestSharp;

namespace Elite3E.RestServices.Services
{
    public class SessionService : ISessionService
    {

        public async Task<IRestResponse> GetSessionResponseAsync()
        {
            return  await new RestClientBuilder()
                .Create()
                .ForResource("session", Method.POST)
                .WithFormData()
                .ExecuteRequestAsync();
        }
    }
}
