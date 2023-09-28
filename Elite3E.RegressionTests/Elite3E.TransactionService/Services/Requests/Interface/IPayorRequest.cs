namespace Elite3E.SoapServices.Services.Requests.Interface
{
    public interface IPayorRequest
    {
        Task<TransactionServiceFT.ExecuteProcessResponse> CreatePayorAsync(string xmlString);
    }
}
