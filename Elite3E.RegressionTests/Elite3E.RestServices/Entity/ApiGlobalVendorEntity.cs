using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiGlobalVendorEntity
    {
        public string GlobalVendorId { get; set; }
        public ValueUnion Description { get; set; }
        public ValueUnion Code { get; set; }
    }
}
