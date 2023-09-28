using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using Elite3E.PageObjects.Interaction.CommonInteraction;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class EnterMatter : ITask
    {
        public MatterEntity MatterEntity { get; }

        private EnterMatter(MatterEntity matterEntity) =>
            MatterEntity = matterEntity;

        public static EnterMatter With(MatterEntity matterEntity) => new(matterEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(MatterLocator.MatterName, MatterEntity.MatterName ));
            
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(MatterEntity.StatusChangeDate))
            {
                actor.AttemptsTo(SendKeys.To(MatterLocator.StatusChangeDate, MatterEntity.StatusChangeDate));
                actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StatusChangeDate, MatterEntity.StatusChangeDate));

            }
            if (!string.IsNullOrEmpty(MatterEntity.OpenDate))
            {
                actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.OpenDate, MatterEntity.OpenDate));
            }
            actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
        }
    }
}
