using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using OpenQA.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class EnterTransactionTypeStandardPosting : ITask
    {

        public const string StandardPostingSection = "Standard Postings";
        public TransactionTypeStandardPostingEntity TransactionTypeStandardPosting { get; }

        private EnterTransactionTypeStandardPosting(TransactionTypeStandardPostingEntity transactionTypeStandardPosting) =>
            TransactionTypeStandardPosting = transactionTypeStandardPosting;

        public static EnterTransactionTypeStandardPosting With(
            TransactionTypeStandardPostingEntity transactionTypeStandardPosting) => new(transactionTypeStandardPosting);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StandardPostingSection));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Lookup.SearchAndSelectSingle("GL Type", TransactionTypeStandardPosting.GlType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Dropdown.SelectOptionByName(TransactionTypeStandardPostingLocator.RevenueRecognition, TransactionTypeStandardPosting.RevenueRecognition));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(TransactionTypeStandardPostingLocator.ARSearchIcon));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, TransactionTypeStandardPosting.Ar + Keys.Enter));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(DoubleClick.On(TransactionTypeStandardPostingLocator.ArReturnResult(TransactionTypeStandardPosting.Ar)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }
    }
}
