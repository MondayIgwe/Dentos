using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
   public class Checkbox: ITask
    {
        public bool IsChecked { get; }
        public IWebLocator Locator { get; }

        private Checkbox(IWebLocator locator, bool isChecked)
        {
            Locator = locator; 
            IsChecked = isChecked;
        }

        public static Checkbox SetStatus(IWebLocator locator, bool IsChecked) => new(locator, IsChecked);
            
        public void PerformAs(IActor actor)
        {
            //Wait for Checkbox
            actor.WaitsUntil(Appearance.Of(Locator), IsEqualTo.True());

            int counter = 0;
            int max = 3;

            while (getCurrentCheckBoxState(actor) != IsChecked && counter < max)
            {
                if (counter == 0)
                {
                    actor.AttemptsTo(JScript.ClickOn(Locator));
                }
                else
                {
                    actor.FindOne(Locator).Click();
                    //new _actor(actor).FindOne(Locator).Click(); 
                }
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                counter++;
            }
        }

        private bool getCurrentCheckBoxState(IActor actor)
        {
            var ele = actor.FindOne(Locator);

            //Get checkbox status first using aria-checked then class then SelectedState.
            string checkboxAriaCheckedAttri = ele.GetAttribute("aria-checked");
            string checkboxAriaLabelAttri = ele.GetAttribute("aria-label");
            string checkboxClassAttri = ele.GetAttribute("class");
            bool currentCheckboxState = false;

            //Regalur checkboxes have the aria checked attribute. Try this first.
            if (!string.IsNullOrEmpty(checkboxAriaCheckedAttri))
            {
                currentCheckboxState = bool.Parse(checkboxAriaCheckedAttri);
            }
            //Grid Checkboxes have a class variable with checked status. Try this second
            else if (!string.IsNullOrEmpty(checkboxAriaLabelAttri))
            {
                currentCheckboxState = checkboxClassAttri.ToLower().Contains("checked");
            }
            //Grid Checkboxes have a class variable with checked status. Try this second
            else if (!string.IsNullOrEmpty(checkboxClassAttri))
            {
                currentCheckboxState = checkboxClassAttri.ToLower().Contains("checked");
            }
            //Try SelectedState if the above fail
            else
            {
                currentCheckboxState = actor.AsksFor(SelectedState.Of(Locator));
            }
            return currentCheckboxState;
        }

    }
}
