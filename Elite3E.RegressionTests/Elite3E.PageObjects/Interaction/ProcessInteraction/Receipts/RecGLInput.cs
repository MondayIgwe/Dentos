using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Reciepts;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Receipts
{
    public class RecGLInput : IQuestion<string>
    {
        private RecGLInput()
        {
        }

        public static RecGLInput RecGLText() => new();

        public string RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            return driver.FindElement(GLRecieptsLocators.Gltypeinput.Query).Text;

        }
    }
    
}
