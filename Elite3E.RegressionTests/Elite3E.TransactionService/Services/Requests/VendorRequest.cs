using Elite3E.SoapServices.Models.Vendor.ResponseModel;
using Elite3E.SoapServices.Services.Builders;
using Elite3E.SoapServices.Services.Requests.Interface;

namespace Elite3E.SoapServices.Services.Requests
{
    public class VendorRequest:IVendorRequest
    {
        public async Task<TransactionServiceFT.ExecuteProcessResponse> CreateVendorAsync()
        {
            var client = new TransactionServiceFT.TransactionServiceClient();

            var xmlString = VendorBuilder.VendorXMLString();

            return await client.ExecuteProcessAsync(xmlString, 1);
        }

        public ProcessExecutionResults GetProcessExecutionResults(string xmlString)
        {
            return XmlExtensions.Deserialise<ProcessExecutionResults>(xmlString);
        }
    }
}
