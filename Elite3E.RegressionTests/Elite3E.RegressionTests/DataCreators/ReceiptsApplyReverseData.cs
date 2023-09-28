using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ReceiptApplyReverse;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ReceiptsApplyReverseData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public ILookUpService _lookUpService = new LookUpService();
        public IReceiptsApplyReverseService _receiptAppRevService = new ReceiptsApplyReverseService();


        public async Task<ApiReceiptsApplyReverseEntity> AddReceiptWithInvoice(ApiReceiptsApplyReverseEntity receiptAppRevEntity)
        {
            //Get Session ID
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Open Process
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ReceiptsApplyReversePayments);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            // Search for Receipt/Apply Reverse 
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, receiptAppRevEntity.DocumentNumber);
            var quickSearchResult = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (quickSearchResult.Rows != null && quickSearchResult.Rows.Any(x => x.Attributes.BaseUserName.Equals(receiptAppRevEntity.DocumentNumber)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Receipt Apply/Reverse  Exists : " + receiptAppRevEntity.InvoiceNumber);
            }

            //Add new Reverse/Apply Receipt
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ReceiptsApplyReversePayments);
            _response.IsSuccessful.Should().BeTrue();

            //Get Reverse/Apply Receipt Id
            var receiptApplyReverseId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            receiptApplyReverseId.Should().NotBeNull();
            Console.WriteLine("clientAccountReceiptId: " + receiptApplyReverseId);
            receiptAppRevEntity.Id = receiptApplyReverseId;

            //Get the Receipt Type
            _response = await _receiptAppRevService.GetReceiptTypeAsync(sessionId, processItemId, receiptAppRevEntity);
            _response.IsSuccessful.Should().BeTrue();
            var receiptApplyReverseSearchRow = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            receiptAppRevEntity.ReceiptTypeValue = receiptApplyReverseSearchRow.Rows.FirstOrDefault(value => value.Attributes.Code.Equals(receiptAppRevEntity.ReceiptTypeAlias)).RowKey;
            receiptAppRevEntity.ReceiptTypeValue.Should().NotBeNullOrEmpty();

            //Add Receipt Type
            _response = await _receiptAppRevService.AddReceiptsReceiptTypeDataAsync(sessionId, processItemId, receiptAppRevEntity);
            _response.IsSuccessful.Should().BeTrue();

            //Add Narrative and Document Number
            _response = await _receiptAppRevService.AddReceiptsApplyReverseDataAsync(sessionId, processItemId, receiptAppRevEntity);
            _response.IsSuccessful.Should().BeTrue();

            //Child Form data - For Invoice
            _response = await _receiptAppRevService.AddChildFormAsync(sessionId, processItemId, receiptAppRevEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _receiptAppRevService.GetInvoiceKeyAsync(sessionId, processItemId, receiptAppRevEntity);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            receiptAppRevEntity.InvoiceNumber = quickSearch.Rows.FirstOrDefault().Attributes.InvoiceNumber;
            var invoiceRow = quickSearch.Rows.FirstOrDefault();
            invoiceRow.Should().NotBeNull();

            receiptAppRevEntity.InvoiceKey = new List<Guid>();
            receiptAppRevEntity.InvoiceKey.Add(new Guid(invoiceRow.RowKey));

            receiptAppRevEntity.ReceiptAmount = (string.IsNullOrEmpty(receiptAppRevEntity.ReceiptAmount)) ? invoiceRow.Attributes.BalanceAmount : receiptAppRevEntity.ReceiptAmount;

            _response = await _receiptAppRevService.AddInvoiceAsync(sessionId, processItemId, receiptAppRevEntity);
            _response.IsSuccessful.Should().BeTrue();

            //Add Receipt Amount
            _response = await _receiptAppRevService.AddReceiptsReceiptAmountDataAsync(sessionId, processItemId, receiptAppRevEntity);
            _response.IsSuccessful.Should().BeTrue();
            //Submission


            _response = await _receiptAppRevService.PostUpdateReceiptAsync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();

            if (_response.Content.Contains("does not match Cash GL Account", StringComparison.CurrentCultureIgnoreCase))
            {
                var updateResponse = JsonHelper.JsonReaderChecker(_response.Content, "message", 1);
                string unitToChangeTo = Regex.Replace(updateResponse, "(.*:)", "");
                //Alternate Regex for finding value: [0-9]+$

                await UpdateFormUnit.ChangeFormUnitAsync(sessionId, processItemId, unitToChangeTo);
            }

            //Submit here
            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ReceiptsApplyReversePayments);
            _response.Content.Should().Contain("responseType\":1", "Failed To submit Receipt Creation");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Receipt has been Created" + receiptAppRevEntity.InvoiceNumber);
                return receiptAppRevEntity;
            }

            return receiptAppRevEntity;
        }
    }
}
