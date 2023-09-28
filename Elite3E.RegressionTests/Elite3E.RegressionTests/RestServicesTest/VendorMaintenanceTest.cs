using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.VendorMaintenance;
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
    public class VendorMaintenanceTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IVendorService _vendorService = new VendorService();
        public IRestResponse _response;
        public EntityData _entityData = new();

        [Test]
        public async Task CreateAVendor()
        {

            var vendor = new ApiVendorEntity()
            {
                GlobalVendor = "DENGBLV"
            };

            vendor.EntityName = await _entityData.SearchOrCreateAnEntityPerson();
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.VendorMaintenanceProcess);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.VendorMaintenanceProcess);
            _response.IsSuccessful.Should().BeTrue();

            var vendorId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            vendorId.Should().NotBeNull();
            Console.WriteLine("Vendor Id: " + vendorId);
            vendor.VendorId = vendorId;

            //QuickSearch GET entity for Vendor 
            _response = await _vendorService.GetVendorEntitySearchList(sessionId, processItemId, vendor);
            _response.IsSuccessful.Should().BeTrue();
            var quickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            vendor.Entity = quickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(vendor.EntityName)).RowKey;

            _response = await _vendorService.AddVendorDataAsync(sessionId, processItemId, vendor);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostReleaseProcessAsync(sessionId, processItemId, ApiConstants.FeeEarnerProcessName);
            _response.IsSuccessful.Should().BeTrue();

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Vendor : " + vendor.EntityName);

            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);

        }
    }
}

