namespace Elite3E.Infrastructure.Entity
{
    public class PersonEntity : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Format { get; set; }
        public string DoB { get; set; }
   
    }

    public class OrganisationEntity : Entity
    {
        public string OrganisationName { get; set; }
        public string Language { get; set; }

    }

    public class Entity
    {
        public string EntityType { get; set; }
        public string SiteType { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string EmailDescription { get; set; }
        public bool? IsEmailDefault { get; set; } = null;
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

}