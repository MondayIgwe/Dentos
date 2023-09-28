using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.ChargeModify
{
    public class ChargeModifyService : IChargeModifyService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddMatterAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCard/rows/" + chargeModify.Id + "/attributes/Matter/value",
                        Value = chargeModify.MatterRowKey,
                        Alias = chargeModify.MatterNumber,
                        Id = "Matter"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddNarrativeAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify)
        {
            var narrativeText = $"<p>{chargeModify.Narrative}</p><p><br></p>";
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCard/rows/" + chargeModify.Id + "/attributes/Narrative/value",
                        Value = narrativeText
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddTaxCodeAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCard/rows/" + chargeModify.Id + "/attributes/TaxCode/value",
                        Value = chargeModify.Taxcode,
                        Alias = chargeModify.TaxCodeDescription,
                        Id = "TaxCode"
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddChargeTypeAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCard/rows/" + chargeModify.Id + "/attributes/ChrgType/value",
                        Value = chargeModify.ChargeTypeCode
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetMatterDeatilsAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "Matter",
                Path = "/objects/ChrgCard/rows/" + chargeModify.Id + "/attributes/Matter/aliasValue",
                Text = chargeModify.MatterNumber,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetTaxCodeDeatilsAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "TaxCode",
                Path = "/objects/ChrgCard/rows/" + chargeModify.Id + "/attributes/TaxCode/aliasValue",
                Text = chargeModify.TaxCodeDescription,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddAmountAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCard/rows/" + chargeModify.Id + "/attributes/WorkAmt/value",
                        Value = chargeModify.Amount
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddCurrencyAsync(string sessionId, string processItemId, ApiChargeModifyEntity chargeModify)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/ChrgCard/rows/" + chargeModify.Id + "/attributes/Currency/value",
                        Value = chargeModify.CurrencyCode
                    }
                }
            }, JsonHelper.Settings); ;

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

    }
}
