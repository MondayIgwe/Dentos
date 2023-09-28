using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class GetDisbursementType : IQuestion<DisbursementTypeEntity>
    {

        private GetDisbursementType()
        {
        }

        public static GetDisbursementType Data() => new();

        public DisbursementTypeEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var disbursementTypeEntity = new DisbursementTypeEntity();

            disbursementTypeEntity.Code = actor.AsksFor(Text.Of(DisbursementTypeLocator.GetCode)).Trim();

            disbursementTypeEntity.Description = actor.AsksFor(ValueAttribute.Of(DisbursementTypeLocator.Description)).Trim();

            disbursementTypeEntity.HardDisbursement =
                driver.FindElements(DisbursementTypeLocator.GetCheckBox.Query)[2].Selected
                    ? "Yes"
                    : "No";

            disbursementTypeEntity.TransactionType = actor.AsksFor(ValueAttribute.Of(DisbursementTypeLocator.TransactionType)).Trim();
                
            return disbursementTypeEntity;
        }
    }
    
}
