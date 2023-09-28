using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Entity;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Entity
{
    public class AddEntity : ITask
    {
        public IWebLocator ElementLocator { get; }

        private AddEntity(IWebLocator elementLocator) =>
            ElementLocator = elementLocator;

        public static AddEntity OfTypePerson(IWebLocator elementLocator) => new(elementLocator);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.QuickSearchAddButton)));
            actor.AttemptsTo(Click.On(ElementLocator));
            actor.AttemptsTo(Click.On(CommonLocator.ButtonElementContainsText("Select")));
        }
    }

    public class AddPersonEntity : ITask
    {
        public PersonEntity PersonEntity { get; }

        private AddPersonEntity(PersonEntity personEntity) =>
            PersonEntity = personEntity;

        public static AddPersonEntity With(PersonEntity personEntity) => new(personEntity);

        public void PerformAs(IActor actor)
        {
            if (!string.IsNullOrEmpty(PersonEntity.EntityType))
                actor.AttemptsTo(Dropdown.SelectOptionByName(PersonEntityLocator.EntityType, PersonEntity.EntityType));

            actor.AttemptsTo(SendKeys.To(PersonEntityLocator.FirstName, PersonEntity.FirstName));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(PersonEntityLocator.LastName, PersonEntity.LastName));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Dropdown.SelectOptionByName(PersonEntityLocator.FormatCode, PersonEntity.Format));
            if (!string.IsNullOrEmpty(PersonEntity.DoB))
                actor.AttemptsTo(SendKeys.To(PersonEntityLocator.DoB, PersonEntity.DoB));
        }
    }
}
