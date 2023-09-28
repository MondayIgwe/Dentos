using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiDepartmentEntity
    {
        public string Id { get; set; }
        public string DepartmentCode{ get;set; } //mandatory
        public string Description { get;set; } //mandatory
        public string GLDepartmentAlias { get;set; } //mandatory
        public ValueUnion GLDepartmentValue { get;set; }
        public string DepartmentGroupAlias { get;set; }
        public ValueUnion DepartmentGroupValue { get;set; }
        public string IsDefaultCheckBoxAlias { get;set; } //Optional: must be YES or NO
        public ValueUnion IsDefaultCheckBoxValue { get;set; }
        public string IsActiveCheckBoxAlias { get;set; } //Optional: must be YES or NO
        public ValueUnion IsActiveCheckBoxValue { get;set; }
        public string StartDate { get;set; }
        public string EndDate { get; set; }
    }
}
