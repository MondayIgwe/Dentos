using Elite3E.RestServices.Entity;
using RestSharp;

namespace Elite3E.RestServices.Services.UnallocatedType
{
    public class UnallocatedTypeService : IUnallocatedTypeService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public Task<IRestResponse> CreateUnallocatedTypeAsync(string sessionId, string processItemId, ApiUnallocatedTypeEntity unallocatedType)
        {
            throw new NotImplementedException();
        }
        //public async Task<IRestResponse> CreateUnallocatedTypeAsync(string sessionId, string processItemId, ApiUnallocatedTypeEntity unallocatedType)
        //{
        //    var body = JsonConvert.SerializeObject(new ChildFormModel()
        //    {
        //        Changes = new List<Changes>()
        //        {
        //            new Changes()
        //            {
        //                Op = "replace",
        //                Path = "/objects/ReceiptType/rows/"+ unallocatedType.Id + "/attributes/Code/value",
        //                Value = receiptType.Code
        //            },
        //            new Changes()
        //            {
        //                Op = "replace",
        //                Path = "/objects/ReceiptType/rows/"+unallocatedType.Id + "/attributes/Description/value",
        //                Value = receiptType.Description
        //            },
        //            new Changes()
        //            {
        //                Op = "replace",
        //                Path = "/objects/ReceiptType/rows/"+unallocatedType.Id + "/attributes/BankAcct/value",
        //                Value = receiptType.BankAccountValue,
        //                Alias = receiptType.BankAccountDisplayName,
        //                Id = "BankAcct"
        //            },
        //            new Changes()
        //            {
        //                Op = "replace",
        //                Path = "/objects/ReceiptType/rows/"+unallocatedType.Id+"/attributes/ToleranceAmt/value",
        //                Value = receiptType.ToleranceAmount
        //            },
        //            new Changes()
        //            {
        //                Op = "replace",
        //                Path = "/objects/ReceiptType/rows/"+unallocatedType.Id+"/attributes/TolerancePct/value",
        //                Value = receiptType.TolerancePercentage
        //            },
        //            new Changes()
        //            {
        //                Op = "replace",
        //                Path = "/objects/ReceiptType/rows/"+unallocatedType.Id+"/attributes/CurrencyType/value",
        //                Value = receiptType.CurrencyTypeValue
        //            }

        //        }

        //    }, JsonHelper.Settings);
        //    return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        //}
    }
}
