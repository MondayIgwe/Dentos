using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.FiscalInvoiceSetup
{
    public interface IFiscalInvoiceSetupService
    {
        Task<IRestResponse> AddFiscalInvoiceSetupAsync(string sessionId, string processItemId, FiscalInvoiceSetupEntity fiscalInvoiceSetupEntity);
        Task<IRestResponse> GetFiscalInvoiceSearchGLTypeAsync(string sessionId, string processItemId, FiscalInvoiceSetupEntity fiscalInvoiceSetupEntity);
        Task<IRestResponse> GetFiscalInvoiceSuspenseGLTypeAsync(string sessionId, string processItemId, FiscalInvoiceSetupEntity fiscalInvoiceSetupEntity);
    }
}
