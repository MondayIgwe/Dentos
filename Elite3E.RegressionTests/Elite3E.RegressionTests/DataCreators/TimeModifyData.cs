using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.TimeModify;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public  class TimeModifyData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public ILookUpService _lookUpService = new LookUpService();
        public ITimeModifyService _timeModifyService = new TimeModifyService();


        public async Task CreateTimeModifyAsync(ApiTimeModifyEntity timeModify)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.TimeModify);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.TimeCard);
            _response.IsSuccessful.Should().BeTrue();

            var timeModifyId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
           
            AssertionExtensions.Should((string)timeModifyId).NotBeNull();
            Console.WriteLine("Time Modify Id: " + timeModifyId);
            timeModify.Id = timeModifyId;

            //Inputting Start Date
            if (!string.IsNullOrEmpty(timeModify.WorkDate))
            {
                //Ensure Format is: 2022-02-17 (yyyy-MM-dd)

                timeModify.WorkDate = DateTime.Parse(timeModify.WorkDate, new CultureInfo("en-US", true)).ToString("d/M/yyyy");
                _response = await _timeModifyService.AddWorkDateAsync(sessionId, processItemId, timeModify);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Validate fee Earner -  For bulk import we are supplying the fee earner id 
            if (string.IsNullOrEmpty(timeModify.FeeEarnerId))
            {
                var feeEarnerDataEntity = new ApiFeeEarnerEntity();
                feeEarnerDataEntity.EntityName = timeModify.FeeEranerName;
                timeModify.FeeEarnerId = await FeeEarnerData.GetFeeEarnerNumber(feeEarnerDataEntity);
            }

            _response = await _timeModifyService.GetFeeEranerDeatilsAsync(sessionId, processItemId, timeModify);

            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            timeModify.FeeEranerRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Number.Equals(timeModify.FeeEarnerId)).RowKey;

            // Add FeeEraner to TimeModify
            _response = await _timeModifyService.AddFeeEranerAsync(sessionId, processItemId, timeModify);

            _response.IsSuccessful.Should().BeTrue();

            // Get matter Details 
            _response = await _timeModifyService.GetMatterDeatilsAsync(sessionId, processItemId, timeModify);

            _response.IsSuccessful.Should().BeTrue();

            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            timeModify.MatterRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Number.Equals(timeModify.MatterNumber)).RowKey;

            // Add Matter to TimeModify
            _response = await _timeModifyService.AddMatterAsync(sessionId, processItemId, timeModify);

            _response.IsSuccessful.Should().BeTrue();

            // Get Time type -- Remove this  ?
            //var timeType = new ApiTimeTypeEntity();
            //timeType.Description = timeModify.TimeType;

            //timeModify.TimeType = await new TimeTypeData().CreateTimeTypeAsync(timeType);

            var parameter = $"/objects/TimeCard/rows/{timeModify.Id}/attributes/TimeType";
            timeModify.TimeTypeCode = await LookUp.GetChildLookUpWithParameterAsync(sessionId, processItemId, ApiConstants.TimeType, timeModify.TimeType, parameter);

            // Add  time Type 
            _response = await _timeModifyService.AddTimeTypeAsync(sessionId, processItemId, timeModify);
            _response.IsSuccessful.Should().BeTrue();


            /// Add narrative 
            _response = await _timeModifyService.AddNarrativeAsync(sessionId, processItemId, timeModify);
            _response.IsSuccessful.Should().BeTrue();

            // Add working Hours
            if (!string.IsNullOrEmpty(timeModify.WorkHours))
            {
                _response = await _timeModifyService.AddWorkingHoursAsync(sessionId, processItemId, timeModify);
                _response.IsSuccessful.Should().BeTrue();
            }

            if (!string.IsNullOrEmpty(timeModify.TaxCodeDescription))
            {
                // Tax code 
                _response = await _timeModifyService.GetTaxCodeDeatilsAsync(sessionId, processItemId, timeModify);
                _response.IsSuccessful.Should().BeTrue();

                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                timeModify.Taxcode = quickSearch.Rows
                    .FirstOrDefault(value => value.Attributes.Description.Equals(timeModify.TaxCodeDescription))
                    .Attributes.Code;

                // Add TaxCode to TimeModify
                _response = await _timeModifyService.AddTaxCodeAsync(sessionId, processItemId, timeModify);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Add Currency
            if (!string.IsNullOrEmpty(timeModify.Currency))
            {
                _response = await _timeModifyService.AddCurrencyAsync(sessionId, processItemId, timeModify);
                _response.IsSuccessful.Should().BeTrue();
            }

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.TimeModify);
            _response.Content.Should().Contain("responseType\":1");

            Console.WriteLine(_response.IsSuccessful ? "TimeModify has been posted" : "Failed To submit Time Modify");
        }
    }
}
