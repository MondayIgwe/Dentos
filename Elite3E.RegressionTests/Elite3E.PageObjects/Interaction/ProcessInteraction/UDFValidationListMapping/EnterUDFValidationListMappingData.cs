using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.UDFValidationListMapping
{
    public class EnterUDFValidationListMappingData : ITask
    {
        public UDFValidationListMappingEntity UDFMappingEntity { get; }

        private EnterUDFValidationListMappingData(UDFValidationListMappingEntity udfEntity) =>
            UDFMappingEntity = udfEntity;

        public static EnterUDFValidationListMappingData With(UDFValidationListMappingEntity udfEntity) => new(udfEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.Mapping, ChildProcessMenuAction.Add));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(UDFValidationListMappingLocators.parentValue, UDFMappingEntity.ParentValue + Keys.Tab));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(UDFValidationListMappingLocators.childValue, UDFMappingEntity.ChildValue + Keys.Enter));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
