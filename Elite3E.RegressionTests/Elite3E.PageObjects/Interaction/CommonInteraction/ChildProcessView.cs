using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class ChildProcessView : ITask
    {
        public string View { get; }
        public string ChildProcessSection { get; }

        private ChildProcessView(string childProcessSection, string view)
        {
            ChildProcessSection = childProcessSection;
            View = view;
        }

        public static ChildProcessView SwitchToView(string childProcessSection, string view) =>
            new(childProcessSection, view);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Changes the child process view to Grid, Form, Full-Form, etc.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var currentView = driver.FindElement(ChildFormProcessViewLocators.CurrentView(ChildProcessSection).Query).Text;

            if(currentView != View)
            {
                actor.AttemptsTo(Click.On(ChildFormProcessViewLocators.SelectActionDropDown(ChildProcessSection)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(Click.On(ChildFormProcessViewLocators.SelectOverlyButton(View)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }            
        }
       
    }
}
