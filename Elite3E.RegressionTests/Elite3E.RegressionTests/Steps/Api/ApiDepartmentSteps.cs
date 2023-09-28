using Elite3E.Infrastructure.Extensions;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using System.Threading.Tasks;
using Elite3E.RegressionTests.DataCreators;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Elite3E.RestServices.Services;
using RestSharp;
using System;
using System.Linq;
using FluentAssertions;
using Elite3E.RegressionTests.DataCreators.DefaultData;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public class ApiDepartmentSteps
    {
        private readonly FeatureContext _featureContext;
        public IProcessService _process = new ProcessService();
        public ISessionService _session = new SessionService();
        public ILookUpService _lookUpService = new LookUpService();
        public IRestResponse _response;
        public DepartmentData _departmentService = new DepartmentData();

        public ApiDepartmentSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [Given(@"I search and create a department with api")]
        public async Task GivenIAddADepartmentWithApi(Table table)
        {
            var departmentEntity = table.CreateInstance<ApiDepartmentEntity>();

            departmentEntity.DepartmentCode.Should().NotBeNullOrEmpty();
            departmentEntity.Description.Should().NotBeNullOrEmpty();
            departmentEntity.GLDepartmentAlias.Should().NotBeNullOrEmpty();

            await _departmentService.SearchAndCreateDepartmentAsync(departmentEntity);

            _featureContext[StepConstants.DepartmentCode] = departmentEntity.DepartmentCode;
            _featureContext[StepConstants.DepartmentDescription] = departmentEntity.Description;
        }

        [Given(@"I search and create a dynamic department with api")]
        public async Task GivenIAddADynamicDepartmentWithApi()
        {
            var departmentEntity = DefaultRegionalValues.GetDynamicDepartmentDefaultValues();

            await new DepartmentData().SearchAndCreateDepartmentAsync(departmentEntity);

            _featureContext[StepConstants.DepartmentCode] = departmentEntity.DepartmentCode;
            _featureContext[StepConstants.DepartmentDescription] = departmentEntity.Description;
        }

    }
}
