using Elite3E.RegressionTests.RestServicesTest;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Services.PayeeMaintenance;
using RestSharp;
using System;
using System.Threading.Tasks;
using Elite3E.RestServices.Services;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiVendorSteps
    {
        private readonly FeatureContext _featureContext;
        public IPayeeMaintenanceService _clientAccountUse = new PayeeMaintenanceService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IProcessDataService _processDataService = new ProcessDataService();
        public IRestResponse _response;
        public CreateVendorMaintenance _vendorService = new();


        public ApiVendorSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I create a new Vendor with the Api")]
        public async Task GivenICreateANewVendorWithTheApi()
        {
            await CreateAVendor();
        }

        private async Task CreateAVendor()
        {
            // check and create vendor
            string vendorCreated = await _vendorService.CreateAVendor();

            if (!string.IsNullOrEmpty(vendorCreated))
            {
                _featureContext[StepConstants.VendorNameContext] = vendorCreated;
            }
            else
            {
                throw new Exception("Error occured in Api while creating Vendor");
            }

        }
    }
}