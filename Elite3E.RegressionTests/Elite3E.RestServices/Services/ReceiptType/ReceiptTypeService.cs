using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.ReceiptType
{
    public  class ReceiptTypeService : IReceiptTypeService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();
        public async Task<IRestResponse> CreateReceiptTypeAsync(string sessionId, string processItemId, ApiReceiptTypeEntity receiptType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ReceiptType/rows/"+ receiptType.Id + "/attributes/Code/value",
                        Value = receiptType.Code
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ReceiptType/rows/"+receiptType.Id + "/attributes/Description/value",
                        Value = receiptType.Description                        
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ReceiptType/rows/"+receiptType.Id + "/attributes/BankAcct/value",
                        Value = receiptType.BankAccountValue,
                        Alias = receiptType.BankAccountDisplayName,
                        Id = "BankAcct"
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ReceiptType/rows/"+receiptType.Id+"/attributes/ToleranceAmt/value",
                        Value = receiptType.ToleranceAmount
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ReceiptType/rows/"+receiptType.Id+"/attributes/TolerancePct/value",
                        Value = receiptType.TolerancePercentage
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/ReceiptType/rows/"+receiptType.Id+"/attributes/CurrencyType/value",
                        Value = receiptType.CurrencyTypeValue
                    }

                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetReceiptTypeBankAccountAsync(string sessionId, string processItemId, ApiReceiptTypeEntity receiptType)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "BankAcct",
                Path = "/objects/ReceiptType/rows/" + receiptType.Id + "/attributes/BankAcct/aliasValue",
                Text = receiptType.BankAccountDisplayName,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
