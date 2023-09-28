using Elite3E.Infrastructure.Extensions;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Services.UnallocatedType;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class UnallocatedTypeTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IUnallocatedTypeService _unallocatedTypeService = new UnallocatedTypeService();
        public IRestResponse _response;


        [Test]
        public async Task UnallocatedTypeTestAsync()
        {
            var unallocatedType = new ApiUnallocatedTypeEntity()
            {
                Code = "Receipt_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+4"),
                Description = "Receipt_Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                BankAccountDisplayName = "London UKME - HSBC Off 1 Acc - GBP",
                CurrencyTypeDescription = "Daily - Azerbaijan Central Bank",
                ToleranceAmount = "50.00",
                TolerancePercentage = "50.00"
            };

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.UnallocatedTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.UnallocatedTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            unallocatedType.Id = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            unallocatedType.Id.Should().NotBeNull();
            Console.WriteLine("RateTypeId: " + unallocatedType.Id);

            //_response = await _receiptTypeService.GetReceiptTypeBankAccountAsync(sessionId, processItemId, unallocatedType);
            //_response.IsSuccessful.Should().BeTrue();
            //var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            // receiptType.BankAccountValue = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(receiptType.BankAccountDisplayName)).RowKey;


            //receiptType.CurrencyTypeValue = await LookUp.GetLookUpKeyValue(sessionId, "CurrencyType", receiptType.CurrencyTypeDescription);

            _response = await _unallocatedTypeService.CreateUnallocatedTypeAsync(sessionId, processItemId, unallocatedType);
            _response.IsSuccessful.Should().BeTrue();

            var parmater = "/objects/UnalType/rows/"+unallocatedType.Id+"/childObjects/UnalTypePost/rows&index=0";



            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ReceiptTypeProcess);
            _response.Content.Should().Contain("responseType\":1");
            
            if(_response.IsSuccessful)
                Console.WriteLine("Fee earner Rate Type Code : " + unallocatedType.Description);
            else
                Console.WriteLine("test failed: " + _response.StatusCode);

        }


    }
}
