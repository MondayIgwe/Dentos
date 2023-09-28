using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.GlobalVendor;
using Elite3E.RestServices.Services.VendorMaintenance;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class VendorMaintenanceData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IVendorService _vendorService = new VendorService();
        public IRestResponse _response;
        public readonly EntityData _entityData = new();
        public IGlobalVendorService _globlaVendorService = new GlobalVendorService();
        private ILookUpService _lookUpService = new LookUpService();

        public async Task<string> CreateVendorAsync(string vendorName, string globalVendor = null)
        {
            var vendor = new ApiVendorEntity()
            {
                GlobalVendor = (string.IsNullOrEmpty(globalVendor)) ? "Amex Global Vendor" : globalVendor
            };

            // Check for Global vendor 
            vendor.GlobalVendor = await SearchAndCreateGlobalVendorTask(vendor.GlobalVendor);

            //Check For Entity
            // vendor.EntityName = await _entityService.CreateAnEntityPerson(vendorName);
            vendor.EntityName = await _entityData.SearchOrCreateAnEntityPerson(vendorName);

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.VendorMaintenanceProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Check Vendor Exists

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, vendor.EntityName);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Global Vendor Exists : " + vendor.EntityName);
                return vendor.EntityName;
            }


            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.VendorMaintenanceProcess);
            _response.IsSuccessful.Should().BeTrue();

            var vendorId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            AssertionExtensions.Should((string)vendorId).NotBeNull();
            Console.WriteLine("Vendor Id: " + vendorId);
            vendor.VendorId = vendorId;

            //QuickSearch GET entity for Vendor 
            _response = await _vendorService.GetVendorEntitySearchList(sessionId, processItemId, vendor);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            vendor.Entity = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(vendor.EntityName)).RowKey;

            vendor.GlobalVendorKey = await LookUp.GetLookUpKeyValue(sessionId, "GlobalVendor_ccc", vendor.GlobalVendor);

            _response = await _vendorService.AddVendorDataAsync(sessionId, processItemId, vendor);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.FeeEarnerProcessName);
            _response.IsSuccessful.Should().BeTrue();

            _response.Content.Should().Contain("responseType\":1,");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Vendor : " + vendor.EntityName);
                return vendor.EntityName;

            }

            throw new Exception("error Creating Global Vendor" + _response.ErrorMessage);
        }

        public async Task<string> SearchAndCreateGlobalVendorTask(string globalVendor = null)
        {
            var globalVendorEntity = new ApiGlobalVendorEntity()
            {
                Code = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") ,
                Description = (string.IsNullOrEmpty(globalVendor)) ? "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : globalVendor
            };

            
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.GlobalVendorProcessName);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, globalVendorEntity.Description.String);

            if (_response.Content.Length > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("The Given Charge Type Description Exists : " + globalVendorEntity.Description.String);
                return globalVendorEntity.Description.String;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.GlobalVendorProcessName);
            _response.IsSuccessful.Should().BeTrue();

            var globalVendorId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            AssertionExtensions.Should((string)globalVendorId).NotBeNull();
            Console.WriteLine("globalVendorId: " + globalVendorId);
            globalVendorEntity.GlobalVendorId = globalVendorId;

            _response = await _globlaVendorService.AddGlobalVendorAsync(sessionId, processItemId, globalVendorEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostSubmitProcessAsync(sessionId, processItemId, ApiConstants.GlobalVendorProcessName);
            _response.Content.Should().Contain("responseType\":1,");

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Client Name : " + globalVendorEntity.Description.String);
                return globalVendorEntity.Description.String;

            }

            throw new Exception("error Creating the Client" + _response.ErrorMessage);
        }
    }
}
