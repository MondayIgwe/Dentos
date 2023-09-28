using System.ComponentModel.DataAnnotations;
using Elite3E.SoapServices.Services.Builders;
using Elite3E.SoapServices.Services.Requests.Interface;

namespace Elite3E.SoapServices.Services.Requests
{
    public class MatterRequest:IMatterRequest
    {
        public async Task<TransactionServiceFT.ExecuteProcessResponse> CreateMatterAsync()
        {
            var client = new TransactionServiceFT.TransactionServiceClient();

            var xmlString = MatterBuilder.MatterXmlString();

            return await client.ExecuteProcessAsync(xmlString, 1);
        }

        public async Task<TransactionServiceFT.ExecuteProcessResponse> CreateMatterAsync(string xmlString)
        {
            var client = new TransactionServiceFT.TransactionServiceClient();
            return await client.ExecuteProcessAsync(xmlString, 1);
        }

    }
}
