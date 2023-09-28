using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QueryInfo;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ChargeType;
using Elite3E.RestServices.Services.ChargeTypeGroup;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ChargeTypeData
    {
        public IProcessDataService _processDataService = new ProcessDataService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private ILookUpService _lookUpService = new LookUpService();
        public IChargeTypeGroupService _chargeTypeGroupService = new ChargeTypeGroupService();
        public IChargeTypeService _chargeTypeService = new ChargeTypeService();

        public async Task<string> SearchaAndCreateChargeTypeGroupDataAsync(string chargeTypeDescription)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ChargeTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, chargeTypeDescription);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Charge Type Description Exists : " + chargeTypeDescription);
                return chargeTypeDescription;
            }


            var chargeTypeGroupEntity = new ApiChargeTypeGroupEntity()
            {
                ChargeTypeGroupCode = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                ChargeTypeGroupDescription = chargeTypeDescription,
                ChargeTypeGroupExcludeOrIncludeListOption = ApiConstants.IsExcludeList,
                ChargeTypeGroupIsExcludeOrIncludeListValue = "1"
            };

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();

            var chargeTypeGroupId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            chargeTypeGroupId.Should().NotBeNull();
            Console.WriteLine("chargeTypeGroupId: " + chargeTypeGroupId);
            chargeTypeGroupEntity.ChargeTypeGroupId = chargeTypeGroupId;

            _response = await _chargeTypeGroupService.AddChargeTypeGroupAsync(sessionId, processItemId, chargeTypeGroupEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeGroupProcess);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted ChargeTypeGroupCode : " + chargeTypeGroupEntity.ChargeTypeGroupDescription.String);

                return chargeTypeGroupEntity.ChargeTypeGroupDescription.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);

            return null;
        }

        public async Task<string> SerchAndCreateChargeTypeDataAsync(string description)
        {
            var chargeTypeEntity = DefaultRegionalValues.GetChargeTypeDefaultValues(description);
            
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

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, chargeTypeEntity.Description.String);
            var existingChargeTypes = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);


            if (existingChargeTypes.Rows != null)
            {
                foreach (var existingChargeType in existingChargeTypes.Rows)
                {
                    if (existingChargeType.Attributes.Description.Equals(chargeTypeEntity.Description.String,
                            StringComparison.CurrentCultureIgnoreCase) || existingChargeType.Attributes.Code.Equals(chargeTypeEntity.ChargeCode.String,
                            StringComparison.CurrentCultureIgnoreCase))
                    {

                        //Storing ClientNumber for UI Test

                        _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                        _response.IsSuccessful.Should().BeTrue();

                        Console.WriteLine("The Given Charge Type Description Exists : " + chargeTypeEntity.Description.String);
                        return chargeTypeEntity.Description.String;
                    }
                }

            }

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
            _response = await _chargeTypeService.GetQueryInfoResponse(sessionId, processItemId, chargeTypeEntity);
            var queryInfoResponse = JsonConvert.DeserializeObject<QueryInfoResponseModel>(_response.Content);
            queryInfoResponse.Should().NotBeNull();

            // Part 2 - Perform Quick Search
            _response = await _chargeTypeService.GetLookupSearchTransactionType(sessionId, processItemId, chargeTypeEntity);
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
                Console.WriteLine("Submitted ChargeTypeCode : " + chargeTypeEntity.Description.String);
                return chargeTypeEntity.Description.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;

        }

        public async Task<string> SearchAndCreateChargeTypeDataAsync(ApiChargeTypeEntity chargeTypeEntity)
        {

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

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId,
                chargeTypeEntity.Description.String);

            var existingChargeType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingChargeType.Rows != null && 
                    existingChargeType.Rows.Any(s => s.Attributes.Description.Equals(chargeTypeEntity.Description.String, StringComparison.CurrentCultureIgnoreCase) 
                    || s.Attributes.Code.Equals(chargeTypeEntity.ChargeCode.String, StringComparison.CurrentCultureIgnoreCase)))
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Charge Type Description Exists : " +
                                    chargeTypeEntity.Description.String);
                return chargeTypeEntity.Description.String;
            }

            //Add New
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            //Get ChargeType ID for Data Input
            var chargeTypeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges
                .FirstOrDefault().Value.String;
            chargeTypeId.Should().NotBeNull();
            Console.WriteLine("chargeTypeId: " + chargeTypeId);
            chargeTypeEntity.ChargeTypeId = chargeTypeId;

            //Perform Lookup for Category (Dropdown)
            chargeTypeEntity.CategoryValue =
                await LookUp.GetLookUpKeyValue(sessionId, "ChrgCatList", chargeTypeEntity.CategoryInput);

            //Perform Lookup for Transaction Type (Search)
            // Part 1 - Perform Quick Info Request
            _response = await _chargeTypeService.GetQueryInfoResponse(sessionId, processItemId, chargeTypeEntity);
            var queryInfoResponse = JsonConvert.DeserializeObject<QueryInfoResponseModel>(_response.Content);
            queryInfoResponse.Should().NotBeNull();

            // Part 2 - Perform Quick Search
            _response = await _chargeTypeService.GetLookupSearchTransactionType(sessionId, processItemId,
                chargeTypeEntity);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            quickSearch.Rows.Should().NotBeNull();
            chargeTypeEntity.TransactionTypeValue = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.Description.Equals(chargeTypeEntity.TransactionTypeAlias))
                .Attributes.Code;
            chargeTypeEntity.TransactionTypeValue.String.Should().NotBeNullOrEmpty();

            // Fill Hard Cost or Soft Cost

            //Fill in ChargeType Data
            _response = await _chargeTypeService.AddChargeTypeAsync(sessionId, processItemId, chargeTypeEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId,
                ApiConstants.ChargeTypeProcess);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted ChargeTypeCode : " + chargeTypeEntity.Description.String);
                return chargeTypeEntity.Description.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }

        public async Task<string> SearchChargeTypeGroupAndAddChargeTypeDataAsync(ApiChargeTypeGroupEntity chargeTypeGroupEntity,
            List<ApiChargeTypeEntity> chargeTypeList)
        {
            if (chargeTypeList != null)
            {
                foreach (var chargeType in chargeTypeList)
                {
                    await SearchAndCreateChargeTypeDataAsync(chargeType);
                }
            }

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ChargeTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, chargeTypeGroupEntity.ChargeTypeGroupDescription.String);

            if (_response.Content.Length == 2)
            {
                _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeGroupProcess);
                _response.IsSuccessful.Should().BeTrue();

                var chargeTypeGroupId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
                chargeTypeGroupId.Should().NotBeNull();
                Console.WriteLine("chargeTypeGroupId: " + chargeTypeGroupId);
                chargeTypeGroupEntity.ChargeTypeGroupId = chargeTypeGroupId;

                _response = await _chargeTypeGroupService.AddChargeTypeGroupAsync(sessionId, processItemId, chargeTypeGroupEntity);
                _response.IsSuccessful.Should().BeTrue();

                // If the Charge type is passed then add it
                if (chargeTypeList != null)
                {
                    _response = await _chargeTypeGroupService.AddChargeTypeDetail(sessionId, processItemId,
                        chargeTypeGroupEntity);

                    var ChargeTypeRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
                    ChargeTypeRowId.Should().NotBeNull();

                    foreach (var chargeType in chargeTypeList)
                    {
                        chargeTypeGroupEntity.ChargeTypeDetailId =
                            await LookUp.GetLookUpKeyValue(sessionId, "ChrgType", chargeType.Description.String);
                        _response = await _chargeTypeGroupService.SelectChargeTypeDetail(sessionId, processItemId,
                            ChargeTypeRowId, chargeTypeGroupEntity);
                    }

                    _response = await _process.PostReleaseProcessAsync(sessionId, processItemId,
                        ApiConstants.ChargeTypeGroupProcess);
                    if (_response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("Submitted ChargeTypeGroupCode : " +
                                          chargeTypeGroupEntity.ChargeTypeGroupDescription.String);

                        return chargeTypeGroupEntity.ChargeTypeGroupDescription.String;
                    }

                    Console.WriteLine("test failed: " + _response.StatusCode);

                }

                _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeGroupProcess);
                _response.IsSuccessful.Should().BeTrue();
            }
            else if (chargeTypeList == null)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();
            }

            // if group already exists then it will add the charge types if they are passed
            if (chargeTypeList != null)
            {
                // Now add the charge Types to it
                var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                var key = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description == chargeTypeGroupEntity.ChargeTypeGroupDescription.String).RowKey;
                chargeTypeGroupEntity.ChargeTypeGroupId = key;
                _response = await _chargeTypeGroupService.SelectChargeTypeGroup(sessionId, processItemId, chargeTypeGroupEntity);
                chargeTypeGroupEntity.ChargeTypeGroupId = key.Replace("-", "");
                // check the charge deatils are already added to the group
                var param = "/objects/ChrgTypeGroup_ccc/rows/" + chargeTypeGroupEntity.ChargeTypeGroupId + "/childObjects/ChrgTypeDetail_ccc";
                var responseExistingChargeTypes = await _processDataService.GetDataAsync(sessionId, processItemId, param);

                foreach (var chargeType in chargeTypeList)
                {
                    // Check the charge type exists in the response, if false add to list 
                    if (!responseExistingChargeTypes.Content.Contains(chargeType.ChargeCode.String))
                    {
                        // Clicking the Add Button for Charge TYpe Grid
                        _response = await _chargeTypeGroupService.AddChargeTypeDetail(sessionId, processItemId,
                            chargeTypeGroupEntity);

                        var chargeTypeRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
                        chargeTypeRowId.Should().NotBeNull();

                        chargeTypeGroupEntity.ChargeTypeDetailId = chargeType.ChargeCode;
                        _response = await _chargeTypeGroupService.SelectChargeTypeDetail(sessionId, processItemId,
                            chargeTypeRowId, chargeTypeGroupEntity);
                    }

                }

                _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeGroupProcess);
                _response.IsSuccessful.Should().BeTrue();
            }
            return null;
        }
    }
}
