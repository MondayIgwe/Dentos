using Elite3E.SoapServices.Models.Vendor.ResponseModel;

namespace Elite3E.SoapServices.Services.Requests.Interface
{
    public interface IVendorRequest
    {
        Task<TransactionServiceFT.ExecuteProcessResponse> CreateVendorAsync();

        ProcessExecutionResults GetProcessExecutionResults(string xmlString);
    }
}
