using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.DoA;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.DoARole
{
    public class DoARole : ITask
    {
        public DoAEntity DoAEntity { get; }
        public string Row { get; }


        private DoARole(DoAEntity doaEntity, string row)
        {
            DoAEntity = doaEntity;
            Row = row;
        }


        public static DoARole EnterData(DoAEntity doaEntity, string row) => new(doaEntity, row);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.DoARoles, ChildProcessMenuAction.Add));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Dropdown.SelectOptionByName(DoALocators.UnitDropdown, DoAEntity.Unit));
            actor.AttemptsTo(Click.On(DoALocators.RowDiv(Row, "Office")));
            actor.AttemptsTo(Dropdown.SelectOptionByName(DoALocators.OfficeDropdown, DoAEntity.Office));
            actor.AttemptsTo(Click.On(DoALocators.RowDiv(Row, "Department")));
            actor.AttemptsTo(Dropdown.SelectOptionByName(DoALocators.DepartmentDropdown, DoAEntity.Department));
            actor.AttemptsTo(Click.On(DoALocators.RowDiv(Row, "NxRole")));
            actor.AttemptsTo(SendKeys.To(DoALocators.DoARoleInput, DoAEntity.Role));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}