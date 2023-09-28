using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.ContactType;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.ContactType
{
    public class GetContactTypeValues : IQuestion<ContactTypeEntity>
    {

        private GetContactTypeValues()
        {
        }

        public static GetContactTypeValues Data() =>
            new GetContactTypeValues();

        public ContactTypeEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var contactType = new ContactTypeEntity()
            {
                code = driver.FindElement(ContactTypeLocators.codeValue.Query).Text.Trim(),
                description = driver.FindElement(ContactTypeLocators.description.Query).GetAttribute("value"),
            };

            return contactType;
        }

    }
}
