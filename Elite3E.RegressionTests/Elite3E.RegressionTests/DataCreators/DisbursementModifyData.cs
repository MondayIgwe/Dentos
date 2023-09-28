using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.DisbursementModify;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class DisbursementModifyData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public IDisbursementModify _disbursmentEntryService = new DisbursementModify();


        public async Task CreateDisbursementModifyAsync(ApiDisbursementModifyEntity disbursementModify)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.DisbursementModify);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.Disbursement);
            _response.IsSuccessful.Should().BeTrue();

            var disbursementId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges
                .FirstOrDefault().Value.String;

            AssertionExtensions.Should((string) disbursementId).NotBeNull();
            Console.WriteLine("Time Entry Id: " + disbursementId);
            disbursementModify.Id = disbursementId;

            //Inputting Start Date
            if (!string.IsNullOrEmpty(disbursementModify.WorkDate))
            {
                //Ensure Format is: 2022-02-17 (yyyy-MM-dd)

                disbursementModify.WorkDate = DateTime
                    .Parse(disbursementModify.WorkDate, new CultureInfo("en-US", true)).ToString("d/M/yyyy");
                _response = await _disbursmentEntryService.AddWorkDateAsync(sessionId, processItemId,
                    disbursementModify);
                _response.IsSuccessful.Should().BeTrue();
            }

            if (!string.IsNullOrEmpty(disbursementModify.WorkQuantity))
            {
                _response = await _disbursmentEntryService.AddWorkQuantityAsync(sessionId, processItemId,
                    disbursementModify);
                _response.IsSuccessful.Should().BeTrue();
            }

            // Get matter Details 
            _response = await _disbursmentEntryService.GetMatterDeatilsAsync(sessionId, processItemId,
                disbursementModify);
            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            disbursementModify.MatterRowKey = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.Number.Equals(disbursementModify.MatterNumber)).RowKey;

            // Add Matter to ChargeEntry
            _response = await _disbursmentEntryService.AddMatterAsync(sessionId, processItemId, disbursementModify);
            _response.IsSuccessful.Should().BeTrue();

            // Get Disbursement Type

            if (string.IsNullOrEmpty(disbursementModify.DisbursementTypeCode.String))
            {
                var disbursementType = new ApiDisbursementTypeEntity()
                {
                    Description = disbursementModify.DisbursementType
                };
                disbursementModify.DisbursementType =
                    await new CostTypeData().SearchAndCreateHardDisbursmentTypeDataAsync(disbursementType);

                _response = await _disbursmentEntryService.GetDisbursementTypeAsync(sessionId, processItemId,
                    disbursementModify);
                _response.IsSuccessful.Should().BeTrue();

                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

                disbursementModify.DisbursementTypeCode = quickSearch.Rows
                    .FirstOrDefault(value => value.Attributes.Description.Equals(disbursementModify.DisbursementType))
                    .Attributes.Code;

            }
            else
            {
                _response = await _disbursmentEntryService.GetDisbursementTypeAsync(sessionId, processItemId,
                    disbursementModify);
                _response.IsSuccessful.Should().BeTrue();
                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                disbursementModify.DisbursementType = quickSearch?.Rows
                    .FirstOrDefault(value =>
                        value.Attributes.Code.Equals(disbursementModify.DisbursementTypeCode.String))
                    ?.Attributes.Description;
            }

            _response = await _disbursmentEntryService.AddDisbursementTypeAsync(sessionId, processItemId,
                disbursementModify);
            _response.IsSuccessful.Should().BeTrue();

            if (!string.IsNullOrEmpty(disbursementModify.Currency))
            {
                //Add curency
                disbursementModify.CurrencyCode =
                    await LookUp.GetCurrencyLookUpKeyValue(sessionId, disbursementModify.Currency);
                _response = await _disbursmentEntryService.AddCurrencyAsync(sessionId, processItemId,
                    disbursementModify);
                _response.IsSuccessful.Should().BeTrue();
            }

            // Work Rate
            if (!string.IsNullOrEmpty(disbursementModify.WorkRate))
            {
                _response = await _disbursmentEntryService.AddWorkRateAsync(sessionId, processItemId,
                    disbursementModify);
                _response.IsSuccessful.Should().BeTrue();
            }

            // Tax code 

        if (!string.IsNullOrEmpty(disbursementModify.TaxCodeDescription))
            {

                _response = await _disbursmentEntryService.GetTaxCodeDeatilsAsync(sessionId, processItemId, disbursementModify);
                _response.IsSuccessful.Should().BeTrue();

                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

                disbursementModify.Taxcode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(disbursementModify.TaxCodeDescription)).Attributes.Code;

                // Add TaxCode to disbursement Entry
                _response = await _disbursmentEntryService.AddTaxCodeAsync(sessionId, processItemId, disbursementModify);
                _response.IsSuccessful.Should().BeTrue();
            }

            /// Add narrative 
            _response = await _disbursmentEntryService.AddNarrativeAsync(sessionId, processItemId, disbursementModify);

            _response.IsSuccessful.Should().BeTrue();            

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.DisbursementModify);
            _response.Content.Should().Contain("responseType\":1");

            Console.WriteLine(_response.IsSuccessful
                ? "Disbursement Modify has been posted"
                : "Failed To submit Disbursement Modify");
        }

    }
}
