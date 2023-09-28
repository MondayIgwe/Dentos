using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class Dropdown : ITask
    {
        public IWebLocator DropdownLocator { get;}
        public string OptionToSelect { get; }

        private Dropdown(IWebLocator dropdownLocator, string optionToSelect)
        {
            OptionToSelect = optionToSelect;
            DropdownLocator = dropdownLocator;
        }

        public static Dropdown SelectOptionByName(IWebLocator dropdownLocator, string optionToSelect) =>
            new(dropdownLocator, optionToSelect);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Selects the option within a drop down field
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
           actor.AttemptsTo(SendKeys.To(DropdownLocator, OptionToSelect));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            driver.FindElement(CommonLocator.DropDownValueToSelect(OptionToSelect).Query).Click();

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
    
}
