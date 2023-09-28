using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.FiscalInvoiceSetup;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RegressionTests.DataCreators
{
    public class FiscalInvoiceSetupData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        private ILookUpService _lookUpService = new LookUpService();
        private readonly IFiscalInvoiceSetupService _fiscalInvoiceSetupService = new FiscalInvoiceSetupService();
        
        public async Task CreateFiscalInvoiceSetup(FiscalInvoiceSetupEntity fiscalInvoiceSetupEntity)
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Get Process Item Id 
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.FiscalInvoiceSetup);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId,
                fiscalInvoiceSetupEntity.Unit.String);

            if (_response.ContentLength > 2)
            {
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                return;
            }

            _response = await _process.AddNewProcessAsync(sessionId, processItemId, ApiConstants.FiscalInvoiceSetup);
            _response.IsSuccessful.Should().BeTrue();

            var fiscalInvoiceSetupId = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content).DataStateChanges.FirstOrDefault().Value.String;
            fiscalInvoiceSetupId.Should().NotBeNull();
            Console.WriteLine("Entity Id: " + fiscalInvoiceSetupId);
            fiscalInvoiceSetupEntity.FiscalSetupId = fiscalInvoiceSetupId;

            fiscalInvoiceSetupEntity.Unit = await LookUp.GetLookUpKeyValueByAliasAsync(sessionId, "NxUnit", fiscalInvoiceSetupEntity.UnitDescription);

            _response = await _fiscalInvoiceSetupService.GetFiscalInvoiceSearchGLTypeAsync(sessionId, processItemId, fiscalInvoiceSetupEntity);
            _response.IsSuccessful.Should().BeTrue();
            var glTypequickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            fiscalInvoiceSetupEntity.BillGlTypeValue = glTypequickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(fiscalInvoiceSetupEntity.BillGlTypeAlias)).RowKey;
            fiscalInvoiceSetupEntity.BillGlTypeValue.String.Should().NotBeNullOrEmpty();


            _response = await _fiscalInvoiceSetupService.GetFiscalInvoiceSuspenseGLTypeAsync(sessionId, processItemId, fiscalInvoiceSetupEntity);
            _response.IsSuccessful.Should().BeTrue();
            var suspenseGLTypequickSearch = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);
            fiscalInvoiceSetupEntity.SuspenseGlTypeValue = suspenseGLTypequickSearch.Rows.FirstOrDefault(value => value.Alias.Equals(fiscalInvoiceSetupEntity.SuspenseGlTypeAlias)).RowKey;
            fiscalInvoiceSetupEntity.SuspenseGlTypeValue.String.Should().NotBeNullOrEmpty();

            _response = await _fiscalInvoiceSetupService.AddFiscalInvoiceSetupAsync(sessionId, processItemId, fiscalInvoiceSetupEntity);
            _response.IsSuccessful.Should().BeTrue();

            _response = await _process.PostSubmitProcessAsync(sessionId, processItemId, ApiConstants.FiscalInvoiceSetup);
            _response.IsSuccessful.Should().BeTrue();

            if (_response.IsSuccessful)
            {
                Console.WriteLine("Fiscal Prefix : " + fiscalInvoiceSetupEntity.FiscalInvoicePrefix);
            }
            else
            {
                Console.WriteLine("test failed: " + _response.StatusCode);
            }

        }
    }
}
