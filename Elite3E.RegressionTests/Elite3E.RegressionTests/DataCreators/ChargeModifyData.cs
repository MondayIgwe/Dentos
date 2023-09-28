using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ChargeEntry;
using Elite3E.RestServices.Services.ChargeModify;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ChargeModifyData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public IChargeModifyService _chargeModifyData = new ChargeModifyService();


        public async Task<bool> CreateChargeModifyAsync(ApiChargeModifyEntity chargeModify)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ChargeModify);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ChargeCard);
            _response.IsSuccessful.Should().BeTrue();

            var chargeModifyId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;

            AssertionExtensions.Should((string)chargeModifyId).NotBeNull();
            Console.WriteLine("Time Entry Id: " + chargeModifyId);
            chargeModify.Id = chargeModifyId;

            // Get matter Details 
            _response = await _chargeModifyData.GetMatterDeatilsAsync(sessionId, processItemId, chargeModify);

            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            chargeModify.MatterRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Number.Equals(chargeModify.MatterNumber)).RowKey;

            // Add Matter to ChargeEntry
            _response = await _chargeModifyData.AddMatterAsync(sessionId, processItemId, chargeModify);

            _response.IsSuccessful.Should().BeTrue();

            // Get charge type

            chargeModify.ChargeType = await new ChargeTypeData().SerchAndCreateChargeTypeDataAsync(chargeModify.ChargeType);

            var parameter = $"/objects/ChrgCard/rows/{chargeModify.Id}/attributes/ChrgType";
            chargeModify.ChargeTypeCode = await LookUp.GetChildLookUpWithParameterAsync(sessionId, processItemId, ApiConstants.ChargeType, chargeModify.ChargeType, parameter);

            _response = await _chargeModifyData.AddChargeTypeAsync(sessionId, processItemId, chargeModify);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _chargeModifyData.AddAmountAsync(sessionId, processItemId, chargeModify);
            _response.IsSuccessful.Should().BeTrue();

            // Tax code 

            _response = await _chargeModifyData.GetTaxCodeDeatilsAsync(sessionId, processItemId, chargeModify);

            _response.IsSuccessful.Should().BeTrue();

            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            chargeModify.Taxcode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(chargeModify.TaxCodeDescription)).Attributes.Code;

            // Add TaxCode to Charge Entry
            _response = await _chargeModifyData.AddTaxCodeAsync(sessionId, processItemId, chargeModify);

            _response.IsSuccessful.Should().BeTrue();

            /// Add narrative 
            _response = await _chargeModifyData.AddNarrativeAsync(sessionId, processItemId, chargeModify);

            _response.IsSuccessful.Should().BeTrue();

            //Add curency
            chargeModify.CurrencyCode = await LookUp.GetCurrencyLookUpKeyValue(sessionId, chargeModify.Currency);
            _response = await _chargeModifyData.AddCurrencyAsync(sessionId, processItemId, chargeModify);

            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ChargeModify);
            _response.Content.Should().Contain("responseType\":1");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Charge Modify has been posted");
                return true;
            }

            Console.WriteLine("Failed To submit Charge Modify");
            return false;
        }

    }
}
