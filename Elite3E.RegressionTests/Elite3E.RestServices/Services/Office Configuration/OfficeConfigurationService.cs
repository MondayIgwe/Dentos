using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.Office_Configuration
{

    public class OfficeConfigurationService : IOfficeConfigurationService
    {

        public IProcessDataService ProcessDataService = new ProcessDataService();
        private ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddOfficeConfigurationDataAsync(string sessionId, string processItemId, ApiOfficeConfiguration officeConfig)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/OfficeConfiguration_ccc/rows/"+officeConfig.OfficeId+"/attributes/Office/value",
                        Value = officeConfig.OfficeKey
                    },
                     new()
                    {
                        Op = "replace",
                        Path = "/objects/OfficeConfiguration_ccc/rows/"+officeConfig.OfficeId+"/attributes/TrustDisbursementType/value",
                        Value = officeConfig.DisbursementTypeKey
                    },
                      new()
                    {
                        Op = "replace",
                        Path = "/objects/OfficeConfiguration_ccc/rows/"+officeConfig.OfficeId+"/attributes/Payee/value",
                        Value = officeConfig.PayeeValue
                    },
                         new()
                    {
                          Id="FinanceClerk",
                          Alias=officeConfig.TImeKeeper,
                        Op = "replace",
                        Path ="/objects/OfficeConfiguration_ccc/rows/"+officeConfig.OfficeId+"/attributes/FinanceClerk/value",
                        Value = officeConfig.TimeKeeperValue
                    },
                    }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetLookupSearchPayee(string sessionId, string processItemId, ApiOfficeConfiguration officeConfig)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Payee",
                ActionPath = "/objects/OfficeConfiguration_ccc/rows/" + officeConfig.OfficeId + "/attributes/Payee/aliasValue",
                Text = officeConfig.PayeeKey,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetLookupTimekeeperLeaver(string sessionId, string processItemId, ApiOfficeConfiguration officeConfig)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "NxBaseUser",
                ActionPath = "/objects/OfficeConfiguration_ccc/rows/"+officeConfig.OfficeId+"/attributes/FinanceClerk/aliasValue",
                Text = officeConfig.TImeKeeper,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
