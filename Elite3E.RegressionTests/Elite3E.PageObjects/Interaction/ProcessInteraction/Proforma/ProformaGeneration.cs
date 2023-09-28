using System;
using System.Threading;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Proforma;


namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Proforma
{
    public class ProformaGeneration : ITask
    {
        public string MatterNumber { get; }

        private ProformaGeneration(string matterNumber) =>
            MatterNumber = matterNumber;

        public static ProformaGeneration AddMatter(string matterNumber) => new(matterNumber);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, "Proforma Generation"));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            actor.AttemptsTo(Click.On(ProformaGenerationLocator.profGenerationCard));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.ProformaGeneration,ChildProcessMenuAction.Add));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.ProformaGeneration, GlobalConstants.Form));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            Thread.Sleep(TimeSpan.FromMilliseconds(1500));

            if(actor.DoesElementExist(ProformaGenerationLocator.MatterInput))
            {

                actor.AttemptsTo(Lookup.SearchAndSelectSingle("Matter", MatterNumber));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                return;
            }

            actor.AttemptsTo(Hover.Over(ProformaGenerationLocator.MatterCell));
            actor.AttemptsTo(Click.On(ProformaGenerationLocator.MatterCell));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.GetDriver().FindElement(ProformaGenerationLocator.MatterInput.Query).SendKeys(MatterNumber);
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.PressKeyWithActions("tab");
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
