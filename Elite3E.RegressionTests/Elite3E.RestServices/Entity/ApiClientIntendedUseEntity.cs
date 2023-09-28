using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiClientIntendedUseEntity
    {
        public ValueUnion Code { get;  set; }
        public ValueUnion Description { get;  set; }
        public string ClientAccountId { get;  set; }
    }
}
