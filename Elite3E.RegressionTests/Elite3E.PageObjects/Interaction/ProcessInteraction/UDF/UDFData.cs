using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.UDF
{
    public class UDFData : ITask
    {
        public UDFEntity UDFEntity { get; }

        private UDFData(UDFEntity udfEntity) =>
            UDFEntity = udfEntity;

        public static UDFData EnterData(UDFEntity udfEntity) => new(udfEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SendKeys.To(UDFLocators.code, UDFEntity.Code));
            actor.AttemptsTo(SendKeys.To(UDFLocators.description, UDFEntity.Description));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(UDFLocators.label, UDFEntity.Label));
            actor.AttemptsTo(SendKeys.To(UDFLocators.typeInput, UDFEntity.Type));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(UDFLocators.activity(UDFEntity.Type)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
