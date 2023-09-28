using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Entity
{
    public class EnterContactEmailEntity : ITask
    {

        public PersonEntity PersonEntity { get; }

        private EnterContactEmailEntity(PersonEntity personEntity) =>
            PersonEntity = personEntity;

        public static EnterContactEmailEntity With(PersonEntity personEntity) =>
            new EnterContactEmailEntity(personEntity);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(ScrollToElement.At(PersonEntityLocator.ContactEmail));
            actor.AttemptsTo(Click.On(PersonEntityLocator.ContactEmail));
            actor.AttemptsTo(ChildProcessMenu.ClickOn("Contact Emails", ChildProcessMenuAction.Add));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(PersonEntityLocator.EmailAddress, PersonEntity.Email));
            actor.AttemptsTo(SendKeys.To(PersonEntityLocator.EmailDescription, PersonEntity.EmailDescription));
            actor.AttemptsTo(SendKeys.To(PersonEntityLocator.StartDate, PersonEntity.StartDate));
            actor.AttemptsTo(SendKeys.To(PersonEntityLocator.EndDate, PersonEntity.EndDate));

            if(PersonEntity.IsEmailDefault!=null)
            {
                actor.AttemptsTo(Checkbox.SetStatus(PersonEntityLocator.EmailIsDefaultCheckbox, (bool)PersonEntity.IsEmailDefault));
            }
        }

    }
}
