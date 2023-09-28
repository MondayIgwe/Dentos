using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class FlyOutProcessErrors : ITask
    {
        private FlyOutProcessErrors()
        {
        }

        public static FlyOutProcessErrors Close() => new();

        public void PerformAs(IActor actor)
        {
            //Closes the error messages panel.
            actor.WaitsUntil(Appearance.Of(CommonLocator.CloseFlyOutIcon), IsEqualTo.True());
            actor.AttemptsTo(Click.On(CommonLocator.Submit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
    
}
