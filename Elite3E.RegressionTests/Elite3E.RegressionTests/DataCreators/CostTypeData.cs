using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
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
using Elite3E.RestServices.Services.CostTypeGroup;
using Elite3E.RestServices.Services.DisbursementType;
using FluentAssertions;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class CostTypeData
    {
        public IProcessDataService _processDataService = new ProcessDataService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private ILookUpService _lookUpService = new LookUpService();
        public ICostTypeGroupService _costTypeGroupService = new CostTypeGroupService();
        public IDisbursementTypeService _disbursementTypeService = new DisbursementTypeService();

        public async Task<string> SearchaAndCreateCostTypeGroupDataAsync(string costTypeDescription)
        {
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.CostTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, costTypeDescription);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Cost Type Description Exists : " + costTypeDescription);
                return costTypeDescription;
            }

            var existingCostTypes = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);


            if (existingCostTypes.Rows != null)
            {
                foreach (var existingCostType in existingCostTypes.Rows)
                {
                    if (existingCostType.Attributes.Description.Equals(costTypeDescription,
                            StringComparison.CurrentCultureIgnoreCase))
                    {

                        //Storing ClientNumber for UI Test

                        _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                        _response.IsSuccessful.Should().BeTrue();

                        Console.WriteLine("The Given Cost Type Description Exists : " + costTypeDescription);
                        return costTypeDescription;
                    }
                }

            }

            // Create the cost Type group
            var costTypeEntity = new ApiCostTypeGroupEntity()
            {
                Code = "CTCode_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+7"),
                Description = costTypeDescription,
                CostTypeGroupIsExcludeOrIncludeListValue = "1",
                CostTypeGroupExcludeOrIncludeListOption = ApiConstants.IsExcludeList,
            };



            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.CostTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();

            var costTypeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            costTypeId.Should().NotBeNull();
            Console.WriteLine("Cost Type Id: " + costTypeId);
            costTypeEntity.CostTypeId = costTypeId;

            _response = await _costTypeGroupService.AddCostTypeGroupDataAsync(sessionId, processItemId, costTypeEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.CostTypeGroupProcess);
            if (_response.IsSuccessful)
            {
                Console.WriteLine("Submitted CostTypeGroupCode : " + costTypeEntity.Code.String);
                return costTypeEntity.Description.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }

        public async Task<string> SearchAndCreateDisbursementTypeDataAsync(ApiDisbursementTypeEntity disbursementTypeEntity)
        {

            //Start Session
            _response = await _session.GetSessionResponseAsync();

            //Get Session ID
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.DisbursementTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //search using the code as it is unique than description
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId,
                disbursementTypeEntity.Code.String);
            var existingDisbursementType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingDisbursementType.Rows != null)
            {
                foreach (var disbursementType in existingDisbursementType.Rows)
                {

                    if (!string.IsNullOrEmpty(disbursementTypeEntity.Code.String))
                    {
                        if (disbursementType.Attributes.Code
                                                    .Equals(disbursementTypeEntity.Code.String))
                        {
                            _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                            _response.IsSuccessful.Should().BeTrue();

                            Console.WriteLine("The Given Charge Type Description Exists : " + disbursementTypeEntity.Description.String);
                            return disbursementTypeEntity.Code.String;
                        }
                    }
                    else if(!string.IsNullOrEmpty(disbursementTypeEntity.Code.String))
                    {
                        if (disbursementType.Attributes.Code.Equals(disbursementTypeEntity.Code.String))
                        {
                            _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                            _response.IsSuccessful.Should().BeTrue();

                            Console.WriteLine("The Given Charge Type Code Exists : " + disbursementTypeEntity.Description.String);
                            return disbursementTypeEntity.Code.String;
                        }
                    }
                }
            }

            //Add New
            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.DisbursementTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            //Get ChargeType ID for Data Input
            var disbursementTypeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges
                .FirstOrDefault().Value.String;
            disbursementTypeId.Should().NotBeNull();
            Console.WriteLine("disbursementTypeId: " + disbursementTypeId);
            disbursementTypeEntity.DisbursementTypeId = disbursementTypeId;

            //Perform Lookup for Transaction Type (Search)
            // Part 1 - Perform Quick Info Request
            _response = await _disbursementTypeService.GetLookupSearchTransactionType(sessionId, processItemId, disbursementTypeEntity);
            var queryInfoResponse = JsonConvert.DeserializeObject<QueryInfoResponseModel>(_response.Content);
            queryInfoResponse.Should().NotBeNull();

            // Part 2 - Perform Quick Search
            _response = await _disbursementTypeService.GetLookupSearchTransactionType(sessionId, processItemId,
                disbursementTypeEntity);
            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            disbursementTypeEntity.TransactionTypeValue = quickSearch.Rows
                .FirstOrDefault(value => value.Attributes.Description.Equals(disbursementTypeEntity.TransactionTypeAlias))
                .Attributes.Code;
            disbursementTypeEntity.TransactionTypeValue.String.Should().NotBeNullOrEmpty();

            //Fill in ChargeType Data

            _response = await _disbursementTypeService.AddDisbursementTypeDataDataAsync(sessionId, processItemId, disbursementTypeEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId,
                ApiConstants.DisbursementTypeProcess);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted Disbursement Type : " + disbursementTypeEntity.Description.String);
                return disbursementTypeEntity.Description.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            throw new Exception(_response.ErrorMessage);

        }

        public async Task<string> SearchDisbursementTypeGroupAndAddDisbursementTypeDataAsync(ApiCostTypeGroupEntity CostTypeGroupEntity,
            List<ApiDisbursementTypeEntity> disbursementTypeList)
        {
            if (disbursementTypeList != null)
            {
                foreach (var disbursementType in disbursementTypeList)
                {
                    await SearchAndCreateDisbursementTypeDataAsync(disbursementType);
                }
            }

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.CostTypeGroupProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, CostTypeGroupEntity.Description.String);

            if (_response.Content.Length == 2)
            {
                _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.CostTypeGroupProcess);
                _response.IsSuccessful.Should().BeTrue();

                var costTypeGroupId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
                costTypeGroupId.Should().NotBeNull();
                Console.WriteLine("costTypeGroupId: " + costTypeGroupId);
                CostTypeGroupEntity.CostTypeId = costTypeGroupId;

                _response = await _costTypeGroupService.AddCostTypeGroupDataAsync(sessionId, processItemId, CostTypeGroupEntity);
                _response.IsSuccessful.Should().BeTrue();

                if (disbursementTypeList != null)
                {
                    _response = await _costTypeGroupService.AddCostTypeDetail(sessionId, processItemId,
                        CostTypeGroupEntity);

                    var chargeTypeRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
                    chargeTypeRowId.Should().NotBeNull();

                    foreach (var chargeType in disbursementTypeList)
                    {
                        CostTypeGroupEntity.CostTypeDetailId =
                            await LookUp.GetLookUpKeyValue(sessionId, "CostType", chargeType.Description.String);
                        _response = await _costTypeGroupService.SelectCostTypeDetail(sessionId, processItemId,
                            chargeTypeRowId, CostTypeGroupEntity);
                    }
                }

                _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.ChargeTypeGroupProcess);

                if (_response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Submitted CostTypeGroupCode : " + CostTypeGroupEntity.Description.String);

                    return CostTypeGroupEntity.Description.String;
                }

                Console.WriteLine("test failed: " + _response.StatusCode);
            }

            if (disbursementTypeList != null)
            {
                // Now add the cost Types to it
                var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

                var key = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description == CostTypeGroupEntity.Description.String).RowKey;

                CostTypeGroupEntity.CostTypeId = key;

                _response = await _costTypeGroupService.SelectCostTypeGroup(sessionId, processItemId, CostTypeGroupEntity);

                CostTypeGroupEntity.CostTypeId = key.Replace("-", "");

                var param = "/objects/CostTypeGroup_ccc/rows/" + CostTypeGroupEntity.CostTypeId + "/childObjects/CostTypeDetail_ccc";
                var responseExistingCostTypes = await _processDataService.GetDataAsync(sessionId, processItemId, param);

                foreach (var disbursementType in disbursementTypeList)
                {
                    if (!responseExistingCostTypes.Content.Contains(disbursementType.Code.String))
                    {
                        _response = await _costTypeGroupService.AddCostTypeDetail(sessionId, processItemId,
                            CostTypeGroupEntity);

                        var ChargeTypeRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
                        ChargeTypeRowId.Should().NotBeNull();

                        _response = await _costTypeGroupService.GetDisbursementTypeValue(sessionId, processItemId,
                            CostTypeGroupEntity, ChargeTypeRowId, disbursementType.Description.String);

                        _response.IsSuccessful.Should().BeTrue();
                        quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                        CostTypeGroupEntity.CostTypeDetailId = quickSearch.Rows.FirstOrDefault(value =>
                            value.Attributes.Description.Equals(disbursementType.Description.String)).RowKey;

                        //CostTypeGroupEntity.CostTypeDetailId = await LookUp.GetLookUpKeyValue(sessionId, "CostType", chargeType.Description.String);
                        _response = await _costTypeGroupService.SelectCostTypeDetail(sessionId, processItemId,
                            ChargeTypeRowId, CostTypeGroupEntity);
                    }
                }

                _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.CostTypeGroupProcess);
                _response.IsSuccessful.Should().BeTrue();

            }

            return null;

        }

        public async Task<string> SearchAndCreateDisbursmentTypeWithBarristerDataAsync(ApiDisbursementTypeEntity disbursementType)
        {
            disbursementType.Code = (string.IsNullOrEmpty(disbursementType.Code.String)) ? "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : disbursementType.Code;
            disbursementType.Description = (string.IsNullOrEmpty(disbursementType.Description.String)) ? "Description_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : disbursementType.Description;
            disbursementType.IsHardDisbursementOrSoftDisbursementOption = (string.IsNullOrEmpty(disbursementType.IsHardDisbursementOrSoftDisbursementOption)) ? ApiConstants.IsSoftDisbursementType : disbursementType.IsHardDisbursementOrSoftDisbursementOption;
            disbursementType.IsHardDisbursementOrSoftDisbursementValue = (string.IsNullOrEmpty(disbursementType.IsHardDisbursementOrSoftDisbursementValue.String)) ? "1" : disbursementType.IsHardDisbursementOrSoftDisbursementValue;
            disbursementType.TransactionTypeAlias = (string.IsNullOrEmpty(disbursementType.TransactionTypeAlias)) ? "Soft Cost" : disbursementType.TransactionTypeAlias;

            return await CreateDisbursementTypeWithBarristerFlagAsync(disbursementType);
        }

        public async Task<string> SearchAndCreateHardDisbursmentTypeDataAsync(ApiDisbursementTypeEntity disbursementType)
        {
            disbursementType.Code = (string.IsNullOrEmpty(disbursementType.Code.String)) ? "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : disbursementType.Code;
            disbursementType.Description = (string.IsNullOrEmpty(disbursementType.Description.String)) ? "Description_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : disbursementType.Description;
            disbursementType.IsHardDisbursementOrSoftDisbursementOption = (string.IsNullOrEmpty(disbursementType.IsHardDisbursementOrSoftDisbursementOption)) ? ApiConstants.IsHardDisbursementType : disbursementType.IsHardDisbursementOrSoftDisbursementOption;
            disbursementType.IsHardDisbursementOrSoftDisbursementValue = (string.IsNullOrEmpty(disbursementType.IsHardDisbursementOrSoftDisbursementValue.String)) ? "1" : disbursementType.IsHardDisbursementOrSoftDisbursementValue;
            disbursementType.TransactionTypeAlias = (string.IsNullOrEmpty(disbursementType.TransactionTypeAlias)) ? "Hard Cost" : disbursementType.TransactionTypeAlias;
            return await CreateDisbursementTypeAsync(disbursementType);
        }

        public async Task<string> SearchAndCreateSoftDisbursmentTypeDataAsync(ApiDisbursementTypeEntity disbursementType)
        {
            disbursementType.Code = (string.IsNullOrEmpty(disbursementType.Code.String)) ? "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : disbursementType.Code;
            disbursementType.Description = (string.IsNullOrEmpty(disbursementType.Description.String)) ? "Description_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : disbursementType.Description;
            disbursementType.IsHardDisbursementOrSoftDisbursementOption = (string.IsNullOrEmpty(disbursementType.IsHardDisbursementOrSoftDisbursementOption)) ? ApiConstants.IsSoftDisbursementType : disbursementType.IsHardDisbursementOrSoftDisbursementOption;
            disbursementType.IsHardDisbursementOrSoftDisbursementValue = (string.IsNullOrEmpty(disbursementType.IsHardDisbursementOrSoftDisbursementValue.String)) ? "1" : disbursementType.IsHardDisbursementOrSoftDisbursementValue;
            disbursementType.TransactionTypeAlias = (string.IsNullOrEmpty(disbursementType.TransactionTypeAlias)) ? "Soft Cost" : disbursementType.TransactionTypeAlias;

            return await CreateDisbursementTypeAsync(disbursementType);
        }
        

        private async Task<string> CreateDisbursementTypeAsync(ApiDisbursementTypeEntity disbursementType)
        {

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.DisbursementTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, disbursementType.Description.String);
            var existingDisbursementType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingDisbursementType.RowCount > 0)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given disbursement Type Description Exists : " + disbursementType.Description.String);
                return disbursementType.Description.String;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.DisbursementTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            var disbursementTypeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            disbursementTypeId.Should().NotBeNull();
            Console.WriteLine("disbursementTypeId: " + disbursementTypeId);
            disbursementType.DisbursementTypeId = disbursementTypeId;

            _response = await _disbursementTypeService.GetLookupSearchTransactionType(sessionId, processItemId, disbursementType);
            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            disbursementType.TransactionTypeValue = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(disbursementType.TransactionTypeAlias)).Attributes.Code;
            disbursementType.TransactionTypeValue.String.Should().NotBeNullOrEmpty();

           
            _response = await _disbursementTypeService.AddDisbursementTypeDataDataAsync(sessionId, processItemId, disbursementType);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.DisbursementTypeProcess);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted Disbursement Type Description : " + disbursementType.Description.String);

                return disbursementType.Description.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }

        private async Task<string> CreateDisbursementTypeWithBarristerFlagAsync(ApiDisbursementTypeEntity disbursementType)
        {

            _response = await _session.GetSessionResponseAsync();

            //Get session Item Id 
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.DisbursementTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();
     
            //Check disbursement type for the given description and barrister fees exists
            _response = await  _disbursementTypeService.GetDisbursementTypeAdvancedSearchList(sessionId, processItemId, disbursementType);
            _response.IsSuccessful.Should().BeTrue();
            var existingDisbursementType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingDisbursementType.RowCount > 0)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The given disbursement type: " + disbursementType.Description.String + "Barrister flag is"+disbursementType.IsBarristerFlag);
                return disbursementType.Description.String;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.DisbursementTypeProcess);
            _response.IsSuccessful.Should().BeTrue();

            var disbursementTypeId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            disbursementTypeId.Should().NotBeNull();
            Console.WriteLine("disbursementTypeId: " + disbursementTypeId);
            disbursementType.DisbursementTypeId = disbursementTypeId;

            _response = await _disbursementTypeService.GetLookupSearchTransactionType(sessionId, processItemId, disbursementType);
            _response.IsSuccessful.Should().BeTrue();

            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            disbursementType.TransactionTypeValue = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(disbursementType.TransactionTypeAlias)).Attributes.Code;
            disbursementType.TransactionTypeValue.String.Should().NotBeNullOrEmpty();


            _response = await _disbursementTypeService.AddDisbursementTypeDataDataAsync(sessionId, processItemId, disbursementType);
            _response.IsSuccessful.Should().BeTrue();
            if (disbursementType.IsBarristerFlag.Equals("true"))
            {
                _response = await _disbursementTypeService.AddBarristerFlag(sessionId, processItemId, disbursementType);
                _response.IsSuccessful.Should().BeTrue();
            }

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.DisbursementTypeProcess);
            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Submitted Disbursement Type Description : " + disbursementType.Description.String);

                return disbursementType.Description.String;
            }

            Console.WriteLine("test failed: " + _response.StatusCode);
            return null;
        }
    }
}
