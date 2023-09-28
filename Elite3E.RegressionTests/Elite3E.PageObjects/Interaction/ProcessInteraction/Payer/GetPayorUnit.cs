using System.Collections.Generic;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payer
{
    public class GetPayerUnit : IQuestion<List<PayerUnitEntity>>
    {
        private GetPayerUnit()
        {
        }

        public static GetPayerUnit Data() => new();

        public List<PayerUnitEntity> RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            var gridRoot = driver.FindElement(PayorUnitLocator.GridRoot.Query);

            var grid = gridRoot.FindElement(PayorUnitLocator.GridLocator.Query);

            var rows = grid.FindElements(PayorUnitLocator.GridRows.Query);

            return rows.Select(
                    row => row.FindElements(TaxOverrideLocator.GridColumns.Query))
                .Select(columns => new PayerUnitEntity()
                {
                    Description = columns[1].FindElement(By.TagName("input")).GetAttribute("value"),
                    Status = columns[2].Text
                })
                .ToList();
        }
    }
}
