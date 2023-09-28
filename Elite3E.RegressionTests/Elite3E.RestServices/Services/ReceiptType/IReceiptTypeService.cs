using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.ReceiptType
{
    public  interface IReceiptTypeService
    {
        Task<IRestResponse> CreateReceiptTypeAsync(string sessionId, string processItemId, ApiReceiptTypeEntity receiptType);
        Task<IRestResponse> GetReceiptTypeBankAccountAsync(string sessionId, string processItemId, ApiReceiptTypeEntity receiptType);
    }
}
