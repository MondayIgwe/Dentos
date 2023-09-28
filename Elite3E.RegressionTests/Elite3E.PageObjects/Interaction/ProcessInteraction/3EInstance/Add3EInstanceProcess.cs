using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator._3EInstance;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction._3EInstance
{
    public class Add3EInstanceProcess : ITask
    {
        public _3EInstanceEntity _3EInstanceEntity { get; }
        private Add3EInstanceProcess(_3EInstanceEntity instanceEntity) => _3EInstanceEntity = instanceEntity;

        public static Add3EInstanceProcess Add3EInstanceDetails(_3EInstanceEntity instanceTypeEntity) =>
            new(instanceTypeEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SendKeys.To(CommonLocator.Code, _3EInstanceEntity.Code));
            var webdriver = actor.Using<BrowseTheWeb>().WebDriver;
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(_3EInstanceEntity.Description);
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(Keys.Tab);
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Dropdown.SelectOptionByName(_3EInstanceLocator.RegionDropDown, _3EInstanceEntity.Region));       
            actor.AttemptsTo(Dropdown.SelectOptionByName(_3EInstanceLocator.InstanceTypeDropDown, _3EInstanceEntity.InstanceType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
    }
}
