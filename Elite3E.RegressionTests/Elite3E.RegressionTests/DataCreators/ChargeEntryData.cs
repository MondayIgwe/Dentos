using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ChargeEntry;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ChargeEntryData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public IChargeEntryService _chargeEntryService = new ChargeEntryService();


        public async Task<bool> CreateChargeEntryAsync(ApiChargeEntryEntity chargeEntry)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ChargeEntry);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ChargeEntry);
            _response.IsSuccessful.Should().BeTrue();

            var chargeEntryId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;

            AssertionExtensions.Should((string)chargeEntryId).NotBeNull();
            Console.WriteLine("Time Entry Id: " + chargeEntryId);
            chargeEntry.Id = chargeEntryId;

            // Get matter Details 
            _response = await _chargeEntryService.GetMatterDeatilsAsync(sessionId, processItemId, chargeEntry);

            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            chargeEntry.MatterRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Number.Equals(chargeEntry.MatterNumber)).RowKey;

            // Add Matter to ChargeEntry
            _response = await _chargeEntryService.AddMatterAsync(sessionId, processItemId, chargeEntry);

            _response.IsSuccessful.Should().BeTrue();

            // Get charge type
            chargeEntry.ChargeType = await new ChargeTypeData().SerchAndCreateChargeTypeDataAsync(chargeEntry.ChargeType);

            var parameter = $"/objects/ChrgCardPending/rows/{chargeEntry.Id}/attributes/ChrgType";
            chargeEntry.ChargeTypeCode = await LookUp.GetChildLookUpWithParameterAsync(sessionId, processItemId, ApiConstants.ChargeType, chargeEntry.ChargeType, parameter);

            _response = await _chargeEntryService.AddChargeTypeAsync(sessionId, processItemId, chargeEntry);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _chargeEntryService.AddAmountAsync(sessionId, processItemId, chargeEntry);
            _response.IsSuccessful.Should().BeTrue();

            // Tax code 

            _response = await _chargeEntryService.GetTaxCodeDeatilsAsync(sessionId, processItemId, chargeEntry);

            _response.IsSuccessful.Should().BeTrue();

            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            chargeEntry.Taxcode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(chargeEntry.TaxCodeDescription)).Attributes.Code;

            // Add TaxCode to Charge Entry
            _response = await _chargeEntryService.AddTaxCodeAsync(sessionId, processItemId, chargeEntry);

            _response.IsSuccessful.Should().BeTrue();

            /// Add narrative 
            _response = await _chargeEntryService.AddNarrativeAsync(sessionId, processItemId, chargeEntry);

            _response.IsSuccessful.Should().BeTrue();

            //Add curency
            chargeEntry.CurrencyCode = await LookUp.GetCurrencyLookUpKeyValue(sessionId, chargeEntry.Currency);
            _response = await _chargeEntryService.AddCurrencyAsync(sessionId, processItemId, chargeEntry);

            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostAllProcessAsync(sessionId, processItemId, ApiConstants.ChargeEntry);
            _response.Content.Should().Contain("responseType\":1");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Charge Entry has been posted");
                return true;
            }

            Console.WriteLine("Failed To submit Charge Entry");
            return false;
        }

    }
}
