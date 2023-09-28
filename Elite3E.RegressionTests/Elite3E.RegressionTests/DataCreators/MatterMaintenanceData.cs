using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Enums;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Matter;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.MatterService;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class MatterMaintenanceData
    {
        public IMatterService _matterService = new MatterService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private CostTypeData _costTypeData = new();
        private ChargeTypeData _chargeTypeData = new();

        public async Task<MatterNumberAndClientNumber> CreateMatter(ApiMatterEntity matterDetails)
        {
            var matterNumberAndClientNumber = new MatterNumberAndClientNumber();

            var client = new ApiClientMaintenanceEntity();

            client.EntityName = matterDetails.Client;
            client.FeeEarnerFullName = matterDetails.FeeEarnerFullName;
            // check the data exists or create the required data 
            var clientMaintenanceData = new ClientMaintenanceData();

            // check client Data
            client = await clientMaintenanceData.ClientData(client);
            Console.WriteLine("Client Number Generated " + client.ClientNumber);

            matterNumberAndClientNumber.ClientNumber = client.ClientNumber;

            // to change the date 
            matterDetails.OpenDate = DateTime.Parse(matterDetails.OpenDate, new CultureInfo("en-US", true)).ToString("d/M/yyyy");


            //Get Session Id

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.MatterProcessName);
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.MatterProcessName);

            var matterId = JsonConvert.DeserializeObject<AddProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            matterId.Should().NotBeNull();
            matterDetails.MatterId = matterId;
            Console.WriteLine("Matter Id: " + matterId);


            _response = await _matterService.GetMatterClientAsync(sessionId, processItemId, matterDetails);
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            matterDetails.ClientAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(matterDetails.Client)).Alias;
            matterDetails.ClientRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(matterDetails.Client)).RowKey;

            matterDetails.StatusCode = await LookUp.GetLookUpKeyValue(sessionId, "MattStatus", matterDetails.Status);
            matterDetails.CurrencyCode = await LookUp.GetCurrencyLookUpKeyValue(sessionId, matterDetails.Currency);
            matterDetails.CurrencyMethodCode = await LookUp.GetLookUpKeyValue(sessionId, "CurrencyDateList", matterDetails.MatterCurrencyMethod);

            //Billing office is required for Fiscal invoicing
            if (!string.IsNullOrEmpty(matterDetails.BillingOffice))
            {
                matterDetails.BillingOfficeCode = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "Office", matterDetails.BillingOffice);
            }


            _response = await _matterService.AddMatterAsync(processItemId, sessionId, matterDetails);
            _response.Should().NotBeNull();

            _response = await _matterService.GetEffectiveDatedRowInformationAsync(processItemId, sessionId, matterDetails);

            var effectiveDateRowId = JsonHelper.JsonReaderChecker(_response.Content, "rows", 1); // ToDo: Write a dynamic reader using key value
            effectiveDateRowId.Should().NotBeNull();
            Console.WriteLine("Effective date Row Id: " + effectiveDateRowId);

            if (string.IsNullOrEmpty(matterDetails.Department))
            {
                //Creating Department if one is not provided
                ApiDepartmentEntity departmentEntity = new()
                {
                    Description = matterDetails.Department
                };
                matterDetails.Department = (await new DepartmentData().SearchAndCreateDepartmentAsync(DefaultRegionalValues.GetDynamicDepartmentDefaultValues(departmentEntity))).Description;
            }

            matterDetails.OfficeKey = await LookUp.GetLookUpKeyValue(sessionId, "Office", matterDetails.Office);
            matterDetails.DepartmentKey = await LookUp.GetLookUpKeyValue(sessionId, "Department", matterDetails.Department);
            matterDetails.SectionKey = await LookUp.GetLookUpKeyValue(sessionId, "Section", matterDetails.Section);

            _response = await _matterService.AddEffectiveDatedInformationAsync(processItemId, sessionId, effectiveDateRowId, matterDetails);

            _response = await _matterService.GetMatterRateAsync(processItemId, sessionId, matterDetails);
            var mattertRateRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 2); // ToDo: Write a dynamic reader using key value 
            mattertRateRowId = (mattertRateRowId.Equals("MattRate")) ? JsonHelper.JsonReaderChecker(_response.Content, "id", 1) : mattertRateRowId;
            mattertRateRowId.Should().NotBeNull();
            Console.WriteLine("Matter Rate  Row Id: " + mattertRateRowId);

            // check that the rate type exists 

            // Create if not exists : 

            var rateMaintenance = new ApiRateMaintenanceEntity()
            {
                Code = matterDetails.Rate,
                Description = matterDetails.Rate,
            };

            matterDetails.Rate = await new RateMaintenanceData().SearchAndCreateRateAsync(rateMaintenance);
            Console.WriteLine("Matter Rate is:" + matterDetails.Rate);
            _response = await _matterService.GetMatterRateAsync(sessionId, processItemId, mattertRateRowId, matterDetails);
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            //If Failing here, check DefaultRegionalValues > GetRateDefaultValues
            matterDetails.RateAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Code.Equals(matterDetails.Rate)).Alias;
            matterDetails.RateCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Code.Equals(matterDetails.Rate)).Attributes.Code;
            Console.WriteLine("Matter RateAlias is:" + matterDetails.RateAlias);

            _response = await _matterService.AddMatterRateAsync(processItemId, sessionId, mattertRateRowId, matterDetails);
            _response.Should().NotBeNull();

            //Add new Billing Information then submit
            _response = await _matterService.GetNewBillingSiteAsync(processItemId, sessionId, matterDetails);
            _response?.Should().NotBeNull();

            matterDetails.SiteId = new List<string>();

            matterDetails.SiteId.Add(JsonHelper.GetTagValue2(_response.Content, "currentRowId"));
            matterDetails.SiteId.Add(JsonHelper.GetTagValue2(_response.Content, "popupInstanceId"));


            _response = await _matterService.AddNewBillingSiteAsync(processItemId, sessionId, matterDetails);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _matterService.PostBillingSiteDataAsync(processItemId, sessionId, matterDetails);
            _response.IsSuccessful.Should().BeTrue();

            Console.WriteLine("Matter Billing Site has been added successfully");
            // Add chargetype group
            await AddChargeTypeGroup(sessionId, processItemId, matterDetails);
            Console.WriteLine("Matter Charge Type group has been added successfully");
            //Add cost type group
            await AddCostTypeGroup(sessionId, processItemId, matterDetails);
            Console.WriteLine("Matter Cost Type Group has been added successfully");
            // Add billing Group 

            if (!string.IsNullOrEmpty(matterDetails.BillingGroupDescription))
            {
                await AddBillingGroup(sessionId, processItemId, matterDetails);
                Console.WriteLine("Matter Billing Group has been added successfully");
            }

            // add matter payor 

            _response = await _matterService.GetMatterPayerFormAsync(processItemId, sessionId, matterDetails);

            var matterPayorRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
            matterPayorRowId.Should().NotBeNull();

            _response = await _matterService.GetMatterPayerDetailsFormAsync(processItemId, sessionId, matterDetails, matterPayorRowId);

            var matterPayorDetailRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 1);
            matterPayorDetailRowId.Should().NotBeNull();


            _response = await _matterService.GetMatterPayeeAsync(sessionId, processItemId, matterPayorRowId, matterPayorDetailRowId, matterDetails);
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            matterDetails.PayorCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(matterDetails.PayorName)).Attributes.PayerIndex;


            _response = await _matterService.AddMatterPayorAsync(processItemId, sessionId, matterPayorRowId,
                matterPayorDetailRowId, matterDetails);

            // Submit Matter

                _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.MatterProcessName);
                _response.IsSuccessful.Should().BeTrue();

            Console.WriteLine("Matter has been posted");
            _response.Content.Should().Contain("responseType\":1,");
            string matterNumber = _response.Headers.Where(x => x.Name.Equals("X-3E-Message")).Select(s => s.Value.ToString()).ToList()[0];

            string matterNumberGenerated = matterNumber.Split("%20")[4];

            Console.WriteLine("Matter Number Generated " + matterNumberGenerated);
            matterNumberAndClientNumber.MatterNumber = matterNumberGenerated;

            return matterNumberAndClientNumber;
        }

        //Use this only for Lite version
        public async Task<MatterNumberAndClientNumber> CreateMatter_Lite(ApiMatterEntity matterDetails)
        {
            var matterNumberAndClientNumber = new MatterNumberAndClientNumber();

            var client = new ApiClientMaintenanceEntity();

            client.EntityName = matterDetails.Client;
            client.FeeEarnerFullName = matterDetails.FeeEarnerFullName;
            // check the data exists or create the required data 
            var clientMaintenanceData = new ClientMaintenanceData();

            // check client Data
            client = await clientMaintenanceData.ClientData(client);
            Console.WriteLine("Client Number Generated " + client.ClientNumber);

            matterNumberAndClientNumber.ClientNumber = client.ClientNumber;

            // to change the date 
            matterDetails.OpenDate = DateTime.Parse(matterDetails.OpenDate, new CultureInfo("en-US", true)).ToString("d/M/yyyy");


            //Get Session Id

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.MatterProcessName);
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.MatterProcessName);

            var matterId = JsonConvert.DeserializeObject<AddProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            matterId.Should().NotBeNull();
            matterDetails.MatterId = matterId;
            Console.WriteLine("Matter Id: " + matterId);


            _response = await _matterService.GetMatterClientAsync(sessionId, processItemId, matterDetails);
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            matterDetails.ClientAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(matterDetails.Client)).Alias;
            matterDetails.ClientRowKey = quickSearch.Rows.FirstOrDefault(value => value.Attributes.DisplayName.Equals(matterDetails.Client)).RowKey;

            matterDetails.StatusCode = await LookUp.GetLookUpKeyValue(sessionId, "MattStatus", matterDetails.Status);
            matterDetails.CurrencyCode = await LookUp.GetCurrencyLookUpKeyValue(sessionId, matterDetails.Currency);
            matterDetails.CurrencyMethodCode = await LookUp.GetLookUpKeyValue(sessionId, "CurrencyDateList", matterDetails.MatterCurrencyMethod);

            //Billing office is required for Fiscal invoicing
            if (!string.IsNullOrEmpty(matterDetails.BillingOffice))
            {
                matterDetails.BillingOfficeCode = await LookUp.GetDropDownAliasKeyFromTheList(sessionId, "Office", matterDetails.BillingOffice);
            }


            _response = await _matterService.AddMatterAsync(processItemId, sessionId, matterDetails);
            _response.Should().NotBeNull();

            _response = await _matterService.GetEffectiveDatedRowInformationAsync(processItemId, sessionId, matterDetails);

            var effectiveDateRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 2); // ToDo: Write a dynamic reader using key value 
            effectiveDateRowId.Should().NotBeNull();
            Console.WriteLine("Effective date Row Id: " + effectiveDateRowId);

            if (string.IsNullOrEmpty(matterDetails.Department))
            {
                //Creating Department if one is not provided
                ApiDepartmentEntity departmentEntity = new()
                {
                    Description = matterDetails.Department
                };
                matterDetails.Department = (await new DepartmentData().SearchAndCreateDepartmentAsync(DefaultRegionalValues.GetDynamicDepartmentDefaultValues(departmentEntity))).Description;
            }

            matterDetails.OfficeKey = await LookUp.GetLookUpKeyValue(sessionId, "Office", matterDetails.Office);
            matterDetails.DepartmentKey = await LookUp.GetLookUpKeyValue(sessionId, "Department", matterDetails.Department);
            matterDetails.SectionKey = await LookUp.GetLookUpKeyValue(sessionId, "Section", matterDetails.Section);
            matterDetails.EDIPracticeKey = await LookUp.GetLookUpKeyValue(sessionId, "PracticeGroup", matterDetails.EDIPractice);

            //using AddEffectiveDatedInformationLiteAsync only for Lite version as an additional field 'Practice Group' is used
            _response = await _matterService.AddEffectiveDatedInformationLiteAsync(processItemId, sessionId, effectiveDateRowId, matterDetails);

            _response = await _matterService.GetMatterRateAsync(processItemId, sessionId, matterDetails);
            var mattertRateRowId = JsonHelper.JsonReaderChecker(_response.Content, "id", 2); // ToDo: Write a dynamic reader using key value 
            mattertRateRowId = (mattertRateRowId.Equals("MattRate")) ? JsonHelper.JsonReaderChecker(_response.Content, "id", 1) : mattertRateRowId;
            mattertRateRowId.Should().NotBeNull();
            Console.WriteLine("Matter Rate  Row Id: " + mattertRateRowId);

            // check that the rate type exists 

            // Create if not exists : 

            var rateMaintenance = new ApiRateMaintenanceEntity()
            {
                Code = matterDetails.Rate,
                Description = matterDetails.Rate,
            };

            matterDetails.Rate = await new RateMaintenanceData().SearchAndCreateRateAsync(rateMaintenance);

            _response = await _matterService.GetMatterRateAsync(sessionId, processItemId, mattertRateRowId, matterDetails);
            quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);


            matterDetails.RateAlias = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matterDetails.Rate)).Alias;
            matterDetails.RateCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matterDetails.Rate)).Attributes.Code;


            _response = await _matterService.AddMatterRateAsync(processItemId, sessionId, mattertRateRowId, matterDetails);
            _response.Should().NotBeNull();

            ////Add new Billing Information then submit
            //_response = await _matterService.GetNewBillingSiteAsync(processItemId, sessionId, matterDetails);
            //_response?.Should().NotBeNull();

            //matterDetails.SiteId = new List<string>();

            //matterDetails.SiteId.Add(JsonHelper.GetTagValue2(_response.Content, "currentRowId"));
            //matterDetails.SiteId.Add(JsonHelper.GetTagValue2(_response.Content, "popupInstanceId"));


            //_response = await _matterService.AddNewBillingSiteAsync(processItemId, sessionId, matterDetails);
            //_response.IsSuccessful.Should().BeTrue();
            //_response = await _matterService.PostBillingSiteDataAsync(processItemId, sessionId, matterDetails);
            //_response.IsSuccessful.Should().BeTrue();

            //// Add chargetype group
            //await AddChargeTypeGroup(sessionId, processItemId, matterDetails);

            ////Add cost type group
            //await AddCostTypeGroup(sessionId, processItemId, matterDetails);

            // Add billing Group 

            //if (!string.IsNullOrEmpty(matterDetails.BillingGroupDescription))
            //{
            //    await AddBillingGroup(sessionId, processItemId, matterDetails);
            //}

            // Submit Matter
            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.MatterProcessName);
            _response.IsSuccessful.Should().BeTrue();
            _response.Content.Should().Contain("responseType\":1,");

            string matterNumber = _response.Headers.Where(x => x.Name.Equals("X-3E-Message")).Select(s => s.Value.ToString()).ToList()[0];

            string matterNumberGenerated = matterNumber.Split("%20")[4];

            Console.WriteLine("Matter Number Generated " + matterNumberGenerated);
            matterNumberAndClientNumber.MatterNumber = matterNumberGenerated;

            return matterNumberAndClientNumber;
        }

        private async Task AddChargeTypeGroup(string sessionId, string processItemId, ApiMatterEntity matter)
        {
            // Add chargetype group
            if (matter.ChargeTypeGroupList != null)
            {
                foreach (var chargeTypeGroupName in matter.ChargeTypeGroupList)
                {
                    matter.ChargeTypeGroupName = chargeTypeGroupName;
                    await _chargeTypeData.SearchaAndCreateChargeTypeGroupDataAsync(matter.ChargeTypeGroupName);
                    _response = await _matterService.GetMatterChargeTypeGroupAsync(sessionId, processItemId, matter);
                    var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                    matter.ChargeTypeGroup = new List<Guid>();
                    matter.ChargeTypeGroup.Add(new Guid(quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.ChargeTypeGroupName)).RowKey));
                    _response = await _matterService.SelectChargeTypeGroupAsync(processItemId, sessionId, matter);
                    _response.IsSuccessful.Should().BeTrue();
                }
            }
            else if (matter.ChargeTypeGroupName != null)
            {
                await _chargeTypeData.SearchaAndCreateChargeTypeGroupDataAsync(matter.ChargeTypeGroupName);
                _response = await _matterService.GetMatterChargeTypeGroupAsync(sessionId, processItemId, matter);
                var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                matter.ChargeTypeGroup = new List<Guid>();
                matter.ChargeTypeGroup.Add(new Guid(quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.ChargeTypeGroupName)).RowKey));
                _response = await _matterService.SelectChargeTypeGroupAsync(processItemId, sessionId, matter);
                _response.IsSuccessful.Should().BeTrue();
                _response.Content.Should().Contain("responseType\":0,");
            }
        }
        private async Task AddCostTypeGroup(string sessionId, string processItemId, ApiMatterEntity matter)
        {
            if (matter.CostTypeGroupList != null)
            {
                foreach (var costTypeGroupName in matter.CostTypeGroupList)
                {
                    matter.CostTypeGroupName = costTypeGroupName;
                    await _costTypeData.SearchaAndCreateCostTypeGroupDataAsync(matter.CostTypeGroupName);
                    _response = await _matterService.GetMatterCostTypeGroupAsync(sessionId, processItemId, matter);
                    var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                    matter.CostTypeGroup = new List<Guid>();
                    matter.CostTypeGroup.Add(new Guid(quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.CostTypeGroupName)).RowKey));
                    _response = await _matterService.SelectCostTypeGroupAsync(processItemId, sessionId, matter);
                    _response.IsSuccessful.Should().BeTrue();
                }
            }
            else if (matter.CostTypeGroupName != null)
            {
                await _costTypeData.SearchaAndCreateCostTypeGroupDataAsync(matter.CostTypeGroupName);
                _response = await _matterService.GetMatterCostTypeGroupAsync(sessionId, processItemId, matter);
                var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                matter.CostTypeGroup = new List<Guid>();
                matter.CostTypeGroup.Add(new Guid(quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.CostTypeGroupName)).RowKey));
                _response = await _matterService.SelectCostTypeGroupAsync(processItemId, sessionId, matter);
                _response.IsSuccessful.Should().BeTrue();
                _response.Content.Should().Contain("responseType\":0,");
            }
        }
        private async Task AddBillingGroup(string sessionId, string processItemId, ApiMatterEntity matter)
        {
            _response = await _matterService.GetBillingGroupFormAsync(processItemId, sessionId, matter);
            var billingResponseObject = JsonConvert.DeserializeObject<AddBillingGroupResponseModel>(_response.Content);

            var billingGroupRowId = billingResponseObject.DataStateChanges.FirstOrDefault().Value.String;
            Console.WriteLine("Matter Billing  Row Id: " + billingGroupRowId);

            if (matter.BillingGroupList != null)
            {

                foreach (var billingGroup in matter.BillingGroupList)
                {
                    matter.BillingGroupDescription = billingGroup;
                    _response = await _matterService.GetMatterBillingGroupAsync(sessionId, processItemId, billingGroupRowId, matter);
                    var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                    matter.BillingGroupCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.BillingGroupDescription)).RowKey;
                    _response = await _matterService.SelectBillingGroupAsync(processItemId, sessionId, billingGroupRowId, matter);
                    _response.IsSuccessful.Should().BeTrue();
                }

            }
            else if (matter.BillingGroupDescription != null)
            {
                _response = await _matterService.GetMatterBillingGroupAsync(sessionId, processItemId, billingGroupRowId, matter);
                var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                matter.BillingGroupCode = quickSearch.Rows.FirstOrDefault(value => value.Attributes.Description.Equals(matter.BillingGroupDescription)).RowKey;
                _response = await _matterService.SelectBillingGroupAsync(processItemId, sessionId, billingGroupRowId, matter);
                _response.IsSuccessful.Should().BeTrue();

            }

        }

        public async Task<ApiProformaEntity> GetMatterNumberOrName(ApiProformaEntity proformaEntity)
        {
            //Get Session Id
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.MatterProcessName);
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            if (string.IsNullOrEmpty(proformaEntity.MatterNumber))
            {
                //Searching by Matter Name
                _response = await new LookUpService().GetWorkListAsync(sessionId, processItemId, proformaEntity.MatterName);
                var existingRateType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                existingRateType.RowCount.Should().BeGreaterThan(0);

                proformaEntity.MatterNumber = existingRateType.Rows.Where(x => x.Attributes.DisplayName.Equals(proformaEntity.MatterName)).FirstOrDefault().Attributes.Number;
                proformaEntity.MatterNumber.Should().NotBeNullOrEmpty();
            }
            else
            {
                //Searching by Matter Number
                _response = await new LookUpService().GetWorkListAsync(sessionId, processItemId, proformaEntity.MatterNumber);
                var existingRateType = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
                existingRateType.RowCount.Should().BeGreaterThan(0);

                proformaEntity.MatterName = existingRateType.Rows.Where(x => x.Attributes.Number.Equals(proformaEntity.MatterNumber)).FirstOrDefault().Attributes.DisplayName;
                proformaEntity.MatterName.Should().NotBeNullOrEmpty();
            }

            _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();

            return proformaEntity;
        }

    }
}
