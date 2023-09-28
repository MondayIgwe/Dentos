namespace Elite3E.RestServices.Entity
{
    public class GJCategoryEntity
    {
        public string GJCategorySetupId { get; set; }
        public string GJCategoryCode { get; set; }
        public string GJCategoryDescription { get; set; }
        public string IsRequireApprovalCheckboxAlias { get; set; } // Yes or No
        public string IsRequireApprovalCheckboxValue { get; set; } // Resolved to 1 or 0
    }
}
