using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Region
{
    public class AddRegionProcess : ITask
    {
        public RegionEntity RegionDetails { get; }
        private AddRegionProcess(RegionEntity regionEntity) => RegionDetails = regionEntity;

        public static AddRegionProcess AddRegionDetails(RegionEntity regionEntity) => new(regionEntity);
        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SendKeys.To(CommonLocator.Code, RegionDetails.Code));

            var webdriver = actor.Using<BrowseTheWeb>().WebDriver;
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(RegionDetails.Description);
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(Keys.Tab);


            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (RegionDetails.IsActive && !actor.AsksFor(SelectedState.Of(CommonLocator.GetActiveCheckbox)))
            {
                actor.AttemptsTo(Click.On(CommonLocator.GetActiveCheckbox));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else if (!RegionDetails.IsActive && actor.AsksFor(SelectedState.Of(CommonLocator.GetActiveCheckbox)))
                actor.AttemptsTo(Click.On(CommonLocator.GetActiveCheckbox));

            bool state = actor.AsksFor(SelectedState.Of(CommonLocator.GetIsDisableIntegrationCheckbox));

            if (RegionDetails.DisableIntegration && !state)
            {
                actor.AttemptsTo(Click.On(CommonLocator.GetDisableIntegrationDiv));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else if (RegionDetails.IsActive && state)
            {
                actor.AttemptsTo(Click.On(CommonLocator.GetDisableIntegrationDiv));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

        }
    }
}

