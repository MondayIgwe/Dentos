using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Services.Office_Configuration;
using RestSharp;
using System;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Services;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiOfficeConfig
    {
        private readonly FeatureContext _featureContext;
        public IOfficeConfigurationService _officeConfig = new OfficeConfigurationService();
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IProcessDataService _processDataService = new ProcessDataService();
        public IRestResponse _response;
        private OfficeConfigurationData _officeConfigurationData = new();

        public ApiOfficeConfig(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I create the Office Configuraton Api")]
        public async Task GivenICreateTheOfficeConfiguratonApi(Table table)
        {
            var configDetails = table.CreateInstance<ApiOfficeConfiguration>();
            await CreateOfficeConfig(configDetails);
        }


        private async Task CreateOfficeConfig(ApiOfficeConfiguration officeConfiguration)
        {

            // check and create client account Data
            officeConfiguration.Office = await _officeConfigurationData.SearchAndCreateOfficeConfigurationDataAsync(officeConfiguration);

            if (!string.IsNullOrEmpty(officeConfiguration.Office))
            {
                _featureContext[StepConstants.OfficeConfig] = officeConfiguration.Office;
            }
            else
            {
                throw new Exception("Error occured in Api while creating Office COnfig");
            }
        }
    }
}
