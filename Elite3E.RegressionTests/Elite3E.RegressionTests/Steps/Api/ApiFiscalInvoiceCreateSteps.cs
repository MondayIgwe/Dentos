using System.Threading.Tasks;
using Elite3E.Infrastructure.Configuration;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Elite3E.RegressionTests.Steps.Api
{
    [Binding]
    public sealed class ApiFiscalInvoiceCreateSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public ApiFiscalInvoiceCreateSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"there exists a fiscal invoice setup")]
        public async Task GivenThereExistsAFiscalInvoiceSetup(Table table)
         {
            var fiscalInvoiceSetupEntity = table.CreateInstance<FiscalInvoiceSetupEntity>();
            fiscalInvoiceSetupEntity.Unit = table.Rows[0][ColumnNames.Unit];
            fiscalInvoiceSetupEntity.FiscalInvoicePrefix = ApplicationConfigurationBuilder.Instance.FiscalInvoicePrefix;
            fiscalInvoiceSetupEntity.NextFiscalInvoiceNumber = "1";
            fiscalInvoiceSetupEntity.SuspenseGlTypeAlias = ApplicationConfigurationBuilder.Instance.SuspenseGlType;
            fiscalInvoiceSetupEntity.BillGlTypeAlias = ApplicationConfigurationBuilder.Instance.SuspenseGlType;

            await new FiscalInvoiceSetupData().CreateFiscalInvoiceSetup(fiscalInvoiceSetupEntity);
        }


    }
}
