using Elite3E.Infrastructure.Database;
using Elite3E.Infrastructure.Entity;
using Elite3E.RegressionTests.DataCreators.DefaultData;
using Elite3E.RegressionTests.StepHelpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Dapper;
using FluentAssertions;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.BOAMetric;
using Elite3E.Infrastructure.Selenium;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Extensions;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using TechTalk.SpecFlow.Assist;
using Elite3E.PageObjects.PageLocators;

namespace Elite3E.RegressionTests.Steps.Database
{
    [Binding]
    public class BOAMetricSteps
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;
        private readonly ITE_3E_DB _DB;

        public BOAMetricSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
            _DB = new TE_3E_DB();
        }

        [StepDefinition(@"I run the boa metric")]
        public void GivenIRunTheBoaMetric(Table table)
        {
            var boaMetricList = table.CreateSet<BOAMetricEntity>();

            List<string> tableNames = new List<string>();

            foreach (var boaMetric in boaMetricList)
            {
                //_actor.AttemptsTo(SearchProcess.ByName(boaMetric.Process));
                _actor.AttemptsTo(Click.On(CommonLocator.SearchIcon));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(SendKeys.To(CommonLocator.SearchInput, boaMetric.Process));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                if(!_actor.DoesElementExist(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)))
                {
                    _actor.PressKeyWithActions("enter");
                    _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }

                _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                string tableName = boaMetric.TableName + "_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+12");
                tableNames.Add(tableName);                

                _actor.WaitsUntil(Appearance.Of(CommonLocator.MetricTableNameInput), IsEqualTo.True());
                _actor.AttemptsTo(SendKeys.To(CommonLocator.MetricTableNameInput, tableName));

                if (!string.IsNullOrEmpty(boaMetric.Currency))
                {
                    _actor.AttemptsTo(Dropdown.SelectOptionByName(BOAMetricLocators.CurrencyInput, boaMetric.Currency));
                }

                if(!string.IsNullOrEmpty(boaMetric.EndDate))
                {
                    _actor.AttemptsTo(DateControl.SelectDate("End Date", boaMetric.EndDate));
                }

                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Save));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                _actor.AttemptsTo(ScrollToElement.At(BOAMetricLocators.RunMetricButton));
                _actor.AttemptsTo(Click.On(BOAMetricLocators.RunMetricButton));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                var message = _actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
                message.Should().Contain("created successfully");

                _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _featureContext[StepConstants.MetricTableNameContext] = tableNames;
        }


        [StepDefinition(@"I perform database validation for boa metric '([^']*)'")]
        public async Task GivenIPerformDatabaseValidationForWipReporting(string boa)
        {
            var tableNameList = (List<string>)_featureContext[StepConstants.MetricTableNameContext];
            string matter = _featureContext[StepConstants.MatterNumberContext].ToString();

            foreach (var tableName in tableNameList)
            {
                //Does Table Exist in DB
                var doesTableExistList = await _DB.DoesTableExistInDB(tableName);
                doesTableExistList.Should().NotBeNull();
                doesTableExistList.Any(x => x.TableName.Equals(tableName)).Should().BeTrue();

                IList<DbBOAReportEntity> dbReponse = null;

                if (tableName.Contains("ClientManagement"))
                {
                    dbReponse = await _DB.QueryBOATable_MxClientManagement_ccc(tableName);
                }
                else if (tableName.Contains("MatterAgedWIP"))
                {
                    dbReponse = await _DB.QueryBOATable_MxMatterWIP_ccc(tableName);
                }
                else if (tableName.Contains("InvestmentMetric"))
                {
                    dbReponse = await _DB.QueryBOATable_MxInvestment_ccc(tableName);
                }
                else if (tableName.Contains("MatterAgedAR"))
                {
                    dbReponse = await _DB.QueryBOATable_MxMatterAgedAR_ccc(tableName);
                }

                dbReponse.Should().NotBeNull();
                var row = dbReponse.FirstOrDefault(x => x.MatterNumber.Equals(matter));
                row.Should().NotBeNull();
                row.BOA.Contains(boa).Should().BeTrue();
            }
        }
    }
}
