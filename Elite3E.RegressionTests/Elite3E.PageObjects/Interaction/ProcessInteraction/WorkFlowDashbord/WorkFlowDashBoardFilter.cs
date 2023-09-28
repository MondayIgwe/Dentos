using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.WorkFlowDashbord
{
    public class WorkFlowDashBoardFilter : ITask
    {
        private string SearchText { get; }
        private string HeaderText { get; }

        public WorkFlowDashBoardFilter(string searchText, string headerText)
        {
            SearchText = searchText;
            HeaderText = headerText;
        }

        public static WorkFlowDashBoardFilter Search(string searchText, string headerText) =>
            new(searchText, headerText);

        public void PerformAs(IActor _actor)
        {
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.WaitsUntil(Appearance.Of(CommonLocator.FilterButton(HeaderText)), IsEqualTo.True());
            _actor.AttemptsTo(Click.On(CommonLocator.FilterButton(HeaderText)));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(CommonLocator.FilterInput(HeaderText), SearchText));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
