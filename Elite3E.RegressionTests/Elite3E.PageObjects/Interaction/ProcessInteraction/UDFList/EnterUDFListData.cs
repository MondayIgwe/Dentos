using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.UDFList
{
    public class EnterUDFListData : ITask
    {

        public UDFEntity UDFEntity { get; }

        private EnterUDFListData(UDFEntity udfEntity) =>
            UDFEntity = udfEntity;

        public static EnterUDFListData With(UDFEntity udfEntity) => new(udfEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(UDFLocators.code, UDFEntity.Code));
            actor.AttemptsTo(SendKeys.To(UDFLocators.description, UDFEntity.Description));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
