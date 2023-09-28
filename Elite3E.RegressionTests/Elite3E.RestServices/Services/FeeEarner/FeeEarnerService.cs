using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.FeeEarner
{
    public class FeeEarnerService : IFeeEarnerService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddFeeEarnerDataAsync(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+"/attributes/Entity/value",
                        Value = feeEarner.Entity,
                        Alias = feeEarner.EntityName,
                        Id = "Entity"
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEffectiveDatedInformationAsync(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Timekeeper/childStates/TkprDate/sortAttributes",
                        Value = new ValueClass
                        {
                           TimekeeperLookUpNumber = new TimekeeperLookUp()
                            {
                                Id = "TimekeeperLkUp1.Number",
                                Direction = 1
                            },
                            EffStart = new EffStart()
                            {
                                Id = "EffStart",
                                Direction = -1,
                                Order = 1
                            }
                        }
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Timekeeper/childStates/TkprDate/clearUISort",
                        Value = true
                    }
                },
                Index = "0",
                Path = "/objects/Timekeeper/rows/" + feeEarner.FeeEarnerId + "/childObjects/TkprDate/rows"

            }, JsonHelper.Settings);

            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> UpdateEffectiveDateInformationAsync(string sessionId, string processItemId, string rowId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+"/childObjects/TkprDate/rows/"+rowId+"/attributes/Office/value",
                        Value = feeEarner.OfficeKey
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+"/childObjects/TkprDate/rows/"+rowId+"/attributes/Department/value",
                        Value = feeEarner.DepartmentKey
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+"/childObjects/TkprDate/rows/"+rowId+"/attributes/Section/value",
                        Value = feeEarner.SectionKey
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+"/childObjects/TkprDate/rows/"+rowId+"/attributes/Title/value",
                        Value = feeEarner.TitleKey
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+"/childObjects/TkprDate/rows/"+rowId+"/attributes/RateClass/value",
                        Value = feeEarner.RateClassKey
                    },
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddFeeEarnerRatesAsync(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner)
        {
            var urlExtension = "data/action/AddByQuery";

            var body = JsonConvert.SerializeObject(new AddQueryModel()
            {
                Path = "/objects/Timekeeper/rows/" + feeEarner.FeeEarnerId + "/childObjects/TkprRate/actions/AddByQuery",
                SelectedRows = new List<Guid>(feeEarner.RateTypeKey)

            }, JsonHelper.Settings); ;
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> GetFeeEarnerEntitySearchList(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "EntityPerson",
                Path = "/objects/Timekeeper/rows/" + feeEarner.FeeEarnerId + "/attributes/Entity/aliasValue",
                Text = feeEarner.EntityName,
                Toprows = 100, 
                ProcessItemId = processItemId,
                AddIdAttribute =  false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetFeeEarnerRateTypeSearchList(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "RateType",
                ActionPath = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+ "/childObjects/TkprRate/actions/AddByQuery",
                Text = feeEarner.RateTypeDescription,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false}, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetWorkflowUserQuickResponse(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "NxBaseUser",
                ActionPath = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+ "/attributes/WF_User/aliasValue",
                Text = feeEarner.WorkflowUserAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false}, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetFeeEarnerRateTypeAdvancedSearchList(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "RateType",
                ActionPath = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+ "/childObjects/TkprRate/actions/AddByQuery",
                Select = new Select()
                {
                    Where = new Where()
                    {
                        Operator = "Or",
                        Predicates = new List<Predicate> 
                        {
                            new Predicate()
                            {
                                Attribute = "IsStandard",
                                Operator = "IsEqualTo",
                                Value = "True"
                            },
                            new Predicate()
                            {
                                Attribute = "IsFirmDefault",
                                Operator = "IsEqualTo",
                                Value = "True"
                            }
                        }
                    },
                    Archetype = "RateType",
                    ArchetypeType = 1
                },
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false}, JsonHelper.Settings);

            return await LookUpService.GetAdvancedLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetEffectiveDatedRatesInformationAsync(string sessionId, string processItemId, string childRowId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+"/childObjects/TkprRate/rows/"+childRowId+"/childObjects/TkprRateDate/rows/-",
                        Value = new ValueClass()
                        {
                            SubclassId = "TkprRateDate"
                        }
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEffectiveDatedRatesAsync(string sessionId, string processItemId, string rowId, string childRowId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/" + feeEarner.FeeEarnerId + "/childObjects/TkprRate/rows/"+ rowId +"/childObjects/TkprRateDate/rows/"+childRowId+"/attributes/DefaultRate/value",
                        Value = feeEarner.EffectiveRate
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/" + feeEarner.FeeEarnerId + "/childObjects/TkprRate/rows/"+ rowId +"/childObjects/TkprRateDate/rows/"+childRowId+"/attributes/Currency/value",
                        Value = feeEarner.EffectiveRateCurrency
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);

        }

        public async Task<IRestResponse> AddEDIStartDateAsync(string sessionId, string processItemId, string rowId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/"+feeEarner.FeeEarnerId+"/childObjects/TkprDate/rows/"+rowId+"/attributes/EffStart/value",
                        Value = feeEarner.EDIStartDate
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddFeeEarnerRateStartDateAsync(string sessionId, string processItemId, string rowId, string childRowId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/" + feeEarner.FeeEarnerId + "/childObjects/TkprRate/rows/"+ rowId +"/childObjects/TkprRateDate/rows/" + childRowId + "/attributes/EffStart/value",
                        Value = feeEarner.FeeEarnerRatesStartDate
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddWorkflowUserDataAsync(string sessionId, string processItemId, ApiFeeEarnerEntity feeEarner)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Timekeeper/rows/" + feeEarner.FeeEarnerId + "/attributes/WF_User/value",
                        Value = feeEarner.WorkflowUserValue,
                        Alias = feeEarner.WorkflowUserAlias,
                        Id = "WF_User"
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
