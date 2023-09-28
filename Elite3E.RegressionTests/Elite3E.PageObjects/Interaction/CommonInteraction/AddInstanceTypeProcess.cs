using Boa.Constrictor.Screenplay;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Instance;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class AddInstanceTypeProcess : ITask
    {
        public InstanceTypeEntity instanceTypeDetails { get; }
        private AddInstanceTypeProcess(InstanceTypeEntity instanceTypeEntity) => instanceTypeDetails = instanceTypeEntity;

        public static AddInstanceTypeProcess AddInstanceTypeDetails(InstanceTypeEntity instanceTypeEntity) =>
            new(instanceTypeEntity);

        public void PerformAs(IActor actor)
        {
            var webdriver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(SendKeys.To(CommonLocator.Code, instanceTypeDetails.Code));      
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(instanceTypeDetails.Description);
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(Keys.Tab);             
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (instanceTypeDetails.IsActive && !actor.AsksFor(SelectedState.Of(CommonLocator.GetActiveCheckbox)))
            {
                actor.AttemptsTo(Click.On(CommonLocator.GetActiveCheckbox));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else if (!instanceTypeDetails.IsActive && actor.AsksFor(SelectedState.Of(CommonLocator.GetActiveCheckbox)))
                actor.AttemptsTo(Click.On(CommonLocator.GetActiveCheckbox));

            bool state = actor.AsksFor(SelectedState.Of(InstanceTypeLocators.GetIsProductionCheckbox));

            if (instanceTypeDetails.IsProduction && !state)
            {
                actor.AttemptsTo(Click.On(InstanceTypeLocators.GetIsProductionCheckbox));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            else if (instanceTypeDetails.IsActive && state)
            {
                actor.AttemptsTo(Click.On(InstanceTypeLocators.GetIsProductionCheckbox));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
    }
}
