using Elite3E.SoapServices.Services.Builders;
using Elite3E.SoapServices.Services.Requests.Interface;

namespace Elite3E.SoapServices.Services.Requests
{
    public class EntityPersonRequest: IEntityPersonRequest
    {
        public async Task<TransactionServiceFT.ExecuteProcessResponse> CreateEntityPersonAsync()
        {
            var client = new TransactionServiceFT.TransactionServiceClient();

            var xmlString = PersonEntityBuilder.PersonEntityXmlString();

            return await client.ExecuteProcessAsync(xmlString, 1);

        }
    }
}