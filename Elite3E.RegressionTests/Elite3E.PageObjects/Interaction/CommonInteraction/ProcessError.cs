using System.Collections.Generic;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class ProcessError : IQuestion<List<string>>
    {
        private ProcessError() {}

        public static ProcessError Messages() =>
            new();

        public List<string> RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Returns all error messages in the process as a list.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.WaitsUntil(Appearance.Of(CommonLocator.ErrorIcon),IsEqualTo.True(),3);
            actor.AttemptsTo(JScript.ClickOn(CommonLocator.ErrorIcon));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var messages = driver.FindElements(CommonLocator.ErrorMessages).Select(element => element.Text).ToList().Distinct().ToList();

            actor.AttemptsTo(JScript.ClickOn(CommonLocator.CloseFlyOutIcon));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            return messages;

        }
    }
    
}
