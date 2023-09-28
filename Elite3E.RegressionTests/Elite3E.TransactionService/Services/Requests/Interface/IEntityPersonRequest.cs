namespace Elite3E.SoapServices.Services.Requests.Interface
{
    public interface IEntityPersonRequest
    {
        Task<TransactionServiceFT.ExecuteProcessResponse> CreateEntityPersonAsync();
    }
}
