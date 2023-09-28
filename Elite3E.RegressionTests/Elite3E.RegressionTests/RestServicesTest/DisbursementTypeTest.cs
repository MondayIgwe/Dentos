using Elite3E.Infrastructure.Extensions;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.DisbursementType;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public  class DisbursementTypeTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IDisbursementTypeService _disbursementTypeService = new DisbursementTypeService();
        public readonly CostTypeData _costTypeData = new();

        [Test]
        public async Task CreateHardDisbursementTypeTask(string code = null, string description = null)
        {
            var disbursementTypeEntity = new ApiDisbursementTypeEntity()
            {
                Code = (string.IsNullOrEmpty(code)) ? "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : code,
                Description = (string.IsNullOrEmpty(description)) ? "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : description,
                IsHardDisbursementOrSoftDisbursementOption = ApiConstants.IsHardDisbursementType,
                IsHardDisbursementOrSoftDisbursementValue = "1",
                TransactionTypeAlias = "Anticipated Hard Cost",
            };
           await _costTypeData.SearchAndCreateHardDisbursmentTypeDataAsync(disbursementTypeEntity);
        }

        [Test]
        public async Task CreateSoftDisbursementTypeTask(string code = null, string description = null)
        {
            var disbursementTypeEntity = new ApiDisbursementTypeEntity()
            {
                Code = (string.IsNullOrEmpty(code)) ? "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : code,
                Description = (string.IsNullOrEmpty(description)) ? "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : description,
                IsHardDisbursementOrSoftDisbursementOption = ApiConstants.IsSoftDisbursementType,
                IsHardDisbursementOrSoftDisbursementValue = "1",
                TransactionTypeAlias = "Anticipated Soft Cost",
            };
            await CreateDisbursementTypeTask(disbursementTypeEntity);
        }

        private async Task CreateDisbursementTypeTask(ApiDisbursementTypeEntity disbursementType)
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.DisbursementTypeProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

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
                Console.WriteLine("Submitted Disbursement Type Code : " + disbursementType.Code.String);

            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);
        }

    }
}
