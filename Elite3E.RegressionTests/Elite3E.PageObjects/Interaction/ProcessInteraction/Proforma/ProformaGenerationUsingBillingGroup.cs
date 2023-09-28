using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Proforma
{
    public class ProformaGenerationUsingBillingGroup :ITask
    {
        public string BillingGroup { get; }

        private ProformaGenerationUsingBillingGroup(string billingGroup) =>
            BillingGroup = billingGroup;

        public static ProformaGenerationUsingBillingGroup With(string billingGroup) => new(billingGroup);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, "Proforma Generation"));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(ProformaGenerationLocator.profGenerationCard));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.ProformaGeneration, ChildProcessMenuAction.Add));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.ProformaGeneration, GlobalConstants.Form));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Lookup.SearchAndSelectSingle("Billing Group", BillingGroup));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
