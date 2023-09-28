
using Elite3E.SoapServices.Services.Requests.Interface;

namespace Elite3E.SoapServices.Services.Requests
{
    public class PayorRequest: IPayorRequest
    {

        public async Task<TransactionServiceFT.ExecuteProcessResponse> CreatePayorAsync(string xmlString)
        {
            var client = new TransactionServiceFT.TransactionServiceClient();
            return await client.ExecuteProcessAsync(xmlString, 1);
        }
    }
}
