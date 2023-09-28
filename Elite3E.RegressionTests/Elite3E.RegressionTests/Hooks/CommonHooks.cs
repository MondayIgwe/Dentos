using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Database;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.RegressionTests.DataCreators;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.Extensions;
using Elite3E.RegressionTests.StepHelpers;
using Elite3E.RestServices.Entity;
using TechTalk.SpecFlow;

namespace Elite3E.RegressionTests.Hooks
{
    [Binding]
    public class CommonHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;

        public CommonHooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _actor = (Actor)featureContext[StepHelpers.StepConstants.ActorInstance];

            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            await new CloseOpenProcess().CloseAllOpenProcessAsync();
            
            var rateTypeData = new RateTypeMaintenanceData();
            //Updating Fee Earner Rate Type: FEE_FIRM
            await rateTypeData.UpdateRateTypeAsync(DefaultRegionalValues.GetFeeEarnerRateTypeDefaultValues());
            //Updating Standard Rate Type: STD
            await rateTypeData.UpdateRateTypeAsync(DefaultRegionalValues.GetStandardRateTypeDefaultValues());
        }

        [BeforeScenario(@"LoginAsAdminUser1")]
        public void AdminLogin()
        {
            var tags = _scenarioContext.ScenarioInfo.Tags;
            var user = string.Empty;

            foreach (var tag in tags)
            {
                if (!tag.StartsWith(FeatureArguments.LoginAs))
                    continue;

                switch (tag)
                {
                    case FeatureArguments.LoginAsAdminUser1:
                        user = ApplicationConfigurationBuilder.Instance.AdminUser1;
                        break;
                    default:
                        throw new Exception($"{tag} : Insert Scripts not found");
                }
            }

            _actor.AttemptsTo(ProxyAs.User(user));

        }

        [AfterScenario(@"CancelProxy")]
        public void CancelProxyImpersonation()
        {
            _actor.AttemptsTo(ProxyUser.CancelProxy());
        }


        [BeforeScenario("PopulateEntityDataSet1", "PopulateEntityOrganisationSet1")]
        public void PopulateTestData()
        {
            var tags = _scenarioContext.ScenarioInfo.Tags;

            var db = new DbOperations();

            foreach (var tag in tags)
            {
                if (!tag.StartsWith(FeatureArguments.Populate))
                    continue;

                switch (tag)
                {
                    case FeatureArguments.PopulateEntityDataSet1:
                        //db.InsertData(PopulateCommands.EntityCommands);
                        break;
                    default:
                        throw new Exception($"{tag} : Insert Scripts not found");
                }
            }

        }

        [BeforeScenario("CleanupEntityDataSet1", "PopulateEntityOrganisationSet1")]
        public void CleanupTestData()
        {
            var db = new DbOperations();

            var tags = _scenarioContext.ScenarioInfo.Tags;

            foreach (var tag in tags)
            {
                if (!tag.StartsWith(FeatureArguments.Populate))
                    continue;

                switch (tag)
                {
                    case FeatureArguments.CleanupEntityDataSet1:
                        //db.DeleteData(CleanupCommands.EntityCommands);
                        break;
                    default:
                        throw new Exception($"{tag} : Delete Scripts not found");
                }
            }
        }

        [AfterScenario("CancelProcess")]
        public void CancelProcess()
        {
            try
            {
                _actor.WaitsUntil(Appearance.Of(CommonLocator.Close), IsEqualTo.True(), 0);
                _actor.AttemptsTo(Click.On(CommonLocator.Close));
            }
            catch (Exception ex)
            {
                Console.Write("Search Prompt not visible " + ex.Message);
            }
            try
            {
                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Cancel));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred when clicking the cancel button: " + ex.Message);
            }
        }


        [AfterStep()]
        public void TakeScreenShot()
        {
            if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                _featureContext.TakeScreenShot(_scenarioContext);
            }
        }



        [AfterScenario()]
        public async Task AfterScenarioKillProcesses()
        {
            if (_scenarioContext.ScenarioExecutionStatus != ScenarioExecutionStatus.TestError)
            {
                return;
            }

            var tagList =  new List<string>
            {
                "MultiUserLeaverWorkflow"
            };

            var scenarioTags = _scenarioContext.ScenarioInfo.ScenarioAndFeatureTags.ToList();

            foreach (var tag in tagList)
            {
                if(scenarioTags.Any(x => x.Equals(tag)))
                {
                    await new CloseOpenProcess().CloseAllOpenProcessAsync();
                    break;
                }
            }
        }
    }
}
