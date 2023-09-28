using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.PropagationProcess
{
    public class PropagationProcessService : IPropagationProcessService
    {
        public ILookUpService LookUpService = new LookUpService();
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public async Task<IRestResponse> AddPropagationProcessAsync(string processItemId, string sessionId, SetupProcessEntity setupProcessEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/SetupPropagation_ccc/rows/" + setupProcessEntity.SetupProcessId + "/attributes/Process/value",
                        Value = setupProcessEntity.ProcessValue
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetPropagationProcess(string sessionId, string processItemId, SetupProcessEntity setupProcessEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "NxFWKAppObject",
                Path = "/objects/SetupPropagation_ccc/rows/" + setupProcessEntity.SetupProcessId + "/attributes/Process/aliasValue",
                Text = setupProcessEntity.ProcessAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}

