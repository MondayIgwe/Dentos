using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.ProformaEdit;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.DataCreators
{
    public class ProformaEditData
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response = new RestResponse();
        public ILookUpService _lookUpService = new LookUpService();
        public IProformaEditService _proformaEditService = new ProformaEditService();


        public async Task<ApiProformaEntity> SearchAndBillProformaAsync(ApiProformaEntity proformaEntity)
        {
            //Either Matter name or Matter Number must be provided
            (string.IsNullOrEmpty(proformaEntity.MatterNumber) && string.IsNullOrEmpty(proformaEntity.MatterName)).Should().BeFalse();

            //Get missing detail.
            proformaEntity = await new MatterMaintenanceData().GetMatterNumberOrName(proformaEntity);

            //Get Session ID
            _response = await _session.GetSessionResponseAsync();
            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content).Id.ToString();
            sessionId.Should().NotBeNull();

            //Open Process
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.ProformaEdit);
            _response.IsSuccessful.Should().BeTrue();
            var processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            //Search for Existing Proforma using Matter Name
            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, proformaEntity.MatterName);
            var quickResponse = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (quickResponse.Rows == null)
            {
                //If No Proforma exists, Skip
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("[Skipped] Proforma Not Found for Matter : " + proformaEntity.MatterNumber + " | " + proformaEntity.MatterName);
                return proformaEntity;
            }

            //Get BatchAdd RowKey
            var row = quickResponse.Rows.Where(x => x.Attributes.LeadMatterRel.Equals(proformaEntity.MatterNumber)).FirstOrDefault();
            row.Should().NotBeNull();
            var batchAddRowKey = row.RowKey;
            AssertionExtensions.Should((string)batchAddRowKey).NotBeNull();

            //Add Proforma to Worklist with BatchAdd
            _response = await _proformaEditService.AddSearchResultToWorklistAsync(sessionId, processItemId, batchAddRowKey);
            _response.IsSuccessful.Should().BeTrue();

            //Bill No Print
            _response = await _proformaEditService.BillNoPrintAsync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();

            //Check For Errors
            _response = await _proformaEditService.GetPossibleErrorsAsync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();
            if (_response.Content.Contains("error", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("[Error] Proforma Not Billed for Matter : " + proformaEntity.MatterNumber + " | " + proformaEntity.MatterName);
            }

            _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();

            //Validation

            //Open Process
            _response = await _process.GetProcessItemIdAsync(sessionId, ApiConstants.Invoices);
            _response.IsSuccessful.Should().BeTrue();
            processItemId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content).ProcessItemId.ToString();
            processItemId.Should().NotBeNull();

            _response = await _lookUpService.GetWorkListAsync(sessionId, processItemId, proformaEntity.MatterNumber);
            quickResponse = JsonConvert.DeserializeObject<QuickSearchResponseModel>(_response.Content);

            if (quickResponse.Rows == null || !quickResponse.Rows.Any(x => x.Attributes.LeadMatterNumber.Equals(proformaEntity.MatterNumber)))
            {
                //If No Proforma exists, Skip
                _response = await _process.PostCancelProcessAsync(sessionId, processItemId);
                _response.IsSuccessful.Should().BeTrue();

                Console.WriteLine("[Failed] Proforma not validated in Invoices : " + proformaEntity.MatterNumber + " | " + proformaEntity.MatterName);
                return proformaEntity;
            }

            Console.WriteLine("[Success] Proforma Edit passed for : " + proformaEntity.MatterNumber + " | " + proformaEntity.MatterName);

            return proformaEntity;
        }
    }
}
