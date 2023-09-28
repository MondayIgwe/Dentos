using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.TimeModify
{
    public class TimeModifyService : ITimeModifyService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddFeeEranerAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/Timekeeper/value",
                        Value = timeModify.FeeEranerRowKey,
                        Alias = timeModify.FeeEarnerId,
                        Id = "Timekeeper"
                    }
                }
            }, JsonHelper.Settings); 

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/Matter/value",
                        Value = timeModify.MatterRowKey,
                        Alias = timeModify.MatterNumber,
                        Id = "Matter"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var narrativeText = $"<p>{timeModify.Narrative}</p><p><br></p>";
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/Narrative/value",
                        Value = narrativeText
                    }
                }
            }, JsonHelper.Settings); 

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/TaxCode/value",
                        Value = timeModify.Taxcode,
                        Alias = timeModify.TaxCodeDescription,
                        Id = "TaxCode"
                    }
                }
            }, JsonHelper.Settings); 

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddTimeTypeAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/TimeType/value",
                        Value = timeModify.TimeTypeCode
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
        public async Task<IRestResponse> AddCurrencyAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/Currency/value",
                        Value = timeModify.Currency
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
        public async Task<IRestResponse> AddWorkingHoursAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/WorkHrs/value",
                        Value = timeModify.WorkHours
                    }
                }
            }, JsonHelper.Settings); 

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddWorkDateAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/WorkDate/value",
                        Value = timeModify.WorkDate //2022-02-17
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetFeeEranerDeatilsAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Timekeeper",
                Path = "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/Timekeeper/aliasValue",
                Text = timeModify.FeeEarnerId,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Matter",
                Path = "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/Matter/aliasValue",
                Text = timeModify.MatterNumber,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiTimeModifyEntity timeModify)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "TaxCode",
                Path = "/objects/TimeCard/rows/" + timeModify.Id + "/attributes/TaxCode/aliasValue",
                Text = timeModify.TaxCodeDescription,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
