using Elite3E.RestServices.Builders;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Elite3E.RestServices.Models.RequestModels.OpenProcess;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.ReceiptApplyReverse
{
    public class ReceiptsApplyReverseService : IReceiptsApplyReverseService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddReceiptsReceiptTypeDataAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {

                Changes = new List<Changes>()
                {
                    new()
                    {
                        Id="ReceiptType",
                        Op = "replace",
                        Path = "/objects/RcptMaster/rows/"+receiptApplyReverseEntity.Id+"/attributes/ReceiptType/value",
                        Value = receiptApplyReverseEntity.ReceiptTypeValue
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddReceiptsReceiptAmountDataAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {

                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RcptMaster/rows/"+receiptApplyReverseEntity.Id+"/attributes/RcptAmt/value",
                        Value = receiptApplyReverseEntity.ReceiptAmount,
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddReceiptsApplyReverseDataAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity)
        {
            //var narrativeText = $"<p>{receiptApplyReverseEntity.Narrative}</p><p><br></p>";
            var narrativeText = $"<p>{receiptApplyReverseEntity.Narrative}</p>";
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {

                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RcptMaster/rows/"+receiptApplyReverseEntity.Id+"/attributes/DocNumber/value",
                        Value = receiptApplyReverseEntity.DocumentNumber,
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RcptMaster/rows/"+receiptApplyReverseEntity.Id+"/attributes/Narrative/value",
                        Value = narrativeText,
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetReceiptTypeAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "ReceiptType",
                Path = "/objects/RcptMaster/rows/"+receiptApplyReverseEntity.Id+"/attributes/ReceiptType/aliasValue",
                Text = receiptApplyReverseEntity.ReceiptTypeAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddChildFormAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity)
        {
            var urlExtension = "data/action/AddByQuery";

            var body = JsonConvert.SerializeObject(new AddQueryModel()
            {
                Path = "/objects/RcptMaster/rows/" + receiptApplyReverseEntity.Id + "/childObjects/RcptInvoice/actions/AddByQuery"

            }, JsonHelper.Settings);
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> GetInvoiceKeyAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "InvPayor",
                ActionPath = "/objects/RcptMaster/rows/" + receiptApplyReverseEntity.Id + "/childObjects/RcptInvoice/actions/AddByQuery",
                Text = receiptApplyReverseEntity.MatterNumber,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddInvoiceAsync(string sessionId, string processItemId, ApiReceiptsApplyReverseEntity receiptApplyReverseEntity)
        {
            var urlExtension = "data/action/AddByQuery";

            var body = JsonConvert.SerializeObject(new AddQueryModel()
            {
                Path = "/objects/RcptMaster/rows/" + receiptApplyReverseEntity.Id + "/childObjects/RcptInvoice/actions/AddByQuery",
                SelectedRows = receiptApplyReverseEntity.InvoiceKey

            }, JsonHelper.Settings); 
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> PostUpdateReceiptAsync(string sessionId, string processItemId)
        {
            var body = JsonConvert.SerializeObject(new Changes()
            {
                Path = "/objects/RcptMaster"
            }, JsonHelper.Settings);

            var urlExtension = "data/action/Update";
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(body)
                .ExecuteRequestAsync();
        }
    }
}
