using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.BillingRulesValidationList
{
    public interface IBillingRulesValidationListService
    {
        Task<IRestResponse> AddBillingRulesDataAsync(string processItemId, string sessionId, ApiBillingRulesValidationListEntity billingRules);
        Task<IRestResponse> GetBillingRulesValidationListRulesAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billinRules);
        Task<IRestResponse> GetBillingRulesForBillingListAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
        Task<IRestResponse> AddBillingRulesValidationListRulesAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
        Task<IRestResponse> AddBillingRulesPendingWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
        Task<IRestResponse> AddBillingRulesPostWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
        Task<IRestResponse> AddBillingRulesBillErrorAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
        Task<IRestResponse> AddBillingRulesPostErrorAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
        Task<IRestResponse> AddBillingRulesPendingErrorAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
        Task<IRestResponse> AddBillingRulesBillWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
        Task<IRestResponse> AddBillingRulesProformaEditWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
        Task<IRestResponse> AddBillingRulesProformaGenrationWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule);
    }
}
