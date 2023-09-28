using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.DoA;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.DoARole
{
    public class GetDoARoleData : IQuestion<DoAEntity>
    {
        public string DoaCode;

        private GetDoARoleData(string doaCode)
        {
            DoaCode = doaCode;
        }

        public static GetDoARoleData Data(string DoaCode) => new(DoaCode);

        public DoAEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(QuickFind.Search(DoaCode));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(DoALocators.DoARolesCard));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var doaEntity = new DoAEntity()
            {
                Unit = driver.FindElement(DoALocators.UnitInput.Query).GetAttribute("value"),
                Office = driver.FindElement(DoALocators.RoleDiv("Office").Query).Text.Trim(),
                Department = driver.FindElement(DoALocators.RoleDiv("Department").Query).Text.Trim(),
                Role = driver.FindElement(DoALocators.RoleDiv("NxRole").Query).Text.Trim()
            };

            return doaEntity;
        }
    }
}



