using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Proforma
{
    public class PEPurge : ITask
    {
        private PEPurge() { }

        public static PEPurge Purge() => new();

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(ChildFormProcessViewLocators.SelectFlyOutMenu(GlobalConstants.DisbursementDetails, "Reload")));

            actor.AttemptsTo(Click.On(PurgeLocator.PurgeButton));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(JScript.ClickOn(PurgeLocator.OkButton));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}

