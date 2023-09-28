using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class Field : IQuestion<bool>
    {
        public IWebLocator Locator { get; }
        private Field(IWebLocator locator) => Locator = locator;

        public static Field IsAvailable(IWebLocator locator) =>
            new(locator);

        public bool RequestAs(IActor actor)
        {
            //Checks if an element is displayed and enabled
            var element = actor.FindOne(Locator);

            return element.Displayed && element.Enabled;
        }
    }

}
