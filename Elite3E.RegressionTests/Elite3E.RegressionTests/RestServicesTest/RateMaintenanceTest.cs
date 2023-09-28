using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Services;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Services.RateMaintenance;
using Elite3E.RestServices.Models.ResponseModels.QuickSearch;
using Elite3E.RestServices.Models.ResponseModels.Session;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class RateMaintenanceTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public IRestResponse _response;
        public IRateMaintenance _rateMaintenance = new RateMaintenanceService();


        [Test]
        public async Task CreateRate()
        {
            var rateMaintenance = new ApiRateMaintenanceEntity()
            {
                Code = "Rates_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+9"),
                Description = "RatesDesc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                RateTypeDescription = "Standard Rate"
            };

            await new RateMaintenanceData().SearchAndCreateRateAsync(rateMaintenance);

        }
    }
}
