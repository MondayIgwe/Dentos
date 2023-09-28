using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiEntity
    {
        public string EntityId { get; set; }
        public ValueUnion EntityType { get; set; }
        public ValueUnion FirstName { get; set; }
        public ValueUnion FormatCode { get; set; }
        public ValueUnion LastName { get; set; }
        public string PersonType { get; set; }
        public ValueUnion SiteDescription { get; set; }
        public ValueUnion SiteType { get; set; }
        public ValueUnion CountryCode { get; set; }
        public ValueUnion Street { get; set; }
        public ValueUnion AddressOrganisation { get; set; }
        public ValueUnion Address { get; set; }
        public ValueUnion City { get; set; }
        public ValueUnion PostCode { get; set; }
        public string FormattedName { get; set; }
        public string Country { get; set; }
        public ValueUnion OrganisationName { get; set; }
        public string LanguageKey { get;  set; }
        public ValueUnion LanguagValue { get; set; }

        public string EntityName { get; set; }

    }
}
