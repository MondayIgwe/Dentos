using Elite3E.SoapServices.Models.Vendor.ResponseModel;

namespace Elite3E.SoapServices.Services.Requests.Interface
{
    public interface IClientRequest
    {
        Task<TransactionServiceFT.ExecuteProcessResponse> CreateClientAsync(string xmlString);
        
    }
}
