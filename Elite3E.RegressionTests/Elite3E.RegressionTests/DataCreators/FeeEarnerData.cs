using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.LookUpResponseModel;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.FeeEarner;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class FeeEarnerData
    {
        public readonly IProcessService _process = new ProcessService();
        public readonly ISessionService _session = new SessionService();
        public readonly IFeeEarnerService _feeEranerService = new FeeEarnerService();
        private readonly ILookUpService _lookUpService = new LookUpService();
        private readonly EntityData _entityData = new();
        public IRestResponse _response;


        public static async Task<string> GetFeeEarnerNumber(ApiFeeEarnerEntity feeEarnerEntity)
        {
            var entity = new ApiEntity()
            {
                FirstName = feeEarnerEntity.EntityName.Split(" ")[0],
                LastName = feeEarnerEntity.EntityName.Split(" ")[1],
                FormattedName = feeEarnerEntity.EntityName
            };

            var entityRespose = await new FeeEarnerData().SearchAndCreateFeeEranerData(feeEarnerEntity, entity);
            return entityRespose.FeeEarnerNumber;
        }

        public static async Task<string> GetFeeEarnerName(ApiFeeEarnerEntity feeEarnerEntity)
        {
            var entity = new ApiEntity()
            {
                FirstName = feeEarnerEntity.EntityName.Split(" ")[0],
                LastName = feeEarnerEntity.EntityName.Split(" ")[1],
                FormattedName = feeEarnerEntity.EntityName
            };

            return (await new FeeEarnerData().SearchAndCreateFeeEranerData(feeEarnerEntity, entity)).EntityName;
        }

        public static async Task<List<string>> GetFeeEarnersNumbers(List<ApiFeeEarnerEntity> feeEarnersList)
        {
            var feeEarnerNumbers = new List<string>();

            foreach (var feeEarner in feeEarnersList)
            {
                var result = await GetFeeEarnerNumber(feeEarner);
                feeEarnerNumbers.Add(result);
            }
            return feeEarnerNumbers;

        }

        public async Task<ApiFeeEarnerEntity> SearchAndCreateFeeEranerData(ApiFeeEarnerEntity feeEarner, ApiEntity entity)
        {
            feeEarner = DefaultRegionalValues.GetFeeEarnerDefaultValues(feeEarner, entity);

            _response = await _session.GetSessionResponseAsync();

            var sessionResponse = JsonConvert.DeserializeObject<Session>(_response.Content);
            var sessionId = sessionResponse.Id.ToString();
            sessionId.Should().NotBeNull();

            feeEarner.WorkflowUserAlias = (string.IsNullOrEmpty(feeEarner.WorkflowUserAlias)) ? null : sessionResponse?.User.Name;

            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.FeeEarnerProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            // Search for feeEarner 
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, feeEarner.EntityName);
            var existingFeeEarner = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (existingFeeEarner.Rows != null)
            {
                foreach (var feeEarnerRow in existingFeeEarner.Rows)
                {
                    if (feeEarnerRow.Attributes.DisplayName.Equals(feeEarner.EntityName,
                            StringComparison.CurrentCultureIgnoreCase))
                    {

                        //Storing ClientNumber for UI Test

                        feeEarner.FeeEarnerNumber = feeEarnerRow.Attributes.Number.ToString();

                        _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                        _response.IsSuccessful.Should().BeTrue();
                        Console.WriteLine("The Given Fee Earner exits:" + feeEarner.EntityName);
                        return feeEarner;
                    }
                }

            }

            feeEarner.EntityName = (await _entityData.SearchOrCreateAnEntity(entity)).FormattedName;

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.FeeEarnerProcessName);
            _response.IsSuccessful.Should().BeTrue();

            var feeEarnerId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            AssertionExtensions.Should((string)feeEarnerId).NotBeNull();
            Console.WriteLine("Entity Id: " + feeEarnerId);
            feeEarner.FeeEarnerId = feeEarnerId;

            //QuickSearch GET entity for fee earner
            _response = await _feeEranerService.GetFeeEarnerEntitySearchList(sessionId, processItemId, feeEarner);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            feeEarner.Entity = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(feeEarner.EntityName)).RowKey;

            _response = await _feeEranerService.AddFeeEarnerDataAsync(sessionId, processItemId, feeEarner);

            _response = await _feeEranerService.AddEffectiveDatedInformationAsync(sessionId, processItemId, feeEarner);
            _response.IsSuccessful.Should().BeTrue();
            var rowId = JsonHelper.JsonReaderChecker(_response.Content, "rows", 1);
            rowId.Should().NotBeNull();
            Console.WriteLine("Effective date info Row Id: " + rowId);

            // to make sure the value is selected from list accross the environments unless specified
            // get look up key values           
            feeEarner.OfficeKey = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "Office", feeEarner.Office);
            feeEarner.DepartmentKey = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "Department", feeEarner.Department);
            feeEarner.SectionKey = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "Section", feeEarner.Section);
            feeEarner.TitleKey = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "Title", feeEarner.Title);
            feeEarner.RateClassKey = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "RateClass", feeEarner.RateClass);

            _response = await _feeEranerService.UpdateEffectiveDateInformationAsync(sessionId, processItemId, rowId, feeEarner);
            _response.IsSuccessful.Should().BeTrue();
            if (!string.IsNullOrEmpty(feeEarner.EDIStartDate))
            {
                feeEarner.EDIStartDate = DateTime.Parse(feeEarner.EDIStartDate, new CultureInfo("en-US", true)).ToString("d/M/yyyy");
                _response = await _feeEranerService.AddEDIStartDateAsync(sessionId, processItemId, rowId, feeEarner);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Get Fee Earner Standard Rate Advanced Search
            _response = await _feeEranerService.GetFeeEarnerRateTypeAdvancedSearchList(sessionId, processItemId, feeEarner);
            _response.IsSuccessful.Should().BeTrue();
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (quickSearch.Rows == null)
            {//IF RateType Does not exist, Create it then search again. 
                feeEarner.RateTypeDescription = await new RateTypeMaintenanceData().CreateRateTypeAsync(DefaultRegionalValues.GetFeeEarnerRateTypeDefaultValues());

                _response = await _feeEranerService.GetFeeEarnerRateTypeSearchList(sessionId, processItemId, feeEarner);
                _response.IsSuccessful.Should().BeTrue();
                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                quickSearch.Rows.Should().NotBeNull();

                quickSearch.Rows.Any(value => value.Attributes.Description.Equals(feeEarner.RateTypeDescription)).Should().BeTrue();
            }
            else
            {
                feeEarner.RateTypeDescription = quickSearch.Rows.FirstOrDefault().Attributes.Description;
            }

            feeEarner.RateTypeKey = new List<Guid>();
            feeEarner.RateTypeKey.Add(new Guid(quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(feeEarner.RateTypeDescription)).RowKey));

            _response = await _feeEranerService.AddFeeEarnerRatesAsync(sessionId, processItemId, feeEarner);
            _response.IsSuccessful.Should().BeTrue();
            var feeEarnerRateRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
            rowId.Should().NotBeNull();
            Console.WriteLine("Fee earner rate Row Id: " + feeEarnerRateRowId);


            // effective date rates 
            _response = await _feeEranerService.GetEffectiveDatedRatesInformationAsync(sessionId, processItemId, feeEarnerRateRowId, feeEarner);
            _response.IsSuccessful.Should().BeTrue();
            var effectiveRateDateRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
            rowId.Should().NotBeNull();
            Console.WriteLine("Effective ratedate Row Id: " + effectiveRateDateRowId);

            // get currency Key
            feeEarner.EffectiveRateCurrency = await LookUp.GetCurrencyLookUpKeyValue(sessionId, feeEarner.EffectiveRateCurrencyDescription);

            //add rate and currency to effective dated rates
            _response = await _feeEranerService.AddEffectiveDatedRatesAsync(sessionId, processItemId, feeEarnerRateRowId, effectiveRateDateRowId, feeEarner);
            _response.IsSuccessful.Should().BeTrue();

            // Add fee earner rate type start date

            if (!string.IsNullOrEmpty(feeEarner.FeeEarnerRatesStartDate))
            {
                feeEarner.FeeEarnerRatesStartDate = DateTime.Parse(feeEarner.FeeEarnerRatesStartDate, new CultureInfo("en-US", true)).ToString("d/M/yyyy");
                _response = await _feeEranerService.AddFeeEarnerRateStartDateAsync(sessionId, processItemId, feeEarnerRateRowId, effectiveRateDateRowId, feeEarner);
                _response.IsSuccessful.Should().BeTrue();
            }

            //Add Workflow User
            if (!string.IsNullOrEmpty(feeEarner.WorkflowUserAlias))
            {
                _response = await _feeEranerService.GetWorkflowUserQuickResponse(sessionId, processItemId, feeEarner);
                quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                quickSearch.Rows.Should().NotBeNull();
                feeEarner.WorkflowUserValue = quickSearch.Rows.Where(value => value.Attributes.BaseUserName.Equals(feeEarner.WorkflowUserAlias)).FirstOrDefault().RowKey;
                _response = await _feeEranerService.AddWorkflowUserDataAsync(sessionId, processItemId, feeEarner);
                _response.IsSuccessful.Should().BeTrue();
            }

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.FeeEarnerProcessName);
            _response.IsSuccessful.Should().BeTrue();
            _response.Content.Should().Contain("responseType\":1");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Fee earner : " + feeEarner.EntityName);

                //Storing FeeEarnerNumber for UI Test
                _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, feeEarner.EntityName);
                var feeEarnerResposeModel = JsonConvert.DeserializeObject<LookupFeeEarnerResponseModel>(_response.Content);
                feeEarner.FeeEarnerNumber = feeEarnerResposeModel.Rows[0].Attributes.Number.ToString();

                return feeEarner;
            }

            throw new Exception("error Creating the FeeEarner" + _response.ErrorMessage);
        }
    }
}
