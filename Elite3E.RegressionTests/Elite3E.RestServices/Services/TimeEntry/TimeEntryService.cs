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

namespace Elite3E.RestServices.Services.TimeEntry
{
    public class TimeEntryService : ITimeEntryService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddFeeEranerAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCardPending/rows/" + timeEntry.Id + "/attributes/Timekeeper/value",
                        Value = timeEntry.FeeEranerRowKey,
                        Alias = timeEntry.FeeEarnerId,
                        Id = "Timekeeper"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCardPending/rows/" + timeEntry.Id + "/attributes/Matter/value",
                        Value = timeEntry.MatterRowKey,
                        Alias = timeEntry.MatterNumber,
                        Id = "Matter"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry)
        {
            var narrativeText = $"<p>{timeEntry.Narrative}</p><p><br></p>";
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCardPending/rows/" + timeEntry.Id + "/attributes/Narrative/value",
                        Value = narrativeText
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCardPending/rows/" + timeEntry.Id + "/attributes/TaxCode/value",
                        Value = timeEntry.Taxcode,
                        Alias = timeEntry.TaxCodeDescription,
                        Id = "TaxCode"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddTimeTypeAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/TimeCardPending/rows/" + timeEntry.Id + "/attributes/TimeType/value",
                        Value = timeEntry.TimeTypeCode
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetFeeEranerDeatilsAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Timekeeper",
                Path = "/objects/TimeCardPending/rows/" + timeEntry.Id + "/attributes/Timekeeper/aliasValue",
                Text = timeEntry.FeeEarnerId,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Matter",
                Path = "/objects/TimeCardPending/rows/" + timeEntry.Id + "/attributes/Matter/aliasValue",
                Text = timeEntry.MatterNumber,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiTimeEntryEntity timeEntry)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "TaxCode",
                Path = "/objects/TimeCardPending/rows/" + timeEntry.Id + "/attributes/TaxCode/aliasValue",
                Text = timeEntry.TaxCodeDescription,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
