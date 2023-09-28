using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.GlobalTimeKeeper
{
    public class GetGlobalTimekeeperValues : IQuestion<GlobalTImekeeperEntity>
    {

        private GetGlobalTimekeeperValues()
        {
        }

        public static GetGlobalTimekeeperValues Data() => new();

        public GlobalTImekeeperEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var globalTimekeeper = new GlobalTImekeeperEntity()
            {
                TimekeeperNumber = driver.FindElement(MatterLocator.timeKeeperValue.Query).GetAttribute("value"),
                TimekeeperName = driver.FindElement(MatterLocator.TimekeeperName.Query).GetAttribute("value"),
                TimekeeperOffice = driver.FindElement(MatterLocator.TimekeeperOffice.Query).GetAttribute("value"),
                TimekeeperTitle = driver.FindElement(MatterLocator.TimekeeperTitle.Query).GetAttribute("value"),
                
            };

            return globalTimekeeper;
        }
    }
}



