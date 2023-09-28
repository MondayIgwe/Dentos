using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class WorkList : ITask
    {
        public WorkListViewEntity WorkListView { get; }

        private WorkList(WorkListViewEntity workListView)
        {
            WorkListView = workListView;
        }

        public static WorkList View(WorkListViewEntity workListView) => new(workListView);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Changes the view to either worklist or folder in the main process.
            actor.AttemptsTo(Click.On(CommonLocator.VerticalMenu));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(WorkListView.ToString().Equals(WorkListViewEntity.Folder.ToString())
                ? Click.On(CommonLocator.FindButtonTagElementContainsText(LocatorConstants.VerticalMenuFolder))
                : Click.On(CommonLocator.FindButtonTagElementContainsText(LocatorConstants.VerticalMenuModelling)));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }

}
