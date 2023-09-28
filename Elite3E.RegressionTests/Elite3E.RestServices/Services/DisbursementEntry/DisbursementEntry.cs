using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.DisbursementEntry
{
    public class DisbursementEntry : IDisbursementEntry
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/CostCardPending/rows/" + disbursementEntry.Id + "/attributes/Matter/value",
                        Value = disbursementEntry.MatterRowKey,
                        Alias = disbursementEntry.MatterNumber,
                        Id = "Matter"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry)
        {
            var narrativeText = $"<p>{disbursementEntry.Narrative}</p><p><br></p>";
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/CostCardPending/rows/" + disbursementEntry.Id + "/attributes/Narrative/value",
                        Value = narrativeText
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/CostCardPending/rows/" + disbursementEntry.Id + "/attributes/TaxCode/value",
                        Value = disbursementEntry.Taxcode,
                        Alias = disbursementEntry.TaxCodeDescription,
                        Id = "TaxCode"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddDisbursementTypeAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/CostCardPending/rows/" + disbursementEntry.Id + "/attributes/CostType/value",
                        Value = disbursementEntry.DisbursementTypeCode,
                        Alias = disbursementEntry.DisbursementType
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Matter",
                Path = "/objects/CostCardPending/rows/" + disbursementEntry.Id + "/attributes/Matter/aliasValue",
                Text = disbursementEntry.MatterNumber,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "TaxCode",
                Path = "/objects/CostCardPending/rows/" + disbursementEntry.Id + "/attributes/TaxCode/aliasValue",
                Text = disbursementEntry.TaxCodeDescription,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetDisbursementTypeAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "CostType",
                ActionPath = "/objects/CostCardPending/rows/" + disbursementEntry.Id + "/attributes/CostType/aliasValue",
                Text = disbursementEntry.DisbursementType,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddWorkRateAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/CostCardPending/rows/" + disbursementEntry.Id + "/attributes/WorkRate/value",
                        Value = disbursementEntry.WorkRateValue
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddCurrencyAsync(string sessionId, string processItemId, ApiDisbursementEntryEntity disbursementEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/CostCardPending/rows/" + disbursementEntry.Id + "/attributes/Currency/value",
                        Value = disbursementEntry.CurrencyCode
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
