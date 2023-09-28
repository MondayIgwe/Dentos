using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Extensions;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using Elite3E.PageObjects.Interaction.CommonInteraction;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class EnterDisbursementType : ITask
    {
        public DisbursementTypeEntity DisbursementTypeEntity { get; }

        private EnterDisbursementType(DisbursementTypeEntity disbursementType) =>
            DisbursementTypeEntity = disbursementType;

        public static EnterDisbursementType With(DisbursementTypeEntity disbursementType) => new(disbursementType);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(DisbursementTypeLocator.CodeInput, DisbursementTypeEntity.Code));

            actor.AttemptsTo(SendKeys.To(DisbursementTypeLocator.Description, DisbursementTypeEntity.Description));

            if(!string.IsNullOrEmpty(DisbursementTypeEntity.TransactionType))
                actor.AttemptsTo(SendKeys.To(DisbursementTypeLocator.TransactionType, DisbursementTypeEntity.TransactionType));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(DisbursementTypeEntity.HardDisbursement))
            {
                var defaultCheckboxGet = driver.FindElements(TransactionTypeLocator.GetCheckBox.Query)[3];
                var defaultCheckboxSet = driver.FindElements(TransactionTypeLocator.SetCheckBox.Query)[3];

                var selected = defaultCheckboxGet.Selected;

                switch (selected)
                {
                    case false when DisbursementTypeEntity.HardDisbursement.ToBoolean():
                    case true when !DisbursementTypeEntity.HardDisbursement.ToBoolean():
                        defaultCheckboxSet.Click();
                        break;
                }
            }

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

        }
    }
}
