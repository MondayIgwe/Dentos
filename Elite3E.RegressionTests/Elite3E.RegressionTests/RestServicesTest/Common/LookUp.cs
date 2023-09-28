using System;
using Elite3E.RestServices.Models.ResponseModels.LookUpResponseModel;
using Elite3E.RestServices.Services;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;

namespace Elite3E.RegressionTests.RestServicesTest.Common
{
    public class LookUp
    {
        public static IRestResponse _response;
        public static ILookUpService _lookUpService = new LookUpService();

        public static async Task<string> GetLookUpKeyValue(string sessionId, string lookUpName, string diplayName)
        {
            _response = await _lookUpService.GetLookUpListAsync(sessionId, lookUpName);
            _response.IsSuccessful.Should().BeTrue();

            var lookupItems = JsonConvert.DeserializeObject<LookUpListItems>(_response.Content)?.Items;
            lookupItems.Any(x => x.Display.Equals(diplayName, StringComparison.CurrentCultureIgnoreCase)).Should().BeTrue();

            return lookupItems.FirstOrDefault(x => x.Display.Equals(diplayName, StringComparison.CurrentCultureIgnoreCase))?.Key;
        }

        public static async Task<string> GetCurrencyLookUpKeyValue(string sessionId, string diplayName)
        {
            var lookUpName = "NxCurrencyCode";
            _response = await _lookUpService.GetLookUpListAsync(sessionId, lookUpName);
            _response.IsSuccessful.Should().BeTrue();

            var lookupItems = JsonConvert.DeserializeObject<LookUpListItems>(_response.Content)?.Items;
            lookupItems.Any(x => x.Display.Contains(diplayName, StringComparison.CurrentCultureIgnoreCase)).Should().BeTrue();

            return lookupItems.FirstOrDefault(x => x.Display.Contains(diplayName, StringComparison.CurrentCultureIgnoreCase))?.Key;
        }



        public static async Task<string> GetDropDownAliasKeyFromTheList(string sessionId, string lookUpName, string diplayName = null)
        {
            _response = await _lookUpService.GetLookUpListAsync(sessionId, lookUpName);
            _response.IsSuccessful.Should().BeTrue();

            var lookupItems = JsonConvert.DeserializeObject<LookUpListItems>(_response.Content)?.Items;
            lookupItems.Count().Should().BeGreaterThan(0);

            return lookupItems.Any(alias => alias.Alias.Equals(diplayName, StringComparison.CurrentCultureIgnoreCase)) ? lookupItems.FirstOrDefault(alias => alias.Alias.Equals(diplayName, StringComparison.CurrentCultureIgnoreCase))?.Key : lookupItems.FirstOrDefault().Key;
        }

        public static async Task<string> GetDropDownDisplayNameKeyFromTheList(string sessionId, string lookUpName, string diplayName = null)
        {
            _response = await _lookUpService.GetLookUpListAsync(sessionId, lookUpName);
            _response.IsSuccessful.Should().BeTrue();

            var lookupItems = JsonConvert.DeserializeObject<LookUpListItems>(_response.Content)?.Items;
            lookupItems.Count().Should().BeGreaterThan(0);

            return lookupItems.Any(alias => alias.Display.Equals(diplayName, StringComparison.CurrentCultureIgnoreCase)) ? lookupItems.FirstOrDefault(alias => alias.Display.Equals(diplayName, StringComparison.CurrentCultureIgnoreCase))?.Key : lookupItems.FirstOrDefault().Key;
        }

        public static async Task<string> GetLookUpKeyValueByAliasAsync(string sessionId, string lookUpName, string alias)
        {
            _response = await _lookUpService.GetLookUpListAsync(sessionId, lookUpName);
            _response.IsSuccessful.Should().BeTrue();

            var lookupItems = JsonConvert.DeserializeObject<LookUpListItems>(_response.Content)?.Items;
            lookupItems.Any(x => x.Alias.Equals(alias, StringComparison.CurrentCultureIgnoreCase)).Should().BeTrue();

            return JsonConvert.DeserializeObject<LookUpListItems>(_response.Content)?.Items.FirstOrDefault(x => x.Alias.Equals(alias, StringComparison.CurrentCultureIgnoreCase))?.Key;
        }
        public static async Task<string> GetChildLookUpWithParameterAsync(string sessionId,string processItemId, string lookUpName, string diplayName, string parameter)
        {
            _response = await _lookUpService.GetChildDropDownLookUpListAsync(sessionId,processItemId, lookUpName,parameter);
            _response.IsSuccessful.Should().BeTrue();
            var items = JsonConvert.DeserializeObject<LookUpListItems>(_response.Content)?.Items;
            return items.FirstOrDefault(x => x.Display.Equals(diplayName, StringComparison.CurrentCultureIgnoreCase))?.Key;
        }
    }
}
