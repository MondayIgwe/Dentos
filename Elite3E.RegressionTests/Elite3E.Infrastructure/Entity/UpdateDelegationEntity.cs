using System.Collections.Generic;

namespace Elite3E.Infrastructure.Entity
{
    public class UpdateDelegationEntity
    {
        public string DelegationUserWithRoles { get; set; }
        public string DelegationUserWithoutRoles { get; set; }
        public string EffectiveDate { get; set; }
        public bool DelegateAllWorkflowsCheckbox { get; set; }
        public List<string> WorkflowsToGrantAccessTo { get; set; }
        public List<string> WorkflowsToRevokeAccessTo { get; set; }

    }
}
