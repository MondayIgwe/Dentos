using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Entity
{
    public class GetContactEmailEntityData : IQuestion<PersonEntity>
    {
        private GetContactEmailEntityData()
        {
        }

        public static GetContactEmailEntityData Data() =>
            new GetContactEmailEntityData();

        public PersonEntity RequestAs(IActor actor)
        {

            var driver = actor.Using<BrowseTheWeb>().WebDriver;
           
            var entityPerson = new PersonEntity()
            {
                Email = driver.FindElement(PersonEntityLocator.EmailAddress.Query).GetAttribute("value"),
                EmailDescription = driver.FindElement(PersonEntityLocator.EmailDescription.Query).GetAttribute("value"),
                StartDate = driver.FindElement(PersonEntityLocator.StartDate.Query).GetAttribute("value"),
                EndDate = driver.FindElement(PersonEntityLocator.EndDate.Query).GetAttribute("value")
            };

            return entityPerson;
        }

    }
}
