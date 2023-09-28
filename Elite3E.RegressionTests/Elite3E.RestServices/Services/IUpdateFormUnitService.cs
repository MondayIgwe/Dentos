using RestSharp;

namespace Elite3E.RestServices.Services
{
    public interface IUpdateFormUnitService
    {
        public Task<IRestResponse> GetUnitsAnync(string sessionId, string processItemId);
        public Task<IRestResponse> PatchNewUnitAsync(string sessionId, string processItemId, string unitToUpdate);

    }
}
