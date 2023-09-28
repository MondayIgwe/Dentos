using Elite3E.SoapServices.Services.Requests.Interface;

namespace Elite3E.SoapServices.Services.Requests
{
    public class NxBizTalkEntityRequest : INxBizTalkEntityRequest
    {

        public async Task<TransactionServiceFT.ExecuteProcessResponse> CreateNxBizTalkEntityAsync(string xmlString)
        {
            var client = new TransactionServiceFT.TransactionServiceClient();
            return await client.ExecuteProcessAsync(xmlString, 1);
        }
    }
}
