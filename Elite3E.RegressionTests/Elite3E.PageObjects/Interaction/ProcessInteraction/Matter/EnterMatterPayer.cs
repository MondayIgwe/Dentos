using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class EnterMatterPayer : ITask
    {
        public string payer { get; }
        public string startDate { get; }

        private EnterMatterPayer(string Payer, string StartDate)
        {
            payer = Payer;
            startDate = StartDate;
        }

        public static EnterMatterPayer With(string Payer, string StartDate) =>
            new EnterMatterPayer(Payer, StartDate);

        public void PerformAs(IActor _actor)
        {
           _actor.AttemptsTo(Click.On(MatterLocator.MatterPayer));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction("Matter Payer", LocatorConstants.AddButton)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(MatterLocator.MatterPayerDateInput, startDate+ Keys.Enter));
            _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction("Payer Detail", LocatorConstants.AddButton)));
            _actor.AttemptsTo(SendKeys.To(MatterLocator.MatterPayerDetail, payer + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
