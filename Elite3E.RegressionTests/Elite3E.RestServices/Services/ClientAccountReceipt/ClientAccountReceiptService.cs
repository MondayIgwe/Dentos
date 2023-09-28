using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Services.ClientAccountReceipt
{
    public class ClientAccountReceiptService : IClientAccountReceiptService
    {
        public ILookUpService LookUpService = new LookUpService();
        public IProcessDataService ProcessDataService = new ProcessDataService();

        public async Task<IRestResponse> AddClientAccountReceiptDataAsync(string sessionId, string processItemId, ApiClientAccountReceiptEntity clientAccountReceipt)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/TrustReceipt/rows/"+clientAccountReceipt.ClientAccountReceiptId+"/attributes/TrustReceiptType/value",
                        Value = clientAccountReceipt.ClientAccountReceiptType
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/TrustReceipt/rows/"+clientAccountReceipt.ClientAccountReceiptId+"/attributes/BankAcctTrust/value",
                        Value = clientAccountReceipt.ClientAccountAcct,
                        Alias = clientAccountReceipt.ClientAccountAcct.String,
                        Id = "BankAcctTrust"
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/TrustReceipt/rows/"+clientAccountReceipt.ClientAccountReceiptId+"/attributes/DocumentNumber/value",
                        Value = clientAccountReceipt.DocumentNumber,
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetClientAccountAcct(string sessionId, string processItemId, ApiClientAccountReceiptEntity clientAccountReceiptEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "BankAcctTrust",
                Path = "/objects/TrustReceipt/rows/" + clientAccountReceiptEntity.ClientAccountReceiptId + "/attributes/BankAcctTrust/aliasValue",
                Text = clientAccountReceiptEntity.ClientAccountAcct.String,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetClientAccountMatter(string sessionId, string processItemId, string rowKey, ApiClientAccountReceiptEntity clientAccountReceiptEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Matter",
                Path = "/objects/TrustReceipt/rows/" + clientAccountReceiptEntity.ClientAccountReceiptId + "/childObjects/TrustReceiptDetail/rows/" + rowKey + "/attributes/Matter/aliasValue",
                Text = clientAccountReceiptEntity.MatterNumber.String,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddClientAccountReceiptDetailDataAsync(string sessionId, string processItemId, string rowKey, ApiClientAccountReceiptEntity clientAccountReceipt)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/TrustReceipt/rows/"+clientAccountReceipt.ClientAccountReceiptId+"/childObjects/TrustReceiptDetail/rows/"+rowKey+"/attributes/Amount/value",
                        Value = clientAccountReceipt.Amount
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/TrustReceipt/rows/"+clientAccountReceipt.ClientAccountReceiptId+"/childObjects/TrustReceiptDetail/rows/"+rowKey+"/attributes/Matter/value",
                        Value = clientAccountReceipt.MatterNumber,
                        Alias = clientAccountReceipt.MatterNumber.String,
                        Id = "Matter"
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);

        }

        public async Task<IRestResponse> GetClientAccountReceiptRowKeyDetail(string processItemId, string sessionId, ApiClientAccountReceiptEntity clientAccountReceipt)
        {
            var body = new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/TrustReceipt/childStates/TrustReceiptDetail/sortAttributes",
                        Value = new ValueClass()
                        {
                            Amount =new Amount()
                            {
                                Id = "Amount",
                                Direction = 1,
                                Order=2
                            },
                            Matter = new Matter()
                            {
                                Id = "Matter",
                                Direction  = 1
                            },
                            TrustIntendedUse = new TrustIntendedUse()
                            {
                                Id = "TrustIntendedUse",
                                Direction  = 1,
                                Order = 1
                            }
                        }
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/TrustReceipt/childStates/TrustReceiptDetail/clearUISort",
                        Value =true
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/TrustReceipt/childStates/TrustReceiptDetail/clearUIGroup",
                        Value =true
                    }
                },
                Index = "0",
                Path = "/objects/TrustReceipt/rows/" + clientAccountReceipt.ClientAccountReceiptId + "/childObjects/TrustReceiptDetail/rows"
            };

            var requestBidy = JsonConvert.SerializeObject(body, JsonHelper.Settings);

            return await ProcessDataService.AddDataAsync(sessionId, processItemId, requestBidy);
        }
    }
}
