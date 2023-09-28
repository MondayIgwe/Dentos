using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.TimeType;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class TimeTypeData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public ILookUpService _lookUpService = new LookUpService();
        public ITimeTypeService _timeTypeService = new TimeTypeService();

        public async Task<string> CreateTimeTypeAsync(ApiTimeTypeEntity timeType)
        {
            timeType.Currency = (string.IsNullOrEmpty(timeType.Currency)) ? "GBP - British Pound" : timeType.Currency;
            timeType.TransactionType = (string.IsNullOrEmpty(timeType.TransactionType)) ? "Fixed - Capped Fees" : timeType.TransactionType;
            timeType.Description = (string.IsNullOrEmpty(timeType.Description)) ? "Fixed-Capped Fees":   timeType.Description;
            timeType.Code  = (string.IsNullOrEmpty(timeType.Code)) ? "FIXED_CAP FEES" : timeType.Code;

            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.TimeType);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, timeType.Description);
            var existingTimeType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingTimeType.Rows != null)
            {
                if (existingTimeType.Rows.FirstOrDefault().Attributes.Description.Equals(timeType.Description))
                {
                    _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                    _response.IsSuccessful.Should().BeTrue();

                    Console.WriteLine("The Given Time Type Code Exists : " + timeType.Description);
                    return timeType.Description;
                }
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.TimeType);
            _response.IsSuccessful.Should().BeTrue();

            var timeTypeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;

            AssertionExtensions.Should((string)timeTypeId).NotBeNull();
            Console.WriteLine("Time Type Id: " + timeTypeId);
            timeType.Id = timeTypeId;


            //Get Transaction Type
            _response = await _timeTypeService.GetTransactionTypeDeatilsAsync(sessionId, processItemId, timeType);

            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            timeType.TransactionTypeCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(timeType.TransactionType)).Attributes.Code;

            timeType.CurrencyCode = await LookUp.GetCurrencyLookUpKeyValue(sessionId, timeType.Currency);

            _response = await _timeTypeService.AddTimeTypeDeatilsAsync(sessionId, processItemId, timeType);

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.TimeType);
            _response.Content.Should().Contain("responseType\":1");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Time type has been created");
                return timeType.Description;
            }

            Console.WriteLine("Failed To submit Time Type");
            return null;

        }
    }
}
