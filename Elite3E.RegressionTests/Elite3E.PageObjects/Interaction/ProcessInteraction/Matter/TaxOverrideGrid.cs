using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class TaxOverrideGrid : IQuestion<TaxOverrideEntity>
    {
        private TaxOverrideGrid()
        {
        }

        public static TaxOverrideGrid Data() => new();

        public TaxOverrideEntity RequestAs(IActor actor)
        {
            
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            var gridRoot = driver.FindElement(TaxOverrideLocator.GridRoot.Query);

            var grid = gridRoot.FindElement(TaxOverrideLocator.GridLocator.Query);

            var row = grid.FindElement(TaxOverrideLocator.GridRows.Query);

            var columns = row.FindElements(TaxOverrideLocator.GridColumns.Query);
            var taxOverride = new TaxOverrideEntity()
            {
                Country = columns[0].Text,
                TaxAreaOverride = columns[1].Text
            };

            return taxOverride;

        }
    }
    
}
