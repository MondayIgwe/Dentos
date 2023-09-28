using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.CoA;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;


namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class ChildProcessGrid : IQuestion<List<string>>
    {
        public string ChildProcessSection { get; }


        private ChildProcessGrid(string childProcessSection)
        {
            ChildProcessSection = childProcessSection;
        }

        public static ChildProcessGrid GetGridColumnsHeaderText(string childProcessSection) => new(childProcessSection);

        public List<string> RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!actor.DoesElementExist(CoALocalLocators.LocalAccount))
            {
                if (actor.DoesElementExist(CommonLocator.ExpandButton))
                {
                    actor.AttemptsTo(Click.On(CommonLocator.ExpandButton));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
            }

            var headertext = new List<string>();

            var childProcessLocator = $"//span[contains(text(),'{ChildProcessSection}')]/parent::div[contains(@class,'compact-view')]/parent::e3e-form-anchor-view-header";

            var horizontalScrollLocator = "//e3e-form-anchor-view//div[@class='ag-body-horizontal-scroll']";

            var headerTextLocator = childProcessLocator + "/following-sibling::e3e-form//div[@ref='eLabel']/span[@ref='eText']";

            var headerElements = driver.FindElements(By.XPath(headerTextLocator));
            var lastColumnElement = "//span[text()='Statutory Account Firm Description']";

            //Get visible elements text and add to list 
            headertext.AddRange(headerElements.Where(element => !string.IsNullOrEmpty(element.Text))
                  .Where(element => !headertext.Contains(element.Text)).Select(element => element.Text));

            //Action to scroll to right 
            var action = new Actions(driver);
            action.ClickAndHold(driver.FindElement(By.XPath(horizontalScrollLocator)));
            action.MoveByOffset(750, 0).Build().Perform();
            action.MoveToElement(driver.FindElement(By.XPath(lastColumnElement))).Build().Perform();

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //Get list of elements after scroll and check element exists in the list and adds to list 
            headerElements = driver.FindElements(By.XPath(headerTextLocator));
            headertext.AddRange(headerElements.Where(element => !string.IsNullOrEmpty(element.Text))
              .Where(element => !headertext.Equals(element.Text)).Select(element => element.Text));
            return headertext;

        }

    }
}
