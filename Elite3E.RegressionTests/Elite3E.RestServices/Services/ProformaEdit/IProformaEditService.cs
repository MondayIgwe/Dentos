using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using RestSharp;


namespace Elite3E.RestServices.Services.ProformaEdit
{
    public interface IProformaEditService
    {
        public Task<IRestResponse> AddSearchResultToWorklistAsync(string sessionId, string processItemId, string BatchAddRowKey);
        public Task<IRestResponse> BillNoPrintAsync(string sessionId, string processItemId);
        public Task<IRestResponse> GetPossibleErrorsAsync(string sessionId, string processItemId);
    }
}
