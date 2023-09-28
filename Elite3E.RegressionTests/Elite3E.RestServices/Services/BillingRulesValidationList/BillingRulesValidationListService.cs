using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.BillingRulesValidationList
{
    public class BillingRulesValidationListService : IBillingRulesValidationListService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddBillingRulesBillErrorAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                   new()
                    {
                        Op = "replace",
                        Path =  "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/IsBillError/value",
                        Value = rule.IsBillError
                    }
                }

            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingRulesBillWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                   new()
                    {
                        Op = "replace",
                        Path =  "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/IsBillWarning/value",
                        Value = rule.IsBillWarning
                    }
                }

            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingRulesDataAsync(string processItemId, string sessionId, ApiBillingRulesValidationListEntity billingRules)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/EBillValList/rows/" + billingRules.Id + "/attributes/Code/value",
                        Value = billingRules.Code
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/EBillValList/rows/" + billingRules.Id + "/attributes/Description/value",
                        Value = billingRules.Description
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingRulesPendingErrorAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                   new()
                    {
                        Op = "replace",
                        Path =  "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/IsPendingError/value",
                        Value = rule.IsPendingError
                    }
                }

            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingRulesPendingWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                   new()
                    {
                        Op = "replace",
                        Path =  "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/IsPendingWarning/value",
                        Value = rule.IsPendingWarning
                    }
                }

            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingRulesPostErrorAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                   new()
                    {
                        Op = "replace",
                        Path =  "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/IsPostError/value",
                        Value = rule.IsPostError
                    }
                }

            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingRulesPostWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                   new()
                    {
                        Op = "replace",
                        Path =  "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/IsPostWarning/value",
                        Value = rule.IsPostWarning
                    }
                }

            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingRulesProformaEditWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                   new()
                    {
                        Op = "replace",
                        Path =  "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/IsProformaEdit/value",
                        Value = rule.IsProformaEdit
                    }
                }

            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingRulesProformaGenrationWarningAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                   new()
                    {
                        Op = "replace",
                        Path =  "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/IsProformaGen/value",
                        Value = rule.IsProformaGen
                    }
                }

            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddBillingRulesValidationListRulesAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/EBillRule/value",
                        Value = rule.BillRuleCode,
                        Alias = rule.BillRuleDescrption,
                        Id = "EBillRule"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetBillingRulesForBillingListAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules, RulesList rule)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "EBillRule",
                ActionPath = "/objects/EBillValList/rows/" + billingRules.Id + "/childObjects/EBillValListRules/rows/"+ rule.RowId +"/attributes/EBillRule/aliasValue",
                Text = rule.BillRuleDescrption,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetBillingRulesValidationListRulesAsync(string sessionId, string processItemId, ApiBillingRulesValidationListEntity billingRules)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/EBillValList/rows/" +billingRules.Id + "/childObjects/EBillValListRules/rows/-",
                        Value = new ValueClass
                        {
                            SubclassId = "EBillValListRules"
                        }
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
