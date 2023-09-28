using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.Interaction.ProcessInteraction.TaxCode;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxCode;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.SetUpPropagation
{
    public class CreateOrUpdateOrDeletePropagation : ITask
    {

        public bool SetUpPropagationCount { get; }
        public SetupPropagationEntity SetupPropagationEntity { get; }

        private CreateOrUpdateOrDeletePropagation(SetupPropagationEntity setupPropagationEntity, bool setUpPropagationCount)
        {
            SetupPropagationEntity = setupPropagationEntity;
            SetUpPropagationCount = setUpPropagationCount;
        }

        public static CreateOrUpdateOrDeletePropagation With(SetupPropagationEntity setupPropagationEntity, bool setUpPropagationCount) => new(setupPropagationEntity, setUpPropagationCount);

        public void PerformAs(IActor actor)
        {
            var getCurrentRoleText = actor.GetElementText(SetUpPropagationLocators.Role);
            // Create/Update/Delete
            switch (SetupPropagationEntity.Action)
            {
                case Operation.Create:
                    if (!SetUpPropagationCount)
                        actor.AttemptsTo(Lookup.SearchAndSelectSingle("Process", SetupPropagationEntity.Process));
                    if (getCurrentRoleText != SetupPropagationEntity.Role)
                        actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.Role, SetupPropagationEntity.Role));
                    break;
                case Operation.Delete:
                    actor.AttemptsTo(DeleteProcess.ClickDelete());
                    break;
                case Operation.Update:
                    // we can only update the Role
                    if (getCurrentRoleText != SetupPropagationEntity.Role)
                        actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.Role, SetupPropagationEntity.Role));
                    break;
            }
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
