
using Elite3E.SoapServices.Services.Requests.Interface;

namespace Elite3E.SoapServices.Services.Requests
{
    public class ClientRequest : IClientRequest
    {

        public async Task<TransactionServiceFT.ExecuteProcessResponse> CreateClientAsync(string xmlString)
        {
            var client = new TransactionServiceFT.TransactionServiceClient();
            return await client.ExecuteProcessAsync(xmlString, 1);
        }
    }
}
