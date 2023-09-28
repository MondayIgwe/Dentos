using Elite3E.RestServices.Entity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Services.ClientAccountReceipt
{
    public interface IClientAccountReceiptService
    {
        Task<IRestResponse> GetClientAccountAcct(string sessionId, string processItemId, ApiClientAccountReceiptEntity clientAccountReceipt);
        Task<IRestResponse> AddClientAccountReceiptDataAsync(string sessionId, string processItemId, ApiClientAccountReceiptEntity clientAccountReceipt);
        Task<IRestResponse> GetClientAccountReceiptRowKeyDetail(string sessionId, string processItemId, ApiClientAccountReceiptEntity clientAccountReceipt);
        Task<IRestResponse> GetClientAccountMatter(string sessionId, string processItemId,string rowKey, ApiClientAccountReceiptEntity clientAccountReceipt);
        Task<IRestResponse> AddClientAccountReceiptDetailDataAsync(string sessionId, string processItemId,string rowkey, ApiClientAccountReceiptEntity clientAccountReceipt);

    }
}
