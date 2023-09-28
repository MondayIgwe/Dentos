using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.DisbursementEntry;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class DisbursementEntryData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public IDisbursementEntry _disbursmentEntryService = new DisbursementEntry();


        public async Task<bool> CreateDisbursementEntryAsync(ApiDisbursementEntryEntity disbursementEntry)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.DisbursmentEntry);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.DisbursmentEntry);
            _response.IsSuccessful.Should().BeTrue();

            var disbursementId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;

            AssertionExtensions.Should((string)disbursementId).NotBeNull();
            Console.WriteLine("Time Entry Id: " + disbursementId);
            disbursementEntry.Id = disbursementId;

            // Get matter Details 
            _response = await _disbursmentEntryService.GetMatterDeatilsAsync(sessionId, processItemId, disbursementEntry);
            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            disbursementEntry.MatterRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Number.Equals(disbursementEntry.MatterNumber)).RowKey;

            // Add Matter to ChargeEntry
            _response = await _disbursmentEntryService.AddMatterAsync(sessionId, processItemId, disbursementEntry);
            _response.IsSuccessful.Should().BeTrue();

            // Get Disbursement Type
            var disbursementType = new ApiDisbursementTypeEntity()
            {
                Description = disbursementEntry.DisbursementType
            };
            disbursementEntry.DisbursementType = await new CostTypeData().SearchAndCreateHardDisbursmentTypeDataAsync(disbursementType);

            _response = await _disbursmentEntryService.GetDisbursementTypeAsync(sessionId, processItemId, disbursementEntry);
            _response.IsSuccessful.Should().BeTrue();

            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            disbursementEntry.DisbursementTypeCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(disbursementEntry.DisbursementType)).Attributes.Code;

            _response = await _disbursmentEntryService.AddDisbursementTypeAsync(sessionId, processItemId, disbursementEntry);
            _response.IsSuccessful.Should().BeTrue();

            //Add curency
            disbursementEntry.CurrencyCode = await LookUp.GetCurrencyLookUpKeyValue(sessionId, disbursementEntry.Currency);
            _response = await _disbursmentEntryService.AddCurrencyAsync(sessionId, processItemId, disbursementEntry);
            _response.IsSuccessful.Should().BeTrue();

            // Work Rate

            _response = await _disbursmentEntryService.AddWorkRateAsync(sessionId, processItemId, disbursementEntry);
            _response.IsSuccessful.Should().BeTrue();

            // Tax code 

            if (!string.IsNullOrEmpty(disbursementEntry.TaxCodeDescription))
            {

                _response = await _disbursmentEntryService.GetTaxCodeDeatilsAsync(sessionId, processItemId, disbursementEntry);
                _response.IsSuccessful.Should().BeTrue();

                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

                disbursementEntry.Taxcode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(disbursementEntry.TaxCodeDescription)).Attributes.Code;

                // Add TaxCode to disbursement Entry
                _response = await _disbursmentEntryService.AddTaxCodeAsync(sessionId, processItemId, disbursementEntry);
                _response.IsSuccessful.Should().BeTrue();
            }

            /// Add narrative 
            _response = await _disbursmentEntryService.AddNarrativeAsync(sessionId, processItemId, disbursementEntry);

            _response.IsSuccessful.Should().BeTrue();            

            _response = await _process.PostAllProcessAsync(sessionId, processItemId, ApiConstants.DisbursmentEntry);
            _response.Content.Should().Contain("responseType\":1");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Disbursement Entry has been posted");
                return true;
            }

            Console.WriteLine("Failed To submit Disbursement Entry");
            return false;
        }

    }
}
