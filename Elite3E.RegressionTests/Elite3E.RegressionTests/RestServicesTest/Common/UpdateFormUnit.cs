using System;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Services;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RestServices.Models.ModelHelper;
using System.Text.RegularExpressions;

namespace Elite3E.RegressionTests.RestServicesTest.Common
{
    public class UpdateFormUnit
    {
        public static IRestResponse _response;
        public static IUpdateFormUnitService _updateUnitService = new UpdateFormUnitService();

        public static async Task ChangeFormUnitAsync(string sessionId, string processItemId, string unitToChangeTo)
        {
            _response = await _updateUnitService.GetUnitsAnync(sessionId, processItemId);
            _response.IsSuccessful.Should().BeTrue();

            var lookupItems = JsonConvert.DeserializeObject<Folder>(_response.Content).UnitList.Items;
            lookupItems.Any(x => x.Key.Equals(unitToChangeTo)).Should().BeTrue();

            _response = await _updateUnitService.PatchNewUnitAsync(sessionId, processItemId, unitToChangeTo);
            _response.IsSuccessful.Should().BeTrue();
        }
    }
}
