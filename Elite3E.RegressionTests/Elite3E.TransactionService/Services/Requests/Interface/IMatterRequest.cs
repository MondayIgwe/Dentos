
namespace Elite3E.SoapServices.Services.Requests.Interface
{
    public interface IMatterRequest
    {
        Task<TransactionServiceFT.ExecuteProcessResponse> CreateMatterAsync();
        Task<TransactionServiceFT.ExecuteProcessResponse> CreateMatterAsync(string xmlString);
    }
}
