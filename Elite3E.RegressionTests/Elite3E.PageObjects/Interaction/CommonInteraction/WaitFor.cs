using System;
using System.Threading;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class WaitFor : ITask
    {
        private WaitFor() {}

        public static WaitFor BackgroundProcessToComplete() =>
            new();

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Waits for the page to load.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            int timeout = (5 * 60 * 4); //5 minutes
            //int timeout = 1250; // 5 minutes 1250 / 4 = 312.5 seconds == 5 minutes 12 seconds
            int counter = 0;
            while (counter < timeout)
            {
                try
                {
                    var element = driver.FindElement(CommonLocator.ActiveProcess.Query);
                    Thread.Sleep(TimeSpan.FromMilliseconds(250));
                    counter++;
                    continue;
                }
                catch(Exception)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                    break;
                }
            }
        }
    }
    
}
