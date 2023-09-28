using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Models.ResponseModels.OpenProcess;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RestServices.Services.OpenProcess;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace Elite3E.RegressionTests.RestServicesTest
{
    internal class OpenProcessTest
    {
        private readonly IProcessService _process = new ProcessService();
        private readonly ISessionService _session = new SessionService();
        private IRestResponse _response;
        private readonly IOpenProcessService _openService = new OpenProcessService();

        [Test]
        public async Task CloseAllOpenProcessAsync()
        {
            _response = await _session.GetSessionResponseAsync();

            var sessionId = JsonConvert.DeserializeObject<Session>(_response.Content)?.Id.ToString();
            sessionId.Should().NotBeNull();

            var userName = JsonConvert.DeserializeObject<Session>(_response.Content)?.User.Name;
            
            _response = await _process.GetPresentationItemIdAsync(sessionId, ApiConstants.NxOpenProcesses);
            _response.IsSuccessful.Should().BeTrue();

            var presentationResponse = JsonConvert.DeserializeObject<PresentationResponseModel>(_response.Content);

            var rowValue = presentationResponse?.Parameters.DataSet.Objects.NxOpenProcessPo.Rows.ToString();
            var id = JsonHelper.JsonReaderChecker(rowValue, "id", 1);
            var value = JsonHelper.JsonReaderChecker(rowValue, "value", 1);

            _response = await _openService.GetParameterDataAsync(sessionId, id, value);
            var contextId = JsonConvert.DeserializeObject<ProcessModel>(_response.Content)?.ContextId;

            _response = await _openService.SearchUserDataAsync(sessionId, id, userName, contextId);

            var dataResponse = JsonConvert.DeserializeObject<ProcessResponseModel>(_response.Content);
            var nxBaseUserValue = dataResponse?.Changes.FirstOrDefault(v => v.Path.Contains("NxBaseUser/value"))?.Value;

            _response = await _openService.PostReportDataAsync(sessionId, value, nxBaseUserValue, userName, id);
            var reportDataModel = JsonConvert.DeserializeObject<ReportDataResponseModel>(_response.Content);
            var groups = reportDataModel?.Data.Data.Groups;

            if (groups != null)
            {
                var actionData = string.Empty;
                foreach (var action in groups.Select(@group => @group.DataRows).SelectMany(rows =>
                             rows.Select(row => row.Actions).SelectMany(actions =>
                                 actions.Where(action => action.Id == "CancelAll"))))
                {
                    actionData = action.Data;
                }

                _response = await _openService.PostOpenProcessCancelActionAsync(sessionId, actionData, value, nxBaseUserValue,
                    userName, id);
                _response.IsSuccessful.Should().BeTrue();
            }
        }
    }
}
