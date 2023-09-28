using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using OpenQA.Selenium;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class EnterTaxOverride : ITask
    {
        public TaxOverrideEntity TaxOverrideEntity { get; }
        const string Section = "Tax Override";

        private EnterTaxOverride(TaxOverrideEntity taxOverrideEntity) =>
            TaxOverrideEntity = taxOverrideEntity;

        public static EnterTaxOverride With(TaxOverrideEntity taxOverrideEntity) => new(taxOverrideEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(ChildProcessMenu.ClickOn(Section,ChildProcessMenuAction.Add));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Dropdown.SelectOptionByName(TaxOverrideLocator.CountryInput, TaxOverrideEntity.Country));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.PressKeyWithActions("tab");
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Dropdown.SelectOptionByName(TaxOverrideLocator.TaxAreaInput, TaxOverrideEntity.TaxAreaOverride));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.PressKeyWithActions("tab");
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
