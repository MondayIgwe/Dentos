using Elite3E.RestServices.Builders;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Elite3E.RestServices.Models.RequestModels.OpenProcess;
using Newtonsoft.Json;
using RestSharp;


namespace Elite3E.RestServices.Services.ProformaEdit
{
    public class ProformaEditService : IProformaEditService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddSearchResultToWorklistAsync(string sessionId, string processItemId, string BatchAddRowKey)
        {
            var body = JsonConvert.SerializeObject(new AddBatchModel()
            {
                Path = "/objects/Proforma/rows/-",
                ItemIDs = new List<System.Guid>(){
                    {new(BatchAddRowKey) }
                }

            }, JsonHelper.Settings);

            var urlExtension = "data/batchadd";
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> BillNoPrintAsync(string sessionId, string processItemId)
        {
            var body = JsonConvert.SerializeObject(new OpenProcessRequestModel()
            {
                PageId = "ProfMaster",
                IsPrint = "false"
            }, JsonHelper.Settings);

            var urlExtension = "process/output/Bill_NP";
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.POST)
                .WithHeader("X-3E-SessionId", sessionId)
                .WithHeader("X-3E-ProcessItemId", processItemId)
                .WithJsonContent(body)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> GetPossibleErrorsAsync(string sessionId, string processItemId)
        {
            var urlExtension = "process/" + processItemId;
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.GET)
                .WithHeader("X-3E-SessionId", sessionId)
                .ExecuteRequestAsync();
        }
    }
}
