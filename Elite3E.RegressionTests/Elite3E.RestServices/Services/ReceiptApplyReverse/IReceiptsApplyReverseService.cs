using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using RestSharp;


namespace Elite3E.RestServices.Services.ReceiptApplyReverse
{
    public interface IReceiptsApplyReverseService
    {
        Task<IRestResponse> GetReceiptTypeAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity);
        Task<IRestResponse> AddReceiptsReceiptTypeDataAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity);
        Task<IRestResponse> AddReceiptsApplyReverseDataAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity);
        Task<IRestResponse> AddReceiptsReceiptAmountDataAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity);
        Task<IRestResponse> AddChildFormAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity);
        Task<IRestResponse> GetInvoiceKeyAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity);
        Task<IRestResponse> AddInvoiceAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity);
        Task<IRestResponse> PostUpdateReceiptAsync(string sessionId, string processItemId);

    }
}
