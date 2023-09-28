using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.Client
{
    public class ClientService : IClientService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddClientDataAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Client/rows/"+ client.Id +"/attributes/Entity/value",
                        Value = client.Entity,
                        Alias = client.EntityName,
                        Id = "Entity"
                    },
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/Client/rows/"+ client.Id +"/attributes/DisplayName/value",
                        Value = client.Name
                    },
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/Client/rows/"+ client.Id +"/attributes/CliStatusType/value",
                        Value = client.StatusCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/Client/rows/"+ client.Id +"/attributes/Country/value",
                        Value = client.CountryCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/Client/rows/"+ client.Id +"/attributes/Currency/value",
                        Value = client.CurrencyCode
                    },
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/Client/rows/"+ client.Id +"/attributes/InvoiceSite/value",
                        Value = client.InvoiceSite,
                        Alias = client.InvoiceSiteName,
                        Id = "InvoiceSite"
                    },
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/Client/rows/"+ client.Id +"/attributes/OpenTkpr/value",
                        Value = client.OpeningFeeEarnerKey,
                        Alias = client.OpeningFeeEarnerAlias,
                        Id = "OpenTkpr"
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddClientInvoiceSiteDataAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/Client/rows/"+ client.Id +"/attributes/InvoiceSite/value",
                        Value = client.InvoiceSite,
                        Alias = client.InvoiceSiteName,
                        Id = "InvoiceSite"
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEffectiveDatedInformationAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Client/childStates/CliDate/sortAttributes",
                        Value = new ValueClass
                        {
                            EffStart = new EffStart()
                            {
                                Id = "EffStart",
                                Direction = -1
                            }
                        }
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Client/childStates/CliDate/clearUISort",
                        Value = true
                    }
                },
                Index = "0",
                Path = "/objects/Client/rows/" + client.Id + "/childObjects/CliDate/rows"

            }, JsonHelper.Settings);

            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetClientBillingFeeEarnerSearchList(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Timekeeper",
                Path = "/objects/Client/rows/ " + client.Id + "/childObjects/CliDate/rows/" + rowId + "/attributes/BillTkpr/aliasValue",
                Text = client.BillingFeeEarnerName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetClientEntitySearchList(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Entity",
                Path = "/objects/Client/rows/ " + client.Id + "/attributes/Entity/aliasValue",
                Text = client.EntityName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetClientInvoiceSiteSearchList(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Site",
                Path = "/objects/Client/rows/ " + client.Id + "/attributes/InvoiceSite/aliasValue",
                Text = client.InvoiceSiteName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetClientOpeningFeeEarnerSearchList(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Timekeeper",
                Path = "/objects/Client/rows/ " + client.Id + "/attributes/OpenTkpr/aliasValue",
                Text = client.OpeningFeeEarnerName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetClientResponsibleFeeEarnerSearchList(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Timekeeper",
                Path = "/objects/Client/rows/ " + client.Id + "/childObjects/CliDate/rows/" + rowId + "/attributes/RspTkpr/aliasValue",
                Text = client.ResponsibleFeeEarnerName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> SelectTheClientAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new AddBatchModel()
            {
                Path = "/objects/Client/rows/-",
                ItemIDs = new List<System.Guid>(){
                    {new(client.ClientNumberRowKeyValue) }
                }

            }, JsonHelper.Settings); ;

            var urlExtension = "data/batchadd";
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> GetBillingRulesValidationListAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "EBillValList",
                ActionPath = "/objects/Client/rows/ " + client.Id + "/childObjects/CliEBillValList/actions/AddByQuery",
                Text = client.BillingRulesValidationName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
        public async Task<IRestResponse> AddBillingRulesValidationListAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var urlExtension = "data/action/AddByQuery";

            var body = JsonConvert.SerializeObject(new AddQueryModel()
            {
                Path = "/objects/Client/rows/" + client.Id + "/childObjects/CliEBillValList/actions/AddByQuery",
                SelectedRows = new List<Guid>(client.BillingRulesValidationKey)

            }, JsonHelper.Settings); ;
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }


        public async  Task<IRestResponse> GetClientSupervisingFeeEarnerSearchList(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Timekeeper",
                Path = "/objects/Client/rows/ " + client.Id + "/childObjects/CliDate/rows/" + rowId + "/attributes/SpvTkpr/aliasValue",
                Text = client.SupervisingFeeEarnerName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> UpdateEffectiveDateInformationAsync(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Client/rows/"+ client.Id +"/childObjects/CliDate/rows/"+ rowId +"/attributes/BillTkpr/value",
                        Value = client.BillingFeeEarnerRowKey,
                        Alias = client.BillingFeeEarnerAlias,
                        Id = "BillTkpr"
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Client/rows/"+ client.Id +"/childObjects/CliDate/rows/"+ rowId +"/attributes/RspTkpr/value",
                        Value = client.ResponsibleFeeEarnerRowKey,
                        Alias = client.ResponsibleFeeEarnerAlias,
                        Id = "RspTkpr"
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Client/rows/"+ client.Id +"/childObjects/CliDate/rows/"+ rowId +"/attributes/SpvTkpr/value",
                        Value = client.SupervisingFeeEarnerRowKey,
                        Alias = client.SupervisingFeeEarnerAlias,
                        Id = "SpvTkpr"
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Client/rows/"+ client.Id +"/childObjects/CliDate/rows/"+ rowId +"/attributes/Office/value",
                        Value = client.OfficeKey,
                       
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetExistingBillingRulesValidationListAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/Client/childStates/CliEBillValList/sortAttributes",
                       Value = new ValueClass
                        {
                            EBillValList = new Models.RequestModels.BillingRulesValidationList()
                            {
                                Id = "EBillValList",
                                Direction = 1,
                                Order = 0
                            }
                        }
                    },
                    new()
                    {
                        Op = "replace",
                        Path ="/objectStates/Client/childStates/CliEBillValList/clearUISort",
                        Value = true
                    }
                },
                Path = "/objects/Client/rows/" + client.Id + "/childObjects/CliEBillValList",
                StartRow = 0,
                CacheBlockSize = 100

            }, JsonHelper.Settings);

            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddDateOpendAsync(string sessionId, string processItemId, ApiClientMaintenanceEntity client)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Client/rows/"+ client.Id +"/attributes/OpenDate/value",
                        Value = client.DateOpened
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEDIStartDateAsync(string sessionId, string processItemId, string rowId, ApiClientMaintenanceEntity client)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Client/rows/"+ client.Id +"/childObjects/CliDate/rows/"+ rowId +"/attributes/EffStart/value",
                        Value = client.EDIStartDate
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
