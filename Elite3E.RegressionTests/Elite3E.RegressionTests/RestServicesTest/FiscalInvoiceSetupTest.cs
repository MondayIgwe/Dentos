using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Services.FiscalInvoiceSetup;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;

namespace Elite3E.RegressionTests.RestServicesTest
{

    public class FiscalInvoiceSetupTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private readonly IFiscalInvoiceSetupService fiscalInvoiceSetupService = new FiscalInvoiceSetupService();

        [Test]
        public async Task CreateFiscalInvoiceSetup()
        {
            var fiscalInvoiceSetupEntity = new FiscalInvoiceSetupEntity()
            {
                FiscalInvoicePrefix = "Test_fis",
                NextFiscalInvoiceNumber = "300",
                UnitDescription = "Dentons PNG",
                SuspenseGlTypeAlias = "Local Adj - The Law Firm of Wael A. Alissa in Assoc. with Denton",
                BillGlTypeAlias = "Local Adj - Dentons Australia Limited"
            };

            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.FiscalInvoiceSetup);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.FiscalInvoiceSetup);
            _response.IsSuccessful.Should().BeTrue();

            var fiscalInvoiceSetupId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            fiscalInvoiceSetupId.Should().NotBeNull();
            Console.WriteLine("Entity Id: " + fiscalInvoiceSetupId);
            fiscalInvoiceSetupEntity.FiscalSetupId = fiscalInvoiceSetupId;

            fiscalInvoiceSetupEntity.Unit = await LookUp.GetLookUpKeyValueByAliasAsync(sessionId, "NxUnit", fiscalInvoiceSetupEntity.UnitDescription);

            _response = await fiscalInvoiceSetupService.GetFiscalInvoiceSearchGLTypeAsync(sessionId, processItemId, fiscalInvoiceSetupEntity);
            _response.IsSuccessful.Should().BeTrue();
            var glTypequickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            fiscalInvoiceSetupEntity.BillGlTypeValue = glTypequickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(fiscalInvoiceSetupEntity.BillGlTypeAlias)).RowKey;
            fiscalInvoiceSetupEntity.BillGlTypeValue.String.Should().NotBeNullOrEmpty();


            _response = await fiscalInvoiceSetupService.GetFiscalInvoiceSuspenseGLTypeAsync(sessionId, processItemId, fiscalInvoiceSetupEntity);
            _response.IsSuccessful.Should().BeTrue();
            var suspenseGLTypequickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            fiscalInvoiceSetupEntity.SuspenseGlTypeValue = suspenseGLTypequickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(fiscalInvoiceSetupEntity.SuspenseGlTypeAlias)).RowKey;
            fiscalInvoiceSetupEntity.SuspenseGlTypeValue.String.Should().NotBeNullOrEmpty();

            _response = await fiscalInvoiceSetupService.AddFiscalInvoiceSetupAsync(sessionId, processItemId, fiscalInvoiceSetupEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostSubmitProcessAsync(sessionId, processItemId, ApiConstants.FiscalInvoiceSetup);
            _response.IsSuccessful.Should().BeTrue();

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Fisical Prefix : " + fiscalInvoiceSetupEntity.FiscalInvoicePrefix);

            }
            else
                Console.WriteLine("test failed: " + _response.StatusCode);

        }
    }
}

