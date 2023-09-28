using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ChargeType;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.QueryInfo;
using Elite3E.RestServices.Models.ResponseModels.Session;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class ChargeTypeTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public ILookUpService _lookUpService = new LookUpService();
        public IRestResponse _response;
        public IChargeTypeService _chargeTypeService = new ChargeTypeService();

        [Test]
        public async Task ChargeTypeTestRunner()
        {
            // code for loading csv file

            //loping thorugh
            var chargeTypeEntity = new ApiChargeTypeEntity()
            {
                ChargeCode = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                Description = "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                CategoryInput = "Billed on Account",
                TransactionTypeAlias = "Anticipated Hard Cost",
                Active = "Yes"
            };

            ChargeTypeTest test = new ChargeTypeTest();
            if (!await test.DoesChargeTypeExist(chargeTypeEntity.Description.String))
            {
                await test.CreateChargeTypeTask(chargeTypeEntity);
            }
        }

        public async Task<bool> DoesChargeTypeExist(string chargeTypeDesc)
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ChargeTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Serach for the ChargeType 
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, chargeTypeDesc);

            bool isFound = (_response.Content.Length > 2) ? true : false;
            Console.WriteLine("Is The Given Charge Type ("+ chargeTypeDesc + ") Found : " + isFound);

            _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();

            return isFound;
        }

        public async Task CreateChargeTypeTask(ApiChargeTypeEntity chargeTypeEntity)
        {
            chargeTypeEntity.Active = resolveActive(chargeTypeEntity.Active.String);

            //Start Session
            _response = await _session.GetSessionResponseAsync();

            //Get Session ID
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ChargeTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Add New
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            //Get ChargeType ID for Data Input
            var chargeTypeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            chargeTypeId.Should().NotBeNull();
            Console.WriteLine("chargeTypeId: " + chargeTypeId);
            chargeTypeEntity.ChargeTypeId = chargeTypeId;

            //Perform Lookup for Category (Dropdown)
            chargeTypeEntity.CategoryValue = await LookUp.GetLookUpKeyValue(sessionId, "ChrgCatList", chargeTypeEntity.CategoryInput);

            //Perform Lookup for Transaction Type (Search)
            // Part 1 - Perform Quick Info Request
            _response = await _chargeTypeService.GetQueryInfoResponse(sessionId,processItemId,chargeTypeEntity);
            var queryInfoResponse = JsonConvert.DeserializeObject<QueryInfoResponseModel>(_response.Content);
            queryInfoResponse.Should().NotBeNull();

            // Part 2 - Perform Quick Search
            _response =  await _chargeTypeService.GetLookupSearchTransactionType(sessionId, processItemId, chargeTypeEntity);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            chargeTypeEntity.TransactionTypeValue = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(chargeTypeEntity.TransactionTypeAlias)).Attributes.Code;
            chargeTypeEntity.TransactionTypeValue.String.Should().NotBeNullOrEmpty();

            //Fill in ChargeType Data
            _response = await _chargeTypeService.AddChargeTypeAsync(sessionId, processItemId, chargeTypeEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeProcess);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted ChargeTypeCode : " + chargeTypeEntity.ChargeCode.String);
            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);
        }

        private static string resolveActive(string EntityActive)
        {
            string activeResult = null;
            EntityActive = EntityActive.ToLower();
            
            if(EntityActive.Equals("0") || EntityActive.Equals("1"))
            {
                activeResult = EntityActive;
            }
            else
            {
                activeResult = (EntityActive.Equals("yes")) ? "1" : "0";
            }
            return activeResult;
        }
    }
}
