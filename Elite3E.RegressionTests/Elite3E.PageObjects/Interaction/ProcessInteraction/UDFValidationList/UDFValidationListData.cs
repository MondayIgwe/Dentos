using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.UDFValidationList
{
    public class UDFValidationListData : ITask
    {
        public List<UDFEntity> UDFEntity { get; }

        private UDFValidationListData(List<UDFEntity> udfEntity) =>
            UDFEntity = udfEntity;

        public static UDFValidationListData With(List<UDFEntity> udfEntity) => new(udfEntity);

        public void PerformAs(IActor actor)
        {
            foreach (var udfUnit in UDFEntity)
            {
                actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.ListItem, ChildProcessMenuAction.Add));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(SendKeys.To(UDFValidationListLocators.udfValidationItemCode, udfUnit.Code + Keys.Tab));

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(SendKeys.To(UDFValidationListLocators.udfValidationItemDescription, udfUnit.Description + Keys.Enter));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }
    }
}
