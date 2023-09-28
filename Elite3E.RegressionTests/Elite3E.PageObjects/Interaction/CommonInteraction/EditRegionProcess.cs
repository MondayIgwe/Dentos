using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class EditRegionProcess : ITask
    {
        public RegionEntity RegionDetails { get; }
        private EditRegionProcess(RegionEntity regionEntity) => RegionDetails = regionEntity;

        public static EditRegionProcess EditRegionDetails(RegionEntity regionEntity) => new(regionEntity);
        public void PerformAs(IActor actor)
        {
            var webdriver = actor.Using<BrowseTheWeb>().WebDriver;
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(RegionDetails.Description);
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(Keys.Tab);
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(CommonLocator.GetDisableIntegrationDiv));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            actor.WaitsUntil(Appearance.Of(CommonLocator.GetActiveCheckbox), IsEqualTo.False());

        }
    }
}
