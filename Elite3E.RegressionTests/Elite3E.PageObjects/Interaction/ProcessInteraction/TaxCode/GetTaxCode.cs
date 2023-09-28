using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxCode;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.TaxCode
{
    public class GetTaxCode : IQuestion<TaxCodeEntity>
    {

        private GetTaxCode()
        {
        }

        public static GetTaxCode Data() => new();

        public TaxCodeEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var taxCode = new TaxCodeEntity
            {
                Code = driver.FindElement(TaxCodeLocator.GetCode.Query).Text.Trim(),
                Description = actor.AsksFor(ValueAttribute.Of(TaxCodeLocator.Description)).Trim(),
                TaxToolRef = actor.AsksFor(ValueAttribute.Of(TaxCodeLocator.TaxToolRef)).Trim()
            };
            return taxCode;
        }
    }
    
}
