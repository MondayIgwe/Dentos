using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiRateMaintenanceEntity
    {
        public string RateId { get; set; }
        public ValueUnion Code { get;  set; }
        public ValueUnion Description { get;  set; }
        public string RateTypeDescription { get;  set; }
        public ValueUnion RateTypeValue { get;  set; }
    }
}
