using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class GetTransactionTypeStandardPosting : IQuestion<TransactionTypeStandardPostingEntity>
    {

        public string SectionName { get; }

        private GetTransactionTypeStandardPosting(string sectionName)
        {
            SectionName = sectionName;
        }

        public static GetTransactionTypeStandardPosting Data(string sectionName) => new(sectionName);
        
        public TransactionTypeStandardPostingEntity RequestAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, SectionName));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var transactionTypeStandardPostingEntity = new TransactionTypeStandardPostingEntity
            {
                GlType = actor.AsksFor(ValueAttribute.Of(TransactionTypeStandardPostingLocator.GlType)).Trim()
            };
            
            var ar = actor.AsksFor(ValueAttribute.Of(TransactionTypeStandardPostingLocator.GlUnit)).Trim() + "-";
            ar = ar + actor.AsksFor(ValueAttribute.Of(TransactionTypeStandardPostingLocator.GlNatural)).Trim() + "-";
            ar = ar + actor.AsksFor(ValueAttribute.Of(TransactionTypeStandardPostingLocator.GlUnitLocal)).Trim() + "-";
            ar = ar + actor.AsksFor(ValueAttribute.Of(TransactionTypeStandardPostingLocator.GlDepartment)).Trim() + "-";
            ar = ar + actor.AsksFor(ValueAttribute.Of(TransactionTypeStandardPostingLocator.GlSection)).Trim() + "-";
            ar = ar + actor.AsksFor(ValueAttribute.Of(TransactionTypeStandardPostingLocator.GlOffice)).Trim() + "-";
            ar = ar + actor.AsksFor(ValueAttribute.Of(TransactionTypeStandardPostingLocator.GlTimekeeper)).Trim();

            transactionTypeStandardPostingEntity.Ar = ar;

            return transactionTypeStandardPostingEntity;
        }
    }
    
}
