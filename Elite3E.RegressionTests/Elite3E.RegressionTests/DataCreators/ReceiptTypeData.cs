using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ReceiptType;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ReceiptTypeData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IReceiptTypeService _receiptTypeService = new ReceiptTypeService();
        private ILookUpService _lookUpService = new LookUpService();
        public IRestResponse _response;

        public async Task<string> ReceiptTypeAsync(ApiReceiptTypeEntity receiptType)
        {

            receiptType.Code = (string.IsNullOrEmpty(receiptType.Code.String)) ? "RType_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+6") : receiptType.Code;
            receiptType.Description = (string.IsNullOrEmpty(receiptType.Description.String)) ? "Receipt_Type_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : receiptType.Description;
            receiptType.ToleranceAmount = (string.IsNullOrEmpty(receiptType.ToleranceAmount)) ? "100.00" : receiptType.ToleranceAmount; ;
            receiptType.TolerancePercentage = (string.IsNullOrEmpty(receiptType.TolerancePercentage)) ? "100.00" : receiptType.TolerancePercentage; ;         
           
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ReceiptTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ReceiptTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            receiptType.Id = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            receiptType.Id.Should().NotBeNull();
            Console.WriteLine("RateTypeId: " + receiptType.Id);

            // To check the update operating Unit  - This needs to be implemented
            //if (!string.IsNullOrEmpty(receiptType.OperatingUnit))
            //{

            //}
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, receiptType.Description.String);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Recepit Type Description Exists : " + receiptType.Description.String);
                return receiptType.Description.String;
            }


            _response = await _receiptTypeService.GetReceiptTypeBankAccountAsync(sessionId, processItemId, receiptType);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            quickSearch.Rows.Should().NotBeNull();

            // to make sure the Bank Account Display Name is selected from list accross the environments unless specified 

            receiptType.BankAccountDisplayName = (string.IsNullOrEmpty(receiptType.BankAccountDisplayName)) ? quickSearch.Rows.FirstOrDefault().Alias : receiptType.BankAccountDisplayName;

            receiptType.BankAccountValue = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(receiptType.BankAccountDisplayName)).RowKey;

            // to make sure the value is selected from list accross the environments unless specified
            receiptType.CurrencyTypeValue = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "CurrencyType", receiptType.CurrencyTypeDescription);

            _response = await _receiptTypeService.CreateReceiptTypeAsync(sessionId, processItemId, receiptType);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ReceiptTypeProcess);
            _response.Content.Should().Contain("responseType\":1");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Receipt Type Description : " + receiptType.Description.String);
                return receiptType.Description.String;
            }

            Console.WriteLine("Creation Api Receipt type failed: " + _response.StatusCode);
            return null;

        }
    }
}
