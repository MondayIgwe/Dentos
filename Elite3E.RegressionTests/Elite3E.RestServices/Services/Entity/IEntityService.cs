using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.Entity
{
    public interface IEntityService
    {
        Task<IRestResponse> AddEntityPersonDataAsync(string processItemId, string sessionId, ApiEntity entity);
        Task<IRestResponse> AddEntityRelationshipAsync(string processItemId, string sessionId, ApiEntity entity);
        Task<IRestResponse> GetEntityRelationshipIdRowIdAsync(string processItemId, string sessionId , ApiEntity entity);
        Task<IRestResponse> GetEntitySiteRowIdAsync(string processItemId, string sessionId, string rowId, ApiEntity entity);
        Task<IRestResponse> AddEntitySiteAsync(string processItemId, string sessionId, string rowId, string siteRowId, ApiEntity entity);
        Task<IRestResponse> AddEntityOrganisationNameAsync(string processItemId, string sessionId, ApiEntity entity);
        Task<IRestResponse> AddEntityOrganisationTypeAsync(string processItemId, string sessionId, ApiEntity entity);
        Task<IRestResponse> AddEntityAddressCityAsync(string processItemId, string sessionId, string rowId, string siteRowId, ApiEntity entity);
        Task<IRestResponse> AddEntityAddressPostCodeAsync(string processItemId, string sessionId, string rowId, string siteRowId, ApiEntity entity);
        Task<IRestResponse> AddEntityAddressOrganisationAsync(string processItemId, string sessionId, string rowId, string siteRowId, ApiEntity entity);
    }
}