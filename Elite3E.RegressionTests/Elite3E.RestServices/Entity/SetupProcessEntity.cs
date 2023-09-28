using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class SetupProcessEntity
    {
        public string SetupProcessId { get; set; }  
        public ValueUnion ProcessValue { get; set; }
        public string ProcessAlias { get; set; }
    }
}
