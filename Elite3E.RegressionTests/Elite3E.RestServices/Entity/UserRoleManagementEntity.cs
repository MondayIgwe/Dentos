using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class UserRoleManagementEntity
    {
        
        public string UserName { get; set; }
        public string UserRoleManagementId { get; set; }  
        public ValueUnion DataRoleValue { get; set; }
        public string DataRoleAlias { get; set; }
        public ValueUnion DefaultOperatingUnitValue { get; set; }
        public string DefaultOperatingAlias { get; set; }
        public List<UserRole> UserRole { get; set; }
        public string ProcessItemId { get; set; }
        public ValueUnion NetworkAlias { get; set; }
        public ValueUnion EmailAddress { get; set; }
        public ValueUnion CanProxy { get; set; }
        public ValueUnion CanEditDashboard { get; set; }
        public ValueUnion IsAllowTimeEntry { get; set; }
        public ValueUnion CanEditGlobalModel { get; set; }
        public string DashboardAlias { get; set; }
        public ValueUnion DashboardValue { get; set; }
        public string LanguageAlias { get; set; }
        public ValueUnion LanguageValue { get; set; }

    }

    public class UserRole
    {
        public string UserRoleManagementChildId { get; set; }
        public string UserRoleAlias { get; set; }
        public ValueUnion UserRoleValue { get; set; }
    }
}
