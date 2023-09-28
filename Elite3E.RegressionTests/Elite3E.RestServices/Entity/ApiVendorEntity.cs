using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiVendorEntity
    {
        public string VendorId { get; set; }
        public ValueUnion Entity { get; set; }
        public string EntityName { get; set; }
        public string GlobalVendorKey { get; set; }
        public string GlobalVendor { get; set; }

    }
}
