using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Reciepts;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Receipts
{
    public class EnterReceiptsCheckboxOverride : ITask
    {


        private EnterReceiptsCheckboxOverride() {}


        public static EnterReceiptsCheckboxOverride Select() => new();

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            driver.FindElement(GLRecieptsLocators.GetRecieptbox.Query).Click();
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
