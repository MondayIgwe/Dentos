using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.FiscalInvoiceSetup
{
    public class FiscalInvoiceSetupService : IFiscalInvoiceSetupService
    {
        public ILookUpService LookUpService = new LookUpService();
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public async Task<IRestResponse> AddFiscalInvoiceSetupAsync(string sessionId, string processItemId, FiscalInvoiceSetupEntity fiscalInvoiceSetupEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/FiscalInvoiceSetup_ccc/rows/" + fiscalInvoiceSetupEntity.FiscalSetupId + "/attributes/NxUnit/value",
                        Value = fiscalInvoiceSetupEntity.Unit
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/FiscalInvoiceSetup_ccc/rows/" +  fiscalInvoiceSetupEntity.FiscalSetupId+ "/attributes/BillGLType/value",
                        Value = fiscalInvoiceSetupEntity.BillGlTypeValue
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/FiscalInvoiceSetup_ccc/rows/" +  fiscalInvoiceSetupEntity.FiscalSetupId + "/attributes/SuspenseGLType/value",
                        Value = fiscalInvoiceSetupEntity.SuspenseGlTypeValue
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/FiscalInvoiceSetup_ccc/rows/" +  fiscalInvoiceSetupEntity.FiscalSetupId + "/attributes/FiscalInvoicePrefix/value",
                        Value = fiscalInvoiceSetupEntity.FiscalInvoicePrefix
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/FiscalInvoiceSetup_ccc/rows/" +  fiscalInvoiceSetupEntity.FiscalSetupId + "/attributes/NextFiscalInvoiceNumber/value",
                        Value = fiscalInvoiceSetupEntity.NextFiscalInvoiceNumber
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetFiscalInvoiceSearchGLTypeAsync(string sessionId, string processItemId, FiscalInvoiceSetupEntity fiscalInvoiceSetupEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "GLType",
                Path = "/objects/FiscalInvoiceSetup_ccc/rows/"+fiscalInvoiceSetupEntity.FiscalSetupId+"/attributes/BillGLType/aliasValue",
                Text = fiscalInvoiceSetupEntity.BillGlTypeAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetFiscalInvoiceSuspenseGLTypeAsync(string sessionId, string processItemId, FiscalInvoiceSetupEntity fiscalInvoiceSetupEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "GLType",
                Path = "/objects/FiscalInvoiceSetup_ccc/rows/" + fiscalInvoiceSetupEntity.FiscalSetupId + "/attributes/SuspenseGLType/aliasValue",
                Text = fiscalInvoiceSetupEntity.SuspenseGlTypeAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
