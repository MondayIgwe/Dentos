using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Extensions;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class EnterTransactionType : ITask
    {
        public TransactionTypeEntity TransactionType { get; }

        private EnterTransactionType(TransactionTypeEntity transactionType) =>
            TransactionType = transactionType;

        public static EnterTransactionType With(TransactionTypeEntity transactionType) => new(transactionType);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(TransactionTypeLocator.CodeInput, TransactionType.TransactionType));
            actor.AttemptsTo(SendKeys.To(TransactionTypeLocator.Description, TransactionType.Description));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(TransactionTypeLocator.CurrencyType, TransactionType.CurrencyType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(TransactionType.Anticipated))
            {
                var defaultCheckboxGet = driver.FindElements(TransactionTypeLocator.GetCheckBox.Query)[6];
                var defaultCheckboxSet = driver.FindElements(TransactionTypeLocator.SetCheckBox.Query)[6];

                var selected = defaultCheckboxGet.Selected;

                if (!selected && TransactionType.Anticipated.ToBoolean())
                    defaultCheckboxSet.Click();
                else if (selected && !TransactionType.Anticipated.ToBoolean())
                    defaultCheckboxSet.Click();
            }

        }
    }
}
