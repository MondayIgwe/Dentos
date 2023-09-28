using System;
using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;


namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class AddChildProcess : ITask
    {
        public List<string> ChildProcessNames { get; }

        private AddChildProcess(List<string> childProcessNames)
        {
            ChildProcessNames = childProcessNames;
        }

        public static AddChildProcess ByName(List<string> childProcessNames) => new(childProcessNames);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //Attempts to add a child form via the tabbed view.
            try
            {
                actor.WaitsUntil(Appearance.Of(CommonLocator.ChildProcessMenuButton), IsEqualTo.True(), 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                actor.AttemptsTo(Click.On(CommonLocator.SwitchToView(LocatorConstants.TabView)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            actor.AttemptsTo(Click.On(CommonLocator.ChildProcessMenuButton));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            foreach (var name in ChildProcessNames)
            {
               driver.FindElement(CommonLocator.ChildProcessName(name).Query).Click();
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            actor.AttemptsTo(JScript.ClickOn(CommonLocator.AddChildProcessButton));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
    
}
