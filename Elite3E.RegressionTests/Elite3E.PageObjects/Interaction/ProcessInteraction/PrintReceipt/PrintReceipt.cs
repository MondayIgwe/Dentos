using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.PrintReceipt
{
    public class PrintReceipt : ITask
    {
        public PrintEntity PrintEntity { get; }

        private PrintReceipt(PrintEntity printEntity) =>
            PrintEntity = printEntity;

        public static PrintReceipt With(PrintEntity printEntity) => new(printEntity);

        public void PerformAs(IActor _actor)
        {
            _actor.AttemptsTo(Click.On(CommonLocator.PrintReceipt));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(SendKeys.To(CommonLocator.PrintJobNameInput, PrintEntity.PrintJobName));
            _actor.AttemptsTo(Click.On(CommonLocator.PrintToScreenCheckbox));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            _actor.AttemptsTo(Dropdown.SelectOptionByName(CommonLocator.PrintTemplateDropdown, PrintEntity.Template));
            _actor.AttemptsTo(Click.On(CommonLocator.Print));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
