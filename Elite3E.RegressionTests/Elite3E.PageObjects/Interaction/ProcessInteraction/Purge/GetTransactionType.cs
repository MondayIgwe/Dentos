using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class GetTransactionType : IQuestion<TransactionTypeEntity>
    {

        private GetTransactionType()
        {
        }

        public static GetTransactionType Data() => new();

        public TransactionTypeEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver; 
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var transactionTypeEntity = new TransactionTypeEntity();

            transactionTypeEntity.TransactionType = actor.AsksFor(Text.Of(TransactionTypeLocator.GetCode)).Trim();

            transactionTypeEntity.Description = actor.AsksFor(ValueAttribute.Of(TransactionTypeLocator.Description)).Trim();

            transactionTypeEntity.Anticipated =
                driver.FindElements(TransactionTypeLocator.GetCheckBox.Query)[6].Selected
                    ? "Yes"
                    : "No";

            transactionTypeEntity.CurrencyType = actor.AsksFor(ValueAttribute.Of(TransactionTypeLocator.CurrencyType)).Trim();
                
            return transactionTypeEntity;
        }
    }
    
}
