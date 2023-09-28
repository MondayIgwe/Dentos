using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction._3EInstance;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator._3EInstance;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.VolumeDiscount;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.VolumeDiscount
{
    public class CreateVolumeDiscount : ITask
    {
        public VolumeDiscountEntity _volumeDiscountEntity { get; }
        private CreateVolumeDiscount(VolumeDiscountEntity volumeDiscountEntity) => _volumeDiscountEntity = volumeDiscountEntity;

        public static CreateVolumeDiscount With(VolumeDiscountEntity volumeDiscountEntity) =>
            new(volumeDiscountEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SendKeys.To(CommonLocator.Code, _volumeDiscountEntity.Code));
            var webdriver = actor.Using<BrowseTheWeb>().WebDriver;
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(_volumeDiscountEntity.Description);
            webdriver.FindElement(CommonLocator.Description.Query).SendKeys(Keys.Tab);
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Dropdown.SelectOptionByName(VolumeDiscountLocator.Office, _volumeDiscountEntity.Office));
            actor.AttemptsTo(Dropdown.SelectOptionByName(VolumeDiscountLocator.ChargeType, _volumeDiscountEntity.ChargeType));
            actor.AttemptsTo(Dropdown.SelectOptionByName(VolumeDiscountLocator.IncreaseChargeType, _volumeDiscountEntity.IncreaseChargeType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }

    }
}
