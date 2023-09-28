using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.MatterService
{
    public class MatterService : IMatterService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddMatterAsync(string processItemId, string sessionId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/attributes/Client/value",
                        Value = matter.ClientRowKey,
                        Alias = matter.ClientAlias,
                        Id = "Client"
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/attributes/OpenDate/value",
                        Value = matter.OpenDate
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/attributes/DisplayName/value",
                        Value = matter.MatterName
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/attributes/MattStatus/value",
                        Value = matter.StatusCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/attributes/Currency/value",
                        Value = matter.CurrencyCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/attributes/CurrencyDateList/value",
                        Value = matter.CurrencyMethodCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/attributes/BillingOffice/value",
                        Value = matter.BillingOfficeCode
                    }

                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);

        }

        public async Task<IRestResponse> GetEffectiveDatedRowInformationAsync(string processItemId, string sessionId, ApiMatterEntity matter)
        {
            var body = new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Matter/childStates/MattDate/sortAttributes",
                        Value = new ValueClass
                        {
                            EffStart = new EffStart()
                            {
                                Id = "EffStart",
                                Direction = -1
                            },
                            NxStartDate = new NxStartDate()
                            {
                                Id = "NxStartDate",
                                Direction = 1,
                                Order = 1
                            }
                        }
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Matter/childStates/MattDate/clearUISort",
                        Value = true
                    }
                },
                Index = "0",
                Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattDate/rows"

            };

            var requestBody = JsonConvert.SerializeObject(body, JsonHelper.Settings);

            return await ProcessDataService.AddDataAsync(sessionId, processItemId, requestBody);

        }

        public async Task<IRestResponse> GetMatterRateAsync(string processItemId, string sessionId, ApiMatterEntity matter)
        {
            var body = new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Matter/childStates/MattRate/sortAttributes",
                        Value = new ValueClass()
                        {
                            IsActive = new IsActive()
                            {
                                Id = "isActive",
                                Direction = -1
                            },
                            CurrDate = new CurrDate()
                            {
                                Id = "CurrDate",
                                Direction  = 1,
                                Order = 1
                            }
                        }

                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Matter/childStates/MattRate/clearUISort",
                        Value = true
                    }
                },
                Index = "0",
                Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattRate/rows"
            };

            var requestBidy = JsonConvert.SerializeObject(body, JsonHelper.Settings);

            return await ProcessDataService.AddDataAsync(sessionId, processItemId, requestBidy);

        }

        public async Task<IRestResponse> AddEffectiveDatedInformationAsync(string processItemId, string sessionId, string rowKey, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattDate/rows/" + rowKey +
                               "/attributes/Office/value",
                        Value = matter.OfficeKey
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattDate/rows/" + rowKey +
                               "/attributes/Department/value",
                        Value = matter.DepartmentKey
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattDate/rows/" + rowKey +
                               "/attributes/Section/value",
                        Value = matter.SectionKey
                    }

                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterPayerFormAsync(string processItemId, string sessionId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op =  "add",
                        Path = "/objects/Matter/rows/"+ matter.MatterId+"/childObjects/MattPayor/rows/-",
                        Value = new ValueClass()
                        {
                            SubclassId = "MattPayor"
                        }
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterPayerDetailsFormAsync(string processItemId, string sessionId, ApiMatterEntity matter, string rowId)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op =  "add",
                        Path = "/objects/Matter/rows/"+ matter.MatterId +"/childObjects/MattPayor/rows/"+ rowId +"/childObjects/MattPayorDetail/rows/-",
                        Value = new ValueClass()
                        {
                            SubclassId = "MattPayorDetail"
                        }
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddMatterPayorAsync(string processItemId, string sessionId, string rowId, string payorRowId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattPayor/rows/"+ rowId +"/childObjects/MattPayorDetail/rows/"+payorRowId+"/attributes/Payor/value",
                        Value = matter.PayorCode,
                        Alias = matter.PayorName,
                        Id = "Payor"
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEffectiveDatedInformationLiteAsync(string processItemId, string sessionId, string rowKey, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattDate/rows/" + rowKey +
                               "/attributes/Office/value",
                        Value = matter.OfficeKey
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattDate/rows/" + rowKey +
                               "/attributes/Department/value",
                        Value = matter.DepartmentKey
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattDate/rows/" + rowKey +
                               "/attributes/Section/value",
                        Value = matter.SectionKey
                    },
                      new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattDate/rows/" + rowKey +
                               "/attributes/PracticeGroup/value",
                        Value = matter.EDIPracticeKey
                    }

                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddMatterRateAsync(string processItemId, string sessionId, string rowKey, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/MattRate/rows/" + rowKey +
                               "/attributes/Rate/value",
                        Value = matter.RateCode,
                        Alias = matter.RateAlias,
                        Id = "Rate"
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetNewBillingSiteAsync(string processItemId, string sessionId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new BillSiteModel()
            {
                InterfaceId = "MattNewSite",
                Path = "/objects/Matter/rows/" + matter.MatterId + "/actions/NewBillSite"
            }, JsonHelper.Settings);

            string urlExtension = "data/action/NewBillSite";
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> AddNewBillingSiteAsync(string processItemId, string sessionId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/MattNewSite/rows/"+matter.SiteId.FirstOrDefault()+"/attributes/Site/value",
                        Value = "155",
                        Alias =  "London - Domestic Client - Subsidiary",
                        Id =  "Site"
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/MattNewSite/rows/"+matter.SiteId.FirstOrDefault()+"/attributes/Country/value",
                        Value = ""
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/MattNewSite/rows/"+matter.SiteId.FirstOrDefault()+"/attributes/SiteType/value",
                        Value = ""
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/MattNewSite/rows/"+matter.SiteId.FirstOrDefault()+"/attributes/Street/value",
                        Value = "34rth2 New Street4"
                    }
                },
                ContextId = matter.SiteId.LastOrDefault()
            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> PostBillingSiteDataAsync(string processItemId, string sessionId,
            ApiMatterEntity matter)
        {

            var body = JsonConvert.SerializeObject(new FormActionModel()
            {
                ContextId = new Guid(matter.SiteId.LastOrDefault()),
                Path = "/objects/MattNewSite/rows/" + matter.SiteId.FirstOrDefault() + "/actions/Ok"

            }, JsonHelper.Settings);
            return await ProcessDataService.PostActionDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> SelectCostTypeGroupAsync(string processItemId, string sessionId, ApiMatterEntity matter)
        {
            var urlExtension = "data/action/AddByQuery";

            var body = JsonConvert.SerializeObject(new AddQueryModel()
            {
                Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/CostTypeGroupMatter_ccc/actions/AddByQuery",
                SelectedRows = new List<Guid>(matter.CostTypeGroup)

            }, JsonHelper.Settings); ;
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> SelectChargeTypeGroupAsync(string processItemId, string sessionId, ApiMatterEntity matter)
        {
            var urlExtension = "data/action/AddByQuery";

            var body = JsonConvert.SerializeObject(new AddQueryModel()
            {
                Path = "/objects/Matter/rows/" + matter.MatterId + "/childObjects/ChrgTypeGroupMatter_ccc/actions/AddByQuery",
                SelectedRows = new List<Guid>(matter.ChargeTypeGroup)

            }, JsonHelper.Settings); ;
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> GetBillingGroupFormAsync(string processItemId, string sessionId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op =  "add",
                        Path = "/objects/Matter/rows/"+ matter.MatterId+"/childObjects/BillingGroupMatter1/rows/-",
                        Value = new ValueClass()
                        {
                            SubclassId = "BillingGroupMatter1"
                        }
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> SelectBillingGroupAsync(string processItemId, string sessionId, string rowId, ApiMatterEntity matter)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/Matter/rows/"+matter.MatterId+"/childObjects/BillingGroupMatter1/rows/" + rowId + "/attributes/BillingGroup/value",
                        Value = matter.BillingGroupCode,
                        Alias = matter.BillingGroupDescription,
                        Id = "BillingGroup"

                    }
                }
            }, JsonHelper.Settings); ;
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterClientAsync(string sessionId, string processItemId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Client",
                Path = "/objects/Matter/rows/ " + matter.MatterId + "/attributes/Client/aliasValue",
                Text = matter.Client,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterRateAsync(string sessionId, string processItemId, string mattertRateRowId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Rate",
                Path = "/objects/Matter/rows/ " + matter.MatterId + "/childObjects/MattRate/rows/" + mattertRateRowId + "/attributes/Rate/aliasValue",
                Text = matter.Rate,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }


        public async Task<IRestResponse> GetMatterPayeeAsync(string sessionId, string processItemId, string payeeRowId, string addPayeeDetailRowId,  ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Payor",
                Path = "/objects/Matter/rows/ " + matter.MatterId + "/childObjects/MattPayor/rows/" + payeeRowId + "/childObjects/MattPayorDetail/rows/"+addPayeeDetailRowId+"/attributes/Payor/aliasValue",
                Text = matter.PayorName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterChargeTypeGroupAsync(string sessionId, string processItemId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "ChrgTypeGroup_ccc",
                ActionPath = "/objects/Matter/rows/ " + matter.MatterId + "/childObjects/ChrgTypeGroupMatter_ccc/actions/AddByQuery",
                Text = matter.ChargeTypeGroupName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterCostTypeGroupAsync(string sessionId, string processItemId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "CostTypeGroup_ccc",
                ActionPath = "/objects/Matter/rows/ " + matter.MatterId + "/childObjects/CostTypeGroupMatter_ccc/actions/AddByQuery",
                Text = matter.CostTypeGroupName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterBillingGroupAsync(string sessionId, string processItemId, string billingGroupRowId, ApiMatterEntity matter)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "BillingGroup",
                Path = "/objects/Matter/rows/ " + matter.MatterId + "/childObjects/BillingGroupMatter1/rows/" + billingGroupRowId + "/attributes/BillingGroup/aliasValue",
                Text = matter.BillingGroupDescription,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
