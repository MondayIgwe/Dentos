using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using RestSharp;

namespace Elite3E.RestServices.Services.Department
{
    public interface IDepartmentService
    {
        Task<IRestResponse> AddDepartmentCodeAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity);
        Task<IRestResponse> AddDescriptionAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity);
        Task<IRestResponse> AddGLDepartmentAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity);
        Task<IRestResponse> AddDepartmentGroupAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity);
        Task<IRestResponse> AddIsDefaultCheckboxAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity);
        Task<IRestResponse> AddIsActiveCheckboxAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity);
        Task<IRestResponse> AddStartDateAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity);
        Task<IRestResponse> AddEndDateAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity);
        Task<IRestResponse> GetGLDepartmentAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity);
    }
}
