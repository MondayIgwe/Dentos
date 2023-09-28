using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.PhaseList;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.PhaseTaskActivity
{
    public class EnterFirmListData : ITask
    {
        public FirmListEntity FirmListEntity { get; }
        public string FirmType { get; }

        private EnterFirmListData(FirmListEntity firmListEntity, string firmType)
        {
            FirmListEntity = firmListEntity;
            FirmType = firmType;
        }
           
        public static EnterFirmListData EnterFirmTypeListData(FirmListEntity firmListEntity, string firmType) => new(firmListEntity, firmType);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(QuickFind.Search(FirmListEntity.FirmListCode));

            if (actor.DoesElementExist(CommonLocator.NoSearchRecords))
            {
                actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView,FirmType));
                actor.AttemptsTo(SendKeys.To(PhaseListLocators.FirmPhaseListCode, FirmListEntity.FirmListCode));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(SendKeys.To(PhaseListLocators.Description, FirmListEntity.FirmListDescription));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

        }
    }
}
