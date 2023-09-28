using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Payor;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Payer
{
    public class EnterPayerUnitData : ITask
    {
        public List<PayerUnitEntity> PayerUnits { get; }

        private EnterPayerUnitData(List<PayerUnitEntity> payerUnits) =>
            PayerUnits = payerUnits;

        public static EnterPayerUnitData With(List<PayerUnitEntity> payerUnits) => new(payerUnits);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            
            string section = "Payor Unit";

            foreach (var payorUnit in PayerUnits)
            {
                actor.AttemptsTo(ChildProcessMenu.ClickOn(section,ChildProcessMenuAction.Add));

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                var input = driver.FindElement(PayorUnitLocator.InputLocator.Query);

                input.SendKeys(payorUnit.Description);

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                input.SendKeys(Keys.Tab);

                var nextInput = driver.FindElement(PayorUnitLocator.InputLocator.Query);

                if (!string.IsNullOrEmpty(payorUnit.Status))
                    nextInput.SendKeys(payorUnit.Status);

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
    }
}
