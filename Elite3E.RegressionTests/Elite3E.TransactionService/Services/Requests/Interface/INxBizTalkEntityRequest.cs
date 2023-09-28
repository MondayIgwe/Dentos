namespace Elite3E.SoapServices.Services.Requests.Interface
{
    public interface INxBizTalkEntityRequest
    {
        Task<TransactionServiceFT.ExecuteProcessResponse> CreateNxBizTalkEntityAsync(string xmlString);
    }
}
