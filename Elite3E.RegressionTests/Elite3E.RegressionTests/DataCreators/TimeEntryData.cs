using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.TimeEntry;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public  class TimeEntryData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public ILookUpService _lookUpService = new LookUpService();
        public ITimeEntryService _timeEntryService = new TimeEntryService();


        public async Task<bool> CreateTimeEntryAsync(ApiTimeEntryEntity timeEntry)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.TimeEntry);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.TimeEntry);
            _response.IsSuccessful.Should().BeTrue();

            var timeEntryId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
           
            AssertionExtensions.Should((string)timeEntryId).NotBeNull();
            Console.WriteLine("Time Entry Id: " + timeEntryId);
            timeEntry.Id = timeEntryId;

            //Validate fee Eraner

            var feeEarnerDataEntity = new ApiFeeEarnerEntity();
            feeEarnerDataEntity.EntityName = timeEntry.FeeEranerName;
            timeEntry.FeeEarnerId = await FeeEarnerData.GetFeeEarnerNumber(feeEarnerDataEntity);

            _response = await _timeEntryService.GetFeeEranerDeatilsAsync(sessionId, processItemId, timeEntry);

            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            timeEntry.FeeEranerRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Number.Equals(timeEntry.FeeEarnerId)).RowKey;

            // Add FeeEraner to TimeEntry
            _response = await _timeEntryService.AddFeeEranerAsync(sessionId, processItemId, timeEntry);

            _response.IsSuccessful.Should().BeTrue();

            // Get matter Details 
            _response = await _timeEntryService.GetMatterDeatilsAsync(sessionId, processItemId, timeEntry);

            _response.IsSuccessful.Should().BeTrue();

            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            timeEntry.MatterRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Number.Equals(timeEntry.MatterNumber)).RowKey;

            // Add Matter to TimeEntry
            _response = await _timeEntryService.AddMatterAsync(sessionId, processItemId, timeEntry);

            _response.IsSuccessful.Should().BeTrue();

            // Get Time type
            var timeType = new ApiTimeTypeEntity();
            timeType.Description = timeEntry.TimeType;

            timeEntry.TimeType = await new TimeTypeData().CreateTimeTypeAsync(timeType);

            var parameter = $"/objects/TimeCardPending/rows/{timeEntry.Id}/attributes/TimeType";
            timeEntry.TimeTypeCode = await LookUp.GetChildLookUpWithParameterAsync(sessionId, processItemId, ApiConstants.TimeType, timeEntry.TimeType, parameter);

            // Add  time Type 
            _response = await _timeEntryService.AddTimeTypeAsync(sessionId, processItemId, timeEntry);

            _response.IsSuccessful.Should().BeTrue();

            // Tax code 

            _response = await _timeEntryService.GetTaxCodeDeatilsAsync(sessionId, processItemId, timeEntry);

            _response.IsSuccessful.Should().BeTrue();

            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            timeEntry.Taxcode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(timeEntry.TaxCodeDescription)).Attributes.Code;

            // Add TaxCode to TimeEntry
            _response = await _timeEntryService.AddTaxCodeAsync(sessionId, processItemId, timeEntry);

            _response.IsSuccessful.Should().BeTrue();

            /// Add narrative 
            _response = await _timeEntryService.AddNarrativeAsync(sessionId, processItemId, timeEntry);

            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostAllProcessAsync(sessionId, processItemId, ApiConstants.TimeEntry);
            _response.Content.Should().Contain("responseType\":1");
            
            if (_response.IsSuccessful)
            {
                Console.WriteLine("TimeEntry has been posted");
                return true;
            }

            Console.WriteLine("Failed To submit Time Entry");
            return false;
        }
    }
}
