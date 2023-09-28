using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public  class ApiClientGroupTypeEntity
    {
        public ValueUnion GroupCode { get; set; }
        public ValueUnion GroupDescription { get; set; }
        public string Id { get; set; }
    }
}
