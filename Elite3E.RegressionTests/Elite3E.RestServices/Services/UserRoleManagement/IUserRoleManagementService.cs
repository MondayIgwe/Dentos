using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.UserRoleManagement
{
    public interface IUserRoleManagementService
    {
        Task<IRestResponse> AddUserAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity);
        Task<IRestResponse> GetDataRoleAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity);
        Task<IRestResponse> AddUserRolesAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity);
        Task<IRestResponse> GetDashboardAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity);
        Task<IRestResponse> GetUserRoleValueAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity,string userRoleID,string userRole);
        Task<IRestResponse> GetUserRoleAdvancedSearchList(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity, string userRoleID, string userRole);

    }
}
