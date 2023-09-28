using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction._3EInstance;
using Elite3E.PageObjects.Interaction.ProcessInteraction.VolumeDiscount;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator._3EInstance;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.VolumeDiscount;
using Elite3E.RegressionTests.StepHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;



namespace Elite3E.RegressionTests.Steps
{



    [Binding]
    public class VolumeDiscountStepDefinitions
    {
        private readonly Actor _actor;
        private readonly FeatureContext _featureContext;



        public VolumeDiscountStepDefinitions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _actor = (Actor)featureContext[StepConstants.ActorInstance];
        }



        [Given(@"I create a volume discount")]
        public void GivenICreateAVolumeDiscount(Table table)
        {
            var volumeEntity = table.CreateInstance<VolumeDiscountEntity>();
            _actor.AttemptsTo(SearchProcess.ByName(Process.VolumeDiscount));
            _actor.WaitsUntil(Existence.Of(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)), IsEqualTo.True());
            _actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            _actor.AttemptsTo(CreateVolumeDiscount.With(volumeEntity));
            _actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, StepConstants.VolumeDiscountGroupDate));

            _featureContext[StepConstants.VolumeDiscountGroup] = volumeEntity.Description;
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.VolumeDiscountGroupDate, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(DateControl.SelectDate("Effective Date", volumeEntity.EffectiveDate));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VolumeDiscountLocator.CalculationMethod, volumeEntity.CalculationMethod));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VolumeDiscountLocator.Currency, volumeEntity.Currency));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(ChildProcessMenu.ClickOn(StepConstants.Tiers, ChildProcessMenuAction.Add));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(VolumeDiscountLocator.TierAmount, volumeEntity.TierAmount + Keys.Tab + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.DoesElementExist(VolumeDiscountLocator.DiscountPercent).Should().BeTrue();
            _actor.AttemptsTo(SendKeys.To(VolumeDiscountLocator.DiscountPercent, volumeEntity.DiscountPercent + Keys.Tab));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(MainProcessMenu.ClickOn(MainProcessMenuAction.Update));
            _actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));

        }


    }
}
