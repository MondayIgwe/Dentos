using System.Collections.Generic;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class GetAllDropdownValues : IQuestion<List<string>>
    {
        public IWebLocator Locator { get;}
        public IWebLocator DropDownOptionsLocator { get; }

        private GetAllDropdownValues(IWebLocator dropdownLocator, IWebLocator dropdownOptions )
        {
            Locator = dropdownLocator;
            DropDownOptionsLocator = dropdownOptions;
        }

        public static GetAllDropdownValues For(IWebLocator dropdownLocator, IWebLocator dropdownOptions ) =>
            new(dropdownLocator,dropdownOptions);

        public List<string> RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Returns all options in a drop down field as a list.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            driver.FindElement(Locator.Query).Click();

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var values = new List<string>();
            values.AddRange(driver.FindElements(DropDownOptionsLocator.Query).Select(ele => ele.Text.Trim()).ToList());

            return values.ToList();
        }
    }
    
}
