using Elite3E.RegressionTests.RestServicesTest.Common;
using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Elite3E.RestServices.Services.RateType;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RestServices.Constants;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.ResponseModels.Process;
using Elite3E.RestServices.Models.ResponseModels.Session;
using Elite3E.RestServices.Services;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.Infrastructure.Extensions;

namespace Elite3E.RegressionTests.RestServicesTest
{
    public class RateTypeTest
    {
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public RateTypeService _rateTypeService = new();
        public IRestResponse _response;
        private readonly RateTypeMaintenanceData _rateTypeMaintenanceData = new();

        [Test]
        public async Task FeeEarnerRateTypeAsync()
        {
            var rateType = new ApiRateTypeEntity()
            {
                RateTypeCurrencyDisplayName = "GBP - British Pound",
                RateTypeCode = "DummyFee",
                RateTypeDescription = "DummyFee",
                IsTimeKeeperCheckbox = "Yes",
                IsFirmDefaultCheckbox = "Yes",
                IsStandardRateCheckbox = "No"
            };

            await _rateTypeMaintenanceData.CreateRateTypeAsync(rateType);
        }

        [Test]
        public async Task StandardRateTypeAsync()
        {
            var rateType = new ApiRateTypeEntity()
            {
                RateTypeCurrencyDisplayName = "GBP - British Pound",
                RateTypeCode = "DummyStand",
                RateTypeDescription = "DummyStand",
                IsStandardRateCheckbox = "Yes",
                IsValidForTimekeeperCheckboxes = "Yes",
                IsValidForMatterCheckboxes = "Yes",
                EffectiveDate = StepArgumentExtension.ReplaceDynamicValues("{Today}-1", "yyyy-MM-dd"), //EffectiveDate Is required for all Rate Types not Fee Earners
                DefaultRateAmount = "100"
            };

            await _rateTypeMaintenanceData.CreateRateTypeAsync(rateType);
        }

        [Test]
        public async Task DisbursementRateTypeAsync()
        {
            var rateType = new ApiRateTypeEntity()
            {
                RateTypeCurrencyDisplayName = "GBP - British Pound",
                RateTypeCode = "DummyDisburs",
                RateTypeDescription = "DummyDisburs",
                IsDisbursementCheckbox = "Yes",
                IsValidForTimekeeperCheckboxes = "Yes",
                IsValidForMatterCheckboxes = "Yes",
                EffectiveDate = StepArgumentExtension.ReplaceDynamicValues("{Today}-1", "yyyy-MM-dd"), //EffectiveDate Is required for all Rate Types not Fee Earners
                DefaultRateAmount = "100"
            };

            await _rateTypeMaintenanceData.CreateRateTypeAsync(rateType);
        }   

        [Test]
        public async Task UpdateFeeEarnerFirmDefaultRateTypeAsync()
        {
            await _rateTypeMaintenanceData.UpdateRateTypeAsync(DefaultRegionalValues.GetFeeEarnerRateTypeDefaultValues());
        }     

        [Test]
        public async Task UpdateStandardRateTypeAsync()
        {
            await _rateTypeMaintenanceData.UpdateRateTypeAsync(DefaultRegionalValues.GetStandardRateTypeDefaultValues());
        }      
    }
}
