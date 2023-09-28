using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.TimeType
{
    public interface ITimeTypeService
    {
        Task<IRestResponse> GetTransactionTypeDeatilsAsync(string sessionId, string processItemId, ApiTimeTypeEntity timeType);
        Task<IRestResponse> AddTimeTypeDeatilsAsync(string sessionId, string processItemId, ApiTimeTypeEntity timeType);
    }
}
