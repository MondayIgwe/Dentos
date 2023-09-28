using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.ChargeEntry
{
    public class ChargeEntryService : IChargeEntryService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCardPending/rows/" + chargeEntry.Id + "/attributes/Matter/value",
                        Value = chargeEntry.MatterRowKey,
                        Alias = chargeEntry.MatterNumber,
                        Id = "Matter"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry)
        {
            var narrativeText = $"<p>{chargeEntry.Narrative}</p><p><br></p>";
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCardPending/rows/" + chargeEntry.Id + "/attributes/Narrative/value",
                        Value = narrativeText
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCardPending/rows/" + chargeEntry.Id + "/attributes/TaxCode/value",
                        Value = chargeEntry.Taxcode,
                        Alias = chargeEntry.TaxCodeDescription,
                        Id = "TaxCode"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddChargeTypeAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCardPending/rows/" + chargeEntry.Id + "/attributes/ChrgType/value",
                        Value = chargeEntry.ChargeTypeCode
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Matter",
                Path = "/objects/ChrgCardPending/rows/" + chargeEntry.Id + "/attributes/Matter/aliasValue",
                Text = chargeEntry.MatterNumber,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "TaxCode",
                Path = "/objects/ChrgCardPending/rows/" + chargeEntry.Id + "/attributes/TaxCode/aliasValue",
                Text = chargeEntry.TaxCodeDescription,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddAmountAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCardPending/rows/" + chargeEntry.Id + "/attributes/Amount/value",
                        Value = chargeEntry.Amount
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddCurrencyAsync(string sessionId, string processItemId, ApiChargeEntryEntity chargeEntry)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCardPending/rows/" + chargeEntry.Id + "/attributes/Currency/value",
                        Value = chargeEntry.CurrencyCode
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
